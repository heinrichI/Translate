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

        public static bool IsOnlyHebrewString(string text)
        {
            //return text.IndexOfAny(HebrewChars.ToCharArray()) >= 0;
            if (string.IsNullOrEmpty(text))
                return false;

            const string regex_match_hebrew = @"^[\u0590-\u05FF\s]+";
            bool match = Regex.IsMatch(text, regex_match_hebrew, RegexOptions.IgnoreCase);
            if (match)
            {
                bool onlySymbol = Regex.IsMatch(text, @"^[\s]+", RegexOptions.IgnoreCase);
                if (onlySymbol)
                    return false;
                else
                    return true;
            }
            return false;
        }
    }
}
