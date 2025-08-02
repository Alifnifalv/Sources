using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class HomePageLink
    {
        [Key]
        public byte HomePageLinkID { get; set; }
        public string HomePageLinkUrl { get; set; }
        public bool Active { get; set; }
        public string Lang { get; set; }
    }
}
