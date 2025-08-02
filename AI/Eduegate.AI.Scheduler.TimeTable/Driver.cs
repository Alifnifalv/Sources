using Eduegate.AI.Scheduler.TimeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable
{
    public class Driver
    {
        public static int POPULATION_SIZE = 9;
        public static double MUTATION_RATE = 0.1;
        public static double CROSSOVER_RATE = 0.9;
        public static int TOURNAMENT_SELECTION_SIZE = 3;
        public static int NUMBER_OF_ELITE_SCHEDULES = 1;
        private ScheduleData data;
        private int scheduleNumber = 0;
        private int classNumber = 1;

        public static void Start()
        {
            var drv = new Driver();
            drv.data = new ScheduleData();

            var geneticAlgorithm = new GeneticAlgorithm(drv.data);
            drv.classNumber = 1;

            var population = new Population(Driver.POPULATION_SIZE, drv.data).SortByFitness();
            while (population.Schedules[0].GetFitness() != 1.0)
            { 
                population.Schedules.ForEach(schedule =>
                {

                });

                population = geneticAlgorithm.Evolve(population).SortByFitness();
                drv.scheduleNumber = 0;
                //population = new Population(Driver.POPULATION_SIZE, drv.data).SortByFitness();
                drv.classNumber = 1;
            }
        }
    }
}
