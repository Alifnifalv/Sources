using Eduegate.AI.Scheduler.TimeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable
{
    public class Schedule
    {
        public List<Class> Classes { get; set; }
        public bool IsFitnessChanged { get; set; }
        public double Fitness { get; set; }
        public int NumberOfConflicts { get; set; }
        private ScheduleData Data { get; set; }
        private int classNumber = 0;

        public Schedule(ScheduleData data)
        {
            IsFitnessChanged = true;
            Fitness = -1;
            Data = data;
            NumberOfConflicts = 0;
            Classes = new List<Class>();
        }

        public Schedule Initialize()
        {
            var random = new Random();

            Data.Departments.ForEach(dept =>
            {
                dept.Subjects.ForEach(subject =>
                {
                    var newClass = new Class(classNumber++, dept, subject);
                    newClass.TimeSeries = Data.TimeSerieses[(int)((Data.TimeSerieses.Count() - 1) * random.NextDouble())];
                    newClass.Room = Data.Rooms[(int)((Data.Rooms.Count - 1) * random.NextDouble())];
                    newClass.Teacher = Data.Teachers[(int)((Data.Teachers.Count - 1) * random.NextDouble())];
                    Classes.Add(newClass);
                });
            });

            return this;
        }

        public List<Class> GetClasses()
        {
            IsFitnessChanged = true;
            return Classes;
        }
        public double GetFitness()
        {
            if (IsFitnessChanged)
            {
                Fitness = CalculateFitness();
                IsFitnessChanged = false;
            }

            return Fitness;
        }

        public double CalculateFitness() {
            NumberOfConflicts = 0;
            Classes.ForEach(cls =>
            {
                if(cls.Room.SeatingCapacity < cls.Subject.MaxNumberOfStudents)
                {
                    NumberOfConflicts++;
                }

                Classes.Where(y => Classes.FindIndex(k => y.Code == k.Code) >= Classes.FindIndex(k => cls.Code == k.Code)).ToList()
                    .ForEach(x =>
                       {
                           if(x.TimeSeries == cls.TimeSeries && x.Code != cls.Code)
                           {
                               if(x.Room.Code == cls.Room.Code)
                               {
                                   NumberOfConflicts++;
                               }

                               if(x.Teacher.Code == cls.Teacher.Code)
                               {
                                   NumberOfConflicts++;
                               }
                           }
                       });
            });

            return 1/ (double)(NumberOfConflicts + 1);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
