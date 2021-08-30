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
            "\n", 
            " "
        };

        bool _trimmed = false;
        string _literal;

        public string GetClearedStringLiteral(string inputText)
        {
            foreach (var literal in _literals)
            {
                if (inputText.EndsWith(literal))
                {
                    _trimmed = true;
                    _literal = literal;
                    return inputText.Substring(0, inputText.Length - literal.Length);
                }
            }

            return inputText;
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
