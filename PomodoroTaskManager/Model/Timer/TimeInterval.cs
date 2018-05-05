using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PomodoroTaskManager.Model.Timer {
    public class TimeInterval {

        int _pomodoroInterval = 25;
        [JsonProperty("PomodoroInterval")]
        public int PomodoroInterval {
            get { return _pomodoroInterval; }
            set { _pomodoroInterval = value; }
        }

        int _breakInterval = 5;
        [JsonProperty("BreakInterval")]
        public int BreakInterval {
            get { return _breakInterval; }
            set { _breakInterval = value; }
        }

        int _longBreakInterval = 15;
        [JsonProperty("LongBreakInterval")]
        public int LongBreakInterval {
            get { return _longBreakInterval; }
            set { _longBreakInterval = value; }
        }

        private readonly string CONFIG_FILE_PATH;

        public TimeInterval() {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dirPath = Path.GetDirectoryName(exePath);
            CONFIG_FILE_PATH = dirPath + @"\PomodoroTaskManagerConfig.json";
        }

        public void SaveJson() {
            var json = JsonConvert.SerializeObject(this);
            using (System.IO.StreamWriter sw =
                new System.IO.StreamWriter(CONFIG_FILE_PATH,
                    false,
                    System.Text.Encoding.UTF8)) {
                sw.WriteLine(json);
            }
        }

        public void LoadJson() {
            if (File.Exists(CONFIG_FILE_PATH) == false) return;
            using (System.IO.StreamReader sr =
                new System.IO.StreamReader(CONFIG_FILE_PATH)) {
                string json = sr.ReadToEnd();
                var deserialized = JsonConvert.DeserializeObject<TimeInterval>(json);
                _pomodoroInterval = deserialized.PomodoroInterval;
                _breakInterval = deserialized.BreakInterval;
                _longBreakInterval = deserialized.LongBreakInterval;
            }
        }
    }
}
