using PomodoroTaskManager.Model.Timer;
using PomodoroTaskManager.Presentation.ViewModel.Command;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace PomodoroTaskManager.Presentation.ViewModel {
    public class SettingsVM : ViewModelBase {

        TimeInterval _timeInterval;

        public SettingsVM(TimeInterval timeInterval)
            : base(Messenger.Default) {

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
