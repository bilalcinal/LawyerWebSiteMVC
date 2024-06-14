using System.Collections.Generic;
using LawyerWebSiteMVC.Data;

namespace LawyerWebSiteMVC.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Article> LatestArticles { get; set; }
    }
}
