using PomodoroTaskManager.Model.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroTaskManager.ViewModel.Command {
    public class SettingSaveCommand : ICommand {

        private readonly TimeInterval _timeInterval;

        public SettingSaveCommand(TimeInterval timeInterval) {
            _timeInterval = timeInterval;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _timeInterval.SaveJson();
        }
    }
}
