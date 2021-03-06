﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{

		public interface IRepository<TEntity> where TEntity : class
		{
			TEntity Get(int id);
			IEnumerable<TEntity> GetAll();
			IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
			void Add(TEntity entity);
			void Remove(TEntity entity);
		}
}