using System.ComponentModel.DataAnnotations;

namespace EvolentHealthContactManagement.Models
{
    public class ContactDTO
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Email address is not proper")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number cannot be alpha numberic")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Status { get; set; }
    }
}