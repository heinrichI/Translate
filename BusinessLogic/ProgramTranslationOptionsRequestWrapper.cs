using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ProgramTranslationOptionsRequestWrapper : ProgramTranslationOptions
    {
        private static string ReadLineWithRequest(string requestString)
        {
            Console.WriteLine(requestString);
            return Console.ReadLine();
        }

        ProgramTranslationOptions options;

        public ProgramTranslationOptionsRequestWrapper(ProgramTranslationOptions options)
        {
            this.options = options;
        }

        override public string ResourcePath { 
            set {options.ResourcePath = value;}
            get
            {
                if (options.ResourcePath == null)
                    options.ResourcePath = ReadLineWithRequest("Enter ResourcePath: ");
                return options.ResourcePath;
            }
        }


        override public string FromLanguage
        {
            set { options.FromLanguage = value; }
            get
            {
                if (options.FromLanguage == null)
                    options.FromLanguage = ReadLineWithRequest("Enter FromLanguage: ");
                return options.FromLanguage;
            }
        }

        override public string ToLanguage
        {
            set { options.ToLanguage = value; }
            get
            {
                if (options.ToLanguage == null)
                    options.ToLanguage = ReadLineWithRequest("Enter ToLanguage: ");
                return options.ToLanguage;
            }
        }

        override public string Mode
        {
            set { options.Mode = value; }
            get
            {
                if (options.Mode == null)
                    options.Mode = ReadLineWithRequest("Enter Mode: ");
                return options.Mode;
            }
        }

        override public bool? SkipError429
        {
            set { options.SkipError429 = value; }
            get
            {
                if (options.SkipError429 == null)
                    options.SkipError429 = bool.Parse(ReadLineWithRequest("Enter SkipError429: "));
                return options.SkipError429;
            }
        }

        override public string SolutionPath
        {
            set { options.SolutionPath = value; }
            get
            {
                if (options.SolutionPath == null)
                    options.SolutionPath = ReadLineWithRequest("Enter SolutionPath: ");
                return options.SolutionPath;
            }
        }

        override public string FileToRefactor
        {
            set { options.FileToRefactor = value; }
            get
            {
                if (options.FileToRefactor == null)
                    options.FileToRefactor = ReadLineWithRequest("Enter FileToRefactor: ");
                return options.FileToRefactor;
            }
        }
    }
}
