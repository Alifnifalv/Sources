using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("LibraryBooksExcel")]
    public partial class LibraryBooksExcel
    {
        public double? LibraryBookIID { get; set; }
        [StringLength(255)]
        public string BookTitle { get; set; }
        [StringLength(255)]
        public string BookNumber { get; set; }
        [StringLength(255)]
        public string ISBNNumber { get; set; }
        [StringLength(255)]
        public string Publisher { get; set; }
        [StringLength(255)]
        public string Author { get; set; }
        [StringLength(255)]
        public string Subject { get; set; }
        [StringLength(255)]
        public string RackNumber { get; set; }
        public double? Quatity { get; set; }
        public double? BookPrice { get; set; }
        [StringLength(255)]
        public string PostPrice { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string BookPhoto { get; set; }
        [StringLength(255)]
        public string BookConditionID { get; set; }
        public double? LibraryBookTypeID { get; set; }
        public double? BookStatusID { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        [StringLength(255)]
        public string CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(255)]
        public string TimeStamps { get; set; }
        public double? SchoolID { get; set; }
        public double? AcademicYearID { get; set; }
        [StringLength(255)]
        public string BookCode { get; set; }
        [StringLength(255)]
        public string Series { get; set; }
        [StringLength(255)]
        public string Pages { get; set; }
        [StringLength(255)]
        public string Acc_No { get; set; }
        [StringLength(255)]
        public string Year { get; set; }
        [StringLength(255)]
        public string Edition { get; set; }
        [StringLength(255)]
        public string Call_No { get; set; }
        [StringLength(255)]
        public string Bill_No { get; set; }
        public double? BookCategoryCodeID { get; set; }
        [StringLength(255)]
        public string BookCodeSequenceNo { get; set; }
        public double? IsActive { get; set; }
    }
}
