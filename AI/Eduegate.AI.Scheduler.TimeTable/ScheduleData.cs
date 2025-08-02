using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable.Models
{
    public class ScheduleData
    {
        public ScheduleData()
        {
            Rooms = new List<Room>();
            Teachers = new List<Teacher>();
            TimeSerieses = new List<TimeSeries>();
            Subjects = new List<Subject>();
            Departments = new List<Department>();
            Intialize();
        }

        public List<Room> Rooms { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<TimeSeries> TimeSerieses { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Department> Departments { get; set; }

        public ScheduleData Intialize()
        {
            Rooms.Add(new Room() { Code = "R1", Name = "Room 1", SeatingCapacity = 21 });
            Rooms.Add(new Room() { Code = "R2", Name = "Room 2", SeatingCapacity = 45 });
            Rooms.Add(new Room() { Code = "R3", Name = "Room 3", SeatingCapacity = 35 });

            TimeSerieses.Add(new TimeSeries() { Id = "MT1", Time = "MT1 09:00 - 10:00" });
            TimeSerieses.Add(new TimeSeries() { Id = "MT2", Time = "MT2 10:00 - 11:00" });
            TimeSerieses.Add(new TimeSeries() { Id = "MT3", Time = "MT3 11:00 - 12:00" });
            TimeSerieses.Add(new TimeSeries() { Id = "MT4", Time = "MT4 12:00 - 13:00" });

            Teachers.Add(new Teacher() { Code = "T1", Name = "James" });
            Teachers.Add(new Teacher() { Code = "T2", Name = "Mike" });
            Teachers.Add(new Teacher() { Code = "T3", Name = "Steve" });
            Teachers.Add(new Teacher() { Code = "T4", Name = "Jane" });

            Subjects.Add(new Subject() { Code = "MAT", Name = "Mathematics", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T1" } } });
            Subjects.Add(new Subject() { Code = "ENG", Name = "English", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T2" } } });
            Subjects.Add(new Subject() { Code = "SSC", Name = "Social Science", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T3" } } });
            Subjects.Add(new Subject() { Code = "ARB", Name = "Arabic", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T4" } } });
            Subjects.Add(new Subject() { Code = "BIO", Name = "Biology", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T1" } } });
            Subjects.Add(new Subject() { Code = "CS", Name = "Computer Science", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T2" } } });
            Subjects.Add(new Subject() { Code = "HIS", Name = "History", MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T3" } } });
            Subjects.Add(new Subject() { Code = "GEO", Name = "Geography" , MaxNumberOfStudents = 25, Teachers = new List<Teacher>() { new Teacher() { Code = "T4" } } });

            Departments.Add(new Department() { Code = "Default", Subjects = Subjects });
            return this;
        }
    }
}
