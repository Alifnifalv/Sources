using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Jobs;
using EntityGenerator.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Domain.Entity.Supports.Models.Mutual;
using Microsoft.VisualBasic;
using System.Globalization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Entity.Setting.Models;
using Eduegate.Utilities.SSRSHelper;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Repository.Recruitment
{
    public class RecruitmentRepository
    {

        #region Registration  ---  Start
        public RegisterUserDTO NewRegistration(RegisterUserDTO registrationDTO) 
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    //Duplicate Validation with emailId
                    var logins = db.RecruitmentLogins.Where(x => x.EmailID == registrationDTO.EmailID)
                        .AsNoTracking()
                        .ToList();

                    if(logins.Count > 0)
                    {
                        registrationDTO.IsError = true;
                        registrationDTO.ReturnMessage = "User is already registered against the mail ID! please check";
                    }
                    else
                    {
                        var roleID = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>("Recruitment_Portal_RoleID").ToString());
                        
                        var PasswordSalt = PasswordHash.CreateHash(registrationDTO.Password);
                        //encryt the value to save in the DB as Password
                        var Password = StringCipher.Encrypt(registrationDTO.Password, PasswordSalt);

                        var recruitmentLogin = new RecruitmentLogin()
                        {
                            UserID = registrationDTO.EmailID,
                            EmailID = registrationDTO.EmailID,
                            UserName = registrationDTO.FirstName,
                            Password = Password,
                            PasswordSalt = PasswordSalt,
                            IsActive = false,
                            RoleID = roleID,
                            CreatedDate = DateTime.Now,
                        };

                        db.RecruitmentLogins.Add(recruitmentLogin);

                        recruitmentLogin.JobSeekers.Add(new JobSeeker()
                        {
                            LoginID = recruitmentLogin.LoginID,
                            FirstName = registrationDTO.FirstName,
                            LastName = registrationDTO.LastName,
                            EmailID= registrationDTO.EmailID,
                            MobileNumber = registrationDTO.TelephoneNo,
                        });

                        db.Entry(recruitmentLogin).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        db.SaveChanges();

                        registrationDTO.IsError = false;
                        registrationDTO.ReturnMessage = "Registered Successfully";
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<RecruitmentRepository>.Fatal(ex.Message, ex);
                    registrationDTO.ReturnMessage = "Something went wrong! please contact admin";
                    registrationDTO.IsError = true;
                }

            }
            return registrationDTO;
        }
        #endregion Registration --- End

        public RegisterUserDTO UpdateUserProfile(RegisterUserDTO registrationDTO, CallContext context)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var userProfile = db.JobSeekers
                                    .Include(x => x.JobSeekerSkillMaps)
                                    .AsNoTracking()
                                    .FirstOrDefault(x => x.SeekerID == registrationDTO.SeekerID);

                    var login = registrationDTO.ConfirmPassword != null ? db.RecruitmentLogins
                            .AsNoTracking().FirstOrDefault(x => x.LoginID == context.LoginID) : null;

                    db.JobSeekerSkillMaps.RemoveRange(userProfile.JobSeekerSkillMaps);
                    db.SaveChanges();

                    if (userProfile == null)
                    {
                        registrationDTO.IsError = true;
                        registrationDTO.ReturnMessage = "Something went wrong !";
                    }
                    else
                    {
                        userProfile.FirstName = registrationDTO.FirstName;
                        userProfile.MiddleName = registrationDTO.MiddleName;
                        userProfile.LastName = registrationDTO.LastName;
                        userProfile.EmailID = registrationDTO.EmailID;
                        userProfile.MobileNumber = registrationDTO.MobileNumber;
                        userProfile.PhoneNumber = registrationDTO.PhoneNumber;
                        userProfile.DateOfBirth = string.IsNullOrEmpty(registrationDTO.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(registrationDTO.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture);
                        userProfile.TotalWorkExperience = registrationDTO.TotalWorkExperience;
                        userProfile.ReferenceCode = registrationDTO.ReferenceCode;
                        userProfile.Facebook = registrationDTO.Facebook;
                        userProfile.Twitter = registrationDTO.Twitter;
                        userProfile.Instagram = registrationDTO.Instagram;
                        userProfile.LinkedIn = registrationDTO.LinkedIn;
                        userProfile.ProfileContentID = registrationDTO.ProfileContentID;
                        userProfile.Age = registrationDTO.Age;
                        userProfile.BloodGroupID = registrationDTO.BloodGroup.Key != null ? int.Parse(registrationDTO.BloodGroup.Key) : null;
                        userProfile.PassportNumber = registrationDTO.PassportNumber;
                        userProfile.PassportExpiry = string.IsNullOrEmpty(registrationDTO.PassportExpiryString) ? (DateTime?)null : DateTime.ParseExact(registrationDTO.PassportExpiryString, dateFormat, CultureInfo.InvariantCulture);
                        userProfile.PassportIssueCountryID = registrationDTO.PassportIssueCountry.Key != null ? int.Parse(registrationDTO.PassportIssueCountry.Key) : null;
                        userProfile.GenderID = registrationDTO.Gender.Key != null ?  byte.Parse(registrationDTO.Gender.Key) : null;
                        userProfile.CountryID = registrationDTO.Country.Key != null ? int.Parse(registrationDTO.Country.Key) : null;
                        userProfile.NationalityID = registrationDTO.Nationality.Key != null ? int.Parse(registrationDTO.Nationality.Key) : null;
                        userProfile.QualificationID = registrationDTO.Qualification.Key != null ? byte.Parse(registrationDTO.Qualification.Key) : null;
                        userProfile.NationalID = registrationDTO.NationalID;

                        db.Entry(userProfile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        if(login != null)
                        {
                            var PasswordSalt = PasswordHash.CreateHash(registrationDTO.ConfirmPassword);
                            //encryt the value to save in the DB as Password
                            var Password = StringCipher.Encrypt(registrationDTO.ConfirmPassword, PasswordSalt);

                            login.PasswordSalt = PasswordSalt;
                            login.Password = Password;
                            login.UpdatedBy = (int?)context.LoginID;
                            login.UpdatedDate = DateTime.Now;

                            db.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        if (registrationDTO.Skill.Count > 0)
                        {
                            foreach (var skill in registrationDTO.Skill)
                            {
                                userProfile.JobSeekerSkillMaps.Add(new JobSeekerSkillMap()
                                {
                                    SeekerID = userProfile.SeekerID,
                                    Description = skill.Value
                                });
                            }

                            foreach(var skillenitity in userProfile.JobSeekerSkillMaps)
                            {
                                db.Entry(skillenitity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        db.SaveChanges();

                        registrationDTO.IsError = false;
                        registrationDTO.ReturnMessage = "Profile updated successfully";
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<RecruitmentRepository>.Fatal(ex.Message, ex);
                    registrationDTO.ReturnMessage = "Something went wrong! please contact admin";
                    registrationDTO.IsError = true;
                }

            }
            return registrationDTO;
        }

        public RegisterUserDTO LoginValidate(RegisterUserDTO loginDTO) 
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var getAuth = db.RecruitmentLogins
                                                .Where(auth => auth.EmailID  == loginDTO.EmailID).AsNoTracking().ToList().LastOrDefault();

                    if (getAuth != null)
                    {
                        if (getAuth.IsActive == false)
                        {
                            loginDTO.IsError = true;
                            loginDTO.ReturnMessage = "login failed ! Please contact the administrator.";
                        }
                        else
                        {
                            string encryptPassword = "";

                            encryptPassword = StringCipher.Encrypt(loginDTO.Password, getAuth.PasswordSalt);

                            loginDTO.Password = encryptPassword;

                            if (getAuth.Password == loginDTO.Password)
                            {
                                loginDTO.IsError = false;
                                loginDTO.LoginID = getAuth.LoginID;
                                loginDTO.UserID = getAuth.UserID;
                                loginDTO.EmailID = getAuth.EmailID;
                            }
                            else
                            {
                                loginDTO.IsError = true;
                                loginDTO.ReturnMessage = "Invalid email or password. Please check.";
                            }
                        }

                    }
                    else
                    {
                        loginDTO.ReturnMessage = "There is no registration found against the mail id to Login! Please register first.";
                        loginDTO.IsError = true;
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<RecruitmentRepository>.Fatal(ex.Message, ex);
                    loginDTO.ReturnMessage = "Something went wrong! please contact admin";
                    loginDTO.IsError = true;
                }

            }
            return loginDTO;
        }

        public RegisterUserDTO GetUserDetails(long? id)
        {
            var result = new RegisterUserDTO();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var getData = db.RecruitmentLogins
                                                .AsNoTracking()
                                                .FirstOrDefault(x => x.LoginID == id);

                    if (getData != null)
                    {
                        result = new RegisterUserDTO()
                        {
                            LoginID = getData.LoginID,
                            UserName = getData.UserName,
                            EmailID = getData.EmailID,
                            UserID = getData.UserID,
                        };
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<RecruitmentRepository>.Fatal(ex.Message, ex);
                    result.ReturnMessage = "Something went wrong! please contact admin";
                    result.IsError = true;
                }

            }
            return result;
        }
        
        public RegisterUserDTO GetProfileDetails(long? loginID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var result = new RegisterUserDTO();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var getData = db.JobSeekers
                        .Include(x => x.Gender)
                        .Include(x => x.Country)
                        .Include(x => x.Nationality)
                        .Include(x => x.PassportIssueCountry)
                        .Include(x => x.BloodGroup)
                        .Include(x => x.Qualification)
                        .Include(x => x.JobSeekerSkillMaps)
                        .AsNoTracking().FirstOrDefault(x => x.LoginID == loginID);
                    if (getData != null)
                    {
                        result = new RegisterUserDTO()
                        {
                            SeekerID = getData.SeekerID,
                            LoginID = getData.LoginID,
                            FirstName = getData.FirstName,
                            MiddleName = getData.MiddleName,
                            TotalWorkExperience = getData.TotalWorkExperience,
                            LastName = getData.LastName,
                            DateOfBirthString = getData.DateOfBirth.HasValue ? getData.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            PassportExpiryString = getData.PassportExpiry.HasValue ? getData.PassportExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            PhoneNumber = getData.PhoneNumber,
                            MobileNumber = getData.MobileNumber,
                            EmailID = getData.EmailID,
                            ReferenceCode = getData.ReferenceCode,
                            ProfileContentID = getData.ProfileContentID,
                            Age = getData.Age,
                            PassportNumber = getData.PassportNumber,
                            NationalID = getData.NationalID,

                            Facebook = getData.Facebook,
                            Twitter = getData.Twitter,
                            Instagram = getData.Instagram,
                            LinkedIn = getData.LinkedIn,

                            Gender = getData.GenderID.HasValue ? new KeyValueDTO()
                            {
                                Key = getData.GenderID.ToString(),
                                Value = getData.Gender?.Description.ToString()
                            }: new KeyValueDTO(),

                            Country = getData.CountryID.HasValue ? new KeyValueDTO()
                            {
                                Key = getData.CountryID.ToString(),
                                Value = getData.Country?.CountryName.ToString()
                            }: new KeyValueDTO(),
                            
                            Nationality = getData.NationalityID.HasValue ? new KeyValueDTO()
                            {
                                Key = getData.NationalityID.ToString(),
                                Value = getData.Nationality?.NationalityName.ToString()
                            }: new KeyValueDTO(),

                            Qualification = getData.QualificationID.HasValue ? new KeyValueDTO()
                            {
                                Key = getData.QualificationID.ToString(),
                                Value = getData.Qualification?.QualificationName.ToString()
                            }: new KeyValueDTO(),  
                            
                            BloodGroup = getData.BloodGroupID.HasValue ? new KeyValueDTO()
                            {
                                Key = getData.BloodGroupID.ToString(),
                                Value = getData.BloodGroup?.BloodGroupName.ToString()
                            }: new KeyValueDTO(),   
                            
                            PassportIssueCountry = getData.PassportIssueCountryID.HasValue ? new KeyValueDTO()
                            {
                                Key = getData.PassportIssueCountryID.ToString(),
                                Value = getData.PassportIssueCountry?.CountryName.ToString()
                            }: new KeyValueDTO(),

                            Skill = getData.JobSeekerSkillMaps.Select(x => new KeyValueDTO()
                            {
                                Key = x.SkillMapID.ToString(),
                                Value = x.Description,
                            }).ToList(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<RecruitmentRepository>.Fatal(ex.Message, ex);
                    result.ReturnMessage = "Something went wrong! please contact admin";
                    result.IsError = true;
                }
            }
            return result;
        }

        public OperationResultDTO GenerateOTPByEmailID(string emailID)
        {
            var data = new OperationResultDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                RecruitmentLogin login = dbContext.RecruitmentLogins.Where(l => l.EmailID == emailID).AsNoTracking().FirstOrDefault();

                if (login != null)
                {
                    string GetOTP =  GenerateOTP();
                    login.OTP = GetOTP;

                    dbContext.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = GetOTP
                    };
                }
                else
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Invalid Account..!"
                    };
                }
            }

            return data;
        }

        public static string GenerateOTP()
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var str = new string(stringChars1);
            return str;
        }

        public OperationResultDTO OTPValidate(RegisterUserDTO loginDTO)
        {
            var result = new OperationResultDTO();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var getOTP = db.RecruitmentLogins.Where(auth => auth.EmailID == loginDTO.EmailID).AsNoTracking().ToList().LastOrDefault();
                
                if (getOTP != null)
                {
                    if (getOTP.OTP == loginDTO.OTP)
                    {
                        result = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                             Message = "OTP has been validated successfully!",
                        };
                    }
                    else
                    {
                        result = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "OTP validation failed",
                        };
                    }
                }
                else
                {
                    result = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = null,
                    };
                }
            }
            return result;
        }

        public JobApplicationDTO ApplyForJob(JobApplicationDTO applicationDTO,CallContext context)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var applicantExp = db.JobSeekers.FirstOrDefault(x => x.LoginID == context.LoginID).TotalWorkExperience;
                var experienceNeed = db.AvailableJobs.FirstOrDefault(x => x.JobIID == applicationDTO.JobID).TotalYearOfExperience;

                var checkExperience = false;

                if(experienceNeed != null || experienceNeed != 0)
                {
                    checkExperience = true;
                }
                else
                {
                    checkExperience = false;
                }

                try
                {
                    //Duplicate Validation with emailId
                    var logins = db.JobApplications
                        .Include(x => x.Applicant)
                        .Where(x => x.Applicant.LoginID == context.LoginID && x.JobID == applicationDTO.JobID)
                        .AsNoTracking()
                        .ToList();

                    //Validation checks 
                    if (logins.Count > 0)
                    {
                        applicationDTO.IsError = true;
                        applicationDTO.ReturnMessage = "Already applied for this job please check!";
                    }
                    else if (applicationDTO.CountryOfResidence == null || applicationDTO.CountryOfResidence?.Key == null)
                    {
                        applicationDTO.IsError = true;
                        applicationDTO.ReturnMessage = "Please select Country of Residence!";
                    }
                    else if (applicationDTO.TotalYearOfExperience == null)
                    {
                        applicationDTO.IsError = true;
                        applicationDTO.ReturnMessage = "Please enter Total Year of Experience !";
                    }
                    else if (checkExperience && (applicantExp < experienceNeed || applicationDTO.TotalYearOfExperience < experienceNeed))
                    {
                        applicationDTO.IsError = true;
                        applicationDTO.ReturnMessage = $"Your profile indicates that you have {applicantExp} of experience. Please ensure you enter a valid experience.";
                    }
                    else if (applicationDTO.CVContentID == null)
                    {
                        applicationDTO.IsError = true;
                        applicationDTO.ReturnMessage = "Please upload latest CV !";
                    }
                    else
                    {
                        var jobApplication = new JobApplication()
                        {
                            ApplicantID = db.JobSeekers.FirstOrDefault(x => x.LoginID == context.LoginID).SeekerID,
                            JobID = applicationDTO.JobID,
                            AppliedDate = DateTime.Now,
                            ReferenceCode = applicationDTO.ReferenceCode,
                            CountryOfResidenceID = applicationDTO.CountryOfResidence != null ? int.Parse(applicationDTO.CountryOfResidence.Key) : null,
                            TotalYearOfExperience = applicationDTO.TotalYearOfExperience,
                            CVContentID = applicationDTO.CVContentID,
                            Remarks = applicationDTO.Remarks,
                        };

                        db.Entry(jobApplication).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        db.SaveChanges();

                        applicationDTO.IsError = false;
                        applicationDTO.ReturnMessage = "Applied Successfully";
                    }
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<RecruitmentRepository>.Fatal(ex.Message, ex);
                    applicationDTO.ReturnMessage = "Something went wrong! please contact admin";
                    applicationDTO.IsError = true;
                }

            }
            return  applicationDTO;
        }

        public OperationResultDTO UpdateIsActiveStatus(string mailID)
        {
            var data = new OperationResultDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                RecruitmentLogin login = dbContext.RecruitmentLogins.Where(l => l.EmailID == mailID).AsNoTracking().FirstOrDefault();

                if (login != null)
                {
                    login.IsActive = true;

                    dbContext.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = null
                    };
                }
                else
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Something went wrong please contact admin..!"
                    };
                }
            }

            return data;
        }
        
        
        public OperationResultDTO ResetLoginPassword(RegisterUserDTO registrationDTO)
        {
            var data = new OperationResultDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                RecruitmentLogin login = dbContext.RecruitmentLogins.Where(l => l.EmailID == registrationDTO.EmailID).AsNoTracking().FirstOrDefault();

                if (login != null)
                {
                    var PasswordSalt = PasswordHash.CreateHash(registrationDTO.Password);
                    //encryt the value to save in the DB as Password
                    var Password = StringCipher.Encrypt(registrationDTO.Password, PasswordSalt);

                    login.Password = Password;
                    login.PasswordSalt = PasswordSalt;

                    dbContext.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Your password has been reset successfully."
                    };
                }
                else
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Something went wrong please contact admin..!"
                    };
                }
            }

            return data;
        }
    }
}
