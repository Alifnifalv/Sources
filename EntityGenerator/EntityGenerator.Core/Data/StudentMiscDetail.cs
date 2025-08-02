using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("StaffID")]
        [InverseProperty("StudentMiscDetails")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentMiscDetails")]
        public virtual Student Student { get; set; }
    }
}
