using Resource;
using RoslynTransformation;
using System.Configuration;
using System.IO;

namespace TranslateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string resourcePath = ConfigurationManager.AppSettings.Get("ResourcePath");
            string hebrewResourcePath = Path.Combine(
                Path.GetDirectoryName(ConfigurationManager.AppSettings.Get("ResourcePath")),
                Path.GetFileNameWithoutExtension(ConfigurationManager.AppSettings.Get("ResourcePath")) + ".he-IL.resx");
            string fileToRefactor = ConfigurationManager.AppSettings.Get("FileToRefactor");

            string designerPath = Path.Combine(
                Path.GetDirectoryName(resourcePath),
                Path.GetFileNameWithoutExtension(resourcePath) + ".Designer.cs");

            string generatedCodeNamespace = RoslynManager.ExtractNamespace(designerPath);

            string refactored;
            using (ResourceManager rm = new ResourceManager(resourcePath,
                designerPath,
                "Strings",
                generatedCodeNamespace))
            {
                RoslynManager roslynManager = new RoslynManager(rm);

                string fileText = System.IO.File.ReadAllText(fileToRefactor);
                refactored = roslynManager.Rewrite(fileText);
            }

            if (!string.IsNullOrEmpty(refactored))
            {
                File.WriteAllText(fileToRefactor, refactored);
            }
        }
    }
}
