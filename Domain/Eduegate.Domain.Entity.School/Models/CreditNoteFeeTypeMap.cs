namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("CreditNoteFeeTypeMaps", Schema = "schools")]
    public partial class CreditNoteFeeTypeMap
    {
        [Key]
        public long CreditNoteFeeTypeMapIID { get; set; }

        public long? SchoolCreditNoteID { get; set; }

        public decimal? Amount { get; set; }

        public int? FeeMasterID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? PeriodID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual Schools School { get; set; }

        public virtual SchoolCreditNote SchoolCreditNote { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public int? Year { get; set; }

        public int? MonthID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }

        public long? FeeDueMonthlySplitID { get; set; }

        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }

        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
    }
}
