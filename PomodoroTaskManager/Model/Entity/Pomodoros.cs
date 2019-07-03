using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManager.Model.Entity
{
    public class Pomodoros
    {
        public int PomodoroId { get; set; }
        public Tasks Task { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Memo { get; set; }

    }
}
