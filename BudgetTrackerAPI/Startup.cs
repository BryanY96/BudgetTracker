using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces;
using MengYu.BudgetTracker.ApplicationCore.ServiceInterfaces;
using MengYu.BudgetTracker.Infrastructure.Repositories;
using MengYu.BudgetTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BudgetTrackerAPI
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

            services.AddControllers();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IExpenditureService, ExpenditureService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BudgetTrackerAPI", Version = "v1" });
            });

            services.AddDbContext<BudgetTrackerDbContext>
                (
                options => options.UseSqlServer(Configuration.GetConnectionString("BudgetTrackerDbConnection"))
                );
            //services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetTrackerAPI v1"));
            }
            
            app.UseHttpsRedirection();

            // trust the client url with localhost:4200
            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetValue<string>(key: "clientSPAUrl")).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
