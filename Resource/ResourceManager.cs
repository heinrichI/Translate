using BusinessLogic;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Resources;
using System.Resources.Tools;

namespace Resource
{
    public class ResourceManager : IResourceManager
    {
        ResXResourceWriter _rw;

        public ResourceManager(string resxFilePath,
            string designerPath,
            string baseName,
            string generatedCodeNamespace)
        {
            _rw = new ResXResourceWriter(resxFilePath);
            this._resxFilePath = resxFilePath;
            this._designerPath = designerPath;
            this._baseName = baseName;
            this._generatedCodeNamespace = generatedCodeNamespace;

            // Enumerate the resources in the file.
            ResXResourceReader rr = new ResXResourceReader(resxFilePath);
            rr.UseResXDataNodes = true;
            IDictionaryEnumerator dict = rr.GetEnumerator();
            while (dict.MoveNext())
            {
                ResXDataNode node = (ResXDataNode)dict.Value;
                _dict.Add(node.Name, node);
                _rw.AddResource(node);
            }
        }

        Dictionary<string, ResXDataNode> _dict = new Dictionary<string, ResXDataNode>();
        private readonly string _resxFilePath;
        private readonly string _designerPath;
        private readonly string _baseName;
        private readonly string _generatedCodeNamespace;

        public string this[string index] => _dict[index].GetValue((ITypeResolutionService)null).ToString();

        public void Add(string name, string stringLiteral)
        {
            _dict.Add(name, new ResXDataNode(name, stringLiteral));

            _rw.AddResource(name, stringLiteral);
        }

        public bool ContainString(string str)
        {
            return _dict.ContainsKey(str);
        }

        public void Dispose()
        {
            //_rw.Generate();
            _rw.Close();


            StreamWriter sw = new StreamWriter(_designerPath);
            string[] errors = null;
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CodeCompileUnit code = StronglyTypedResourceBuilder.Create(_resxFilePath, _baseName,
                                                                       _generatedCodeNamespace, provider,
                                                                       false, out errors);
            //if (errors.Length > 0)
            //    foreach (var error in errors)
            //        Console.WriteLine(error);

            provider.GenerateCodeFromCompileUnit(code, sw, new CodeGeneratorOptions());
            sw.Close();
        }
    }
}
