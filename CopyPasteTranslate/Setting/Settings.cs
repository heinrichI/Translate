﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CopyPasteTranslate.Setting
{
    public class Settings
    {
        public string Version { get; set; }

        //public CompareSettings Compare { get; set; }
        //public RecentFilesSettings RecentFiles { get; set; }

        public GeneralSettings General { get; set; }

        public ToolsSettings Tools { get; set; }

        //public FcpExportSettings FcpExportSettings { get; set; }
        //public SubtitleSettings SubtitleSettings { get; set; }
        //public ProxySettings Proxy { get; set; }
        //public WordListSettings WordLists { get; set; }
        //public FixCommonErrorsSettings CommonErrors { get; set; }
        //public VobSubOcrSettings VobSubOcr { get; set; }
        //public VideoControlsSettings VideoControls { get; set; }
        //public NetworkSettings NetworkSettings { get; set; }
        //public Shortcuts Shortcuts { get; set; }
        //public RemoveTextForHearingImpairedSettings RemoveTextForHearingImpaired { get; set; }
        //public SubtitleBeaming SubtitleBeaming { get; set; }
        //public List<MultipleSearchAndReplaceGroup> MultipleSearchAndReplaceGroups { get; set; }

        public void Reset()
        {
            //RecentFiles = new RecentFilesSettings();
            General = new GeneralSettings();
            Tools = new ToolsSettings();
            //FcpExportSettings = new FcpExportSettings();
            //WordLists = new WordListSettings();
            //SubtitleSettings = new SubtitleSettings();
            //Proxy = new ProxySettings();
            //CommonErrors = new FixCommonErrorsSettings();
            //VobSubOcr = new VobSubOcrSettings();
            //VideoControls = new VideoControlsSettings();
            //NetworkSettings = new NetworkSettings();
            //MultipleSearchAndReplaceGroups = new List<MultipleSearchAndReplaceGroup>();
            //Shortcuts = new Shortcuts();
            //RemoveTextForHearingImpaired = new RemoveTextForHearingImpairedSettings();
            //SubtitleBeaming = new SubtitleBeaming();
            //Compare = new CompareSettings();
        }

        private Settings()
        {
            Reset();
        }

        public void Save()
        {
            // this is too slow: Serialize(Configuration.SettingsFileName, this);
            CustomSerialize(Configuration.SettingsFileName, this);
        }

        //private static void Serialize(string fileName, Settings settings)
        //{
        //    var s = new XmlSerializer(typeof(Settings));
        //    TextWriter w = new StreamWriter(fileName);
        //    s.Serialize(w, settings);
        //    w.Close();
        //}

        

        public static Settings GetSettings()
        {
            var settings = new Settings();
            var settingsFileName = Configuration.SettingsFileName;
            if (File.Exists(settingsFileName))
            {
                //try
                //{
                    //too slow... :(  - settings = Deserialize(settingsFileName); // 688 msecs
                    settings = CustomDeserialize(settingsFileName); //  15 msecs

                    if (settings.General.DefaultEncoding.StartsWith("utf-8", StringComparison.Ordinal))
                    {
                        settings.General.DefaultEncoding = TextEncoding.Utf8WithBom;
                    }

                    //if (string.IsNullOrEmpty(settings.Version))
                    //{  // 3.5.14 or older
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainVideoToggleStartEndCurrent))
                    //    {
                    //        settings.Shortcuts.MainVideoToggleStartEndCurrent = "F4";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainVideoPlaySelectedLines))
                    //    {
                    //        settings.Shortcuts.MainVideoPlaySelectedLines = "F5";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainVideoGoToStartCurrent))
                    //    {
                    //        settings.Shortcuts.MainVideoGoToStartCurrent = "F6";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainVideo3000MsLeft))
                    //    {
                    //        settings.Shortcuts.MainVideo3000MsLeft = "F7";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainAdjustSetStartAndOffsetTheRest2))
                    //    {
                    //        settings.Shortcuts.MainAdjustSetStartAndOffsetTheRest2 = "F9";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainAdjustSetEndAndGotoNext))
                    //    {
                    //        settings.Shortcuts.MainAdjustSetEndAndGotoNext = "F10";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainCreateSetStart))
                    //    {
                    //        settings.Shortcuts.MainCreateSetStart = "F11";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainCreateSetEnd))
                    //    {
                    //        settings.Shortcuts.MainCreateSetEnd = "F12";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainCreateInsertSubAtVideoPos))
                    //    {
                    //        settings.Shortcuts.MainCreateInsertSubAtVideoPos = "Shift+F9";
                    //    }
                    //    if (string.IsNullOrEmpty(settings.Shortcuts.MainVideoGoToStartCurrent))
                    //    {
                    //        settings.Shortcuts.MainVideoGoToStartCurrent = "Shift+F11";
                    //    }
                    //}
                    //else if (settings.Version.StartsWith("3.5.15", StringComparison.Ordinal) ||
                    //         settings.Version.StartsWith("3.5.14", StringComparison.Ordinal) ||
                    //         settings.Version.StartsWith("3.5.13", StringComparison.Ordinal))
                    //{
                    //    settings.Shortcuts.MainTranslateAuto = "Control+Shift+G";
                    //    settings.Tools.MicrosoftTranslatorTokenEndpoint = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
                    //}
                //}
                //catch (Exception exception)
                //{
                //    settings = new Settings();
                //    SeLogger.Error(exception, "Failed to load " + settingsFileName);
                //}

                if (!string.IsNullOrEmpty(settings.General.ListViewLineSeparatorString))
                {
                    settings.General.ListViewLineSeparatorString = settings.General.ListViewLineSeparatorString.Replace("\n", string.Empty).Replace("\r", string.Empty);
                }

                if (string.IsNullOrWhiteSpace(settings.General.ListViewLineSeparatorString))
                {
                    settings.General.ListViewLineSeparatorString = "<br />";
                }

                //if (settings.Shortcuts.GeneralToggleTranslationMode == "Control+U" && settings.Shortcuts.MainTextBoxSelectionToLower == "Control+U")
                //{
                //    settings.Shortcuts.GeneralToggleTranslationMode = "Control+Shift+O";
                //    settings.Shortcuts.GeneralSwitchOriginalAndTranslation = "Control+Alt+O";
                //}

                if (settings.General.UseFFmpegForWaveExtraction && string.IsNullOrEmpty(settings.General.FFmpegLocation) && Configuration.IsRunningOnWindows)
                {
                    var guessPath = Path.Combine(Configuration.DataDirectory, "ffmpeg", "ffmpeg.exe");
                    if (File.Exists(guessPath))
                    {
                        settings.General.FFmpegLocation = guessPath;
                    }
                }
            }

            return settings;
        }
        
        //private static Settings Deserialize(string fileName)
        //{
        //    var r = new StreamReader(fileName);
        //    var s = new XmlSerializer(typeof(Settings));
        //    var settings = (Settings)s.Deserialize(r);
        //    r.Close();

        //    if (settings.RecentFiles == null)
        //        settings.RecentFiles = new RecentFilesSettings();
        //    if (settings.General == null)
        //        settings.General = new GeneralSettings();
        //    if (settings.SsaStyle == null)
        //        settings.SsaStyle = new SsaStyleSettings();
        //    if (settings.CommonErrors == null)
        //        settings.CommonErrors = new FixCommonErrorsSettings();
        //    if (settings.VideoControls == null)
        //        settings.VideoControls = new VideoControlsSettings();
        //    if (settings.VobSubOcr == null)
        //        settings.VobSubOcr = new VobSubOcrSettings();
        //    if (settings.MultipleSearchAndReplaceList == null)
        //        settings.MultipleSearchAndReplaceList = new List<MultipleSearchAndReplaceSetting>();
        //    if (settings.NetworkSettings == null)
        //        settings.NetworkSettings = new NetworkSettings();
        //    if (settings.Shortcuts == null)
        //        settings.Shortcuts = new Shortcuts();

        //    return settings;
        //}

        /// <summary>
        /// A faster serializer than xml serializer... which is insanely slow (first time)!!!!
        /// This method is auto-generated with XmlSerializerGenerator
        /// </summary>
        /// <param name="fileName">File name of xml settings file to load</param>
        /// <returns>Newly loaded settings</returns>
        private static Settings CustomDeserialize(string fileName)
        {
            var doc = new XmlDocument { PreserveWhitespace = true };
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                doc.Load(stream);
            }

            var settings = new Settings();

            XmlNode versionNode = doc.DocumentElement.SelectSingleNode("Version");
            if (versionNode != null)
            {
                settings.Version = versionNode.InnerText;
            }

            //// Compare
            //XmlNode nodeCompare = doc.DocumentElement.SelectSingleNode("Compare");
            //if (nodeCompare != null)
            //{
            //    XmlNode xnode = nodeCompare.SelectSingleNode("ShowOnlyDifferences");
            //    if (xnode != null)
            //    {
            //        settings.Compare.ShowOnlyDifferences = Convert.ToBoolean(xnode.InnerText);
            //    }

            //    xnode = nodeCompare.SelectSingleNode("OnlyLookForDifferenceInText");
            //    if (xnode != null)
            //    {
            //        settings.Compare.OnlyLookForDifferenceInText = Convert.ToBoolean(xnode.InnerText);
            //    }

            //    xnode = nodeCompare.SelectSingleNode("IgnoreLineBreaks");
            //    if (xnode != null)
            //    {
            //        settings.Compare.IgnoreLineBreaks = Convert.ToBoolean(xnode.InnerText);
            //    }

            //    xnode = nodeCompare.SelectSingleNode("IgnoreFormatting");
            //    if (xnode != null)
            //    {
            //        settings.Compare.IgnoreFormatting = Convert.ToBoolean(xnode.InnerText);
            //    }
            //}

            // Recent files
            //XmlNode node = doc.DocumentElement.SelectSingleNode("RecentFiles");
            //foreach (XmlNode listNode in node.SelectNodes("FileNames/FileName"))
            //{
            //    string firstVisibleIndex = "-1";
            //    if (listNode.Attributes["FirstVisibleIndex"] != null)
            //    {
            //        firstVisibleIndex = listNode.Attributes["FirstVisibleIndex"].Value;
            //    }

            //    string firstSelectedIndex = "-1";
            //    if (listNode.Attributes["FirstSelectedIndex"] != null)
            //    {
            //        firstSelectedIndex = listNode.Attributes["FirstSelectedIndex"].Value;
            //    }

            //    string videoFileName = null;
            //    if (listNode.Attributes["VideoFileName"] != null)
            //    {
            //        videoFileName = listNode.Attributes["VideoFileName"].Value;
            //    }

            //    string originalFileName = null;
            //    if (listNode.Attributes["OriginalFileName"] != null)
            //    {
            //        originalFileName = listNode.Attributes["OriginalFileName"].Value;
            //    }

            //    long videoOffset = 0;
            //    if (listNode.Attributes["VideoOffset"] != null)
            //    {
            //        long.TryParse(listNode.Attributes["VideoOffset"].Value, out videoOffset);
            //    }

            //    bool isSmpte = false;
            //    if (listNode.Attributes["IsSmpte"] != null)
            //    {
            //        bool.TryParse(listNode.Attributes["IsSmpte"].Value, out isSmpte);
            //    }

            //    settings.RecentFiles.Files.Add(new RecentFileEntry { FileName = listNode.InnerText, FirstVisibleIndex = int.Parse(firstVisibleIndex, CultureInfo.InvariantCulture), FirstSelectedIndex = int.Parse(firstSelectedIndex, CultureInfo.InvariantCulture), VideoFileName = videoFileName, OriginalFileName = originalFileName, VideoOffsetInMs = videoOffset, VideoIsSmpte = isSmpte });
            //}

            // General
            XmlNode node = doc.DocumentElement.SelectSingleNode("General");

            // Profiles
            //int profileCount = 0;
            //foreach (XmlNode listNode in node.SelectNodes("Profiles/Profile"))
            //{
            //    if (profileCount == 0)
            //    {
            //        settings.General.Profiles.Clear();
            //    }

            //    var p = new RulesProfile();
            //    var subtitleLineMaximumLength = listNode.SelectSingleNode("SubtitleLineMaximumLength")?.InnerText;
            //    var subtitleMaximumCharactersPerSeconds = listNode.SelectSingleNode("SubtitleMaximumCharactersPerSeconds")?.InnerText;
            //    var subtitleOptimalCharactersPerSeconds = listNode.SelectSingleNode("SubtitleOptimalCharactersPerSeconds")?.InnerText;
            //    var subtitleMinimumDisplayMilliseconds = listNode.SelectSingleNode("SubtitleMinimumDisplayMilliseconds")?.InnerText;
            //    var subtitleMaximumDisplayMilliseconds = listNode.SelectSingleNode("SubtitleMaximumDisplayMilliseconds")?.InnerText;
            //    var subtitleMaximumWordsPerMinute = listNode.SelectSingleNode("SubtitleMaximumWordsPerMinute")?.InnerText;
            //    var cpsIncludesSpace = listNode.SelectSingleNode("CpsIncludesSpace")?.InnerText;
            //    var maxNumberOfLines = listNode.SelectSingleNode("MaxNumberOfLines")?.InnerText;
            //    var mergeLinesShorterThan = listNode.SelectSingleNode("MergeLinesShorterThan")?.InnerText;
            //    var minimumMillisecondsBetweenLines = listNode.SelectSingleNode("MinimumMillisecondsBetweenLines")?.InnerText;

            //    var dialogStyle = DialogType.DashBothLinesWithSpace;
            //    if (listNode.SelectSingleNode("DialogStyle") == null || !Enum.IsDefined(typeof(DialogType), listNode.SelectSingleNode("DialogStyle").InnerText))
            //    { //TODO: Remove after 2022
            //        if (listNode.SelectSingleNode("Name") != null)
            //        {
            //            var lookup = new List<RulesProfile>();
            //            GeneralSettings.AddExtraProfiles(lookup);
            //            var match = lookup.Find(LookupProfile => LookupProfile.Name == listNode.SelectSingleNode("Name").InnerText);
            //            if (match != null)
            //            {
            //                dialogStyle = match.DialogStyle; // update style when upgrading from 3.5.13 or below
            //            }
            //            else
            //            {
            //                dialogStyle = DialogType.DashBothLinesWithSpace;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        dialogStyle = (DialogType)Enum.Parse(typeof(DialogType), listNode.SelectSingleNode("DialogStyle")?.InnerText);
            //    }

            //    var continuationStyle = ContinuationStyle.NoneLeadingTrailingDots;
            //    if (listNode.SelectSingleNode("ContinuationStyle") == null || !Enum.IsDefined(typeof(ContinuationStyle), listNode.SelectSingleNode("ContinuationStyle").InnerText))
            //    { //TODO: Remove after 2022
            //        if (listNode.SelectSingleNode("Name") != null)
            //        {
            //            var lookup = new List<RulesProfile>();
            //            GeneralSettings.AddExtraProfiles(lookup);
            //            var match = lookup.Find(LookupProfile => LookupProfile.Name == listNode.SelectSingleNode("Name").InnerText);
            //            if (match != null)
            //            {
            //                continuationStyle = match.ContinuationStyle; // update style when upgrading from 3.5.13 or below
            //            }
            //            else
            //            {
            //                continuationStyle = ContinuationStyle.NoneLeadingTrailingDots;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        continuationStyle = (ContinuationStyle)Enum.Parse(typeof(ContinuationStyle), listNode.SelectSingleNode("ContinuationStyle")?.InnerText);
            //    }

            //    settings.General.Profiles.Add(new RulesProfile
            //    {
            //        Name = listNode.SelectSingleNode("Name")?.InnerText,
            //        SubtitleLineMaximumLength = Convert.ToInt32(subtitleLineMaximumLength, CultureInfo.InvariantCulture),
            //        SubtitleMaximumCharactersPerSeconds = Convert.ToDecimal(subtitleMaximumCharactersPerSeconds, CultureInfo.InvariantCulture),
            //        SubtitleOptimalCharactersPerSeconds = Convert.ToDecimal(subtitleOptimalCharactersPerSeconds, CultureInfo.InvariantCulture),
            //        SubtitleMinimumDisplayMilliseconds = Convert.ToInt32(subtitleMinimumDisplayMilliseconds, CultureInfo.InvariantCulture),
            //        SubtitleMaximumDisplayMilliseconds = Convert.ToInt32(subtitleMaximumDisplayMilliseconds, CultureInfo.InvariantCulture),
            //        SubtitleMaximumWordsPerMinute = Convert.ToDecimal(subtitleMaximumWordsPerMinute, CultureInfo.InvariantCulture),
            //        CpsIncludesSpace = Convert.ToBoolean(cpsIncludesSpace, CultureInfo.InvariantCulture),
            //        MaxNumberOfLines = Convert.ToInt32(maxNumberOfLines, CultureInfo.InvariantCulture),
            //        MergeLinesShorterThan = Convert.ToInt32(mergeLinesShorterThan, CultureInfo.InvariantCulture),
            //        MinimumMillisecondsBetweenLines = Convert.ToInt32(minimumMillisecondsBetweenLines, CultureInfo.InvariantCulture),
            //        DialogStyle = dialogStyle,
            //        ContinuationStyle = continuationStyle
            //    });
            //    profileCount++;
            //}


            XmlNode subNode = node.SelectSingleNode("CurrentProfile");
            if (subNode != null)
            {
                settings.General.CurrentProfile = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ShowToolbarNew");
            if (subNode != null)
            {
                settings.General.ShowToolbarNew = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarOpen");
            if (subNode != null)
            {
                settings.General.ShowToolbarOpen = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarSave");
            if (subNode != null)
            {
                settings.General.ShowToolbarSave = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarSaveAs");
            if (subNode != null)
            {
                settings.General.ShowToolbarSaveAs = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarFind");
            if (subNode != null)
            {
                settings.General.ShowToolbarFind = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarReplace");
            if (subNode != null)
            {
                settings.General.ShowToolbarReplace = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarFixCommonErrors");
            if (subNode != null)
            {
                settings.General.ShowToolbarFixCommonErrors = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarRemoveTextForHi");
            if (subNode != null)
            {
                settings.General.ShowToolbarRemoveTextForHi = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarVisualSync");
            if (subNode != null)
            {
                settings.General.ShowToolbarVisualSync = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarSpellCheck");
            if (subNode != null)
            {
                settings.General.ShowToolbarSpellCheck = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarNetflixGlyphCheck");
            if (subNode != null)
            {
                settings.General.ShowToolbarNetflixGlyphCheck = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarSettings");
            if (subNode != null)
            {
                settings.General.ShowToolbarSettings = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowToolbarHelp");
            if (subNode != null)
            {
                settings.General.ShowToolbarHelp = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowFrameRate");
            if (subNode != null)
            {
                settings.General.ShowFrameRate = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowVideoControls");
            if (subNode != null)
            {
                settings.General.ShowVideoControls = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("TextAndOrigianlTextBoxesSwitched");
            if (subNode != null)
            {
                settings.General.TextAndOrigianlTextBoxesSwitched = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowVideoPlayer");
            if (subNode != null)
            {
                settings.General.ShowVideoPlayer = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowAudioVisualizer");
            if (subNode != null)
            {
                settings.General.ShowAudioVisualizer = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowWaveform");
            if (subNode != null)
            {
                settings.General.ShowWaveform = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowSpectrogram");
            if (subNode != null)
            {
                settings.General.ShowSpectrogram = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DefaultFrameRate");
            if (subNode != null)
            {
                settings.General.DefaultFrameRate = Convert.ToDouble(subNode.InnerText, CultureInfo.InvariantCulture);
                if (settings.General.DefaultFrameRate > 23975)
                {
                    settings.General.DefaultFrameRate = 23.976;
                }

                settings.General.CurrentFrameRate = settings.General.DefaultFrameRate;
            }

            subNode = node.SelectSingleNode("DefaultSubtitleFormat");
            if (subNode != null)
            {
                settings.General.DefaultSubtitleFormat = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("DefaultSaveAsFormat");
            if (subNode != null)
            {
                settings.General.DefaultSaveAsFormat = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("FavoriteSubtitleFormats");
            if (subNode != null)
            {
                settings.General.FavoriteSubtitleFormats = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("DefaultEncoding");
            if (subNode != null)
            {
                settings.General.DefaultEncoding = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AutoConvertToUtf8");
            if (subNode != null)
            {
                settings.General.AutoConvertToUtf8 = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoGuessAnsiEncoding");
            if (subNode != null)
            {
                settings.General.AutoGuessAnsiEncoding = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SystemSubtitleFontNameOverride");
            if (subNode != null)
            {
                settings.General.SystemSubtitleFontNameOverride = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("SystemSubtitleFontSizeOverride");
            if (!string.IsNullOrEmpty(subNode?.InnerText))
            {
                settings.General.SystemSubtitleFontSizeOverride = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleFontName");
            if (subNode != null)
            {
                settings.General.SubtitleFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("SubtitleTextBoxFontSize");
            if (subNode != null)
            {
                settings.General.SubtitleTextBoxFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleListViewFontSize");
            if (subNode != null)
            {
                settings.General.SubtitleListViewFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleTextBoxFontBold");
            if (subNode != null)
            {
                settings.General.SubtitleTextBoxFontBold = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleListViewFontBold");
            if (subNode != null)
            {
                settings.General.SubtitleListViewFontBold = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleTextBoxSyntaxColor");
            if (subNode != null)
            {
                settings.General.SubtitleTextBoxSyntaxColor = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleTextBoxHtmlColor");
            if (subNode != null)
            {
                settings.General.SubtitleTextBoxHtmlColor = Color.FromArgb(Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("SubtitleTextBoxAssColor");
            if (subNode != null)
            {
                settings.General.SubtitleTextBoxAssColor = Color.FromArgb(Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("SubtitleFontColor");
            if (subNode != null)
            {
                settings.General.SubtitleFontColor = Color.FromArgb(Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("SubtitleBackgroundColor");
            if (subNode != null)
            {
                settings.General.SubtitleBackgroundColor = Color.FromArgb(Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("MeasureFontName");
            if (subNode != null)
            {
                settings.General.MeasureFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MeasureFontSize");
            if (subNode != null)
            {
                settings.General.MeasureFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MeasureFontBold");
            if (subNode != null)
            {
                settings.General.MeasureFontBold = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleLineMaximumPixelWidth");
            if (subNode != null)
            {
                settings.General.SubtitleLineMaximumPixelWidth = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("CenterSubtitleInTextBox");
            if (subNode != null)
            {
                settings.General.CenterSubtitleInTextBox = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowRecentFiles");
            if (subNode != null)
            {
                settings.General.ShowRecentFiles = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("RememberSelectedLine");
            if (subNode != null)
            {
                settings.General.RememberSelectedLine = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("StartLoadLastFile");
            if (subNode != null)
            {
                settings.General.StartLoadLastFile = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("StartRememberPositionAndSize");
            if (subNode != null)
            {
                settings.General.StartRememberPositionAndSize = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("StartPosition");
            if (subNode != null)
            {
                settings.General.StartPosition = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("StartSize");
            if (subNode != null)
            {
                settings.General.StartSize = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("SplitContainerMainSplitterDistance");
            if (subNode != null)
            {
                settings.General.SplitContainerMainSplitterDistance = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SplitContainer1SplitterDistance");
            if (subNode != null)
            {
                settings.General.SplitContainer1SplitterDistance = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SplitContainerListViewAndTextSplitterDistance");
            if (subNode != null)
            {
                settings.General.SplitContainerListViewAndTextSplitterDistance = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("StartInSourceView");
            if (subNode != null)
            {
                settings.General.StartInSourceView = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("RemoveBlankLinesWhenOpening");
            if (subNode != null)
            {
                settings.General.RemoveBlankLinesWhenOpening = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("RemoveBadCharsWhenOpening");
            if (subNode != null)
            {
                settings.General.RemoveBadCharsWhenOpening = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleLineMaximumLength");
            if (subNode != null)
            {
                settings.General.SubtitleLineMaximumLength = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MaxNumberOfLines");
            if (subNode != null)
            {
                settings.General.MaxNumberOfLines = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MaxNumberOfLinesPlusAbort");
            if (subNode != null)
            {
                settings.General.MaxNumberOfLinesPlusAbort = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MergeLinesShorterThan");
            if (subNode != null)
            {
                settings.General.MergeLinesShorterThan = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleMinimumDisplayMilliseconds");
            if (subNode != null)
            {
                settings.General.SubtitleMinimumDisplayMilliseconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleMaximumDisplayMilliseconds");
            if (subNode != null)
            {
                settings.General.SubtitleMaximumDisplayMilliseconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MinimumMillisecondsBetweenLines");
            if (subNode != null)
            {
                settings.General.MinimumMillisecondsBetweenLines = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SetStartEndHumanDelay");
            if (subNode != null)
            {
                settings.General.SetStartEndHumanDelay = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoWrapLineWhileTyping");
            if (subNode != null)
            {
                settings.General.AutoWrapLineWhileTyping = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleMaximumCharactersPerSeconds");
            if (subNode != null)
            {
                settings.General.SubtitleMaximumCharactersPerSeconds = Convert.ToDouble(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleOptimalCharactersPerSeconds");
            if (subNode != null)
            {
                settings.General.SubtitleOptimalCharactersPerSeconds = Convert.ToDouble(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("CharactersPerSecondsIgnoreWhiteSpace");
            if (subNode != null)
            {
                settings.General.CharactersPerSecondsIgnoreWhiteSpace = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("IgnoreArabicDiacritics");
            if (subNode != null)
            {
                settings.General.IgnoreArabicDiacritics = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SubtitleMaximumWordsPerMinute");
            if (subNode != null)
            {
                settings.General.SubtitleMaximumWordsPerMinute = Convert.ToDouble(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            //subNode = node.SelectSingleNode("DialogStyle");
            //if (subNode != null)
            //{
            //    settings.General.DialogStyle = (DialogType)Enum.Parse(typeof(DialogType), subNode.InnerText);
            //}

            //subNode = node.SelectSingleNode("ContinuationStyle");
            //if (subNode != null)
            //{
            //    settings.General.ContinuationStyle = (ContinuationStyle)Enum.Parse(typeof(ContinuationStyle), subNode.InnerText);
            //}

            subNode = node.SelectSingleNode("ContinuationPause");
            if (subNode != null)
            {
                settings.General.ContinuationPause = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixContinuationStyleUncheckInsertsAllCaps");
            if (subNode != null)
            {
                settings.General.FixContinuationStyleUncheckInsertsAllCaps = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixContinuationStyleUncheckInsertsItalic");
            if (subNode != null)
            {
                settings.General.FixContinuationStyleUncheckInsertsItalic = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixContinuationStyleUncheckInsertsLowercase");
            if (subNode != null)
            {
                settings.General.FixContinuationStyleUncheckInsertsLowercase = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixContinuationStyleHideContinuationCandidatesWithoutName");
            if (subNode != null)
            {
                settings.General.FixContinuationStyleHideContinuationCandidatesWithoutName = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixContinuationStyleIgnoreLyrics");
            if (subNode != null)
            {
                settings.General.FixContinuationStyleIgnoreLyrics = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SpellCheckLanguage");
            if (subNode != null)
            {
                settings.General.SpellCheckLanguage = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("VideoPlayer");
            if (subNode != null)
            {
                settings.General.VideoPlayer = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("VideoPlayerDefaultVolume");
            if (subNode != null)
            {
                settings.General.VideoPlayerDefaultVolume = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VideoPlayerPreviewFontName");
            if (subNode != null)
            {
                settings.General.VideoPlayerPreviewFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("VideoPlayerPreviewFontSize");
            if (subNode != null)
            {
                settings.General.VideoPlayerPreviewFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VideoPlayerPreviewFontBold");
            if (subNode != null)
            {
                settings.General.VideoPlayerPreviewFontBold = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VideoPlayerShowStopButton");
            if (subNode != null)
            {
                settings.General.VideoPlayerShowStopButton = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VideoPlayerShowMuteButton");
            if (subNode != null)
            {
                settings.General.VideoPlayerShowMuteButton = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VideoPlayerShowFullscreenButton");
            if (subNode != null)
            {
                settings.General.VideoPlayerShowFullscreenButton = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("Language");
            if (subNode != null)
            {
                settings.General.Language = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ListViewLineSeparatorString");
            if (subNode != null)
            {
                settings.General.ListViewLineSeparatorString = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ListViewDoubleClickAction");
            if (subNode != null)
            {
                settings.General.ListViewDoubleClickAction = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SaveAsUseFileNameFrom");
            if (subNode != null)
            {
                settings.General.SaveAsUseFileNameFrom = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("UppercaseLetters");
            if (subNode != null)
            {
                settings.General.UppercaseLetters = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("DefaultAdjustMilliseconds");
            if (subNode != null)
            {
                settings.General.DefaultAdjustMilliseconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoRepeatOn");
            if (subNode != null)
            {
                settings.General.AutoRepeatOn = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoRepeatCount");
            if (subNode != null)
            {
                settings.General.AutoRepeatCount = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SyncListViewWithVideoWhilePlaying");
            if (subNode != null)
            {
                settings.General.SyncListViewWithVideoWhilePlaying = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoContinueOn");
            if (subNode != null)
            {
                settings.General.AutoContinueOn = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoContinueDelay");
            if (subNode != null)
            {
                settings.General.AutoContinueDelay = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ReturnToStartAfterRepeat");
            if (subNode != null)
            {
                settings.General.ReturnToStartAfterRepeat = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBackupSeconds");
            if (subNode != null)
            {
                settings.General.AutoBackupSeconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBackupDeleteAfterMonths");
            if (subNode != null)
            {
                settings.General.AutoBackupDeleteAfterMonths = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SpellChecker");
            if (subNode != null)
            {
                settings.General.SpellChecker = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AllowEditOfOriginalSubtitle");
            if (subNode != null)
            {
                settings.General.AllowEditOfOriginalSubtitle = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("PromptDeleteLines");
            if (subNode != null)
            {
                settings.General.PromptDeleteLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("Undocked");
            if (subNode != null)
            {
                settings.General.Undocked = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("UndockedVideoPosition");
            if (subNode != null)
            {
                settings.General.UndockedVideoPosition = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("UndockedVideoFullscreen");
            if (subNode != null)
            {
                settings.General.UndockedVideoFullscreen = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("UndockedWaveformPosition");
            if (subNode != null)
            {
                settings.General.UndockedWaveformPosition = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("UndockedVideoControlsPosition");
            if (subNode != null)
            {
                settings.General.UndockedVideoControlsPosition = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("WaveformCenter");
            if (subNode != null)
            {
                settings.General.WaveformCenter = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("WaveformAutoGenWhenOpeningVideo");
            if (subNode != null)
            {
                settings.General.WaveformAutoGenWhenOpeningVideo = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("WaveformUpdateIntervalMs");
            if (subNode != null)
            {
                settings.General.WaveformUpdateIntervalMs = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SmallDelayMilliseconds");
            if (subNode != null)
            {
                settings.General.SmallDelayMilliseconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("LargeDelayMilliseconds");
            if (subNode != null)
            {
                settings.General.LargeDelayMilliseconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowOriginalAsPreviewIfAvailable");
            if (subNode != null)
            {
                settings.General.ShowOriginalAsPreviewIfAvailable = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("LastPacCodePage");
            if (subNode != null)
            {
                settings.General.LastPacCodePage = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("OpenSubtitleExtraExtensions");
            if (subNode != null)
            {
                settings.General.OpenSubtitleExtraExtensions = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("ListViewColumnsRememberSize");
            if (subNode != null)
            {
                settings.General.ListViewColumnsRememberSize = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("ListViewNumberWidth");
            if (subNode != null)
            {
                settings.General.ListViewNumberWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewStartWidth");
            if (subNode != null)
            {
                settings.General.ListViewStartWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewEndWidth");
            if (subNode != null)
            {
                settings.General.ListViewEndWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewDurationWidth");
            if (subNode != null)
            {
                settings.General.ListViewDurationWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewCpsWidth");
            if (subNode != null)
            {
                settings.General.ListViewCpsWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewWpmWidth");
            if (subNode != null)
            {
                settings.General.ListViewWpmWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewGapWidth");
            if (subNode != null)
            {
                settings.General.ListViewGapWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewActorWidth");
            if (subNode != null)
            {
                settings.General.ListViewActorWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewRegionWidth");
            if (subNode != null)
            {
                settings.General.ListViewRegionWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewTextWidth");
            if (subNode != null)
            {
                settings.General.ListViewTextWidth = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DirectShowDoubleLoad");
            if (subNode != null)
            {
                settings.General.DirectShowDoubleLoad = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VlcWaveTranscodeSettings");
            if (subNode != null)
            {
                settings.General.VlcWaveTranscodeSettings = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("VlcLocation");
            if (subNode != null)
            {
                settings.General.VlcLocation = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("VlcLocationRelative");
            if (subNode != null)
            {
                settings.General.VlcLocationRelative = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("MpvVideoOutputWindows");
            if (subNode != null)
            {
                settings.General.MpvVideoOutputWindows = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("MpvVideoOutputLinux");
            if (subNode != null)
            {
                settings.General.MpvVideoOutputLinux = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("MpvExtraOptions");
            if (subNode != null)
            {
                settings.General.MpvExtraOptions = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("MpvLogging");
            if (subNode != null)
            {
                settings.General.MpvLogging = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("MpvHandlesPreviewText");
            if (subNode != null)
            {
                settings.General.MpvHandlesPreviewText = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("MpvPreviewTextPrimaryColor");
            if (subNode != null)
            {
                settings.General.MpvPreviewTextPrimaryColor = ColorTranslator.FromHtml(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("MpvPreviewTextOutlineWidth");
            if (subNode != null)
            {
                settings.General.MpvPreviewTextOutlineWidth = Convert.ToDecimal(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("MpvPreviewTextShadowWidth");
            if (subNode != null)
            {
                settings.General.MpvPreviewTextShadowWidth = Convert.ToDecimal(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("MpvPreviewTextOpaqueBox");
            if (subNode != null)
            {
                settings.General.MpvPreviewTextOpaqueBox = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("MpvPreviewTextAlignment");
            if (subNode != null)
            {
                settings.General.MpvPreviewTextAlignment = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MpvPreviewTextMarginVertical");
            if (subNode != null)
            {
                settings.General.MpvPreviewTextMarginVertical = Convert.ToInt32(subNode.InnerText.Trim());
            }
            subNode = node.SelectSingleNode("MpcHcLocation");
            if (subNode != null)
            {
                settings.General.MpcHcLocation = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("MkvMergeLocation");
            if (subNode != null)
            {
                settings.General.MkvMergeLocation = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("UseFFmpegForWaveExtraction");
            if (subNode != null)
            {
                settings.General.UseFFmpegForWaveExtraction = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("FFmpegLocation");
            if (subNode != null)
            {
                settings.General.FFmpegLocation = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("FFmpegSceneThreshold");
            if (subNode != null)
            {
                settings.General.FFmpegSceneThreshold = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("UseTimeFormatHHMMSSFF");
            if (subNode != null)
            {
                settings.General.UseTimeFormatHHMMSSFF = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("SplitBehavior");
            if (subNode != null)
            {
                settings.General.SplitBehavior = Convert.ToInt32(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("SplitRemovesDashes");
            if (subNode != null)
            {
                settings.General.SplitRemovesDashes = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("ClearStatusBarAfterSeconds");
            if (subNode != null)
            {
                settings.General.ClearStatusBarAfterSeconds = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("Company");
            if (subNode != null)
            {
                settings.General.Company = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("DisableVideoAutoLoading");
            if (subNode != null)
            {
                settings.General.DisableVideoAutoLoading = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("AllowVolumeBoost");
            if (subNode != null)
            {
                settings.General.AllowVolumeBoost = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("RightToLeftMode");
            if (subNode != null)
            {
                settings.General.RightToLeftMode = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("LastSaveAsFormat");
            if (subNode != null)
            {
                settings.General.LastSaveAsFormat = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("CheckForUpdates");
            if (subNode != null)
            {
                settings.General.CheckForUpdates = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("LastCheckForUpdates");
            if (subNode != null)
            {
                settings.General.LastCheckForUpdates = Convert.ToDateTime(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("AutoSave");
            if (subNode != null)
            {
                settings.General.AutoSave = Convert.ToBoolean(subNode.InnerText.Trim());
            }

            subNode = node.SelectSingleNode("PreviewAssaText");
            if (subNode != null)
            {
                settings.General.PreviewAssaText = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("TagsInToggleHiTags");
            if (subNode != null)
            {
                settings.General.TagsInToggleHiTags = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ShowProgress");
            if (subNode != null)
            {
                settings.General.ShowProgress = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowNegativeDurationInfoOnSave");
            if (subNode != null)
            {
                settings.General.ShowNegativeDurationInfoOnSave = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowFormatRequiresUtf8Warning");
            if (subNode != null)
            {
                settings.General.ShowFormatRequiresUtf8Warning = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DefaultVideoOffsetInMs");
            if (subNode != null)
            {
                settings.General.DefaultVideoOffsetInMs = Convert.ToInt64(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DefaultVideoOffsetInMsList");
            if (subNode != null)
            {
                settings.General.DefaultVideoOffsetInMsList = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AutoSetVideoSmpteForTtml");
            if (subNode != null)
            {
                settings.General.AutoSetVideoSmpteForTtml = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoSetVideoSmpteForTtmlPrompt");
            if (subNode != null)
            {
                settings.General.AutoSetVideoSmpteForTtmlPrompt = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("TitleBarAsterisk");
            if (subNode != null)
            {
                settings.General.TitleBarAsterisk = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("TitleBarFullFileName");
            if (subNode != null)
            {
                settings.General.TitleBarFullFileName = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MeasurementConverterCloseOnInsert");
            if (subNode != null)
            {
                settings.General.MeasurementConverterCloseOnInsert = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MeasurementConverterCategories");
            if (subNode != null)
            {
                settings.General.MeasurementConverterCategories = subNode.InnerText.Trim();
            }

            subNode = node.SelectSingleNode("SubtitleTextBoxMaxHeight");
            if (subNode != null)
            {
                settings.General.SubtitleTextBoxMaxHeight = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AllowLetterShortcutsInTextBox");
            if (subNode != null)
            {
                settings.General.AllowLetterShortcutsInTextBox = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DarkThemeBackColor");
            if (subNode != null)
            {
                settings.General.DarkThemeBackColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("DarkThemeForeColor");
            if (subNode != null)
            {
                settings.General.DarkThemeForeColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("UseDarkTheme");
            if (subNode != null)
            {
                settings.General.UseDarkTheme = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DarkThemeShowListViewGridLines");
            if (subNode != null)
            {
                settings.General.DarkThemeShowListViewGridLines = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ShowBetaStuff");
            if (subNode != null)
            {
                settings.General.ShowBetaStuff = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("DebugTranslationSync");
            if (subNode != null)
            {
                settings.General.DebugTranslationSync = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("NewEmptyDefaultMs");
            if (subNode != null)
            {
                settings.General.NewEmptyDefaultMs = Convert.ToInt32(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MoveVideo100Or500MsPlaySmallSample");
            if (subNode != null)
            {
                settings.General.MoveVideo100Or500MsPlaySmallSample = Convert.ToBoolean(subNode.InnerText.Trim(), CultureInfo.InvariantCulture);
            }

            // Tools
            node = doc.DocumentElement.SelectSingleNode("Tools");

            //// ASSA templates (by user)
            //int assaTagTemplateCount = 0;
            //foreach (XmlNode listNode in node.SelectNodes("AssTagTemplates/Template"))
            //{
            //    if (assaTagTemplateCount == 0)
            //    {
            //        settings.Tools.AssaTagTemplates = new List<AssaTemplateItem>();
            //    }

            //    var template = new AssaTemplateItem();
            //    template.Tag = listNode.SelectSingleNode("Tag")?.InnerText;
            //    template.Hint = listNode.SelectSingleNode("Hint")?.InnerText;
            //    if (!string.IsNullOrEmpty(template.Tag))
            //    {
            //        settings.Tools.AssaTagTemplates.Add(template);
            //    }

            //    assaTagTemplateCount++;
            //}

            subNode = node.SelectSingleNode("StartSceneIndex");
            if (subNode != null)
            {
                settings.Tools.StartSceneIndex = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("EndSceneIndex");
            if (subNode != null)
            {
                settings.Tools.EndSceneIndex = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("VerifyPlaySeconds");
            if (subNode != null)
            {
                settings.Tools.VerifyPlaySeconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixShortDisplayTimesAllowMoveStartTime");
            if (subNode != null)
            {
                settings.Tools.FixShortDisplayTimesAllowMoveStartTime = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("RemoveEmptyLinesBetweenText");
            if (subNode != null)
            {
                settings.Tools.RemoveEmptyLinesBetweenText = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MusicSymbol");
            if (subNode != null)
            {
                settings.Tools.MusicSymbol = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MusicSymbolReplace");
            if (subNode != null)
            {
                settings.Tools.MusicSymbolReplace = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("UnicodeSymbolsToInsert");
            if (subNode != null)
            {
                settings.Tools.UnicodeSymbolsToInsert = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("SpellCheckAutoChangeNameCasing");
            if (subNode != null)
            {
                settings.Tools.SpellCheckAutoChangeNameCasing = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SpellCheckAutoChangeNamesUseSuggestions");
            if (subNode != null)
            {
                settings.Tools.SpellCheckAutoChangeNamesUseSuggestions = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SpellCheckOneLetterWords");
            if (subNode != null)
            {
                settings.Tools.CheckOneLetterWords = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SpellCheckEnglishAllowInQuoteAsIng");
            if (subNode != null)
            {
                settings.Tools.SpellCheckEnglishAllowInQuoteAsIng = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("RememberUseAlwaysList");
            if (subNode != null)
            {
                settings.Tools.RememberUseAlwaysList = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("LiveSpellCheck");
            if (subNode != null)
            {
                settings.Tools.LiveSpellCheck = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SpellCheckShowCompletedMessage");
            if (subNode != null)
            {
                settings.Tools.SpellCheckShowCompletedMessage = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("OcrFixUseHardcodedRules");
            if (subNode != null)
            {
                settings.Tools.OcrFixUseHardcodedRules = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("OcrBinaryImageCompareRgbThreshold");
            if (subNode != null)
            {
                settings.Tools.OcrBinaryImageCompareRgbThreshold = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("OcrTesseract4RgbThreshold");
            if (subNode != null)
            {
                settings.Tools.OcrTesseract4RgbThreshold = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("OcrAddLetterRow1");
            if (subNode != null)
            {
                settings.Tools.OcrAddLetterRow1 = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("OcrAddLetterRow2");
            if (subNode != null)
            {
                settings.Tools.OcrAddLetterRow2 = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("OcrTrainFonts");
            if (subNode != null)
            {
                settings.Tools.OcrTrainFonts = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("OcrTrainMergedLetters");
            if (subNode != null)
            {
                settings.Tools.OcrTrainMergedLetters = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("OcrTrainSrtFile");
            if (subNode != null)
            {
                settings.Tools.OcrTrainSrtFile = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("OcrUseWordSplitList");
            if (subNode != null)
            {
                settings.Tools.OcrUseWordSplitList = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BDOpenIn");
            if (subNode != null)
            {
                settings.Tools.BDOpenIn = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("Interjections");
            if (subNode != null)
            {
                settings.Tools.Interjections = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MicrosoftBingApiId");
            if (subNode != null)
            {
                settings.Tools.MicrosoftBingApiId = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MicrosoftTranslatorApiKey");
            if (subNode != null)
            {
                settings.Tools.MicrosoftTranslatorApiKey = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MicrosoftTranslatorTokenEndpoint");
            if (subNode != null)
            {
                settings.Tools.MicrosoftTranslatorTokenEndpoint = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MicrosoftTranslatorCategory");
            if (subNode != null)
            {
                settings.Tools.MicrosoftTranslatorCategory = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GoogleApiV2Key");
            if (subNode != null)
            {
                settings.Tools.GoogleApiV2Key = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GoogleTranslateNoKeyWarningShow");
            if (subNode != null)
            {
                settings.Tools.GoogleTranslateNoKeyWarningShow = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GoogleApiV1ChunkSize");
            if (subNode != null)
            {
                settings.Tools.GoogleApiV1ChunkSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GoogleTranslateLastTargetLanguage");
            if (subNode != null)
            {
                settings.Tools.GoogleTranslateLastTargetLanguage = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("TranslateAllowSplit");
            if (subNode != null)
            {
                settings.Tools.TranslateAllowSplit = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("TranslateLastService");
            if (subNode != null)
            {
                settings.Tools.TranslateLastService = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("TranslateMergeStrategy");
            if (subNode != null)
            {
                settings.Tools.TranslateMergeStrategy = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("TranslateViaCopyPasteSeparator");
            if (subNode != null)
            {
                settings.Tools.TranslateViaCopyPasteSeparator = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("TranslateViaCopyPasteMaxSize");
            if (subNode != null)
            {
                settings.Tools.TranslateViaCopyPasteMaxSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("TranslateViaCopyPasteAutoCopyToClipboard");
            if (subNode != null)
            {
                settings.Tools.TranslateViaCopyPasteAutoCopyToClipboard = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxColorDurationSmall");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxColorDurationSmall = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxColorDurationBig");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxColorDurationBig = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxColorLongLines");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxColorLongLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxColorWideLines");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxColorWideLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxMoreThanXLines");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxMoreThanXLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxColorOverlap");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxColorOverlap = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxColorGap");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxColorGap = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewSyntaxErrorColor");
            if (subNode != null)
            {
                settings.Tools.ListViewSyntaxErrorColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("ListViewUnfocusedSelectedColor");
            if (subNode != null)
            {
                settings.Tools.ListViewUnfocusedSelectedColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("Color1");
            if (subNode != null)
            {
                settings.Tools.Color1 = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("Color2");
            if (subNode != null)
            {
                settings.Tools.Color2 = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("Color3");
            if (subNode != null)
            {
                settings.Tools.Color3 = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("Color4");
            if (subNode != null)
            {
                settings.Tools.Color4 = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnStartTime");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnStartTime = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnEndTime");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnEndTime = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnDuration");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnDuration = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnCharsPerSec");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnCharsPerSec = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnWordsPerMin");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnWordsPerMin = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnGap");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnGap = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnActor");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnActor = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewShowColumnRegion");
            if (subNode != null)
            {
                settings.Tools.ListViewShowColumnRegion = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ListViewMultipleReplaceShowColumnRuleInfo");
            if (subNode != null)
            {
                settings.Tools.ListViewMultipleReplaceShowColumnRuleInfo = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SplitAdvanced");
            if (subNode != null)
            {
                settings.Tools.SplitAdvanced = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SplitOutputFolder");
            if (subNode != null)
            {
                settings.Tools.SplitOutputFolder = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("SplitNumberOfParts");
            if (subNode != null)
            {
                settings.Tools.SplitNumberOfParts = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("SplitVia");
            if (subNode != null)
            {
                settings.Tools.SplitVia = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("JoinCorrectTimeCodes");
            if (subNode != null)
            {
                settings.Tools.JoinCorrectTimeCodes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("JoinAddMs");
            if (subNode != null)
            {
                settings.Tools.JoinAddMs = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("NewEmptyTranslationText");
            if (subNode != null)
            {
                settings.Tools.NewEmptyTranslationText = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertOutputFolder");
            if (subNode != null)
            {
                settings.Tools.BatchConvertOutputFolder = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertOverwriteExisting");
            if (subNode != null)
            {
                settings.Tools.BatchConvertOverwriteExisting = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertSaveInSourceFolder");
            if (subNode != null)
            {
                settings.Tools.BatchConvertSaveInSourceFolder = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertRemoveFormatting");
            if (subNode != null)
            {
                settings.Tools.BatchConvertRemoveFormatting = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertRemoveStyle");
            if (subNode != null)
            {
                settings.Tools.BatchConvertRemoveStyle = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertBridgeGaps");
            if (subNode != null)
            {
                settings.Tools.BatchConvertBridgeGaps = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertFixCasing");
            if (subNode != null)
            {
                settings.Tools.BatchConvertFixCasing = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertRemoveTextForHI");
            if (subNode != null)
            {
                settings.Tools.BatchConvertRemoveTextForHI = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertFixCommonErrors");
            if (subNode != null)
            {
                settings.Tools.BatchConvertFixCommonErrors = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertMultipleReplace");
            if (subNode != null)
            {
                settings.Tools.BatchConvertMultipleReplace = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertFixRtl");
            if (subNode != null)
            {
                settings.Tools.BatchConvertFixRtl = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertFixRtlMode");
            if (subNode != null)
            {
                settings.Tools.BatchConvertFixRtlMode = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertAutoBalance");
            if (subNode != null)
            {
                settings.Tools.BatchConvertAutoBalance = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertSplitLongLines");
            if (subNode != null)
            {
                settings.Tools.BatchConvertSplitLongLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertSetMinDisplayTimeBetweenSubtitles");
            if (subNode != null)
            {
                settings.Tools.BatchConvertSetMinDisplayTimeBetweenSubtitles = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertMergeShortLines");
            if (subNode != null)
            {
                settings.Tools.BatchConvertMergeShortLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertRemoveLineBreaks");
            if (subNode != null)
            {
                settings.Tools.BatchConvertRemoveLineBreaks = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertMergeSameText");
            if (subNode != null)
            {
                settings.Tools.BatchConvertMergeSameText = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertMergeSameTimeCodes");
            if (subNode != null)
            {
                settings.Tools.BatchConvertMergeSameTimeCodes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertChangeSpeed");
            if (subNode != null)
            {
                settings.Tools.BatchConvertChangeSpeed = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertAdjustDisplayDuration");
            if (subNode != null)
            {
                settings.Tools.BatchConvertAdjustDisplayDuration = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertApplyDurationLimits");
            if (subNode != null)
            {
                settings.Tools.BatchConvertApplyDurationLimits = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertDeleteLines");
            if (subNode != null)
            {
                settings.Tools.BatchConvertDeleteLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertAssaChangeRes");
            if (subNode != null)
            {
                settings.Tools.BatchConvertAssaChangeRes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertChangeFrameRate");
            if (subNode != null)
            {
                settings.Tools.BatchConvertChangeFrameRate = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertOffsetTimeCodes");
            if (subNode != null)
            {
                settings.Tools.BatchConvertOffsetTimeCodes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertLanguage");
            if (subNode != null)
            {
                settings.Tools.BatchConvertLanguage = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertFormat");
            if (subNode != null)
            {
                settings.Tools.BatchConvertFormat = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertAssStyles");
            if (subNode != null)
            {
                settings.Tools.BatchConvertAssStyles = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertSsaStyles");
            if (subNode != null)
            {
                settings.Tools.BatchConvertSsaStyles = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertUseStyleFromSource");
            if (subNode != null)
            {
                settings.Tools.BatchConvertUseStyleFromSource = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertExportCustomTextTemplate");
            if (subNode != null)
            {
                settings.Tools.BatchConvertExportCustomTextTemplate = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertTsOverrideXPosition");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOverrideXPosition = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsOverrideYPosition");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOverrideYPosition = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsOverrideBottomMargin");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOverrideBottomMargin = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsOverrideHAlign");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOverrideHAlign = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertTsOverrideHMargin");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOverrideHMargin = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsOverrideScreenSize");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOverrideScreenSize = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsScreenWidth");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsScreenWidth = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsScreenHeight");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsScreenHeight = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsOnlyTeletext");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsOnlyTeletext = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BatchConvertTsFileNameAppend");
            if (subNode != null)
            {
                settings.Tools.BatchConvertTsFileNameAppend = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BatchConvertMkvLanguageCodeStyle");
            if (subNode != null)
            {
                settings.Tools.BatchConvertMkvLanguageCodeStyle = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("WaveformBatchLastFolder");
            if (subNode != null)
            {
                settings.Tools.WaveformBatchLastFolder = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ModifySelectionRule");
            if (subNode != null)
            {
                settings.Tools.ModifySelectionRule = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ModifySelectionText");
            if (subNode != null)
            {
                settings.Tools.ModifySelectionText = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ModifySelectionCaseSensitive");
            if (subNode != null)
            {
                settings.Tools.ModifySelectionCaseSensitive = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportVobSubFontName");
            if (subNode != null)
            {
                settings.Tools.ExportVobSubFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportVobSubFontSize");
            if (subNode != null)
            {
                settings.Tools.ExportVobSubFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportVobSubVideoResolution");
            if (subNode != null)
            {
                settings.Tools.ExportVobSubVideoResolution = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportVobSubSimpleRendering");
            if (subNode != null)
            {
                settings.Tools.ExportVobSubSimpleRendering = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportVobAntiAliasingWithTransparency");
            if (subNode != null)
            {
                settings.Tools.ExportVobAntiAliasingWithTransparency = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportVobSubLanguage");
            if (subNode != null)
            {
                settings.Tools.ExportVobSubLanguage = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportBluRayFontName");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportBluRayFontSize");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportFcpFontName");
            if (subNode != null)
            {
                settings.Tools.ExportFcpFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportFontNameOther");
            if (subNode != null)
            {
                settings.Tools.ExportFontNameOther = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportFcpFontSize");
            if (subNode != null)
            {
                settings.Tools.ExportFcpFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportFcpImageType");
            if (subNode != null)
            {
                settings.Tools.ExportFcpImageType = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportFcpPalNtsc");
            if (subNode != null)
            {
                settings.Tools.ExportFcpPalNtsc = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportBdnXmlImageType");
            if (subNode != null)
            {
                settings.Tools.ExportBdnXmlImageType = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportLastFontSize");
            if (subNode != null)
            {
                settings.Tools.ExportLastFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLastLineHeight");
            if (subNode != null)
            {
                settings.Tools.ExportLastLineHeight = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLastBorderWidth");
            if (subNode != null)
            {
                settings.Tools.ExportLastBorderWidth = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLastFontBold");
            if (subNode != null)
            {
                settings.Tools.ExportLastFontBold = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBluRayVideoResolution");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayVideoResolution = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportFcpVideoResolution");
            if (subNode != null)
            {
                settings.Tools.ExportFcpVideoResolution = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportFontColor");
            if (subNode != null)
            {
                settings.Tools.ExportFontColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("ExportBorderColor");
            if (subNode != null)
            {
                settings.Tools.ExportBorderColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("ExportShadowColor");
            if (subNode != null)
            {
                settings.Tools.ExportShadowColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("ExportBoxBorderSize");
            if (subNode != null)
            {
                settings.Tools.ExportBoxBorderSize = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBottomMarginUnit");
            if (subNode != null)
            {
                settings.Tools.ExportBottomMarginUnit = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportBottomMarginPercent");
            if (subNode != null)
            {
                settings.Tools.ExportBottomMarginPercent = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBottomMarginPixels");
            if (subNode != null)
            {
                settings.Tools.ExportBottomMarginPixels = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLeftRightMarginUnit");
            if (subNode != null)
            {
                settings.Tools.ExportLeftRightMarginUnit = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportLeftRightMarginPercent");
            if (subNode != null)
            {
                settings.Tools.ExportLeftRightMarginPercent = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLeftRightMarginPixels");
            if (subNode != null)
            {
                settings.Tools.ExportLeftRightMarginPixels = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportHorizontalAlignment");
            if (subNode != null)
            {
                settings.Tools.ExportHorizontalAlignment = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBluRayBottomMarginPercent");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayBottomMarginPercent = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBluRayBottomMarginPixels");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayBottomMarginPixels = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBluRayShadow");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayShadow = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportBluRayRemoveSmallGaps");
            if (subNode != null)
            {
                settings.Tools.ExportBluRayRemoveSmallGaps = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportCdgBackgroundImage");
            if (subNode != null)
            {
                settings.Tools.ExportCdgBackgroundImage = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportCdgMarginLeft");
            if (subNode != null)
            {
                settings.Tools.ExportCdgMarginLeft = Convert.ToInt32(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("ExportCdgMarginBottom");
            if (subNode != null)
            {
                settings.Tools.ExportCdgMarginBottom = Convert.ToInt32(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("ExportCdgFormat");
            if (subNode != null)
            {
                settings.Tools.ExportCdgFormat = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("Export3DType");
            if (subNode != null)
            {
                settings.Tools.Export3DType = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("Export3DDepth");
            if (subNode != null)
            {
                settings.Tools.Export3DDepth = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLastShadowTransparency");
            if (subNode != null)
            {
                settings.Tools.ExportLastShadowTransparency = int.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportLastFrameRate");
            if (subNode != null)
            {
                settings.Tools.ExportLastFrameRate = double.Parse(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportFullFrame");
            if (subNode != null)
            {
                settings.Tools.ExportFullFrame = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportFcpFullPathUrl");
            if (subNode != null)
            {
                settings.Tools.ExportFcpFullPathUrl = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportPenLineJoin");
            if (subNode != null)
            {
                settings.Tools.ExportPenLineJoin = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("FixCommonErrorsFixOverlapAllowEqualEndStart");
            if (subNode != null)
            {
                settings.Tools.FixCommonErrorsFixOverlapAllowEqualEndStart = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FixCommonErrorsSkipStepOne");
            if (subNode != null)
            {
                settings.Tools.FixCommonErrorsSkipStepOne = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextSplitting");
            if (subNode != null)
            {
                settings.Tools.ImportTextSplitting = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ImportTextMergeShortLines");
            if (subNode != null)
            {
                settings.Tools.ImportTextMergeShortLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextLineBreak");
            if (subNode != null)
            {
                settings.Tools.ImportTextLineBreak = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ImportTextRemoveEmptyLines");
            if (subNode != null)
            {
                settings.Tools.ImportTextRemoveEmptyLines = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextAutoSplitAtBlank");
            if (subNode != null)
            {
                settings.Tools.ImportTextAutoSplitAtBlank = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextRemoveLinesNoLetters");
            if (subNode != null)
            {
                settings.Tools.ImportTextRemoveLinesNoLetters = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextGenerateTimeCodes");
            if (subNode != null)
            {
                settings.Tools.ImportTextGenerateTimeCodes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextTakeTimeCodeFromFileName");
            if (subNode != null)
            {
                settings.Tools.ImportTextTakeTimeCodeFromFileName = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextAutoBreak");
            if (subNode != null)
            {
                settings.Tools.ImportTextAutoBreak = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextAutoBreakAtEnd");
            if (subNode != null)
            {
                settings.Tools.ImportTextAutoBreakAtEnd = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextGap");
            if (subNode != null)
            {
                settings.Tools.ImportTextGap = Convert.ToDecimal(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("ImportTextAutoSplitNumberOfLines");
            if (subNode != null)
            {
                settings.Tools.ImportTextAutoSplitNumberOfLines = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextAutoBreakAtEndMarkerText");
            if (subNode != null)
            {
                settings.Tools.ImportTextAutoBreakAtEndMarkerText = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ImportTextDurationAuto");
            if (subNode != null)
            {
                settings.Tools.ImportTextDurationAuto = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ImportTextFixedDuration");
            if (subNode != null)
            {
                settings.Tools.ImportTextFixedDuration = Convert.ToDecimal(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("GenerateTimeCodePatterns");
            if (subNode != null)
            {
                settings.Tools.GenerateTimeCodePatterns = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenerateTimeCodePatterns");
            if (subNode != null)
            {
                settings.Tools.GenerateTimeCodePatterns = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("MusicSymbolStyle");
            if (subNode != null)
            {
                settings.Tools.MusicSymbolStyle = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BinEditBackgroundColor");
            if (subNode != null)
            {
                settings.Tools.BinEditBackgroundColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("BinEditImageBackgroundColor");
            if (subNode != null)
            {
                settings.Tools.BinEditImageBackgroundColor = Color.FromArgb(int.Parse(subNode.InnerText, CultureInfo.InvariantCulture));
            }

            subNode = node.SelectSingleNode("BinEditVerticalMargin");
            if (subNode != null)
            {
                settings.Tools.BinEditVerticalMargin = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BinEditLeftMargin");
            if (subNode != null)
            {
                settings.Tools.BinEditLeftMargin = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BinEditRightMargin");
            if (subNode != null)
            {
                settings.Tools.BinEditRightMargin = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BridgeGapMilliseconds");
            if (subNode != null)
            {
                settings.Tools.BridgeGapMilliseconds = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportCustomTemplates");
            if (subNode != null)
            {
                settings.Tools.ExportCustomTemplates = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ChangeCasingChoice");
            if (subNode != null)
            {
                settings.Tools.ChangeCasingChoice = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("UseNoLineBreakAfter");
            if (subNode != null)
            {
                settings.Tools.UseNoLineBreakAfter = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("NoLineBreakAfterEnglish");
            if (subNode != null)
            {
                settings.Tools.NoLineBreakAfterEnglish = subNode.InnerText.Replace("  ", " ");
            }

            subNode = node.SelectSingleNode("ExportTextFormatText");
            if (subNode != null)
            {
                settings.Tools.ExportTextFormatText = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportTextRemoveStyling");
            if (subNode != null)
            {
                settings.Tools.ExportTextRemoveStyling = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextShowLineNumbers");
            if (subNode != null)
            {
                settings.Tools.ExportTextShowLineNumbers = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextShowLineNumbersNewLine");
            if (subNode != null)
            {
                settings.Tools.ExportTextShowLineNumbersNewLine = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextShowTimeCodes");
            if (subNode != null)
            {
                settings.Tools.ExportTextShowTimeCodes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextShowTimeCodesNewLine");
            if (subNode != null)
            {
                settings.Tools.ExportTextShowTimeCodesNewLine = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextNewLineAfterText");
            if (subNode != null)
            {
                settings.Tools.ExportTextNewLineAfterText = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextNewLineBetweenSubtitles");
            if (subNode != null)
            {
                settings.Tools.ExportTextNewLineBetweenSubtitles = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ExportTextTimeCodeFormat");
            if (subNode != null)
            {
                settings.Tools.ExportTextTimeCodeFormat = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ExportTextTimeCodeSeparator");
            if (subNode != null)
            {
                settings.Tools.ExportTextTimeCodeSeparator = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("VideoOffsetKeepTimeCodes");
            if (subNode != null)
            {
                settings.Tools.VideoOffsetKeepTimeCodes = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MoveStartEndMs");
            if (subNode != null)
            {
                settings.Tools.MoveStartEndMs = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AdjustDurationSeconds");
            if (subNode != null)
            {
                settings.Tools.AdjustDurationSeconds = Convert.ToDecimal(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AdjustDurationPercent");
            if (subNode != null)
            {
                settings.Tools.AdjustDurationPercent = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AdjustDurationLast");
            if (subNode != null)
            {
                settings.Tools.AdjustDurationLast = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AdjustDurationExtendOnly");
            if (subNode != null)
            {
                settings.Tools.AdjustDurationExtendOnly = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBreakCommaBreakEarly");
            if (subNode != null)
            {
                settings.Tools.AutoBreakCommaBreakEarly = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBreakDashEarly");
            if (subNode != null)
            {
                settings.Tools.AutoBreakDashEarly = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBreakLineEndingEarly");
            if (subNode != null)
            {
                settings.Tools.AutoBreakLineEndingEarly = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBreakUsePixelWidth");
            if (subNode != null)
            {
                settings.Tools.AutoBreakUsePixelWidth = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBreakPreferBottomHeavy");
            if (subNode != null)
            {
                settings.Tools.AutoBreakPreferBottomHeavy = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AutoBreakPreferBottomPercent");
            if (subNode != null)
            {
                settings.Tools.AutoBreakPreferBottomPercent = Convert.ToDouble(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ApplyMinimumDurationLimit");
            if (subNode != null)
            {
                settings.Tools.ApplyMinimumDurationLimit = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ApplyMaximumDurationLimit");
            if (subNode != null)
            {
                settings.Tools.ApplyMaximumDurationLimit = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("MergeShortLinesMaxGap");
            if (subNode != null)
            {
                settings.Tools.MergeShortLinesMaxGap = Convert.ToInt32(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("MergeShortLinesMaxChars");
            if (subNode != null)
            {
                settings.Tools.MergeShortLinesMaxChars = Convert.ToInt32(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("MergeShortLinesOnlyContinuous");
            if (subNode != null)
            {
                settings.Tools.MergeShortLinesOnlyContinuous = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("ColumnPasteColumn");
            if (subNode != null)
            {
                settings.Tools.ColumnPasteColumn = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("ColumnPasteOverwriteMode");
            if (subNode != null)
            {
                settings.Tools.ColumnPasteOverwriteMode = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AssaAttachmentFontTextPreview");
            if (subNode != null)
            {
                settings.Tools.AssaAttachmentFontTextPreview = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AssaSetPositionTarget");
            if (subNode != null)
            {
                settings.Tools.AssaSetPositionTarget = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("VisualSyncStartSize");
            if (subNode != null)
            {
                settings.Tools.VisualSyncStartSize = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("BlankVideoColor");
            if (subNode != null)
            {
                settings.Tools.BlankVideoColor = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("BlankVideoMinutes");
            if (subNode != null)
            {
                settings.Tools.BlankVideoMinutes = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BlankVideoFrameRate");
            if (subNode != null)
            {
                settings.Tools.BlankVideoFrameRate = Convert.ToDecimal(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("BlankVideoUseCheckeredImage");
            if (subNode != null)
            {
                settings.Tools.BlankVideoUseCheckeredImage = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AssaProgressBarBackColor");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarBackColor = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("AssaProgressBarForeColor");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarForeColor = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("AssaProgressBarTextColor");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarTextColor = ColorTranslator.FromHtml(subNode.InnerText);
            }

            subNode = node.SelectSingleNode("AssaProgressBarHeight");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarHeight = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AssaProgressBarSplitterWidth");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarSplitterWidth = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AssaProgressBarSplitterHeight");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarSplitterHeight = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AssaProgressBarFontName");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarFontName = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("AssaProgressBarFontSize");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarFontSize = Convert.ToInt32(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AssaProgressBarTopAlign");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarTopAlign = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("AssaProgressBarTextAlign");
            if (subNode != null)
            {
                settings.Tools.AssaProgressBarTextAlign = subNode.InnerText;
            }


            subNode = node.SelectSingleNode("GenVideoEncoding");
            if (subNode != null)
            {
                settings.Tools.GenVideoEncoding = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenVideoPreset");
            if (subNode != null)
            {
                settings.Tools.GenVideoPreset = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenVideoCrf");
            if (subNode != null)
            {
                settings.Tools.GenVideoCrf = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenVideoTune");
            if (subNode != null)
            {
                settings.Tools.GenVideoTune = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenVideoAudioEncoding");
            if (subNode != null)
            {
                settings.Tools.GenVideoAudioEncoding = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenVideoAudioForceStereo");
            if (subNode != null)
            {
                settings.Tools.GenVideoAudioForceStereo = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GenVideoAudioSampleRate");
            if (subNode != null)
            {
                settings.Tools.GenVideoAudioSampleRate = subNode.InnerText;
            }

            subNode = node.SelectSingleNode("GenVideoTargetFileSize");
            if (subNode != null)
            {
                settings.Tools.GenVideoTargetFileSize = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GenVideoFontSizePercentOfHeight");
            if (subNode != null)
            {
                settings.Tools.GenVideoFontSizePercentOfHeight = (float)Convert.ToDecimal(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GenVideoNonAssaBox");
            if (subNode != null)
            {
                settings.Tools.GenVideoNonAssaBox = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GenVideoNonAssaAlignRight");
            if (subNode != null)
            {
                settings.Tools.GenVideoNonAssaAlignRight = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("GenVideoNonAssaFixRtlUnicode");
            if (subNode != null)
            {
                settings.Tools.GenVideoNonAssaFixRtlUnicode = Convert.ToBoolean(subNode.InnerText, CultureInfo.InvariantCulture);
            }

            subNode = node.SelectSingleNode("FindHistory");
            if (subNode != null)
            {
                foreach (XmlNode findItem in subNode.ChildNodes)
                {
                    if (findItem.Name == "Text")
                    {
                        settings.Tools.FindHistory.Add(findItem.InnerText);
                    }
                }
            }


            return settings;
        }
       

        private static void CustomSerialize(string fileName, Settings settings)
        {
            var xws = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
            var sb = new StringBuilder();
            using (var textWriter = XmlWriter.Create(sb, xws))
            {
                textWriter.WriteStartDocument();

                textWriter.WriteStartElement("Settings", string.Empty);

                textWriter.WriteElementString("Version", Utilities.AssemblyVersion);

                //textWriter.WriteStartElement("Compare", string.Empty);
                //textWriter.WriteElementString("ShowOnlyDifferences", settings.Compare.ShowOnlyDifferences.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("OnlyLookForDifferenceInText", settings.Compare.OnlyLookForDifferenceInText.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("IgnoreLineBreaks", settings.Compare.IgnoreLineBreaks.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("IgnoreFormatting", settings.Compare.IgnoreFormatting.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("RecentFiles", string.Empty);
                //textWriter.WriteStartElement("FileNames", string.Empty);
                //foreach (var item in settings.RecentFiles.Files)
                //{
                //    textWriter.WriteStartElement("FileName");
                //    if (item.OriginalFileName != null)
                //    {
                //        textWriter.WriteAttributeString("OriginalFileName", item.OriginalFileName);
                //    }

                //    if (item.VideoFileName != null)
                //    {
                //        textWriter.WriteAttributeString("VideoFileName", item.VideoFileName);
                //    }

                //    textWriter.WriteAttributeString("FirstVisibleIndex", item.FirstVisibleIndex.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteAttributeString("FirstSelectedIndex", item.FirstSelectedIndex.ToString(CultureInfo.InvariantCulture));

                //    if (item.VideoOffsetInMs != 0)
                //    {
                //        textWriter.WriteAttributeString("VideoOffset", item.VideoOffsetInMs.ToString(CultureInfo.InvariantCulture));
                //    }

                //    if (item.VideoIsSmpte)
                //    {
                //        textWriter.WriteAttributeString("IsSmpte", item.VideoIsSmpte.ToString(CultureInfo.InvariantCulture));
                //    }

                //    textWriter.WriteString(item.FileName);
                //    textWriter.WriteEndElement();
                //}
                //textWriter.WriteEndElement();
                //textWriter.WriteEndElement();

                textWriter.WriteStartElement("General", string.Empty);

                //textWriter.WriteStartElement("Profiles", string.Empty);
                //foreach (var profile in settings.General.Profiles)
                //{
                //    textWriter.WriteStartElement("Profile");
                //    textWriter.WriteElementString("Name", profile.Name);
                //    textWriter.WriteElementString("SubtitleLineMaximumLength", profile.SubtitleLineMaximumLength.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("SubtitleMaximumCharactersPerSeconds", profile.SubtitleMaximumCharactersPerSeconds.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("SubtitleOptimalCharactersPerSeconds", profile.SubtitleOptimalCharactersPerSeconds.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("SubtitleMinimumDisplayMilliseconds", profile.SubtitleMinimumDisplayMilliseconds.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("SubtitleMaximumDisplayMilliseconds", profile.SubtitleMaximumDisplayMilliseconds.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("SubtitleMaximumWordsPerMinute", profile.SubtitleMaximumWordsPerMinute.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("MinimumMillisecondsBetweenLines", profile.MinimumMillisecondsBetweenLines.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("CpsIncludesSpace", profile.CpsIncludesSpace.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("MaxNumberOfLines", profile.MaxNumberOfLines.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("MergeLinesShorterThan", profile.MergeLinesShorterThan.ToString(CultureInfo.InvariantCulture));
                //    textWriter.WriteElementString("DialogStyle", profile.DialogStyle.ToString());
                //    textWriter.WriteElementString("ContinuationStyle", profile.ContinuationStyle.ToString());
                //    textWriter.WriteEndElement();
                //}
                //textWriter.WriteEndElement();

                textWriter.WriteElementString("CurrentProfile", settings.General.CurrentProfile);
                textWriter.WriteElementString("ShowToolbarNew", settings.General.ShowToolbarNew.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarOpen", settings.General.ShowToolbarOpen.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarSave", settings.General.ShowToolbarSave.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarSaveAs", settings.General.ShowToolbarSaveAs.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarFind", settings.General.ShowToolbarFind.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarReplace", settings.General.ShowToolbarReplace.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarFixCommonErrors", settings.General.ShowToolbarFixCommonErrors.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarRemoveTextForHi", settings.General.ShowToolbarRemoveTextForHi.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarVisualSync", settings.General.ShowToolbarVisualSync.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarSpellCheck", settings.General.ShowToolbarSpellCheck.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarNetflixGlyphCheck", settings.General.ShowToolbarNetflixGlyphCheck.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarSettings", settings.General.ShowToolbarSettings.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowToolbarHelp", settings.General.ShowToolbarHelp.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowFrameRate", settings.General.ShowFrameRate.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowVideoControls", settings.General.ShowVideoControls.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("TextAndOrigianlTextBoxesSwitched", settings.General.TextAndOrigianlTextBoxesSwitched.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowVideoPlayer", settings.General.ShowVideoPlayer.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowAudioVisualizer", settings.General.ShowAudioVisualizer.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowWaveform", settings.General.ShowWaveform.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowSpectrogram", settings.General.ShowSpectrogram.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DefaultFrameRate", settings.General.DefaultFrameRate.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DefaultSubtitleFormat", settings.General.DefaultSubtitleFormat);
                textWriter.WriteElementString("DefaultSaveAsFormat", settings.General.DefaultSaveAsFormat);
                textWriter.WriteElementString("FavoriteSubtitleFormats", settings.General.FavoriteSubtitleFormats);
                textWriter.WriteElementString("DefaultEncoding", settings.General.DefaultEncoding);
                textWriter.WriteElementString("AutoConvertToUtf8", settings.General.AutoConvertToUtf8.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoGuessAnsiEncoding", settings.General.AutoGuessAnsiEncoding.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SystemSubtitleFontNameOverride", settings.General.SystemSubtitleFontNameOverride);
                textWriter.WriteElementString("SystemSubtitleFontSizeOverride", settings.General.SystemSubtitleFontSizeOverride.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleFontName", settings.General.SubtitleFontName);
                textWriter.WriteElementString("SubtitleTextBoxFontSize", settings.General.SubtitleTextBoxFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleListViewFontSize", settings.General.SubtitleListViewFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleTextBoxFontBold", settings.General.SubtitleTextBoxFontBold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleListViewFontBold", settings.General.SubtitleListViewFontBold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleTextBoxSyntaxColor", settings.General.SubtitleTextBoxSyntaxColor.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleTextBoxHtmlColor", settings.General.SubtitleTextBoxHtmlColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleTextBoxAssColor", settings.General.SubtitleTextBoxAssColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleFontColor", settings.General.SubtitleFontColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleBackgroundColor", settings.General.SubtitleBackgroundColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MeasureFontName", settings.General.MeasureFontName);
                textWriter.WriteElementString("MeasureFontSize", settings.General.MeasureFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MeasureFontBold", settings.General.MeasureFontBold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleLineMaximumPixelWidth", settings.General.SubtitleLineMaximumPixelWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("CenterSubtitleInTextBox", settings.General.CenterSubtitleInTextBox.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowRecentFiles", settings.General.ShowRecentFiles.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("RememberSelectedLine", settings.General.RememberSelectedLine.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("StartLoadLastFile", settings.General.StartLoadLastFile.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("StartRememberPositionAndSize", settings.General.StartRememberPositionAndSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("StartPosition", settings.General.StartPosition);
                textWriter.WriteElementString("StartSize", settings.General.StartSize);
                textWriter.WriteElementString("SplitContainerMainSplitterDistance", settings.General.SplitContainerMainSplitterDistance.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitContainer1SplitterDistance", settings.General.SplitContainer1SplitterDistance.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitContainerListViewAndTextSplitterDistance", settings.General.SplitContainerListViewAndTextSplitterDistance.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("StartInSourceView", settings.General.StartInSourceView.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("RemoveBlankLinesWhenOpening", settings.General.RemoveBlankLinesWhenOpening.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("RemoveBadCharsWhenOpening", settings.General.RemoveBadCharsWhenOpening.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleLineMaximumLength", settings.General.SubtitleLineMaximumLength.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MaxNumberOfLines", settings.General.MaxNumberOfLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MaxNumberOfLinesPlusAbort", settings.General.MaxNumberOfLinesPlusAbort.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MergeLinesShorterThan", settings.General.MergeLinesShorterThan.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleMinimumDisplayMilliseconds", settings.General.SubtitleMinimumDisplayMilliseconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleMaximumDisplayMilliseconds", settings.General.SubtitleMaximumDisplayMilliseconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MinimumMillisecondsBetweenLines", settings.General.MinimumMillisecondsBetweenLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SetStartEndHumanDelay", settings.General.SetStartEndHumanDelay.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoWrapLineWhileTyping", settings.General.AutoWrapLineWhileTyping.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleMaximumCharactersPerSeconds", settings.General.SubtitleMaximumCharactersPerSeconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleOptimalCharactersPerSeconds", settings.General.SubtitleOptimalCharactersPerSeconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("CharactersPerSecondsIgnoreWhiteSpace", settings.General.CharactersPerSecondsIgnoreWhiteSpace.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("IgnoreArabicDiacritics", settings.General.IgnoreArabicDiacritics.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SubtitleMaximumWordsPerMinute", settings.General.SubtitleMaximumWordsPerMinute.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DialogStyle", settings.General.DialogStyle.ToString());
                //textWriter.WriteElementString("ContinuationStyle", settings.General.ContinuationStyle.ToString());
                textWriter.WriteElementString("ContinuationPause", settings.General.ContinuationPause.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FixContinuationStyleUncheckInsertsAllCaps", settings.General.FixContinuationStyleUncheckInsertsAllCaps.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FixContinuationStyleUncheckInsertsItalic", settings.General.FixContinuationStyleUncheckInsertsItalic.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FixContinuationStyleUncheckInsertsLowercase", settings.General.FixContinuationStyleUncheckInsertsLowercase.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FixContinuationStyleHideContinuationCandidatesWithoutName", settings.General.FixContinuationStyleHideContinuationCandidatesWithoutName.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SpellCheckLanguage", settings.General.SpellCheckLanguage);
                textWriter.WriteElementString("VideoPlayer", settings.General.VideoPlayer);
                textWriter.WriteElementString("VideoPlayerDefaultVolume", settings.General.VideoPlayerDefaultVolume.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VideoPlayerPreviewFontName", settings.General.VideoPlayerPreviewFontName);
                textWriter.WriteElementString("VideoPlayerPreviewFontSize", settings.General.VideoPlayerPreviewFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VideoPlayerPreviewFontBold", settings.General.VideoPlayerPreviewFontBold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VideoPlayerShowStopButton", settings.General.VideoPlayerShowStopButton.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VideoPlayerShowMuteButton", settings.General.VideoPlayerShowMuteButton.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VideoPlayerShowFullscreenButton", settings.General.VideoPlayerShowFullscreenButton.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("Language", settings.General.Language);
                textWriter.WriteElementString("ListViewLineSeparatorString", settings.General.ListViewLineSeparatorString);
                textWriter.WriteElementString("ListViewDoubleClickAction", settings.General.ListViewDoubleClickAction.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SaveAsUseFileNameFrom", settings.General.SaveAsUseFileNameFrom);
                textWriter.WriteElementString("UppercaseLetters", settings.General.UppercaseLetters);
                textWriter.WriteElementString("DefaultAdjustMilliseconds", settings.General.DefaultAdjustMilliseconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoRepeatOn", settings.General.AutoRepeatOn.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoRepeatCount", settings.General.AutoRepeatCount.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoContinueOn", settings.General.AutoContinueOn.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoContinueDelay", settings.General.AutoContinueDelay.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ReturnToStartAfterRepeat", settings.General.ReturnToStartAfterRepeat.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SyncListViewWithVideoWhilePlaying", settings.General.SyncListViewWithVideoWhilePlaying.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBackupSeconds", settings.General.AutoBackupSeconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBackupDeleteAfterMonths", settings.General.AutoBackupDeleteAfterMonths.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SpellChecker", settings.General.SpellChecker);
                textWriter.WriteElementString("AllowEditOfOriginalSubtitle", settings.General.AllowEditOfOriginalSubtitle.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("PromptDeleteLines", settings.General.PromptDeleteLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("Undocked", settings.General.Undocked.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("UndockedVideoPosition", settings.General.UndockedVideoPosition);
                textWriter.WriteElementString("UndockedVideoFullscreen", settings.General.UndockedVideoFullscreen.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("UndockedWaveformPosition", settings.General.UndockedWaveformPosition);
                textWriter.WriteElementString("UndockedVideoControlsPosition", settings.General.UndockedVideoControlsPosition);
                textWriter.WriteElementString("WaveformCenter", settings.General.WaveformCenter.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("WaveformAutoGenWhenOpeningVideo", settings.General.WaveformAutoGenWhenOpeningVideo.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("WaveformUpdateIntervalMs", settings.General.WaveformUpdateIntervalMs.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SmallDelayMilliseconds", settings.General.SmallDelayMilliseconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("LargeDelayMilliseconds", settings.General.LargeDelayMilliseconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowOriginalAsPreviewIfAvailable", settings.General.ShowOriginalAsPreviewIfAvailable.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("LastPacCodePage", settings.General.LastPacCodePage.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("OpenSubtitleExtraExtensions", settings.General.OpenSubtitleExtraExtensions);
                textWriter.WriteElementString("ListViewColumnsRememberSize", settings.General.ListViewColumnsRememberSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewNumberWidth", settings.General.ListViewNumberWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewStartWidth", settings.General.ListViewStartWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewEndWidth", settings.General.ListViewEndWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewDurationWidth", settings.General.ListViewDurationWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewCpsWidth", settings.General.ListViewCpsWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewWpmWidth", settings.General.ListViewWpmWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewGapWidth", settings.General.ListViewGapWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewActorWidth", settings.General.ListViewActorWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewRegionWidth", settings.General.ListViewRegionWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DirectShowDoubleLoad", settings.General.DirectShowDoubleLoad.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VlcWaveTranscodeSettings", settings.General.VlcWaveTranscodeSettings);
                textWriter.WriteElementString("VlcLocation", settings.General.VlcLocation);
                textWriter.WriteElementString("VlcLocationRelative", settings.General.VlcLocationRelative);
                textWriter.WriteElementString("MpvVideoOutputWindows", settings.General.MpvVideoOutputWindows);
                textWriter.WriteElementString("MpvVideoOutputLinux", settings.General.MpvVideoOutputLinux);
                textWriter.WriteElementString("MpvExtraOptions", settings.General.MpvExtraOptions);
                textWriter.WriteElementString("MpvLogging", settings.General.MpvLogging.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MpvHandlesPreviewText", settings.General.MpvHandlesPreviewText.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MpvPreviewTextPrimaryColor", ColorTranslator.ToHtml(settings.General.MpvPreviewTextPrimaryColor));
                textWriter.WriteElementString("MpvPreviewTextOutlineWidth", settings.General.MpvPreviewTextOutlineWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MpvPreviewTextShadowWidth", settings.General.MpvPreviewTextShadowWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MpvPreviewTextOpaqueBox", settings.General.MpvPreviewTextOpaqueBox.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MpvPreviewTextAlignment", settings.General.MpvPreviewTextAlignment);
                textWriter.WriteElementString("MpvPreviewTextMarginVertical", settings.General.MpvPreviewTextMarginVertical.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MpcHcLocation", settings.General.MpcHcLocation);
                textWriter.WriteElementString("MkvMergeLocation", settings.General.MkvMergeLocation);
                textWriter.WriteElementString("UseFFmpegForWaveExtraction", settings.General.UseFFmpegForWaveExtraction.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FFmpegLocation", settings.General.FFmpegLocation);
                textWriter.WriteElementString("FFmpegSceneThreshold", settings.General.FFmpegSceneThreshold);
                textWriter.WriteElementString("UseTimeFormatHHMMSSFF", settings.General.UseTimeFormatHHMMSSFF.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitBehavior", settings.General.SplitBehavior.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitRemovesDashes", settings.General.SplitRemovesDashes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ClearStatusBarAfterSeconds", settings.General.ClearStatusBarAfterSeconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("Company", settings.General.Company);
                textWriter.WriteElementString("MoveVideo100Or500MsPlaySmallSample", settings.General.MoveVideo100Or500MsPlaySmallSample.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DisableVideoAutoLoading", settings.General.DisableVideoAutoLoading.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AllowVolumeBoost", settings.General.AllowVolumeBoost.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("RightToLeftMode", settings.General.RightToLeftMode.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("LastSaveAsFormat", settings.General.LastSaveAsFormat);
                textWriter.WriteElementString("CheckForUpdates", settings.General.CheckForUpdates.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("LastCheckForUpdates", settings.General.LastCheckForUpdates.ToString("yyyy-MM-dd"));
                textWriter.WriteElementString("AutoSave", settings.General.AutoSave.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("PreviewAssaText", settings.General.PreviewAssaText);
                textWriter.WriteElementString("TagsInToggleHiTags", settings.General.TagsInToggleHiTags);
                textWriter.WriteElementString("ShowProgress", settings.General.ShowProgress.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowNegativeDurationInfoOnSave", settings.General.ShowNegativeDurationInfoOnSave.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowFormatRequiresUtf8Warning", settings.General.ShowFormatRequiresUtf8Warning.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DefaultVideoOffsetInMs", settings.General.DefaultVideoOffsetInMs.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DefaultVideoOffsetInMsList", settings.General.DefaultVideoOffsetInMsList);
                textWriter.WriteElementString("AutoSetVideoSmpteForTtml", settings.General.AutoSetVideoSmpteForTtml.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoSetVideoSmpteForTtmlPrompt", settings.General.AutoSetVideoSmpteForTtmlPrompt.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("TitleBarAsterisk", settings.General.TitleBarAsterisk);
                textWriter.WriteElementString("TitleBarFullFileName", settings.General.TitleBarFullFileName.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MeasurementConverterCloseOnInsert", settings.General.MeasurementConverterCloseOnInsert.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MeasurementConverterCategories", settings.General.MeasurementConverterCategories);
                textWriter.WriteElementString("SubtitleTextBoxMaxHeight", settings.General.SubtitleTextBoxMaxHeight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AllowLetterShortcutsInTextBox", settings.General.AllowLetterShortcutsInTextBox.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DarkThemeBackColor", settings.General.DarkThemeBackColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DarkThemeForeColor", settings.General.DarkThemeForeColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("UseDarkTheme", settings.General.UseDarkTheme.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DarkThemeShowListViewGridLines", settings.General.DarkThemeShowListViewGridLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ShowBetaStuff", settings.General.ShowBetaStuff.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("DebugTranslationSync", settings.General.DebugTranslationSync.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("NewEmptyDefaultMs", settings.General.NewEmptyDefaultMs.ToString(CultureInfo.InvariantCulture));

                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Tools", string.Empty);

                //textWriter.WriteStartElement("AssTagTemplates", string.Empty);
                //foreach (var template in settings.Tools.AssaTagTemplates)
                //{
                //    textWriter.WriteStartElement("Template");
                //    textWriter.WriteElementString("Tag", template.Tag);
                //    textWriter.WriteElementString("Hint", template.Hint);
                //    textWriter.WriteEndElement();
                //}
                //textWriter.WriteEndElement();

                textWriter.WriteElementString("StartSceneIndex", settings.Tools.StartSceneIndex.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("EndSceneIndex", settings.Tools.EndSceneIndex.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("VerifyPlaySeconds", settings.Tools.VerifyPlaySeconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FixShortDisplayTimesAllowMoveStartTime", settings.Tools.FixShortDisplayTimesAllowMoveStartTime.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("RemoveEmptyLinesBetweenText", settings.Tools.RemoveEmptyLinesBetweenText.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MusicSymbol", settings.Tools.MusicSymbol);
                textWriter.WriteElementString("MusicSymbolReplace", settings.Tools.MusicSymbolReplace);
                textWriter.WriteElementString("UnicodeSymbolsToInsert", settings.Tools.UnicodeSymbolsToInsert);
                textWriter.WriteElementString("SpellCheckAutoChangeNameCasing", settings.Tools.SpellCheckAutoChangeNameCasing.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SpellCheckAutoChangeNamesUseSuggestions", settings.Tools.SpellCheckAutoChangeNamesUseSuggestions.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SpellCheckOneLetterWords", settings.Tools.CheckOneLetterWords.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SpellCheckEnglishAllowInQuoteAsIng", settings.Tools.SpellCheckEnglishAllowInQuoteAsIng.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("RememberUseAlwaysList", settings.Tools.RememberUseAlwaysList.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("LiveSpellCheck", settings.Tools.LiveSpellCheck.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SpellCheckShowCompletedMessage", settings.Tools.SpellCheckShowCompletedMessage.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("OcrFixUseHardcodedRules", settings.Tools.OcrFixUseHardcodedRules.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("OcrBinaryImageCompareRgbThreshold", settings.Tools.OcrBinaryImageCompareRgbThreshold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("OcrTesseract4RgbThreshold", settings.Tools.OcrTesseract4RgbThreshold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("OcrAddLetterRow1", settings.Tools.OcrAddLetterRow1);
                textWriter.WriteElementString("OcrAddLetterRow2", settings.Tools.OcrAddLetterRow2);
                textWriter.WriteElementString("OcrTrainFonts", settings.Tools.OcrTrainFonts);
                textWriter.WriteElementString("OcrTrainMergedLetters", settings.Tools.OcrTrainMergedLetters);
                textWriter.WriteElementString("OcrTrainSrtFile", settings.Tools.OcrTrainSrtFile);
                textWriter.WriteElementString("OcrUseWordSplitList", settings.Tools.OcrUseWordSplitList.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BDOpenIn", settings.Tools.BDOpenIn);
                textWriter.WriteElementString("Interjections", settings.Tools.Interjections);
                textWriter.WriteElementString("MicrosoftBingApiId", settings.Tools.MicrosoftBingApiId);
                textWriter.WriteElementString("MicrosoftTranslatorApiKey", settings.Tools.MicrosoftTranslatorApiKey);
                textWriter.WriteElementString("MicrosoftTranslatorTokenEndpoint", settings.Tools.MicrosoftTranslatorTokenEndpoint);
                textWriter.WriteElementString("MicrosoftTranslatorCategory", settings.Tools.MicrosoftTranslatorCategory);
                textWriter.WriteElementString("GoogleApiV2Key", settings.Tools.GoogleApiV2Key);
                textWriter.WriteElementString("GoogleTranslateNoKeyWarningShow", settings.Tools.GoogleTranslateNoKeyWarningShow.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GoogleApiV1ChunkSize", settings.Tools.GoogleApiV1ChunkSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GoogleTranslateLastTargetLanguage", settings.Tools.GoogleTranslateLastTargetLanguage);
                textWriter.WriteElementString("TranslateAllowSplit", settings.Tools.TranslateAllowSplit.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("TranslateLastService", settings.Tools.TranslateLastService);
                textWriter.WriteElementString("TranslateMergeStrategy", settings.Tools.TranslateMergeStrategy);
                textWriter.WriteElementString("TranslateViaCopyPasteSeparator", settings.Tools.TranslateViaCopyPasteSeparator);
                textWriter.WriteElementString("TranslateViaCopyPasteMaxSize", settings.Tools.TranslateViaCopyPasteMaxSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("TranslateViaCopyPasteAutoCopyToClipboard", settings.Tools.TranslateViaCopyPasteAutoCopyToClipboard.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxColorDurationSmall", settings.Tools.ListViewSyntaxColorDurationSmall.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxColorDurationBig", settings.Tools.ListViewSyntaxColorDurationBig.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxColorLongLines", settings.Tools.ListViewSyntaxColorLongLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxColorWideLines", settings.Tools.ListViewSyntaxColorWideLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxMoreThanXLines", settings.Tools.ListViewSyntaxMoreThanXLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxColorOverlap", settings.Tools.ListViewSyntaxColorOverlap.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxColorGap", settings.Tools.ListViewSyntaxColorGap.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewSyntaxErrorColor", settings.Tools.ListViewSyntaxErrorColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewUnfocusedSelectedColor", settings.Tools.ListViewUnfocusedSelectedColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("Color1", ColorTranslator.ToHtml(settings.Tools.Color1));
                textWriter.WriteElementString("Color2", ColorTranslator.ToHtml(settings.Tools.Color2));
                textWriter.WriteElementString("Color3", ColorTranslator.ToHtml(settings.Tools.Color3));
                textWriter.WriteElementString("Color4", ColorTranslator.ToHtml(settings.Tools.Color4));
                textWriter.WriteElementString("ListViewShowColumnStartTime", settings.Tools.ListViewShowColumnStartTime.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnEndTime", settings.Tools.ListViewShowColumnEndTime.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnDuration", settings.Tools.ListViewShowColumnDuration.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnCharsPerSec", settings.Tools.ListViewShowColumnCharsPerSec.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnWordsPerMin", settings.Tools.ListViewShowColumnWordsPerMin.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnGap", settings.Tools.ListViewShowColumnGap.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnActor", settings.Tools.ListViewShowColumnActor.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewShowColumnRegion", settings.Tools.ListViewShowColumnRegion.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ListViewMultipleReplaceShowColumnRuleInfo", settings.Tools.ListViewMultipleReplaceShowColumnRuleInfo.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitAdvanced", settings.Tools.SplitAdvanced.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitOutputFolder", settings.Tools.SplitOutputFolder);
                textWriter.WriteElementString("SplitNumberOfParts", settings.Tools.SplitNumberOfParts.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("SplitVia", settings.Tools.SplitVia);
                textWriter.WriteElementString("JoinCorrectTimeCodes", settings.Tools.JoinCorrectTimeCodes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("JoinAddMs", settings.Tools.JoinAddMs.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("NewEmptyTranslationText", settings.Tools.NewEmptyTranslationText);
                textWriter.WriteElementString("BatchConvertOutputFolder", settings.Tools.BatchConvertOutputFolder);
                textWriter.WriteElementString("BatchConvertOverwriteExisting", settings.Tools.BatchConvertOverwriteExisting.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertSaveInSourceFolder", settings.Tools.BatchConvertSaveInSourceFolder.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertRemoveFormatting", settings.Tools.BatchConvertRemoveFormatting.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertRemoveStyle", settings.Tools.BatchConvertRemoveStyle.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertBridgeGaps", settings.Tools.BatchConvertBridgeGaps.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertFixCasing", settings.Tools.BatchConvertFixCasing.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertRemoveTextForHI", settings.Tools.BatchConvertRemoveTextForHI.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertSplitLongLines", settings.Tools.BatchConvertSplitLongLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertFixCommonErrors", settings.Tools.BatchConvertFixCommonErrors.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertMultipleReplace", settings.Tools.BatchConvertMultipleReplace.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertFixRtl", settings.Tools.BatchConvertFixRtl.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertFixRtlMode", settings.Tools.BatchConvertFixRtlMode);
                textWriter.WriteElementString("BatchConvertAutoBalance", settings.Tools.BatchConvertAutoBalance.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertSetMinDisplayTimeBetweenSubtitles", settings.Tools.BatchConvertSetMinDisplayTimeBetweenSubtitles.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertMergeShortLines", settings.Tools.BatchConvertMergeShortLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertRemoveLineBreaks", settings.Tools.BatchConvertRemoveLineBreaks.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertMergeSameText", settings.Tools.BatchConvertMergeSameText.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertMergeSameTimeCodes", settings.Tools.BatchConvertMergeSameTimeCodes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertChangeSpeed", settings.Tools.BatchConvertChangeSpeed.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertAdjustDisplayDuration", settings.Tools.BatchConvertAdjustDisplayDuration.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertApplyDurationLimits", settings.Tools.BatchConvertApplyDurationLimits.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertDeleteLines", settings.Tools.BatchConvertDeleteLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertAssaChangeRes", settings.Tools.BatchConvertAssaChangeRes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertChangeFrameRate", settings.Tools.BatchConvertChangeFrameRate.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertOffsetTimeCodes", settings.Tools.BatchConvertOffsetTimeCodes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertLanguage", settings.Tools.BatchConvertLanguage);
                textWriter.WriteElementString("BatchConvertFormat", settings.Tools.BatchConvertFormat);
                textWriter.WriteElementString("BatchConvertAssStyles", settings.Tools.BatchConvertAssStyles);
                textWriter.WriteElementString("BatchConvertSsaStyles", settings.Tools.BatchConvertSsaStyles);
                textWriter.WriteElementString("BatchConvertUseStyleFromSource", settings.Tools.BatchConvertUseStyleFromSource.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertExportCustomTextTemplate", settings.Tools.BatchConvertExportCustomTextTemplate);
                textWriter.WriteElementString("BatchConvertTsOverrideXPosition", settings.Tools.BatchConvertTsOverrideXPosition.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsOverrideYPosition", settings.Tools.BatchConvertTsOverrideYPosition.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsOverrideBottomMargin", settings.Tools.BatchConvertTsOverrideBottomMargin.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsOverrideHAlign", settings.Tools.BatchConvertTsOverrideHAlign);
                textWriter.WriteElementString("BatchConvertTsOverrideHMargin", settings.Tools.BatchConvertTsOverrideHMargin.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsOverrideScreenSize", settings.Tools.BatchConvertTsOverrideScreenSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsScreenWidth", settings.Tools.BatchConvertTsScreenWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsScreenHeight", settings.Tools.BatchConvertTsScreenHeight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsOnlyTeletext", settings.Tools.BatchConvertTsOnlyTeletext.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BatchConvertTsFileNameAppend", settings.Tools.BatchConvertTsFileNameAppend);
                textWriter.WriteElementString("BatchConvertMkvLanguageCodeStyle", settings.Tools.BatchConvertMkvLanguageCodeStyle);
                textWriter.WriteElementString("WaveformBatchLastFolder", settings.Tools.WaveformBatchLastFolder);
                textWriter.WriteElementString("ModifySelectionRule", settings.Tools.ModifySelectionRule);
                textWriter.WriteElementString("ModifySelectionText", settings.Tools.ModifySelectionText);
                textWriter.WriteElementString("ModifySelectionCaseSensitive", settings.Tools.ModifySelectionCaseSensitive.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportVobSubFontName", settings.Tools.ExportVobSubFontName);
                textWriter.WriteElementString("ExportVobSubFontSize", settings.Tools.ExportVobSubFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportVobSubVideoResolution", settings.Tools.ExportVobSubVideoResolution);
                textWriter.WriteElementString("ExportVobSubLanguage", settings.Tools.ExportVobSubLanguage);
                textWriter.WriteElementString("ExportVobSubSimpleRendering", settings.Tools.ExportVobSubSimpleRendering.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportVobAntiAliasingWithTransparency", settings.Tools.ExportVobAntiAliasingWithTransparency.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBluRayFontName", settings.Tools.ExportBluRayFontName);
                textWriter.WriteElementString("ExportBluRayFontSize", settings.Tools.ExportBluRayFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportFcpFontName", settings.Tools.ExportFcpFontName);
                textWriter.WriteElementString("ExportFontNameOther", settings.Tools.ExportFontNameOther);
                textWriter.WriteElementString("ExportFcpFontSize", settings.Tools.ExportFcpFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportFcpImageType", settings.Tools.ExportFcpImageType);
                textWriter.WriteElementString("ExportFcpPalNtsc", settings.Tools.ExportFcpPalNtsc);
                textWriter.WriteElementString("ExportBdnXmlImageType", settings.Tools.ExportBdnXmlImageType);
                textWriter.WriteElementString("ExportLastFontSize", settings.Tools.ExportLastFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLastLineHeight", settings.Tools.ExportLastLineHeight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLastBorderWidth", settings.Tools.ExportLastBorderWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLastFontBold", settings.Tools.ExportLastFontBold.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBluRayVideoResolution", settings.Tools.ExportBluRayVideoResolution);
                textWriter.WriteElementString("ExportFcpVideoResolution", settings.Tools.ExportFcpVideoResolution);
                textWriter.WriteElementString("ExportFontColor", settings.Tools.ExportFontColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBorderColor", settings.Tools.ExportBorderColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportShadowColor", settings.Tools.ExportShadowColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBoxBorderSize", settings.Tools.ExportBoxBorderSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBottomMarginUnit", settings.Tools.ExportBottomMarginUnit);
                textWriter.WriteElementString("ExportBottomMarginPercent", settings.Tools.ExportBottomMarginPercent.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBottomMarginPixels", settings.Tools.ExportBottomMarginPixels.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLeftRightMarginUnit", settings.Tools.ExportLeftRightMarginUnit);
                textWriter.WriteElementString("ExportLeftRightMarginPercent", settings.Tools.ExportLeftRightMarginPercent.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLeftRightMarginPixels", settings.Tools.ExportLeftRightMarginPixels.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportHorizontalAlignment", settings.Tools.ExportHorizontalAlignment.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBluRayBottomMarginPercent", settings.Tools.ExportBluRayBottomMarginPercent.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBluRayBottomMarginPixels", settings.Tools.ExportBluRayBottomMarginPixels.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBluRayShadow", settings.Tools.ExportBluRayShadow.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportBluRayRemoveSmallGaps", settings.Tools.ExportBluRayRemoveSmallGaps.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportCdgBackgroundImage", settings.Tools.ExportCdgBackgroundImage);
                textWriter.WriteElementString("ExportCdgMarginLeft", settings.Tools.ExportCdgMarginLeft.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportCdgMarginBottom", settings.Tools.ExportCdgMarginBottom.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportCdgFormat", settings.Tools.ExportCdgFormat);
                textWriter.WriteElementString("Export3DType", settings.Tools.Export3DType.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("Export3DDepth", settings.Tools.Export3DDepth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLastShadowTransparency", settings.Tools.ExportLastShadowTransparency.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportLastFrameRate", settings.Tools.ExportLastFrameRate.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportFullFrame", settings.Tools.ExportFullFrame.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportFcpFullPathUrl", settings.Tools.ExportFcpFullPathUrl.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportPenLineJoin", settings.Tools.ExportPenLineJoin);
                textWriter.WriteElementString("FixCommonErrorsFixOverlapAllowEqualEndStart", settings.Tools.FixCommonErrorsFixOverlapAllowEqualEndStart.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("FixCommonErrorsSkipStepOne", settings.Tools.FixCommonErrorsSkipStepOne.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextSplitting", settings.Tools.ImportTextSplitting);
                textWriter.WriteElementString("ImportTextMergeShortLines", settings.Tools.ImportTextMergeShortLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextLineBreak", settings.Tools.ImportTextLineBreak);
                textWriter.WriteElementString("ImportTextRemoveEmptyLines", settings.Tools.ImportTextRemoveEmptyLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextAutoSplitAtBlank", settings.Tools.ImportTextAutoSplitAtBlank.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextRemoveLinesNoLetters", settings.Tools.ImportTextRemoveLinesNoLetters.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextGenerateTimeCodes", settings.Tools.ImportTextGenerateTimeCodes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextTakeTimeCodeFromFileName", settings.Tools.ImportTextTakeTimeCodeFromFileName.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextAutoBreak", settings.Tools.ImportTextAutoBreak.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextAutoBreakAtEnd", settings.Tools.ImportTextAutoBreakAtEnd.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextGap", settings.Tools.ImportTextGap.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextAutoSplitNumberOfLines", settings.Tools.ImportTextAutoSplitNumberOfLines.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextAutoBreakAtEndMarkerText", settings.Tools.ImportTextAutoBreakAtEndMarkerText);
                textWriter.WriteElementString("ImportTextDurationAuto", settings.Tools.ImportTextDurationAuto.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ImportTextFixedDuration", settings.Tools.ImportTextFixedDuration.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GenerateTimeCodePatterns", settings.Tools.GenerateTimeCodePatterns);
                textWriter.WriteElementString("MusicSymbolStyle", settings.Tools.MusicSymbolStyle);
                textWriter.WriteElementString("BinEditBackgroundColor", settings.Tools.BinEditBackgroundColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BinEditImageBackgroundColor", settings.Tools.BinEditImageBackgroundColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BinEditVerticalMargin", settings.Tools.BinEditVerticalMargin.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BinEditLeftMargin", settings.Tools.BinEditLeftMargin.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BinEditRightMargin", settings.Tools.BinEditRightMargin.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BridgeGapMilliseconds", settings.Tools.BridgeGapMilliseconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportCustomTemplates", settings.Tools.ExportCustomTemplates);
                textWriter.WriteElementString("ChangeCasingChoice", settings.Tools.ChangeCasingChoice);
                textWriter.WriteElementString("UseNoLineBreakAfter", settings.Tools.UseNoLineBreakAfter.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("NoLineBreakAfterEnglish", settings.Tools.NoLineBreakAfterEnglish);
                textWriter.WriteElementString("ExportTextFormatText", settings.Tools.ExportTextFormatText);
                textWriter.WriteElementString("ExportTextRemoveStyling", settings.Tools.ExportTextRemoveStyling.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextShowLineNumbers", settings.Tools.ExportTextShowLineNumbers.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextShowLineNumbersNewLine", settings.Tools.ExportTextShowLineNumbersNewLine.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextShowTimeCodes", settings.Tools.ExportTextShowTimeCodes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextShowTimeCodesNewLine", settings.Tools.ExportTextShowTimeCodesNewLine.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextNewLineAfterText", settings.Tools.ExportTextNewLineAfterText.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextNewLineBetweenSubtitles", settings.Tools.ExportTextNewLineBetweenSubtitles.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ExportTextTimeCodeFormat", settings.Tools.ExportTextTimeCodeFormat);
                textWriter.WriteElementString("ExportTextTimeCodeSeparator", settings.Tools.ExportTextTimeCodeSeparator);
                textWriter.WriteElementString("VideoOffsetKeepTimeCodes", settings.Tools.VideoOffsetKeepTimeCodes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MoveStartEndMs", settings.Tools.MoveStartEndMs.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AdjustDurationSeconds", settings.Tools.AdjustDurationSeconds.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AdjustDurationPercent", settings.Tools.AdjustDurationPercent.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AdjustDurationLast", settings.Tools.AdjustDurationLast);
                textWriter.WriteElementString("AdjustDurationExtendOnly", settings.Tools.AdjustDurationExtendOnly.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBreakCommaBreakEarly", settings.Tools.AutoBreakCommaBreakEarly.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBreakDashEarly", settings.Tools.AutoBreakDashEarly.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBreakLineEndingEarly", settings.Tools.AutoBreakLineEndingEarly.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBreakUsePixelWidth", settings.Tools.AutoBreakUsePixelWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBreakPreferBottomHeavy", settings.Tools.AutoBreakPreferBottomHeavy.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AutoBreakPreferBottomPercent", settings.Tools.AutoBreakPreferBottomPercent.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ApplyMinimumDurationLimit", settings.Tools.ApplyMinimumDurationLimit.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ApplyMaximumDurationLimit", settings.Tools.ApplyMaximumDurationLimit.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MergeShortLinesMaxGap", settings.Tools.MergeShortLinesMaxGap.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MergeShortLinesMaxChars", settings.Tools.MergeShortLinesMaxChars.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("MergeShortLinesOnlyContinuous", settings.Tools.MergeShortLinesOnlyContinuous.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("ColumnPasteColumn", settings.Tools.ColumnPasteColumn);
                textWriter.WriteElementString("ColumnPasteOverwriteMode", settings.Tools.ColumnPasteOverwriteMode);
                textWriter.WriteElementString("AssaAttachmentFontTextPreview", settings.Tools.AssaAttachmentFontTextPreview);
                textWriter.WriteElementString("AssaSetPositionTarget", settings.Tools.AssaSetPositionTarget);
                textWriter.WriteElementString("VisualSyncStartSize", settings.Tools.VisualSyncStartSize);
                textWriter.WriteElementString("BlankVideoColor", ColorTranslator.ToHtml(settings.Tools.BlankVideoColor));
                textWriter.WriteElementString("BlankVideoUseCheckeredImage", settings.Tools.BlankVideoUseCheckeredImage.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BlankVideoMinutes", settings.Tools.BlankVideoMinutes.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("BlankVideoFrameRate", settings.Tools.BlankVideoFrameRate.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AssaProgressBarBackColor", ColorTranslator.ToHtml(settings.Tools.AssaProgressBarBackColor));
                textWriter.WriteElementString("AssaProgressBarForeColor", ColorTranslator.ToHtml(settings.Tools.AssaProgressBarForeColor));
                textWriter.WriteElementString("AssaProgressBarTextColor", ColorTranslator.ToHtml(settings.Tools.AssaProgressBarTextColor));
                textWriter.WriteElementString("AssaProgressBarHeight", settings.Tools.AssaProgressBarHeight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AssaProgressBarSplitterWidth", settings.Tools.AssaProgressBarSplitterWidth.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AssaProgressBarSplitterHeight", settings.Tools.AssaProgressBarSplitterHeight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AssaProgressBarFontName", settings.Tools.AssaProgressBarFontName);
                textWriter.WriteElementString("AssaProgressBarFontSize", settings.Tools.AssaProgressBarFontSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AssaProgressBarTopAlign", settings.Tools.AssaProgressBarTopAlign.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("AssaProgressBarTextAlign", settings.Tools.AssaProgressBarTextAlign);
                textWriter.WriteElementString("GenVideoEncoding", settings.Tools.GenVideoEncoding);
                textWriter.WriteElementString("GenVideoPreset", settings.Tools.GenVideoPreset);
                textWriter.WriteElementString("GenVideoCrf", settings.Tools.GenVideoCrf);
                textWriter.WriteElementString("GenVideoTune", settings.Tools.GenVideoTune);
                textWriter.WriteElementString("GenVideoAudioEncoding", settings.Tools.GenVideoAudioEncoding);
                textWriter.WriteElementString("GenVideoAudioForceStereo", settings.Tools.GenVideoAudioForceStereo.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GenVideoAudioSampleRate", settings.Tools.GenVideoAudioSampleRate);
                textWriter.WriteElementString("GenVideoTargetFileSize", settings.Tools.GenVideoTargetFileSize.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GenVideoFontSizePercentOfHeight", settings.Tools.GenVideoFontSizePercentOfHeight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GenVideoNonAssaBox", settings.Tools.GenVideoNonAssaBox.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GenVideoNonAssaAlignRight", settings.Tools.GenVideoNonAssaAlignRight.ToString(CultureInfo.InvariantCulture));
                textWriter.WriteElementString("GenVideoNonAssaFixRtlUnicode", settings.Tools.GenVideoNonAssaFixRtlUnicode.ToString(CultureInfo.InvariantCulture));

                if (settings.Tools.FindHistory != null && settings.Tools.FindHistory.Count > 0)
                {
                    const int maximumFindHistoryItems = 10;
                    textWriter.WriteStartElement("FindHistory", string.Empty);
                    int maxIndex = settings.Tools.FindHistory.Count;
                    if (maxIndex > maximumFindHistoryItems)
                    {
                        maxIndex = maximumFindHistoryItems;
                    }

                    for (int index = 0; index < maxIndex; index++)
                    {
                        var text = settings.Tools.FindHistory[index];
                        textWriter.WriteElementString("Text", text);
                    }
                    textWriter.WriteEndElement();
                }
                textWriter.WriteEndElement();

                //textWriter.WriteStartElement("SubtitleSettings", string.Empty);
                //textWriter.WriteStartElement("AssaStyleStorageCategories", string.Empty);
                //foreach (var category in settings.SubtitleSettings.AssaStyleStorageCategories)
                //{
                //    if (!string.IsNullOrEmpty(category?.Name))
                //    {
                //        textWriter.WriteStartElement("Category", string.Empty);
                //        textWriter.WriteElementString("Name", category.Name);
                //        textWriter.WriteElementString("IsDefault", category.IsDefault.ToString(CultureInfo.InvariantCulture));
                //        foreach (var style in category.Styles)
                //        {
                //            textWriter.WriteStartElement("Style");
                //            textWriter.WriteElementString("Name", style.Name);
                //            textWriter.WriteElementString("FontName", style.FontName);
                //            textWriter.WriteElementString("FontSize", style.FontSize.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Bold", style.Bold.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Italic", style.Italic.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Underline", style.Underline.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("StrikeOut", style.Strikeout.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Primary", style.Primary.ToArgb().ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Secondary", style.Secondary.ToArgb().ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Outline", style.Outline.ToArgb().ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Background", style.Background.ToArgb().ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("ShadowWidth", style.ShadowWidth.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("OutlineWidth", style.OutlineWidth.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Alignment", style.Alignment);
                //            textWriter.WriteElementString("MarginLeft", style.MarginLeft.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("MarginRight", style.MarginRight.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("MarginVertical", style.MarginVertical.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("BorderStyle", style.BorderStyle);
                //            textWriter.WriteElementString("ScaleX", style.ScaleX.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("ScaleY", style.ScaleY.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Spacing", style.Spacing.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("Angle", style.Angle.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteEndElement();
                //        }
                //        textWriter.WriteEndElement();
                //    }
                //}
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("AssaApplyOverrideTags", string.Empty);
                //foreach (var tag in settings.SubtitleSettings.AssaOverrideTagHistory)
                //{
                //    textWriter.WriteElementString("Tag", tag);
                //}
                //textWriter.WriteEndElement();

                //textWriter.WriteElementString("AssaResolutionAutoNew", settings.SubtitleSettings.AssaResolutionAutoNew.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("AssaResolutionPromptChange", settings.SubtitleSettings.AssaResolutionPromptChange.ToString(CultureInfo.InvariantCulture));

                //textWriter.WriteElementString("DCinemaFontFile", settings.SubtitleSettings.DCinemaFontFile);
                //textWriter.WriteElementString("DCinemaFontSize", settings.SubtitleSettings.DCinemaFontSize.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DCinemaBottomMargin", settings.SubtitleSettings.DCinemaBottomMargin.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DCinemaZPosition", settings.SubtitleSettings.DCinemaZPosition.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DCinemaFadeUpTime", settings.SubtitleSettings.DCinemaFadeUpTime.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DCinemaFadeDownTime", settings.SubtitleSettings.DCinemaFadeDownTime.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DCinemaAutoGenerateSubtitleId", settings.SubtitleSettings.DCinemaAutoGenerateSubtitleId.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("SamiDisplayTwoClassesAsTwoSubtitles", settings.SubtitleSettings.SamiDisplayTwoClassesAsTwoSubtitles.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("SamiHtmlEncodeMode", settings.SubtitleSettings.SamiHtmlEncodeMode.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TimedText10TimeCodeFormat", settings.SubtitleSettings.TimedText10TimeCodeFormat);
                //textWriter.WriteElementString("TimedText10ShowStyleAndLanguage", settings.SubtitleSettings.TimedText10ShowStyleAndLanguage.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TimedText10FileExtension", settings.SubtitleSettings.TimedText10FileExtension);
                //textWriter.WriteElementString("TimedTextItunesTopOrigin", settings.SubtitleSettings.TimedTextItunesTopOrigin);
                //textWriter.WriteElementString("TimedTextItunesTopExtent", settings.SubtitleSettings.TimedTextItunesTopExtent);
                //textWriter.WriteElementString("TimedTextItunesBottomOrigin", settings.SubtitleSettings.TimedTextItunesBottomOrigin);
                //textWriter.WriteElementString("TimedTextItunesBottomExtent", settings.SubtitleSettings.TimedTextItunesBottomExtent);
                //textWriter.WriteElementString("FcpFontSize", settings.SubtitleSettings.FcpFontSize.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FcpFontName", settings.SubtitleSettings.FcpFontName);
                //textWriter.WriteElementString("Cavena890StartOfMessage", settings.SubtitleSettings.Cavena890StartOfMessage);
                //textWriter.WriteElementString("EbuStlTeletextUseBox", settings.SubtitleSettings.EbuStlTeletextUseBox.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("EbuStlTeletextUseDoubleHeight", settings.SubtitleSettings.EbuStlTeletextUseDoubleHeight.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("EbuStlMarginTop", settings.SubtitleSettings.EbuStlMarginTop.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("EbuStlMarginBottom", settings.SubtitleSettings.EbuStlMarginBottom.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("EbuStlNewLineRows", settings.SubtitleSettings.EbuStlNewLineRows.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("PacVerticalTop", settings.SubtitleSettings.PacVerticalTop.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("PacVerticalCenter", settings.SubtitleSettings.PacVerticalCenter.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("PacVerticalBottom", settings.SubtitleSettings.PacVerticalBottom.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DvdStudioProHeader", settings.SubtitleSettings.DvdStudioProHeader.TrimEnd() + Environment.NewLine);
                //textWriter.WriteElementString("TmpegEncXmlFontName", settings.SubtitleSettings.TmpegEncXmlFontName.TrimEnd());
                //textWriter.WriteElementString("TmpegEncXmlFontHeight", settings.SubtitleSettings.TmpegEncXmlFontHeight.TrimEnd());
                //textWriter.WriteElementString("TmpegEncXmlPosition", settings.SubtitleSettings.TmpegEncXmlPosition.TrimEnd());
                //textWriter.WriteElementString("CheetahCaptionAlwayWriteEndTime", settings.SubtitleSettings.CheetahCaptionAlwayWriteEndTime.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("NuendoCharacterListFile", settings.SubtitleSettings.NuendoCharacterListFile);
                //textWriter.WriteElementString("WebVttTimescale", settings.SubtitleSettings.WebVttTimescale.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WebVttCueAn1", settings.SubtitleSettings.WebVttCueAn1);
                //textWriter.WriteElementString("WebVttCueAn2", settings.SubtitleSettings.WebVttCueAn2);
                //textWriter.WriteElementString("WebVttCueAn3", settings.SubtitleSettings.WebVttCueAn3);
                //textWriter.WriteElementString("WebVttCueAn4", settings.SubtitleSettings.WebVttCueAn4);
                //textWriter.WriteElementString("WebVttCueAn5", settings.SubtitleSettings.WebVttCueAn5);
                //textWriter.WriteElementString("WebVttCueAn6", settings.SubtitleSettings.WebVttCueAn6);
                //textWriter.WriteElementString("WebVttCueAn7", settings.SubtitleSettings.WebVttCueAn7);
                //textWriter.WriteElementString("WebVttCueAn8", settings.SubtitleSettings.WebVttCueAn8);
                //textWriter.WriteElementString("WebVttCueAn9", settings.SubtitleSettings.WebVttCueAn9);
                //textWriter.WriteElementString("MPlayer2Extension", settings.SubtitleSettings.MPlayer2Extension);
                //textWriter.WriteElementString("TeletextItalicFix", settings.SubtitleSettings.TeletextItalicFix.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("MccDebug", settings.SubtitleSettings.MccDebug.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WebVttUseXTimestampMap", settings.SubtitleSettings.WebVttUseXTimestampMap.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("Proxy", string.Empty);
                //textWriter.WriteElementString("ProxyAddress", settings.Proxy.ProxyAddress);
                //textWriter.WriteElementString("UserName", settings.Proxy.UserName);
                //textWriter.WriteElementString("Password", settings.Proxy.Password);
                //textWriter.WriteElementString("Domain", settings.Proxy.Domain);
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("WordLists", string.Empty);
                //textWriter.WriteElementString("LastLanguage", settings.WordLists.LastLanguage);
                //textWriter.WriteElementString("Names", settings.WordLists.NamesUrl);
                //textWriter.WriteElementString("UseOnlineNames", settings.WordLists.UseOnlineNames.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("FcpExportSettings", string.Empty);
                //textWriter.WriteElementString("FontName", settings.FcpExportSettings.FontName);
                //textWriter.WriteElementString("FontSize", settings.FcpExportSettings.FontSize.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("Alignment", settings.FcpExportSettings.Alignment);
                //textWriter.WriteElementString("Baseline", settings.FcpExportSettings.Baseline.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("Color", settings.FcpExportSettings.Color.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("CommonErrors", string.Empty);
                //textWriter.WriteElementString("StartPosition", settings.CommonErrors.StartPosition);
                //textWriter.WriteElementString("StartSize", settings.CommonErrors.StartSize);
                //textWriter.WriteElementString("EmptyLinesTicked", settings.CommonErrors.EmptyLinesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("OverlappingDisplayTimeTicked", settings.CommonErrors.OverlappingDisplayTimeTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TooShortDisplayTimeTicked", settings.CommonErrors.TooShortDisplayTimeTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TooLongDisplayTimeTicked", settings.CommonErrors.TooLongDisplayTimeTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TooShortGapTicked", settings.CommonErrors.TooShortGapTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("InvalidItalicTagsTicked", settings.CommonErrors.InvalidItalicTagsTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("BreakLongLinesTicked", settings.CommonErrors.BreakLongLinesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("MergeShortLinesTicked", settings.CommonErrors.MergeShortLinesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("MergeShortLinesAllTicked", settings.CommonErrors.MergeShortLinesAllTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UnneededSpacesTicked", settings.CommonErrors.UnneededSpacesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UnneededPeriodsTicked", settings.CommonErrors.UnneededPeriodsTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixCommasTicked", settings.CommonErrors.FixCommasTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("MissingSpacesTicked", settings.CommonErrors.MissingSpacesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("AddMissingQuotesTicked", settings.CommonErrors.AddMissingQuotesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("Fix3PlusLinesTicked", settings.CommonErrors.Fix3PlusLinesTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixHyphensTicked", settings.CommonErrors.FixHyphensTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixHyphensRemoveSingleLineTicked", settings.CommonErrors.FixHyphensRemoveSingleLineTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UppercaseIInsideLowercaseWordTicked", settings.CommonErrors.UppercaseIInsideLowercaseWordTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DoubleApostropheToQuoteTicked", settings.CommonErrors.DoubleApostropheToQuoteTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("AddPeriodAfterParagraphTicked", settings.CommonErrors.AddPeriodAfterParagraphTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("StartWithUppercaseLetterAfterParagraphTicked", settings.CommonErrors.StartWithUppercaseLetterAfterParagraphTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("StartWithUppercaseLetterAfterPeriodInsideParagraphTicked", settings.CommonErrors.StartWithUppercaseLetterAfterPeriodInsideParagraphTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("StartWithUppercaseLetterAfterColonTicked", settings.CommonErrors.StartWithUppercaseLetterAfterColonTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("AloneLowercaseIToUppercaseIEnglishTicked", settings.CommonErrors.AloneLowercaseIToUppercaseIEnglishTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixOcrErrorsViaReplaceListTicked", settings.CommonErrors.FixOcrErrorsViaReplaceListTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveSpaceBetweenNumberTicked", settings.CommonErrors.RemoveSpaceBetweenNumberTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixDialogsOnOneLineTicked", settings.CommonErrors.FixDialogsOnOneLineTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveDialogFirstLineInNonDialogs", settings.CommonErrors.RemoveDialogFirstLineInNonDialogs.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TurkishAnsiTicked", settings.CommonErrors.TurkishAnsiTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DanishLetterITicked", settings.CommonErrors.DanishLetterITicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("SpanishInvertedQuestionAndExclamationMarksTicked", settings.CommonErrors.SpanishInvertedQuestionAndExclamationMarksTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixDoubleDashTicked", settings.CommonErrors.FixDoubleDashTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixDoubleGreaterThanTicked", settings.CommonErrors.FixDoubleGreaterThanTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixEllipsesStartTicked", settings.CommonErrors.FixEllipsesStartTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixMissingOpenBracketTicked", settings.CommonErrors.FixMissingOpenBracketTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixMusicNotationTicked", settings.CommonErrors.FixMusicNotationTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixContinuationStyleTicked", settings.CommonErrors.FixContinuationStyleTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixUnnecessaryLeadingDotsTicked", settings.CommonErrors.FixUnnecessaryLeadingDotsTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("NormalizeStringsTicked", settings.CommonErrors.NormalizeStringsTicked.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DefaultFixes", settings.CommonErrors.DefaultFixes);
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("VideoControls", string.Empty);
                //textWriter.WriteElementString("CustomSearchText1", settings.VideoControls.CustomSearchText1);
                //textWriter.WriteElementString("CustomSearchText2", settings.VideoControls.CustomSearchText2);
                //textWriter.WriteElementString("CustomSearchText3", settings.VideoControls.CustomSearchText3);
                //textWriter.WriteElementString("CustomSearchText4", settings.VideoControls.CustomSearchText4);
                //textWriter.WriteElementString("CustomSearchText5", settings.VideoControls.CustomSearchText5);
                //textWriter.WriteElementString("CustomSearchUrl1", settings.VideoControls.CustomSearchUrl1);
                //textWriter.WriteElementString("CustomSearchUrl2", settings.VideoControls.CustomSearchUrl2);
                //textWriter.WriteElementString("CustomSearchUrl3", settings.VideoControls.CustomSearchUrl3);
                //textWriter.WriteElementString("CustomSearchUrl4", settings.VideoControls.CustomSearchUrl4);
                //textWriter.WriteElementString("CustomSearchUrl5", settings.VideoControls.CustomSearchUrl5);
                //textWriter.WriteElementString("LastActiveTab", settings.VideoControls.LastActiveTab);
                //textWriter.WriteElementString("WaveformDrawGrid", settings.VideoControls.WaveformDrawGrid.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformDrawCps", settings.VideoControls.WaveformDrawCps.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformDrawWpm", settings.VideoControls.WaveformDrawWpm.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformAllowOverlap", settings.VideoControls.WaveformAllowOverlap.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformFocusOnMouseEnter", settings.VideoControls.WaveformFocusOnMouseEnter.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformListViewFocusOnMouseEnter", settings.VideoControls.WaveformListViewFocusOnMouseEnter.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformSetVideoPositionOnMoveStartEnd", settings.VideoControls.WaveformSetVideoPositionOnMoveStartEnd.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformSingleClickSelect", settings.VideoControls.WaveformSingleClickSelect.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformSnapToSceneChanges", settings.VideoControls.WaveformSnapToSceneChanges.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformBorderHitMs", settings.VideoControls.WaveformBorderHitMs.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformGridColor", settings.VideoControls.WaveformGridColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformColor", settings.VideoControls.WaveformColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformSelectedColor", settings.VideoControls.WaveformSelectedColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformBackgroundColor", settings.VideoControls.WaveformBackgroundColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformTextColor", settings.VideoControls.WaveformTextColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformCursorColor", settings.VideoControls.WaveformCursorColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformChaptersColor", settings.VideoControls.WaveformChaptersColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformTextSize", settings.VideoControls.WaveformTextSize.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformTextBold", settings.VideoControls.WaveformTextBold.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformDoubleClickOnNonParagraphAction", settings.VideoControls.WaveformDoubleClickOnNonParagraphAction);
                //textWriter.WriteElementString("WaveformRightClickOnNonParagraphAction", settings.VideoControls.WaveformRightClickOnNonParagraphAction);
                //textWriter.WriteElementString("WaveformMouseWheelScrollUpIsForward", settings.VideoControls.WaveformMouseWheelScrollUpIsForward.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("GenerateSpectrogram", settings.VideoControls.GenerateSpectrogram.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("SpectrogramAppearance", settings.VideoControls.SpectrogramAppearance);
                //textWriter.WriteElementString("WaveformMinimumSampleRate", settings.VideoControls.WaveformMinimumSampleRate.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformSeeksSilenceDurationSeconds", settings.VideoControls.WaveformSeeksSilenceDurationSeconds.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformSeeksSilenceMaxVolume", settings.VideoControls.WaveformSeeksSilenceMaxVolume.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformUnwrapText", settings.VideoControls.WaveformUnwrapText.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("WaveformHideWpmCpsLabels", settings.VideoControls.WaveformHideWpmCpsLabels.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("NetworkSettings", string.Empty);
                //textWriter.WriteElementString("SessionKey", settings.NetworkSettings.SessionKey);
                //textWriter.WriteElementString("UserName", settings.NetworkSettings.UserName);
                //textWriter.WriteElementString("WebApiUrl", settings.NetworkSettings.WebApiUrl);
                //textWriter.WriteElementString("PollIntervalSeconds", settings.NetworkSettings.PollIntervalSeconds.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("NewMessageSound", settings.NetworkSettings.NewMessageSound);
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("VobSubOcr", string.Empty);
                //textWriter.WriteElementString("XOrMorePixelsMakesSpace", settings.VobSubOcr.XOrMorePixelsMakesSpace.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("AllowDifferenceInPercent", settings.VobSubOcr.AllowDifferenceInPercent.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("BlurayAllowDifferenceInPercent", settings.VobSubOcr.BlurayAllowDifferenceInPercent.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LastImageCompareFolder", settings.VobSubOcr.LastImageCompareFolder);
                //textWriter.WriteElementString("LastModiLanguageId", settings.VobSubOcr.LastModiLanguageId.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LastOcrMethod", settings.VobSubOcr.LastOcrMethod);
                //textWriter.WriteElementString("TesseractLastLanguage", settings.VobSubOcr.TesseractLastLanguage);
                //textWriter.WriteElementString("UseTesseractFallback", settings.VobSubOcr.UseTesseractFallback.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UseItalicsInTesseract", settings.VobSubOcr.UseItalicsInTesseract.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TesseractEngineMode", settings.VobSubOcr.TesseractEngineMode.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UseMusicSymbolsInTesseract", settings.VobSubOcr.UseMusicSymbolsInTesseract.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RightToLeft", settings.VobSubOcr.RightToLeft.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("TopToBottom", settings.VobSubOcr.TopToBottom.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("DefaultMillisecondsForUnknownDurations", settings.VobSubOcr.DefaultMillisecondsForUnknownDurations.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FixOcrErrors", settings.VobSubOcr.FixOcrErrors.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("PromptForUnknownWords", settings.VobSubOcr.PromptForUnknownWords.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("GuessUnknownWords", settings.VobSubOcr.GuessUnknownWords.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("AutoBreakSubtitleIfMoreThanTwoLines", settings.VobSubOcr.AutoBreakSubtitleIfMoreThanTwoLines.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("ItalicFactor", settings.VobSubOcr.ItalicFactor.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrDraw", settings.VobSubOcr.LineOcrDraw.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrMinHeightSplit", settings.VobSubOcr.LineOcrMinHeightSplit.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrAdvancedItalic", settings.VobSubOcr.LineOcrAdvancedItalic.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrLastLanguages", settings.VobSubOcr.LineOcrLastLanguages);
                //textWriter.WriteElementString("LineOcrLastSpellCheck", settings.VobSubOcr.LineOcrLastSpellCheck);
                //textWriter.WriteElementString("LineOcrLinesToAutoGuess", settings.VobSubOcr.LineOcrLinesToAutoGuess.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrMinLineHeight", settings.VobSubOcr.LineOcrMinLineHeight.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrMaxLineHeight", settings.VobSubOcr.LineOcrMaxLineHeight.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LineOcrMaxErrorPixels", settings.VobSubOcr.LineOcrMaxErrorPixels.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LastBinaryImageCompareDb", settings.VobSubOcr.LastBinaryImageCompareDb);
                //textWriter.WriteElementString("LastBinaryImageSpellCheck", settings.VobSubOcr.LastBinaryImageSpellCheck);
                //textWriter.WriteElementString("BinaryAutoDetectBestDb", settings.VobSubOcr.BinaryAutoDetectBestDb.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("LastTesseractSpellCheck", settings.VobSubOcr.LastTesseractSpellCheck);
                //textWriter.WriteElementString("CaptureTopAlign", settings.VobSubOcr.CaptureTopAlign.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UnfocusedAttentionBlinkCount", settings.VobSubOcr.UnfocusedAttentionBlinkCount.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("UnfocusedAttentionPlaySoundCount", settings.VobSubOcr.UnfocusedAttentionPlaySoundCount.ToString(CultureInfo.InvariantCulture));

                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("MultipleSearchAndReplaceGroups", string.Empty);
                //foreach (var group in settings.MultipleSearchAndReplaceGroups)
                //{
                //    if (!string.IsNullOrEmpty(group?.Name))
                //    {
                //        textWriter.WriteStartElement("Group", string.Empty);
                //        textWriter.WriteElementString("Name", group.Name);
                //        textWriter.WriteElementString("Enabled", group.Enabled.ToString(CultureInfo.InvariantCulture));
                //        foreach (var item in group.Rules)
                //        {
                //            textWriter.WriteStartElement("Rule", string.Empty);
                //            textWriter.WriteElementString("Enabled", item.Enabled.ToString(CultureInfo.InvariantCulture));
                //            textWriter.WriteElementString("FindWhat", item.FindWhat);
                //            textWriter.WriteElementString("ReplaceWith", item.ReplaceWith);
                //            textWriter.WriteElementString("SearchType", item.SearchType);
                //            textWriter.WriteElementString("Description", item.Description);
                //            textWriter.WriteEndElement();
                //        }
                //        textWriter.WriteEndElement();
                //    }
                //}
                //textWriter.WriteEndElement();

                //WriteShortcuts(settings.Shortcuts, textWriter);

                //textWriter.WriteStartElement("RemoveTextForHearingImpaired", string.Empty);
                //textWriter.WriteElementString("RemoveTextBetweenBrackets", settings.RemoveTextForHearingImpaired.RemoveTextBetweenBrackets.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBetweenParentheses", settings.RemoveTextForHearingImpaired.RemoveTextBetweenParentheses.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBetweenCurlyBrackets", settings.RemoveTextForHearingImpaired.RemoveTextBetweenCurlyBrackets.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBetweenQuestionMarks", settings.RemoveTextForHearingImpaired.RemoveTextBetweenQuestionMarks.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBetweenCustom", settings.RemoveTextForHearingImpaired.RemoveTextBetweenCustom.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBetweenCustomBefore", settings.RemoveTextForHearingImpaired.RemoveTextBetweenCustomBefore);
                //textWriter.WriteElementString("RemoveTextBetweenCustomAfter", settings.RemoveTextForHearingImpaired.RemoveTextBetweenCustomAfter);
                //textWriter.WriteElementString("RemoveTextBetweenOnlySeparateLines", settings.RemoveTextForHearingImpaired.RemoveTextBetweenOnlySeparateLines.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBeforeColon", settings.RemoveTextForHearingImpaired.RemoveTextBeforeColon.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBeforeColonOnlyIfUppercase", settings.RemoveTextForHearingImpaired.RemoveTextBeforeColonOnlyIfUppercase.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveTextBeforeColonOnlyOnSeparateLine", settings.RemoveTextForHearingImpaired.RemoveTextBeforeColonOnlyOnSeparateLine.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveInterjections", settings.RemoveTextForHearingImpaired.RemoveInterjections.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveInterjectionsOnlyOnSeparateLine", settings.RemoveTextForHearingImpaired.RemoveInterjectionsOnlyOnSeparateLine.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveIfAllUppercase", settings.RemoveTextForHearingImpaired.RemoveIfAllUppercase.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveIfContains", settings.RemoveTextForHearingImpaired.RemoveIfContains.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("RemoveIfContainsText", settings.RemoveTextForHearingImpaired.RemoveIfContainsText);
                //textWriter.WriteEndElement();

                //textWriter.WriteStartElement("SubtitleBeaming", string.Empty);
                //textWriter.WriteElementString("FontName", settings.SubtitleBeaming.FontName);
                //textWriter.WriteElementString("FontColor", settings.SubtitleBeaming.FontColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("FontSize", settings.SubtitleBeaming.FontSize.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("BorderColor", settings.SubtitleBeaming.BorderColor.ToArgb().ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteElementString("BorderWidth", settings.SubtitleBeaming.BorderWidth.ToString(CultureInfo.InvariantCulture));
                //textWriter.WriteEndElement();

                textWriter.WriteEndElement();

                textWriter.WriteEndDocument();
                textWriter.Flush();

                try
                {
                    File.WriteAllText(fileName, sb.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""), Encoding.UTF8);
                }
                catch
                {
                    // ignored
                }
            }
        }       
    }
}
