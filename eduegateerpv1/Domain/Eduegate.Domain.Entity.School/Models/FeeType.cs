using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Eduegate.Domain.Entity.School.Models
{
    [Table("FeeTypes", Schema = "schools")]
    public partial class FeeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeType()
        {
            FeeFineTypes = new HashSet<FeeFineType>();
            FeeMasters = new HashSet<FeeMaster>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeTypeID { get; set; }

        [Required]
        [StringLength(20)]
        public string FeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? FeeGroupId { get; set; }

        public byte? FeeCycleId { get; set; }

        public bool? IsRefundable { get; set; }
        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }
        public virtual Schools School { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual FeeCycle FeeCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeFineType> FeeFineTypes { get; set; }

        public virtual FeeGroup FeeGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters { get; set; }

        //public virtual FeeType FeeTypes1 { get; set; }

        //public virtual FeeType FeeType1 { get; set; }
    }
}
