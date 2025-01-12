using FinancialTransactions.Application;
using FinancialTransactions.Infrastructure;
using FinancialTransactions.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FinancialTransacations.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
                c.EnableAnnotations();  
            });

            services.AddInjectionPersistence();
            services.AddInjectionApplication();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();  
                app.UseSwaggerUI();
            }

            
            app.UseRouting(); 

            app.UseHttpsRedirection();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  
            });
        }
    }
}
