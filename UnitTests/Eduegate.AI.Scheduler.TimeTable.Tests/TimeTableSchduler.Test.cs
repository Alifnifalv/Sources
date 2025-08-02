using System;
using System.Linq;
using Eduegate.Domain;
using Eduegate.Domain.Payroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eduegate.AI.Scheduler.TimeTable.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var settingBL = new Domain.Setting.SettingBL(null);

            string teacherDesignationCode = settingBL.GetSettingValue<string>("DESIGNATION_CODE_TEACHER");

            var teachers = new EmployeeBL(null).GetEmployeesByDesignation(teacherDesignationCode);

            var geneticAlgorithm = new GeneticAlgorithmTimetable();
           /* geneticAlgorithm.SetContext(teachers.Select(x=> x.EmployeeCode).ToArray());*/
            geneticAlgorithm.Start();
        }
    }
}
