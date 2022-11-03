using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
