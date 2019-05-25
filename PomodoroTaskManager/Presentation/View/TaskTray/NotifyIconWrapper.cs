using PomodoroTaskManager.DataTypeDef.Enum;
using PomodoroTaskManager.Model.Timer;
using PomodoroTaskManager.Presentation.View.TaskList;
using PomodoroTaskManager.Presentation.View.Setting;
using PomodoroTaskManager.Presentation.View.Pomodoro;
using PomodoroTaskManager.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PomodoroTaskManager.Presentation.View.TaskTray {
    /// <summary>
    /// タスクトレイ通知アイコン
    /// </summary>
    public partial class NotifyIconWrapper : Component
    {
        private Em_Mode _emMode = Em_Mode.Stop;
        public Em_Mode EmMode {
            get { return _emMode; }
            set {
                _emMode = value;
                _endPomodoroVM.emMode = _emMode;
            }
        }

        // Model
        PomodoroTimer _pomodoroTime;
        TimeInterval _timeInterval;

        // View Model
        SettingsVM _settingsVM;
        EndPomodoroVM _endPomodoroVM;
        TaskListVM _taskListVM;


        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        public NotifyIconWrapper()
        {
            // コンポーネントの初期化
            InitializeComponent();

            // タイマーのインスタンスを生成
            _timeInterval = new TimeInterval();
            _timeInterval.LoadJson();
            _pomodoroTime = new PomodoroTimer(_timeInterval);
            _pomodoroTime.PomodoroTimerTickEventHandler += new PomodoroTimer.TimerTickEventHandler(CallBackEventProgress);

            // Window の初期化
            _settingsVM = new SettingsVM(_timeInterval);
            _endPomodoroVM = new EndPomodoroVM(_pomodoroTime);
            _taskListVM = new TaskListVM();

            // コンテキストメニューのイベントを設定
            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
            this.toolStripMenuItem_Start.Click += this.toolStripMenuItem_Start_Click;
            this.toolStripMenuItem_Break.Click += this.toolStripMenuItem_Break_Click;
            this.toolStripMenuItem_LongBreak.Click += this.toolStripMenuItem_LongBreak_Click;
            this.toolStripMenuItem_Settings.Click += this.toolStripMenuItem_Settings_Click;
            this.toolStripMenuItem_TaskEdit.Click += this.toolStripMenuItem_TaskEdit_Click;

            // TextBox の初期化
            toolStripMenuItem_TimeText.Text = "00:00";

        }

        /// <summary>
        /// コンテナ を指定して NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        /// <param name="container">コンテナ</param>
        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// タイマーの Tick のコールバック
        /// </summary>
        /// <param name="e"></param>
        private void CallBackEventProgress(TimerTickEventArgs e) {
            if (e.TickKind == Em_TickKind.Normal) {
                toolStripMenuItem_TimeText.Text = e.Time;

            } else {
                if (_emMode == Em_Mode.Pomodoro) {
                    toolStripMenuItem_Start.Enabled = true;
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
                    toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_TimeText.Image")));
                    EndPomodoroWindow endPomodoroWindow = new EndPomodoroWindow(_endPomodoroVM);
                    endPomodoroWindow.ShowDialog();

                } else if (_emMode == Em_Mode.Break) {
                    toolStripMenuItem_Break.Visible = true;
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
                    toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_TimeText.Image")));
                    EndPomodoroWindow endPomodoroWindow = new EndPomodoroWindow(_endPomodoroVM);
                    endPomodoroWindow.ShowDialog();

                } else if (_emMode == Em_Mode.LongBreak) {
                    toolStripMenuItem_LongBreak.Visible = true;
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
                    toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_TimeText.Image")));
                    EndPomodoroWindow endPomodoroWindow = new EndPomodoroWindow(_endPomodoroVM);
                    endPomodoroWindow.ShowDialog();

                }
            }
        }

        /// <summary>
        /// コンテキストメニュー "終了" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // 現在のアプリケーションを終了
            Application.Current.Shutdown();
        }

        /// <summary>
        /// コンテキストメニュー "Start Pomodoro" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Start_Click(object sender, EventArgs e) {
            EmMode = Em_Mode.Pomodoro;
            _pomodoroTime.ResetTimer();
            _pomodoroTime.StartTimer(_emMode);
            toolStripMenuItem_Start.Visible = false;
            toolStripMenuItem_Break.Visible = true;
            toolStripMenuItem_LongBreak.Visible = true;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_Start.Image")));
        } 

        /// <summary>
        /// コンテキストメニュー "Break Pomodoro" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Break_Click(object sender, EventArgs e) {
            EmMode = Em_Mode.Break;
            _pomodoroTime.ResetTimer();
            _pomodoroTime.StartTimer(_emMode);

            toolStripMenuItem_Start.Visible = true;
            toolStripMenuItem_Break.Visible = false;
            toolStripMenuItem_LongBreak.Visible = true;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_Break.Image")));
        }

        /// <summary>
        /// コンテキストメニュー "Long Break Pomodoro" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_LongBreak_Click(object sender, EventArgs e) {
            EmMode = Em_Mode.LongBreak;
            _pomodoroTime.ResetTimer();
            _pomodoroTime.StartTimer(_emMode);

            toolStripMenuItem_Start.Visible = true;
            toolStripMenuItem_Break.Visible = true;
            toolStripMenuItem_LongBreak.Visible = false;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_LongBreak.Image")));
        }

        /// <summary>
        /// コンテキストメニュー "Task Edit" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_TaskEdit_Click(object sender, EventArgs e) {
            var taskListWindow = new TaskListWindow(_taskListVM);
            taskListWindow.ShowDialog();
        }

        /// <summary>
        /// コンテキストメニュー "Settings..." を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            SettingsWindow settingWindow = new SettingsWindow(_settingsVM);
            settingWindow.ShowDialog();
        }
    }
}
