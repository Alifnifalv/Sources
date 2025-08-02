using Eduegate.Integrations.Adapters.BMS;
using Eduegate.Integrations.Engine.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eduegate.Integrations.Factory
{
    public class IntegratorFactory
    {
        static IStudentMigration _studentFactory;
        static IClassSectionMigration _classSectionFactory;
        static ILoginMigrator _loginFactory;

        public static IStudentMigration GetStudentFactory(string instance)
        {
            _studentFactory = GetInstance(instance) as IStudentMigration;
            return _studentFactory;
        }


        public static IClassSectionMigration GetClassSectionFactory(string instance)
        {
            _classSectionFactory = GetInstance(instance) as IClassSectionMigration;
            return _classSectionFactory;
        }

        public static ILoginMigrator GetLoginData(string instance)
        {
            _loginFactory = GetInstance(instance) as ILoginMigrator;
            return _loginFactory;
        }


        private static object GetInstance(string instance)
        {
            switch (instance)
            {
                case "mograsys":
                    return new MograsysIntegrator();
                default:
                    return new MograsysIntegrator();
            }
        }
    }
}
