using System.ComponentModel.DataAnnotations;

namespace JWTToken_API.Model
{
    public class Login
    {
        [Required(ErrorMessage ="Email address is required.")]
        [EmailAddress(ErrorMessage ="Please enter a valid emailid.")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
