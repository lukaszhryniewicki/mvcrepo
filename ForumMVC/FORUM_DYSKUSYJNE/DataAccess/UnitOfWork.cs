using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.DataAccess.Repositories;
using FORUM_DYSKUSYJNE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// stary kod przed refaktoringiem
namespace FORUM_DYSKUSYJNE.DataAccess
{

	public class UnitOfWork:IUnitOfWork
	{
		private readonly ForumDatabase _context ;
		public ISectionRepository Sections { get; }
		public ITopicRepository Topics { get; }
		public UnitOfWork(ForumDatabase context, ISectionRepository sectionRepository,ITopicRepository topicRepository)
		{
			_context = context;
			Sections = sectionRepository;
			Topics = topicRepository;
		}
		public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

		public IRepository<T> Repository<T>() where T : class
		{
			// Jeżeli instancja danego repozytorium istnieje - zostanie zwrócona
			if (repositories.Keys.Contains(typeof(T)) == true)
				return repositories[typeof(T)] as IRepository<T>;
			// Jeżeli nie, zostanie utworzona nowa i dodana do słownika
			IRepository<T> repo = new Repository<T>(_context);
			repositories.Add(typeof(T), repo);
			return repo;
		}
		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}