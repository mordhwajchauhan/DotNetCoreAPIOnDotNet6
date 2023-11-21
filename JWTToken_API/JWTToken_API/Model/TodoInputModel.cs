using System.ComponentModel.DataAnnotations;

namespace JWTToken_API.Model
{
    public class TodoInputModel
    {        
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }       
    }
}
