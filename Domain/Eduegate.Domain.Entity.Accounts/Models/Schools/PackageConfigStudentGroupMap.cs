using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Schools
{
    [Table("PackageConfigStudentGroupMaps", Schema = "schools")]
    public partial class PackageConfigStudentGroupMap
    {
        [Key]
        public long PackageConfigStudentGroupMapIID { get; set; }

        public int? StudentGroupID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? PackageConfigID { get; set; }

        [Required]
        public bool? IsActive { get; set; }

        public virtual PackageConfig PackageConfig { get; set; }
    }
}