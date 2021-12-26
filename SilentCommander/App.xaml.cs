using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace SilentCommander
{
    public partial class App : Application
    {
        public static string[] Args;
        public static Dictionary<string, object> parsedArgs;
        void app_Startup(object sender, StartupEventArgs e)
        {
            parsedArgs = new Dictionary<string, object>();
            // If no command line arguments were provided, don't process them if (e.Args.Length == 0) return;
            parsedArgs = new Dictionary<string, object>();
            parsedArgs["waittime"] = -1f;
            parsedArgs["wait"] = false;
            if (e.Args.Length > 0)
            {
                Args = e.Args;
                for (int i = 0; i < Args.Length - 1; i++)
                {
                    switch (Args[i])
                    {
                        case "-w":
                            parsedArgs["wait"] = true;
                            break;
                        case "-t":
                            parsedArgs["waittime"] = int.Parse(Args[i + 1]);
                            i++; // skip next arg as we just read it
                            break;
                    }
                }
                parsedArgs.Add("cmd", Args[Args.Length - 1]);
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.Arguments = "/c " + "\"" + (string)App.parsedArgs["cmd"] + "\"";
                myProcess.EnableRaisingEvents = true;
                myProcess.Start();
                if ((bool)App.parsedArgs["wait"])
                {
                    if ((int)App.parsedArgs["waittime"] < 0)
                        myProcess.WaitForExit();
                    else
                        myProcess.WaitForExit((int)App.parsedArgs["waittime"]);
                }
            }
        }
    }
}
