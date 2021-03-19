using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace RoslynTransformation
{
    public class StringLiteralRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel SemanticModel;

        public StringLiteralRewriter(SemanticModel semanticModel) => SemanticModel = semanticModel;

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.IsKind(SyntaxKind.StringLiteralExpression))
            {
                //var first = node;

                var str = (string)node.Token.Value;
                //TypeSyntax variableTypeName = node..Type;
                NameSyntax name = IdentifierName("Strings")
                    .WithLeadingTrivia(node.GetLeadingTrivia())
                    .WithTrailingTrivia(node.GetTrailingTrivia());
                var newNode = QualifiedName(name, IdentifierName("Test1"));
                return newNode;
                //var newNode = LiteralExpression(SyntaxKind.StringLiteralExpression,
                //    Literal(
                //        TriviaList(),
                //        str + " 2",
                //        str,
                //        TriviaList()));

                //return node.ReplaceNode(node, newNode);
            }

            return node;
        }
    }
}
