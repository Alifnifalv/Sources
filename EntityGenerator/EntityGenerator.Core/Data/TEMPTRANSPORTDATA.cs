using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TEMPTRANSPORTDATA")]
    public partial class TEMPTRANSPORTDATA
    {
        public long COLLID { get; set; }
        public bool COLLCCollectionStatus { get; set; }
        public bool? COLLCCancelStatus { get; set; }
        public long CANID { get; set; }
        [StringLength(50)]
        public string CANCLINVNO { get; set; }
        public bool CANCCollectionStatus { get; set; }
        public bool? CANCancelStatus { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        public long? studentID { get; set; }
        public long? RowNum { get; set; }
    }
}
