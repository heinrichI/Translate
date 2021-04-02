using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTransformationNetFrame
{
    internal class BlackList
    {
        static readonly string[] _blackList = { "view", "object" };

        internal static bool IsIn(string name)
        {
            return _blackList.Contains(name);
        }
    }
}
