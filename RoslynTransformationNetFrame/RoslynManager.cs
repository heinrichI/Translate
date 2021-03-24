using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Linq;

namespace RoslynTransformationNetFrame
{
    public class RoslynManager
    {
        private readonly IResourceManager _translateResourceManager;

        public RoslynManager(IResourceManager translateResourceManager)
        {
            this._translateResourceManager = translateResourceManager;
        }

        public string Rewrite(string sourceCode, 
            string generatedCodeNamespace = null)
        {
            bool changed = false;
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            SyntaxNode root = tree.GetRoot();

            var compilation = CSharpCompilation.Create("TransformationCS",
                 new SyntaxTree[] { tree }, 
                 null, new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            var model = compilation.GetSemanticModel(tree);

            if (generatedCodeNamespace != null)
            {
                var rootCompUnit = (CompilationUnitSyntax)root;
                if (!rootCompUnit.Usings.Any(u => u.Name.ToString() == generatedCodeNamespace))
                {
                    changed = true;
                    NameSyntax nameSyntax = SyntaxFactory.ParseName(generatedCodeNamespace);
                    var name = SyntaxFactory.UsingDirective(nameSyntax);
                    //compilationUnitSyntax = compilationUnitSyntax
                    //    .AddUsings(Syntax.UsingDirective(name).NormalizeWhitespace());
                    root = rootCompUnit.AddUsings(name.NormalizeWhitespace()
                        .WithTrailingTrivia(new[] 
                        {
                            SyntaxFactory.CarriageReturnLineFeed
                            //SyntaxFactory.EndOfLine("\n"),
                            //SyntaxFactory.Whitespace("") 
                        })
                        );
                }
            }

            var rewriter = new StringLiteralRewriter(model, _translateResourceManager);

            var newSource = rewriter.Visit(root);
            if (newSource != root)
                changed = true;

            if (changed)
            {
                Debug.WriteLine(newSource.ToFullString());
                return newSource.ToFullString();
            }

            return string.Empty;
        }
    }
}
