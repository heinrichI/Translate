using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Row> GetRow();
    }
}
