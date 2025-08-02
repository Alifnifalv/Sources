using Eduegate.Domain;
using Eduegate.Domain.Mappers.School.Academics;
using Eduegate.Domain.Payroll;
using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.AI.Scheduler.TimeTable.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Scheduler");
            //Driver.Start();
            var settingBL = new Domain.Setting.SettingBL(null);

            string teacherDesignationCode = settingBL.GetSettingValue<string>("DESIGNATION_CODE_TEACHER");

            var teachers = new EmployeeBL(null).GetEmployeesByDesignation(teacherDesignationCode);

            int classTimingSetID = settingBL.GetSettingValue<int>("CLASS_TIMING_SET_ID");

            var timeSeries = ClassTimingMapper.Mapper(null).GetClassTimingByClassSet(classTimingSetID);

            List<KeyValueDTO> classes = ClassMapper.Mapper(null).GetClassesBySchool(30);

            List<KeyValueDTO> subject = SubjectMapper.Mapper(null).GetSubjectByType(1);

            var geneticAlgorithm = new GeneticAlgorithmTimetable();

            geneticAlgorithm.SetDepartments(new string[] { "Test" });

            geneticAlgorithm.SetTeachers(teachers.Select(x => x.EmployeeCode).ToArray());
            geneticAlgorithm.SetTimeSeries(timeSeries.Select(x => x.TimingDescription).ToArray());
            geneticAlgorithm.SetRooms(classes.Select(x => x.Value).ToArray());
            geneticAlgorithm.SetSubjects(subject.Select(x => x.Value).ToArray());

            geneticAlgorithm.Start();
            System.Console.WriteLine("Press any key to continue");
            System.Console.ReadKey();
        }
    }
}