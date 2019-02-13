using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerDataLayer
{
    [Table("TaskDetails")]
    public class Task
    {
        [Key]
        public int TaskID { get; set; }
        public int ParentID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public DateTime ? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public short Priority { get; set; }
      

    }
}
