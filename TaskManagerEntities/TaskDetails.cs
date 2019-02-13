using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerEntities
{
    public class TaskDetails
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public DateTime ? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public short Priority { get; set; }
        

    }
}
