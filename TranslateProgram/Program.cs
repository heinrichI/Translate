using BusinessLogic;
using CopyPasteTranslate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resource;
using RoslynTransformationNetFrame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TranslateService;

namespace TranslateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration conf = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddCommandLine(args)
            .Build();

            var options = conf.Get<Option>();
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<Option>(options);

            if (options.Skip429Error)
                Console.WriteLine("Skip 429 error enable!");

            TranslateMemory tm = new TranslateMemory(options.FromLanguage, options.ToLanguage);
            ITranslatorService translatorService = new GoogleTranslator(options.FromLanguage, options.ToLanguage);
            TranslatorWithTM translatorWithTM = new TranslatorWithTM(tm, translatorService);

            if (options.Mode == "Code")
            {
                string hebrewResourcePath = Path.Combine(
                    Path.GetDirectoryName(options.ResourcePath),
                    Path.GetFileNameWithoutExtension(options.ResourcePath) + ".he-IL.resx");
                string fileToRefactor = options.FileToRefactor;

                string designerPath = Path.Combine(
                    Path.GetDirectoryName(options.ResourcePath),
                    Path.GetFileNameWithoutExtension(options.ResourcePath) + ".Designer.cs");


                string designerText = System.IO.File.ReadAllText(designerPath);
                string generatedCodeNamespace = RoslynHelper.ExtractNamespace(designerText);
                bool internalClass = RoslynHelper.IsInternalClass(designerText);

                if (string.IsNullOrEmpty(generatedCodeNamespace))
                    throw new ArgumentNullException(nameof(generatedCodeNamespace));

                string fileText = System.IO.File.ReadAllText(fileToRefactor);

                RoslynReader roslynReader = new RoslynReader();
                ReadOnlyCollection<string> literals = roslynReader.GetAllHebrewLiterals(fileText);
                TranslateViaCopyPasteStarter.TranslateViaCopyPaste(literals);
                //Dictionary<string, string> translateLiterals = copyPasteTranslator.TranslateAll(literals);

                string refactored;
                System.Text.Encoding encodingSourced;
                using (IResourceManager englishResource = new ResourceManager(options.ResourcePath,
                    designerPath,
                    "Strings",
                    generatedCodeNamespace,
                    internalClass: internalClass))
                using (IResourceManager hebrewResource = new ResourceManager(hebrewResourcePath))
                {
                    TranslateResourceManager translateResourceManager = new TranslateResourceManager(
                    englishResource,
                    hebrewResource,
                    translatorWithTM,
                    options.Skip429Error);

                    translateResourceManager.Synchronize();

                    RoslynManager roslynManager = new RoslynManager(translateResourceManager,
                        options.SolutionPath);

                    encodingSourced = EncodingHelper.DetectTextEncoding(fileToRefactor, out _);

                    refactored = roslynManager.Rewrite(fileText, generatedCodeNamespace);
                }

                if (!string.IsNullOrEmpty(refactored))
                {
                    File.WriteAllText(fileToRefactor, refactored, encodingSourced);

                    System.Text.Encoding encodingWriting = EncodingHelper.DetectTextEncoding(fileToRefactor, out _);
                    if (encodingWriting != encodingSourced)
                    {
                        ConsoleColor currentForeground = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"File written in {encodingWriting}, but was in {encodingSourced}.");
                        Console.ForegroundColor = currentForeground;
                    }
                }
            }
            else if (options.Mode == "Form")
            {
                string englishResourcePath = Path.Combine(
                    Path.GetDirectoryName(options.ResourcePath),
                    Path.GetFileNameWithoutExtension(options.ResourcePath) + ".en.resx");

                if (!File.Exists(englishResourcePath))
                    throw new FileNotFoundException("englishResourcePath");
                    //ResourceHelper.Create(englishResourcePath);

                using (IResourceManager resource = new ResourceManager(options.ResourcePath, onlyRead: true))
                using (IResourceManager englishResource = new ResourceManager(englishResourcePath))
                {
                    foreach (KeyValuePair<string, string> item in resource)
                    {
                        if (HebrewUtils.IsHebrewString(item.Value))
                        {
                            string translate = translatorWithTM.Translate(item.Value, false);
                     
                            if (HebrewUtils.IsHebrewString(translate))
                            {
                                Console.WriteLine($"Can not translate {item.Value}");
                                return;
                            }

                            if (!englishResource.ContainKey(item.Key))
                            {
                                englishResource.Add(item.Key, translate);
                            }
                            else
                            {
                                Console.WriteLine($"Resource already contain {item.Key}");
                            }
                        }
                    }
                  
                }
            }
            else
                throw new ArgumentException("Unknown mode " + nameof(options.Mode));

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
