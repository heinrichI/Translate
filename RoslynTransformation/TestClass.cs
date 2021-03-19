using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTransformation
{
    public class TestClass
    {
        public static void Rewrite(string sampleCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);

            SyntaxNode root = tree.GetRoot();

            var compilation = CSharpCompilation.Create("TransformationCS",
                 new SyntaxTree[] { tree }, 
                 null, new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            var model = compilation.GetSemanticModel(tree);

            var rewriter = new StringLiteralRewriter(model);

            var newSource = rewriter.Visit(root);

            if (newSource != root)
            {
                Debug.WriteLine(newSource.ToFullString());
                //File.WriteAllText(sourceTree.FilePath, newSource.ToFullString());
            }
        }

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
    }
}
