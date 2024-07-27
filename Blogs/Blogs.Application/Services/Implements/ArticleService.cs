using System.Linq.Expressions;
using Blogs.Application.Dtos.Articles;
using Blogs.Application.Dtos.BlogCategories;
using Blogs.Application.Services.Interfaces;
using Blogs.Domain.Agg.ArticleAgg;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Blogs.Application.Services.Implements;

internal class ArticleService : IArticleService
{

	private readonly IArticleRepository _articleRepository;
	private readonly IFileService _fileService;

	public ArticleService(IArticleRepository articleRepository, IFileService fileService)
	{
		_articleRepository = articleRepository;
		_fileService = fileService;
	}


	public OperationResult Create(CreateArticleDto dto)
	{
		if (_articleRepository.IsExists(x => x.Title == dto.Title.Trim()))
			return new(Status.BadRequest, ErrorMessages.DuplicateError, dto.Title);

		var slug = dto.Slug.GenerateSlug();

		if (_articleRepository.IsExists(x => x.Slug == slug))
			return new(Status.BadRequest, ErrorMessages.DuplicateError, dto.Slug);

		if (dto.ImageFile == null || !dto.ImageFile.IsImage())
			return new(Status.BadRequest, "لطفا یک عکس معتبر انخاب کنید!", dto.ImageFile?.Name);

		var imageName =
			_fileService.UploadFileAndReturnFileName(dto.ImageFile, Directories.ArticleImageDirectory);

		if (imageName is null)
			return new(Status.InternalServerError, "مشکلی هنگام آپلود عکس بوجود آمده است! دوباره تلاش کنید",
				dto.ImageFile.Name);

		_fileService.ResizeImage(imageName, Directories.ArticleImageDirectory400, 400);
		_fileService.ResizeImage(imageName, Directories.ArticleImageDirectory100, 100);

		if (_articleRepository.Insert(new Article(dto.Title,imageName,dto.ImageAlt,dto.UserId,dto.Author,dto.CategoryId,dto.SubCategoryId,slug,dto.Summary,dto.Content,dto.IsSpecial)))
			return new(Status.Success, "پست جدید ایجاد شد!");

		//if insert was not successful we need to delete uploaded images :
		_fileService.DeleteFile(imageName, Directories.ArticleImageDirectory);
		_fileService.DeleteFile(imageName, Directories.ArticleImageDirectory100);
		_fileService.DeleteFile(imageName, Directories.ArticleImageDirectory400);

		return new(Status.InternalServerError, "مشکلی در سرور وجود دارد!دوباره تلاش کنید.");


	}

	public OperationResult Edit(EditArticleDto dto)
	{

		try
		{
			var article = _articleRepository.GetBy(x => x.Id == dto.Id);
			if (article is null)
				throw new NullReferenceException();

			var editedSlug = dto.Slug.Trim().GenerateSlug();

			if (article.Slug != dto.Slug.Trim())
			{

				if (_articleRepository.IsExists(x => x.Slug == editedSlug))
					return new(Status.BadRequest, "اسلاگ تکراری است!", "Slug");
			}

			if (article.Title != dto.Title.Trim())
			{
				if (_articleRepository.IsExists(x => x.Title == dto.Title.Trim()))
					return new(Status.BadRequest, "عنوان تکراری است!", "Title");
			}
			if (dto.ImageFile != null)
			{
				var newImageName = _fileService.UploadFileAndReturnFileName(dto.ImageFile,
					Directories.ArticleImageDirectory);

				if (newImageName is null)
					return new(Status.InternalServerError, "مشکلی هنگام آپلود عکس بوجود آمده است! دوباره تلاش کنید",
						dto.ImageFile.Name);

				_fileService.ResizeImage(newImageName, Directories.ArticleImageDirectory400, 400);
				_fileService.ResizeImage(newImageName, Directories.ArticleImageDirectory100, 100);

				article.Edit(
					dto.Title,
					newImageName,
					dto.ImageAlt,
					dto.CategoryId,
					dto.SubCategoryId, editedSlug,
					dto.Summary,
					dto.Content,
					dto.IsSpecial);

				if (_articleRepository.Update(article))
					return new(Status.Success, "پست  ویرایش شد!");

				//if Update was not successful we need to delete uploaded images :
				_fileService.DeleteFile(newImageName, Directories.ArticleImageDirectory);
				_fileService.DeleteFile(newImageName, Directories.ArticleImageDirectory100);
				_fileService.DeleteFile(newImageName, Directories.ArticleImageDirectory400);

				return new(Status.InternalServerError, "مشکلی در سرور به وجود آمده! لطفا دوباره تلاش کنید.");

			}

			article.Edit(
				dto.Title,
				dto.ImageName,
				dto.ImageAlt,
				dto.CategoryId,
				dto.SubCategoryId, editedSlug,
				dto.Summary,
				dto.Content,
				dto.IsSpecial);

			if (_articleRepository.Update(article))
				return new(Status.Success, "پست  ویرایش شد!");

			return new(Status.InternalServerError, "مشکلی در سرور به وجود آمده! لطفا دوباره تلاش کنید.");

		}
		catch (Exception e)
		{
			return new(Status.InternalServerError, "مشکلی در سرور به وجود آمده! لطفا دوباره تلاش کنید.");
		}



	}

	public EditArticleDto GetForEdit(int articleId)
	{

		var article = _articleRepository.GetBy(x => x.Id == articleId);

		if (article is null)
			return new();

		return new EditArticleDto
		{
			Id = article.Id,
			Title = article.Title,
			ImageName = article.ImageName,
			ImageAlt = article.ImageAlt,
			ImageFile = null,
			CategoryId = article.CategoryId,
			SubCategoryId = article.SubCategoryId,
			Slug = article.Slug,
			Summary = article.Summary,
			Content = article.Content,
			IsSpecial = article.IsSpecial
		};

	}

	public bool Exists(Expression<Func<Article, bool>> expression)
	{
		return _articleRepository.IsExists(expression);
	}

	public void ActivationChange(int articleId)
	{
		var article = _articleRepository.GetBy(x => x.Id == articleId);

		if (article is not null)
		{
			article.ActivationChange();
			_articleRepository.Update(article);
		}
	}

	public void IncreaseVisits(int articleId)
	{
		var article = _articleRepository.GetBy(x => x.Id == articleId);

		if (article is not null)
		{
			article.IncreaseTotalVisits();
			_articleRepository.Update(article);
		}
	}

	public void Like(int articleId)
	{
		var article = _articleRepository.GetBy(x => x.Id == articleId);

		if (article is not null)
		{
			article.IncreaseLikes();
			_articleRepository.Update(article);
		}


	}

	public void DisLike(int articleId)
	{
		var article = _articleRepository.GetBy(x => x.Id == articleId);

		if (article is not null)
		{
			article.IncreaseDislikes();
			_articleRepository.Update(article);
		}


	}

	public void ChangeSpacialation(int articleId)
	{
		var article = _articleRepository.GetBy(x => x.Id == articleId);

		if (article is not null)
		{
			article.ChangeSpecialation();
			_articleRepository.Update(article);
		}

	}
}