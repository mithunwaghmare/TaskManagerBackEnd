using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace TaskManagerDataLayer
{
    public class TaskManagerContext :DbContext
    {
        public TaskManagerContext() :base("name=TaskManagerContext")
        {

        }
        public virtual DbSet<Task> tasks { get; set; }

    }
}
