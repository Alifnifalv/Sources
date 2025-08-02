using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_FEE_OUTSTANDING
    {
        public long StudentFeeDueIID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public bool CollectionStatus { get; set; }
        public bool? IsMandatoryToPay { get; set; }
        public long? StudentID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fee_Due { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal Fee_Col { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal Fee_Crn { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal Fee_Stl { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Bal { get; set; }
    }
}
