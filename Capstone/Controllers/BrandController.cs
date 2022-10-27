using Capstone.Models;
using Capstone.Repositories;
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
    }
}
