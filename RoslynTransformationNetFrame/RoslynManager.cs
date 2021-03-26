using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.MSBuild;

namespace RoslynTransformationNetFrame
{
    public class RoslynManager
    {
        private readonly IResourceManager _translateResourceManager;
        private readonly string _solutionPath;

        public RoslynManager(IResourceManager translateResourceManager,
            string solutionPath = null)
        {
            this._translateResourceManager = translateResourceManager;
            this._solutionPath = solutionPath;
        }

        public string Rewrite(string sourceCode, 
            string generatedCodeNamespace = null)
        {
            bool changed = false;
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            SyntaxNode root = tree.GetRoot();

            CSharpCompilation compilation = null;
            if (string.IsNullOrEmpty(_solutionPath))
            {
                compilation = CSharpCompilation.Create("TransformationCS",
                     new SyntaxTree[] { tree },
                     null, new CSharpCompilationOptions(OutputKind.ConsoleApplication));
            }
            else
            {
                //AdhocWorkspace ws = new AdhocWorkspace();

                //Solution solution = ws.AddSolution(SolutionInfo.Create(
                //    SolutionId.CreateNewId(),
                //    VersionStamp.Create(),
                //    filePath: _solutionPath
                //));


                Solution solution = null;
                //Task.Run(() =>
                //{
                    var workspace = MSBuildWorkspace.Create();
                    solution = workspace.OpenSolutionAsync(_solutionPath).Result;
                //}).Wait();
                if (!solution.Projects.Any())
                    throw new System.Exception("There is not any project!");
                var proj = solution.Projects.Single(p => p.FilePath == "c:\\Liram\\ramplus\\Liram.Ramplus.UI.Controls\\Liram.Ramplus.UI.Controls.csproj");
                var comp = proj.GetCompilationAsync().Result;

                var compilation2 = comp.AddSyntaxTrees(tree);
                var model2 = comp.GetSemanticModel(compilation2.SyntaxTrees.First());

                //var compilations = Task.WhenAll(solution.Projects.Select(x => x.GetCompilationAsync())).Result;

                //return compilations
                //    .SelectMany(compilation => compilation.SyntaxTrees.Select(syntaxTree => compilation.GetSemanticModel(syntaxTree)))
                //    .SelectMany(
                //        semanticModel => semanticModel
                //            .SyntaxTree
                //            .GetRoot()
                //            .DescendantNodes()
                //            .OfType<InterfaceDeclarationSyntax>()
                //            .Select(interfaceDeclarationSyntax => semanticModel.GetDeclaredSymbol(interfaceDeclarationSyntax)))
                //    .Where(symbol => predicate(symbol.ToDisplayString()))
                //    .ToImmutableList();

                //                var mscorlib = MetadataReference.CreateFromFile(_solutionPath);
                //                var ws = new AdhocWorkspace();
                //                //Create new solution
                //                var solId = SolutionId.CreateNewId();
                //                var solutionInfo = SolutionInfo.Create(solId, VersionStamp.Create());
                //                //Create new project
                //                var project = ws.AddProject("Sample", "C#");
                //                project = project.AddMetadataReference(mscorlib);
                //                //Add project to workspace
                //                ws.TryApplyChanges(project.Solution);
                //                string text = @"
                //class C
                //{
                //    void M()
                //    {
                //        M();
                //        M();
                //    }
                //}";
                //                var sourceText = SourceText.From(text);
                //                //Create new document
                //                var doc = ws.AddDocument(project.Id, "NewDoc", sourceText);
                //                //Get the semantic model
                //                var model = doc.GetSemanticModelAsync().Result;
                //                //Get the syntax node for the first invocation to M()
                //                var methodInvocation = doc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<InvocationExpressionSyntax>().First();
                //                var methodSymbol = model.GetSymbolInfo(methodInvocation).Symbol;
                //                //Finds all references to M()
                //                IEnumerable<ReferencedSymbol> referencesToM = SymbolFinder.FindReferencesAsync(methodSymbol, doc.Project.Solution).Result;
            }

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
