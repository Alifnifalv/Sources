using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolCreditNote", Schema = "schools")]
    [Index("CreditNoteDate", Name = "IDX_SchoolCreditNote_CreditNoteDate")]
    [Index("CreditNoteDate", Name = "IDX_SchoolCreditNote_CreditNoteDate_StudentID__CreditNoteNumber__IsCancelled")]
    [Index("StudentID", "CreditNoteDate", Name = "IDX_SchoolCreditNote_StudentIDCreditNoteDate_CreditNoteNumber__IsCancelled")]
    [Index("StudentID", Name = "IDX_SchoolCreditNote_StudentID_")]
    [Index("StudentID", Name = "IDX_SchoolCreditNote_StudentID_ClassID__UpdatedBy__CreatedBy")]
    public partial class SchoolCreditNote
    {
        public SchoolCreditNote()
        {
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
        }

        [Key]
        public long SchoolCreditNoteIID { get; set; }
        public int? ClassID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreditNoteDate { get; set; }
        public bool Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Amount { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? SectionID { get; set; }
        public bool? IsDebitNote { get; set; }
        [StringLength(100)]
        public string CreditNoteNumber { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SchoolCreditNotes")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("SchoolCreditNotes")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("SchoolCreditNotes")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SchoolCreditNotes")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("SchoolCreditNotes")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("SchoolCreditNotes")]
        public virtual Student Student { get; set; }
        [InverseProperty("SchoolCreditNote")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
    }
}
