using System.Collections.Generic;
using DicomCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Worklist_Server.Controllers
{
    [Route("api/[controller]")]
    public class WorklistItemsController : Controller
    {
        // GET: api/worklistitems
        [HttpGet]
        public IEnumerable<DicomConvertedItem> Get()
        {
            return Configuration.MyCollection;
        }

        // GET api/worklistitems/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/worklistitems
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/worklistitems/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/worklistitems/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
