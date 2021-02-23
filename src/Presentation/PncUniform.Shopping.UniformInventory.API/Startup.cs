using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PncUniform.Shopping.UniformInventory.Application;
using PncUniform.Shopping.UniformInventory.Application.Db;
using FluentValidation.AspNetCore;
using System.Reflection;
using PncUniform.Shopping.UniformInventory.Application.Customers.Queries;

namespace PncUniform.Shopping.UniformInventory.API
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
            services.AddDbContext<UniformManagementContext>(options => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=PncUniform;Trusted_Connection=True;"));
            services.AddControllers()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FindCustomerQueryValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PncUniform.Shopping.UniformInventory.API", Version = "v1" });
            });
            services.AddApplication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PncUniform.Shopping.UniformInventory.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
