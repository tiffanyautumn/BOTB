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
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_brandRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_brandRepository.GetBrandById(id));
        }

        [HttpPost]
        public IActionResult Post(Brand brand)
        {
            try
            {
                _brandRepository.AddBrand(brand);
                return CreatedAtAction("Get", new { id = brand.Id }, brand);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }
            _brandRepository.UpdateBrand(brand);
            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _brandRepository.DeleteBrand(id);
            return NoContent();
        }
    }
}
