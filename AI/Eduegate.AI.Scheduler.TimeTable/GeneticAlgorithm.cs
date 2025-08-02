using Eduegate.AI.Scheduler.TimeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Enumerable;

namespace Eduegate.AI.Scheduler.TimeTable
{
    public class GeneticAlgorithm
    {
        public ScheduleData ScheduleData { get; set; }
        public GeneticAlgorithm(ScheduleData data)
        {
            ScheduleData = data;
        }

        public Population Evolve(Population population)
        {
            return MutatePopulation(CrossOverPopulation(population));
        }

        public Population CrossOverPopulation(Population population)
        {
            var crossOverPopulation = new Population(population.Schedules.Count(), ScheduleData);

            foreach (var index in Range(0, Driver.NUMBER_OF_ELITE_SCHEDULES))
            {
                crossOverPopulation.Schedules[index] = population.Schedules[index];
            }

            var random = new Random();

            foreach (var index in Range(Driver.NUMBER_OF_ELITE_SCHEDULES, population.Schedules.Count()-1))
            {
                if(Driver.CROSSOVER_RATE > random.NextDouble())
                {
                    var schedule1 = SelectTournamentPopulation(population).SortByFitness().Schedules[0];
                    var schedule2 = SelectTournamentPopulation(population).SortByFitness().Schedules[0];
                    crossOverPopulation.Schedules[index] = CrossOverSchedule(schedule1, schedule2);
                }
                else
                    crossOverPopulation.Schedules[index] = population.Schedules[index];
            }

            return crossOverPopulation;
        }

        public Schedule CrossOverSchedule(Schedule schedule1, Schedule schedule2)
        {
            var crossOverSchedule = new Schedule(ScheduleData).Initialize();
            var random = new Random();

            foreach (var index in Range(0, crossOverSchedule.Classes.Count()))
            {
                if(random.NextDouble() > 0.5)
                    crossOverSchedule.Classes[index] = schedule1.Classes[index];
                else
                    crossOverSchedule.Classes[index] = schedule2.Classes[index];
            }

            return crossOverSchedule;
        }

       
        private Population SelectTournamentPopulation(Population population)
        {
            var tournamentPopulation = new Population(Driver.TOURNAMENT_SELECTION_SIZE, ScheduleData);

            var random = new Random();

            foreach (var index in Range(0, Driver.TOURNAMENT_SELECTION_SIZE - 1))
            {
                tournamentPopulation.Schedules[index] = population.Schedules[(int)(random.NextDouble()* (population.Schedules.Count()))];
            }

            return tournamentPopulation;
        }

        private Population MutatePopulation(Population population)
        {
            var mutualPopulation = new Population(population.Schedules.Count(), ScheduleData);
            var schedules = mutualPopulation.Schedules;

            foreach (var index in Range(0, Driver.NUMBER_OF_ELITE_SCHEDULES))
            {
                schedules[index] = population.Schedules[index];
            }

            foreach (var index in Range(Driver.NUMBER_OF_ELITE_SCHEDULES, population.Schedules.Count()-1))
            {
                schedules[index] = MutateSchedule(population.Schedules[index]);
            }

            return mutualPopulation;
        }

        private Schedule MutateSchedule(Schedule mutateSchedule)
        {
            var schedule = new Schedule(ScheduleData).Initialize();
            var random = new Random();

            foreach (var index in Range(0, mutateSchedule.Classes.Count()))
            {
                if(Driver.MUTATION_RATE > random.NextDouble())
                {
                    mutateSchedule.Classes[index] = schedule.Classes[index];
                }
            }

            return mutateSchedule;
        }
    }
}
