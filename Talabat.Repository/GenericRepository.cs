using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;

		public GenericRepository(StoreContext context)
		{
			_context = context;
		}
		public async Task<IReadOnlyList<T>> GetAll()
		{
			if(typeof(T) == typeof(Product))
			{
				return (IReadOnlyList<T>)await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
			}
			return await _context.Set<T>().ToListAsync();
		}


		public async Task<T> GetById(int id)
		{

			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpec(spec).ToListAsync();
		}
		public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpec(spec).FirstOrDefaultAsync();
		}
		private  IQueryable<T> ApplySpec(ISpecification<T> spec)
		{
			return  SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec);
		}

		public async Task<int> GetCountSpec(ISpecification<T> spec)
		{
			return await ApplySpec(spec).CountAsync();
		}

		public async Task Add(T entity)
			=> await _context.Set<T>().AddAsync(entity);
		

		public void Update(T entity)
			=> _context.Update(entity);

		public void Delete(T entity)
		=>_context.Remove(entity);
	}
}
