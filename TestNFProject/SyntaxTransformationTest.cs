using NUnit.Framework;
using RoslynTransformationNetFrame;

namespace TestNFProject
{
    public class SyntaxTransformationTest
    {
        RoslynManager _testClass;
        FakeResourceManager _fakeResourceManager; 

        public SyntaxTransformationTest()
        {
            //_resourceManager = new Mock<IResourceManager>();
            //_resourceManager.Setup(r => r.GenerateName(It.IsAny<string>())).Returns<string>(t => t);
            _fakeResourceManager = new FakeResourceManager();
            _testClass = new RoslynManager(_fakeResourceManager);
        }

        [Test]
        public void TestEnum()
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
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestMethodName()
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
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestVariableName()
        {
            const string sourceCode
                =
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
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestVariableName2()
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
        private void InitGridColumns() 
        {
            GridColumn column = view.AddGridColumn(""TopIndex"", ""מספר"", """");
            column.SortOrder = ColumnSortOrder.Ascending;
            view.AddGridColumn(""RowType"", ""סוג"", """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { ""טקסט"", ""כותרת"", ""מטבע"" }),
                true, false, false, true);
            view.AddGridColumn(""RowText"", ""טקסט"", """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
            view.AddGridColumn(""NoField1"", ""מאפיינים"", """", null, true, false, false, true);

            view.SetButtonEdit(""NoField1"", """", EditProperties, null);

            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
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
        private void InitGridColumns() 
        {
            GridColumn column = view.AddGridColumn(""TopIndex"", Strings.Program_column, """");
            column.SortOrder = ColumnSortOrder.Ascending;
            view.AddGridColumn(""RowType"", Strings.Program_InitGridColumns, """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { Strings.Program_InitGridColumns2, Strings.Program_InitGridColumns3, Strings.Program_InitGridColumns4 }),
                true, false, false, true);
            view.AddGridColumn(""RowText"", Strings.Program_InitGridColumns2, """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
            view.AddGridColumn(""NoField1"", Strings.Program_InitGridColumns5, """", null, true, false, false, true);

            view.SetButtonEdit(""NoField1"", """", EditProperties, null);

            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestComment()
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
            int MidCompSize = 150;
            int BigCompSize = 270;

            //דוח רווח והפסד
            IDPanel panIncome = new IDPanel(ReportConst.REP_Income);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual("", result);
        }

        [Test]
        public void TestNamespace()
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
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(""הצג"");
        }
    }
}";

            const string expected =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;
using Organization.UI.Controls.Properties;

namespace HelloWorld
{
    class Program
    {
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(Strings.Program_lblRepIncomeLevel);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode, "Organization.UI.Controls.Properties");
            Assert.AreEqual(expected, result);
        }  
        
        [Test]
        public void TestNamespace2()
        {
            const string sourceCode =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;
using Organization.UI.Controls.Properties;

namespace HelloWorld
{
    class Program
    {
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(""הצג"");
        }
    }
}";

            const string expected =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;
using Organization.UI.Controls.Properties;

namespace HelloWorld
{
    class Program
    {
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(Strings.Program_lblRepIncomeLevel);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode, "Organization.UI.Controls.Properties");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestNamespace3()
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
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(""הצג"");
        }
    }
}";

            const string expected =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;
using Organization.UI.Controls.Properties;

namespace HelloWorld
{
    class Program
    {
      private void InitComponents()
        {
            lblRepIncomeLevel = new Label().Init(Strings.Program_lblRepIncomeLevel);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode, "Organization.UI.Controls.Properties");
            Assert.AreEqual(expected, result);
        }

        /*
        [Test]
        public void TestRenameField()
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
        //comment1
        CheckBox chkAutoMove, chkAutoMove3;
        //comment2
        CheckBox _chkAutoMove2;
        Label lblRepIncomeLevel;

        private void InitComponents()
        {
            lblRepIncomeLevel = ""Label1"";
            int localVariable1;
            localVariable1 = 5;
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
        //comment1
        CheckBox _chkAutoMove, 
        CheckBox _chkAutoMove3;
        //comment2
        CheckBox _chkAutoMove2;
        Label _lblRepIncomeLevel;

        private void InitComponents()
        {
            _lblRepIncomeLevel = ""Label1"";
            int localVariable1;
            localVariable1 = 5;
        }
    }
}";
            //string result = _testClass.Rewrite(sourceCode);
            string result = RoslynHelper.Refactor(sourceCode);
            Assert.AreEqual(expected, result);
        }*/
    }
}
