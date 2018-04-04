using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechTree.Common;

namespace TechTree.Api
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContexts(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                var microServices = services.GetRequiredService<IEnumerable<IMicroService>>();

                foreach (var microService in microServices)
                {
                    var serviceLogger = services.GetRequiredService<ILoggerFactory>()
                        .CreateLogger(microService.GetType());

                    serviceLogger.LogInformation($"Discovered micro service '{microService.Name}'");

                    foreach (var dbContextType in microService.GetDbContextTypes())
                    {
                        var logger = services.GetRequiredService<ILoggerFactory>()
                            .CreateLogger(dbContextType);

                        try
                        {
                            logger.LogInformation($"Migrating database associated with context {dbContextType.Name}");

                            var dbContext = (DbContext) services.GetRequiredService(dbContextType);

                            // dotnet ef migrations add Initial --startup-project=../Api/api.csproj

                            dbContext.Database.Migrate();

                            //seeder?.Invoke(context, services);

                            logger.LogInformation($"Migrated database associated with context {dbContextType.Name}");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"An error occurred while migrating the database used on context {dbContextType.Name}");
                        }
                    }
                }
            }

            return webHost;
        }
    }
}