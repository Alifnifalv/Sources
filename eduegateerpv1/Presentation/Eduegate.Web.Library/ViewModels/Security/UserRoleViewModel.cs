using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Admin;

namespace Eduegate.Web.Library.ViewModels.Security
{
    public class UserRoleViewModel : BaseMasterViewModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public Eduegate.Framework.Helper.Enums.UserRole Role {
            get
            {
                return (Framework.Helper.Enums.UserRole)Enum.Parse(typeof(Framework.Helper.Enums.UserRole), RoleID.ToString());
            }
        }

        public static List<UserRoleViewModel> ToVM(List<UserRoleDTO> dtos)
        {
            var roles = new List<UserRoleViewModel>();

           dtos.ForEach(a=> {
               roles.Add(new UserRoleViewModel() { RoleID = a.RoleID, RoleName = a.RoleName });
           });

            return roles;
        }
    }
}
