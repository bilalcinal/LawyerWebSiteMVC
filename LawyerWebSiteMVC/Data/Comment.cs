using LawyerWebSiteMVC.Core;

namespace LawyerWebSiteMVC.Data;
public class Comment : BaseEntity
{
        public string Name { get; set;}
        public string Email { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
}
