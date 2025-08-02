using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmployeeCatalogRelations", Schema = "catalog")]
    public partial class EmployeeCatalogRelation
    {
        [Key]
        public long EmployeeCatalogRelationsIID { get; set; }
        public Nullable<short> RelationTypeID { get; set; }
        public Nullable<long> RelationID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual RelationType RelationType { get; set; }
    }
}
