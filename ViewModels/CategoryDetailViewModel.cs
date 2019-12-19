using System.Collections.Generic;
using BoardGames.Models;

namespace BoardGames.ViewModels
{
    public class CategoryDetailViewModel
    {
        public Category Category { get; set; }
        public GamesTableViewModel GamesTableViewModel { get; set; }
        
        public CategoryDetailViewModel(Category category)
        {
            Category = category;
        }
    }
}