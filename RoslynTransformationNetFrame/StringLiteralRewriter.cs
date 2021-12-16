using BusinessLogic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoslynTransformationNetFrame
{
    public class StringLiteralRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private readonly IResourceManager _translateResourceManager;

        string _currentClass = null;
        string _currentMethod;
        string _expressionLeft;
        string _variableName;

        public StringLiteralRewriter(SemanticModel semanticModel,
           IResourceManager translateResourceManager)
        {
            _semanticModel = semanticModel;
            _translateResourceManager = translateResourceManager;
        }

        /*public override SyntaxList<FieldDeclarationSyntax> VisitList<FieldDeclarationSyntax>(SyntaxList<FieldDeclarationSyntax> list)
        //public SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list)
        //    where TNode : FieldDeclarationSyntax
        {
            if (list.Count > 0 && list[0].Parent is ClassDeclarationSyntax)
            {
                FieldDeclarationSyntax fie = list[0];

                var variableDeclaration = SyntaxFactory.VariableDeclaration(
                    fie.Declaration.Type)
                    .AddVariables(var1);

                var fieldDeclaration = SyntaxFactory.FieldDeclaration(variableDeclaration)
               //.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
               .AddModifiers(replace[0].Modifiers.ToArray())
               .WithTriviaFrom(replace[0]);

                var variableDeclaration2 = SyntaxFactory.VariableDeclaration(
                   replace[0].Declaration.Type)
                   .AddVariables(var2);

                base.VisitList(list.Add());
            }
            return base.VisitList(list);
        }*/

        
        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (_currentClass == null)
                _currentClass = node.Identifier.ValueText;

            return base.VisitClassDeclaration(node);

            /*
            //rename all field declaration 

            base.VisitClassDeclaration(node);
           
            var fields = node.DescendantNodes().OfType<FieldDeclarationSyntax>();
            List<ISymbol> symbolsToRename = new List<ISymbol>();
            List<FieldDeclarationSyntax> replace = new List<FieldDeclarationSyntax>();
            foreach (var field in fields)
            {
                if (field.Declaration.Variables.Count > 1)
                    replace.Add(field);
                //foreach (VariableDeclaratorSyntax variable in field.Declaration.Variables)
                //{
                //    ISymbol symbol = _semanticModel.GetDeclaredSymbol(variable);
                //    symbolsToRename.Add(symbol);
                //}
            }

            VariableDeclaratorSyntax var1 = replace[0].Declaration.Variables.First();
            VariableDeclaratorSyntax var2 = replace[0].Declaration.Variables[1];
            //ISymbol symbol = _semanticModel.GetDeclaredSymbol(var1);

            // Create a string variable: (bool canceled;)
            var variableDeclaration = SyntaxFactory.VariableDeclaration(
                replace[0].Declaration.Type)
                .AddVariables(var1);

            var fieldDeclaration = SyntaxFactory.FieldDeclaration(variableDeclaration)
                    //.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
                    .AddModifiers(replace[0].Modifiers.ToArray())
                    .WithTriviaFrom(replace[0]);

            var variableDeclaration2 = SyntaxFactory.VariableDeclaration(
               replace[0].Declaration.Type)
               .AddVariables(var2);

            var fieldDeclaration2 = SyntaxFactory.FieldDeclaration(variableDeclaration2)
                .AddModifiers(replace[0].Modifiers.ToArray())
                .WithTrailingTrivia(new[]
                {
                    SyntaxFactory.CarriageReturnLineFeed
                });

            var newNode = node.ReplaceNode(replace[0], fieldDeclaration);
            //.WithTriviaFrom(node);

            var fieldDeclarationInNewRoot = newNode.FindNode(fieldDeclaration.Span);
            //newNode = newNode.InsertNodesAfter(fieldDeclarationInNewRoot, new[] { fieldDeclaration2 });
            //newNode.WithAdditionalAnnotations()
            int index = node.Members.IndexOf(replace[0]);

            var newClass = newNode
                .WithMembers(newNode.Members.Insert(index + 1, fieldDeclaration2));
            return newClass;

            //return SyntaxFactory.VariableDeclarator(
            //        SyntaxFactory.SeparatedList(new[] { symbolsToRename[0] }),
            //        declarator.AsClause,
            //        null)
            //    .WithTriviaFrom(node);


            //var declarators = declarator.Names.Select(n =>
            //SyntaxFactory.VariableDeclarator(
            //    SyntaxFactory.SeparatedList(new[] { n }),
            //    declarator.AsClause,
            //    null))
            //    .ToList();

            //if (declarator.Initializer != null)
            //{
            //    var last = declarators.Last();
            //    last = last.WithInitializer(declarator.Initializer);
            //    declarators[declarators.Count - 1] = last;
            //}

            //return declarators.Select(d =>
            //    d.WithTrailingTrivia(SyntaxFactory.EndOfLineTrivia(Environment.NewLine))
            //        .WithAdditionalAnnotations(Formatter.Annotation));
            */
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            _currentMethod = node.Identifier.ValueText;
            return base.VisitMethodDeclaration(node);
        }

        //public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        //{
        //    //_variableName = node.Identifier.ValueText;
        //    if (node.Expression is AssignmentExpressionSyntax exp)
        //    {
        //        if (exp.Left is IdentifierNameSyntax left)
        //        {
        //            _expressionLeft = left.Identifier.ValueText;
        //        }
        //    }
        //    var visited = base.VisitExpressionStatement(node);
        //    _expressionLeft = null;
        //    return visited;
        //}

        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (node.Left is IdentifierNameSyntax left)
            {
                _expressionLeft = left.Identifier.ValueText;
            }
            var visited = base.VisitAssignmentExpression(node);
           _expressionLeft = null;
            return visited;
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (node.Declaration.Variables.Any())
            {
                var var1 = node.Declaration.Variables.First();
                _variableName = var1.Identifier.ValueText;
                //if (varDecl. is IdentifierNameSyntax left)
                //{
                //    _expressionLeft = left.Identifier.ValueText;
                //}
            }
            var visited = base.VisitLocalDeclarationStatement(node);
            _variableName = null;
            return visited;
        }

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var list = node.GetLeadingTrivia();
            var endOfLine = list.Where(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
            if (!endOfLine.Any())
            {
                list = list.Add(SyntaxFactory.CarriageReturnLineFeed);

                return base.VisitNamespaceDeclaration(node)
                    .WithLeadingTrivia(list)
                    .WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return base.VisitNamespaceDeclaration(node);
        }

        /*
         * public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            List<IFieldSymbol> symbolsToRename = new List<IFieldSymbol>();
            foreach (VariableDeclaratorSyntax variable in node.Declaration.Variables)
            {
                ISymbol symbol = _semanticModel.GetDeclaredSymbol(variable);
                if (symbol is IFieldSymbol fieldSymbol)
                {
                    if (fieldSymbol.DeclaredAccessibility == Accessibility.Private
                        && !fieldSymbol.Name.StartsWith("_"))
                    {
                        //rename
                        symbolsToRename.Add(fieldSymbol);
                       // node.ReplaceSyntax()
                        //return variable.WithIdentifier(
                        //    SyntaxFactory.Identifier($"_{fieldSymbol.Name}"))
                        //    .WithTriviaFrom(node);
                    }
                }
                // Do stuff with the symbol here
            }

            if (symbolsToRename.Count > 0)
            {
                return node.AddDeclarationVariables(new[]
                {
                SyntaxFactory.VariableDeclarator(symbolsToRename[0].Name)
            });
            }

            //var t = node.Declaration.Type;
            //ISymbol semantic = _semanticModel.GetDeclaredSymbol(node);
            ////if (semantic.DeclaredAccessibility == Accessibility.Private)
            ////{
            ////    Renamer.RenameSymbolAsync
            ////}
            ////var symbolInfo = _semanticModel.GetSymbolInfo(node);
            //var symb2 = _semanticModel.GetTypeInfo(node);
            ////if (symbolInfo.Symbol.Is)
            return base.VisitFieldDeclaration(node);
        }
        */

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

                    Replacer replacer = new Replacer();
                    replacer.AddNodeAction = (
                        string startLiteral,
                        string endLiteral,
                        LiteralExpressionSyntax oldNode,
                        QualifiedNameSyntax newNode2) =>
                    {
                        ExpressionSyntax result = newNode2;

                        if (!string.IsNullOrEmpty(startLiteral))
                        {
                            if (!result.HasLeadingTrivia)
                                result = result.WithLeadingTrivia(SyntaxFactory.Whitespace(" "));

                            var text = SyntaxFactory.LiteralExpression(
                           SyntaxKind.StringLiteralExpression,
                           SyntaxFactory.Literal(startLiteral))
                           .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                           .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                            result = SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, text, result)
                                 .WithLeadingTrivia(oldNode.GetLeadingTrivia());
                        }

                        if (!string.IsNullOrEmpty(endLiteral))
                        {
                            var text1 = SyntaxFactory.LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                SyntaxFactory.Literal(endLiteral))
                                .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                                .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                            result = SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, result, text1)
                                 .WithTrailingTrivia(oldNode.GetTrailingTrivia());
                        }

                        return result;
                    };
                    /*replacer.AddNodeToEndAction = (string literal,
                        LiteralExpressionSyntax oldNode,
                        ExpressionSyntax newNode2) =>
                    {
                        var text = SyntaxFactory.LiteralExpression(
                            SyntaxKind.StringLiteralExpression, 
                            SyntaxFactory.Literal(literal))
                            .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                            .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                        return SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, newNode2, text)
                             .WithTrailingTrivia(oldNode.GetTrailingTrivia());
                    };
                    replacer.AddNodeToStartAction = (string literal,
                        LiteralExpressionSyntax oldNode,
                        ExpressionSyntax newNode2) =>
                    {
                        var text = SyntaxFactory.LiteralExpression(
                            SyntaxKind.StringLiteralExpression, 
                            SyntaxFactory.Literal(literal))
                            .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                            .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                        return SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, text, newNode2 )
                             .WithLeadingTrivia(oldNode.GetLeadingTrivia());
                    };*/

                    stringLiteral = replacer.GetClearedStringLiteral(stringLiteral);

                    string name = GetNameAndAddToResource(stringLiteral, node);

                    //TypeSyntax variableTypeName = node..Type;
                    NameSyntax nameSyntax = SyntaxFactory.IdentifierName("Strings")
                        .WithLeadingTrivia(node.GetLeadingTrivia());

                    QualifiedNameSyntax newNode = SyntaxFactory.QualifiedName(
                        nameSyntax, 
                        SyntaxFactory.IdentifierName(name))
                        .WithTrailingTrivia(node.GetTrailingTrivia());

                    var replaced = replacer.AddNode(node, newNode);
                    if (replaced != null)
                    {
                        return replaced;
                    }
                    //if (trimmed)
                    //{
                    //    var text = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                    //    SyntaxFactory.Literal(literal))
                    //        .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                    //        .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                    //    return SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, newNode, text)
                    //         .WithTrailingTrivia(node.GetTrailingTrivia());
                    //}
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

        private string GetNameAndAddToResource(string stringLiteral, SyntaxNode node)
        {
            if (_translateResourceManager.ContainValue(stringLiteral))
            {
                string name = _translateResourceManager.GetKeyByValue(stringLiteral);
                Console.WriteLine($"Resource already have string {stringLiteral}, insert him key {name}");
                return name;
            }
            else
            {
                string name = GenerateName(node);
                _translateResourceManager.Add(name, stringLiteral);
                return name;
            }
        }

        public override SyntaxNode VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
        {
            bool needRewrite = false;
            SyntaxList<InterpolatedStringContentSyntax> newList = new SyntaxList<InterpolatedStringContentSyntax>();
            //NameSyntax nameSyntax2 = SyntaxFactory.IdentifierName("Strings");
            //var newNode2 = SyntaxFactory.QualifiedName(nameSyntax2, SyntaxFactory.IdentifierName("Test"));
            //var interpolationSyntax2 = SyntaxFactory.Interpolation(newNode2);
            //newList2 = newList2.Add(interpolationSyntax2);
            //return node.WithContents(newList2);

            //InterpolatedStringExpressionSyntax newNode2 = node;

            foreach (InterpolatedStringContentSyntax content in node.Contents)
            {
                if (content is InterpolatedStringTextSyntax stringContent)
                {
                    if (HebrewUtils.IsHebrewString(stringContent.TextToken.ValueText))
                    {
                        needRewrite = true;

                        ReplacerInterpolatedString replacer = new ReplacerInterpolatedString();
                        string trimmed = stringContent.TextToken.ValueText;

                        trimmed = replacer.Trim(trimmed);


                        //List<InterpolatedStringContentSyntax> list = new List<InterpolatedStringContentSyntax>();

                        newList = replacer.AddStartTokens(newList);

                        string name = GetNameAndAddToResource(trimmed, node);

                        NameSyntax nameSyntax = SyntaxFactory.IdentifierName("Strings");
                            //.WithLeadingTrivia(node.GetLeadingTrivia());

                        var newNode = SyntaxFactory.QualifiedName(nameSyntax, SyntaxFactory.IdentifierName(name));
                        //.WithTrailingTrivia(node.GetTrailingTrivia())
                        // .WithLeadingTrivia(node.GetLeadingTrivia());

                        var interpolationSyntax = SyntaxFactory.Interpolation(newNode);
                        newList = newList.Add(interpolationSyntax);
                        // var newNode2 = node.ReplaceNode(content, interpolationSyntax);

                        newList = replacer.AddEndTokens(newList);
                        
                        //var newNode3 = newNode2.AddContents(interpolatedWhiteSpace);
                        //var newList = node.Contents.Insert(0, interpolatedWhiteSpace);
                        //return node.ReplaceNodes(node.Contents, newList);
                        //node.Contents.(node.Contents, newList);
                        //var newNode3 = newNode2.AddContents(newList);
                        //var newList = node.Contents.ReplaceRange(content, list);

                        //var index = newList.IndexOf(content);
                        //return node.WithContents(newList);

                        //return node.WithLeadingTrivia(node.GetLeadingTrivia());
                    }
                    else
                    {
                        newList = newList.Add(content);
                    }
                }
                else
                {
                    newList = newList.Add(content);
                }
            }
            if (needRewrite)
            {
                return node.WithContents(newList);
            }
            return base.VisitInterpolatedStringExpression(node);
        }

        /*public override SyntaxNode VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            if (HebrewUtils.IsHebrewString(node.TextToken.ValueText))
            {
                string name = GetNameAndAddToResource(node.TextToken.ValueText, node);

                NameSyntax nameSyntax = SyntaxFactory.IdentifierName("Strings");
                var newNode = SyntaxFactory.QualifiedName(nameSyntax, SyntaxFactory.IdentifierName(name));
                var interpolationSyntax = SyntaxFactory.Interpolation(newNode);
                return interpolationSyntax;
                //list.Add(interpolationSyntax);

                //return parent.Contents.ReplaceRange(node, list);
            }
            return base.VisitInterpolatedStringText(node);
        }*/


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
               string postFix = GetPostFix(node);

                newName = $"{_currentClass}_{postFix}";
            }

            if (_translateResourceManager.ContainKey(newName))
                newName = NameGenerator.Generate(newName, _translateResourceManager);
            
            return newName;
        }

        private string GetPostFix(SyntaxNode node)
        {
            string postFix = null;
            if (string.IsNullOrEmpty(postFix)
                 && !node.IsKind(SyntaxKind.InterpolatedStringExpression))
            {
                postFix = RoslynHelper.GetSimpleMember(node.Parent, node);
                if (!string.IsNullOrEmpty(postFix))
                    return postFix;
            }

            if (!string.IsNullOrEmpty(_expressionLeft))
                return _expressionLeft;
            if (!string.IsNullOrEmpty(_variableName))
                return _variableName;
            if (string.IsNullOrEmpty(postFix))
                postFix = GetConstName(node.Parent.Parent);
            // if (string.IsNullOrEmpty(postFix))
            //    postFix = GetSimpleAssigment(node.Parent);
            if (!node.IsKind(SyntaxKind.InterpolatedStringExpression))
            {
                if (string.IsNullOrEmpty(postFix))
                    postFix = RoslynHelper.GetFirstMemberNameFromArgumentList(node.Parent.Parent);
                if (!(node.Parent.Parent.Parent is BlockSyntax))
                {
                    if (string.IsNullOrEmpty(postFix))
                        postFix = RoslynHelper.GetObjectName(node.Parent.Parent.Parent);
                    if (!(node.Parent.Parent.Parent.Parent is BlockSyntax))
                    {
                        if (string.IsNullOrEmpty(postFix))
                            postFix = RoslynHelper.GetObjectName(node.Parent.Parent.Parent.Parent);
                        if (!(node.Parent.Parent.Parent.Parent.Parent is BlockSyntax))
                        {
                            if (string.IsNullOrEmpty(postFix))
                                postFix = RoslynHelper.GetObjectName(node.Parent.Parent.Parent.Parent.Parent);
                            if (!(node.Parent.Parent.Parent.Parent.Parent.Parent is BlockSyntax))
                            {
                                if (string.IsNullOrEmpty(postFix))
                                    postFix = RoslynHelper.GetObjectName(node.Parent.Parent.Parent.Parent.Parent.Parent);
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(postFix))
                    postFix = RoslynHelper.GetFieldName(node.Parent.Parent.Parent.Parent);

                if (string.IsNullOrEmpty(postFix))
                    postFix = RoslynHelper.GetCaseSwithName(node.Parent.Parent);
            }
            if (string.IsNullOrEmpty(postFix))
                postFix = _currentMethod;

            return postFix;
        }

        private string GetConstName(SyntaxNode node)
        {
            try
            {
                MemberAccessExpressionSyntax identifier = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().FirstOrDefault();
                if (identifier != null)
                {
                    var sym = _semanticModel.GetSymbolInfo(identifier);
                    if (sym.Symbol is IFieldSymbol field
                        && field.IsConst)
                    {
                        return sym.Symbol.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{node.ToString()} {ex.Message}");
                //throw;
            }

            return string.Empty;
        }

        private string GetEnumName(SyntaxNode node)
        {
            var identifiers = node.DescendantNodes().OfType<IdentifierNameSyntax>();
            //foreach (var identifier in identifiers)
            //{
            //    var typeIden = _semanticModel.GetTypeInfo(identifier);
            //    if (typeIden.Type.TypeKind == TypeKind.Enum)
            //}
            IdentifierNameSyntax en = identifiers
             .Where(i =>
             {
                 try
                 {
                     return _semanticModel.GetTypeInfo(i).Type?.TypeKind == TypeKind.Enum;
                 }
                 catch (System.Exception ex)
                 {
                     return false;
                 }

             })?.LastOrDefault();
            if (en != null)
            {
                return en.Identifier.ValueText;
            }
            return null;
        }


    }
}
