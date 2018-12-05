using System.ComponentModel.DataAnnotations;

namespace Dialogue.Models
{
    public class UserLoginViewModel
    {
        [Display(Name = "Username")]
        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string UserName {get; set;}
        
        [Required]
        [DataType(DataType.Password)]
        public string PasswordString {get; set;}
    }
    
}