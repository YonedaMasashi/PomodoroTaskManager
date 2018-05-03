using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PomodoroTaskManager.ViewModel.Base;
using PomodoroTaskManager.Model.Timer;

namespace PomodoroTaskManager.ViewModel {
    public class SettingsVM : ViewModelBase {

        TimeInterval _timeInterval;

        public SettingsVM(TimeInterval timeInterval) {
            _timeInterval = timeInterval;
        }

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
