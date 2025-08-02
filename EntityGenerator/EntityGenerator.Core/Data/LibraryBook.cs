using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBooks", Schema = "schools")]
    [Index("SchoolID", Name = "IDX_LibraryBooks_SchoolID_")]
    [Index("SchoolID", Name = "IDX_LibraryBooks_SchoolID_BookTitle")]
    public partial class LibraryBook
    {
        public LibraryBook()
        {
            LibraryBookCategoryMaps = new HashSet<LibraryBookCategoryMap>();
            LibraryBookMaps = new HashSet<LibraryBookMap>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
        }

        [Key]
        public long LibraryBookIID { get; set; }
        [StringLength(100)]
        public string BookTitle { get; set; }
        [StringLength(50)]
        public string BookNumber { get; set; }
        public string ISBNNumber { get; set; }
        public string Publisher { get; set; }
        [StringLength(100)]
        public string Author { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        [StringLength(20)]
        public string RackNumber { get; set; }
        public int? Quatity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BookPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PostPrice { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(200)]
        public string BookPhoto { get; set; }
        public byte? BookConditionID { get; set; }
        public byte? LibraryBookTypeID { get; set; }
        public byte? BookStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? BookCategoryCodeID { get; set; }
        [StringLength(100)]
        public string BookCode { get; set; }
        [StringLength(100)]
        public string Series { get; set; }
        [StringLength(100)]
        public string Pages { get; set; }
        [StringLength(100)]
        public string Acc_No { get; set; }
        [StringLength(100)]
        public string Year { get; set; }
        [StringLength(100)]
        public string Edition { get; set; }
        [StringLength(100)]
        public string Call_No { get; set; }
        [StringLength(100)]
        public string Bill_No { get; set; }
        public int? BookCodeSequenceNo { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(200)]
        public string PlaceOfPublication { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("LibraryBooks")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("BookCategoryCodeID")]
        [InverseProperty("LibraryBooks")]
        public virtual LibraryBookCategory BookCategoryCode { get; set; }
        [ForeignKey("BookConditionID")]
        [InverseProperty("LibraryBooks")]
        public virtual LibraryBookCondition BookCondition { get; set; }
        [ForeignKey("BookStatusID")]
        [InverseProperty("LibraryBooks")]
        public virtual LibraryBookStatus BookStatus { get; set; }
        [ForeignKey("LibraryBookTypeID")]
        [InverseProperty("LibraryBooks")]
        public virtual LibraryBookType1 LibraryBookType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("LibraryBooks")]
        public virtual School School { get; set; }
        [InverseProperty("LibraryBook")]
        public virtual ICollection<LibraryBookCategoryMap> LibraryBookCategoryMaps { get; set; }
        [InverseProperty("LibraryBook")]
        public virtual ICollection<LibraryBookMap> LibraryBookMaps { get; set; }
        [InverseProperty("Book")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
    }
}
