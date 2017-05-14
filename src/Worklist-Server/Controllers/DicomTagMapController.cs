using System.IO;
using DicomCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Worklist_Server.Controllers
{
    [Route("api/[controller]")]
    public class DicomTagMapController : Controller
    {
        // GET: api/dicomtagmap
        [HttpGet]
        public DicomTagMap Get()
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "tagmap.json"));
            var map = JsonConvert.DeserializeObject<DicomTagMap>(json);
            return map;
        }

        // POST api/dicomtagmap
        [HttpPost]
        public void Post([FromBody]DicomTagMap value)
        {
            var json = JsonConvert.SerializeObject(value);
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "tagmap.json"), json);
        }
    }
}
