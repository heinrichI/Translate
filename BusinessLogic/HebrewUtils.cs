using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    class HebrewUtils
    {
        public static bool IsHebrewString(string text)
        {
            //return text.IndexOfAny(HebrewChars.ToCharArray()) >= 0;
            if (string.IsNullOrEmpty(text))
                return false;

            const string regex_match_hebrew = @"[\u0590-\u05FF]+";
            return Regex.IsMatch(text, regex_match_hebrew, RegexOptions.IgnoreCase);
        }
    }
}
