using PomodoroTaskManager.DataTypeDef.Enum;
using PomodoroTaskManager.Model.Timer;
using PomodoroTaskManager.ViewModel.Base;
using PomodoroTaskManager.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private PomodoroTimer _pomodoroTimer;

        #endregion

        #region Command
        public PomodoroActionCommand PushedPomodoroActionButtonCommand { get; private set; }
        #endregion

        public EndPomodoroVM(PomodoroTimer pomodoroTimer) {
            _emMode = Em_Mode.Stop;
            _pomodoroTimer = pomodoroTimer;
            _pomodoroTimer.PropertyChanged += EmMode_PropertyChanged;
            PushedPomodoroActionButtonCommand = new PomodoroActionCommand(_pomodoroTimer);
        }

        /// <summary>
        /// Model の変更通知を受け取る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmMode_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "EmMode") {
                emMode = _pomodoroTimer.EmMode;
            }
        }
    }
}
