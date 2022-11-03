using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
