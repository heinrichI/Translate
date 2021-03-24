using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IResourceManager : IDisposable, IEnumerable<KeyValuePair<string, string>>
    {
        string this[string index]
        {
            get;
        }

        bool ContainString(string str);

        void Add(string name, string stringLiteral);
    }
}
