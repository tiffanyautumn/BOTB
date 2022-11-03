using Capstone.Models;
using Capstone.Repositories;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ProductReviewController(IProductReviewRepository productReviewRepository, IUserProfileRepository userProfileRepository)
        {
            _productReviewRepository = productReviewRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_productReviewRepository.GetProductReviewById(id));
        }

        [HttpGet("product/{id}")]
        public IActionResult GetAllByProductId(int id)
        {
            return Ok(_productReviewRepository.GetProductReviewsByProductId(id));
        }

        [HttpPost]
        public IActionResult Post(ProductReview productReview)
        {
            var currentUserProfile = GetCurrentUserProfile();
            productReview.UserProfileId = currentUserProfile.Id;
            try
            {
                _productReviewRepository.AddProductReview(productReview);
                return CreatedAtAction("GetById", new { id = productReview.Id }, productReview);
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            ProductReview productReview = _productReviewRepository.GetProductReviewById(id);
            var currentUserProfile = GetCurrentUserProfile();
            if(currentUserProfile.UserTypeId == 1 || productReview.UserProfileId == currentUserProfile.Id)
            {
                _productReviewRepository.DeleteProductReview(id);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
            
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseId(firebaseUserId);
        }

    }
}
