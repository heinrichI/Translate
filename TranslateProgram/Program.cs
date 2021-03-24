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
            string resourcePath = ConfigurationManager.AppSettings.Get("ResourcePath");

            string fromLanguage = ConfigurationManager.AppSettings.Get("FromLanguage");
            string toLanguage = ConfigurationManager.AppSettings.Get("ToLanguage");

            string mode = ConfigurationManager.AppSettings.Get("Mode");

            TranslateMemory tm = new TranslateMemory(fromLanguage, toLanguage);
            ITranslatorService translatorService = new GoogleTranslator(fromLanguage, toLanguage);

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

                if (string.IsNullOrEmpty(generatedCodeNamespace))
                    throw new ArgumentNullException(nameof(generatedCodeNamespace));


                string refactored;
                using (IResourceManager englishResource = new ResourceManager(resourcePath,
                    designerPath,
                    "Strings",
                    generatedCodeNamespace))
                using (IResourceManager hebrewResource = new ResourceManager(hebrewResourcePath))
                {
                    TranslateResourceManager translateResourceManager = new TranslateResourceManager(
                    englishResource,
                    hebrewResource,
                    tm,
                    translatorService);

                    RoslynManager roslynManager = new RoslynManager(translateResourceManager);

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

                //if (File.Exists(englishResourcePath))
                    //ResourceHelper.Create(englishResourcePath);

                using (IResourceManager resource = new ResourceManager(resourcePath, onlyRead: true))
                using (IResourceManager englishResource = new ResourceManager(englishResourcePath))
                {
                    foreach (KeyValuePair<string, string> item in resource)
                    {
                        if (HebrewUtils.IsHebrewString(item.Value))
                        {
                            string translate;
                            if (tm.Contain(item.Value))
                            {
                                translate = tm.GetTm(item.Value);
                                Console.WriteLine($"Used translate memory for {item.Value} - {translate}");
                            }
                            else
                            {
                                translate = translatorService.Translate(item.Value, false);
                                if (HebrewUtils.IsHebrewString(translate))
                                {
                                    Console.WriteLine($"Can not translate {item.Value}");
                                    return;
                                }
                                tm.Add(item.Value, translate);
                            }
                            if (!englishResource.ContainString(translate))
                            {
                                englishResource.Add(item.Key, translate);
                            }
                        }
                    }
                  
                }
            }
            else
                throw new ArgumentException("Unknown mode " + mode);
        }
    }
}
