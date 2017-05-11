using Microsoft.AspNetCore.Mvc;
using Dicom;
using DicomCore;
using System.Linq;
using System.Text;
using Dicom.Log;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Worklist_Server.Controllers
{
    [Route("api/[controller]")]
    public class DicomDumpController : Controller
    {
        // GET: api/dicomdump
        [HttpGet]
        public string Get()
        {
            var datasets = Configuration.WorklistItems.ToList();
            
            var log = new StringBuilder();

            foreach (var dataset in datasets)
            {
                log.AppendLine("-------------------------------------------------------------------------------------------------");
                IDicomDatasetWalker walkerTexasRanger = new DicomDatasetDumper(log);

                var a = new DicomDatasetWalker(dataset);
                a.Walk(walkerTexasRanger);
            }
            
            return log.ToString();
        }

        // GET api/dicomdump/id
        [HttpGet("{id}")]
        public string Get(string id)
        {
            var dataset = Configuration.WorklistItems.FirstOrDefault(i => i.Get<string>(DicomTag.PatientID) == id);
            
            var log = new StringBuilder();
            IDicomDatasetWalker walkerTexasRanger = new DicomDatasetDumper(log);
            var a = new DicomDatasetWalker(dataset);
            a.Walk(walkerTexasRanger);


            return log.ToString();
        }
    }
}
