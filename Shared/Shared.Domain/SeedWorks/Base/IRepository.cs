using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.SeedWorks.Base
{
	public interface IRepository<TKey, TEntity> where TEntity : class
	{


		List<TEntity>? GetAll(bool eager = false);
		List<TEntity>? GetAll(Expression<Func<TEntity, bool>> expression, bool eager = false);

		IQueryable<TEntity>? GetAsQueryable(bool eager = false);
		IQueryable<TEntity>? GetAsQueryable(Expression<Func<TEntity, bool>> expression, bool eager = false);



		TEntity? GetBy(Expression<Func<TEntity, bool>> expression, bool eager = false);
		TEntity? GetById(TKey id, bool eager = false);

		bool Update(TEntity entity);
		bool Insert(TEntity entity);
		bool Delete(TEntity entity);

		bool IsExists(Expression<Func<TEntity, bool>> expression);

		bool Save();
		void Dispose();

	}
}
