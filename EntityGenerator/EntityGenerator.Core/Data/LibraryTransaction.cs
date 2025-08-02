using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryTransactions", Schema = "schools")]
    [Index("LibraryBookMapID", Name = "IDX_LibraryTransactions_LibraryBookMapID_")]
    [Index("LibraryTransactionTypeID", "SchoolID", Name = "IDX_LibraryTransactions_LibraryTransactionTypeID__SchoolID_LibraryBookMapID")]
    [Index("SchoolID", Name = "IDX_LibraryTransactions_SchoolID_")]
    public partial class LibraryTransaction
    {
        [Key]
        public long LibraryTransactionIID { get; set; }
        public byte? LibraryTransactionTypeID { get; set; }
        [StringLength(50)]
        public string Notes { get; set; }
        public long? BookID { get; set; }
        public long? StudentID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReturnDueDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool IsReturned { get; set; }
        public byte? BookCondionID { get; set; }
        public bool? IsCollected { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string Acc_No { get; set; }
        [StringLength(100)]
        public string Call_No { get; set; }
        public int? LibraryBookMapID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("LibraryTransactions")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("BookID")]
        [InverseProperty("LibraryTransactions")]
        public virtual LibraryBook Book { get; set; }
        [ForeignKey("BookCondionID")]
        [InverseProperty("LibraryTransactions")]
        public virtual LibraryBookCondition BookCondion { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("LibraryTransactions")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LibraryBookMapID")]
        [InverseProperty("LibraryTransactions")]
        public virtual LibraryBookMap LibraryBookMap { get; set; }
        [ForeignKey("LibraryTransactionTypeID")]
        [InverseProperty("LibraryTransactions")]
        public virtual LibraryTransactionType LibraryTransactionType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("LibraryTransactions")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("LibraryTransactions")]
        public virtual Student Student { get; set; }
    }
}
