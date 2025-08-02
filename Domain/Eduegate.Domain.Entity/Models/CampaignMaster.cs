using System;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CampaignMaster
    {
        [Key]
        public byte CampaignID { get; set; }
        public string Campaign { get; set; }
        public Nullable<byte> VoucherAmount { get; set; }
        public Nullable<byte> VoucherValidity { get; set; }
        public Nullable<System.DateTime> VoucherValidFrom { get; set; }
        public Nullable<System.DateTime> VoucherValidTill { get; set; }
    }
}
