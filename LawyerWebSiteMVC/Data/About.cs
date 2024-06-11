using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawyerWebSiteMVC.Core;

namespace LawyerWebSiteMVC.Data
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

    }
}