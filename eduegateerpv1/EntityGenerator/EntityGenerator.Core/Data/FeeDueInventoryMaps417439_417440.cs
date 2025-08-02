using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("FeeDueInventoryMaps417439_417440", Schema = "schools")]
    public partial class FeeDueInventoryMaps417439_417440
    {
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
    }
}
