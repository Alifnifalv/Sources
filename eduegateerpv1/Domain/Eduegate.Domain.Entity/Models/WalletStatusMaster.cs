using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WalletStatusMaster
    {
        [Key]
        public short StatusId { get; set; }
        public string Description { get; set; }
        public int LanguageID { get; set; }
    }
}
