using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTransformationNetFrame
{
    public class RoslynReader
    {
        public ReadOnlyCollection<string> GetAllHebrewLiterals(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            SyntaxNode root = tree.GetRoot();

            var reader = new StringLiteralReader();

            reader.Visit(root);

            return new ReadOnlyCollection<string>(reader.Literals);
        }
    }
}
