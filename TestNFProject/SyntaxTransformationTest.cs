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
            view.AddGridColumn(""RowType"", Strings.Program_InitGridColumns, """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { Strings.Program_RepositoryItemImageComboBox, Strings.Program_RepositoryItemImageComboBox2, Strings.Program_RepositoryItemImageComboBox3 }),
                true, false, false, true);
            view.AddGridColumn(""RowText"", Strings.Program_RepositoryItemImageComboBox, """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
            view.AddGridColumn(""NoField1"", Strings.Program_InitGridColumns2, """", null, true, false, false, true);

            view.SetButtonEdit(""NoField1"", """", EditProperties, null);

            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            _fakeResourceManager.ContainValue("מספר");
            _fakeResourceManager.ContainValue("סוג");
            _fakeResourceManager.ContainValue("טקסט");
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
            view.AddGridColumn(""RowText"", Strings.Program_InitGridColumns, """", new RepositoryItemTextEdit().InitTextEdit(), true, false, false, true);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            _fakeResourceManager.ContainValue("טקסט");
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
            view.AddGridColumn(""RowType"", Strings.Program_InitGridColumns, """",
                new RepositoryItemImageComboBox().InitImageCombo(new[] { 0, 1, 2 }, new[] { Strings.Program_RepositoryItemImageComboBox, Strings.Program_RepositoryItemImageComboBox2, Strings.Program_RepositoryItemImageComboBox3 }),
                true, false, false, true);
        }
    }
}";
            _fakeResourceManager.Clear();

            string result = _testClass.Rewrite(sourceCode);
            Assert.AreEqual(expected, result);
            _fakeResourceManager.ContainValue("טקסט");
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
            _fakeResourceManager.ContainValue("מע\"מ");
            _fakeResourceManager.ContainValue("מקדמות");
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
            _fakeResourceManager.ContainValue("נא לקלוט");
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
            _fakeResourceManager.ContainValue("נא לקלוט");
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
            _fakeResourceManager.ContainValue("נא לקלוט");
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
            _fakeResourceManager.ContainValue("שימו לב: תקופת ניסיון מסתיימת ב");
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
            _fakeResourceManager.ContainValue("מספר מסמכים שהופקו");
            _fakeResourceManager.ContainValue("ניתן להפים עד");
            _fakeResourceManager.ContainValue("מסמכים");
            _fakeResourceManager.ContainValue("במקופה מ");
            _fakeResourceManager.ContainValue("עד");
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
            _fakeResourceManager.ContainValue("סוג אשראי");
            _fakeResourceManager.ContainValue("אשראי רגיל");
            _fakeResourceManager.ContainValue("תשלומים");
            _fakeResourceManager.ContainValue("קרדיט/תשלומים");
            _fakeResourceManager.ContainValue("תשלום מיידי");
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
            _fakeResourceManager.ContainValue("שם");
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
            _fakeResourceManager.ContainValue("ברירת מחדל");
            _fakeResourceManager.ContainValue("זכות");
            _fakeResourceManager.ContainValue("חובה");
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
            _fakeResourceManager.ContainValue("ACC_AccountKey|70|מספר|ACC_FullName|230|שם");
        }
    }
}
