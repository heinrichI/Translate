using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class TranslateResourceManager : IResourceManager
    {
        private IResourceManager _englishResource;
        private IResourceManager _hebrewResource;
        private TranslateMemory _tm;
        private ITranslatorService _translatorService;

        public TranslateResourceManager(IResourceManager englishResource, IResourceManager hebrewResource, TranslateMemory tm, ITranslatorService translatorService)
        {
            this._englishResource = englishResource;
            this._hebrewResource = hebrewResource;
            this._tm = tm;
            this._translatorService = translatorService;
        }

        public bool ContainValue(string name)
        {
            return _englishResource.ContainValue(name);
        }

        public bool ContainKey(string key)
        {
            return _englishResource.ContainKey(key);
        }

        //public string this[string index] => _englishResource[index];


        public void Add(string name, string stringLiteral)
        {
            _hebrewResource.Add(name, stringLiteral);

            string translate;
            if (_tm.Contain(stringLiteral))
            {
                translate = _tm.GetTm(stringLiteral);
                Console.WriteLine($"Used translate memory for {stringLiteral} - {translate}");
            }
            else
            {
                translate = _translatorService.Translate(stringLiteral, false);
                if (HebrewUtils.IsHebrewString(translate))
                {
                    Console.WriteLine($"Can not translate {stringLiteral}");
                    return;
                }
                _tm.Add(stringLiteral, translate);
            }
            _englishResource.Add(name, translate);
        }

        public void Dispose()
        {
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _englishResource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _englishResource.GetEnumerator();
        }

        public string GetKeyByValue(string value)
        {
            return _englishResource.GetKeyByValue(value);
        }
    }
}
