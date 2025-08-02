using Eduegate.AI.Scheduler.TimeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Enumerable;

namespace Eduegate.AI.Scheduler.TimeTable
{
    public class Population
    {
        public Population(int count, ScheduleData data)
        {
            Schedules = new List<Schedule>();
            foreach (var index in Range(0, count))
            {
                Schedules.Add(new Schedule(data).Initialize());
            }
        }

        public List<Schedule> Schedules { get; set; }
        public Population SortByFitness()
        {
            Schedules.Sort((sc1, sc2) =>
            {
                int returnValue = 0;
                if (sc1.GetFitness() < sc2.GetFitness()) returnValue = -1;
                else if (sc1.GetFitness() < sc2.GetFitness()) returnValue = 1;
                return returnValue;
            });
            return this;
        }
    }
}
