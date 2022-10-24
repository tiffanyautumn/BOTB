using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }    
        public string Brand { get; set; }
        [Required]
        public int TypeId { get; set; }
        public Type Type { get; set; }  
        public string ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }  
        public List<ProductIngredient> ProductIngredients { get; set; }
    }
}
