using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RelationTypes", Schema = "catalog")]
    public partial class RelationType
    {
        public RelationType()
        {
            EmployeeCatalogRelations = new HashSet<EmployeeCatalogRelation>();
        }

        [Key]
        public short RelationTypeID { get; set; }
        [StringLength(100)]
        public string RelationName { get; set; }

        [InverseProperty("RelationType")]
        public virtual ICollection<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
    }
}
