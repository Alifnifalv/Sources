using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeCatalogRelations", Schema = "catalog")]
    public partial class EmployeeCatalogRelation
    {
        [Key]
        public long EmployeeCatalogRelationsIID { get; set; }
        public short? RelationTypeID { get; set; }
        public long? RelationID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("RelationTypeID")]
        [InverseProperty("EmployeeCatalogRelations")]
        public virtual RelationType RelationType { get; set; }
    }
}
