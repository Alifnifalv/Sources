using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eduegate.Integrations.Engine.Helper
{
    public interface IClassSectionMigration
    {
        public List<ClassDTO> GetClasses();
        public List<SectionDTO> GetSections();
    }
}
