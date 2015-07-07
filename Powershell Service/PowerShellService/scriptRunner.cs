using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Configuration;
using System.IO;
using System.Diagnostics;


namespace PowerShellService
{
    public class scriptRunner
    {
        readonly System.Timers.Timer _timer;
        private int _duration;


        public scriptRunner()
        {
            string stringTime = ConfigurationManager.AppSettings["Timer"];
            _duration = Convert.ToInt32(stringTime);
            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Run();
        }

        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }

        public static void Run()
        {
            using (var powerShellPipeLine = PowerShell.Create())
            {
                

                try
                {
                    string scriptPath = ConfigurationManager.AppSettings["ScriptPath"];

                    string script = File.ReadAllText(scriptPath);

                    powerShellPipeLine.AddScript(script);

                    powerShellPipeLine.Invoke();


                }
                catch (Exception except )
                {

                    EventLog.WriteEntry("PowerShell", except.Message, EventLogEntryType.Error);
                }

            }



        }


    }
}
