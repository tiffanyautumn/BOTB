using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class ProductIngredient
    {
        public int Id { get; set; }
        [Required]
        public int IngredientId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public bool Active { get; set; }

        public string Use { get; set; } 
        public Ingredient Ingredient { get; set; }
        public Product Product { get; set; }
    }
}
