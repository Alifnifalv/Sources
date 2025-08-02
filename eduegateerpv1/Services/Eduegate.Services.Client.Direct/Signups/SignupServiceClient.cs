using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Services.Contracts.Signups;
using Eduegate.Services.Signups;

namespace Eduegate.Services.Client.Direct.Signups
{
    public class SignupServiceClient : ISignupService
    {
        SignupService service = new SignupService();

        public SignupServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public SignUpGroupDTO GetSignupGroupDetailsByID(int signupGroupID)
        {
            return service.GetSignupGroupDetailsByID(signupGroupID);
        }

        public List<SignUpDTO> GetActiveSignUpDetailsByEmployeeID(long employeeID)
        {
            return service.GetActiveSignUpDetailsByEmployeeID(employeeID);
        }

        public string SaveSignupSlotRemarkMap(SignupSlotRemarkMapDTO slotRemarkMap)
        {
            return service.SaveSignupSlotRemarkMap(slotRemarkMap);
        }

        public List<KeyValueDTO> GetAvailableSlotsByDate(string stringDate)
        {
            return service.GetAvailableSlotsByDate(stringDate);
        }

    }
}