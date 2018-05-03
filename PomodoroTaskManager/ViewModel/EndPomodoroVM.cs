using PomodoroTaskManager.DataTypeDef.Enum;
using PomodoroTaskManager.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroTaskManager.ViewModel {

    public class EndPomodoroVM : ViewModelBase {

        #region Property
        private Em_Mode _emMode;
        public Em_Mode emMode {
            get { return _emMode; }
            set {
                if (_emMode != value) {
                    _emMode = value;
                    RaisePropertyChanged("emMode");
                }
            }
        }

        #endregion

        #region Command
        public ICommand PushedStartPomodoroButtonCommand { get; private set; }
        public ICommand PushedBreakButtonCommand { get; private set; }
        public ICommand PushedSetBreakButtonCommand { get; private set; }
        #endregion

        public EndPomodoroVM() {
            _emMode = Em_Mode.Stop;
        }
    }
}
