using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateProgram
{
    internal class Option
    {
        public string FromLanguage { get; set; }

        public string ToLanguage { get; set; }

        public string FileToRefactor { get; set; }

        public string ResourcePath { get; set; }

        public string Mode { get; set; }

        public bool Skip429Error { get; set; }

        public string SolutionPath { get; set; }
    }
}
