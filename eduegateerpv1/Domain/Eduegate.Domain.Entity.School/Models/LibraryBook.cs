namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LibraryBooks", Schema = "schools")]
    public partial class LibraryBook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LibraryBook()
        {
            LibraryBookCategoryMaps = new HashSet<LibraryBookCategoryMap>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
            LibraryBookMaps = new HashSet<LibraryBookMap>();
        }

        [Key]
        public long LibraryBookIID { get; set; }

        [StringLength(100)]
        public string BookTitle { get; set; }

        [StringLength(50)]
        public string BookNumber { get; set; }

        [StringLength(20)]
        public string ISBNNumber { get; set; }

        [StringLength(50)]
        public string Publisher { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(20)]
        public string RackNumber { get; set; }

        public int? Quatity { get; set; }

        public decimal? BookPrice { get; set; }

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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

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

        public long? BookCategoryCodeID { get; set; }

        public int? BookCodeSequenceNo { get; set; }

        public bool? IsActive { get; set; }

        public string PlaceOfPublication { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual LibraryBookCategory LibraryBookCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryBookCategoryMap> LibraryBookCategoryMaps { get; set; }

        public virtual LibraryBookCondition LibraryBookCondition { get; set; }

        public virtual LibraryBookStatus LibraryBookStatus { get; set; }

        public virtual LibraryBookType LibraryBookTypes { get; set; }

        public virtual Schools School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryBookMap> LibraryBookMaps { get; set; }
    }
}
