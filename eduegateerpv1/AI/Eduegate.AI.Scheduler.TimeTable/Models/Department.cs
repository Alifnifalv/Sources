using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable.Models
{
    public class Department
    {
        public Department()
        {
            Subjects = new List<Subject>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
