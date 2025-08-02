using Eduegate.Services.Contracts.School.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eduegate.Integrations.Engine.Helper
{
    public interface IStudentMigration
    {
        public List<StudentDTO> GetStudents();
    }
}
