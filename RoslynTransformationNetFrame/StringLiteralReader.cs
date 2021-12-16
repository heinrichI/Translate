using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTransformationNetFrame
{
    public class StringLiteralReader : CSharpSyntaxWalker
    {
        public List<string> Literals { get; } = new List<string>();

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.IsKind(SyntaxKind.StringLiteralExpression))
            {
                string stringLiteral = (string)node.Token.Value;

                if (HebrewUtils.IsHebrewString(stringLiteral))
                {
                    Replacer replacer = new Replacer();

                    stringLiteral = replacer.GetClearedStringLiteral(stringLiteral);

                    Literals.Add(stringLiteral);
                }
            }
        }
    }
}
