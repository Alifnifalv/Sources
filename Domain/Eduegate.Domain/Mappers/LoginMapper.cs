using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Notifications;
using System.Linq;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain.Mappers
{
    public class LoginMapper : IDTOEntityMapper<LoginDTO, Login>
    {
        private CallContext _context;

        public static LoginMapper Mapper(CallContext context)
        {
            var mapper = new LoginMapper();
            mapper._context = context;
            return mapper;
        }
        public LoginDTO ToDTO(Login entity)
        {
            if (entity != null)
            {
                return new LoginDTO()
                {
                    LoginIID = entity.LoginIID,
                    LoginUserID = entity.LoginUserID,
                    LoginEmailID = entity.LoginEmailID,

                    PasswordHint = entity.PasswordHint,
                    ProfileFile = entity.ProfileFile,
                    StatusID = entity.StatusID.IsNotNull() ? (Infrastructure.Enums.LoginUserStatus)entity.StatusID : 0,
                    LastLoginDate = entity.LastLoginDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    RequirePasswordReset = entity.RequirePasswordReset,
                    RegisteredCountryID = entity.RegisteredCountryID.IsNotNull() ? entity.RegisteredCountryID : null,
                    RegisteredIP = entity.RegisteredIP.IsNotNull() ? entity.RegisteredIP : null,
                    RegisteredIPCountry = entity.RegisteredIPCountry.IsNotNull() ? entity.RegisteredIPCountry : null,
                    SiteID = entity.SiteID.IsNotNull() ? entity.SiteID : null
                };
            }
            else return new LoginDTO();
        }

        public Login ToEntity(LoginDTO dto)
        {
            if (dto != null)
            {
                var entity = new Login()
                {
                    LoginIID = dto.LoginIID,
                    LoginUserID = dto.LoginUserID,
                    LoginEmailID = dto.LoginEmailID,
                    UserName = dto.LoginEmailID,
                    ProfileFile = dto.ProfileFile,
                    PasswordHint = dto.PasswordHint,
                    StatusID = dto.StatusID > 0 ? (byte)dto.StatusID : (byte?)null,
                    LastLoginDate = dto.LastLoginDate,
                    UpdatedBy = int.Parse(_context.LoginID.ToString()),
                    UpdatedDate = DateTime.Now,
                    ////TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    RequirePasswordReset = dto.RequirePasswordReset,
                    RegisteredCountryID = dto.RegisteredCountryID.IsNotNull() ? dto.RegisteredCountryID : null,
                    RegisteredIP = dto.RegisteredIP.IsNotNull() ? dto.RegisteredIP : null,
                    RegisteredIPCountry = dto.RegisteredIPCountry.IsNotNull() ? dto.RegisteredIPCountry : null,
                    SiteID = dto.SiteID.IsNotNull() ? dto.SiteID : null
                };

                if (entity.LoginIID == 0)
                {
                    entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                    entity.CreatedDate = DateTime.Now;
                }

                // manage Password Flag
                if (dto.IsRequired)
                {
                    entity.PasswordSalt = PasswordHash.CreateHash(dto.Password);
                    //encryt the value to save in the DB as Password
                    entity.Password = StringCipher.Encrypt(dto.Password, entity.PasswordSalt);
                }

                return entity;
            }
            else return null;
        }

        public List<ClaimSetLoginMap> ToClaimSetLoginMapDTO(UserDTO dto)
        {
            var maps = new List<ClaimSetLoginMap>();

            foreach (var claim in dto.ClaimSets)
            {
                maps.Add(new ClaimSetLoginMap() { LoginID = _context.LoginID, ClaimSetID = long.Parse(claim.Value) });
            }
            return maps;
        }

        public OperationResultDTO ResetPasswordOTPGenerate(string emailID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var data = new OperationResultDTO();

                var hostUser = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EmailUser").ToString();
                var emaildata = new EmailNotificationDTO();

                var fromEmail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EmailFrom").ToString();

                var ccEmail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EmailID").ToString();

                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                try
                {
                    emaildata.TemplateName = "ForgetPassword";
                    emaildata.Subject = "OTP";
                    emaildata.ToEmailID = emailID;
                    emaildata.FromEmailID = fromEmail;
                    emaildata.ToBCCEmailID = ccEmail;
                    emaildata.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
                    emaildata.AdditionalParameters.Add(new Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = "User", ParameterValue = "", ParameterObject = null });

                    Login login = dbContext.Logins.Where(l => l.LoginEmailID == emailID && l.StatusID == 1).AsNoTracking().FirstOrDefault();

                    if (login != null)
                    {
                        string GetOTP = GenerateOTP();
                        emaildata.Subject = "OTP:-" + GetOTP;

                        login.LastOTP = GetOTP;
                        dbContext.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();

                        string Subject = "OTP - Don't share your code with others ";
                        string emailBody = @"<strong style='font-size:1.2em;' font-family:'Times New Roman;'><b>Here is your One Time Password</b></strong><br />
                                        <strong style='font-size:1em;' font-family:'Times New Roman;'>To Validate Your Email Address</strong><br />
                                        <strong style='font-size:3rem;'>" + GetOTP + @"</strong><br /><strong style='font-size:0.6em;' font-family:'Times New Roman;'>Don't share this code with others</strong>";
                        
                        string mailMessage = PopulateBodyForOTP(emailID, "Welcome", "Here is your One Time Password", "Don't share this code with others", "Please note : do not reply to this email as it is a computer generated email", GetOTP);

                        var sendValue = new OperationResultDTO();
                        var mailParameters = new Dictionary<string, string>();

                        if (hostDet.ToLower() == "live")
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(emailID, Subject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                        }
                        else
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, Subject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                        }

                        //if (sendValue.operationResult == OperationResult.Success)
                        //{
                        //    data = new OperationResultDTO()
                        //    {
                        //        operationResult = OperationResult.Success,
                        //        Message = sendValue.Message
                        //    };
                        //}
                        //else
                        //{
                        //    data = new OperationResultDTO()
                        //    {
                        //        operationResult = OperationResult.Error,
                        //        Message = sendValue.Message
                        //    };
                        //}
                        data = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = "OTP Has been sent to respective email!"
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
                catch (Exception ex)
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };

                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"OTP Mailing failed. Error message: {errorMessage}", ex);
                }

                return data;
            }
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

        private string PopulateBodyForOTP(string Name, string content_01, string content_02, string content_03, string content_04, string content_otp)
        {
            string body = "<!DOCTYPE html> <html> <head> <title> </title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta name='viewport' content='width=device-width, initial-scale=1'><meta http-equiv='X-UA-Compatible' content='IE=edge' /><style type='text/css'>@media screen {@font-face {font-family: 'Lato';font-style: normal;font-weight: 400;src: local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff');}@font-face {font-family: 'Lato';font-style: normal;font-weight: 700;src: local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff');}@font-face {font-family: 'Lato';font-style: italic;font-weight: 400;src: local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff');}@font-face {font-family: 'Lato';font-style: italic;font-weight: 700;src: local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff');}}body,table,td,a {-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;}table,td {mso-table-lspace: 0pt;mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}img {border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;}table {border-collapse: collapse !important;}body {height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: none !important;font-size: inherit !important;font-family: inherit !important;font-weight: inherit !important;line-height: inherit !important;}@media screen and (max-width:600px) {h1 {font-size: 32px !important;line-height: 32px !important;}}div[style*='margin: 16px 0;'] {margin: 0 !important;}</style></head><body style='background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td bgcolor='#bd051c' align='center'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> </td></tr></table></td></tr><tr><td bgcolor='#bd051c' align='center' style='padding: 0px 10px 0px 10px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;'><div align='center' style='width:100%;display:inline-block;text-align:center;'><img src='https://parent.pearlschool.org/img/podarlogo_mails.png'  width='320' height='60' border='0' /></div><h1 style='font-size: 48px; font-weight: 400; margin: 2px;'>{content_01}</h1> <img src=' https://img.icons8.com/clouds/100/000000/handshake.png' width='125' height='120' style='display: block; border: 0px;' /></td></tr></table></td></tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td bgcolor='#ffffff' align='left' style='padding: 1rem; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'><p style='margin: 0;text-align:center;'>{content_02}</p></td></tr><tr><td align='left'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td bgcolor='#ffffff' align='center' style='padding: 1rem'><table border='0' cellspacing='0' cellpadding='0'><tr><td align='center' style='border-radius: 3px;'><strong style='font-size: 4rem; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #bd051c; text-decoration: none; padding: 15px 25px; display: inline-block; '>{content_otp}</strong></td></tr></table></td></tr></table></td></tr><tr><td bgcolor='#ffffff' align='left' style='padding: 1rem; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'><p style='margin: 0;text-align:center;margin-bottom:1.5rem;'>{content_03}</p></td></tr><tr><td bgcolor='#ffffff' align='left' style='color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 11px; font-weight: 400; line-height: 25px;'><p style='margin: 0;text-align:center;'>{content_04}</p></td></tr></table></td></tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td bgcolor='black' align='center' style='padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #fffefe; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'><div class='copyrightdiv' style='padding-bottom:1rem;'>Copyright &copy; {YEAR} <a style='text-decoration: none' target='_blank' href='http://pearlschool.org/'><span style='color: white; font-weight: bold;'>&nbsp;&nbsp; PEARL SCHOOL</span></a></div></td></tr></table></td></tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td bgcolor='#f4f4f4' align='left' style='padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;'><br><div class='PoweredBy' style='text-align:center;'><div style='padding-bottom:1rem;'>Powered By: <a style='text-decoration: none; color: #0c7aec;' id='eduegate' href='https://softopsolutionpvtltd.com/' target='_blank'>SOFTOP SOLUTIONS PVT LTD</a></div><a href='https://www.facebook.com/pearladmin1/'><img src='https://parent.pearlschool.org/Images/logo/fb-logo.png' alt='facebook' width='30' height='25' border='0' style='margin-right:.5rem;' /></a><a href='https://www.linkedin.com/company/pearl-school-qatar/?viewAsMember=true'><img src='https://parent.pearlschool.org/Images/logo/linkedin-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' /></a><a href='https://www.instagram.com/pearlschool_qatar/'><img src='https://parent.pearlschool.org/Images/logo/insta-logo.png' alt='instagram' width='30' height='25' border='0' style='margin-right:.5rem;' /></a><a href='https://www.youtube.com/channel/UCFQKYMivtaUgeSifVmg79aQ'><img src='https://parent.pearlschool.org/Images/logo/youtube-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' /></a></div></td></tr></table></td></tr></table></body></html>";

            body = body.Replace("{content_01}", content_01);
            body = body.Replace("{content_02}", content_02);
            body = body.Replace("{content_03}", content_03);
            body = body.Replace("{content_04}", content_04);
            body = body.Replace("{content_otp}", content_otp);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }

        public OperationResultDTO GenerateOTPByEmailID(string emailID)
        {
            var data = new OperationResultDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                Login login = dbContext.Logins.Where(l => l.LoginEmailID == emailID && l.StatusID == 1).AsNoTracking().FirstOrDefault();

                if (login != null)
                {
                    string GetOTP = GenerateOTP();
                    login.LastOTP = GetOTP;
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

        public OperationResultDTO ResetPasswordVerifyOTP(string OTP, string email)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var data = new OperationResultDTO();

                try
                {
                    Login login = dbContext.Logins.Where(l => l.LoginEmailID == email && l.LastOTP == OTP).AsNoTracking().FirstOrDefault();

                    if (login != null)
                    {
                        data = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = ""
                        };
                    }
                    else
                    {
                        data = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Invalid OTP"
                        };
                    }
                }
                catch (Exception ex)
                {
                    data = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return data;
            }
        }

        public UserDTO FillUserDeatils(string email, string password)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                UserDTO userDTO = new UserDTO();

                Login login = dbContext.Logins.Where(l => l.LoginEmailID == email).AsNoTracking().FirstOrDefault();

                if (login != null)
                {
                    userDTO.LoginUserID = login.LoginUserID;
                    userDTO.PasswordSalt = password;
                    userDTO.Password = userDTO.PasswordSalt;
                    userDTO.LoginID = login.LoginIID.ToString();
                }

                return userDTO;
            }
        }

    }
}