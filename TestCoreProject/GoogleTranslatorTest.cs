using System.Collections.Generic;
using NUnit.Framework;
using TranslateService;

namespace TestProject
{
    public class Tests
    {

        [Test]
        public void GetTranslateTest1()
        {
            string fullResponse = "{\"sentences\":[{\"trans\":\"Инвестор ACS\",\"orig\":\"משקיע ACS\",\"backend\":3,\"model_specification\":[{},{}],\"translation_engine_debug_info\":[{\"model_tracking\":{\"checkpoint_md5\":\"ef4a126affdcc2d3c84e987e2d0fb6b1\",\"launch_doc\":\"tea_GermanicB_afdaislbnosvfyyiiw_en_2020q2.md\"}},{\"model_tracking\":{\"checkpoint_md5\":\"cce7c67b3f2439089dd6b428e0b83b88\",\"launch_doc\":\"en_ru_2020q2.md\"}}]}],\"src\":\"iw\",\"alternative_translations\":[{\"src_phrase\":\"משקיע ACS\",\"alternative\":[{\"word_postproc\":\"Инвестор ACS\",\"score\":0,\"has_preceding_space\":true,\"attach_to_next_token\":false},{\"word_postproc\":\"ACS инвестор\",\"score\":0,\"has_preceding_space\":true,\"attach_to_next_token\":false}],\"srcunicodeoffsets\":[{\"begin\":0,\"end\":9}],\"raw_src_segment\":\"משקיע ACS\",\"start_pos\":0,\"end_pos\":0}],\"spell\":{}}";
            string translate = GoogleTranslator.GetTranslate(fullResponse);
            Assert.AreEqual("Инвестор ACS", translate);
        }
    }
}