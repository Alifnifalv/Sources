namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.PackageConfig")]
    public partial class PackageConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PackageConfig()
        {
            PackageConfigClassMaps = new HashSet<PackageConfigClassMap>();
            PackageConfigFeeStructureMaps = new HashSet<PackageConfigFeeStructureMap>();
            PackageConfigStudentGroupMaps = new HashSet<PackageConfigStudentGroupMap>();
            PackageConfigStudentMaps = new HashSet<PackageConfigStudentMap>();
        }

        [Key]
        public long PackageConfigIID { get; set; }

        public int? AcadamicYearID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool IsActive { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsAutoCreditNote { get; set; }

        public long? CreditNoteAccountID { get; set; }

        public virtual Account Account { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfigClassMap> PackageConfigClassMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }
    }
}
