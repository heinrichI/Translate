using NUnit.Framework;
using RoslynTransformation;
using TestNFProject;

namespace TestProject
{
    public class SyntaxTransformationTest
    {
        RoslynManager _testClass;

        public SyntaxTransformationTest()
        {
            //_resourceManager = new Mock<IResourceManager>();
            //_resourceManager.Setup(r => r.GenerateName(It.IsAny<string>())).Returns<string>(t => t);

            _testClass = new RoslynManager(new FakeResourceManager());
        }

        [Test]
        public void TestEnum()
        {
            const string sampleCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        public enum AuditTreeCommand
        {
            Back = 0,
            EditNode = 1,
            NewSiblingBefore = 2,
            NewSiblingAfter = 3,
            NewChild = 4,
            NewAccount = 5,
            EncodeAccounts = 6,
            Refresh = 7,
            Delete = 8,
            ClearEncode = 9,
            NewAuditTransaction = 10,
            ShowNodeExplanations = 11,
        }

        private void AddOptionsForCreate(ContextMenuStrip menu)
        {
            menu.AddMenuItem(""הוסף סעיף לפני"", () => InvokeCommand(this.AuditTreeCommand.NewSiblingBefore));
            menu.AddMenuItem(""הוסף סעיף אחרי"", () => InvokeCommand(AuditTreeCommand.NewSiblingAfter));
            menu.AddMenuItem(""הוסף סעיף ילד"", () => InvokeCommand(AuditTreeCommand.NewChild));
            menu.AddMenuItem(""הוסף חשבון מרשימה"", () => InvokeCommand(AuditTreeCommand.EncodeAccounts));
            menu.AddMenuItem(""הוסף חשבון חדש כילד"", () => InvokeCommand(AuditTreeCommand.NewAccount));
        }
    }
}";

            const string expected =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        public enum AuditTreeCommand
        {
            Back = 0,
            EditNode = 1,
            NewSiblingBefore = 2,
            NewSiblingAfter = 3,
            NewChild = 4,
            NewAccount = 5,
            EncodeAccounts = 6,
            Refresh = 7,
            Delete = 8,
            ClearEncode = 9,
            NewAuditTransaction = 10,
            ShowNodeExplanations = 11,
        }

        private void AddOptionsForCreate(ContextMenuStrip menu)
        {
            menu.AddMenuItem(Strings.NewSiblingBefore, () => InvokeCommand(this.AuditTreeCommand.NewSiblingBefore));
            menu.AddMenuItem(Strings.NewSiblingAfter, () => InvokeCommand(AuditTreeCommand.NewSiblingAfter));
            menu.AddMenuItem(Strings.NewChild, () => InvokeCommand(AuditTreeCommand.NewChild));
            menu.AddMenuItem(Strings.EncodeAccounts, () => InvokeCommand(AuditTreeCommand.EncodeAccounts));
            menu.AddMenuItem(Strings.NewAccount, () => InvokeCommand(AuditTreeCommand.NewAccount));
        }
    }
}";

            string result = _testClass.Rewrite(sampleCode);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestMethodName()
        {
            const string sampleCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        private void InitRangeEditors()
        {
            CreateRangeEditor(""תאריך אסמכתא"");
            CreateRangeEditor(""תאריך ערך"");
        }
    }
}";

            const string expected =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        private void InitRangeEditors()
        {
            CreateRangeEditor(Strings.Program_InitRangeEditors);
            CreateRangeEditor(Strings.Program_InitRangeEditors2);
        }
    }
}";

            string result = _testClass.Rewrite(sampleCode);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestVariableName()
        {
            const string sampleCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(""הצג"");
            cbRepIncomeLevel = new ImageComboBoxEdit().InitImageCombo(new Dictionary<int, string>() {
                {2, ""מבנה""}, {3, ""סעיף""}, {4, ""פרוט""}, {5, ""חשבון""}});
            lblIncomeSumText = new Label().Init(""טקסט סה\""כ לשנה"");
            edtIncomeSumText = new TextEdit().InitTextEdit();
            edtIncomeSumText_Prev = new TextEdit().InitTextEdit();
            edtIncomeSumText_After = new TextEdit().InitTextEdit();
            chkSumRowsDynamicText = new CheckBox().Init(""קיצור לרווח / הפסד לכל הסעיפים הרלוונטיים"");

            chkAutoMove = new CheckBox().Init(""העברה אוטומטית"");
            chkAutoMove_ByYears = new CheckBox().Init(""כל שנה בנפרד"");
        }
    }
}";

            const string expected =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(Strings.Program_lblRepIncomeLevel);
            cbRepIncomeLevel = new ImageComboBoxEdit().InitImageCombo(new Dictionary<int, string>() {
                {2, Strings.Program_cbRepIncomeLevel}, {3, Strings.Program_cbRepIncomeLevel2}, {4, Strings.Program_cbRepIncomeLevel3}, {5, Strings.Program_cbRepIncomeLevel4}});
            lblIncomeSumText = new Label().Init(Strings.Program_lblIncomeSumText);
            edtIncomeSumText = new TextEdit().InitTextEdit();
            edtIncomeSumText_Prev = new TextEdit().InitTextEdit();
            edtIncomeSumText_After = new TextEdit().InitTextEdit();
            chkSumRowsDynamicText = new CheckBox().Init(Strings.Program_chkSumRowsDynamicText);

            chkAutoMove = new CheckBox().Init(Strings.Program_chkAutoMove);
            chkAutoMove_ByYears = new CheckBox().Init(Strings.Program_chkAutoMove_ByYears);
        }
    }
}";

            string result = _testClass.Rewrite(sampleCode);
            Assert.AreEqual(expected, result);
        }  
        
        [Test]
        public void TestComment()
        {
            const string sampleCode =
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
            int MidCompSize = 150;
            int BigCompSize = 270;

            //דוח רווח והפסד
            IDPanel panIncome = new IDPanel(ReportConst.REP_Income);
        }
    }
}";

            string result = _testClass.Rewrite(sampleCode);
            Assert.AreEqual("", result);
        }
    }
}
