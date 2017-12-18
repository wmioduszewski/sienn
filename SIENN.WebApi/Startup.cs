namespace SIENN.WebApi
{
    using System.Reflection;
    using AutoMapper;
    using DbAccess.Persistance;
    using DbAccess.Repositories;
    using DbAccess.UnitOfWork;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services.Mapping;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            services.AddDbContext<SiennDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("SIENN.WebApi")));

            services.AddScoped<IProductRepository, ProductRepository>();

            //https://ardalis.com/registering-open-generics-in-aspnet-core-dependency-injection
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SIENN Recruitment API"
                });
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIENN Recruitment API v1"); });

            app.UseMvc();
        }
    }
}