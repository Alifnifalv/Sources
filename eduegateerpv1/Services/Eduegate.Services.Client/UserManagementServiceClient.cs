using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Service.Client
{
    public class UserManagementServiceClient: BaseClient, IUserManagement
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string userManagementService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.USER_MANAGEMENT_SERVICE_NAME);

        public UserManagementServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public UserManagementDTO GetUserManagement(decimal userIID)
        {
            return ServiceHelper.HttpGetRequest<UserManagementDTO>(string.Concat(userManagementService, "GetUserManagement?userIID=", userIID), _callContext, _logger);
        }

        public bool SaveUserManagement(UserManagementDTO userManagementDTO)
        {
           var result = ServiceHelper.HttpPostRequest(userManagementService + "SaveUserManagement", userManagementDTO, _callContext);
           return JsonConvert.DeserializeObject<bool>(result);
        }

    }
}
