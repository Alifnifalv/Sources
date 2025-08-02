using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.SignUp.SignUps;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Signups
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISignupService" in both code and config file together.
    public interface ISignupService
    {
        public SignUpGroupDTO GetSignupGroupDetailsByID(int signupGroupID);

        public List<SignUpDTO> GetActiveSignUpDetailsByEmployeeID(long employeeID);

        public string SaveSignupSlotRemarkMap(SignupSlotRemarkMapDTO slotRemarkMap);

        public List<KeyValueDTO> GetAvailableSlotsByDate(string stringDate);

    }
}