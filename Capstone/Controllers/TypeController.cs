
using Capstone.Models;
using Capstone.Repositories;
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

    }
}
