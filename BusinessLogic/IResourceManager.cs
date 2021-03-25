using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IResourceManager : IDisposable, IEnumerable<KeyValuePair<string, string>>
    {
        //string this[string index]
        //{
        //    get;
        //}

        bool ContainKey(string key);

        bool ContainValue(string value);

        void Add(string name, string value);

        string GetKeyByValue(string value);
    }
}
