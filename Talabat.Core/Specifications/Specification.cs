using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
	public class Specification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get ; set ; }
		public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Take { get ; set ; }
		public int Skip { get; set; }
		public bool IsPaginated { get; set; }

		public Specification()
		{
		}
		public Specification(Expression<Func<T, bool>> CriteriaExp) => Criteria = CriteriaExp;
		public void AddOrderBy(Expression<Func<T, object>> OrderByExp)
		{
			OrderBy = OrderByExp;
		}
		public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExp)
		{
			OrderBy = OrderByDescExp;
		}
		public Task ApplyPagination(int skip ,int take)
		{
			IsPaginated = true;
			Skip = skip;
			Take = take;
			return Task.CompletedTask;
		}
	}
}
