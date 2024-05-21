using LawyerWebSiteMVC.Core;
namespace LawyerWebSiteMVC.Data
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public string link { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ArticlePhoto> ArticlePhotos { get; set; }
    }
}
