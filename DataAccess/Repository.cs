using BusinessLogic;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private readonly Option _option;
        SqlConnection _cnn;
        SqlCommand _command;

        public Repository(Option option)
        {
            _cnn = new SqlConnection(option.ConnectionString);
            this._option = option;
        }

        public void Dispose()
        {
            _command.Dispose();
            _cnn.Close();
            _cnn.Dispose();
        }

        public IEnumerable<Row> GetRow()
        {
            SqlDataReader dataReader;

            _cnn.Open();
            _command = new SqlCommand($"SELECT {_option.IdColumn}, {_option.ColumnName} FROM {_option.TableName}", _cnn);

            dataReader = _command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    string id = dataReader[_option.IdColumn].ToString();
                    string name = dataReader[_option.ColumnName].ToString();


                    yield return new Row(id, name);
                }
            }

            dataReader.Close(); //All data is fetched, Close the datareader in order to be able to run the update command
        }
    }
}
