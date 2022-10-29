using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ProductController(IProductRepository productRepository, IUserProfileRepository userProfileRepository)
        {
            _productRepository = productRepository;
            _userProfileRepository = userProfileRepository;
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
            try
            {
                _productRepository.AddProduct(product);
                return CreatedAtAction("Get", new { id = product.Id }, product);
            }
            catch(Exception)
            {
                return BadRequest();
            }
           
           
        }

        [HttpGet("search")]
        public IActionResult Search(string q)
        {
            return Ok(_productRepository.Search(q));
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

        [HttpDelete("delete/userProduct/{id}")]
        public IActionResult DeleteUserProduct(int id)
        {
            _productRepository.DeleteUserProduct(id);
            return NoContent();
        }

       
        [HttpPost("create/userProduct")]
        public IActionResult PostUserProduct(Product product)
        {
            var currentUserProfile = GetCurrentUserProfile();
            try
            {
                _productRepository.AddUserProduct(product, currentUserProfile.Id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [HttpGet("userProduct/{id}")]
        public IActionResult GetUserProductById(int id)
        {
            return Ok(_productRepository.GetUserProductById(id));
        }

        [HttpGet("getUserProducts")]
        public IActionResult GetUserProductsByUserId()
        {
            var currentUserProfile = GetCurrentUserProfile();
            return Ok(_productRepository.GetUserProductsByUserId(currentUserProfile.Id));
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseId(firebaseUserId);
        }
    }
}
