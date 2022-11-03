using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientHazardController : ControllerBase
    {
        private readonly IIngredientHazardRepository _ingredientHazardRepository;

        public IngredientHazardController(IIngredientHazardRepository ingredientHazardRepository)
        {
            _ingredientHazardRepository = ingredientHazardRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_ingredientHazardRepository.GetIngredientHazardById(id));
        }

        [HttpGet("ingredient/{id}")]
        public IActionResult GetByIngredientId(int id)
        {
            return Ok(_ingredientHazardRepository.GetHazardsByIngredientId(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, IngredientHazard ingredientHazard)
        {
            if (id != ingredientHazard.Id)
            {
                return BadRequest();
            }
            _ingredientHazardRepository.UpdateIngredientHazard(ingredientHazard);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _ingredientHazardRepository.DeleteIngredientHazard(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(IngredientHazard ingredientHazard)
        {
            try
            {
                _ingredientHazardRepository.AddIngredientHazard(ingredientHazard);
                return Ok(_ingredientHazardRepository.GetIngredientHazardById(ingredientHazard.Id));
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }
    }
}
