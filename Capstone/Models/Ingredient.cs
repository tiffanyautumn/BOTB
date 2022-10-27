using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
        public string ImageUrl { get; set; }
        public IngredientReview IngredientReview { get; set; }  
        public List<Use> Uses { get; set; }
        public List<IngredientHazard> Hazards { get; set; }   

    }
}
