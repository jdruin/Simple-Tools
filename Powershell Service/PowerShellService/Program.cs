using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Topshelf;

namespace PowerShellService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<scriptRunner>(s =>
                {
                    s.ConstructUsing(name => new scriptRunner());
                    s.WhenStarted(sr => sr.Start());
                    s.WhenStopped(sr => sr.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription(ConfigurationManager.AppSettings["ServiceDescription"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["DisplayName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["DisplayName"]);

            });
        }
    }
}
