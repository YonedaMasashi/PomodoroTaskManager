using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroTaskManager.Presentation.ViewModel.Command
{
    class PomodoroMemoSaveCommand : ICommand {
        public PomodoroMemoSaveCommand() {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
        }
    }
}
