using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.AI.Scheduler.TimeTable
{
    public class GeneticAlgorithmTimetable
    {
        // Parameters
        static string[] ROOMS = { "A", "B", "C", "D" };
        static string[] TEACHERS = { "T1", "T2", "T3" };
        static string[] TIME_SERIES = { "07:30-07:40", "07:40-08:20", "08:20-09:00", "09:00-09:25", "09:25-10:05", "10:05-10:40", "10:40-11:20", "11:20-12:00", "12:00-12:15", "12:15-12:55", "12:55-13:30", "13:30-13:40" };
        static string[] SUBJECTS = { "Maths", "Physics", "Chemistry" };
        static string[] DEPARTMENTS = { "D1", "D2" };

        // Chromosome representation
        static List<(string, string, string, string, string)> CreateChromosome()
        {
            var chromosome = new List<(string, string, string, string, string)>();
            var random = new Random();

            foreach (var room in ROOMS)
            {
                foreach (var timeSlot in TIME_SERIES)
                {
                    var classTuple = (room, TEACHERS[random.Next(TEACHERS.Length)], timeSlot,
                                      SUBJECTS[random.Next(SUBJECTS.Length)], DEPARTMENTS[random.Next(DEPARTMENTS.Length)]);

                    // Check if the class tuple already exists in the chromosome
                    while (chromosome.Contains(classTuple))
                    {
                        classTuple = (room, TEACHERS[random.Next(TEACHERS.Length)], timeSlot,
                                      SUBJECTS[random.Next(SUBJECTS.Length)], DEPARTMENTS[random.Next(DEPARTMENTS.Length)]);
                    }

                    chromosome.Add(classTuple);
                }
            }

            return chromosome;
        }

        // Fitness function
        static double CalculateFitness(List<(string, string, string, string, string)> chromosome)
        {
            var conflicts = 0;
            var teacherCounts = new Dictionary<string, int>();
            var departmentCounts = new Dictionary<string, int>();
            foreach (var teacher in TEACHERS)
            {
                teacherCounts[teacher] = 0;
            }
            foreach (var department in DEPARTMENTS)
            {
                departmentCounts[department] = 0;
            }
            for (int i = 0; i < chromosome.Count; i++)
            {
                for (int j = i + 1; j < chromosome.Count; j++)
                {
                    if (chromosome[i].Item1 == chromosome[j].Item1 && chromosome[i].Item3 == chromosome[j].Item3)
                    {
                        if (chromosome[i].Item2 == chromosome[j].Item2)
                        {
                            conflicts++;
                        }
                        if (chromosome[i].Item4 == chromosome[j].Item4)
                        {
                            conflicts++;
                        }
                    }
                }
                teacherCounts[chromosome[i].Item2]++;
                departmentCounts[chromosome[i].Item5]++;
            }
            var score = 1.0 / (conflicts + 1);
            score *= teacherCounts.Values.Sum() / TEACHERS.Length;
            score *= departmentCounts.Values.Sum() / DEPARTMENTS.Length;
            return score;
        }

        // Selection
        static List<List<(string, string, string, string, string)>> Selection(List<List<(string, string, string, string, string)>> population)
        {
            var tournamentSize = 3;
            var selected = new List<List<(string, string, string, string, string)>>();
            for (int i = 0; i < population.Count; i++)
            {
                var tournament = population.OrderBy(x => Guid.NewGuid()).Take(tournamentSize).ToList();
                var winner = tournament.OrderByDescending(x => CalculateFitness(x)).First();
                selected.Add(winner);
            }
            return selected;
        }

        // Crossover
        static List<(string, string, string, string, string)> Crossover(List<(string, string, string, string, string)> parent1,
            List<(string, string, string, string, string)> parent2)
        {
            var child = new List<(string, string, string, string, string)>();
            var midpoint = parent1.Count / 2;
            child.AddRange(parent1.Take(midpoint));
            foreach (var gene in parent2)
            {
                if (!child.Contains(gene))
                {
                    child.Add(gene);
                }
            }
            return child;
        }
        // Mutation
        static List<(string, string, string, string, string)> Mutation(List<(string, string, string, string, string)> chromosome)
        {
            var mutatedChromosome = new List<(string, string, string, string, string)>(chromosome);
            var index1 = new Random().Next(chromosome.Count);
            var index2 = new Random().Next(chromosome.Count);
            var gene1 = mutatedChromosome[index1];
            var gene2 = mutatedChromosome[index2];
            mutatedChromosome[index1] = gene2;
            mutatedChromosome[index2] = gene1;
            return mutatedChromosome;
        }

        // Generation
        static List<List<(string, string, string, string, string)>> Generate(int populationSize)
        {
            var population = new List<List<(string, string, string, string, string)>>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(CreateChromosome());
            }
            return population;
        }

        public void SetRooms(string[] rooms)
        {
            ROOMS = rooms;
        }

        public void SetTeachers(string[] teachers)
        {
            TEACHERS = teachers;
        }

        public void SetSubjects(string[] subjects)
        {
            SUBJECTS = subjects;
        }

        public void SetDepartments(string[] departments)
        {
            DEPARTMENTS = departments;
        }

        public void SetTimeSeries(string[] timeSeries)
        {
            TIME_SERIES = timeSeries;
        }

        public void Start()
        {
            var populationSize = 100;
            var generations = 100;
            var elitism = 0.1;
            var mutationRate = 0.1;

            var population = Generate(populationSize);

            for (int i = 0; i < generations; i++)
            {
                var sortedPopulation = population.OrderByDescending(x => CalculateFitness(x)).ToList();
                var nextGeneration = new List<List<(string, string, string, string, string)>>();

                // Elitism
                var elitismCount = (int)(elitism * populationSize);
                nextGeneration.AddRange(sortedPopulation.Take(elitismCount));

                // Selection and crossover
                var selectedParents = Selection(sortedPopulation);
                for (int j = 0; j < (populationSize - elitismCount); j++)
                {
                    var parent1 = selectedParents[new Random().Next(selectedParents.Count)];
                    var parent2 = selectedParents[new Random().Next(selectedParents.Count)];
                    var child = Crossover(parent1, parent2);
                    nextGeneration.Add(child);
                }

                // Mutation
                for (int j = elitismCount; j < populationSize; j++)
                {
                    if (new Random().NextDouble() < mutationRate)
                    {
                        var mutatedChromosome = Mutation(nextGeneration[j]);
                        nextGeneration[j] = mutatedChromosome;
                    }
                }

                population = nextGeneration;
            }

            // Print the final timetable
            var finalTimetable = population.OrderByDescending(x => CalculateFitness(x)).First();
            Console.WriteLine("Final timetable:");
            foreach (var room in ROOMS)
            {
                Console.WriteLine("Room " + room + ":");
                foreach (var timeSlot in TIME_SERIES)
                {
                    var classTuple = finalTimetable.First(x => x.Item1 == room && x.Item3 == timeSlot);
                    Console.WriteLine(timeSlot + " - " + classTuple.Item4 + " by " + classTuple.Item2 + " in " + classTuple.Item5);
                }
            }
        }
    }
}
