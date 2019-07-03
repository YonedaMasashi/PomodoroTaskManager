using PomodoroTaskManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroTaskManager.Presentation.ViewModel.Command
{
    public class TaskListEditCommand : ICommand
    {
        public TaskListEditCommand()
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var taskListVM = (TaskListVM)parameter;

            CreateTaskService service = new CreateTaskService();
            service.AddTask(taskListVM.TaskName, taskListVM.CategoryName);
        }
    }
}
