using System.ComponentModel.DataAnnotations;

namespace JWTToken_API.Model
{
    public class TodoModel
    {
        [Key]       
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }       
    }
}
