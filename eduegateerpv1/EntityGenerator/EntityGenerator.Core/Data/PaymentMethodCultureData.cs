using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentMethodCultureDatas", Schema = "mutual")]
    public partial class PaymentMethodCultureData
    {
        [Key]
        public short PaymentMethodID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string PaymentMethod { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("PaymentMethodCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("PaymentMethodID")]
        [InverseProperty("PaymentMethodCultureDatas")]
        public virtual PaymentMethod PaymentMethodNavigation { get; set; }
    }
}
