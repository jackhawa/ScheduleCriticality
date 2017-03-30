using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchedulePath.Repository;
using SchedulePath.Models;
using SchedulePath.Services;

namespace SchedulePath.Controllers
{
    public class PRocessController : Controller
    {
        private ICepRepository _repository;

        public PRocessController(ICepRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [Route("api/processes")]
        public IEnumerable<Process> Get()
        {
            return _repository.GetProcesses();
        }

        [HttpPost]
        [Route("api/processes")]
        public IActionResult Post([FromBody]Process activity)
        {
            _repository.AddProcess(activity);
            return Ok();
        }

        [HttpPut]
        [Route("api/processes")]
        public void Put([FromBody] Process request)
        {
            _repository.UpdateProcess(request);
        }

        [HttpDelete]
        [Route("api/processes/{id}")]
        public void Delete(int id)
        {
            _repository.DeleteProcess(id);
        }
    }
}
