using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Eduegate.Domain.Mappers.Inventory
{
    public class TenderMasterMapper : DTOEntityDynamicMapper
    {
        public static TenderMasterMapper Mapper(CallContext context)
        {
            var mapper = new TenderMasterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TenderMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(entityToDTO(IID));
        }

        public TenderMasterDTO entityToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Tenders.Where(x => x.TenderIID == IID)
                    .Include(i => i.TenderType)
                    .Include(i => i.TenderStatus)
                    .Include(i => i.TenderAuthenticationLogs).ThenInclude(j => j.Authentication).ThenInclude(k => k.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private TenderMasterDTO ToDTO(Tender entity)
        {
            var tenderDTO = new TenderMasterDTO();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                if (entity.IsNotNull())
                {
                    tenderDTO = new TenderMasterDTO()
                    {
                        TenderIID = entity.TenderIID,
                        Name = entity.Name,
                        Title = entity.Title,
                        Description = entity.Description,
                        TenderTypeID = entity.TenderTypeID,
                        TenderStatusID = entity.TenderStatusID,
                        TenderType = entity.TenderTypeID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.TenderTypeID.ToString(),
                            Value = entity.TenderType != null ? entity.TenderType.TendorType : null
                        } : new KeyValueDTO(),
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        SubmissionDate = entity.SubmissionDate,
                        OpeningDate = entity.OpeningDate,
                        IsActive = entity.IsActive,
                        IsOpened = entity.IsOpened,
                        NumOfAuthorities = entity.NumOfAuthorities,
                        TenderStatus = entity.TenderStatusID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.TenderStatusID.ToString(),
                            Value = entity.TenderStatus != null ? entity.TenderStatus.Description : null
                        } : new KeyValueDTO(),
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate,
                    };

                tenderDTO.TenderAuthenticationList = new List<TenderAuthenticationDTO>();
                foreach (var dat in entity.TenderAuthenticationLogs)
                {
                        tenderDTO.TenderAuthenticationList.Add(new TenderAuthenticationDTO()
                        {
                            TenderAuthMapIID = dat.TenderAuthMapIID,
                            TenderID = dat.TenderID,
                            AuthenticationID = dat.Authentication.AuthenticationID,
                            UserID = dat.Authentication.UserID,
                            UserName = dat.Authentication.UserName,
                            EmailID = dat.Authentication.EmailID,
                            Password = dat.Authentication.Password,
                            OldPassword = dat.Authentication.Password,
                            OldPasswordSalt = dat.Authentication.PasswordSalt,
                            PasswordHint = dat.Authentication.PasswordHint,
                            PasswordSalt = dat.Authentication.PasswordSalt,
                            IsActive = dat.IsActive,
                            IsApprover = dat.IsApprover,
                            EmployeeID = dat.Authentication.EmployeeID,
                            Employee = dat.Authentication.EmployeeID.HasValue ? new KeyValueDTO()
                            {
                                Key = dat.Authentication.EmployeeID.ToString(),
                                Value = dat.Authentication.Employee != null ? dat.Authentication.Employee?.EmployeeCode + " - " + dat.Authentication.Employee?.FirstName + " " + dat.Authentication.Employee?.MiddleName + " " + dat.Authentication.Employee?.LastName : null
                            } : new KeyValueDTO(),
                        });
                }
            }

            return tenderDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TenderMasterDTO;
            using (var dbContext = new dbEduegateERPContext())
            {
                var type = dbContext.TenderTypes1.FirstOrDefault(x => x.TenderTypeID == toDto.TenderTypeID).TendorType.ToString();

                if (type != null)
                {
                    if (type == "Closed")
                    {
                        if (toDto.TenderAuthenticationList.Count <= 0)
                        {
                            throw new Exception("Please provide Authendication List / Tender Committee !!");
                        }

                        if(toDto.OpeningDate == null || toDto.EndDate == null)
                        {
                            throw new Exception("please select the opening date/End date");
                        }

                        if(toDto.NumOfAuthorities != null && toDto.NumOfAuthorities > toDto.TenderAuthenticationList.Count)
                        {
                            throw new Exception("Number of authorities must be less than or equal to the Authendication/Tender Committee list");
                        }
                    }
                }

                #region Date checking
                if (toDto.SubmissionDate.HasValue && toDto.TenderIID == 0)
                {
                    if (toDto.SubmissionDate.Value.Date < DateTime.Now.Date 
                            || toDto.SubmissionDate.Value.Date < toDto.StartDate.Value.Date
                        || toDto.SubmissionDate.Value.Date < toDto.OpeningDate.Value.Date
                            || toDto.SubmissionDate.Value.Date < toDto.EndDate.Value.Date)
                    {
                        throw new Exception("please select the submission date properly");

                    }
                }

                if (toDto.EndDate.HasValue && toDto.TenderIID == 0)
                {
                    if (toDto.EndDate.Value.Date < DateTime.Now.Date
                        || toDto.EndDate.Value.Date < toDto.StartDate.Value.Date
                            || toDto.EndDate.Value.Date < toDto.OpeningDate.Value.Date)
                    {
                        throw new Exception("please select the end date properly");
                    }
                }
                #endregion 

                var entity = new Tender()
                {
                    TenderIID = toDto.TenderIID,
                    Name = toDto.Name,
                    Title = toDto.Title,
                    Description = toDto.Description,

                    StartDate = toDto.StartDate,
                    EndDate = toDto.EndDate,
                    SubmissionDate = toDto.SubmissionDate,
                    OpeningDate = toDto.OpeningDate,

                    TenderTypeID = toDto.TenderTypeID,
                    TenderStatusID = toDto.TenderStatusID,

                    IsActive = toDto.IsActive,
                    IsOpened = toDto.IsOpened,
                    NumOfAuthorities = toDto.NumOfAuthorities,

                    CreatedBy = (int?)(toDto.TenderIID == 0 ? _context.LoginID : toDto.CreatedBy),
                    CreatedDate = toDto.TenderIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = (int?)(toDto.TenderIID == 0 ? null : _context.LoginID),
                    UpdatedDate = toDto.TenderIID == 0 ? null : DateTime.Now,
                };

                var IIDs = toDto.TenderAuthenticationList
                .Select(a => a.AuthenticationID).ToList();

                //delete maps TODO_BID
                var deleteMapEntity = dbContext.TenderAuthenticationLogs.Where(x => x.TenderID == entity.TenderIID &&
                    !IIDs.Contains((long)x.AuthenticationID)).AsNoTracking().ToList();

                if (deleteMapEntity.IsNotNull())
                    dbContext.TenderAuthenticationLogs.RemoveRange(deleteMapEntity);

                var userList = new List<TenderAuthentication>();

                foreach (var auth in toDto.TenderAuthenticationList)
                {
                    var checkDuplicate = dbContext.TenderAuthentications
                        .AsNoTracking()
                        .FirstOrDefault(e => e.EmailID == auth.EmailID);

                    if (checkDuplicate != null && auth.AuthenticationID == 0)
                    {
                        throw new Exception("The mail id: " + auth.EmailID + " is already existing against the user: " + auth.UserName + " please use existing user selection !");
                    }

                    string randomPassword = PasswordGenerator.GenerateRandomPassword(12);
                    var passwordSalt = PasswordHash.CreateHash(randomPassword);

                    auth.Password = randomPassword;

                    userList.Add(new TenderAuthentication()
                    {
                        AuthenticationID = auth.AuthenticationID,
                        UserName = auth.UserName,
                        UserID = auth.UserID,
                        EmailID = auth.EmailID,
                        PasswordSalt = passwordSalt,
                        Password = StringCipher.Encrypt(randomPassword, passwordSalt),
                        PasswordHint = auth.PasswordHint,
                        EmployeeID = auth.EmployeeID,
                        IsActive = auth.IsActive,
                    });
                }

                foreach (var userEntity in userList)
                {
                    //Insert data only when authendicationID is 0
                    if (userEntity.AuthenticationID == 0)
                    {
                        dbContext.Entry(userEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();
                    }
                    //new screen and user already existing case : authendication password changes for security
                    else if(toDto.TenderIID == 0 && userEntity.AuthenticationID != 0)
                    {
                        dbContext.Entry(userEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }

                // Create logs for TenderAuthentication  
                entity.TenderAuthenticationLogs = new List<TenderAuthenticationLog>();

                foreach (var log in toDto.TenderAuthenticationList)
                {
                    entity.TenderAuthenticationLogs.Add(new TenderAuthenticationLog()
                    {
                        TenderAuthMapIID = log.TenderAuthMapIID,
                        TenderID = toDto.TenderIID != 0 ? toDto.TenderIID : log.TenderID,
                        AuthenticationID = userList.FirstOrDefault(x => x.EmailID == log.EmailID).AuthenticationID,
                        IsActive = log.IsActive,
                        IsApprover = log.IsApprover,
                        
                    });
                }

                if (entity.TenderIID == 0)
                {
                    dbContext.Tenders.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var mapEntity in entity.TenderAuthenticationLogs)
                    {
                        if (mapEntity.TenderAuthMapIID == 0)
                        {
                            dbContext.Entry(mapEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(mapEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        dbContext.SaveChanges();
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                var sendCredentials = toDto.TenderAuthenticationList.Where(c => c.TenderAuthMapIID == 0).ToList();

                if (sendCredentials.Any())
                {
                    SendMailToCommittiee(sendCredentials,toDto);
                }

                return ToDTOString(entityToDTO(entity.TenderIID));
            }
        }

        #region Send Mail to Tender Committee

        public string SendMailToCommittiee(List<TenderAuthenticationDTO> authDto,TenderMasterDTO masterDTO)
        {
            //var result = "Mail send Succesfully !";
            var result = "";
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var vendorUrl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("VendorRootUrl");

            var startDate = masterDTO.StartDate.HasValue ? masterDTO.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            var openingDate = masterDTO.OpeningDate.HasValue ? masterDTO.OpeningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            var endDate = masterDTO.EndDate.HasValue ? masterDTO.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.BidOpeningCredential.ToString());
            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
            var mailParameters = new Dictionary<string, string>();

            foreach (var send in authDto)
            {
                var emailSubject = string.Empty;
                var emailBody = string.Empty;

                if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
                {
                    emailSubject = emailTemplate.Subject;

                    emailBody = emailTemplate.EmailTemplate;

                    emailBody = emailBody.Replace("{TenderName}", masterDTO.Name);
                    emailBody = emailBody.Replace("{StartDate}", startDate);
                    emailBody = emailBody.Replace("{OpeningDate}", openingDate);
                    emailBody = emailBody.Replace("{EndDate}", endDate);
                    emailBody = emailBody.Replace("{BidLoginLink}", vendorUrl+ "bid/bidlogin");
                    emailBody = emailBody.Replace("{UserName}", send.UserName);
                    emailBody = emailBody.Replace("{UserID}", send.UserID);
                    emailBody = emailBody.Replace("{OGPassword}", send.Password);
                    string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(send.EmailID, emailBody);

                    if (emailBody != "")
                    {
                        if (hostDet.ToLower() == "live")
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(send.EmailID, emailSubject, mailMessage, EmailTypes.VendorPortal, mailParameters);
                        }
                        else
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.VendorPortal, mailParameters);
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }

    #region Random PassWord Generator
    public static class PasswordGenerator
    {
        private static readonly char[] ValidCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=-".ToCharArray();

        public static string GenerateRandomPassword(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Password length must be greater than zero.");

            var password = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBuffer = new byte[length];
                rng.GetBytes(randomBuffer);

                for (int i = 0; i < length; i++)
                {
                    // Map the random byte to a valid character index  
                    int index = randomBuffer[i] % ValidCharacters.Length;
                    password.Append(ValidCharacters[index]);
                }
            }

            return password.ToString();
        }

    }

    #endregion
}