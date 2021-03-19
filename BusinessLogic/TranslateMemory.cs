using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BusinessLogic
{
    public class TranslateMemory : ITranslateMemory
    {
        Dictionary<string, string> _dictionary;
        string _fileName;

        public TranslateMemory(string fromLanguage, string toLanguage)
        {
            _fileName = $"{fromLanguage}_{toLanguage}.xml";
            if (File.Exists(_fileName))
            {
                _dictionary = new DictionarySerializer<string, string>().Deserialize(_fileName);
            }
            else
                _dictionary = new Dictionary<string, string>();
        }

        public bool Contain(string text)
        {
            return _dictionary.ContainsKey(text);
        }

        public string GetTm(string text)
        {
            return _dictionary[text];
        }

        public void Add(string text, string translate)
        {
            _dictionary.Add(text, translate);

            new DictionarySerializer<string, string>().Serialize(_fileName, _dictionary);
        }
    }
}
