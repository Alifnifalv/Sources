using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Schools
{
    [Table("FineMasters", Schema = "schools")]
    public partial class FineMaster
    {
        public FineMaster()
        {
        }

        [Key]
        public int FineMasterID { get; set; }

        [Required]
        [StringLength(20)]
        public string FineCode { get; set; }

        [Required]
        [StringLength(100)]
        public string FineName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public short? FeeFineTypeID { get; set; }

        public long? LedgerAccountID { get; set; }

        public decimal? Amount { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        public virtual Account LedgerAccount { get; set; }

        //public virtual School School { get; set; }
    }
}