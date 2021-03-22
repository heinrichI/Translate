using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace RoslynTransformation
{
    public class RoslynManager
    {
        private readonly IResourceManager _resourceManager;

        public RoslynManager(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string Rewrite(string sampleCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);

            SyntaxNode root = tree.GetRoot();

            var compilation = CSharpCompilation.Create("TransformationCS",
                 new SyntaxTree[] { tree }, 
                 null, new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            var model = compilation.GetSemanticModel(tree);

            var rewriter = new StringLiteralRewriter(model, _resourceManager);

            var newSource = rewriter.Visit(root);

            if (newSource != root)
            {
                Debug.WriteLine(newSource.ToFullString());
                return newSource.ToFullString();
            }

            return string.Empty;
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
