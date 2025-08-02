namespace Eduegate.Services.Contracts
{
    public interface IUserManagement
    {
        UserManagementDTO GetUserManagement(decimal userIID);

        bool SaveUserManagement(UserManagementDTO userManagementDTO);
    }
}