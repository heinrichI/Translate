using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyPasteTranslate.Setting
{
    public class GeneralSettings
    {
        //public List<RulesProfile> Profiles { get; set; }
        public string CurrentProfile { get; set; }
        public bool ShowToolbarNew { get; set; }
        public bool ShowToolbarOpen { get; set; }
        public bool ShowToolbarSave { get; set; }
        public bool ShowToolbarSaveAs { get; set; }
        public bool ShowToolbarFind { get; set; }
        public bool ShowToolbarReplace { get; set; }
        public bool ShowToolbarFixCommonErrors { get; set; }
        public bool ShowToolbarRemoveTextForHi { get; set; }
        public bool ShowToolbarVisualSync { get; set; }
        public bool ShowToolbarSpellCheck { get; set; }
        public bool ShowToolbarNetflixGlyphCheck { get; set; }
        public bool ShowToolbarSettings { get; set; }
        public bool ShowToolbarHelp { get; set; }

        public bool ShowVideoPlayer { get; set; }
        public bool ShowAudioVisualizer { get; set; }
        public bool ShowWaveform { get; set; }
        public bool ShowSpectrogram { get; set; }
        public bool ShowFrameRate { get; set; }
        public bool ShowVideoControls { get; set; }
        public bool TextAndOrigianlTextBoxesSwitched { get; set; }
        public double DefaultFrameRate { get; set; }
        public double CurrentFrameRate { get; set; }
        public string DefaultSubtitleFormat { get; set; }
        public string DefaultSaveAsFormat { get; set; }
        public string FavoriteSubtitleFormats { get; set; }
        public string DefaultEncoding { get; set; }
        public bool AutoConvertToUtf8 { get; set; }
        public bool AutoGuessAnsiEncoding { get; set; }
        public string SystemSubtitleFontNameOverride { get; set; }
        public int SystemSubtitleFontSizeOverride { get; set; }

        public string SubtitleFontName { get; set; }
        public int SubtitleTextBoxFontSize { get; set; }
        public bool SubtitleTextBoxSyntaxColor { get; set; }
        public Color SubtitleTextBoxHtmlColor { get; set; }
        public Color SubtitleTextBoxAssColor { get; set; }
        public int SubtitleListViewFontSize { get; set; }
        public bool SubtitleTextBoxFontBold { get; set; }
        public bool SubtitleListViewFontBold { get; set; }
        public Color SubtitleFontColor { get; set; }
        public Color SubtitleBackgroundColor { get; set; }
        public string MeasureFontName { get; set; }
        public int MeasureFontSize { get; set; }
        public bool MeasureFontBold { get; set; }
        public int SubtitleLineMaximumPixelWidth { get; set; }
        public bool CenterSubtitleInTextBox { get; set; }
        public bool ShowRecentFiles { get; set; }
        public bool RememberSelectedLine { get; set; }
        public bool StartLoadLastFile { get; set; }
        public bool StartRememberPositionAndSize { get; set; }
        public string StartPosition { get; set; }
        public string StartSize { get; set; }
        public int SplitContainerMainSplitterDistance { get; set; }
        public int SplitContainer1SplitterDistance { get; set; }
        public int SplitContainerListViewAndTextSplitterDistance { get; set; }
        public bool StartInSourceView { get; set; }
        public bool RemoveBlankLinesWhenOpening { get; set; }
        public bool RemoveBadCharsWhenOpening { get; set; }
        public int SubtitleLineMaximumLength { get; set; }
        public int MaxNumberOfLines { get; set; }
        public int MaxNumberOfLinesPlusAbort { get; set; }
        public int MergeLinesShorterThan { get; set; }
        public int SubtitleMinimumDisplayMilliseconds { get; set; }
        public int SubtitleMaximumDisplayMilliseconds { get; set; }
        public int MinimumMillisecondsBetweenLines { get; set; }
        public int SetStartEndHumanDelay { get; set; }
        public bool AutoWrapLineWhileTyping { get; set; }
        public double SubtitleMaximumCharactersPerSeconds { get; set; }
        public double SubtitleOptimalCharactersPerSeconds { get; set; }
        public bool CharactersPerSecondsIgnoreWhiteSpace { get; set; }
        public bool IgnoreArabicDiacritics { get; set; }
        public double SubtitleMaximumWordsPerMinute { get; set; }

        public DialogType DialogStyle { get; set; }
        //public ContinuationStyle ContinuationStyle { get; set; }
        public int ContinuationPause { get; set; }
        public bool FixContinuationStyleUncheckInsertsAllCaps { get; set; }
        public bool FixContinuationStyleUncheckInsertsItalic { get; set; }
        public bool FixContinuationStyleUncheckInsertsLowercase { get; set; }
        public bool FixContinuationStyleHideContinuationCandidatesWithoutName { get; set; }
        public bool FixContinuationStyleIgnoreLyrics { get; set; }
        public string SpellCheckLanguage { get; set; }
        public string VideoPlayer { get; set; }
        public int VideoPlayerDefaultVolume { get; set; }
        public string VideoPlayerPreviewFontName { get; set; }
        public int VideoPlayerPreviewFontSize { get; set; }
        public bool VideoPlayerPreviewFontBold { get; set; }
        public bool VideoPlayerShowStopButton { get; set; }
        public bool VideoPlayerShowFullscreenButton { get; set; }
        public bool VideoPlayerShowMuteButton { get; set; }
        public string Language { get; set; }
        public string ListViewLineSeparatorString { get; set; }
        public int ListViewDoubleClickAction { get; set; }
        public string SaveAsUseFileNameFrom { get; set; }
        public string UppercaseLetters { get; set; }
        public int DefaultAdjustMilliseconds { get; set; }
        public bool AutoRepeatOn { get; set; }
        public int AutoRepeatCount { get; set; }
        public bool AutoContinueOn { get; set; }
        public int AutoContinueDelay { get; set; }
        public bool ReturnToStartAfterRepeat { get; set; }
        public bool SyncListViewWithVideoWhilePlaying { get; set; }
        public int AutoBackupSeconds { get; set; }
        public int AutoBackupDeleteAfterMonths { get; set; }
        public string SpellChecker { get; set; }
        public bool AllowEditOfOriginalSubtitle { get; set; }
        public bool PromptDeleteLines { get; set; }
        public bool Undocked { get; set; }
        public string UndockedVideoPosition { get; set; }
        public bool UndockedVideoFullscreen { get; set; }
        public string UndockedWaveformPosition { get; set; }
        public string UndockedVideoControlsPosition { get; set; }
        public bool WaveformCenter { get; set; }
        public bool WaveformAutoGenWhenOpeningVideo { get; set; }
        public int WaveformUpdateIntervalMs { get; set; }
        public int SmallDelayMilliseconds { get; set; }
        public int LargeDelayMilliseconds { get; set; }
        public bool ShowOriginalAsPreviewIfAvailable { get; set; }
        public int LastPacCodePage { get; set; }
        public string OpenSubtitleExtraExtensions { get; set; }
        public bool ListViewColumnsRememberSize { get; set; }
        public int ListViewNumberWidth { get; set; }
        public int ListViewStartWidth { get; set; }
        public int ListViewEndWidth { get; set; }
        public int ListViewDurationWidth { get; set; }
        public int ListViewCpsWidth { get; set; }
        public int ListViewWpmWidth { get; set; }
        public int ListViewGapWidth { get; set; }
        public int ListViewActorWidth { get; set; }
        public int ListViewRegionWidth { get; set; }
        public int ListViewTextWidth { get; set; }
        public bool DirectShowDoubleLoad { get; set; }
        public string VlcWaveTranscodeSettings { get; set; }
        public string VlcLocation { get; set; }
        public string VlcLocationRelative { get; set; }
        public string MpvVideoOutputWindows { get; set; }
        public string MpvVideoOutputLinux { get; set; }
        public string MpvExtraOptions { get; set; }
        public bool MpvLogging { get; set; }
        public bool MpvHandlesPreviewText { get; set; }
        public Color MpvPreviewTextPrimaryColor { get; set; }
        public decimal MpvPreviewTextOutlineWidth { get; set; }
        public decimal MpvPreviewTextShadowWidth { get; set; }
        public bool MpvPreviewTextOpaqueBox { get; set; }
        public string MpvPreviewTextAlignment { get; set; }
        public int MpvPreviewTextMarginVertical { get; set; }
        public string MpcHcLocation { get; set; }
        public string MkvMergeLocation { get; set; }
        public bool UseFFmpegForWaveExtraction { get; set; }
        public string FFmpegLocation { get; set; }
        public string FFmpegSceneThreshold { get; set; }
        public bool UseTimeFormatHHMMSSFF { get; set; }
        public int SplitBehavior { get; set; }
        public bool SplitRemovesDashes { get; set; }
        public int ClearStatusBarAfterSeconds { get; set; }
        public string Company { get; set; }
        public bool MoveVideo100Or500MsPlaySmallSample { get; set; }
        public bool DisableVideoAutoLoading { get; set; }
        public bool AllowVolumeBoost { get; set; }
        public int NewEmptyDefaultMs { get; set; }
        public bool RightToLeftMode { get; set; }
        public string LastSaveAsFormat { get; set; }
        public bool CheckForUpdates { get; set; }
        public DateTime LastCheckForUpdates { get; set; }
        public bool AutoSave { get; set; }
        public string PreviewAssaText { get; set; }
        public string TagsInToggleHiTags { get; set; }
        public bool ShowProgress { get; set; }
        public bool ShowNegativeDurationInfoOnSave { get; set; }
        public bool ShowFormatRequiresUtf8Warning { get; set; }
        public long DefaultVideoOffsetInMs { get; set; }
        public string DefaultVideoOffsetInMsList { get; set; }
        public long CurrentVideoOffsetInMs { get; set; }
        public bool CurrentVideoIsSmpte { get; set; }
        public bool AutoSetVideoSmpteForTtml { get; set; }
        public bool AutoSetVideoSmpteForTtmlPrompt { get; set; }
        public string TitleBarAsterisk { get; set; } // Show asteriks "before" or "after" file name (any other value will hide asteriks)
        public bool TitleBarFullFileName { get; set; } // Show full file name with path or just file name
        public bool MeasurementConverterCloseOnInsert { get; set; }
        public string MeasurementConverterCategories { get; set; }
        public int SubtitleTextBoxMaxHeight { get; set; }
        public bool AllowLetterShortcutsInTextBox { get; set; }
        public Color DarkThemeForeColor { get; set; }
        public Color DarkThemeBackColor { get; set; }
        public bool UseDarkTheme { get; set; }
        public bool DarkThemeShowListViewGridLines { get; set; }
        public bool ShowBetaStuff { get; set; }
        public bool DebugTranslationSync { get; set; }

        public GeneralSettings()
        {
            ShowToolbarNew = true;
            ShowToolbarOpen = true;
            ShowToolbarSave = true;
            ShowToolbarSaveAs = false;
            ShowToolbarFind = true;
            ShowToolbarReplace = true;
            ShowToolbarFixCommonErrors = false;
            ShowToolbarVisualSync = true;
            ShowToolbarSpellCheck = true;
            ShowToolbarNetflixGlyphCheck = true;
            ShowToolbarSettings = false;
            ShowToolbarHelp = true;

            ShowVideoPlayer = true;
            ShowAudioVisualizer = true;
            ShowWaveform = true;
            ShowSpectrogram = true;
            ShowFrameRate = false;
            ShowVideoControls = true;
            DefaultFrameRate = 23.976;
            CurrentFrameRate = DefaultFrameRate;
            SubtitleFontName = "Tahoma";
            SubtitleTextBoxFontSize = 12;
            SubtitleListViewFontSize = 10;
            SubtitleTextBoxSyntaxColor = false;
            SubtitleTextBoxHtmlColor = Color.CornflowerBlue;
            SubtitleTextBoxAssColor = Color.BlueViolet;
            SubtitleTextBoxFontBold = true;
            SubtitleFontColor = Color.Black;
            MeasureFontName = "Arial";
            MeasureFontSize = 24;
            MeasureFontBold = false;
            SubtitleLineMaximumPixelWidth = 576;
            SubtitleBackgroundColor = Color.White;
            CenterSubtitleInTextBox = false;
            DefaultSubtitleFormat = "SubRip";
            DefaultEncoding = TextEncoding.Utf8WithBom;
            AutoConvertToUtf8 = false;
            IgnoreArabicDiacritics = false;
            AutoGuessAnsiEncoding = true;
            ShowRecentFiles = true;
            RememberSelectedLine = true;
            StartLoadLastFile = true;
            StartRememberPositionAndSize = true;
            SubtitleLineMaximumLength = 43;
            MaxNumberOfLines = 2;
            MaxNumberOfLinesPlusAbort = 1;
            MergeLinesShorterThan = 33;
            SubtitleMinimumDisplayMilliseconds = 1000;
            SubtitleMaximumDisplayMilliseconds = 8 * 1000;
            RemoveBadCharsWhenOpening = true;
            MinimumMillisecondsBetweenLines = 24;
            SetStartEndHumanDelay = 100;
            AutoWrapLineWhileTyping = false;
            SubtitleMaximumCharactersPerSeconds = 25.0;
            SubtitleOptimalCharactersPerSeconds = 15.0;
            SubtitleMaximumWordsPerMinute = 400;
            DialogStyle = DialogType.DashBothLinesWithSpace;
            //ContinuationStyle = ContinuationStyle.None;
            ContinuationPause = 2000;
            FixContinuationStyleUncheckInsertsAllCaps = true;
            FixContinuationStyleUncheckInsertsItalic = true;
            FixContinuationStyleUncheckInsertsLowercase = true;
            FixContinuationStyleHideContinuationCandidatesWithoutName = true;
            FixContinuationStyleIgnoreLyrics = true;
            SpellCheckLanguage = null;
            VideoPlayer = string.Empty;
            VideoPlayerDefaultVolume = 75;
            VideoPlayerPreviewFontName = "Tahoma";
            VideoPlayerPreviewFontSize = 12;
            VideoPlayerPreviewFontBold = true;
            VideoPlayerShowStopButton = true;
            VideoPlayerShowMuteButton = true;
            VideoPlayerShowFullscreenButton = true;
            ListViewLineSeparatorString = "<br />";
            ListViewDoubleClickAction = 1;
            SaveAsUseFileNameFrom = "video";
            UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWZYXÆØÃÅÄÖÉÈÁÂÀÇÊÍÓÔÕÚŁАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯĞİŞÜÙÁÌÑÎ";
            DefaultAdjustMilliseconds = 1000;
            AutoRepeatOn = true;
            AutoRepeatCount = 2;
            AutoContinueOn = false;
            AutoContinueDelay = 2;
            ReturnToStartAfterRepeat = false;
            SyncListViewWithVideoWhilePlaying = false;
            AutoBackupSeconds = 60 * 5;
            AutoBackupDeleteAfterMonths = 3;
            SpellChecker = "hunspell";
            AllowEditOfOriginalSubtitle = true;
            PromptDeleteLines = true;
            Undocked = false;
            UndockedVideoPosition = "-32000;-32000";
            UndockedWaveformPosition = "-32000;-32000";
            UndockedVideoControlsPosition = "-32000;-32000";
            WaveformUpdateIntervalMs = 40;
            SmallDelayMilliseconds = 500;
            LargeDelayMilliseconds = 5000;
            OpenSubtitleExtraExtensions = "*.mp4;*.m4v;*.mkv;*.ts"; // matroska/mp4/m4v files (can contain subtitles)
            ListViewColumnsRememberSize = true;
            DirectShowDoubleLoad = true;
            VlcWaveTranscodeSettings = "acodec=s16l"; // "acodec=s16l,channels=1,ab=64,samplerate=8000";
            MpvVideoOutputWindows = string.Empty; // could also be e.g. "gpu" or "directshow"
            MpvVideoOutputLinux = string.Empty; // could also be e.g. "x11";
            MpvHandlesPreviewText = true;
            MpvPreviewTextPrimaryColor = Color.White;
            MpvPreviewTextOutlineWidth = 1;
            MpvPreviewTextShadowWidth = 1;
            MpvPreviewTextOpaqueBox = false;
            MpvPreviewTextAlignment = "2";
            MpvPreviewTextMarginVertical = 10;
            FFmpegSceneThreshold = "0.4"; // threshold for generating scene changes - 0.2 is sensitive (more scene change), 0.6 is less sensitive (fewer scene changes)
            UseTimeFormatHHMMSSFF = false;
            SplitBehavior = 1; // 0=take gap from left, 1=divide evenly, 2=take gap from right
            SplitRemovesDashes = true;
            ClearStatusBarAfterSeconds = 10;
            MoveVideo100Or500MsPlaySmallSample = false;
            DisableVideoAutoLoading = false;
            RightToLeftMode = false;
            LastSaveAsFormat = string.Empty;
            SystemSubtitleFontNameOverride = string.Empty;
            CheckForUpdates = true;
            LastCheckForUpdates = DateTime.Now;
            ShowProgress = false;
            ShowNegativeDurationInfoOnSave = true;
            ShowFormatRequiresUtf8Warning = true;
            DefaultVideoOffsetInMs = 10 * 60 * 60 * 1000;
            DefaultVideoOffsetInMsList = "36000000;3600000";
            DarkThemeForeColor = Color.FromArgb(155, 155, 155);
            DarkThemeBackColor = Color.FromArgb(30, 30, 30);
            UseDarkTheme = false;
            DarkThemeShowListViewGridLines = false;
            AutoSetVideoSmpteForTtml = true;
            AutoSetVideoSmpteForTtmlPrompt = true;
            TitleBarAsterisk = "before";
            MeasurementConverterCloseOnInsert = true;
            MeasurementConverterCategories = "Length;Kilometers;Meters";
            PreviewAssaText = "ABCDEFGHIJKL abcdefghijkl 123";
            TagsInToggleHiTags = "[;]";
            SubtitleTextBoxMaxHeight = 300;
            ShowBetaStuff = false;
            DebugTranslationSync = false;
            NewEmptyDefaultMs = 2000;
            DialogStyle = DialogType.DashBothLinesWithSpace;
            //ContinuationStyle = ContinuationStyle.None;

            if (Configuration.IsRunningOnLinux)
            {
                SubtitleFontName = Configuration.DefaultLinuxFontName;
                VideoPlayerPreviewFontName = Configuration.DefaultLinuxFontName;
            }

            //Profiles = new List<RulesProfile>();
            //CurrentProfile = "Default";
            //Profiles.Add(new RulesProfile
            //{
            //    Name = CurrentProfile,
            //    SubtitleLineMaximumLength = SubtitleLineMaximumLength,
            //    MaxNumberOfLines = MaxNumberOfLines,
            //    MergeLinesShorterThan = MergeLinesShorterThan,
            //    SubtitleMaximumCharactersPerSeconds = (decimal)SubtitleMaximumCharactersPerSeconds,
            //    SubtitleOptimalCharactersPerSeconds = (decimal)SubtitleOptimalCharactersPerSeconds,
            //    SubtitleMaximumDisplayMilliseconds = SubtitleMaximumDisplayMilliseconds,
            //    SubtitleMinimumDisplayMilliseconds = SubtitleMinimumDisplayMilliseconds,
            //    SubtitleMaximumWordsPerMinute = (decimal)SubtitleMaximumWordsPerMinute,
            //    CpsIncludesSpace = !CharactersPerSecondsIgnoreWhiteSpace,
            //    MinimumMillisecondsBetweenLines = MinimumMillisecondsBetweenLines,
            //    DialogStyle = DialogStyle,
            //    ContinuationStyle = ContinuationStyle
            //});
            //AddExtraProfiles(Profiles);
        }

        //internal static void AddExtraProfiles(List<RulesProfile> profiles)
        //{
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Netflix (English)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 20,
        //        SubtitleOptimalCharactersPerSeconds = 15,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 833,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashBothLinesWithoutSpace,
        //        ContinuationStyle = ContinuationStyle.NoneEllipsisForPauses
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Netflix (Other languages)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 17,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 833,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.NoneEllipsisForPauses
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Netflix (Dutch)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 17,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 833,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashSecondLineWithoutSpace,
        //        ContinuationStyle = ContinuationStyle.LeadingTrailingEllipsis
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Amazon Prime (English/Spanish/French)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 17,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 1000,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.NoneEllipsisForPauses,
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Amazon Prime (Arabic)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 20,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 1000,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.NoneEllipsisForPauses,
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Amazon Prime (Danish)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 17,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 1000,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashBothLinesWithoutSpace,
        //        ContinuationStyle = ContinuationStyle.NoneEllipsisForPauses,
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Amazon Prime (Dutch)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 17,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 1000,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 84, // 2 frames for 23.976 fps videos
        //        DialogStyle = DialogType.DashSecondLineWithoutSpace,
        //        ContinuationStyle = ContinuationStyle.NoneEllipsisForPauses,
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Arte (German/English)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 20,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 10000,
        //        SubtitleMinimumDisplayMilliseconds = 1000,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 200, // 5 frames for 25 fps videos
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Dutch professional subtitles (23.976/24 fps)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 15,
        //        SubtitleOptimalCharactersPerSeconds = 11,
        //        SubtitleMaximumDisplayMilliseconds = 7007,
        //        SubtitleMinimumDisplayMilliseconds = 1400,
        //        SubtitleMaximumWordsPerMinute = 280,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 125,
        //        DialogStyle = DialogType.DashSecondLineWithoutSpace,
        //        ContinuationStyle = ContinuationStyle.OnlyTrailingDots
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Dutch professional subtitles (25 fps)",
        //        SubtitleLineMaximumLength = 42,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 43,
        //        SubtitleMaximumCharactersPerSeconds = 15,
        //        SubtitleOptimalCharactersPerSeconds = 11,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 1400,
        //        SubtitleMaximumWordsPerMinute = 280,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 120,
        //        DialogStyle = DialogType.DashSecondLineWithoutSpace,
        //        ContinuationStyle = ContinuationStyle.OnlyTrailingDots
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Dutch fansubs (23.976/24 fps)",
        //        SubtitleLineMaximumLength = 45,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 46,
        //        SubtitleMaximumCharactersPerSeconds = 22.5m,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7007,
        //        SubtitleMinimumDisplayMilliseconds = 1200,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 125,
        //        DialogStyle = DialogType.DashSecondLineWithSpace,
        //        ContinuationStyle = ContinuationStyle.OnlyTrailingDots
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Dutch fansubs (25 fps)",
        //        SubtitleLineMaximumLength = 45,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 46,
        //        SubtitleMaximumCharactersPerSeconds = 22.5m,
        //        SubtitleOptimalCharactersPerSeconds = 12,
        //        SubtitleMaximumDisplayMilliseconds = 7000,
        //        SubtitleMinimumDisplayMilliseconds = 1200,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 120,
        //        DialogStyle = DialogType.DashSecondLineWithSpace,
        //        ContinuationStyle = ContinuationStyle.OnlyTrailingDots
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Danish professional subtitles (23.976/24 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 15,
        //        SubtitleOptimalCharactersPerSeconds = 10,
        //        SubtitleMaximumDisplayMilliseconds = 8008,
        //        SubtitleMinimumDisplayMilliseconds = 2002,
        //        SubtitleMaximumWordsPerMinute = 280,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 125,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.LeadingTrailingDashDots
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "Danish professional subtitles (25 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 15,
        //        SubtitleOptimalCharactersPerSeconds = 10,
        //        SubtitleMaximumDisplayMilliseconds = 8000,
        //        SubtitleMinimumDisplayMilliseconds = 2000,
        //        SubtitleMaximumWordsPerMinute = 280,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 120,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.LeadingTrailingDashDots
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "SW2 (French) (23.976/24 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 25,
        //        SubtitleOptimalCharactersPerSeconds = 18,
        //        SubtitleMaximumDisplayMilliseconds = 5005,
        //        SubtitleMinimumDisplayMilliseconds = 792,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 125,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "SW2 (French) (25 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 25,
        //        SubtitleOptimalCharactersPerSeconds = 18,
        //        SubtitleMaximumDisplayMilliseconds = 5000,
        //        SubtitleMinimumDisplayMilliseconds = 800,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 120,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "SW3 (French) (23.976/24 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 25,
        //        SubtitleOptimalCharactersPerSeconds = 18,
        //        SubtitleMaximumDisplayMilliseconds = 5005,
        //        SubtitleMinimumDisplayMilliseconds = 792,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 167,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "SW3 (French) (25 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 25,
        //        SubtitleOptimalCharactersPerSeconds = 18,
        //        SubtitleMaximumDisplayMilliseconds = 5000,
        //        SubtitleMinimumDisplayMilliseconds = 800,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 160,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "SW4 (French) (23.976/24 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 25,
        //        SubtitleOptimalCharactersPerSeconds = 18,
        //        SubtitleMaximumDisplayMilliseconds = 5005,
        //        SubtitleMinimumDisplayMilliseconds = 792,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 250,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //    profiles.Add(new RulesProfile
        //    {
        //        Name = "SW4 (French) (25 fps)",
        //        SubtitleLineMaximumLength = 40,
        //        MaxNumberOfLines = 2,
        //        MergeLinesShorterThan = 41,
        //        SubtitleMaximumCharactersPerSeconds = 25,
        //        SubtitleOptimalCharactersPerSeconds = 18,
        //        SubtitleMaximumDisplayMilliseconds = 5000,
        //        SubtitleMinimumDisplayMilliseconds = 800,
        //        SubtitleMaximumWordsPerMinute = 300,
        //        CpsIncludesSpace = true,
        //        MinimumMillisecondsBetweenLines = 240,
        //        DialogStyle = DialogType.DashBothLinesWithSpace,
        //        ContinuationStyle = ContinuationStyle.None
        //    });
        //}
    }
}
