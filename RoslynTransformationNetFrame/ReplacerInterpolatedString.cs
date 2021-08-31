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
    class ReplacerInterpolatedString
    {
        string ended = null;
        string started = null;


        readonly List<SymbolContainer> _interpolatedSymbols = new List<SymbolContainer>();

        public ReplacerInterpolatedString()
        {
            _interpolatedSymbols.Add(new SymbolContainer(' ', (int count) =>
            new string(Enumerable.Repeat<char>(
                        ' ',
                        count)
                        .ToArray())));
            _interpolatedSymbols.Add(new SymbolContainer('-', (int count) =>
            new string(Enumerable.Repeat<char>(
                        '-',
                        count)
                        .ToArray())));
            _interpolatedSymbols.Add(new SymbolContainer('\n', (int count) =>
                "\\n"));
        }

        public string Trim(string input)
        {
            string returnString = input;
            foreach (var interpolatedSymbol in _interpolatedSymbols)
            {
                if (string.IsNullOrEmpty(ended))
                {
                    string trimmed = returnString.TrimEnd(interpolatedSymbol.Char);
                    int count = returnString.Length - trimmed.Length;

                    if (count > 0)
                    {
                        ended = interpolatedSymbol.GenerateString(count);
                        returnString = trimmed;
                    }
                }

                if (string.IsNullOrEmpty(started))
                {
                    string trimmed = returnString.TrimStart(interpolatedSymbol.Char);
                    int count = returnString.Length - trimmed.Length;
                    if (count > 0)
                    {
                        started = interpolatedSymbol.GenerateString(count);
                        returnString = trimmed;
                    }
                }
            }

            return returnString;
        }

        internal SyntaxList<InterpolatedStringContentSyntax> AddStartTokens(SyntaxList<InterpolatedStringContentSyntax> syntaxList)
        {
            if (!string.IsNullOrEmpty(started))
            {
                var newTextToken = SyntaxFactory.Token(SyntaxTriviaList.Empty,
                    SyntaxKind.InterpolatedStringTextToken,
                    started,
                    "valueText",
                    SyntaxTriviaList.Empty);
                var interpolatedWhiteSpace = SyntaxFactory.InterpolatedStringText(newTextToken);
                return syntaxList.Add(interpolatedWhiteSpace);
            }

            return syntaxList;
        }

        internal SyntaxList<InterpolatedStringContentSyntax> AddEndTokens(SyntaxList<InterpolatedStringContentSyntax> syntaxList)
        {
            if (!string.IsNullOrEmpty(ended))
            {
                //var whiteSpace = SyntaxFactory.ParseTokens(" ").First();
                //var whiteSpace = SyntaxFactory.Token(SyntaxKind.WhitespaceTrivia);
                var newTextToken = SyntaxFactory.Token(SyntaxTriviaList.Empty,
                    SyntaxKind.InterpolatedStringTextToken,
                    ended,
                    "valueText",
                    SyntaxTriviaList.Empty);
                var interpolatedWhiteSpace = SyntaxFactory.InterpolatedStringText(newTextToken);
                return syntaxList.Add(interpolatedWhiteSpace);
            }

            return syntaxList;
        }
    }
}
