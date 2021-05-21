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
            Assert.IsTrue(_fakeResourceManager.ContainValue("הוסף סעיף לפני"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("הוסף סעיף אחרי"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("הוסף סעיף ילד"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("הוסף חשבון מרשימה"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("הוסף חשבון חדש כילד"));
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
            Assert.IsTrue(_fakeResourceManager.ContainValue("תאריך אסמכתא"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("תאריך ערך"));

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
            Assert.IsTrue(_fakeResourceManager.ContainValue("הצג"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("חשבון"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("פרוט"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("סעיף"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מבנה"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("טקסט סה\"כ לשנה"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("קיצור לרווח / הפסד לכל הסעיפים הרלוונטיים"));
        }

        [Test]
        public void TestVariableName_DuplicateString()
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
            view.AddGridColumn(""RowType"", Strings.Program_RowType, """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { Strings.Program_RepositoryItemImageComboBox, Strings.Program_RepositoryItemImageComboBox2, Strings.Program_RepositoryItemImageComboBox3 }),
                true, false, false, true);
            view.AddGridColumn(""RowText"", Strings.Program_RepositoryItemImageComboBox, """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
            view.AddGridColumn(""NoField1"", Strings.Program_NoField1, """", null, true, false, false, true);

            view.SetButtonEdit(""NoField1"", """", EditProperties, null);

            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("מספר"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("סוג"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("טקסט"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מאפיינים"));
        }

        [Test]
        public void TestVariableName_VariableShouldBeFirst()
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
            view.AddGridColumn(""RowText"", ""טקסט"", """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
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
            view.AddGridColumn(""RowText"", Strings.Program_RowText, """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("טקסט"));
        }

        [Test]
        public void TestVariableName_VariableShouldBeFirst2()
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
            view.AddGridColumn(""RowType"", ""סוג"", """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { ""טקסט"", ""כותרת"", ""מטבע"" }),
                true, false, false, true);
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
            view.AddGridColumn(""RowType"", Strings.Program_RowType, """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { Strings.Program_RepositoryItemImageComboBox, Strings.Program_RepositoryItemImageComboBox2, Strings.Program_RepositoryItemImageComboBox3 }),
                true, false, false, true);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("טקסט"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("סוג"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("כותרת"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מטבע"));
        }

        [Test]
        public void TestVariableName3()
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
        bool is_OneSidedCodeType = false;

        private void InitGridColumns() 
        {
            if (is_OneSidedCodeType)
            {
                chkIsVat = new CheckBox().Init(""מע\""מ"", true);
                chkIsVat.Location = new Point(LabelsLeft, lblCodeType.Top);
                chkIsVat.Width = LabelsWidth;
                chkIsVat.CheckedChanged += chkIsVat_CheckedChanged;

                chkIsAdvPayment = new CheckBox().Init(""מקדמות"", true);
                chkIsAdvPayment.Location = new Point(LabelsLeft - LabelsWidth, cbCodeType.Top);
                chkIsAdvPayment.Width = LabelsWidth;

                pnlGeneral.Controls.AddRange(new Control[] { chkIsVat, chkIsAdvPayment });
            }
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
        bool is_OneSidedCodeType = false;

        private void InitGridColumns() 
        {
            if (is_OneSidedCodeType)
            {
                chkIsVat = new CheckBox().Init(Strings.Program_chkIsVat, true);
                chkIsVat.Location = new Point(LabelsLeft, lblCodeType.Top);
                chkIsVat.Width = LabelsWidth;
                chkIsVat.CheckedChanged += chkIsVat_CheckedChanged;

                chkIsAdvPayment = new CheckBox().Init(Strings.Program_chkIsAdvPayment, true);
                chkIsAdvPayment.Location = new Point(LabelsLeft - LabelsWidth, cbCodeType.Top);
                chkIsAdvPayment.Width = LabelsWidth;

                pnlGeneral.Controls.AddRange(new Control[] { chkIsVat, chkIsAdvPayment });
            }
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("מע\"מ"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מקדמות"));
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
            Assert.IsTrue(_fakeResourceManager.ContainValue("הצג"));
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
            Assert.IsTrue(_fakeResourceManager.ContainValue("הצג"));
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
            Assert.IsTrue(_fakeResourceManager.ContainValue("הצג"));
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

        [Test]
        public void TestConstName()
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
        private void CreateButtonPanel() 
        {
            pnlButtons = new Panel();
            pnlButtons.Size = new Size(800, 60);

            Dictionary<int, string> Items = new Dictionary<int, string>();
            Items.Add(ReportConst.REP_BalanceList, ""מאזנים"");
            Items.Add(ReportConst.REP_Income, ""דוח רווח והפסד"");
            Items.Add(ReportConst.REP_TaxRecon, ""דו התאמה לצרכי מס"");
        }
    }

    public static class ReportConst
    {
        public const int
            REP_StartPage = 0,
            REP_Income = 1,
            REP_BalanceList = 2,
            REP_CapitalChange = 3,
            REP_CashFlow = 4,
            REP_TaxRecon = 5;
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
        private void CreateButtonPanel() 
        {
            pnlButtons = new Panel();
            pnlButtons.Size = new Size(800, 60);

            Dictionary<int, string> Items = new Dictionary<int, string>();
            Items.Add(ReportConst.REP_BalanceList, Strings.Program_REP_BalanceList);
            Items.Add(ReportConst.REP_Income, Strings.Program_REP_Income);
            Items.Add(ReportConst.REP_TaxRecon, Strings.Program_REP_TaxRecon);
        }
    }

    public static class ReportConst
    {
        public const int
            REP_StartPage = 0,
            REP_Income = 1,
            REP_BalanceList = 2,
            REP_CapitalChange = 3,
            REP_CashFlow = 4,
            REP_TaxRecon = 5;
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("מאזנים"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("דוח רווח והפסד"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("דו התאמה לצרכי מס"));
        }

        [Test]
        public void TestFirstMember()
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
        private void InitMeetingsGrid()
        {
            viewMeetings.AddGridColumn(nameof(MeetingModel.WorkerId), ""עובד"", """", 
                WorkerLookups.GetWorkersLookup_Repository(tbWorkers: Model.tbWorkers));
            viewMeetings.AddGridColumn(nameof(MeetingModel.StartDate), ""תאריך"", ""dd/MM/yyyy hh:mm:ss"");

            viewMeetings.AddGridColumn(nameof(MeetingModel.TypeId), ""סוג"", """", MessageLookups.GetCallActionLookup());
            viewMeetings.AddGridColumn(nameof(MeetingModel.StatusId), ""סטטוס"", """", MessageLookups.GetCallStatusLookup());
            viewMeetings.AddGridColumn(nameof(MeetingModel.Conclusion), ""מסקנות"", """");
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
        private void InitMeetingsGrid()
        {
            viewMeetings.AddGridColumn(nameof(MeetingModel.WorkerId), Strings.Program_WorkerId, """", 
                WorkerLookups.GetWorkersLookup_Repository(tbWorkers: Model.tbWorkers));
            viewMeetings.AddGridColumn(nameof(MeetingModel.StartDate), Strings.Program_StartDate, ""dd/MM/yyyy hh:mm:ss"");

            viewMeetings.AddGridColumn(nameof(MeetingModel.TypeId), Strings.Program_TypeId, """", MessageLookups.GetCallActionLookup());
            viewMeetings.AddGridColumn(nameof(MeetingModel.StatusId), Strings.Program_StatusId, """", MessageLookups.GetCallStatusLookup());
            viewMeetings.AddGridColumn(nameof(MeetingModel.Conclusion), Strings.Program_Conclusion, """");
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("עובד"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("תאריך"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("סוג"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("סטטוס"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מסקנות"));
        }

        [Test]
        public void TestInterpollatedString()
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
        public bool IsValidData()
        {
            MessageUtils.ShowError($""נא לקלוט  {GetCpiText(field.Caption)}!"");
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
        public bool IsValidData()
        {
            MessageUtils.ShowError($""{Strings.Program_IsValidData}  {GetCpiText(field.Caption)}!"");
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("נא לקלוט"));
        }

        [Test]
        public void TestInterpollatedString2()
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
        public bool IsValidData()
        {
            MessageUtils.ShowError($""נא לקלוט{GetCpiText(field.Caption)}!"");
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
        public bool IsValidData()
        {
            MessageUtils.ShowError($""{Strings.Program_IsValidData}{GetCpiText(field.Caption)}!"");
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("נא לקלוט"));
        }

        [Test]
        public void TestInterpollatedString3()
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
        public bool IsValidData()
        {
            MessageUtils.ShowError($""{field.Name} נא לקלוט {GetCpiText(field.Caption)}!"");
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
        public bool IsValidData()
        {
            MessageUtils.ShowError($""{field.Name} {Strings.Program_IsValidData} {GetCpiText(field.Caption)}!"");
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("נא לקלוט"));
        }

        [Test]
        public void TestInterpollatedString4()
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
        public bool IsValidData()
        {
            if (user.IsTrialLicense)
            {
                messages.Add(new SystemMessage
                {
                    Text = $""שימו לב: תקופת ניסיון מסתיימת ב-{user.ServiceEnd:dd/MM/yyyy}"",
                    action = ShowBuyCloudLicenseForm
                });
            }
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
        public bool IsValidData()
        {
            if (user.IsTrialLicense)
            {
                messages.Add(new SystemMessage
                {
                    Text = $""{Strings.Program_Text}-{user.ServiceEnd:dd/MM/yyyy}"",
                    action = ShowBuyCloudLicenseForm
                });
            }
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("שימו לב: תקופת ניסיון מסתיימת ב"));
        }        
        
        [Test]
        public void TestInterpollatedString5()
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
        private static string ComposeDocumentLimitMessage(EntityLimitInfo info)
        {
            return $""מספר מסמכים שהופקו: {info.CreatedEntityCount},""+
                $"" ניתן להפים עד {info.AllowedEntityCount} מסמכים"" +
                $"" במקופה מ-{info.Period.From:dd/MM/yyyy} עד {info.Period.To:dd/MM/yyyy}"";
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
        private static string ComposeDocumentLimitMessage(EntityLimitInfo info)
        {
            return $""{Strings.Program_ComposeDocumentLimitMessage} {info.CreatedEntityCount},""+
                $"" {Strings.Program_ComposeDocumentLimitMessage2} {info.AllowedEntityCount} {Strings.Program_ComposeDocumentLimitMessage3}"" +
                $"" {Strings.Program_ComposeDocumentLimitMessage4}-{info.Period.From:dd/MM/yyyy} {Strings.Program_ComposeDocumentLimitMessage5} {info.Period.To:dd/MM/yyyy}"";
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("מספר מסמכים שהופקו:"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("ניתן להפים עד"));
            Assert.IsFalse(_fakeResourceManager.ContainValue(" ניתן להפים עד "));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מסמכים"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("במקופה מ"));
            Assert.IsFalse(_fakeResourceManager.ContainValue(" במקופה מ-"));
            Assert.IsFalse(_fakeResourceManager.ContainValue("במקופה מ-"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("עד"));
            Assert.IsFalse(_fakeResourceManager.ContainValue(" עד "));
        } 
        
        [Test]
        public void TestInterpollatedString6()
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
        private static string ComposeDocumentLimitMessage(EntityLimitInfo info)
        {
            return $""DateTime: {info.CreatedEntityCount}"";
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TestConst()
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
        // work data
        ValuesEditor editor;

        private void InitEditor()
        {
            editor.Fields.Add(new ComboBoxField<int>
            {
                FieldName = nameof(creditCard.CreditTerms),
                Caption = ""סוג אשראי"",
                Items = new Dictionary<int, string>
                {
                    [CreditCardConst.TERMS_REGULAR] = ""אשראי רגיל"",
                    [CreditCardConst.TERMS_PAYMENTS] = ""תשלומים"",
                    [CreditCardConst.TERMS_CREDIT_PAYMENTS] = ""קרדיט/תשלומים"",
                    [CreditCardConst.TERMS_DIRECT] = ""תשלום מיידי""
                },
                OnEditValueChanged = ApplyCreditTerms,
                ReadOnly = TermsReadOnly
            });
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
        // work data
        ValuesEditor editor;

        private void InitEditor()
        {
            editor.Fields.Add(new ComboBoxField<int>
            {
                FieldName = nameof(creditCard.CreditTerms),
                Caption = Strings.Program_Caption,
                Items = new Dictionary<int, string>
                {
                    [CreditCardConst.TERMS_REGULAR] = Strings.Program_TERMS_REGULAR,
                    [CreditCardConst.TERMS_PAYMENTS] = Strings.Program_TERMS_PAYMENTS,
                    [CreditCardConst.TERMS_CREDIT_PAYMENTS] = Strings.Program_TERMS_CREDIT_PAYMENTS,
                    [CreditCardConst.TERMS_DIRECT] = Strings.Program_TERMS_DIRECT
                },
                OnEditValueChanged = ApplyCreditTerms,
                ReadOnly = TermsReadOnly
            });
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("סוג אשראי"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("אשראי רגיל"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("תשלומים"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("קרדיט/תשלומים"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("תשלום מיידי"));
        }

        [Test]
        public void TestObjectName()
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
        private void InitEditor()
        {
            edtACC_ClientType_Id.Properties.InitGridLookupColumns(""CLT_Name"", ""שם"");
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
        private void InitEditor()
        {
            edtACC_ClientType_Id.Properties.InitGridLookupColumns(""CLT_Name"", Strings.Program_edtACC_ClientType_Id);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("שם"));
        }

        [Test]
        public void TestObjectName2()
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
        private void InitEditor()
        {
            icbIncomeSign.InitImageCombo(new Dictionary<int, string>() { { 0, ""ברירת מחדל"" }, { 1, ""זכות"" }, { 2, ""חובה"" } });
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
        private void InitEditor()
        {
            icbIncomeSign.InitImageCombo(new Dictionary<int, string>() { { 0, Strings.Program_icbIncomeSign }, { 1, Strings.Program_icbIncomeSign2 }, { 2, Strings.Program_icbIncomeSign3 } });
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("ברירת מחדל"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("זכות"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("חובה"));
        }

        [Test]
        public void TestObjectName3()
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
        public void InitFormMasav(string AccKey)
        {
            edtAccCode.Visible = false;
            ctrlPopupGrid popupAccount = new ctrlPopupGrid();
            pnlTop.Controls.Add(popupAccount);
            popupAccount.Name = ""popupAccountLink"";
            popupAccount.Size = edtAccCode.Size;
            popupAccount.Location = edtAccCode.Location;
            popupAccount.DisplayField = ""ACC_AccountKey"";
            popupAccount.GridFields = ""ACC_AccountKey|70|מספר|ACC_FullName|230|שם"";
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
        public void InitFormMasav(string AccKey)
        {
            edtAccCode.Visible = false;
            ctrlPopupGrid popupAccount = new ctrlPopupGrid();
            pnlTop.Controls.Add(popupAccount);
            popupAccount.Name = ""popupAccountLink"";
            popupAccount.Size = edtAccCode.Size;
            popupAccount.Location = edtAccCode.Location;
            popupAccount.DisplayField = ""ACC_AccountKey"";
            popupAccount.GridFields = Strings.Program_popupAccount;
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("ACC_AccountKey|70|מספר|ACC_FullName|230|שם"));
        }  
        
        [Test]
        public void TestObjectName4()
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
        public void Init()
        {
            RamControlUtils.SetFormDefaults(frmEdit, ""frmEdit"", ""עריכת מסמך: "" + DocTypeDB.GetDocumentType(MainDocTypeId).Description);
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
        public void Init()
        {
            RamControlUtils.SetFormDefaults(frmEdit, ""frmEdit"", Strings.Program_frmEdit + DocTypeDB.GetDocumentType(MainDocTypeId).Description);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("עריכת מסמך: "));
        } 
        
        [Test]
        public void TestObjectName5()
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
        public void Init()
        {
            btnPrint.SetDropDownMenu(
                new string[] { btnPrint.Text.Replace(""\n"", "" ""), ""בחירת מדפסת"", ""בחירת תבנית הדפסה"", ""הדפס באנגלית"", ""שמירה ויצוא לקובץ PDF"", ""שמור והצג"" },
                new Action[] { DoSaveAndPrint, DoSaveAndPrint_Dialog, ChoosePrintTemplate, SaveAndPrintInEnglish, DoSaveAndExportPdf, DoSaveAndPreview });
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
        public void Init()
        {
            btnPrint.SetDropDownMenu(
                new string[] { btnPrint.Text.Replace(""\n"", "" ""), Strings.Program_btnPrint, Strings.Program_btnPrint2, Strings.Program_btnPrint3, Strings.Program_btnPrint4, Strings.Program_btnPrint5 },
                new Action[] { DoSaveAndPrint, DoSaveAndPrint_Dialog, ChoosePrintTemplate, SaveAndPrintInEnglish, DoSaveAndExportPdf, DoSaveAndPreview });
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("בחירת מדפסת"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("שמירה ויצוא לקובץ PDF"));
        }  
        
        [Test]
        public void TestGlobalVariable()
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
        DataTable tbPaymentTypes;
 
        const string ToolTip_ChargeDay = ""תאריך העברת תשלום ע\""י חברת אשראי לחשבון בנק של העסק בחודש עוקב"";

        public void InitFormMasav(string AccKey)
        {
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
        DataTable tbPaymentTypes;
 
        const string ToolTip_ChargeDay = Strings.Program_ToolTip_ChargeDay;

        public void InitFormMasav(string AccKey)
        {
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue(@"תאריך העברת תשלום ע""י חברת אשראי לחשבון בנק של העסק בחודש עוקב"));
        } 
        
        [Test]
        public void TestGlobalVariable2()
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
        string AccountTypeStr => IsSuppliers ? ""ספק"" : ""לקוח"";
        string AccountsTypeStr => IsSuppliers ? ""ספקים"" : ""לקוחות"";
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
        string AccountTypeStr => IsSuppliers ? Strings.Program_AccountTypeStr : Strings.Program_AccountTypeStr2;
        string AccountsTypeStr => IsSuppliers ? Strings.Program_AccountsTypeStr : Strings.Program_AccountsTypeStr2;
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("ספק"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("לקוח"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("ספקים"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("לקוחות"));
        }

        [Test]
        public void TestNewLineSkip()
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
        public void Init()
        {
            using (var progress = RamForms.Manager.CreatePanelProgress())
            {
                progress.DisplayMessage(""מתבצע בניית מסד נתונים ראשי.\n"" + ""עם סיום התהליך תוכל להתחיל להינות מהשימוש במערכת"");
            }
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
        public void Init()
        {
            using (var progress = RamForms.Manager.CreatePanelProgress())
            {
                progress.DisplayMessage(Strings.Program_progress + ""\n"" + Strings.Program_progress2);
            }
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("מתבצע בניית מסד נתונים ראשי."));
            Assert.IsFalse(_fakeResourceManager.ContainValue("מתבצע בניית מסד נתונים ראשי.\n"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("עם סיום התהליך תוכל להתחיל להינות מהשימוש במערכת"));
        } 
        
        [Test]
        public void TestSwitch()
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
        public static string ToDescription(this DocTypeDetailedTransaction value)
        {
            switch (value)
            {
                case DocTypeDetailedTransaction.Regular: return ""רגילה"";
                case DocTypeDetailedTransaction.Detailed: return ""מפורטת"";
                case DocTypeDetailedTransaction.DetailedTwoSides: return ""מפורטת שני צדדים"";
            }
            return "";
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
        public static string ToDescription(this DocTypeDetailedTransaction value)
        {
            switch (value)
            {
                case DocTypeDetailedTransaction.Regular: return Strings.Program_Regular;
                case DocTypeDetailedTransaction.Detailed: return Strings.Program_Detailed;
                case DocTypeDetailedTransaction.DetailedTwoSides: return Strings.Program_DetailedTwoSides;
            }
            return "";
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("רגילה"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מפורטת"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מפורטת שני צדדים"));
        }
        
        [Test]
        public void TestSwitch2()
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
        public static string Localize(string fieldName)
        {
            switch (fieldName)
            {
                case ""ENT_DueDate"":
                    return @""תאריך"";
                case ""ENT_Referance"":
                    return @""אסמכתא"";
                case ""ENT_Description"":
                    return @""פרטים"";
            }
            return fieldName;
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
        public static string Localize(string fieldName)
        {
            switch (fieldName)
            {
                case ""ENT_DueDate"":
                    return Strings.Program_ENT_DueDate;
                case ""ENT_Referance"":
                    return Strings.Program_ENT_Referance;
                case ""ENT_Description"":
                    return Strings.Program_ENT_Description;
            }
            return fieldName;
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("תאריך"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("אסמכתא"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("פרטים"));
        }

        [Test]
        public void TestSwitch3()
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
        public static string Localize(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(ENT_Account_Id):
                    return ""קוד חשבון"";
                case nameof(ENT_Batch_Id):
                    return ""מספר מנה"";
                case nameof(ENT_Branch_Id):
                    return ""Branch ID"";
                case nameof(ENT_Curr_Id):
                    return ""Currency ID"";
            }
            return fieldName;
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
        public static string Localize(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(ENT_Account_Id):
                    return Strings.Program_ENT_Account_Id;
                case nameof(ENT_Batch_Id):
                    return Strings.Program_ENT_Batch_Id;
                case nameof(ENT_Branch_Id):
                    return ""Branch ID"";
                case nameof(ENT_Curr_Id):
                    return ""Currency ID"";
            }
            return fieldName;
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("קוד חשבון"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("מספר מנה"));
        }
        
        [Test]
        public void TestMenuAction()
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
        private void CreateMenuAccounts()
        {
            ContextMenuStrip menuAccounts = new ContextMenuStrip();
            menuAccounts.AddMenuItem(""mniEditAccount"", ""פרטי חשבון"", EditAccount);
            if (!IsSuppliers)
            {
                menuAccounts.AddMenuItem(""mniAccountBanks"", ""בנקים ללקוח"", ShowAccountBanks).Visible = AdminState.DebugVersion; // by Rami's request
                menuAccounts.AddMenuItem(""mniClientAgreements"", ""הסכם לקוח"", ShowClientAgreement);
            }
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
        private void CreateMenuAccounts()
        {
            ContextMenuStrip menuAccounts = new ContextMenuStrip();
            menuAccounts.AddMenuItem(""mniEditAccount"", Strings.Program_mniEditAccount, EditAccount);
            if (!IsSuppliers)
            {
                menuAccounts.AddMenuItem(""mniAccountBanks"", Strings.Program_mniAccountBanks, ShowAccountBanks).Visible = AdminState.DebugVersion; // by Rami's request
                menuAccounts.AddMenuItem(""mniClientAgreements"", Strings.Program_mniClientAgreements, ShowClientAgreement);
            }
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("פרטי חשבון"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("בנקים ללקוח"));
            Assert.IsTrue(_fakeResourceManager.ContainValue("הסכם לקוח"));
        }
    
        [Test]
        public void TestItemsProgram()
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
        private void CreateMenuAccounts()
        {
            itemsProgram.Add(MenuKeys.ProgramSmsSettings, ""הגדרות שליחת SMS"", null);
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
        private void CreateMenuAccounts()
        {
            itemsProgram.Add(MenuKeys.ProgramSmsSettings, Strings.Program_ProgramSmsSettings, null);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("הגדרות שליחת SMS"));
        }

        [Test]
        public void Test2()
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
        protected virtual void InitGridMenu()
        {
            view.AddGridMenuItem(""הצג מסמך"", ShowFocusedDocument);
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
        protected virtual void InitGridMenu()
        {
            view.AddGridMenuItem(Strings.Program_InitGridMenu, ShowFocusedDocument);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_fakeResourceManager.ContainValue("הצג מסמך"));
        }
    }
}
