using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class HomePageLink
    {
        public byte HomePageLinkID { get; set; }
        public string HomePageLinkUrl { get; set; }
        public bool Active { get; set; }
        public string Lang { get; set; }
    }
}
