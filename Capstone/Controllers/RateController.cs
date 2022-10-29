using Capstone.Models;
using Capstone.Repositories;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateRepository _rateRepository;

        public RateController(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_rateRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_rateRepository.GetRateById(id));
        }

        [HttpPost]
        public IActionResult Post(Rate rate)
        {
            try
            {
                _rateRepository.AddRate(rate);
                return CreatedAtAction("Get", new { id = rate.Id }, rate);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Rate rate)
        {
            if (id != rate.Id)
            {
                return BadRequest();
            }
            _rateRepository.UpdateRate(rate);
            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _rateRepository.DeleteRate(id);
            return NoContent();
        }
    }
}
