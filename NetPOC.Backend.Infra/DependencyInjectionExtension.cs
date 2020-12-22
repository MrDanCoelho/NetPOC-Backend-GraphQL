using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetPOC.Backend.Application.Services;
using NetPOC.Backend.Domain;
using NetPOC.Backend.Domain.Interfaces.IRepositories;
using NetPOC.Backend.Domain.Interfaces.IServices;
using NetPOC.Backend.Infra.Repositories;
using Serilog;

namespace NetPOC.Backend.Infra
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Helper de configuração de injeção de dependências
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> contendo os serviços da aplicação</param>
        /// <param name="config"><see cref="IConfiguration"/> da aplicação</param>
        public static void ConfigureAllServices(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureSettings(config);
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.ConfigureDistributedCache(config);
            services.ConfigureLogger();
        }
        
        /// <summary>
        /// Helper de configuração de <see cref="AppSettings"/> da applicação
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> contendo os serviços da aplicação</param>
        /// <param name="config"><see cref="IConfiguration"/> da aplicação</param>
        private static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config);
            services.AddSingleton(x => x.GetRequiredService<IOptions<AppSettings>>().Value);
        }
        
        /// <summary>
        /// Helper de configuração de repositórios da aplicação
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> contendo os serviços da aplicação</param>
        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            
            services.AddScoped<IUserRepository, UserRepository>();
        }

        /// <summary>
        /// Helper de configuração de serviços da aplicação
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> contendo os serviços da aplicação</param>
        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        /// <summary>
        /// Helper de configuração de cache distribuído
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> contendo os serviços da aplicação</param>
        /// <param name="config"><see cref="IConfiguration"/> da aplicação</param>
        private static void ConfigureDistributedCache(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetSection("ConnectionStrings:DistributedCache").Value;

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;

                var assemblyName = Assembly.GetEntryAssembly()?.GetName();
                if (assemblyName != null) options.InstanceName = assemblyName.Name;
            });
        }
        
        /// <summary>
        /// Helper de configuração de logs da applicação
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> contendo os serviços da aplicação</param>
        private static void ConfigureLogger(this IServiceCollection services)
        {
            // var providers = new LoggerProviderCollection();
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                // .WriteTo.Providers(providers)
                .CreateLogger();

            services.AddSingleton((ILogger)Log.Logger);

            // services.AddSingleton<ILoggerFactory>(sc =>
            // {
            //     var providerCollection = sc.GetService<LoggerProviderCollection>();
            //     var factory = new SerilogLoggerFactory(null, true, providerCollection);
            //
            //     foreach (var provider in sc.GetServices<ILoggerProvider>())
            //         factory.AddProvider(provider);
            //
            //     return factory;
            // });
        }
    }
}