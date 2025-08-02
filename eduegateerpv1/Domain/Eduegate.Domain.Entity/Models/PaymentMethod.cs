using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentMethods", Schema = "mutual")]
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            this.EntityTypePaymentMethodMaps = new List<EntityTypePaymentMethodMap>();
            this.PaymentMethodSiteMaps = new List<PaymentMethodSiteMap>();
            this.SKUPaymentMethodExceptionMaps = new List<SKUPaymentMethodExceptionMaps>();
            this.PaymentGroups = new List<PaymentGroup>();
        }

        [Key]
        public short PaymentMethodID { get; set; }
        [Column("PaymentMethod")]
        [StringLength(50)]
        public string PaymentMethod1 { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public bool? IsVirtual { get; set; }
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        public virtual ICollection<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; } 
        public virtual ICollection<SKUPaymentMethodExceptionMaps> SKUPaymentMethodExceptionMaps { get; set; }
        public virtual ICollection<PaymentGroup> PaymentGroups { get; set; }
    }
}
