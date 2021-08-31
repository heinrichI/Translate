using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTransformationNetFrame
{
    class SymbolContainer
    {
        public SymbolContainer(char v, Func<int, string> p)
        {
            Char = v;
            GenerateString = p;
        }

        public char Char { get; }
        public Func<int, string> GenerateString { get; }
    }
}
