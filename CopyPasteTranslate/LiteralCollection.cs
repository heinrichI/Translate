using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyPasteTranslate
{
    public class LiteralCollection
    {
        private LiteralCollection literalCollection;

        public LiteralCollection()
        {

        }

        public LiteralCollection(LiteralCollection literalCollection)
        {
            if (literalCollection.Literals != null)
            {
                foreach (var literal in literalCollection.Literals)
                {
                    Literals.Add(literal);
                }
            }
            if (literalCollection.Paragraphs != null)
            {
                Paragraphs.AddRange(literalCollection.Paragraphs);
            }
        }

        public List<string> Literals { get; internal set; } = new List<string>();

        public List<Paragraph> Paragraphs { get; private set; } = new List<Paragraph>();
    }
}
