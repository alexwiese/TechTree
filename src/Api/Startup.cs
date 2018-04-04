using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using TechTree.Common;

namespace TechTree.Api
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
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ); 

            // Add micro services
            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(c => c.AssignableTo<IMicroService>())
                .As<IMicroService>()
                .WithSingletonLifetime());

            ConfigureDbContexts(services);
        }

        private void ConfigureDbContexts(IServiceCollection services)
        {
            var dbContexts = DependencyContext.Default.RuntimeLibraries
                .SelectMany(library => library.GetDefaultAssemblyNames(DependencyContext.Default))
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(type => type.Namespace?.StartsWith("Microsoft") != true && type != typeof(DbContext) && typeof(DbContext).IsAssignableFrom(type) && !type.IsAbstract)
                .ToList();

            foreach (var typeInfo in dbContexts)
            {
                var gen = typeof(EntityFrameworkServiceCollectionExtensions).GetRuntimeMethods()
                    .First(rm => rm.Name == nameof(EntityFrameworkServiceCollectionExtensions.AddDbContext))
                    .MakeGenericMethod(typeInfo);

                gen.Invoke(null, new object[]
                {
                    services, (Action<DbContextOptionsBuilder>) (options =>
                        options.UseSqlServer(Configuration.GetConnectionString(typeInfo.Name),
                            sqlOptions =>
                                sqlOptions.MigrationsAssembly(
                                    typeInfo.GetTypeInfo().Assembly.GetName().Name))),
                    ServiceLifetime.Scoped,
                    ServiceLifetime.Scoped
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
