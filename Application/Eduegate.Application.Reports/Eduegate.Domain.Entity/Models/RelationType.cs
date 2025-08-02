using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class RelationType
    {
        public RelationType()
        {
            this.EmployeeCatalogRelations = new List<EmployeeCatalogRelation>();
        }

        public short RelationTypeID { get; set; }
        public string RelationName { get; set; }
        public virtual ICollection<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
    }
}
