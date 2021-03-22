using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace RoslynTransformation
{
    public class StringLiteralRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private readonly IResourceManager _resourceManager;

        public StringLiteralRewriter(SemanticModel semanticModel,
            IResourceManager resourceManager)
        {
            _semanticModel = semanticModel;
            _resourceManager = resourceManager;
        }

        string _currentClass = null;
        string _currentMethod;

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (_currentClass == null)
                _currentClass = node.Identifier.ValueText;
            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            _currentMethod = node.Identifier.ValueText;
            return base.VisitMethodDeclaration(node);
        }

        string _expressionLeft;
        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            //_variableName = node.Identifier.ValueText;
            if (node.Expression is AssignmentExpressionSyntax exp)
            {
                if (exp.Left is IdentifierNameSyntax left)
                {
                    _expressionLeft = left.Identifier.ValueText;
                }
            }
            return base.VisitExpressionStatement(node);
        }

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.IsKind(SyntaxKind.StringLiteralExpression))
            {
                string stringLiteral = (string)node.Token.Value;

                if (HebrewUtils.IsHebrewString(stringLiteral))
                {

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

                    //var en = node.Parent.Parent.Parent.Parent.Parent.DescendantNodes().OfType<ExpressionStatementSyntax>();


                    //TypeSyntax variableTypeName = node..Type;
                    NameSyntax nameSyntax = SyntaxFactory.IdentifierName("Strings")
                        .WithLeadingTrivia(node.GetLeadingTrivia())
                        .WithTrailingTrivia(node.GetTrailingTrivia());

                    string name;
                    if (_resourceManager.ContainString(stringLiteral))
                    {
                        name = _resourceManager[stringLiteral];
                    }
                    else
                    {
                        name = GenerateName(node);
                        _resourceManager.Add(name, stringLiteral);
                    }


                    var newNode = SyntaxFactory.QualifiedName(nameSyntax, SyntaxFactory.IdentifierName(name));
                    return newNode;
                    //var newNode = LiteralExpression(SyntaxKind.StringLiteralExpression,
                    //    Literal(
                    //        TriviaList(),
                    //        str + " 2",
                    //        str,
                    //        TriviaList()));

                    //return node.ReplaceNode(node, newNode);
                }
            }

            return node;
        }

        private string GenerateName(SyntaxNode node)
        {
            string newName;
            string enumName = GetEnumName(node.Parent.Parent);
            if (!string.IsNullOrEmpty(enumName))
            {
                newName = enumName;
            }
            else
            {
                string postFix = _expressionLeft;
                if (string.IsNullOrEmpty(postFix))
                    postFix = _currentMethod;

                newName = $"{_currentClass}_{postFix}";
            }

            if (_resourceManager.ContainString(newName))
                newName = NameGenerator.Generate(newName, _resourceManager);
            
            return newName;
        }

        private string GetEnumName(SyntaxNode node)
        {
            IdentifierNameSyntax en = node.DescendantNodes().OfType<IdentifierNameSyntax>()
             .Where(i => _semanticModel.GetTypeInfo(i).Type?.TypeKind == TypeKind.Enum)?.LastOrDefault();
            if (en != null)
            {
                return en.Identifier.ValueText;
            }
            return null;
        }
    }
}
