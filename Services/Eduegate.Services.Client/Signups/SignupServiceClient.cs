using System;
using Eduegate.Framework;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.Signups;
using Eduegate.Services.Contracts.SignUp.SignUps;
using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Client.Signups
{
    public class SignupServiceClient : BaseClient, ISignupService
    {
        public SignupServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public SignUpGroupDTO GetSignupGroupDetailsByID(int signupGroupID)
        {
            throw new NotImplementedException();
        }

        public List<SignUpDTO> GetActiveSignUpDetailsByEmployeeID(long employeeID)
        {
            throw new NotImplementedException();
        }

        public string SaveSignupSlotRemarkMap(SignupSlotRemarkMapDTO slotRemarkMap)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAvailableSlotsByDate(string stringDate)
        {
            throw new NotImplementedException();
        }

    }
}