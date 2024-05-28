using LawyerWebSiteMVC.Data;

namespace LawyerWebSiteMVC.Models
{
    public class ArticleViewModel
    {
        public List<Article> Articles { get; set; }
        public List<Category> Categories { get; set; }
    }
}
