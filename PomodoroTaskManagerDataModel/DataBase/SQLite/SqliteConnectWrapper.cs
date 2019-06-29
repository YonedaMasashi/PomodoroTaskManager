using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PomodoroTaskManagerDataModel.DataBase.SQLite
{
    public class SqliteConnectWrapper : DBConnectionWrapper
    {

        private SQLiteConnection _DbConnection;

        public SqliteConnectWrapper(string dbConnectString)
        {
            _DbConnection = new SQLiteConnection("Data Source=" + dbConnectString);
            _DbConnection.Open();
        }

        public void Dispose()
        {
            _DbConnection.Dispose();
        }

        public void Close()
        {
            _DbConnection.Close();
        }

        public DbTransaction CreateTransaction() {
            return _DbConnection.BeginTransaction();
        }

        public IEnumerable<T> Query<T>(string query)
        {
            return _DbConnection.Query<T>(query);
        }
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>
            (string query, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _DbConnection.Query<TFirst, TSecond, TReturn>(query, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public int Execute(string sql, object param, DbTransaction transaction)
        {
            return _DbConnection.Execute(sql, param, transaction);
        }
    }
}
