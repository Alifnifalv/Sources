using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("RelationTypes", Schema = "catalog")]
    public partial class RelationType
    {
        public RelationType()
        {
            this.EmployeeCatalogRelations = new List<EmployeeCatalogRelation>();
        }

        [Key]
        public short RelationTypeID { get; set; }
        public string RelationName { get; set; }
        public virtual ICollection<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
    }
}
