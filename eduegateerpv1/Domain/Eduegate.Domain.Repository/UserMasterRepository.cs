using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class UserMasterRepository
    {
        public Login PasswordSignIn(string userID, string password)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Logins.Where(a => a.LoginEmailID == userID && a.Password == password)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public string UserRole(long userID)
        {
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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
