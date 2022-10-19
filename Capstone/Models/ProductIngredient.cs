namespace Capstone.Models
{
    public class ProductIngredient
    {
        public int Id { get; set; } 
        public int IngredientId { get; set; }   
        public int ProductId { get; set; }  
        public bool Active { get; set; }
        public string Use { get; set; } 
        public Ingredient Ingredient { get; set; }
        public Product Product { get; set; }
    }
}
