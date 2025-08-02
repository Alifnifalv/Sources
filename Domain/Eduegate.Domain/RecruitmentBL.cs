using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Repository.Recruitment;
using Eduegate.Domain.Repository.School;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Services.Contracts.Notifications;
using System.Collections.Generic;


namespace Eduegate.Domain
{
    public class RecruitmentBL
    {
        private static Eduegate.Framework.CallContext _callContext { get; set; }

        public RecruitmentBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public RegisterUserDTO LoginValidate(RegisterUserDTO loginDTO)
        {
            var returnData = new RecruitmentRepository().LoginValidate(loginDTO);

            return returnData;
        }  
        
        public OperationResultDTO OTPValidate(RegisterUserDTO loginDTO) 
        {
            var returnData = new RecruitmentRepository().OTPValidate(loginDTO);
            return returnData;
        }

        public RegisterUserDTO GetUserDetails(long? id)
        {
            var returnData = new RecruitmentRepository().GetUserDetails(id);

            return returnData;
        }

        public List<JobsDTO> GetAvailableJobList()
        {
            var dtoList = new List<JobsDTO>();
            dtoList = Mappers.HR.Employment.JobApplicationManagementMapper.Mapper(_callContext).GetAvailableJobList();

            return dtoList;
        }
        
        public RegisterUserDTO GetProfileDetails(long? loginID) 
        {
            var dto = new RegisterUserDTO();

            dto = new RecruitmentRepository().GetProfileDetails(loginID);

            return dto;
        }

        public RegisterUserDTO RegisterUser(RegisterUserDTO registrationDTO)
        {
            var returnData = new RecruitmentRepository().NewRegistration(registrationDTO);

            return returnData;
        } 
        
        public RegisterUserDTO UpdateUserProfile(RegisterUserDTO registrationDTO)
        {
            var returnData = new RecruitmentRepository().UpdateUserProfile(registrationDTO,_callContext);
            return returnData;
        }
        
        public OperationResultDTO GenerateAndSendMailOTP(RegisterUserDTO registrationDTO)
        {
            var returnData = new RecruitmentRepository().GenerateOTPByEmailID(registrationDTO.EmailID);

            if(returnData.operationResult == Framework.Contracts.Common.Enums.OperationResult.Success)
            {
                var emailTemplate = EmailTemplateMapper.Mapper(_callContext).GetEmailTemplateDetails(EmailTemplates.OTPMail.ToString());

                if(emailTemplate != null)
                {
                    SendOTPMail(registrationDTO, emailTemplate,returnData.Message);
                }
            }

            return returnData;
        }

        public string SendOTPMail(RegisterUserDTO dto, NotificationEmailTemplateDTO emailTemplate, string OTP)
        {
            string classCode = string.Empty;

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            var defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
            var companyName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_FULL_NAME");


            var emailSubject = string.Empty;
            var emailBody = string.Empty;
            if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
            {
                emailSubject = emailTemplate.Subject;

                emailBody = emailTemplate.EmailTemplate;

                emailBody = emailBody.Replace("{OTP}", OTP);
            }

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).PopulateBody(dto.EmailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(dto.EmailID, emailSubject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                }
            }

            return null;
        }

        public OperationResultDTO UpdateIsActiveStatus(RegisterUserDTO registrationDTO)
        {
            var returnData = new RecruitmentRepository().UpdateIsActiveStatus(registrationDTO.EmailID);

            if (returnData.operationResult == Framework.Contracts.Common.Enums.OperationResult.Success)
            {
                SendWelcomeMail(registrationDTO);
            }

            return returnData;
        }

        public string SendWelcomeMail(RegisterUserDTO dto)
        {
            string classCode = string.Empty;

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            var defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
            var careerUrl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CareerRootUrl");
            var companyName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_FULL_NAME");

            var emailTemplate = EmailTemplateMapper.Mapper(_callContext).GetEmailTemplateDetails(EmailTemplates.JobSeekerRegistration.ToString());

            var emailSubject = string.Empty;
            var emailBody = string.Empty;
            if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
            {
                emailSubject = emailTemplate.Subject;

                emailBody = emailTemplate.EmailTemplate;

                emailBody = emailBody.Replace("{JobSeekerName}", dto.FirstName);
                emailBody = emailBody.Replace("{CompanyName}", companyName);
                emailSubject = emailSubject.Replace("{CompanyName}", companyName);
                emailBody = emailBody.Replace("{EmailID}", dto.EmailID);
                emailBody = emailBody.Replace("{Password}", dto.Password);
                emailBody = emailBody.Replace("{RecruitmentPortalLink}", careerUrl+ "Account/Login");
            }

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).PopulateBody(dto.EmailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(dto.EmailID, emailSubject, mailMessage, EmailTypes.RecruitmentPortal, mailParameters);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.RecruitmentPortal, mailParameters);
                }
            }

            return null;
        }


        public JobApplicationDTO ApplyForJob(JobApplicationDTO applicationDTO) 
        {
            var returnData = new RecruitmentRepository().ApplyForJob(applicationDTO,_callContext);

            return returnData;
        }

        public List<JobApplicationDTO> GetAppliedJobList()
        {
            var dtoList = new List<JobApplicationDTO>();
            dtoList = Mappers.HR.Employment.JobApplicationManagementMapper.Mapper(_callContext).GetAppliedJobList();

            return dtoList;
        }
        
        public List<JobInterviewMapDTO> GetMyInterviewList()
        {
            var dtoList = new List<JobInterviewMapDTO>();
            dtoList = Mappers.HR.Employment.JobInterviewManagementMapper.Mapper(_callContext).GetMyInterviewList();

            return dtoList;
        } 


        public List<JobApplicationDTO> GetApplicantsForShortList(long ID,bool? isShortListed)
        {
            var dtoList = new List<JobApplicationDTO>();
            dtoList = Mappers.HR.Employment.JobApplicationManagementMapper.Mapper(_callContext).GetApplicantsForShortList(ID, isShortListed);

            return dtoList;
        } 
        
        public string JobInterviewApplicantResponse(bool? response, long? interviewID, long? applicantID)
        {
            var result = Mappers.HR.Employment.JobInterviewManagementMapper.Mapper(_callContext).JobInterviewApplicantResponse(response,interviewID,applicantID);
            return null;
        }

        public OperationResultDTO ResetLoginPassword(RegisterUserDTO registrationDTO)
        {
            var returnData = new RecruitmentRepository().ResetLoginPassword(registrationDTO);
            return returnData;
        }

        public JobDescriptionDTO GetJobDescriptionByJDMasterID(long JDMasterID)
        {
            var result = Mappers.HR.Payroll.JobDescriptionMapper.Mapper(_callContext).GetJobDescriptionByJDMasterID(JDMasterID);

            return result;
        }

        public List<EmployeeJobDescriptionDTO> GetJDListByLoginID()
        {
            var dtoList = new List<EmployeeJobDescriptionDTO>();
            dtoList = Mappers.HR.Payroll.EmployeeJobDescriptionMapper.Mapper(_callContext).GetJDListByLoginID();

            return dtoList;
        }

        public OperationResultDTO MarkJDasAgreed(long iid)
        {
            var result = Mappers.HR.Payroll.EmployeeJobDescriptionMapper.Mapper(_callContext).MarkJDasAgreed(iid);
            return result;
        }
    }
}
