namespace Capstone.Models
{
    public class ProductReview
    {
        public int Id { get; set; } 
        public int UserProfileId { get; set; }  
        public UserProfile UserProfile { get; set; }    
        public int ProductId { get; set; }
        public string Comment { get; set; } 
        public int AffordabilityRate { get; set; }  
        public int EfficacyRate { get; set; }
        public int OverallRate { get; set; }    
    }
}
