﻿using Capstone.Models;
using Capstone.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_productRepository.GetProductById(id));
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            _productRepository.AddProduct(product);
            return CreatedAtAction("Get", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            if( id != product.Id )
            {
                return BadRequest();
            }
            _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}