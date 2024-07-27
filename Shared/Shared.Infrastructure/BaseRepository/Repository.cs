using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Shared.Domain.SeedWorks.Base;

namespace Shared.Infrastructure.BaseRepository;

public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{

	private readonly DbContext _context;

	private DbSet<TEntity> DbSet => _context.Set<TEntity>();

	public Repository(DbContext context)
	{
		_context = context;
	}

	public List<TEntity>? GetAll(bool eager = false)
	{
		try
		{

			var queryable = Table<TEntity>();

			if (eager)
				queryable = GetRelations(queryable);

			return queryable?.ToList();
		}
		catch (Exception e)
		{
			return null;
		}
	}

	public List<TEntity>? GetAll(Expression<Func<TEntity, bool>> expression, bool eager = false)
	{
		try
		{

			var queryable = Table<TEntity>()?.Where(expression);

			if (eager)
				queryable = GetRelations(queryable);

			return queryable?.ToList();
		}
		catch (Exception e)
		{
			return null;
		}
	}

	public IQueryable<TEntity>? GetAsQueryable(bool eager = false)
	{
		try
		{
			var queryable = Table<TEntity>();

			if (eager)
				queryable = GetRelations(queryable);

			return queryable;
		}
		catch (Exception e)
		{
			return null;
		}
	}

	public IQueryable<TEntity>? GetAsQueryable(Expression<Func<TEntity, bool>> expression, bool eager = false)
	{
		try
		{
			var queryable = Table<TEntity>()?.Where(expression);

			if (eager)
				queryable = GetRelations(queryable);

			return queryable;
		}
		catch (Exception e)
		{
			return null;
		}
	}


	public TEntity? GetBy(Expression<Func<TEntity, bool>> expression, bool eager = false)
	{
		try
		{

			var queryable = Table<TEntity>();

			if (eager)
				queryable = GetRelations(queryable);

			return queryable?.FirstOrDefault(expression);

		}
		catch (Exception e)
		{
			return null;
		}
	}

	public bool Update(TEntity entity)
	{
		try
		{

			DbSet.Update(entity);

			return Save() > 0;

		}
		catch (Exception e)
		{
			return false;
		}
	}

	public bool Insert(TEntity entity)
	{
		try
		{

			DbSet.Add(entity);

			return Save() > 0;

		}
		catch (Exception e)
		{
			return false;
		}
	}

	public bool Delete(TEntity entity)
	{
		try
		{

			DbSet.Remove(entity);

			return Save() > 0;

		}
		catch (Exception e)
		{
			return false;
		}
	}

	public bool IsExists(Expression<Func<TEntity, bool>> expression)
	{

		try
		{
			return Table<TEntity>().Any(expression);
		}
		catch (Exception e)
		{
			return false;
		}

	}

	public int Save()
	{
		return _context.SaveChanges();
	}

	public void Dispose()
	{
		_context?.Dispose();
	}

	#region Private Methods

	private IQueryable<TEntity>? Table<T>() where T : BaseEntity<TKey>
	{
		try
		{
			return _context.Set<TEntity>().AsNoTracking();

		}
		catch (Exception e)
		{
			return null;
		}

	}

	private IQueryable<TEntity>? GetRelations(IQueryable<TEntity>? queryable)
	{

		if (!queryable.Any())
		{
			return null;
		}

		var type = queryable.ElementType.FullName;
		foreach (var property in _context.Model.FindEntityType(type)?.GetNavigations()!)
		{
			queryable?.Include(property.Name);
		}

		return queryable;
	}


	#endregion
}