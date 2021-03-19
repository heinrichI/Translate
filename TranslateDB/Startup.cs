using BusinessLogic;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranslateService;

namespace TranslateDB
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IConfiguration>(Configuration)
                .AddScoped<IRepository, Repository>()
                .AddScoped<IWriteRepository, WriteRepository>()
                .AddSingleton<MainLoop>();
        }
    }
}
