using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PomodoroTaskManager.ViewModel.Base;
using PomodoroTaskManager.Model.Timer;
using PomodoroTaskManager.ViewModel.Command;

namespace PomodoroTaskManager.ViewModel {
    public class SettingsVM : ViewModelBase {

        TimeInterval _timeInterval;

        public SettingsVM(TimeInterval timeInterval) {
            _timeInterval = timeInterval;
            PushedSettingSaveCommand = new SettingSaveCommand(_timeInterval);
        }

        #region Command
        public SettingSaveCommand PushedSettingSaveCommand { get; private set; }
        #endregion

        public int PomodoroInterval {
            get { return _timeInterval.PomodoroInterval; }
            set {
                _timeInterval.PomodoroInterval = value;
                RaisePropertyChanged("PomodoroInterval");
            }
        }

        public int BreakInterval {
            get { return _timeInterval.BreakInterval; }
            set {
                _timeInterval.BreakInterval = value;
                RaisePropertyChanged("BreakInterval");
            }
        }

        public int LongBreakInterval {
            get { return _timeInterval.LongBreakInterval; }
            set {
                _timeInterval.LongBreakInterval = value;
                RaisePropertyChanged("LongBreakInterval");
            }
        }

    }
}
