using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawyerWebSiteMVC.Data;

namespace LawyerWebSiteMVC.Models
{
    public class ArticleDetailsViewModel
    {
        public Article Article { get; set; }
        public Article PreviousArticle { get; set; }
        public Article NextArticle { get; set; }
        public IEnumerable<Article> LatestArticles { get; set; }

    }
}