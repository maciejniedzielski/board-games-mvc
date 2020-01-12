using System.ComponentModel.DataAnnotations;

namespace BoardGames.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}