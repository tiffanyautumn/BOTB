namespace Capstone.Models
{
    public class IngredientHazard
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int HazardId { get; set; }
        public Hazard Hazard { get; set; }
        public string Case { get; set; }    
    }
}
