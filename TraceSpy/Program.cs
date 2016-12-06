using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TraceSpy
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string log4netConfigFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\log4net.xml";

            log4net.Config.XmlConfigurator.Configure(new FileInfo(log4netConfigFilePath));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
