using Eduegate.Domain.Mappers.School.Academics;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Integrations.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Eduegate.Integrations.Engine
{
    public class ClassSectionSync
    {
        public static void Sync()
        {
            var classFactory = IntegratorFactory.GetClassSectionFactory(ConfigurationManager.AppSettings["Client"]);
            var classes = classFactory.GetClasses();
            var mapper = ClassMapper.Mapper(null);

            foreach (var studentClass in classes)
            {
                mapper.SaveEntity(studentClass);
            }
        }
    }
}
