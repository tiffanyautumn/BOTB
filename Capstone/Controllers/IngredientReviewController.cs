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
    public class IngredientReviewController : ControllerBase
    {
        private readonly IIngredientReviewRepository _ingredientReviewRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public IngredientReviewController(IIngredientReviewRepository ingredientReviewRepository, IUserProfileRepository userProfileRepository)
        {
            _ingredientReviewRepository = ingredientReviewRepository;
            _userProfileRepository = userProfileRepository;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_ingredientReviewRepository.GetIngredientReviewById(id));
        }

        [HttpPost("create")]
        public IActionResult Post(IngredientReview ingredientReview)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if(currentUserProfile.UserTypeId == 1 || currentUserProfile.UserTypeId == 2)
            {
                try
                {
                    ingredientReview.UserProfileId = currentUserProfile.Id;
                    ingredientReview.DateReviewed = DateTime.Now;
                    _ingredientReviewRepository.AddIngredientReview(ingredientReview);
                    return Ok(_ingredientReviewRepository.GetIngredientReviewById(ingredientReview.Id));
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, IngredientReview ingredientReview)
        {
            var currentUserProfile = GetCurrentUserProfile();
            ingredientReview.UserProfileId = currentUserProfile.Id;
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

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseId(firebaseUserId);
        }
    }
}
