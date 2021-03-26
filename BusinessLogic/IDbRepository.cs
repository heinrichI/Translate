using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IDbRepository : IDisposable
    {
        IList<string> GetTables();

        IList<string> GetCharColumns(string tableName);

        string GetIdentity(string tableName);
    }
}