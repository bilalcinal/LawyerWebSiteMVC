using LawyerWebSiteMVC.Core;

namespace LawyerWebSiteMVC.Data;
public class Slider : BaseEntity
{
    public byte[] Image { get; set; }
    public string Content { get; set; }
    public int MyProperty { get; set; }
}
