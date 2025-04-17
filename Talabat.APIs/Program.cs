using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Service

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //------------------------------------------------------------------------------------------
            //ask CLR to create object from store Context 
            builder.Services.AddDbContext<StoreContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default Connection"));
            }
            );
            builder.Services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Identity Connection"));
            });


            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });



            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
			
			//ask CLR to create object from any class inherited from Generic repository
			builder.Services.AddApplicationService();
            builder.Services.AddIdentityService(builder.Configuration);


			#endregion

			var app = builder.Build();

            #region Update-Database (without PMC)
            using var scope = app.Services.CreateScope();
            //collect all scoped services 
            var service = scope.ServiceProvider;
            //hold service
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = service.GetRequiredService<StoreContext>();
                //catch store context object which was created from CLR
                await dbContext.Database.MigrateAsync();
                //Update-Database
                await StoreContextSeed.SeedAsync(dbContext);

                var userContext = service.GetRequiredService<UserContext>();
                await userContext.Database.MigrateAsync();
                var UserManager = service.GetRequiredService<UserManager<AppUser>>();
                await AppUserSeed.UserAsyncSeed(UserManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured while creating database");
            }
            #endregion

            #region Services & MiddleWares


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //redirect to error controllers  
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run(); 
            #endregion
        }
    }
}
