using Eduegate.Domain;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Mutual;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.MobileAppWrapper;
using Eduegate.Domain.Repository.HR.Employee;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.School.Students;
using System.Text.RegularExpressions;

namespace Eduegate.Services.MobileAppWrapper
{
    public class AppSecurityService : BaseService, IAppSecurityService
    {
        public AppSecurityService(CallContext context)
        {
            CallContext = context;
        }

        public OperationResultDTO Login(UserDTO user)
        {
            return new AppDataBL(CallContext).Login(user);
        }

        #region Parent app login
        public OperationResultDTO ParentLogin(UserDTO user)
        {

            if (!IsEmail(user.LoginEmailID))
            {
                var loginEmailID = !string.IsNullOrEmpty(user.LoginEmailID) ? StudentMapper.Mapper(CallContext).GetParentLoginDetailsByParentCode(user.LoginEmailID) : null;
                if (loginEmailID != null)
                {
                    user.LoginEmailID = loginEmailID;
                }
            }

            var parentDetails = GetParentDetails(user.LoginEmailID);

            if (parentDetails.GaurdianEmail != null)
            {

                return new AppDataBL(CallContext).Login(user, Framework.Helper.Enums.ApplicationType.ParentPortal);
            }
            else
            {
                var error = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Invalide User..!"
                };
                return error;
            }
        }
        private bool IsEmail(string input)
        {
            // Define a regular expression pattern for email validation
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Use Regex.IsMatch to check if the input matches the email pattern
            return Regex.IsMatch(input, emailPattern);
        }
        #endregion End parent app login

        //To Get Parent Details
        public GuardianDTO GetParentDetails(string emailID)
        {
            return StudentMapper
                .Mapper(CallContext)
                .GetParentDetails(emailID);
        }

        public StudentDTO GetStudentDetailsByEmailID(string emailID)
        {
            return StudentMapper
                .Mapper(CallContext)
                .GetStudentDetailsByEmailID(emailID);
        }

        public GuardianDTO GetParentDetailsByParentCode(string parentCode, UserDTO user)
        {
            return StudentMapper
                .Mapper(CallContext)
                .GetParentDetailsByParentCode(parentCode, user);
        }

        //End To Get Parent Details
        public OperationResultDTO StaffLogin(UserDTO user)
        {
            //user.IsDriver = false;
            var message = new OperationResultDTO();

            if (string.IsNullOrEmpty(user.LoginEmailID))
            {
                var loginEmailID = !string.IsNullOrEmpty(user.LoginUserID) ? EmployeeRoleMapper.Mapper(CallContext).GetEmployeeEmailIDByEmployeeCode(user.LoginUserID) : null;
                if (loginEmailID != null)
                {
                    user.LoginEmailID = loginEmailID;
                }
            }

            var employeedetail = new AppDataBL(CallContext).GetEmployeeDetails(user.LoginEmailID);

            if (employeedetail != null)
            {
                //var designationData = EmployeeRoleMapper.Mapper(CallContext).GetDriverDetailsByLoginID(long.Parse(employeedetail.LoginID));

                //if(designationData != null)
                //{
                //    user.IsDriver = true;
                //}

                message = new AppDataBL(CallContext).Login(user, Framework.Helper.Enums.ApplicationType.ERP);
            }
            else
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Only for Employee"
                };

            }
            return message;
        }

        public UserDTO GetUserDetails()
        {
            var details = new AppDataBL(CallContext).GetUserDetails();

            //Only for employee login time
            //if (details != null)
            //{
            //    var employeedetail = new AppDataBL(CallContext).GetEmployeeDetails(details.LoginEmailID);

            //    if (employeedetail != null)
            //    {
            //        var designationData = EmployeeRoleMapper.Mapper(CallContext).GetDriverDetailsByLoginID(long.Parse(employeedetail.LoginID));

            //        if (designationData != null)
            //        {
            //            details.IsDriver = true;
            //        }
            //        else
            //        {
            //            details.IsDriver = false;
            //        }
            //    }
            //}

            return details;
        }

        public UserDTO LogOut()
        {
            return new AppDataBL(CallContext).LogOut();
        }

        public string GenerateApiKey(string uuid, string version)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO ResetPasswordOTPGenerate(string emailID)
        {
            var data = new OperationResultDTO();

            data = LoginMapper.Mapper(CallContext).ResetPasswordOTPGenerate(emailID);

            return data;
        }

        public OperationResultDTO ResetPasswordVerifyOTP(string OTP, string email)
        {
            var data = new OperationResultDTO();

            data = LoginMapper.Mapper(CallContext).ResetPasswordVerifyOTP(OTP, email);

            return data;
        }

        public OperationResultDTO SubmitPasswordChange(string email, string password)
        {
            var data = new OperationResultDTO();

            var user = LoginMapper.Mapper(CallContext).FillUserDeatils(email, password);

            if (user.LoginID != null)
            {
                var passwordResetResult = new AccountBL(CallContext).ResetPassword(user);

                if (passwordResetResult == Common.Success)
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Password Reset Success"
                    };
                }
                else if (passwordResetResult == Common.UnSuccessful)
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Password reset failed!"
                    };
                }
            }
            else
            {
                data = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Something went wrong!"
                };
            }

            return data;
        }

        #region Visitor app login
        public OperationResultDTO VisitorLogin(UserDTO user)
        {
            var result = new OperationResultDTO();

            var visitorDetails = VisitorMapper.Mapper(CallContext).GetVisitorDetails(user.QID, user.PassportNumber);

            if (visitorDetails != null)
            {
                if (visitorDetails.IsError == true)
                {
                    result = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = visitorDetails.ErrorMessage
                    };
                }
                else
                {
                    result = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = ""
                    };
                }
            }
            else
            {
                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "No visitor details found."
                };
            }

            return result;
        }
        #endregion End visitor app login


        #region Student app login
        public OperationResultDTO StudentLogin(UserDTO user)
        {
            var message = new OperationResultDTO();

            if (!IsEmail(user.LoginEmailID))
            {
                var loginEmailID = !string.IsNullOrEmpty(user.LoginEmailID) ? StudentMapper.Mapper(CallContext).GetStudentLoginDetailsByStudentCode(user.LoginEmailID) : null;
                if (loginEmailID != null)
                {
                    user.LoginEmailID = loginEmailID;
                }

                var studentDetails = GetStudentDetailsByEmailID(user.LoginEmailID);

                if (studentDetails.StudentIID > 0)
                {
                    message = new AppDataBL(CallContext).Login(user, Framework.Helper.Enums.ApplicationType.StudentMobileApp);
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Invalide User..!"
                    };
                }
            }

            return message;
        }
        #endregion End Stduent app login

    }
}