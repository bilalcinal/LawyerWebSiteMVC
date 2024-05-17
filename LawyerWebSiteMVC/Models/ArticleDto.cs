using LawyerWebSiteMVC.Data;

namespace LawyerWebSiteMVC.Models;
public class ArticleDto
{
    public Article Article { get; set; }
    public List<ArticlePhoto> ArticlePhotos { get; set; }
}
