using PomodoroTaskManager.Model.Repository;
using PomodoroTaskManager.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManager.Service
{
    public class CreateTaskService
    {

        // だれから注入してもらうか…
        TaskRepository taskRepository;


        public  CreateTaskService()
        {

        }

        public bool AddTask(string taskName, string categoryName)
        {
            var newTask = new Tasks();
            taskRepository.AddTask(newTask);

            return true;
        }
    }
}
