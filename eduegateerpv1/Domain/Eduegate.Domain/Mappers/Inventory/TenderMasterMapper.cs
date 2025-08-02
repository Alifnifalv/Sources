using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Security;
using System.Security.Cryptography;
using System.Text;
using Eduegate.Services.Contracts.Enums;
using System.Globalization;

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
                    .Include(i => i.TenderAuthentications)
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
                    foreach (var dat in entity.TenderAuthentications)
                    {
                        if (dat.UserID != null)
                        {
                            tenderDTO.TenderAuthenticationList.Add(new TenderAuthenticationDTO()
                            {
                                AuthenticationID = dat.AuthenticationID,
                                UserID = dat.UserID,
                                UserName = dat.UserName,
                                EmailID = dat.EmailID,
                                Password = dat.Password,
                                OldPassword = dat.Password,
                                OldPasswordSalt = dat.PasswordSalt,
                                PasswordHint = dat.PasswordHint,
                                PasswordSalt = dat.PasswordSalt,
                                IsActive = dat.IsActive,
                                IsApprover = dat.IsApprover,
                            });
                        }
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
                            throw new Exception("Please provide tender authentication !!");
                        }
                    }
                }

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

                //delete maps
                var entities = dbContext.TenderAuthentications.Where(x => x.TenderID == entity.TenderIID &&
                    !IIDs.Contains(x.AuthenticationID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.TenderAuthentications.RemoveRange(entities);

                entity.TenderAuthentications = new List<TenderAuthentication>();

                foreach (var dat in toDto.TenderAuthenticationList)
                {
                    // Generate a random password of your desired length (for example, 12 characters)  
                    string randomPassword = PasswordGenerator.GenerateRandomPassword(12);
                    dat.Password = randomPassword;

                    // Hash the password (with salt)  
                    var pswrdslt = PasswordHash.CreateHash(randomPassword);

                    entity.TenderAuthentications.Add(new TenderAuthentication()
                    {
                        AuthenticationID = dat.AuthenticationID,
                        UserName = dat.UserName,
                        UserID = dat.UserID,
                        EmailID = dat.EmailID,
                        PasswordSalt = dat.AuthenticationID == 0 ? pswrdslt : dat.OldPasswordSalt ,
                        Password = dat.AuthenticationID == 0 ? StringCipher.Encrypt(randomPassword, pswrdslt) : dat.OldPassword,
                        IsActive = dat.IsActive,
                        IsApprover = dat.IsApprover,
                        PasswordHint = dat.PasswordHint,
                    });

                }

                dbContext.Tenders.Add(entity);
                if (entity.TenderIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.TenderAuthentications.Count > 0)
                    {
                        foreach (var pswrd in entity.TenderAuthentications)
                        {
                            if (pswrd.AuthenticationID == 0)
                            {
                                dbContext.Entry(pswrd).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(pswrd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                //Save Log data for Tender and Bid operations Handling
                //TODO -- while giving reference with Tender and Authendication Tables getting error while save!
                foreach (var auth in entity.TenderAuthentications)
                {
                    var existLog = dbContext.TenderAuthenticationLogs.FirstOrDefault(d => d.TenderID == toDto.TenderIID && d.AuthenticationID == auth.AuthenticationID);

                    if (existLog != null)
                    {
                        existLog.IsActive = auth.IsActive;
                        existLog.IsApprover = auth.IsApprover;

                        dbContext.Entry(existLog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        var logEntity = new TenderAuthenticationLog()
                        {
                            TenderID = auth.TenderID,
                            AuthenticationID = auth.AuthenticationID,
                            IsActive = auth.IsActive,
                            IsApprover = auth.IsApprover,
                        };

                        dbContext.Add(logEntity);
                        dbContext.Entry(logEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();

                        var OGPassword = toDto.TenderAuthenticationList.FirstOrDefault(x => x.EmailID == auth.EmailID).Password;

                        SendMailToCommittiee(auth, OGPassword);
                    }
                }

                return ToDTOString(entityToDTO(entity.TenderIID));
            }
        }

        #region Send Mail to Tender Committee

        public string SendMailToCommittiee(TenderAuthentication authEntity,string OGPassword) 
        {
            //var result = "Mail send Succesfully !";
            var result = "";
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var bidLoginLink = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_BID_LOGIN_LINK");

            var startDate = authEntity.Tender.StartDate.HasValue ? authEntity.Tender.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            var openingDate = authEntity.Tender.OpeningDate.HasValue ? authEntity.Tender.OpeningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            var endDate = authEntity.Tender.EndDate.HasValue ? authEntity.Tender.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                //Send Mail Notification
                String emailDetails = "";
                String emailSub = "";

                emailSub = "Tender Bid Opening Credential";
                emailDetails = @"<h3> Dear " + authEntity.UserName + @"</h3>
                <p style=""font-family:Helvetica;font-size:1rem;font-weight:bold"">I hope this message finds you well. Please find below the details related to the tender: <b>" + authEntity.Tender.Name + @"</b> </p> <br/>
                <p> <b>Username: </b> " + authEntity.UserName + @"</p>
                <p> <b>User ID: </b> " + authEntity.UserID + @"</p>
                <p> <b>Tender Name: </b> " + authEntity.Tender.Name + @"</p>
                <p> <b>Start Date: </b> " + startDate + @"</p>
                <p> <b>Opening Date: </b> " + openingDate + @"</p>
                <p> <b>End Date: </b> " + endDate + @"</p>
                <p> <b>Password: </b> " + OGPassword + @"</p>
                <br/>
                    Now You can login and check the bids, please click on the link below: <br />
                    Link : <a href='"+bidLoginLink+@"'> Click here to login.</a> <br />
                    please use UserID/User name ,EmailID and password for Login. <br />
                <p>If you have any questions or need further assistance, feel free to reach out.</p>
                <p style=""font-size:0.7rem"">Please Note: do not reply to this email as it is a computer generated email</p>";
                

                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(authEntity.EmailID, emailDetails);

                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                var mailParameters = new Dictionary<string, string>();

                if (emailDetails != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(authEntity.EmailID, emailSub, mailMessage, EmailTypes.VendorPortal, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.VendorPortal, mailParameters);
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