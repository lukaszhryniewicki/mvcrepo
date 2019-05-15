using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Infrastructure.Data;
using System.Data.Entity;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class DataService:IDataService
	{
		private readonly ForumDatabase context;

		public DataService(ForumDatabase context_)
		{
			context = context_;

		}

		public void Dispose()
		{
			context.Dispose();
		}

		public DbSet<T> GetDbSet<T>() where T : class
		{
			return context.Set<T>();
		}
		public void SaveChanges()
		{
			context.SaveChanges();
		}
	}
}