namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentGroupFeeMasters")]
    public partial class StudentGroupFeeMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentGroupFeeMaster()
        {
            StudentGroupFeeTypeMaps = new HashSet<StudentGroupFeeTypeMap>();
        }

        [Key]
        public long StudentGroupFeeMasterIID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? StudentGroupID { get; set; }

        public int? AcadamicYearID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }
    }
}
