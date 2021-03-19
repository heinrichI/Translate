using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ITranslatorService
    {
        string Translate(string unstranslatedText, bool optimizeSpecialSequences);
    }
}
