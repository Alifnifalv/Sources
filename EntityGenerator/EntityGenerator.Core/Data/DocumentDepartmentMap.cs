using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("DepartmentID")]
        [InverseProperty("DocumentDepartmentMaps")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("DocumentDepartmentMaps")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
