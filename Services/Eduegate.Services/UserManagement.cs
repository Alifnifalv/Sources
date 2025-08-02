using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Services.Contracts;

namespace Eduegate.Services
{
    public class UserManagement : IUserManagement
    {
        public UserManagementDTO GetUserManagement(decimal userIID)
        {
            try
            {
                return new UserManagementBL().GetUserManagement(userIID);
            }
            catch(Exception exception)
            {
                Eduegate.Logger.LogHelper<UserManagementDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveUserManagement(UserManagementDTO userManagementDTO)
        {
            try
            {
                return new UserManagementBL().SaveUserManagement(userManagementDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserManagementDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}
