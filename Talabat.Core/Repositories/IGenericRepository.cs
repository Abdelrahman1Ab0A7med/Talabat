using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<IReadOnlyList<T>> GetAll();
		Task<T> GetById(int id);
		#region With Specification

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
		Task<T> GetByIdWithSpecAsync(ISpecification<T> spec); 
		Task<int> GetCountSpec(ISpecification<T>spec);
		#endregion
		Task Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
