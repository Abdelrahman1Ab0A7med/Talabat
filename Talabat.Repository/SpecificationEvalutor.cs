using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvalutor<T> where T : BaseEntity
	{
		//build query by merging criteria with includes 
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
		{
			var query = inputQuery;//DBcontext.DBSet
			
			if (spec.Criteria is not null){//check if your query apply any conditions? 
				query = query.Where(spec.Criteria); //apply conditions on your query
			}
			if(spec.OrderBy is not null)
			{
				query = query.OrderBy(spec.OrderBy);
			}
			if (spec.OrderByDesc is not null)
			{
				query = query.OrderByDescending(spec.OrderByDesc);
			}
			if (spec.IsPaginated)
			{
				query = query.Skip(spec.Skip).Take(spec.Take);
			}
			//insert your  includes in your query 
			//Agergate take your query and append each include to the query
			query = spec.Includes.Aggregate(query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
			return query;
		}
	}
}
