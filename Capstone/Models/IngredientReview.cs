using System;

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
        public UserProfile Profile { get; set; }    
        public int UserId { get; set; }
    }
}
