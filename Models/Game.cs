using System;
using System.Collections.Generic;

namespace BoardGames.Models
{
    public partial class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NoOfPlayers { get; set; }
        public string Age { get; set; }
        public string AverageGameTime { get; set; }
        public int? Randomness { get; set; }
        public int? Interaction { get; set; }
        public int? Complexity { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? Added { get; set; }
        public DateTime? Edited { get; set; }

        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
