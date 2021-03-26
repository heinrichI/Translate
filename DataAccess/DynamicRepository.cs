using BusinessLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class DynamicRepository : IDynamicRepository
    {
        SqlConnection _cnn;
        SqlCommand _command;

        public DynamicRepository(Option option)
        {
            _cnn = new SqlConnection(option.ConnectionString);

            _cnn.Open();
        }

        public void Dispose()
        {
            _command.Dispose();
            _cnn.Close();
            _cnn.Dispose();
        }

        public IEnumerable<dynamic> GetDynamic(string[] columns, string tableName)
        {
            _command = new SqlCommand($"SELECT {string.Join(", ", columns)} FROM {tableName}", _cnn);
            using (var reader = _command.ExecuteReader())
            {
                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                foreach (IDataRecord record in reader as IEnumerable)
                {
                    var expando = new ExpandoObject() as IDictionary<string, object>;
                    foreach (var name in names)
                        expando[name] = record[name];

                    yield return expando;
                }
            }
        }

        public IEnumerable<IDataRecord> GetDataRecord(string[] columns, string tableName)
        {
            string query = $"SELECT {string.Join(", ", columns)} FROM {tableName}";
            //Console.WriteLine(query);
            _command = new SqlCommand(query, _cnn);
            using (var reader = _command.ExecuteReader())
            {
                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                foreach (IDataRecord record in reader as IEnumerable)
                {
                    yield return record; //yield return to keep the reader open
                }
            }
        }
    }
}
