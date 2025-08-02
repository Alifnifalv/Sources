//using Eduegate.Domain;
using Eduegate.Domain.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Integrations.Factory;
//using Eduegate.Services.Contracts.School.Students;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
namespace Eduegate.Integrations.Engine
{
   public class LoginSync
    {
        public static void Sync()
        {
            var loginFactory = IntegratorFactory.GetLoginData(ConfigurationManager.AppSettings["Client"]);
            var loginData = loginFactory.GetLoginData();
            foreach(var login in loginData) {
                new Eduegate.Domain.AccountBL(null).SaveLogin(new Services.Contracts.UserDTO()
                {
                    //UserName = login.
                });
            }
        }

    }
}
