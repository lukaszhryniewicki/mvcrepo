using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{

	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected ForumDatabase context = ForumDatabase.Create();

		public Repository(ForumDatabase context_)
		{
			context = context_;
		}

		public void Add(TEntity entity)
		{
			context.Set<TEntity>().Add(entity);
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return context.Set<TEntity>().Where(predicate).ToList();
		}

		public TEntity Get(int id)
		{
			return context.Set<TEntity>().Find(id);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return context.Set<TEntity>().ToList();
		}

		public void Remove(TEntity entity)
		{
			context.Set<TEntity>().Remove(entity);
		}
	}
}