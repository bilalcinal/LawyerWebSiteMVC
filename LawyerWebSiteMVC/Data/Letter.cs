using LawyerWebSiteMVC.Core;

namespace LawyerWebSiteMVC.Data;
    public class Letter : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PhoneNumber { get; set; }
    }