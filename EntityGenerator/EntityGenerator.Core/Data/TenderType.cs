using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TenderTypes", Schema = "account")]
    public partial class TenderType
    {
        public TenderType()
        {
            PaymentModes = new HashSet<PaymentMode>();
        }

        [Key]
        public int TenderTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TenderTypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("TenderType")]
        public virtual ICollection<PaymentMode> PaymentModes { get; set; }
    }
}
