using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLogic
{
    public interface IDynamicRepository : IDisposable
    {
        IEnumerable<dynamic> GetDynamic(string[] columns, string tableName);

        IEnumerable<IDataRecord> GetDataRecord(string[] columns, string tableName);
    }
}