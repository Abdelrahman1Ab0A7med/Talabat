using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _context;
		private Hashtable _repositories ;

		public UnitOfWork(StoreContext context)
		{
			this._context = context;
			_repositories = new Hashtable();
		}
		public async Task<int> CompleteAsync()
		=> await _context.SaveChangesAsync();

		public async ValueTask DisposeAsync()
		=> await _context.DisposeAsync();

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var type = typeof(TEntity).Name;
			if(!_repositories.ContainsKey(type))
			{
				var Repository = new GenericRepository<TEntity>(_context);
				_repositories.Add(type, Repository);
			}
			return _repositories[type] as GenericRepository<TEntity>;
		}
	}
}
