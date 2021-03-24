using NUnit.Framework;
using RoslynTransformationNetFrame;

namespace TestNFProject
{
    public class RoslynHelperTest
    {
        [Test]
        public void TestExtractNamespace()
        {
            const string sourceCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
       private void CreatePanels()
        {
            int SmallCompSize = 70;
        }
    }
}";

            string result = RoslynHelper.ExtractNamespace(sourceCode);
            Assert.AreEqual("HelloWorld", result);
        }
    }
}
