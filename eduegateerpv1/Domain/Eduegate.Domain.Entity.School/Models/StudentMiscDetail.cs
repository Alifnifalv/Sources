namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("StudentMiscDetails", Schema = "schools")]
    public partial class StudentMiscDetail
    {
        [Key]
        public long StudentMiscDetailsIID { get; set; }

        public long? StudentID { get; set; }

        [StringLength(50)]
        public string BankAccountNumber { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string IFCCode { get; set; }

        [StringLength(50)]
        public string NationalID { get; set; }

        [StringLength(50)]
        public string LocalIdentificationNumber { get; set; }

        [StringLength(2000)]
        public string PreviousSchoolDetails { get; set; }

        [StringLength(2000)]
        public string Notes { get; set; }

        [StringLength(2000)]
        public string CurrentAddress { get; set; }

        public bool? IsGuardianIsCurrent { get; set; }

        [StringLength(2000)]
        public string PermanentAddress { get; set; }

        public bool? IsPermanentIsCurrent { get; set; }

        public long? StaffID { get; set; }

        public byte? GuardianTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Student Student { get; set; }
    }
}
