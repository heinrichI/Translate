using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resource;
using RoslynTransformation;

namespace TranslateProgram
{
    class Program
    {
        static void Main(string[] args)
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
            menu.AddMenuItem(""הוסף סעיף לפני"", () => InvokeCommand(AuditTreeCommand.NewSiblingBefore));
            menu.AddMenuItem(""הוסף סעיף אחרי"", () => InvokeCommand(AuditTreeCommand.NewSiblingAfter));
            menu.AddMenuItem(""הוסף סעיף ילד"", () => InvokeCommand(AuditTreeCommand.NewChild));
            menu.AddMenuItem(""הוסף חשבון מרשימה"", () => InvokeCommand(AuditTreeCommand.EncodeAccounts));
            menu.AddMenuItem(""הוסף חשבון חדש כילד"", () => InvokeCommand(AuditTreeCommand.NewAccount));
        }
    }
}";

            TestClass.Rewrite(sampleCode);
            ResourceHelper.Test("c:\\Liram\\TranslateHardcode\\1.resx");
        }
    }
}
