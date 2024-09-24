using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogService.Command;
using Blogs1.Domain.BlogAgg;
using Blogs1.Domain.BlogAgg.IRepositories;
using Blogs1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;

namespace Blogs1.Infrastructure.Repositories
{
	internal class BlogRepository : BaseRepository, IBlogRepository
	{
		public BlogRepository(BlogContext context) : base(context)
		{
		}

		public async Task<bool> Exists(Expression<Func<Blog, bool>> expression)
		{
			try
			{
				return await Table<Blog>().AnyAsync(expression);
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> Create(CreateBlog command)
		{
			try
			{

				var blog = new Blog(command.Title.Trim(), command.ImageName, command.ImageAlt.Trim(), command.UserId,
					command.Author.Trim(), command.CategoryId, command.SubCategoryId, command.Slug.Trim(),
					command.Summary.Trim(), command.Content, command.IsSpecial);

				Insert(blog);
				return await Save() > 0;

			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> ActivationChange(long blogId)
		{
			try
			{
				var blog = Table<Blog>().First(x => x.Id == blogId);

				blog.ActivationChange();
				Update(blog);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> SpecialationChange(long blogId)
		{
			try
			{
				var blog = Table<Blog>().First(x => x.Id == blogId);

				blog.ChangeSpecialation();
				Update(blog);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
