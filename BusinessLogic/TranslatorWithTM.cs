using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class TranslatorWithTM : ITranslatorService
    {
        private TranslateMemory _translateMemory;
        private ITranslatorService _translatorService;

        public TranslatorWithTM(TranslateMemory translateMemory,
            ITranslatorService translatorService)
        {
            this._translateMemory = translateMemory;
            _translatorService = translatorService;
        }

        public string Translate(string unstranslatedText, bool optimizeSpecialSequences)
        {
            string translate = null;
            if (_translateMemory.Contain(unstranslatedText))
            {
                translate = _translateMemory.GetTm(unstranslatedText);
                Console.WriteLine($"Used translate memory for {unstranslatedText} - {translate}");
            }
            else
            {
                Console.WriteLine($"Request translate for {unstranslatedText}");
                translate = _translatorService.Translate(unstranslatedText, true);
                if (!HebrewUtils.IsHebrewString(translate))
                {
                    _translateMemory.Add(unstranslatedText, translate);
                }
            }

            return translate;
        }
    }
}
