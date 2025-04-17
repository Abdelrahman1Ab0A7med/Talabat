using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
	public static class AplicationServices
	{
		public static void AddApplicationService(this IServiceCollection Services) {
			
			Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			Services.AddScoped<IOrderService, OrderService>(); 
			Services.AddAutoMapper(typeof(MappingProfiles));
			Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));//configration for bad request error message
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				//hold Invalid modelState
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					//hold Errors messages 
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
											 .SelectMany(p => p.Value.Errors)
											 .Select(e => e.ErrorMessage).ToList();
					//put error messages 
					var error = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(error);
				};

			});

		//	return Services;
		}
	}
}
