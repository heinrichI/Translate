using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Resources.NetStandard;

namespace Resource
{
    public class ResourceHelper
    {
        public static void Test(string resxFilename)
        {
            // Create a resource file to read.
            CreateResourceFile(resxFilename);

            // Enumerate the resources in the file.
            ResXResourceReader rr = new ResXResourceReader(resxFilename);
            rr.UseResXDataNodes = true;
            IDictionaryEnumerator dict = rr.GetEnumerator();
            while (dict.MoveNext())
            {
                ResXDataNode node = (ResXDataNode)dict.Value;
                Console.WriteLine("{0,-20} {1,-20} {2}",
                                  node.Name + ":",
                                  node.GetValue((ITypeResolutionService)null),
                                  !String.IsNullOrEmpty(node.Comment) ? "// " + node.Comment : "");
            }

            StreamWriter sw = new StreamWriter(@".\DemoResources.cs");
            string[] errors = null;
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CodeCompileUnit code = StronglyTypedResourceBuilder.Create("Demo.resx", "DemoResources",
                                                                       "DemoApp", provider,
                                                                       false, out errors);
            if (errors.Length > 0)
                foreach (var error in errors)
                    Console.WriteLine(error);

            provider.GenerateCodeFromCompileUnit(code, sw, new CodeGeneratorOptions());
            sw.Close();
        }

        private static void CreateResourceFile(string resxFilename)
        {
            ResXResourceWriter rw = new ResXResourceWriter(resxFilename);
            string[] resNames = {"Country", "Population", "Area",
                           "Capital", "LCity" };
            string[] columnHeaders = { "Country Name", "Population (2010}",
                                 "Area", "Capital", "Largest City" };
            string[] comments = { "The localized country name", "Estimated population, 2010",
                            "The area in square miles", "Capital city or chief administrative center",
                            "The largest city based on 2010 data" };
            rw.AddResource("Title", "Country Information");
            rw.AddResource("nColumns", resNames.Length);
            for (int ctr = 0; ctr < resNames.Length; ctr++)
            {
                ResXDataNode node = new ResXDataNode(resNames[ctr], columnHeaders[ctr]);
                node.Comment = comments[ctr];
                rw.AddResource(node);
            }
            rw.Generate();
            rw.Close();
        }
    }

}
