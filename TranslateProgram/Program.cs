using BusinessLogic;
using Resource;
using RoslynTransformationNetFrame;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TranslateService;
using CommandLine;

namespace TranslateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramTranslationOptions cmdLineOptions = new ProgramTranslationOptions();
            Parser.Default.ParseArguments<ProgramTranslationOptions>(args).WithParsed(parsed => cmdLineOptions = parsed);

            ProgramTranslationOptions configOptions = ConfigurationManagerOptionsReader.GetArguments(ConfigurationManager.AppSettings);

            ProgramTranslationOptions options = new ProgramTranslationOptionsRequestWrapper(cmdLineOptions.fillNullsFrom(configOptions));

            string resourcePath = options.ResourcePath;

            string fromLanguage = options.FromLanguage;
            string toLanguage = options.ToLanguage;

            string mode = options.Mode;

            bool skip429 = options.SkipError429.Value;
            if (skip429)
                Console.WriteLine("Skip 429 error enabled!");

            TranslateMemory tm = new TranslateMemory(fromLanguage, toLanguage);
            ITranslatorService translatorService = new GoogleTranslator(fromLanguage, toLanguage);
            TranslatorWithTM translatorWithTM = new TranslatorWithTM(tm, translatorService);

            if (mode == "Code")
            {
                string hebrewResourcePath = Path.Combine(
                    Path.GetDirectoryName(options.ResourcePath),
                    Path.GetFileNameWithoutExtension(options.ResourcePath) + ".he-IL.resx");
                string fileToRefactor = options.FileToRefactor;

                string designerPath = Path.Combine(
                    Path.GetDirectoryName(resourcePath),
                    Path.GetFileNameWithoutExtension(resourcePath) + ".Designer.cs");


                string designerText = System.IO.File.ReadAllText(designerPath);
                string generatedCodeNamespace = RoslynHelper.ExtractNamespace(designerText);
                bool internalClass = RoslynHelper.IsInternalClass(designerText);

                if (string.IsNullOrEmpty(generatedCodeNamespace))
                    throw new ArgumentNullException(nameof(generatedCodeNamespace));

                string solutionPath = options.SolutionPath;

                string refactored;
                System.Text.Encoding encodingSourced;
                using (IResourceManager englishResource = new ResourceManager(resourcePath,
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
                    skip429);

                    translateResourceManager.Synchronize();

                    RoslynManager roslynManager = new RoslynManager(translateResourceManager,
                        solutionPath);

                    string fileText = System.IO.File.ReadAllText(fileToRefactor);

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
            else if (mode == "Form")
            {
                string englishResourcePath = Path.Combine(
                    Path.GetDirectoryName(resourcePath),
                    Path.GetFileNameWithoutExtension(resourcePath) + ".en.resx");

                if (!File.Exists(englishResourcePath))
                    throw new FileNotFoundException("englishResourcePath");
                    //ResourceHelper.Create(englishResourcePath);

                using (IResourceManager resource = new ResourceManager(resourcePath, onlyRead: true))
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
                throw new ArgumentException("Unknown mode " + mode);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
