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
        public Func<string, LiteralExpressionSyntax, QualifiedNameSyntax, BinaryExpressionSyntax> AddNodeToEndAction { get; set; }

        public Func<string, LiteralExpressionSyntax, QualifiedNameSyntax, BinaryExpressionSyntax> AddNodeToStartAction { get; internal set; }

        public Func<string, string, LiteralExpressionSyntax, QualifiedNameSyntax, ExpressionSyntax> AddNodeAction { get; internal set; }

        string[] _endLiterals = new string[] 
        {
            "\r",
            "\n",
            " ",
            ">",
        };  
        
        string[] _startLiterals = new string[] 
        {
            "<",
        };

        bool _trimmed = false;
        string _endLiteral = string.Empty;
        string _startLiteral = string.Empty;

        public string GetClearedStringLiteral(string inputText)
        {
            string curTrimmed = inputText;

            while(EndWith(curTrimmed, _endLiterals, out string literal))
            {
                _trimmed = true;
                _endLiteral = literal + _endLiteral;
                curTrimmed = curTrimmed.Substring(0, curTrimmed.Length - literal.Length);
            }

            while (StartWith(curTrimmed, _startLiterals, out string literal))
            {
                _trimmed = true;
                _startLiteral = _startLiteral + literal;
                curTrimmed = curTrimmed.Substring(literal.Length, curTrimmed.Length - literal.Length);
            }

            if (_trimmed)
                return curTrimmed;

            return inputText;
        }

        private static bool StartWith(string inputText, string[] literals, out string literal)
        {
            foreach (var lit in literals)
            {
                if (inputText.StartsWith(lit))
                {
                    literal = lit;
                    return true;
                }
            }
            literal = string.Empty;
            return false;
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

        public ExpressionSyntax AddNode(LiteralExpressionSyntax oldNode, QualifiedNameSyntax newNode)
        {
            if (_trimmed)
            {
                return AddNodeAction(_startLiteral, _endLiteral, oldNode, newNode);
            }

            return null;
        }
    }
}
