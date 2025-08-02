using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Due_Not_Collecetd_Accounts_Term1_20221221
    {
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public long? StudentId { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodFrom { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fee_Due { get; set; }
        public int ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public int IsTC { get; set; }
        public bool? IsActive { get; set; }
    }
}
