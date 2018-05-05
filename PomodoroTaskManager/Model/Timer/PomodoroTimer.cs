using PomodoroTaskManager.DataTypeDef.Enum;
using PomodoroTaskManager.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PomodoroTaskManager.Model.Timer {

    public enum Em_TickKind {
        Normal,
        End
    }
    
    public class TimerTickEventArgs : EventArgs {

        private readonly string _Time;
        public string Time { get { return _Time; } }

        private readonly Em_TickKind _emTickKind;
        public Em_TickKind TickKind { get { return _emTickKind; } }

        public TimerTickEventArgs(string time, Em_TickKind emTickKind) {
            _Time = time;
            _emTickKind = emTickKind;
        }
    }

    public class PomodoroTimer : BindableBase {

        DispatcherTimer dispatcherTimer;    // タイマーオブジェクト
        DateTime StartTime;                 // カウント開始時刻
        TimeSpan nowtimespan;               // Startボタンが押されてから現在までの経過時間
        TimeSpan oldtimespan;               // 一時停止ボタンが押されるまでに経過した時間の蓄積

        TimeInterval _timeInterval;

        Em_Mode _emMode = Em_Mode.Stop;
        public Em_Mode EmMode {
            get { return _emMode; }
            set { SetProperty(ref _emMode, value); }
        }

        #region Event Delegate

        public delegate void TimerTickEventHandler(TimerTickEventArgs e);
        public event TimerTickEventHandler PomodoroTimerTickEventHandler;

        #endregion

        public PomodoroTimer(TimeInterval timeInterval) {
            _timeInterval = timeInterval;

            // タイマーのインスタンスを生成
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200); // 200msecごとに Tick を発火
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick); // Tick が発火した時に dispatcherTimer_Tick を起動
        }


        // タイマー Tick処理
        void dispatcherTimer_Tick(object sender, EventArgs e) {
            nowtimespan = DateTime.Now.Subtract(StartTime);
            // イベント発火
            string time = oldtimespan.Add(nowtimespan).ToString(@"mm\:ss");
            PomodoroTimerTickEventHandler(new TimerTickEventArgs(time, Em_TickKind.Normal));

            if (_emMode == Em_Mode.Pomodoro) {
                if (TimeSpan.Compare(oldtimespan.Add(nowtimespan), new TimeSpan(0, 0, _timeInterval.PomodoroInterval)) >= 0) {
                    StopTimer();
                    PomodoroTimerTickEventHandler(new TimerTickEventArgs(time, Em_TickKind.End));
                    ResetTimer();
                }
            } else if (_emMode == Em_Mode.Break) {
                if (TimeSpan.Compare(oldtimespan.Add(nowtimespan), new TimeSpan(0, 0, _timeInterval.BreakInterval)) >= 0) {
                    StopTimer();
                    PomodoroTimerTickEventHandler(new TimerTickEventArgs(time, Em_TickKind.End));
                    ResetTimer();
                }
            } else if (_emMode == Em_Mode.LongBreak) {
                if (TimeSpan.Compare(oldtimespan.Add(nowtimespan), new TimeSpan(0, 0, _timeInterval.LongBreakInterval)) >= 0) {
                    StopTimer();
                    PomodoroTimerTickEventHandler(new TimerTickEventArgs(time, Em_TickKind.End));
                    ResetTimer();
                }
            }
        }

        /// <summary>
        /// インターバル値を設定する
        /// </summary>
        /// <param name="pomodoroInterval"></param>
        /// <param name="brekInterval"></param>
        /// <param name="longBreakInterval"></param>
        public void SetIntervale(TimeInterval timeInterval) {
            _timeInterval = timeInterval;
        }

        /// <summary>
        /// タイマースタート
        /// </summary>
        public void StartTimer(Em_Mode emMode) {
            EmMode = emMode;
            StartTime = DateTime.Now;
            dispatcherTimer.Start();
        }

        /// <summary>
        /// タイマーストップ
        /// </summary>
        public void StopTimer() {
            oldtimespan = oldtimespan.Add(nowtimespan);
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// タイマーリセット
        /// </summary>
        public void ResetTimer() {
            oldtimespan = new TimeSpan();
            PomodoroTimerTickEventHandler(new TimerTickEventArgs("00:00", Em_TickKind.Normal));
        }

    }
}
