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
    public class SourceController : ControllerBase
    {
        private readonly ISourceRepository _sourceRepository;

        public SourceController(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_sourceRepository.GetAll());
        }

        [HttpGet("review/{id}")]
        public IActionResult GetSourcesByReviewId(int id)
        {
            return Ok(_sourceRepository.GetAllByReviewId(id));
        }

        [HttpPost]
        public IActionResult Post(Source source)
        {
            try
            {
                _sourceRepository.AddSource(source);
                return CreatedAtAction("Get", new { id = source.Id }, source);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Source source)
        {
            if (id != source.Id)
            {
                return BadRequest();
            }
            _sourceRepository.UpdateSource(source);
            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _sourceRepository.DeleteSource(id);
            return NoContent();
        }
    }
}
