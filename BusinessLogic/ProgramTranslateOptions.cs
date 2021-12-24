using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace BusinessLogic
{
    public class ProgramTranslationOptions
    {
        [Option("ResourcePath", Required = false, HelpText = "Path to resource file. Should not include file name.")]
        virtual public string ResourcePath { get; set; }

        [Option("FromLanguage", Required = false, HelpText = "")]
        virtual public string FromLanguage { get; set; }

        [Option("ToLanguage", Required = false, HelpText = "")]
        virtual public string ToLanguage { get; set; }

        [Option("Mode", Required = false, HelpText = "")]
        virtual public string Mode { get; set; }

        [Option("SkipError429", Required = false, HelpText = "")]
        virtual public bool? SkipError429 { get; set; }

        [Option("SolutionPath", Required = false, HelpText = "")]
        virtual public string SolutionPath { get; set; }

        [Option("FileToRefactor", Required = false, HelpText = "")]
        virtual public string FileToRefactor { get; set; }

        public ProgramTranslationOptions fillNullsFrom(ProgramTranslationOptions other)
        {
            ResourcePath = ResourcePath ?? other.ResourcePath;
            FromLanguage = FromLanguage ?? other.FromLanguage;
            ToLanguage = ToLanguage ?? other.ToLanguage;
            Mode = Mode ?? other.Mode;
            SkipError429 = SkipError429 ?? other.SkipError429;
            SolutionPath = SolutionPath ?? other.SolutionPath;
            FileToRefactor = FileToRefactor ?? other.FileToRefactor;
            return this;
        }
    }
}
