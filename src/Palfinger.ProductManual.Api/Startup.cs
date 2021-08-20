using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Infrastructure.Data;
using Palfinger.ProductManual.Infrastructure.Data.Repositories;
using Palfinger.ProductManual.Queries.Handlers;

namespace Palfinger.ProductManual.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetManualByProductIdQueryHandler).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Palfinger.ProductManual.Api", Version = "v1"});
            });
            services.AddDbContext<ProductManualDbContext>(options =>
            {//TODO test sin migration hsitory table si crea tabla migraciones igual
                options.UseSqlite(Configuration.GetConnectionString("DBConnection"), 
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory"));
            });
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Palfinger.ProductManual.Api v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}