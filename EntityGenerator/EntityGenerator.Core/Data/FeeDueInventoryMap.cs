using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeDueInventoryMaps", Schema = "schools")]
    [Index("FeeDueFeeTypeMapsID", Name = "IDX_FeeDueInventoryMaps_FeeDueFeeTypeMapsID_")]
    [Index("ProductCategoryMapID", Name = "IDX_FeeDueInventoryMaps_ProductCategoryMapID_StudentFeeDueID__FeeMasterID__TransactionHeadID__Creat")]
    public partial class FeeDueInventoryMap
    {
        [Key]
        public long FeeDueInventoryMapIID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? ProductCategoryMapID { get; set; }
        public int? FeeMasterID { get; set; }
        public long? TransactionHeadID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }

        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("FeeDueInventoryMaps")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("FeeDueInventoryMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("ProductCategoryMapID")]
        [InverseProperty("FeeDueInventoryMaps")]
        public virtual ProductCategoryMap ProductCategoryMap { get; set; }
        [ForeignKey("StudentFeeDueID")]
        [InverseProperty("FeeDueInventoryMaps")]
        public virtual StudentFeeDue StudentFeeDue { get; set; }
        [ForeignKey("TransactionHeadID")]
        [InverseProperty("FeeDueInventoryMaps")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
