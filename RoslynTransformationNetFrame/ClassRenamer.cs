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
    // Below CSharpSyntaxRewriter renames multiple occurrences of a particular class name under the SyntaxNode being visited.
    // Note that the below rewriter is not a full / correct implementation of symbolic rename. For example, it doesn't
    // handle destructors / aliases etc. A full implementation for symbolic rename would be more complicated and is
    // beyond the scope of this sample. The intent of this sample is mainly to demonstrate how symbolic info can be used
    // in conjunction a rewriter to make syntactic changes.
    public class ClassRenamer : CSharpSyntaxRewriter
    {
        public ISymbol SearchSymbol { get; set; }
        public SemanticModel SemanticModel { get; set; }
        public string NewName { get; set; }

        // Replace old ClassDeclarationSyntax with new one.
        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var updatedClassDeclaration = (ClassDeclarationSyntax)base.VisitClassDeclaration(node);

            // Get TypeSymbol corresponding to the ClassDeclarationSyntax and check whether
            // it is the same as the TypeSymbol we are searching for.
            var classSymbol = SemanticModel.GetDeclaredSymbol(node);
            if (classSymbol.Equals(SearchSymbol))
            {
                // Replace the identifier token containing the name of the class.
                SyntaxToken updatedIdentifierToken =
                    SyntaxFactory.Identifier(
                        updatedClassDeclaration.Identifier.LeadingTrivia,
                        NewName,
                        updatedClassDeclaration.Identifier.TrailingTrivia);

                updatedClassDeclaration = updatedClassDeclaration.WithIdentifier(updatedIdentifierToken);
            }

            return updatedClassDeclaration;
        }

        // Replace old ConstructorDeclarationSyntax with new one.
        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var updatedConstructorDeclaration = (ConstructorDeclarationSyntax)base.VisitConstructorDeclaration(node);

            // Get TypeSymbol corresponding to the containing ClassDeclarationSyntax for the 
            // ConstructorDeclarationSyntax and check whether it is the same as the TypeSymbol
            // we are searching for.
            var classSymbol = (ITypeSymbol)SemanticModel.GetDeclaredSymbol(node).ContainingSymbol;
            if (classSymbol.Equals(SearchSymbol))
            {
                // Replace the identifier token containing the name of the class.
                SyntaxToken updatedIdentifierToken =
                    SyntaxFactory.Identifier(
                        updatedConstructorDeclaration.Identifier.LeadingTrivia,
                        NewName,
                        updatedConstructorDeclaration.Identifier.TrailingTrivia);

                updatedConstructorDeclaration = updatedConstructorDeclaration.WithIdentifier(updatedIdentifierToken);
            }

            return updatedConstructorDeclaration;
        }

        // Replace all occurrences of old class name with new one.
        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            var updatedIdentifierName = (IdentifierNameSyntax)base.VisitIdentifierName(node);

            // Get TypeSymbol corresponding to the IdentifierNameSyntax and check whether
            // it is the same as the TypeSymbol we are searching for.
            var identifierSymbol = SemanticModel.GetSymbolInfo(node).Symbol;

            // Handle |C| x = new C().
            var isMatchingTypeName = identifierSymbol.Equals(SearchSymbol);

            // Handle C x = new |C|().
            var isMatchingConstructor =
                identifierSymbol is IMethodSymbol &&
                ((IMethodSymbol)identifierSymbol).MethodKind == MethodKind.Constructor &&
                identifierSymbol.ContainingSymbol.Equals(SearchSymbol);

            if (isMatchingTypeName || isMatchingConstructor)
            {
                // Replace the identifier token containing the name of the class.
                SyntaxToken updatedIdentifierToken =
                    SyntaxFactory.Identifier(
                        updatedIdentifierName.Identifier.LeadingTrivia,
                        NewName,
                        updatedIdentifierName.Identifier.TrailingTrivia);

                updatedIdentifierName = updatedIdentifierName.WithIdentifier(updatedIdentifierToken);
            }

            return updatedIdentifierName;
        }
    }
}
