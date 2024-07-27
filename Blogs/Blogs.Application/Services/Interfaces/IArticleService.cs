using Blogs.Application.Dtos.BlogCategories;
using Shared.Application.Utility;
using System.Linq.Expressions;
using Blogs.Application.Dtos.Articles;
using Blogs.Domain.Agg.ArticleAgg;

namespace Blogs.Application.Services.Interfaces;

public interface IArticleService
{
	OperationResult Create(CreateArticleDto dto);

	OperationResult Edit(EditArticleDto dto);

	EditArticleDto GetForEdit(int articleId);

	bool Exists(Expression<Func<Article,bool>> expression);

	void ActivationChange(int articleId);

	void IncreaseVisits(int articleId);

	void Like(int articleId);

	void DisLike(int articleId);

	void ChangeSpacialation (int articleId);
}