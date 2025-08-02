using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerMasterPosition
    {
        [Key]
        public int RefBannerID { get; set; }
        public byte Position { get; set; }
    }
}
