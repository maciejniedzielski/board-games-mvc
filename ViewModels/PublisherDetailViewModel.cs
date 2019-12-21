using System.Collections.Generic;
using BoardGames.Models;

namespace BoardGames.ViewModels
{
    public class PublisherDetailViewModel
    {
        public Publisher Publisher { get; set; }
        public GamesTableViewModel GamesTableViewModel { get; set; }
        
        public PublisherDetailViewModel(Publisher publisher)
        {
            Publisher = publisher;
        }
    }
}