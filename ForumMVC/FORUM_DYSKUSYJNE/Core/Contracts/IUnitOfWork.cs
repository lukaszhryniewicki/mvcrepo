using FORUM_DYSKUSYJNE.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IUnitOfWork
	{
		
		ITopicRepository Topics { get; }
		ISectionRepository Sections { get; }

		void Complete();
		IRepository<T> Repository<T>() where T: class;
	}
}
