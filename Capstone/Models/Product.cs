using System.Collections.Generic;

namespace Capstone.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Brand { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }  
        public string ImageUrl { get; set; }    
        public decimal Price { get; set; }  
        public List<ProductIngredient> ProductIngredients { get; set; }
    }
}
