using BusinessLogic;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Resources;
using System.Resources.Tools;

namespace Resource
{
    public class ResourceManager : IResourceManager
    {
        ResXResourceWriter _rw;

        public ResourceManager(string resxFilePath,
            string designerPath = null,
            string baseName = null,
            string generatedCodeNamespace = null,
            bool onlyRead = false)
        {
            this._resxFilePath = resxFilePath;
            this._designerPath = designerPath;
            this._baseName = baseName;
            this._generatedCodeNamespace = generatedCodeNamespace;
            this._onlyRead = onlyRead;

            if (!_onlyRead)
            {
                _rw = new ResXResourceWriter(resxFilePath);
            }

            // Enumerate the resources in the file.
            ResXResourceReader rr = new ResXResourceReader(resxFilePath);
            rr.UseResXDataNodes = true;
            if (File.Exists(resxFilePath))
            {
                IDictionaryEnumerator dict = rr.GetEnumerator();
                while (dict.MoveNext())
                {
                    ResXDataNode node = (ResXDataNode)dict.Value;
                    string stringValue = node.GetValue((ITypeResolutionService)null)?.ToString();
                    if (!string.IsNullOrEmpty(stringValue))
                    {
                        _dict.Add(node.Name, stringValue);
                    }
                    if (!_onlyRead)
                    {
                        _rw.AddResource(node);
                    }
                }
            }
        }

        //Dictionary<string, ResXDataNode> _dict = new Dictionary<string, ResXDataNode>();
        Dictionary<string, string> _dict = new Dictionary<string, string>();
        private readonly string _resxFilePath;
        private readonly string _designerPath;
        private readonly string _baseName;
        private readonly string _generatedCodeNamespace;
        private readonly bool _onlyRead;

        //public string this[string index] => _dict[index];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _dict.GetEnumerator();
            //return new DictionaryEnum(_dict);
        }

        public void Add(string name, string stringLiteral)
        {
            if (!_onlyRead)
            {
                _dict.Add(name, stringLiteral);

                _rw.AddResource(name, stringLiteral);
            }
        }

        public bool ContainKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        public bool ContainValue(string value)
        {
            return _dict.ContainsValue(value);
        }

        public string GetKeyByValue(string value)
        {
            return _dict.FirstOrDefault(x => x.Value == value).Key;
        }

        public void Dispose()
        {
            if (!_onlyRead)
            {
                //_rw.Generate();
                _rw.Close();

                if (!string.IsNullOrEmpty(_designerPath))
                {
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

    }
}
