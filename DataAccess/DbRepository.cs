using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class DbRepository : IDbRepository
    {
        SqlCommand _command;
        SqlConnection _cnn;
        private readonly Option _option;

        public DbRepository(Option option)
        {
            _cnn = new SqlConnection(option.ConnectionString);

            _cnn.Open();
            this._option = option;
        }

        public void Dispose()
        {
            _command.Dispose();
            _cnn.Close();
            _cnn.Dispose();
        }

        public IList<string> GetTables()
        {
            SqlDataReader dataReader;

            _command = new SqlCommand(
                 @"SELECT TABLE_NAME
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = @dbName
            ORDER BY TABLE_NAME", _cnn);
            _command.Parameters.AddWithValue("dbName", _option.DataBase);

            dataReader = _command.ExecuteReader();

            List<string> list = new List<string>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    string name = dataReader[0].ToString();

                    list.Add(name);
                }
            }

            dataReader.Close();

            return list;
        }

        public IList<string> GetCharColumns(string tableName)
        {
            SqlDataReader dataReader;

            _command = new SqlCommand(
                 @"SELECT COLUMN_NAME--, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @tableName
--ORDER BY ORDINAL_POSITION
AND DATA_TYPE = 'nvarchar'", _cnn);
            _command.Parameters.AddWithValue("tableName", tableName);

            dataReader = _command.ExecuteReader();

            List<string> list = new List<string>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    string name = dataReader[0].ToString();

                    list.Add(name);
                }
            }

            dataReader.Close();

            return list;
        }

        public string GetIdentity(string tableName)
        {
            SqlDataReader dataReader;

            _command = new SqlCommand(
                 @"SELECT NAME
FROM     SYS.IDENTITY_COLUMNS 
WHERE OBJECT_NAME(OBJECT_ID) = @tableName
AND OBJECT_SCHEMA_NAME(object_id) = 'dbo'", _cnn);
            _command.Parameters.AddWithValue("tableName", tableName);

            dataReader = _command.ExecuteReader();

            string identity = null;
            if (dataReader.HasRows)
            {
                if (dataReader.Read())
                {
                    identity = dataReader[0].ToString();
                }
            }

            dataReader.Close();

            return identity;
        }
    }
}
