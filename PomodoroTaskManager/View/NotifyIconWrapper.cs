using PomodoroTaskManager.DataTypeDef.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PomodoroTaskManager.View
{
    /// <summary>
    /// タスクトレイ通知アイコン
    /// </summary>
    public partial class NotifyIconWrapper : Component
    {
        DispatcherTimer dispatcherTimer;    // タイマーオブジェクト
        DateTime StartTime;                 // カウント開始時刻
        TimeSpan nowtimespan;               // Startボタンが押されてから現在までの経過時間
        TimeSpan oldtimespan;               // 一時停止ボタンが押されるまでに経過した時間の蓄積

        int pomodoroInterval = 25;
        int breakInterval = 5;
        int longBreakInterval = 15;

        Em_Mode emMode = Em_Mode.Stop;

        SettingsWindow settingWindow;


        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        public NotifyIconWrapper()
        {
            // コンポーネントの初期化
            InitializeComponent();

            // Window の初期化
            settingWindow = new SettingsWindow();

            // コンテキストメニューのイベントを設定
            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
            this.toolStripMenuItem_Start.Click += this.toolStripMenuItem_Start_Click;
            this.toolStripMenuItem_Break.Click += this.toolStripMenuItem_Break_Click;
            this.toolStripMenuItem_LongBreak.Click += this.toolStripMenuItem_LongBreak_Click;
            this.toolStripMenuItem_Settings.Click += this.toolStripMenuItem_Settings_Click;

            // TextBox の初期化
            toolStripMenuItem_TimeText.Text = "00:00";

            // タイマーのインスタンスを生成
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200); // 200msecごとに Tick を発火
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick); // Tick が発火した時に dispatcherTimer_Tick を起動
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

        // タイマー Tick処理
        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            nowtimespan = DateTime.Now.Subtract(StartTime);
            toolStripMenuItem_TimeText.Text = oldtimespan.Add(nowtimespan).ToString(@"mm\:ss");

            if (emMode == Em_Mode.Pomodoro)
            {
                if (TimeSpan.Compare(oldtimespan.Add(nowtimespan), new TimeSpan(0, 0, pomodoroInterval)) >= 0)
                {
                    MessageBox.Show("Pomodoro End");
                    TimerStop();
                    TimerReset();
                    toolStripMenuItem_Start.Enabled = true;
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
                    toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_TimeText.Image")));
                }
            }
            else if (emMode == Em_Mode.Break)
            {
                if (TimeSpan.Compare(oldtimespan.Add(nowtimespan), new TimeSpan(0, 0, breakInterval)) >= 0)
                {
                    MessageBox.Show("Break End");
                    TimerStop();
                    TimerReset();
                    toolStripMenuItem_Break.Visible = true;
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
                    toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_TimeText.Image")));
                }
            }
            else if (emMode == Em_Mode.LongBreak)
            {
                if (TimeSpan.Compare(oldtimespan.Add(nowtimespan), new TimeSpan(0, 0, longBreakInterval)) >= 0)
                {
                    MessageBox.Show("Long Break End");
                    TimerStop();
                    TimerReset();
                    toolStripMenuItem_LongBreak.Visible = true;
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
                    toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_TimeText.Image")));
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
        private void toolStripMenuItem_Start_Click(object sender, EventArgs e)
        {
            TimerReset();
            TimerStart();
            emMode = Em_Mode.Pomodoro;
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
        private void toolStripMenuItem_Break_Click(object sender, EventArgs e)
        {
            TimerReset();
            TimerStart();
            emMode = Em_Mode.Break;

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
        private void toolStripMenuItem_LongBreak_Click(object sender, EventArgs e)
        {
            TimerReset();
            TimerStart();
            emMode = Em_Mode.LongBreak;

            toolStripMenuItem_Start.Visible = true;
            toolStripMenuItem_Break.Visible = true;
            toolStripMenuItem_LongBreak.Visible = false;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            toolStripMenuItem_TimeText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_LongBreak.Image")));
        }

        /// <summary>
        /// コンテキストメニュー "Settings..." を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            settingWindow.TxtPomodoroInterval.Text = pomodoroInterval.ToString();
            settingWindow.TxtBreakInterval.Text = breakInterval.ToString();
            settingWindow.TxtLongBreakInterval.Text = longBreakInterval.ToString();
            settingWindow.ShowDialog();
            pomodoroInterval = int.Parse(settingWindow.TxtPomodoroInterval.Text);
            breakInterval = int.Parse(settingWindow.TxtBreakInterval.Text);
            longBreakInterval = int.Parse(settingWindow.TxtLongBreakInterval.Text);
        }

        // タイマー操作：開始
        private void TimerStart()
        {
            StartTime = DateTime.Now;
            dispatcherTimer.Start();
        }

        // タイマー操作：停止
        private void TimerStop()
        {
            oldtimespan = oldtimespan.Add(nowtimespan);
            dispatcherTimer.Stop();
        }

        // タイマー操作：リセット
        private void TimerReset()
        {
            oldtimespan = new TimeSpan();
            toolStripMenuItem_TimeText.Text = "00:00";
        }

    }
}
