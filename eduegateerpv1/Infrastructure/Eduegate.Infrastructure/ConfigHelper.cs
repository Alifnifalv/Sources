using System;
using System.Configuration;

namespace Eduegate.Infrastructure
{
    public class ConfigHelper
    {
        public static string GetDefaultConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["dbEduegateERPContext"] == null)
            {
                return Environment.GetEnvironmentVariable("dbEduegateERPContext");
            }
            else
            {
                return  ConfigurationManager.ConnectionStrings["dbEduegateERPContext"].ConnectionString;
            }
        }

        public static string GetSchoolConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["dbEduegateSchoolContext"] == null)
            {
                return Environment.GetEnvironmentVariable("dbEduegateSchoolContext");
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["dbEduegateSchoolContext"].ConnectionString;
            }
        }

        public static string GetDefaultLoggerConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["EduegatedERP_LoggerContext"] == null)
            {
                return Environment.GetEnvironmentVariable("EduegatedERP_LoggerContext");
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["EduegatedERP_LoggerContext"].ConnectionString;
            }
        }

        public static string GetIntegrationConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["integrationDbContext"] == null)
            {
                return Environment.GetEnvironmentVariable("integrationDbContext");
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["integrationDbContext"].ConnectionString;
            }
        }

        public static string GetContentConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["dbContentContext"] == null)
            {
                return Environment.GetEnvironmentVariable("dbContentContext");
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["dbContentContext"].ConnectionString;
            }
        }

        public static string GetBlinkConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["dbBlinkContext"] == null)
            {
                return Environment.GetEnvironmentVariable("dbBlinkContext");
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["dbBlinkContext"].ConnectionString;
            }
        }

        public static string GetWorkFlowConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["dbWorkflowERPContext"] == null)
            {
                return Environment.GetEnvironmentVariable("dbWorkflowERPContext");
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["dbWorkflowERPContext"].ConnectionString;
            }
        }

    }
}