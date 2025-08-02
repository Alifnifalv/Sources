using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Repository
{
    public class UserMasterRepository
    {
        public Login PasswordSignIn(string userID, string password)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Logins.Where(a => a.LoginEmailID == userID && a.Password == password).FirstOrDefault();
            }
        }

        public string UserRole(long userID)
        {
            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEduegateERPContext"].ConnectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select RoleName from UserMasterUserRoleMap INNER JOIN UserRoles on UserMasterUserRoleMap.RoleID = UserRoles.RoleID Where UserMasterUserRoleMap.UserID = @UserID", conn);
                command.Parameters.Add(new SqlParameter("@UserID", userID));
                var roleName = command.ExecuteScalar();

                if (roleName == null)
                    return "None";
                else
                    return roleName.ToString();
            }
        }

    }
}
