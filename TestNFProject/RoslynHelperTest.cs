using NUnit.Framework;
using RoslynTransformationNetFrame;

namespace TestNFProject
{
    public class RoslynHelperTest
    {
        [Test]
        public void ExtractNamespaceTest()
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

        [Test]
        public void IsInternalClassTest()
        {
            const string sourceCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    public class Program
    {
       private void CreatePanels()
        {
            int SmallCompSize = 70;
        }
    }
}";

            bool result = RoslynHelper.IsInternalClass(sourceCode);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInternalClassTest2()
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

            bool result = RoslynHelper.IsInternalClass(sourceCode);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInternalClassTest3()
        {
            const string sourceCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    internal class Program
    {
       private void CreatePanels()
        {
            int SmallCompSize = 70;
        }
    }
}";

            bool result = RoslynHelper.IsInternalClass(sourceCode);
            Assert.IsTrue(result);
        }
    }
}
