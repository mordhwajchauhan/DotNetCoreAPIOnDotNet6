using JWTToken_API.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JWTToken_API.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserModel
    {       
        
        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid emailid.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
