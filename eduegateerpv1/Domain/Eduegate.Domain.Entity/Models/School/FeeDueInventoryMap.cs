using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("FeeDueInventoryMaps", Schema = "schools")]
    public partial class FeeDueInventoryMap
    {
        [Key]
        public long FeeDueInventoryMapIID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? ProductCategoryMapID { get; set; }

        public int? FeeMasterID { get; set; }

        public long? TransactionHeadID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public virtual ProductCategoryMap ProductCategoryMap { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        //public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }

        //public virtual FeeMaster FeeMaster { get; set; }

        //public virtual StudentFeeDue StudentFeeDue { get; set; }
    }
}