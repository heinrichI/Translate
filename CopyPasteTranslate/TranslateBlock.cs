using System;
using System.Drawing;
using System.Windows.Forms;

namespace CopyPasteTranslate
{
    public partial class TranslateBlock : Form
    {
        private CopyPasteBlock _sourceBlock;
        public string TargetText { get; set; }

        public TranslateBlock(CopyPasteBlock source, string title, bool autoCopy)
        {
            //UiUtil.PreInitialize(this);
            InitializeComponent();
            //UiUtil.FixFonts(this);

            //labelInfo.Text = LanguageSettings.Current.GoogleTranslate.TranslateBlockInfo;
            //buttonGetTargetGet.Text = LanguageSettings.Current.GoogleTranslate.TranslateBlockGetFromClipboard;
            //buttonCopySourceTextToClipboard.Text = LanguageSettings.Current.GoogleTranslate.TranslateBlockCopySourceText;

            _sourceBlock = source;
            Text = title;
            if (autoCopy)
            {
                buttonCopySourceTextToClipboard_Click(null, null);
                buttonCopySourceTextToClipboard.Font = new Font(Font.FontFamily.Name, buttonCopySourceTextToClipboard.Font.Size, FontStyle.Regular);
            }
            else
            {
                buttonCopySourceTextToClipboard.Font = new Font(Font.FontFamily.Name, buttonCopySourceTextToClipboard.Font.Size, FontStyle.Bold);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonGetTargetGet_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                var x = Clipboard.GetData(DataFormats.UnicodeText);
                var text = x.ToString();
                if (text.Trim() == _sourceBlock.TargetText.Trim())
                {
                    MessageBox.Show("Clipboard contains source text!" + Environment.NewLine +
                        Environment.NewLine +
                        "Go to translator and translate, the copy result to clipboard and click this button again.");
                    return;
                }
                TargetText = text;
            }
            DialogResult = DialogResult.OK;
        }

        private void buttonCopySourceTextToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_sourceBlock.TargetText);
            buttonCopySourceTextToClipboard.Font = new Font(Font.FontFamily.Name, buttonCopySourceTextToClipboard.Font.Size, FontStyle.Regular);
        }

        private void TranslateBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                buttonGetTargetGet_Click(sender, e);
            }
        }
    }
}
