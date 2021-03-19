using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ITranslateMemory
    {
        bool Contain(string text);

        string GetTm(string text);

        void Add(string text, string translate);
    }
}
