using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerSupplierMaps", Schema = "mutual")]
    public partial class CustomerSupplierMap
    {
        [Key]
        public long CustomerSupplierMapIID { get; set; }
        public long? CustomerID { get; set; }
        public long? SupplierID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("CustomerSupplierMaps")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("CustomerSupplierMaps")]
        public virtual Supplier Supplier { get; set; }
    }
}
