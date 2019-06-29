using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManagerDataModel.DataBase
{
    public interface DBConnectionWrapper : IDisposable
    {
        void Close();

        DbTransaction CreateTransaction();
        IEnumerable<T> Query<T>(string query);

        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>
            (string query, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        int Execute(string sql, object param, DbTransaction transaction);
    }
}
