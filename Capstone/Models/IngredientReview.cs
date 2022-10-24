using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class IngredientReview
    {
        public int Id { get; set; }
   
        public string Review { get; set; }
     
        public string Source { get; set; }
        public DateTime DateReviewed { get; set; }
        public int RateId { get; set; }
        public Rate Rate { get; set; }
        public UserProfile UserProfile { get; set; }    
        public int UserId { get; set; }

        public int IngredientId { get; set; }   
        public Ingredient Ingredient { get; set; }  
    }
}
