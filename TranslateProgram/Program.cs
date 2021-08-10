using BusinessLogic;
using Resource;
using RoslynTransformationNetFrame;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TranslateService;

namespace TranslateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class1.Open();

            string resourcePath = ConfigurationManager.AppSettings.Get("ResourcePath");

            string fromLanguage = ConfigurationManager.AppSettings.Get("FromLanguage");
            string toLanguage = ConfigurationManager.AppSettings.Get("ToLanguage");

            string mode = ConfigurationManager.AppSettings.Get("Mode");

            bool skip429 = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("Skip429"));
            if (skip429)
                Console.WriteLine("Skip 429 error enable!");

            TranslateMemory tm = new TranslateMemory(fromLanguage, toLanguage);
            ITranslatorService translatorService = new GoogleTranslator(fromLanguage, toLanguage);
            TranslatorWithTM translatorWithTM = new TranslatorWithTM(tm, translatorService);

            if (mode == "Code")
            {
                string hebrewResourcePath = Path.Combine(
                    Path.GetDirectoryName(ConfigurationManager.AppSettings.Get("ResourcePath")),
                    Path.GetFileNameWithoutExtension(ConfigurationManager.AppSettings.Get("ResourcePath")) + ".he-IL.resx");
                string fileToRefactor = ConfigurationManager.AppSettings.Get("FileToRefactor");

                string designerPath = Path.Combine(
                    Path.GetDirectoryName(resourcePath),
                    Path.GetFileNameWithoutExtension(resourcePath) + ".Designer.cs");


                string designerText = System.IO.File.ReadAllText(designerPath);
                string generatedCodeNamespace = RoslynHelper.ExtractNamespace(designerText);
                bool internalClass = RoslynHelper.IsInternalClass(designerText);

                if (string.IsNullOrEmpty(generatedCodeNamespace))
                    throw new ArgumentNullException(nameof(generatedCodeNamespace));

                string solutionPath = ConfigurationManager.AppSettings.Get("SolutionPath");

                string refactored;
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
                    refactored = roslynManager.Rewrite(fileText, generatedCodeNamespace);
                }

                if (!string.IsNullOrEmpty(refactored))
                {
                    File.WriteAllText(fileToRefactor, refactored);
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
