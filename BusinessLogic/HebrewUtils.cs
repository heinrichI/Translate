using System;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class HebrewUtils
    {
        public static bool IsHebrewString(string text)
        {
            //return text.IndexOfAny(HebrewChars.ToCharArray()) >= 0;
            if (string.IsNullOrEmpty(text))
                return false;

            const string regex_match_hebrew = @"[\u0590-\u05FF]+";
            return Regex.IsMatch(text, regex_match_hebrew, RegexOptions.IgnoreCase);
        }

        public bool IsHebrewString(object name)
        {
            throw new NotImplementedException();
        }
    }
}
