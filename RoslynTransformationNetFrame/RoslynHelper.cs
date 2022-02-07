using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
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

        internal static string GetCaseSwithName(SyntaxNode node)
        {
            if (node is SwitchSectionSyntax switchSyntax)
            {
                SwitchLabelSyntax label = switchSyntax.Labels.FirstOrDefault();
                if (label != null)
                {
                    var name = GetSimpleMember(label, last: true);
                    if (string.IsNullOrEmpty(name)
                        && label is CaseSwitchLabelSyntax caseLabel)
                    {
                        if (caseLabel.Value is InvocationExpressionSyntax inv)
                        {
                            name = inv.ArgumentList.Arguments.FirstOrDefault().Expression.ToString();
                        }

                        if (string.IsNullOrEmpty(name))
                            name = caseLabel.Value.ToString().Trim('\"');
                    }
                    return name;
                }
            }
            return null;
        }

        internal static string GetFieldName(SyntaxNode node)
        {
            if (node is FieldDeclarationSyntax field)
            {
                if (field.Modifiers.Any(m => m.ValueText == "const"))
                {
                }

                return field.Declaration.Variables.First().Identifier.ValueText;
            }

            return null;
        }

        internal static string GetObjectName(SyntaxNode node)
        {
            if (node is InvocationExpressionSyntax invocationExpression)
            {
                //var sym = _semanticModel.GetSymbolInfo(invocationExpression);
                var identifiers = node.DescendantNodes().OfType<IdentifierNameSyntax>().ToList();
                if (identifiers.Count > 1)
                {
                    //если есть запрешенные слова, то возврашаем первый аргумент
                    if (identifiers.Any(i => BlackList.IsIn(i.ToString())))
                    {
                        var firstArgument = invocationExpression.ArgumentList.Arguments.FirstOrDefault();
                        if (firstArgument != null)
                        {
                            if (firstArgument.Expression is MemberAccessExpressionSyntax membExp)
                                return membExp.Name.ToString();
                            else if (firstArgument.Expression is LiteralExpressionSyntax)
                            {
                                string text = firstArgument.Expression.ToString().Trim('"');
                                if (!HebrewUtils.IsHebrewString(text))
                                    return text;
                            }
                        }
                    }
                    else
                    {
                        var identifier = identifiers.FirstOrDefault();
                        if (identifier != null)
                        {
                            string name = identifier.ToString();
                                return name;
                        }
                    }
                }
            }
            else if (node is ArgumentListSyntax argumentList)
            {
                var first = argumentList.Arguments.FirstOrDefault();
                if (first != null)
                {
                    if (first.Expression is IdentifierNameSyntax)
                        return first.Expression.ToString();
                    else if (first.Expression is ArrayCreationExpressionSyntax array)
                    {
                        var first2 = array.Initializer.Expressions.FirstOrDefault();
                        if (first2 != null && first2 is InvocationExpressionSyntax inv)
                        {
                            var identifier = inv
                             .DescendantNodes()
                             .OfType<ExpressionSyntax>()
                             .Where(m => m.Kind() == SyntaxKind.IdentifierName)
                             .FirstOrDefault();
                            if (identifier != null)
                                return identifier.ToString();
                        }
                    }
                }
            }
            else if (node is PropertyDeclarationSyntax property)
            {
                return property.Identifier.ValueText;
            }
            return null;
        }

        //private string GetSimpleAssigment(SyntaxNode parent)
        //{
        //    var simpleMember = node
        //                       .DescendantNodes()
        //                       .OfType<AssignmentExpressionSyntax>()
        //                       .Where(m => m.Kind() == SyntaxKind.SimpleMemberAccessExpression)
        //                       .FirstOrDefault();
        //    if (simpleMember != null)
        //    {
        //        return simpleMember.Name.ToString();
        //    }

        //    return null;
        //}

        internal static string GetSimpleMember(SyntaxNode parent, SyntaxNode target = null, bool last = false)
        {
            var simpleMembers = parent
                     .DescendantNodes()
                     .TakeWhile(f => target != null ? f != target : true)
                     .OfType<MemberAccessExpressionSyntax>()
                     .Where(m => m.Kind() == SyntaxKind.SimpleMemberAccessExpression);
            MemberAccessExpressionSyntax simpleMember = simpleMembers.FirstOrDefault();
            if (simpleMember != null && simpleMember.Expression != null)
            {
                int constIndex = simpleMember.Expression.ToString().IndexOf("const", StringComparison.InvariantCultureIgnoreCase);
                if (constIndex != -1
                    || last)
                {
                    return simpleMember.Name.ToString();

                }
                else
                {
                    if (simpleMember.Expression is IdentifierNameSyntax)
                        return simpleMember.Expression.ToString();
                }
            }

            return null;
        }

        internal static string GetFirstMemberNameFromArgumentList(SyntaxNode node)
        {
            try
            {
                var argumentList = node.DescendantNodes().OfType<BaseArgumentListSyntax>().FirstOrDefault();
                if (argumentList != null)
                {
                    var simpleMember = argumentList
                        .DescendantNodes()
                        .OfType<MemberAccessExpressionSyntax>()
                        .Where(m => m.Kind() == SyntaxKind.SimpleMemberAccessExpression)
                        .FirstOrDefault();
                    if (simpleMember != null)
                    {
                        return simpleMember.Name.ToString();
                    }
                }
                /*else if (node is ArgumentListSyntax argumenList)
                {
                    var firstArgument = argumenList.Arguments.FirstOrDefault();
                    if (firstArgument != null)
                        return firstArgument.Expression.ToString().Trim('"');
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{node.ToString()} {ex.Message}");
                //throw;
            }

            return string.Empty;
        }

    }
}
