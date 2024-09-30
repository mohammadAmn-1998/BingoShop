using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Domain.BlogAgg;
using Blogs1.Domain.BlogAgg.IRepositories;
using Blogs1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;

namespace Blogs1.Infrastructure.Repositories
{
	internal class BlogCategoryRepository :BaseRepository , IBlogCategoryRepository
	{
		public BlogCategoryRepository(BlogContext context) : base(context)
		{
		}

		public async Task<BlogCategory?> GetBy(long categoryId)
		{
			try
			{
				return await GetById<BlogCategory>(categoryId);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public async Task<bool> Exists(Expression<Func<BlogCategory, bool>> expression)
			
			=> await Table<BlogCategory>().AnyAsync(expression);

		public async Task<bool> Create(CreateBlogCategory command)
		{
			try
			{

				var blogCategory = new BlogCategory(command.Title.Trim(), command.ImageName!, command.ParentId,
					command.Slug, command.ImageAlt);

				Insert(blogCategory);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> ChangeActivation(long categoryId)
		{
			try
			{
				var blogCategory =await GetById<BlogCategory>(categoryId);

				if (blogCategory == null)
					throw new NullReferenceException();

				if (blogCategory.Active)
				{
					var subCategories = await Table<BlogCategory>().Where(x => x.ParentId == blogCategory.Id).ToListAsync();

					foreach (var subCategory in subCategories)
					{
						if (subCategory.Active)
						{
							subCategory.ActivationChange();
							Update(subCategory);
						}
					}
				}

				blogCategory.ActivationChange();
				Update(blogCategory);

				return await Save() > 0;
			}
			catch (Exception r)
			{
				return false;
			}
		}

		public async Task<bool> Update(EditBlogCategory command)
		{
			try
			{
				var blogCategory = await GetById<BlogCategory>(command.Id);

				if (blogCategory == null)
					throw new NullReferenceException();

				blogCategory.Edit(command.Title.Trim(),command.ImageName!,command.Slug,command.ImageAlt,command.ParentId);
				Update(blogCategory);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<BlogCategory?> GetById(long categoryId)
		{
			try
			{
				return await GetById<BlogCategory>(categoryId);
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
