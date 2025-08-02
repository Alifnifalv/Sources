using Eduegate.Integrations.Engine.DbContexts;
using Eduegate.Integrations.Engine.Helper;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eduegate.Integrations.Adapters.BMS
{
    public class ClassSectionMigrator : IClassSectionMigration
    {
        public List<ClassDTO> GetClasses()
        {
            using (var dbContext = new IntegrationDbContext())
            {

                var dtos = new List<ClassDTO>();
                foreach (var student in
                    dbContext.Students.FromSqlRaw("Select * from [sims].[sims_admission_classes]").ToList())
                {
                    dtos.Add(new ClassDTO()
                    {
                    });
                }

                return dtos;
            }
        }

        public List<SectionDTO> GetSections()
        {
            using (var dbContext = new IntegrationDbContext())
            {
                var dtos = new List<SectionDTO>();
                foreach (var student in
                    dbContext.Students.FromSqlRaw("Select sims_student_enroll_number StudentCode, " +
                        "sims_student_passport_first_name_en as FirstName from sims.sims_student").ToList())
                {
                    dtos.Add(new SectionDTO()
                    {
                    });
                }

                return dtos;
            }
        }
    }
}
