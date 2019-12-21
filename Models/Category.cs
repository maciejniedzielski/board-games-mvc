using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGames.Models
{
    public partial class Category
    {
        public Category()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime? Added { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
