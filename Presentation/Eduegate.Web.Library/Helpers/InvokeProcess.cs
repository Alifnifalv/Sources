using Eduegate.Domain;
using System.Diagnostics;

namespace Eduegate.Web.Library.Helpers
{
    public class InvokeProcess
    {
        public static string Process(string arg)
        {
            string line = string.Empty;
            var reportGenerator = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportGenerator");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = reportGenerator,
                    Arguments = arg,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };             
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                line = process.StandardOutput.ReadLine();
            }

            process.WaitForExit();
            return line;
        }
    }
}
