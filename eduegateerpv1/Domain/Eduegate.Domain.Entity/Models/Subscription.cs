using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Subscriptions", Schema = "cms")]
    public partial class Subscription
    {
        [Key]
        public long SubscriptionIID { get; set; }

        [StringLength(200)]
        public string SubscriptionEmail { get; set; }

        public byte? StatusID { get; set; }

        [StringLength(200)]
        public string VarificationCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }
    }
}