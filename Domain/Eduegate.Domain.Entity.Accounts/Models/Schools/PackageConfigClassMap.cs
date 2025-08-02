using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Schools
{
    [Table("PackageConfigClassMaps", Schema = "schools")]
    public partial class PackageConfigClassMap
    {
        [Key]
        public long PackageConfigClassMapIID { get; set; }

        public int? ClassID { get; set; }

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