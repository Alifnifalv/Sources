using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Employee
{
    public class EmployeeRoleDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO

    {
         [DataMember]
        public int EmployeeRoleID { get; set; }
        [DataMember]
        public string EmployeeRoleName { get; set; }
    }
}
