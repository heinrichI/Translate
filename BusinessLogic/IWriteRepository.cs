using System;

namespace BusinessLogic
{
    public interface IWriteRepository : IDisposable
    {
        int Update(string id, string translate);
    }
}
