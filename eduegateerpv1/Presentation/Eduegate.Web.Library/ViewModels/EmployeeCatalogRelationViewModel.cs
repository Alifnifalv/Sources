using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class EmployeeCatalogRelationViewModel : BaseMasterViewModel
    {
        public long EmployeeCatalogRelationsID { get; set; }
        public Nullable<short> RelationTypeID { get; set; }
        public Nullable<long> RelationID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
    }
}
