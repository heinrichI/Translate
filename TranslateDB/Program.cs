using System;
using System.Collections.Generic;
using BusinessLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranslateService;

namespace TranslateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration conf = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddCommandLine(args)
            .Build();

            var options = conf.Get<Option>();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<Option>(options);

            TranslateMemory tm = new TranslateMemory(options.FromLanguage, options.ToLanguage);
            services.AddSingleton<ITranslateMemory>(tm);

            GoogleTranslator gt = new GoogleTranslator(options.FromLanguage, options.ToLanguage);
            services.AddSingleton<ITranslatorService>(gt);



            Startup startup = new Startup(conf);
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            MainLoop loop = serviceProvider.GetService<MainLoop>();
            loop.Run();
         

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
