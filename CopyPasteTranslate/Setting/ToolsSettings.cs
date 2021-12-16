using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyPasteTranslate.Setting
{
    public class ToolsSettings
    {
        //public List<AssaTemplateItem> AssaTagTemplates { get; set; }
        public int StartSceneIndex { get; set; }
        public int EndSceneIndex { get; set; }
        public int VerifyPlaySeconds { get; set; }
        public bool FixShortDisplayTimesAllowMoveStartTime { get; set; }
        public bool RemoveEmptyLinesBetweenText { get; set; }
        public string MusicSymbol { get; set; }
        public string MusicSymbolReplace { get; set; }
        public string UnicodeSymbolsToInsert { get; set; }
        public bool SpellCheckAutoChangeNameCasing { get; set; }
        public bool SpellCheckAutoChangeNamesUseSuggestions { get; set; }
        public bool CheckOneLetterWords { get; set; }
        public bool SpellCheckEnglishAllowInQuoteAsIng { get; set; }
        public bool RememberUseAlwaysList { get; set; }
        public bool LiveSpellCheck { get; set; }
        public bool SpellCheckShowCompletedMessage { get; set; }
        public bool OcrFixUseHardcodedRules { get; set; }
        public int OcrBinaryImageCompareRgbThreshold { get; set; }
        public int OcrTesseract4RgbThreshold { get; set; }
        public string OcrAddLetterRow1 { get; set; }
        public string OcrAddLetterRow2 { get; set; }
        public string OcrTrainFonts { get; set; }
        public string OcrTrainMergedLetters { get; set; }
        public string OcrTrainSrtFile { get; set; }
        public bool OcrUseWordSplitList { get; set; }
        public string BDOpenIn { get; set; }
        public string Interjections { get; set; }
        public string MicrosoftBingApiId { get; set; }
        public string MicrosoftTranslatorApiKey { get; set; }
        public string MicrosoftTranslatorTokenEndpoint { get; set; }
        public string MicrosoftTranslatorCategory { get; set; }
        public string GoogleApiV2Key { get; set; }
        public bool GoogleTranslateNoKeyWarningShow { get; set; }
        public int GoogleApiV1ChunkSize { get; set; }
        public string GoogleTranslateLastTargetLanguage { get; set; }
        public bool TranslateAllowSplit { get; set; }
        public string TranslateLastService { get; set; }
        public string TranslateMergeStrategy { get; set; }
        public string TranslateViaCopyPasteSeparator { get; set; }
        public int TranslateViaCopyPasteMaxSize { get; set; }
        public bool TranslateViaCopyPasteAutoCopyToClipboard { get; set; }
        public bool ListViewSyntaxColorDurationSmall { get; set; }
        public bool ListViewSyntaxColorDurationBig { get; set; }
        public bool ListViewSyntaxColorOverlap { get; set; }
        public bool ListViewSyntaxColorLongLines { get; set; }
        public bool ListViewSyntaxColorWideLines { get; set; }
        public bool ListViewSyntaxColorGap { get; set; }
        public bool ListViewSyntaxMoreThanXLines { get; set; }
        public Color ListViewSyntaxErrorColor { get; set; }
        public Color ListViewUnfocusedSelectedColor { get; set; }
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public Color Color3 { get; set; }
        public Color Color4 { get; set; }
        public bool ListViewShowColumnStartTime { get; set; }
        public bool ListViewShowColumnEndTime { get; set; }
        public bool ListViewShowColumnDuration { get; set; }
        public bool ListViewShowColumnCharsPerSec { get; set; }
        public bool ListViewShowColumnWordsPerMin { get; set; }
        public bool ListViewShowColumnGap { get; set; }
        public bool ListViewShowColumnActor { get; set; }
        public bool ListViewShowColumnRegion { get; set; }
        public bool ListViewMultipleReplaceShowColumnRuleInfo { get; set; }
        public bool SplitAdvanced { get; set; }
        public string SplitOutputFolder { get; set; }
        public int SplitNumberOfParts { get; set; }
        public string SplitVia { get; set; }
        public bool JoinCorrectTimeCodes { get; set; }
        public int JoinAddMs { get; set; }
        public string LastShowEarlierOrLaterSelection { get; set; }
        public string NewEmptyTranslationText { get; set; }
        public string BatchConvertOutputFolder { get; set; }
        public bool BatchConvertOverwriteExisting { get; set; }
        public bool BatchConvertSaveInSourceFolder { get; set; }
        public bool BatchConvertRemoveFormatting { get; set; }
        public bool BatchConvertRemoveStyle { get; set; }
        public bool BatchConvertBridgeGaps { get; set; }
        public bool BatchConvertFixCasing { get; set; }
        public bool BatchConvertRemoveTextForHI { get; set; }
        public bool BatchConvertFixCommonErrors { get; set; }
        public bool BatchConvertMultipleReplace { get; set; }
        public bool BatchConvertFixRtl { get; set; }
        public string BatchConvertFixRtlMode { get; set; }
        public bool BatchConvertSplitLongLines { get; set; }
        public bool BatchConvertAutoBalance { get; set; }
        public bool BatchConvertSetMinDisplayTimeBetweenSubtitles { get; set; }
        public bool BatchConvertMergeShortLines { get; set; }
        public bool BatchConvertRemoveLineBreaks { get; set; }
        public bool BatchConvertMergeSameText { get; set; }
        public bool BatchConvertMergeSameTimeCodes { get; set; }
        public bool BatchConvertChangeFrameRate { get; set; }
        public bool BatchConvertChangeSpeed { get; set; }
        public bool BatchConvertAdjustDisplayDuration { get; set; }
        public bool BatchConvertApplyDurationLimits { get; set; }
        public bool BatchConvertDeleteLines { get; set; }
        public bool BatchConvertAssaChangeRes { get; set; }
        public bool BatchConvertOffsetTimeCodes { get; set; }
        public string BatchConvertLanguage { get; set; }
        public string BatchConvertFormat { get; set; }
        public string BatchConvertAssStyles { get; set; }
        public string BatchConvertSsaStyles { get; set; }
        public bool BatchConvertUseStyleFromSource { get; set; }
        public string BatchConvertExportCustomTextTemplate { get; set; }
        public bool BatchConvertTsOverrideXPosition { get; set; }
        public bool BatchConvertTsOverrideYPosition { get; set; }
        public int BatchConvertTsOverrideBottomMargin { get; set; }
        public string BatchConvertTsOverrideHAlign { get; set; }
        public int BatchConvertTsOverrideHMargin { get; set; }
        public bool BatchConvertTsOverrideScreenSize { get; set; }
        public int BatchConvertTsScreenWidth { get; set; }
        public int BatchConvertTsScreenHeight { get; set; }
        public string BatchConvertTsFileNameAppend { get; set; }
        public bool BatchConvertTsOnlyTeletext { get; set; }
        public string BatchConvertMkvLanguageCodeStyle { get; set; }
        public string WaveformBatchLastFolder { get; set; }
        public string ModifySelectionText { get; set; }
        public string ModifySelectionRule { get; set; }
        public bool ModifySelectionCaseSensitive { get; set; }
        public string ExportVobSubFontName { get; set; }
        public int ExportVobSubFontSize { get; set; }
        public string ExportVobSubVideoResolution { get; set; }
        public string ExportVobSubLanguage { get; set; }
        public bool ExportVobSubSimpleRendering { get; set; }
        public bool ExportVobAntiAliasingWithTransparency { get; set; }
        public string ExportBluRayFontName { get; set; }
        public int ExportBluRayFontSize { get; set; }
        public string ExportFcpFontName { get; set; }
        public string ExportFontNameOther { get; set; }
        public int ExportFcpFontSize { get; set; }
        public string ExportFcpImageType { get; set; }
        public string ExportFcpPalNtsc { get; set; }
        public string ExportBdnXmlImageType { get; set; }
        public int ExportLastFontSize { get; set; }
        public int ExportLastLineHeight { get; set; }
        public int ExportLastBorderWidth { get; set; }
        public bool ExportLastFontBold { get; set; }
        public string ExportBluRayVideoResolution { get; set; }
        public string ExportFcpVideoResolution { get; set; }
        public Color ExportFontColor { get; set; }
        public Color ExportBorderColor { get; set; }
        public Color ExportShadowColor { get; set; }
        public int ExportBoxBorderSize { get; set; }
        public string ExportBottomMarginUnit { get; set; }
        public int ExportBottomMarginPercent { get; set; }
        public int ExportBottomMarginPixels { get; set; }
        public string ExportLeftRightMarginUnit { get; set; }
        public int ExportLeftRightMarginPercent { get; set; }
        public int ExportLeftRightMarginPixels { get; set; }
        public int ExportHorizontalAlignment { get; set; }
        public int ExportBluRayBottomMarginPercent { get; set; }
        public int ExportBluRayBottomMarginPixels { get; set; }
        public int ExportBluRayShadow { get; set; }
        public bool ExportBluRayRemoveSmallGaps { get; set; }
        public string ExportCdgBackgroundImage { get; set; }
        public int ExportCdgMarginLeft { get; set; }
        public int ExportCdgMarginBottom { get; set; }
        public string ExportCdgFormat { get; set; }
        public int Export3DType { get; set; }
        public int Export3DDepth { get; set; }
        public int ExportLastShadowTransparency { get; set; }
        public double ExportLastFrameRate { get; set; }
        public bool ExportFullFrame { get; set; }
        public bool ExportFcpFullPathUrl { get; set; }
        public string ExportPenLineJoin { get; set; }
        public Color BinEditBackgroundColor { get; set; }
        public Color BinEditImageBackgroundColor { get; set; }
        public int BinEditVerticalMargin { get; set; }
        public int BinEditLeftMargin { get; set; }
        public int BinEditRightMargin { get; set; }
        public bool FixCommonErrorsFixOverlapAllowEqualEndStart { get; set; }
        public bool FixCommonErrorsSkipStepOne { get; set; }
        public string ImportTextSplitting { get; set; }
        public string ImportTextLineBreak { get; set; }
        public bool ImportTextMergeShortLines { get; set; }
        public bool ImportTextRemoveEmptyLines { get; set; }
        public bool ImportTextAutoSplitAtBlank { get; set; }
        public bool ImportTextRemoveLinesNoLetters { get; set; }
        public bool ImportTextGenerateTimeCodes { get; set; }
        public bool ImportTextTakeTimeCodeFromFileName { get; set; }
        public bool ImportTextAutoBreak { get; set; }
        public bool ImportTextAutoBreakAtEnd { get; set; }
        public decimal ImportTextGap { get; set; }
        public decimal ImportTextAutoSplitNumberOfLines { get; set; }
        public string ImportTextAutoBreakAtEndMarkerText { get; set; }
        public bool ImportTextDurationAuto { get; set; }
        public decimal ImportTextFixedDuration { get; set; }
        public string GenerateTimeCodePatterns { get; set; }
        public string MusicSymbolStyle { get; set; }
        public int BridgeGapMilliseconds { get; set; }
        public string ExportCustomTemplates { get; set; }
        public string ChangeCasingChoice { get; set; }
        public bool UseNoLineBreakAfter { get; set; }
        public string NoLineBreakAfterEnglish { get; set; }
        public List<string> FindHistory { get; set; }
        public string ExportTextFormatText { get; set; }
        public bool ExportTextRemoveStyling { get; set; }
        public bool ExportTextShowLineNumbers { get; set; }
        public bool ExportTextShowLineNumbersNewLine { get; set; }
        public bool ExportTextShowTimeCodes { get; set; }
        public bool ExportTextShowTimeCodesNewLine { get; set; }
        public bool ExportTextNewLineAfterText { get; set; }
        public bool ExportTextNewLineBetweenSubtitles { get; set; }
        public string ExportTextTimeCodeFormat { get; set; }
        public string ExportTextTimeCodeSeparator { get; set; }
        public bool VideoOffsetKeepTimeCodes { get; set; }
        public int MoveStartEndMs { get; set; }
        public decimal AdjustDurationSeconds { get; set; }
        public int AdjustDurationPercent { get; set; }
        public string AdjustDurationLast { get; set; }
        public bool AdjustDurationExtendOnly { get; set; }
        public bool AutoBreakCommaBreakEarly { get; set; }
        public bool AutoBreakDashEarly { get; set; }
        public bool AutoBreakLineEndingEarly { get; set; }
        public bool AutoBreakUsePixelWidth { get; set; }
        public bool AutoBreakPreferBottomHeavy { get; set; }
        public double AutoBreakPreferBottomPercent { get; set; }
        public bool ApplyMinimumDurationLimit { get; set; }
        public bool ApplyMaximumDurationLimit { get; set; }
        public int MergeShortLinesMaxGap { get; set; }
        public int MergeShortLinesMaxChars { get; set; }
        public bool MergeShortLinesOnlyContinuous { get; set; }
        public string ColumnPasteColumn { get; set; }
        public string ColumnPasteOverwriteMode { get; set; }
        public string AssaAttachmentFontTextPreview { get; set; }
        public string AssaSetPositionTarget { get; set; }
        public string VisualSyncStartSize { get; set; }
        public Color BlankVideoColor { get; set; }
        public bool BlankVideoUseCheckeredImage { get; set; }
        public int BlankVideoMinutes { get; set; }
        public decimal BlankVideoFrameRate { get; set; }
        public Color AssaProgressBarForeColor { get; set; }
        public Color AssaProgressBarBackColor { get; set; }
        public Color AssaProgressBarTextColor { get; set; }
        public int AssaProgressBarHeight { get; set; }
        public int AssaProgressBarSplitterWidth { get; set; }
        public int AssaProgressBarSplitterHeight { get; set; }
        public string AssaProgressBarFontName { get; set; }
        public int AssaProgressBarFontSize { get; set; }
        public bool AssaProgressBarTopAlign { get; set; }
        public string AssaProgressBarTextAlign { get; set; }
        public string GenVideoEncoding { get; set; }
        public string GenVideoPreset { get; set; }
        public string GenVideoCrf { get; set; }
        public string GenVideoTune { get; set; }
        public string GenVideoAudioEncoding { get; set; }
        public bool GenVideoAudioForceStereo { get; set; }
        public string GenVideoAudioSampleRate { get; set; }
        public bool GenVideoTargetFileSize { get; set; }
        public float GenVideoFontSizePercentOfHeight { get; set; }
        public bool GenVideoNonAssaBox { get; set; }
        public bool GenVideoNonAssaAlignRight { get; set; }
        public bool GenVideoNonAssaFixRtlUnicode { get; set; }

        public ToolsSettings()
        {
            //AssaTagTemplates = new List<AssaTemplateItem>();
            StartSceneIndex = 1;
            EndSceneIndex = 1;
            VerifyPlaySeconds = 2;
            FixShortDisplayTimesAllowMoveStartTime = false;
            RemoveEmptyLinesBetweenText = true;
            MusicSymbol = "♪";
            MusicSymbolReplace = "â™ª,â™," + // ♪ + ♫ in UTF-8 opened as ANSI
                                 "<s M/>,<s m/>," + // music symbols by subtitle creator
                                 "#,*,¶"; // common music symbols
            UnicodeSymbolsToInsert = "♪;♫;°;☺;☹;♥;©;☮;☯;Σ;∞;≡;⇒;π";
            SpellCheckAutoChangeNameCasing = false;
            SpellCheckAutoChangeNamesUseSuggestions = false;
            OcrFixUseHardcodedRules = true;
            OcrBinaryImageCompareRgbThreshold = 200;
            OcrTesseract4RgbThreshold = 200;
            OcrAddLetterRow1 = "♪;á;é;í;ó;ö;ő;ú;ü;ű;ç;ñ;å;¿";
            OcrAddLetterRow2 = "♫;Á;É;Í;Ó;Ö;Ő;Ú;Ü;Ű;Ç;Ñ;Å;¡";
            OcrTrainFonts = "Arial;Calibri;Corbel;Futura Std Book;Futura Bis;Helvetica Neue;Lucida Console;Tahoma;Trebuchet MS;Verdana";
            OcrTrainMergedLetters = "ff ft fi fj fy fl rf rt rv rw ry rt rz ryt tt TV tw yt yw wy wf ryt xy";
            OcrUseWordSplitList = true;
            Interjections = "Ah;Ahem;Ahh;Ahhh;Ahhhh;Eh;Ehh;Ehhh;Hm;Hmm;Hmmm;Huh;Mm;Mmm;Mmmm;Phew;Gah;Oh;Ohh;Ohhh;Ow;Oww;Owww;Ugh;Ughh;Uh;Uhh;Uhhh;Whew";
            MicrosoftTranslatorTokenEndpoint = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
            GoogleTranslateNoKeyWarningShow = true;
            GoogleApiV1ChunkSize = 1500;
            GoogleTranslateLastTargetLanguage = "en";
            TranslateAllowSplit = true;
            TranslateViaCopyPasteAutoCopyToClipboard = true;
            TranslateViaCopyPasteMaxSize = 5000;
            TranslateViaCopyPasteSeparator = ".";
            CheckOneLetterWords = true;
            SpellCheckEnglishAllowInQuoteAsIng = false;
            SpellCheckShowCompletedMessage = true;
            ListViewSyntaxColorDurationSmall = true;
            ListViewSyntaxColorDurationBig = true;
            ListViewSyntaxColorOverlap = true;
            ListViewSyntaxColorLongLines = true;
            ListViewSyntaxColorWideLines = false;
            ListViewSyntaxMoreThanXLines = true;
            ListViewSyntaxColorGap = true;
            ListViewSyntaxErrorColor = Color.FromArgb(255, 180, 150);
            ListViewUnfocusedSelectedColor = Color.LightBlue;
            Color1 = Color.Yellow;
            Color2 = Color.FromArgb(byte.MaxValue, 0, 0);
            Color3 = Color.FromArgb(0, byte.MaxValue, 0);
            Color4 = Color.Cyan;
            ListViewShowColumnStartTime = true;
            ListViewShowColumnEndTime = true;
            ListViewShowColumnDuration = true;
            SplitAdvanced = false;
            SplitNumberOfParts = 3;
            SplitVia = "Lines";
            JoinCorrectTimeCodes = true;
            NewEmptyTranslationText = string.Empty;
            BatchConvertLanguage = string.Empty;
            BatchConvertTsOverrideBottomMargin = 5; // pct
            BatchConvertTsScreenWidth = 1920;
            BatchConvertTsScreenHeight = 1080;
            BatchConvertTsOverrideHAlign = "center"; // left center right
            BatchConvertTsOverrideHMargin = 5; // pct
            BatchConvertTsFileNameAppend = ".{two-letter-country-code}";
            ModifySelectionRule = "Contains";
            ModifySelectionText = string.Empty;
            ImportTextDurationAuto = true;
            ImportTextGap = 84;
            ImportTextFixedDuration = 2500;
            GenerateTimeCodePatterns = "HH:mm:ss;yyyy-MM-dd;dddd dd MMMM yyyy <br>HH:mm:ss;dddd dd MMMM yyyy <br>hh:mm:ss tt;s";
            MusicSymbolStyle = "Double"; // 'Double' or 'Single'
            ExportFontColor = Color.White;
            ExportBorderColor = Color.FromArgb(255, 0, 0, 0);
            ExportShadowColor = Color.FromArgb(255, 0, 0, 0);
            ExportBoxBorderSize = 8;
            ExportBottomMarginUnit = "%";
            ExportBottomMarginPercent = 5;
            ExportBottomMarginPixels = 15;
            ExportLeftRightMarginUnit = "%";
            ExportLeftRightMarginPercent = 5;
            ExportLeftRightMarginPixels = 15;
            ExportHorizontalAlignment = 1; // 1=center (0=left, 2=right)
            ExportVobSubSimpleRendering = false;
            ExportVobAntiAliasingWithTransparency = true;
            ExportBluRayBottomMarginPercent = 5;
            ExportBluRayBottomMarginPixels = 20;
            ExportBluRayShadow = 1;
            Export3DType = 0;
            Export3DDepth = 0;
            ExportCdgMarginLeft = 160;
            ExportCdgMarginBottom = 67;
            ExportLastShadowTransparency = 200;
            ExportLastFrameRate = 24.0d;
            ExportFullFrame = false;
            ExportPenLineJoin = "Round";
            ExportFcpImageType = "Bmp";
            ExportFcpPalNtsc = "PAL";
            ExportLastBorderWidth = 4;
            BinEditBackgroundColor = Color.Black;
            BinEditImageBackgroundColor = Color.Blue;
            BinEditVerticalMargin = 10;
            BinEditLeftMargin = 10;
            BinEditRightMargin = 10;
            BridgeGapMilliseconds = 100;
            ExportCustomTemplates = "SubRipÆÆ{number}\r\n{start} --> {end}\r\n{text}\r\n\r\nÆhh:mm:ss,zzzÆ[Do not modify]ÆæMicroDVDÆÆ{{start}}{{end}}{text}\r\nÆffÆ||Æ";
            UseNoLineBreakAfter = false;
            NoLineBreakAfterEnglish = " Mrs.; Ms.; Mr.; Dr.; a; an; the; my; my own; your; his; our; their; it's; is; are;'s; 're; would;'ll;'ve;'d; will; that; which; who; whom; whose; whichever; whoever; wherever; each; either; every; all; both; few; many; sevaral; all; any; most; been; been doing; none; some; my own; your own; his own; her own; our own; their own; I; she; he; as per; as regards; into; onto; than; where as; abaft; aboard; about; above; across; afore; after; against; along; alongside; amid; amidst; among; amongst; anenst; apropos; apud; around; as; aside; astride; at; athwart; atop; barring; before; behind; below; beneath; beside; besides; between; betwixt; beyond; but; by; circa; ca; concerning; despite; down; during; except; excluding; following; for; forenenst; from; given; in; including; inside; into; lest; like; minus; modulo; near; next; of; off; on; onto; opposite; out; outside; over; pace; past; per; plus; pro; qua; regarding; round; sans; save; since; than; through; thru; throughout; thruout; till; to; toward; towards; under; underneath; unlike; until; unto; up; upon; versus; vs; via; vice; with; within; without; considering; respecting; one; two; another; three; our; five; six; seven; eight; nine; ten; eleven; twelve; thirteen; fourteen; fifteen; sixteen; seventeen; eighteen; nineteen; twenty; thirty; forty; fifty; sixty; seventy; eighty; ninety; hundred; thousand; million; billion; trillion; while; however; what; zero; little; enough; after; although; and; as; if; though; although; because; before; both; but; even; how; than; nor; or; only; unless; until; yet; was; were";
            FindHistory = new List<string>();
            ExportTextFormatText = "None";
            ExportTextRemoveStyling = true;
            ExportTextShowLineNumbersNewLine = true;
            ExportTextShowTimeCodesNewLine = true;
            ExportTextNewLineAfterText = true;
            ExportTextNewLineBetweenSubtitles = true;
            ImportTextLineBreak = "|";
            ImportTextAutoSplitNumberOfLines = 2;
            ImportTextAutoSplitAtBlank = true;
            ImportTextAutoBreakAtEndMarkerText = ".!?";
            ImportTextAutoBreakAtEnd = true;
            MoveStartEndMs = 100;
            AdjustDurationSeconds = 0.1m;
            AdjustDurationPercent = 120;
            AdjustDurationExtendOnly = true;
            AutoBreakCommaBreakEarly = false;
            AutoBreakDashEarly = true;
            AutoBreakLineEndingEarly = false;
            AutoBreakUsePixelWidth = true;
            AutoBreakPreferBottomHeavy = true;
            AutoBreakPreferBottomPercent = 5;
            ApplyMinimumDurationLimit = true;
            ApplyMaximumDurationLimit = true;
            MergeShortLinesMaxGap = 250;
            MergeShortLinesMaxChars = 50;
            MergeShortLinesOnlyContinuous = true;
            ColumnPasteColumn = "all";
            ColumnPasteOverwriteMode = "overwrite";
            AssaAttachmentFontTextPreview =
                "Hello World!" + Environment.NewLine +
                "こんにちは世界" + Environment.NewLine +
                "你好世界！" + Environment.NewLine +
                "1234567890";
            BlankVideoColor = Color.CadetBlue;
            BlankVideoUseCheckeredImage = true;
            BlankVideoMinutes = 2;
            BlankVideoFrameRate = 23.976m;
            AssaProgressBarForeColor = Color.FromArgb(200, 200, 0, 0);
            AssaProgressBarBackColor = Color.FromArgb(150, 80, 80, 80);
            AssaProgressBarTextColor = Color.White;
            AssaProgressBarHeight = 40;
            AssaProgressBarSplitterWidth = 2;
            AssaProgressBarSplitterHeight = 40;
            AssaProgressBarFontName = "Arial";
            AssaProgressBarFontSize = 30;
            AssaProgressBarTextAlign = "left";

            GenVideoEncoding = "libx264";
            GenVideoPreset = "medium";
            GenVideoCrf = "23";
            GenVideoTune = "film";
            GenVideoAudioEncoding = "copy";
            GenVideoAudioForceStereo = true;
            GenVideoAudioSampleRate = "48000";
            GenVideoFontSizePercentOfHeight = 0.078f;
            GenVideoNonAssaBox = true;
        }
    }
}
