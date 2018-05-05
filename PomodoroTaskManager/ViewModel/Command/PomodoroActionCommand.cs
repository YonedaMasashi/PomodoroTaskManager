using PomodoroTaskManager.Model.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroTaskManager.ViewModel.Command {
    public class PomodoroActionCommand : ICommand {

        private readonly PomodoroTimer _pomodoroTimer;

        public PomodoroActionCommand(PomodoroTimer pomodoroTimer) {
            _pomodoroTimer = pomodoroTimer;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            string pushedButtonName = (string)parameter;
            if (pushedButtonName == "StartPomodoro") {
                _pomodoroTimer.StartTimer(DataTypeDef.Enum.Em_Mode.Pomodoro);

            } else if (pushedButtonName == "Break") {
                _pomodoroTimer.StartTimer(DataTypeDef.Enum.Em_Mode.Break);

            } else if (pushedButtonName == "LongBreak") {
                _pomodoroTimer.StartTimer(DataTypeDef.Enum.Em_Mode.LongBreak);
            }
        }
    }
}
