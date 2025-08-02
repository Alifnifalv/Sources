using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherMasterDetail
    {
        [Key]
        public long VoucherMasterDetailsID { get; set; }
        public long RefVoucherID { get; set; }
        public long RefUserID { get; set; }
        public string Remarks { get; set; }
        public virtual UserMaster UserMaster { get; set; }
        public virtual VoucherMaster VoucherMaster { get; set; }
    }
}
