using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTransformationNetFrame
{
    class Replacer
    {
        public Func<string, LiteralExpressionSyntax, QualifiedNameSyntax, BinaryExpressionSyntax> AddNodeAction { get; set; }

        string[] _literals = new string[] 
        {
            "\r",
            "\n",
            " "
        };

        bool _trimmed = false;
        string _literal = string.Empty;

        public string GetClearedStringLiteral(string inputText)
        {
            string curTrimmed = inputText;

            while(EndWith(curTrimmed, _literals, out string literal))
            {
                _trimmed = true;
                _literal = literal + _literal;
                curTrimmed = curTrimmed.Substring(0, curTrimmed.Length - literal.Length);
            }
         
            if (_trimmed)
                return curTrimmed;

            return inputText;
        }

        private static bool EndWith(string inputText, string[] literals, out string literal)
        {
            foreach (var lit in literals)
            {
                if (inputText.EndsWith(lit))
                {
                    literal = lit;
                    return true;
                }
            }
            literal = string.Empty;
            return false;
        }

        public BinaryExpressionSyntax AddNode(LiteralExpressionSyntax oldNode, QualifiedNameSyntax newNode)
        {
            if (_trimmed)
            {
                return AddNodeAction(_literal, oldNode, newNode);
            }

            return null;
        }
    }
}
