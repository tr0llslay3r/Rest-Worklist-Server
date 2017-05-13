using System;
using System.IO;
using Dicom.Network;
using DicomCore;
using Microsoft.AspNetCore.Hosting;

namespace Worklist_Server
{
    public class Program
    {
        private static int worklistport;
        public static void Main(string[] args)
        {
            if (args.Length != 1)
                worklistport = 13337;
            else
                worklistport = Convert.ToInt32(args[0]);

            StartDicomServer();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("wwwroot")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        private static void StartDicomServer()
        {
            Configuration.StationName = "EL_STATIONE";

            var m_server = DicomServer.Create<CFindSCP>(worklistport);
        }
    }
}
