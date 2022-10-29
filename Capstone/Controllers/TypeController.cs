
using Capstone.Models;
using Capstone.Repositories;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Type = Capstone.Models.Type;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository _typeRepository;

        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_typeRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_typeRepository.GetTypeById(id));
        }

        [HttpPost]
        public IActionResult Post(Type type)
        {
            try
            {
                _typeRepository.AddType(type);
                return CreatedAtAction("Get", new { id = type.Id }, type);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Type type)
        {
            if (id != type.Id)
            {
                return BadRequest();
            }
            _typeRepository.UpdateType(type);
            return NoContent();
        }
        

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _typeRepository.DeleteType(id);
            return NoContent();
        }

    }
}
