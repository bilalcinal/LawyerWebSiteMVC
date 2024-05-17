using System.ComponentModel.DataAnnotations;

namespace LawyerWebSiteMVC.Core;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }  = DateTime.Now;
    public bool IsDeleted { get; set; }
}
