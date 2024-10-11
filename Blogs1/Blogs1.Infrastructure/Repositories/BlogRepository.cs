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

		public async Task<EditBlog?> GetForEdit(long blogId)
		{
			try
			{
				var blog = await GetById<Blog>(blogId);

				if (blog == null)
					throw new NullReferenceException();

				return new EditBlog
				{
					Title = blog.Title,
					Slug = blog.Slug,
					ImageName = blog.ImageName,
					ImageAlt = blog.ImageAlt,
					UserId = blog.UserId,
					Author = blog.Author,
					CategoryId = blog.CategoryId,
					SubCategoryId = blog.SubCategoryId,
					Summary = blog.Summary,
					Content = blog.Content,
					IsSpecial = blog.IsSpecial,
					Id = blog.Id
				};

			}
			catch (Exception e)
			{
				return null;
			}
		}

		public async Task<Blog?> GetById(long id)
		{
			try
			{
				return await GetById<Blog>(id);
			}
			catch (Exception e)
			{
				return null;
			}
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

		public async Task<bool> Edit(EditBlog command)
		{
			try
			{

				var blog = await GetById<Blog>(command.Id);

				if (blog == null)
					throw new NullReferenceException();

				blog.Edit(command.Title.Trim(),command.ImageName!,command.ImageAlt.Trim(),command.CategoryId,command.SubCategoryId,command.Slug.Trim(),command.Summary.Trim(),command.Content,command.IsSpecial);

				Update(blog);
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

		public async Task<bool> IncreaseVisits(string slug)
		{
			try
			{
				var blog = Table<Blog>().First(x => x.Slug == slug);

				blog.IncreaseTotalVisits();
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
