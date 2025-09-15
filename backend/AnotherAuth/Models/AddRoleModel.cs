using System.ComponentModel.DataAnnotations;

namespace AnotherAuth.Models
{
    public class AddRoleModel
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string role { get; set; }
    }
}
