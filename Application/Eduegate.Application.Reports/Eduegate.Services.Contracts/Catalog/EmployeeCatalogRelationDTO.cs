using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class EmployeeCatalogRelationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeCatalogRelationsID { get; set; }
        [DataMember]
        public Nullable<short> RelationTypeID { get; set; }
        [DataMember]
        public Nullable<long> RelationID { get; set; }
        [DataMember]
        public Nullable<long> EmployeeID { get; set; }
    }   
}
