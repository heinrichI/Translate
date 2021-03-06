using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTransformationNetFrame
{
    internal class BlackList
    {
        static readonly string[] _blackList = 
        { 
            "view", 
            "object", 
            "AddMenuItem", 
            "itemsProgram",
            "itemsAdmin",
            "itemsDocuments"
        };

        internal static bool IsIn(string name)
        {
            return _blackList.Contains(name);
        }
    }
}
