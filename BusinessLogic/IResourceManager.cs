using System;

namespace BusinessLogic
{
    public interface IResourceManager : IDisposable
    {
        string this[string index]
        {
            get;
        }

        bool ContainString(string str);

        void Add(string name, string stringLiteral);
    }
}
