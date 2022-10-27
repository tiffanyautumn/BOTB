using Capstone.Models;
using Capstone.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HazardController : ControllerBase
    {
        private readonly IHazardRepository _hazardRepository;

        public HazardController(IHazardRepository hazardRepository)
        {
            _hazardRepository = hazardRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_hazardRepository.GetAll());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_hazardRepository.GetHazardById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Hazard hazard)
        {
            if (id != hazard.Id)
            {
                return BadRequest();
            }
            _hazardRepository.UpdateHazard(hazard);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _hazardRepository.DeleteHazard(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(Hazard hazard)
        {
            try
            {
                _hazardRepository.AddHazard(hazard);
                return CreatedAtAction("Get", new { id = hazard.Id }, hazard);
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }
    }
}
