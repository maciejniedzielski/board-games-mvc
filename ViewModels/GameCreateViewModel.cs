using System;
using System.ComponentModel.DataAnnotations;
using BoardGames.Validators;

namespace BoardGames.Models
{
    public partial class GameCreateViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [UniqueGameName]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Number of players is required")]
        public string NoOfPlayers { get; set; }
        
        [Required(ErrorMessage = "Age is required")]
        public string Age { get; set; }
        
        [Required(ErrorMessage = "Average game time is required")]
        public string AverageGameTime { get; set; }
        
        [Range(1, 6, ErrorMessage = "Randomness must be number between 1-6")]
        [Display(Name = "Randomness")]
        public int? Randomness { get; set; }
        
        [Range(1, 6, ErrorMessage = "Interaction must be number between 1-6")]
        [Display(Name = "Interaction")]
        public int? Interaction { get; set; }
        
        [Range(1, 6, ErrorMessage = "Complexity must be number between 1-6")]
        [Display(Name = "Complexity")]
        public int? Complexity { get; set; }
        
        public int? CategoryId { get; set; }
        
        public int? PublisherId { get; set; }
        public DateTime? Added { get; set; }
        public DateTime? Edited { get; set; }

        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}