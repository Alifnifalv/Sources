using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WalletStatusMaster
    {
        public short StatusId { get; set; }
        public string Description { get; set; }
        public int LanguageID { get; set; }
    }
}
