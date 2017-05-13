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
        private readonly WorklistRepository m_WorklistRepository;

        public WorklistItemsController()
        {
            m_WorklistRepository = new WorklistRepository();
        }
        // GET: api/worklistitems
        [HttpGet]
        public IEnumerable<Dictionary<string, string>> Get()
        {
            return m_WorklistRepository.GetReadableWorklistItems();
        }

        // GET api/worklistitems/5
        [HttpGet("{id}")]
        public Dictionary<string, string> Get(string id)
        {
            return m_WorklistRepository.GetReadableWorklistItems().SingleOrDefault(i => i["PatientID"] == id);
        }

        // POST api/worklistitems
        [HttpPost]
        public void Post([FromBody]Dictionary<string, string> value)
        {
            m_WorklistRepository.GetReadableWorklistItems().Add(value);
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
