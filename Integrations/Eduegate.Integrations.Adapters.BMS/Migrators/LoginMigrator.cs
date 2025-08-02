using Eduegate.Integrations.Engine.DbContexts;
using Eduegate.Services.Contracts.School.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Integrations.Adapters.BMS
{ 
   public class LoginMigrator
    {
        public List<LoginsDTO> GetLoginData()
        {
            using (var dbContext = new IntegrationDbContext())
            {
                var loginDto = new List<LoginsDTO>();
                foreach (var login in
                    dbContext.Login.FromSqlRaw("Select comn_user_code as LoginUserID,comn_user_name as UserName, "+
                                    "case comn_user_status when 'A' then 1 else 0 end as StatusID from comn.[comn_user]").ToList())
                {
                    loginDto.Add(new LoginsDTO()
                    {
                        RequirePasswordReset = true,
                        LoginUserID= login.LoginUserID,
                        LoginEmailID = login.UserName,  
                        //StatusID = (Eduegate.Services.Contracts.Enum)login.StatusID==1?1:0

                    });
                }

                return loginDto;
            }
        }
    }
}
