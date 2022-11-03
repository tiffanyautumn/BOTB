using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class IngredientReview
    {
        public int Id { get; set; }
   
        public string Review { get; set; }
     
        public DateTime DateReviewed { get; set; }
        public int RateId { get; set; }
        public Rate Rate { get; set; }
        public UserProfile UserProfile { get; set; }    
        public int UserProfileId { get; set; }

        public int IngredientId { get; set; }   
        public Ingredient Ingredient { get; set; }  
        public List<Source> Sources { get; set; }

    }
}
