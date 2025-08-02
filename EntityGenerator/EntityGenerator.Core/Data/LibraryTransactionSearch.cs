using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LibraryTransactionSearch
    {
        public long LibraryTransactionIID { get; set; }
        public long? MINIID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReturnDueDate { get; set; }
        public byte? LibraryTransactionTypeID { get; set; }
        [StringLength(100)]
        public string Call_No { get; set; }
        [StringLength(100)]
        public string Acc_No { get; set; }
        public byte? CurrentStatusID { get; set; }
        public int IsStudent { get; set; }
        [StringLength(555)]
        public string Stud_Emp_Name { get; set; }
        public long? Stud_Emp_ID { get; set; }
        [StringLength(50)]
        public string Dep_Class { get; set; }
        [StringLength(50)]
        public string Desig_Section { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(50)]
        public string CurrentStatus { get; set; }
        [StringLength(50)]
        public string Notes { get; set; }
        [StringLength(100)]
        public string Book { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
