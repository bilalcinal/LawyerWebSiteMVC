using LawyerWebSiteMVC.Core;

namespace LawyerWebSiteMVC.Data;
public class ArticlePhoto : BaseEntity
{
    public byte[] Image { get; set; }
    public int ArticleId { get; set; }
}
