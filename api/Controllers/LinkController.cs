using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchedulePath.Models;
using SchedulePath.Services;

namespace SchedulePath.Controllers
{
    public class LinkController: Controller
    {
        private ILinkService _service;

        public LinkController(ILinkService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/links")]
        public Link Get()
        {
            return _service.GetLink();
        }
        

        [HttpPost]
        [Route("api/links")]
        public IActionResult Post([FromBody]Link request)
        {
            _service.AddLink(request);
            return Ok();
        }

        [HttpPut]
        [Route("api/links")]
        public void Put([FromBody] Link request)
        {
            _service.UpdateLink(request);
        }

        [HttpDelete]
        [Route("api/links/{id}")]
        public void Delete(int id)
        {
            _service.DeleteLink(id);
        }
    }
}
