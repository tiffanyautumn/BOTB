using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductIngredientController : ControllerBase
    {
        private readonly IProductIngredientRepository _productIngredientRepository;
        public ProductIngredientController(IProductIngredientRepository productIngredientRepository)
        {
            _productIngredientRepository = productIngredientRepository;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_productIngredientRepository.GetProductIngredientById(id));
        }

        [HttpGet("product/{id}")]
        public IActionResult GetByProductId(int id)
        {
            return Ok(_productIngredientRepository.GetProductIngredientsByProductId(id));
        }
        [HttpPost]
        public IActionResult Post(ProductIngredient productIngredient)
        {
            try
            {
                _productIngredientRepository.AddProductIngredient(productIngredient);
                return Ok(_productIngredientRepository.GetProductIngredientById(productIngredient.Id));
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProductIngredient productIngredient)
        {
            if (id != productIngredient.Id)
            {
                return BadRequest();
            }
            _productIngredientRepository.UpdateProductIngredient(productIngredient);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productIngredientRepository.DeleteProductIngredient(id);
            return NoContent();
        }
    }
}
