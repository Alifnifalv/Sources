using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable.Models
{
    public class Class
    {
        public Class(int id, Department dpt, Subject sbj)
        {
            Code = id.ToString();
            Teacher = new Teacher();
            TimeSeries = new TimeSeries();
            Subject = sbj;
            Department = dpt;
            Room = new Room();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int MaxNumberOfStudent { get; set; }
        public Teacher Teacher { get; set; }
        public Department Department { get; set; }
        public Subject Subject { get; set; }
        public TimeSeries TimeSeries { get; set; }
        public Room Room { get; set; }
    }
}
