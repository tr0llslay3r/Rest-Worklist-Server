using System.Collections.Generic;
using DicomCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Worklist_Server.Controllers
{
    [Route("api/[controller]")]
    public class WorklistItemsController : Controller
    {
        // GET: api/worklistitems
        [HttpGet]
        public IEnumerable<Dictionary<string, string>> Get()
        {
            return Configuration.GetReadableWorklistItems();
        }

        // GET api/worklistitems/5
        [HttpGet("{id}")]
        public Dictionary<string, string> Get(string id)
        {
            return Configuration.GetReadableWorklistItems().SingleOrDefault(i => i["Id"] == id);
        }

        // POST api/worklistitems
        [HttpPost]
        public void Post([FromBody]Dictionary<string, string> value)
        {
            Configuration.GetReadableWorklistItems().Add(value);
        }

        // PUT api/worklistitems/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Dictionary<string, string> value)
        {
        }

        // DELETE api/worklistitems/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
