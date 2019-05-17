using SqlKata.Compilers;
using SqlKata.Execution;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlKataSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SQLiteConnection("Data Source=MyDb.db");
            var compiler = new SqliteCompiler();

            var db = new QueryFactory(connection, compiler);
            var sqlResult = db.Query("Users").Where("Id", 1).Where("Status", "Active").First();
            var user = db.Query("Users").Select("Id", "Status").Where("Id", 1).Where("Status", "Active").Get<Entity.User>();

        }
    }
}
