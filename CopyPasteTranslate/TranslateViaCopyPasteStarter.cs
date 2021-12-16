using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyPasteTranslate
{
    public class TranslateViaCopyPasteStarter
    {
        public static void TranslateViaCopyPaste(ReadOnlyCollection<string> literals)
        {
            LiteralCollection  lc = new LiteralCollection();
            lc.Paragraphs.Add(new Paragraph());
            lc.Literals.AddRange(literals);
            using (var form = new TranslateViaCopyPaste(lc))
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                //var isOriginalVisible = SubtitleListview1.IsOriginalTextColumnVisible;
                //SaveSubtitleListviewIndices();
                //MakeHistoryForUndo(_language.BeforeGoogleTranslation);
                //ShowSubtitleTimer.Stop();
                //var oldHash = _changeSubtitleHash;
                //_subtitleOriginal = new Subtitle(_subtitle);
                //_subtitleOriginalFileName = _fileName;
                //_fileName = null;
                //_subtitle.Paragraphs.Clear();
                //foreach (var p in form.TranslatedSubtitle.Paragraphs)
                //{
                //    _subtitle.Paragraphs.Add(new Paragraph(p));
                //}

                //ShowStatus(_language.SubtitleTranslated);
                //_changeOriginalSubtitleHash = oldHash;
                //_changeSubtitleHash = -1;
                //ShowSubtitleTimer.Start();
                //ShowSource();
                //SubtitleListview1.ShowOriginalTextColumn(_languageGeneral.OriginalText);
                //SubtitleListview1.AutoSizeAllColumns(this);
                //SetupOriginalEdit();
                //_changeOriginalSubtitleHash = oldHash;
                //SubtitleListview1.Fill(_subtitle, _subtitleOriginal);
                //ResetHistory();
                //_fileName = null;
                //RestoreSubtitleListviewIndices();
                //_converted = true;
                //SetTitle();
                //SetEncoding(Encoding.UTF8);
                //if (!isOriginalVisible)
                //{
                //    toolStripMenuItemShowOriginalInPreview.Checked = false;
                //    Configuration.Settings.General.ShowOriginalAsPreviewIfAvailable = false;
                //    audioVisualizer.Invalidate();
                //}
            }
        }
    }
}
