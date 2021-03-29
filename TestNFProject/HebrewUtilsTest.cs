using BusinessLogic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNFProject
{
    public class HebrewUtilsTest
    {
        [TestCase("actPaymentTypes", false)]
        [TestCase("איסוף שעות עובד", true)]
        [TestCase("&Run SQL", false)]
        [TestCase(@"Save
שמור", true)]
        [TestCase(@"OK
אישור", true)]
        [TestCase(@"Reset filters
בטל סינון", true)]
        public void IsHebrewStringTest(string text, bool isHebrew)
        {
            bool result = HebrewUtils.IsHebrewString(text);

            Assert.AreEqual(isHebrew, result);
        }

        [TestCase("actPaymentTypes", false)]
        [TestCase("איסוף שעות עובד", true)]
        [TestCase("&Run SQL", false)]
        [TestCase(@"Save
שמור", false)]
        [TestCase(@"OK
אישור", false)]
        [TestCase(@"Reset filters
בטל סינון", false)]
        [TestCase(@"
", false)]
        public void IsOnlyHebrewStringTest(string text, bool isHebrew)
        {
            bool result = HebrewUtils.IsOnlyHebrewString(text);

            Assert.AreEqual(isHebrew, result);
        }
    }
}
