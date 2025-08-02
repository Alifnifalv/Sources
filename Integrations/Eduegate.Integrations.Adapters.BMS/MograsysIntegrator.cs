using Eduegate.Integrations.Engine.Helper;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Common;
using Eduegate.Services.Contracts.School.Students;
using System.Collections.Generic;

namespace Eduegate.Integrations.Adapters.BMS
{
    public class MograsysIntegrator : IStudentMigration, IClassSectionMigration, ILoginMigrator
    {
        public List<ClassDTO> GetClasses()
        {
            return new ClassSectionMigrator().GetClasses();
        }

        public List<SectionDTO> GetSections()
        {
            return new ClassSectionMigrator().GetSections();
        }

        public List<StudentDTO> GetStudents()
        {
            return new StudentMigrator().GetStudents();
        }

        public List<LoginsDTO> GetLoginData()
        {
            return new LoginMigrator().GetLoginData();
        }
    }
}