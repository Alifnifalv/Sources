using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LibraryTransactions_14092022
    {
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
    }
}
