using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Schools
{
    [Table("PackageConfigFeeStructureMaps", Schema = "schools")]
    public partial class PackageConfigFeeStructureMap
    {
        [Key]
        public long PackageConfigFeeStructureMapIID { get; set; }

        public long? PackageConfigID { get; set; }

        public long? FeeStructureID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public bool IsActive { get; set; }

        public virtual PackageConfig PackageConfig { get; set; }
    }
}