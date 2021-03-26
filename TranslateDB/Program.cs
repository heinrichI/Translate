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

            if (options.Mode == "Column")
            {
                ColumnLoop loop = serviceProvider.GetService<ColumnLoop>();
                loop.Run();
            }
            else if (options.Mode == "DB")
            {
                if (string.IsNullOrEmpty(options.DataBase))
                    throw new Exception("Database name is empty!");

                DBLoop loop = serviceProvider.GetService<DBLoop>();
                loop.Run();
            }
            else
                throw new ArgumentException("Unknown mode " + options.Mode);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
