using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Schools
{
    [Table("PackageConfig", Schema = "schools")]
    public partial class PackageConfig
    {
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

        //public byte[] TimeStamps { get; set; }

        [Required]
        public bool? IsActive { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsAutoCreditNote { get; set; }

        public long? CreditNoteAccountID { get; set; }

        public virtual Account CreditNoteAccount { get; set; }

        public virtual ICollection<PackageConfigClassMap> PackageConfigClassMaps { get; set; }

        public virtual ICollection<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }

        public virtual ICollection<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }

        public virtual ICollection<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }
    }
}