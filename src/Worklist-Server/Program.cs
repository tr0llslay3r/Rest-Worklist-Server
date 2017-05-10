using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Dicom;
using Dicom.Network;
using DicomCore;
using Microsoft.AspNetCore.Hosting;

namespace Worklist_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
            var port = Convert.ToInt32(5104);
            var count = Convert.ToInt32(10);
            Configuration.MyCollection = new ObservableCollection<DicomConvertedItem>();
            Configuration.LogList = new ObservableCollection<LogItem>();
            Configuration.WorklistItems = RandomStuffFactory.CreateRandomDatasetList(count);
            foreach (var worklistItem in Configuration.WorklistItems)
            {
                var wi = new DicomConvertedItem
                {
                    Name = worklistItem.Get<string>(DicomTag.PatientName),
                    Id = worklistItem.Get<string>(DicomTag.PatientID),
                    Status = worklistItem.Get(DicomTag.PerformedProcedureStepStatus, "UNKNOWN"),
                    ScheduledDate = worklistItem.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).First().Get<string>(DicomTag.ScheduledProcedureStepStartDate),
                    ScheduledStationName = worklistItem.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).First().Get<string>(DicomTag.ScheduledStationName)
                };
                Configuration.MyCollection.Add(wi);
            }
            var m_server = DicomServer.Create<CFindSCP>(13337);
        }
    }
}
