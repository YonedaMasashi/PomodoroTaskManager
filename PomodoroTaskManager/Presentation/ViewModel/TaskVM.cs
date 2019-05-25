using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManager.Presentation.ViewModel
{
    public class TaskVM : ViewModelBase
    {
        private string _TaskName = "";
        public string TaskName {
            get { return _TaskName; }
            set { _TaskName = value; }
        }

        private int _TaskId = 0;
        public int TaskId {
            get { return _TaskId; }
            set { _TaskId = value; }
        }

        private string _CategoryName = "";
        public string CategoryName {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private int _CategoryId = 0;
        public int CategoryId {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public TaskVM() : base(Messenger.Default)
        {
        }

    }
}
