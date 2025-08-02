namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.LibraryTransactions")]
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

        public DateTime? TransactionDate { get; set; }

        public DateTime? ReturnDueDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
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

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual LibraryBookCondition LibraryBookCondition { get; set; }

        public virtual LibraryBookMap LibraryBookMap { get; set; }

        public virtual LibraryBook LibraryBook { get; set; }

        public virtual LibraryTransactionType LibraryTransactionType { get; set; }

        public virtual School School { get; set; }

        public virtual Student Student { get; set; }
    }
}
