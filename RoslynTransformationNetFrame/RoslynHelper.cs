using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace RoslynTransformationNetFrame
{
    public class RoslynHelper
    {
        public static void ParseByUsingTheObjectModel(string sampleCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            foreach (MemberDeclarationSyntax item in root.Members)
            {
                if (item.Kind() == SyntaxKind.ClassDeclaration)
                {
                    var @class = (ClassDeclarationSyntax)item;

                    foreach (var subItem in @class.Members)
                    {
                        if (subItem.Kind() == SyntaxKind.MethodDeclaration)
                        {
                            var method = (MethodDeclarationSyntax)subItem;
                            // etc….	
                        }
                    }
                }
            }
        }

        public static string ExtractNamespace(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            SyntaxNode root = tree.GetRoot();

            //var rootCompUnit = (CompilationUnitSyntax)root;
            //NamespaceDeclarationSyntax y = (NamespaceDeclarationSyntax)rootCompUnit.Members.Where(m => m.IsKind(SyntaxKind.NamespaceDeclaration)).Single();
            //var y2 = y.Name.ToString();

            var namespaceNodes = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>();
            //CompilationUnitSyntax
            NamespaceDeclarationSyntax namespaceNode = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            if (namespaceNode != null)
            {
                return namespaceNode.Name.ToString();
                //if (namespaceNode.Name is IdentifierNameSyntax name)
                //{
                //    return name.Identifier.ValueText;
                //}
                //if (namespaceNode.Name is QualifiedNameSyntax qname)
                //{
                //    return qname.Right.Identifier.ValueText;
                //}
            }

            return null;
        }

        public static bool IsInternalClass(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            SyntaxNode root = tree.GetRoot();

            var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            {
                if (classDeclaration.Modifiers.Any())
                {
                    if (classDeclaration.Modifiers[0].Kind() == SyntaxKind.InternalKeyword)
                        return true;
                }
            }

            return false;
        }

        public static string Refactor(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            SyntaxNode root = tree.GetRoot();

            var compilation = CSharpCompilation.Create("TransformationCS",
                 new SyntaxTree[] { tree },
                 null, new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            var model = compilation.GetSemanticModel(tree);

            // Get the ClassDeclarationSyntax corresponding to 'class C' above.
            //var fields = root
            //    .DescendantNodes().OfType<FieldDeclarationSyntax>()
            //    .Where(f => f.Declaration.Variables.Any(v =>
            //    {
            //        if (model.GetDeclaredSymbol(v) is IFieldSymbol symbol)
            //        {
            //            if (symbol.DeclaredAccessibility == Accessibility.Private
            //                && !symbol.Name.StartsWith("_"))
            //            return true;
            //        }
            //        return false;
            //    })).ToList();

            var variables = root
                .DescendantNodes().OfType<VariableDeclaratorSyntax>()
                .Where(v =>
                {
                    if (model.GetDeclaredSymbol(v) is IFieldSymbol symbol)
                    {
                        if (symbol.DeclaredAccessibility == Accessibility.Private
                            && !symbol.Name.StartsWith("_"))
                            return true;
                    }
                    return false;
                }).ToList();

            // Get Symbol corresponding to class C above.
            var searchSymbol = model.GetDeclaredSymbol(variables.First());
            var rewriter = new ClassRenamer()
            {
                SearchSymbol = searchSymbol,
                SemanticModel = model,
                NewName = "C1"
            };
            SyntaxNode newRoot = rewriter.Visit(root);

            return newRoot.ToFullString();
        }

    }
}
