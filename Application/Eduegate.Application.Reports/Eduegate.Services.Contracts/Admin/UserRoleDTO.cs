using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Admin
{
    [DataContract]
    public class UserRoleDTO
    {
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string RoleName { get; set; }
    }
}
