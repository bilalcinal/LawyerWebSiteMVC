using System.ComponentModel.DataAnnotations;
using LawyerWebSiteMVC.Core;

namespace LawyerWebSiteMVC.Data
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Status { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
