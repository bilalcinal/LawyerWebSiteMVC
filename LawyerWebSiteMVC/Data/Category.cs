using LawyerWebSiteMVC.Core;
namespace LawyerWebSiteMVC.Data;
public class Category: BaseEntity
{
    public string CategoryName { get; set; }
    public virtual ICollection<Article> Articles { get; set; }

}
