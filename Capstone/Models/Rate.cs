using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Rate
    {
        public int Id { get; set; }
        [Required]
        public string Rating { get; set; }  
    }
}
