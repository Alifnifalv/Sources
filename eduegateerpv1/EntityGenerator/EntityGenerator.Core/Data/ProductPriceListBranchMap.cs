using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListBranchMaps", Schema = "catalog")]
    public partial class ProductPriceListBranchMap
    {
        [Key]
        public long ProductPriceListBranchMapIID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? BranchID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("ProductPriceListBranchMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListBranchMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
