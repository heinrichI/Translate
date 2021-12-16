using System;

namespace CopyPasteTranslate
{
    public class Paragraph : ITranslationBaseUnit
    {
        public int Number { get; set; }

        public string Text { get; set; }

        public bool Forced { get; set; }

        public string Extra { get; set; }

        public bool IsComment { get; set; }

        public string Actor { get; set; }
        public string Region { get; set; }

        public string MarginL { get; set; }
        public string MarginR { get; set; }
        public string MarginV { get; set; }

        public string Effect { get; set; }

        public int Layer { get; set; }

        public string Id { get; }

        public string Language { get; set; }

        public string Style { get; set; }

        public bool NewSection { get; set; }

        public string Bookmark { get; set; }

        private static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public Paragraph() : this(string.Empty)
        {
        }

        public Paragraph(string text)
        {
            Text = text;
            Id = GenerateId();
        }

        public Paragraph(Paragraph paragraph, bool generateNewId = true)
        {
            Number = paragraph.Number;
            Text = paragraph.Text;
            Forced = paragraph.Forced;
            Extra = paragraph.Extra;
            IsComment = paragraph.IsComment;
            Actor = paragraph.Actor;
            Region = paragraph.Region;
            MarginL = paragraph.MarginL;
            MarginR = paragraph.MarginR;
            MarginV = paragraph.MarginV;
            Effect = paragraph.Effect;
            Layer = paragraph.Layer;
            Id = generateNewId ? GenerateId() : paragraph.Id;
            Language = paragraph.Language;
            Style = paragraph.Style;
            NewSection = paragraph.NewSection;
            Bookmark = paragraph.Bookmark;
        }

        public override string ToString()
        {
            return $"{Text}";
        }

        public int NumberOfLines => Utilities.GetNumberOfLines(Text);
    }
}
