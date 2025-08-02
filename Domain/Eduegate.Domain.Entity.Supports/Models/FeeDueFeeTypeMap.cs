using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("FeeDueFeeTypeMaps", Schema = "schools")]
    public partial class FeeDueFeeTypeMap
    {
        public FeeDueFeeTypeMap()
        {

        }

        [Key]
        public long FeeDueFeeTypeMapsIID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? ClassFeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public decimal? Amount { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public bool Status { get; set; }

        public int? FeeMasterID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public long? FineMasterStudentMapID { get; set; }

        public int? FineMasterID { get; set; }

        public decimal? CollectedAmount { get; set; }

        public long? FeeStructureFeeMapID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual StudentFeeDue StudentFeeDue { get; set; }
    }
}
