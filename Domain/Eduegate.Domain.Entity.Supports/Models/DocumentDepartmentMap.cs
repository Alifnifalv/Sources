using Eduegate.Domain.Entity.Supports.Models.Mutual;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("DocumentDepartmentMaps", Schema = "cs")]
    public partial class DocumentDepartmentMap
    {
        [Key]
        public long DocumentDepartmentMapIID { get; set; }

        public int? DocumentTypeID { get; set; }

        public long? DepartmentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Department Department { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}