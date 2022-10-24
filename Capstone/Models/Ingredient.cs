using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Function { get; set; }    
        public string SafetyInfo { get; set; }  
        public IngredientReview IngredientReview { get; set; }  

    }
}
