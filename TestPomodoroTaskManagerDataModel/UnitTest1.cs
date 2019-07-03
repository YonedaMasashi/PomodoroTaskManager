using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomodoroTaskManagerDataModel.DataBase.SQLite;
using PomodoroTaskManagerDataModel.DataBase;
using PomodoroTaskManager.Model.Entity;

namespace TestPomodoroTaskManagerDataModel
{
    [TestClass]
    public class UnitTest1
    {
        const string baseTestDbPath = "../../テスト用_PomodoroTasks.db";

        private void CopyTestDB(string testDBPath)
        {
            if (File.Exists(testDBPath) == true)
            {
                File.Delete(testDBPath);
            }
            File.Copy(baseTestDbPath, testDBPath);
        }

        [TestMethod]
        public void TestSelect()
        {
            DBConnectionWrapper wrapper = null;
            using (wrapper = new SqliteConnectWrapper("PomodoroTasks.db"))
            {

                // One to One
                {
                    var query = "SELECT * FROM Tasks";

                    var result = wrapper.Query<Tasks>(query);

                    foreach (var p in result)
                    {
                        Debug.WriteLine($"ID: {p.TaskId}  Name: {p.TaskName}");
                    }
                }

                // On to Many
                {
                    string query = "SELECT * FROM Pomodoros AS P INNER JOIN Tasks AS T ON P.TaskId = T.TaskId;";
                    var pomodoros = wrapper.Query<Pomodoros, Tasks, Pomodoros>(
                            query,
                            (pomodoro, task) =>
                            {
                                pomodoro.Task = task;
                                return pomodoro;
                            },
                            splitOn: "TaskId");
                    foreach (var pomodoro in pomodoros)
                    {
                        Debug.WriteLine($"TaskID: {pomodoro.PomodoroId} TaskName:{pomodoro.Task.TaskName}");
                    }
                }

                // 中間テーブル
                {
                    string query = "";
                    query += "SELECT * FROM (Tasks AS T INNER JOIN TaskTagMap AS TTM ON T.TaskId = TTM.TaskId) ";
                    query += "INNER JOIN Categories AS C ON TTM.CategoryId = C.CategoryId;";

                    var taskDictionary = new Dictionary<int, Tasks>();
                    var list = wrapper.Query<Tasks, Categories, Tasks>(
                        query,
                        (task, category) =>
                        {
                            Tasks taskEntry;
                            if (!taskDictionary.TryGetValue(task.TaskId, out taskEntry))
                            {
                                taskEntry = task;
                                taskEntry.Categories = new List<Categories>();
                                taskDictionary.Add(taskEntry.TaskId, taskEntry);
                            }
                            taskEntry.Categories.Add(category);
                            return taskEntry;
                        },
                        splitOn: "TaskId,CategoryId").Distinct();

                    foreach (var task in list)
                    {
                        Debug.WriteLine($"TaskId:{task.TaskId} TaskName:{task.TaskName}:");
                        foreach(var cat in task.Categories)
                        {
                            Debug.WriteLine($"    CategoryId:{cat.CategoryId} CategoryName:{cat.CategoryName}");
                        }
                    }
                }

                wrapper.Close();
            }
        }


        [TestMethod]
        public void TestInsert()
        {
            string testDbPath = "./InsertPomodoro.db";
            CopyTestDB(testDbPath);

            using (SqliteConnectWrapper wrapper = new SqliteConnectWrapper(testDbPath))
            {
                using (var tran = wrapper.CreateTransaction())
                {
                    var category = new Categories();
                    category.CategoryName = "TestInsert1";
                    category.Description = "TestInsert1";

                    try
                    {

                        string sql = "insert into Categories(CategoryName, Description) values(@CategoryName, @Description)";
                        wrapper.Execute(sql, category, tran);

                        tran.Commit();
                        
                    } catch (Exception e)
                    {
                        Debug.Write(e.Message);
                        tran.Rollback();
                    }

                    // 確認
                    string query = "";
                    query += "SELECT * FROM Categories;";

                    var result = wrapper.Query<Categories>(query);

                    var findElem = result.Where(elem => elem.CategoryName == category.CategoryName).Single();

                    Assert.AreEqual(category.CategoryName, findElem.CategoryName);
                    Assert.AreEqual(category.Description, findElem.Description);
                }
            }
        }
    }
}

