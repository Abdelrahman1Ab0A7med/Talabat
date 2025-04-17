using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Repositories
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		Task<int> CompleteAsync();
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


	}
}
