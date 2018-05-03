using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManager.Model.Timer {
    public class TimeInterval {
        int _pomodoroInterval = 25;
        public int PomodoroInterval {
            get { return _pomodoroInterval; }
            set { _pomodoroInterval = value; }
        }

        int _breakInterval = 5;
        public int BreakInterval {
            get { return _breakInterval; }
            set { _breakInterval = value; }
        }

        int _longBreakInterval = 15;
        public int LongBreakInterval {
            get { return _longBreakInterval; }
            set { _longBreakInterval = value; }
        }

        public TimeInterval() {
        }

        public TimeInterval(int pomodoroInterval, int breakInterval, int longBreakInterval) {
            _pomodoroInterval = pomodoroInterval;
            _breakInterval = breakInterval;
            _longBreakInterval = longBreakInterval;
        }
    }
}
