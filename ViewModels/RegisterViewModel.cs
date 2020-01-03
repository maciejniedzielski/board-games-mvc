using System.ComponentModel.DataAnnotations;

namespace BoardGames.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Given passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}