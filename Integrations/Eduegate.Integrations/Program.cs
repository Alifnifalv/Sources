using Eduegate.Integrations.Engine;
using System.Linq;

namespace Eduegate.Integrations
{
    class Program
    {
        static void Main(string[] args)
        {
            //ClassSectionSync.Sync();
            //SyncStudent.Sync();
            var parameter = ExtractArgs(args);
            if (parameter.MigrateStudentProfile)
            {
                SyncStudent.SyncStudentProfie(parameter);
            }
        }

        private static ArgParameter ExtractArgs(string[] args)
        {
            var parameter = new ArgParameter();
            //if (args.Contains("-stdprfl"))
            //{
                parameter.MigrateStudentProfile = true;

                //if (args.ToList().Any(x => x.Contains("-serverpath")))
                //{
                //    parameter.ServerPath = args.FirstOrDefault(x => x.Contains("-serverpath"));
                    parameter.ServerPath = "https://api.mograsys.com/kindoapi/Content/pearl/Images/StudentImages";///parameter.ServerPath.Substring(12);
                //}

                //if (args.ToList().Any(x => x.Contains("-localpath")))
                //{
                //    parameter.LocalPath = args.FirstOrDefault(x => x.Contains("-localpath"));
                    parameter.LocalPath = @"C:\\Vineetha\\Migration\\Images\\";//parameter.LocalPath.Substring(12);
            //    }
            //}

            //if (args.Contains("-sourcedb"))
            //{
                parameter.MigrateTopics = true;
            //}
            return parameter;
        }
    }
}
