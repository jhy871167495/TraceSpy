using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TraceSpy
{
    public class Logger
    {
        private static Logger instance;
        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }

                return instance;
            }
        }

        private Timer logTimer;

        private StringBuilder logContainer;

        private Logger()
        {
            logContainer = new StringBuilder();
            logTimer = new Timer();

            logTimer.Interval = 30 * 60 * 1000;
            logTimer.Tick += new EventHandler(logTimer_Tick);
            logTimer.Start();
        }

        private void logTimer_Tick(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\log\\"))
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\log\\");

            var files = Directory.GetFiles(Application.StartupPath + "\\log\\");

            foreach (var filePath in files)
            {
                FileInfo file = new FileInfo(filePath);

                if (file.LastWriteTime < DateTime.Now.AddMonths(-1))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void WriteLogToFile()
        {
            try
            {
                string file = Application.StartupPath + "\\log\\" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                              DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ".txt";
                System.IO.File.AppendAllText(file, logContainer.ToString());
            }
            catch { }
            finally
            {
                logContainer = new StringBuilder();
            }
        }

        public void WriteLog(string str)
        {
            try
            {
                var directoryPath = Application.StartupPath + "\\log";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string file = Application.StartupPath + "\\log\\" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                              DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ".txt";
                System.IO.File.AppendAllText(file, str);
            }
            catch { }
            finally
            {
            }
        }
    }
}