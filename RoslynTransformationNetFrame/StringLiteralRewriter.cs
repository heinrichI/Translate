using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace RoslynTransformation
{
    public class StringLiteralRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel SemanticModel;

        public StringLiteralRewriter(SemanticModel semanticModel) => SemanticModel = semanticModel;

        string _currentClass;

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            _currentClass = node.Identifier.ValueText;
            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.IsKind(SyntaxKind.StringLiteralExpression))
            {
                var str = (string)node.Token.Value;

                //var en1 = node.DescendantNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
                //var en2 = node.Parent.Parent.DescendantNodes().OfType<IdentifierNameSyntax>();//.Where(i => i.Kind() == SyntaxKind.;
                ////var enumType = context.SemanticModel.GetTypeInfo(switchBlock.Expression).Type as INamedTypeSymbol;
                //IdentifierNameSyntax en3 = node.Parent.Parent.DescendantNodes().OfType<IdentifierNameSyntax>().Skip(2).First();
                //var symb = SemanticModel.GetDeclaredSymbol(en3);
                //var symb4 = SemanticModel.GetSymbolInfo(en3);
                //var symb3 = SemanticModel.GetType();
                //var symb2 = SemanticModel.GetTypeInfo(en3).Type;
                //var s1 = symb2.SpecialType;
                //var s2 = symb2.TypeKind;

                //var en4 = node.Parent.Parent.DescendantNodes().OfType<IdentifierNameSyntax>()
                //    .Where(i => SemanticModel.GetTypeInfo(i).Type.TypeKind == TypeKind.Enum)
                //    .Select(i => SemanticModel.GetTypeInfo(i));

                string enumName = GetEnumName(node.Parent.Parent);
                //string className = GetEnumName(node.Parent.Parent);

                //TypeSyntax variableTypeName = node..Type;
                NameSyntax name = SyntaxFactory.IdentifierName("Strings")
                    .WithLeadingTrivia(node.GetLeadingTrivia())
                    .WithTrailingTrivia(node.GetTrailingTrivia());
                var newNode = SyntaxFactory.QualifiedName(name, SyntaxFactory.IdentifierName($"{_currentClass}_{enumName}"));
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

        private string GetEnumName(SyntaxNode node)
        {
            IdentifierNameSyntax en = node.DescendantNodes().OfType<IdentifierNameSyntax>()
             .Where(i => SemanticModel.GetTypeInfo(i).Type.TypeKind == TypeKind.Enum).LastOrDefault();
            if (en != null)
            {
                return en.Identifier.ValueText;
            }
            return null;
        }
    }
}
