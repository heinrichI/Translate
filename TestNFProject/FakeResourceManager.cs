using BusinessLogic;
using System.Collections.Generic;

namespace TestNFProject
{
    class FakeResourceManager : IResourceManager
    {
        Dictionary<string, string> _dict = new Dictionary<string, string>();
        public string this[string index] => _dict[index];

        public void Add(string name, string stringLiteral)
        {
            _dict.Add(name, stringLiteral);
        }

        public bool ContainString(string str)
        {
            return _dict.ContainsKey(str);
        }

        public void Dispose()
        {
        }
    }
}
