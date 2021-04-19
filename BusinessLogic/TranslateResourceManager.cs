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
        private ITranslatorService _translatorService;
        private readonly bool _skip429;

        public TranslateResourceManager(IResourceManager englishResource,
            IResourceManager hebrewResource,
            ITranslatorService translatorService,
            bool skip429)
        {
            this._englishResource = englishResource;
            this._hebrewResource = hebrewResource;
            this._translatorService = translatorService;
            this._skip429 = skip429;
        }

        public bool ContainValue(string name)
        {
            return _hebrewResource.ContainValue(name);
        }

        public bool ContainKey(string key)
        {
            return _hebrewResource.ContainKey(key);
        }

        //public string this[string index] => _englishResource[index];


        public void Add(string name, string stringLiteral)
        {
            _hebrewResource.Add(name, stringLiteral);

            string translate = null;
            try
            {
                translate = _translatorService.Translate(stringLiteral, false);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (429) Too Many Requests.")
                {
                    if (_skip429)
                    {
                        Console.WriteLine(ex.Message);
                        _englishResource.Add(name, stringLiteral);
                        return;
                    }
                    else
                        throw;
                }
                else
                    throw;
            }

            if (HebrewUtils.IsHebrewString(translate))
            {
                Console.WriteLine($"Can not translate {stringLiteral}");
                return;
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
            return _hebrewResource.GetKeyByValue(value);
        }
    }
}
