using BusinessLogic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class WriteRepository : IWriteRepository
    {
        private readonly Option _option;
        SqlConnection _cnn;

        public WriteRepository(Option option)
        {
            _cnn = new SqlConnection(option.ConnectionString);
            _cnn.Open();
            this._option = option;
        }

        public void Dispose()
        {
            _cnn.Close();
            _cnn.Dispose();
        }

        public int Update(string id, string translate)
        {
            using (SqlCommand update = new SqlCommand($"UPDATE {_option.TableName} SET {_option.ColumnName}= @translate WHERE {_option.IdColumn}=@id", _cnn))
            {
                update.Parameters.Add("@translate", SqlDbType.NVarChar).Value = translate;
                update.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                int updated = update.ExecuteNonQuery();
                return updated;
            }
        }
    }
}
