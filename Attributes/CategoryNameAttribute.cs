using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoardGames.Models;

namespace BoardGames.Validators
{
    public class UniqueGameNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var dbContext = (AppDbContext)validationContext
                .GetService(typeof(AppDbContext));

            var name = (string)value;
            var foundGameName = dbContext.Game.FirstOrDefault(g => g.Name == name).Name;
            
            return String.IsNullOrEmpty(foundGameName) ? ValidationResult.Success : new ValidationResult("Game with given name already exists");
        }    
    }
}
