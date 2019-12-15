using System;
using System.Collections.Generic;

namespace BoardGames.Models
{
    public partial class Category
    {
        public Category()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Added { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
