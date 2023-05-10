using Amazon.Core.Repositories;
using Amazon.Repository;
using Amazon.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add Service for StoreContext Class that represent the database
            builder.Services.AddDbContext<StoreContext>((options) =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //Add DP for IGenericRepository to ask clr return obj from GenericRepositoty class
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            var app = builder.Build();

            //fetch all Scoped Services from app and put it in scope container
            // should use using keyword to dispose all objs that will be created from this services
            using var scope = app.Services.CreateScope();
            // fetch scoped services itself 
            var services = scope.ServiceProvider;

            //create service form class that implement ILoggerFactory Interface to
            //log exceprion or Errors in consle in special way to be understanded
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //try to ask from CLR to Create Obj from StoreContext Service
                var dbContext = services.GetRequiredService<StoreContext>();
                //try to applay all migration that not applied to database
                await dbContext.Database.MigrateAsync();

                //Try to seed all data for just first time
                await StoreContextSeed.SeedAsync(dbContext);

            }catch(Exception ex)
            {
                //Create Obj from ILooger to log at Console
                var logger = loggerFactory.CreateLogger<Program>();
                //Log error At console withe specific Messege
                logger.LogError(ex, "An Error Occure during Apply Migrations on Database");
            }

            #region Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers();


            #endregion 
            
            app.Run();
        }
    }
}