using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IDataService:IDisposable
	{
		DbSet<T> GetDbSet<T>() where T : class;
		void SaveChanges();
	}
}
