using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduegate.ERP.Launcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(args.Contains("-i"))
            {
                //Application.Run(new EduegateInstallerWizard());
            }
            else
            {
                //Application.Run(new EduegateLaucher());
            }
        }
    }
}
