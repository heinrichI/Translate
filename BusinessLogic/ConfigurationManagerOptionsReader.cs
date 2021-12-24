using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace BusinessLogic
{
    public static class ConfigurationManagerOptionsReader
    {
        public static ProgramTranslationOptions GetArguments(NameValueCollection AppSettings)
        {
            ProgramTranslationOptions result = new ProgramTranslationOptions();
            result.ResourcePath = AppSettings.Get("ResourcePath");
            result.FromLanguage = AppSettings.Get("FromLanguage");
            result.ToLanguage = AppSettings.Get("ToLanguage");
            result.Mode = AppSettings.Get("Mode");
            result.SkipError429 = Convert.ToBoolean(AppSettings.Get("Skip429"));
            result.SolutionPath = AppSettings.Get("SolutionPath");
            result.FileToRefactor = AppSettings.Get("FileToRefactor");

            return result;
        }
    }
}
