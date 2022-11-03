using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseController : ControllerBase
    {
        private readonly IUseRepository _useRepository;

        public UseController(IUseRepository useRepository)
        {
            _useRepository = useRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_useRepository.GetUseById(id));
        }

        [HttpGet("getbyingredient/{IngredientId}")]
        public IActionResult GetByIngredientId(int IngredientId)
        {
            return Ok(_useRepository.GetAllIngredientUses(IngredientId));
        }
        [HttpPost]
        public IActionResult Post(Use use)
        {
            try
            {
                _useRepository.AddUse(use);
                return Ok(_useRepository.GetUseById(use.Id));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost("productIngredient")]
        public IActionResult PIPost(ProductIngredientUse use)
        {
            try
            {
                _useRepository.AddProductIngredientUse(use);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _useRepository.DeleteUse(id);
            return NoContent();
        }
    }
}
