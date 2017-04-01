using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SchedulePath.Models;
using SchedulePath.Services;

namespace SchedulePath.Controllers
{
    public class ActivityController : Controller
    {
        private IActivityService _service;
        
        public ActivityController(IActivityService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("api/activities")]
        public IEnumerable<Activity> Get()
        {
            return _service.GetActivities();
        }

        [HttpGet]
        [Route("api/activities/compute/{withCriticalPath}")]
        public GraphConfig Compute(bool withCriticalPath)
        {
            return _service.Process(withCriticalPath);
        }
        
        [HttpPost]
        [Route("api/activities")]
        public IActionResult Post([FromBody]ActivityRequest activity)
        {
            _service.AddActivity(activity);
            return Ok();
        }
        
        [HttpPut]
        [Route("api/activities")]
        public void Put([FromBody] ActivityRequest request)
        {
            _service.UpdateActivity(request);
        }
        
        [HttpDelete]
        [Route("api/activities/{id}")]
        public void Delete(int id)
        {
            _service.DeleteActivity(id);
        }
    }
}
