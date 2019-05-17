using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManagerDataModel.DataBase
{
    interface DBConnectionWrapper : IDisposable
    {
        void Close();

        DbTransaction CreateTransaction();
        IEnumerable<T> Query<T>(string query);
    }
}
