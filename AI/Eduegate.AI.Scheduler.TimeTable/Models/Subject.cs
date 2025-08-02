using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable.Models
{
    public class Subject
    {
        public Subject()
        {
            Teachers = new List<Teacher>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int MaxNumberOfStudents { get;set;}
        public List<Teacher> Teachers { get; set; }
    }
}
