using System;

namespace BusinessLogic
{
    public interface IWriteRepository : IDisposable
    {
        int Update(string id, string translate);

        int Update(string tableName, string identityColumn, string columnName, 
            string stringValue, object identity);
    }
}
