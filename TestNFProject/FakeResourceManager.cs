using BusinessLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestNFProject
{
    class FakeResourceManager : IResourceManager
    {
        Dictionary<string, string> _dict = new Dictionary<string, string>();

        //public string this[string index] => _dict[index];

        public void Add(string name, string stringLiteral)
        {
            _dict.Add(name, stringLiteral);
        }

        public bool ContainKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        public bool ContainValue(string value)
        {
            return _dict.ContainsValue(value);
        }

        public void Dispose()
        {
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public string GetKeyByValue(string value)
        {
            return _dict.FirstOrDefault(x => x.Value == value).Key;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal void Clear()
        {
            _dict.Clear();
        }

    }
}
