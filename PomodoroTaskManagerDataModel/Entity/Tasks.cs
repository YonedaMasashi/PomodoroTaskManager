using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTaskManagerDataModel.Entity
{
    public class Tasks
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Estimate { get; set; }
        public List<Categories> Categories { get; set; }

    }
}
