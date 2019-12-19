using System.Collections.Generic;
using BoardGames.Models;

namespace BoardGames.ViewModels
{
    public class GamesTableViewModel
    {
        public List<Game> Games { get; set; }
        public string Caption { get; set; }
        public bool Editable { get; set; }
        
        public GamesTableViewModel(List<Game> games, string caption, bool editable)
        {
            Games = games;
            Caption = caption;
            Editable = editable;
        }
    }
}