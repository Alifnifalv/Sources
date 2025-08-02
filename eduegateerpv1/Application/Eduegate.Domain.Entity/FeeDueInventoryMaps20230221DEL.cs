namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeDueInventoryMaps20230221DEL")]
    public partial class FeeDueInventoryMaps20230221DEL
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

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }
    }
}
