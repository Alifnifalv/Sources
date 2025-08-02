using Eduegate.Domain.Entity.SignUp.Models;
using Eduegate.Domain.Mappers.SignUp.SignUps;
using Eduegate.Domain.SignUp.SignUps;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Services.Contracts.Signups;

namespace Eduegate.Services.Signups
{
    public class SignupService : BaseService, ISignupService
    {
        public SignUpGroupDTO GetSignupGroupDetailsByID(int signupGroupID)
        {
            return SignUpGroupMapper.Mapper(CallContext).GetGroupDetailsByID(signupGroupID);
        }

        public List<SignUpDTO> GetActiveSignUpDetailsByEmployeeID(long employeeID)
        {
            return new SignUpBL(CallContext).GetEmployeesActiveSignups(employeeID);
        }

        public string SaveSignupSlotRemarkMap(SignupSlotRemarkMapDTO slotRemarkMap)
        {
            return SignupSlotRemarkMapMapper.Mapper(CallContext).SaveSignupSlotRemarkMap(slotRemarkMap);
        }

        public List<KeyValueDTO> GetAvailableSlotsByDate(string stringDate)
        {
            return SignUpMapper.Mapper(CallContext).GetAvailableSlotsByDate(stringDate);
        }

    }
}