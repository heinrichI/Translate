using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslateService;

namespace TestNFProject
{
    public class AbstractTranslatorServiceTest
    {
        [Test]
        public void EncodeSequences2Test()
        {
            const string translate = @"Customize columns
התאמה אישית של עמודות";
            AbstractTranslatorService translatorService = new GoogleTranslator("he", "en");
            Dictionary<int, string> encodeInfo = new Dictionary<int, string>();
            string codedString = translatorService.EncodeSequences2(translate, out encodeInfo);

            Assert.AreEqual(1, encodeInfo.Count);

            string decodedString = translatorService.DecodeSequences(codedString, encodeInfo);
            Assert.AreEqual(translate, decodedString);
        }

        [Test]
        public void EncodeSequences2Test2()
        {
            const string translate = @"Customize columns
התאמה אישית של עמודות";
            AbstractTranslatorService translatorService = new GoogleTranslator("he", "en");
            Dictionary<int, string> encodeInfo = new Dictionary<int, string>();
            string codedString = translatorService.EncodeSequences2(translate, out encodeInfo);

            Assert.AreEqual(1, encodeInfo.Count);
            codedString = codedString.Replace("התאמה אישית של עמודות", "Customize columns");

            string decodedString = translatorService.DecodeSequences(codedString, encodeInfo);
            Assert.AreEqual(@"Customize columns
Customize columns", decodedString);
        }
    }
}
