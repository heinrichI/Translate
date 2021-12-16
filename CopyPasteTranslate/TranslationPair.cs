using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyPasteTranslate
{
    public class TranslationPair : IEquatable<TranslationPair>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public TranslationPair()
        {

        }

        public TranslationPair(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return UpcaseFirstLetter(Name);
        }

        private static string UpcaseFirstLetter(string text)
        {
            if (text.Length > 1)
            {
                text = char.ToUpper(text[0]) + text.Substring(1).ToLowerInvariant();
            }

            return text;
        }

        public bool Equals(TranslationPair other)
        {
            return Code.Equals(other.Code);
        }

        public override int GetHashCode()
        {
            return Code != null ? Code.GetHashCode() : 0;
        }
    }
}
