using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PomodoroTaskManager.Presentation.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManager.Presentation.ViewModel
{
    public class TaskListVM : ViewModelBase {

        public ObservableCollection<TaskVM> TaskList = new ObservableCollection<TaskVM>();

        private string _TaskName = "";
        public string TaskName {
            get { return _TaskName; }
            set { _TaskName = value; }
        }

        private string _CategoryName = "";
        public string CategoryName {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        #region Command
        public TaskListEditCommand PushedTaskListEditCommand { get; private set; }
        #endregion

        public TaskListVM() : base(Messenger.Default)
        {
            PushedTaskListEditCommand = new TaskListEditCommand();
        }
    }
}
