using Capstone.Models;
using Capstone.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientReviewController : ControllerBase
    {
        private readonly IIngredientReviewRepository _ingredientReviewRepository;

        public IngredientReviewController(IIngredientReviewRepository ingredientReviewRepository)
        {
            _ingredientReviewRepository = ingredientReviewRepository;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_ingredientReviewRepository.GetIngredientReviewById(id));
        }

        [HttpPost("create")]
        public IActionResult Post(IngredientReview ingredientReview)
        {
            try
            {
                ingredientReview.DateReviewed = DateTime.Now;
                _ingredientReviewRepository.AddIngredientReview(ingredientReview);
                return Ok(_ingredientReviewRepository.GetIngredientReviewById(ingredientReview.Id));
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, IngredientReview ingredientReview)
        {
            if (id != ingredientReview.Id)
            {
                return BadRequest();
            }
            ingredientReview.DateReviewed = DateTime.Now;
            _ingredientReviewRepository.UpdateIngredientReview(ingredientReview);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _ingredientReviewRepository.DeleteIngredientReview(id);
            return NoContent();
        }
    }
}
