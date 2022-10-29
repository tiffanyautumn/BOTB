namespace Capstone.Models
{
    public class UserProduct
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }  
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
