using Newtonsoft.Json;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Domain.Entity.CRM.Models;
using Eduegate.Domain.Entity.CRM;
using System;
using Eduegate.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Eduegate.Domain.Entity.School.Models.School;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain.Mappers.CRM.Leads
{
    public class LeadMapper : DTOEntityDynamicMapper
    {
        public static LeadMapper Mapper(CallContext context)
        {
            var mapper = new LeadMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeadDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LeadDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                var entity = dbContext.Leads.Where(X => X.LeadIID == IID)
                    .Include(i => i.AcademicYear1)
                    .Include(i => i.Class)
                    .Include(i => i.Syllabu)
                    .Include(i => i.Contact)
                    .Include(i => i.Nationality)
                    .Include(i => i.Communications).ThenInclude(i => i.CommunicationType)
                    .Include(i => i.Communications).ThenInclude(i => i.EmailTemplate)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new LeadDTO()
                {
                    LeadIID = entity.LeadIID,
                    LeadName = entity.LeadName,
                    LeadCode = entity.LeadCode,
                    LeadTypeID = entity.LeadTypeID,
                    GenderID = entity.GenderID,
                    LeadSourceID = entity.LeadSourceID,
                    EmailAddress = entity.EmailAddress,
                    ContactID = entity.ContactID,
                    MarketSegmentID = entity.MarketSegmentID,
                    IndustryTypeID = entity.IndustryTypeID,
                    RequestTypeID = entity.RequestTypeID,
                    CompanyID = entity.CompanyID,
                    IsOrganization = entity.IsOrganization,
                    OrgnanizationName = entity.OrgnanizationName,
                    StudentName = entity.StudentName,
                    ParentName = entity.ParentName,
                    ClassID = entity.ClassID,
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYear = entity.AcademicYearID.HasValue ? entity.AcademicYear1.Description + " (" + entity.AcademicYear1.AcademicYearCode + ")" : null,
                    SchoolID = entity.SchoolID,
                    LeadStatusID = entity.LeadStatusID,
                    MobileNumber = entity.MobileNumber,
                    DateOfBirth = entity.DateOfBirth,
                    Remarks = entity.Remarks,
                    ClassName = entity.ClassID.HasValue ? entity.Class.ClassDescription : null,
                    CurriculamID = entity.CurriculamID,
                    Curriculam = entity.Syllabu != null ? entity.Syllabu.SyllabusDescription : null,
                    CreatedBy = Convert.ToInt32(entity.CreatedBy),
                    UpdatedBy = Convert.ToInt32(entity.UpdatedBy),
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    NationalityID = entity.NationalityID,
                    Nationality = entity.NationalityID.HasValue ? new KeyValueDTO() { Key = entity.NationalityID.ToString(), Value = entity.Nationality.NationalityName.ToString()} : new KeyValueDTO(),
                };
                LeadContactDetails(entity, dto);
                LeadCommunicationDetails(entity, dto);
                dto.AgeCriteriaWarningMsg = GetAgeCriteriaDetails(dto.ClassID, dto.AcademicYearID, dto.SchoolID, dto.DateOfBirth).AgeCriteriaWarningMsg;
                return dto;
            }
        }

        private void LeadContactDetails(Lead leadData, LeadDTO dto)
        {
            var contactMap = leadData.Contact;

            if (contactMap != null)
            {
                dto.LeadContact.ContactIID = contactMap.ContactIID;
                dto.LeadContact.CivilIDNumber = contactMap.CivilIDNumber;
                dto.LeadContact.AddressName = contactMap.AddressName;
                dto.LeadContact.Flat = contactMap.Flat;
                dto.LeadContact.Block = contactMap.Block;
                dto.LeadContact.TelephoneCode = contactMap.TelephoneCode;
                dto.LeadContact.PhoneNo1 = contactMap.PhoneNo1;
                dto.LeadContact.PhoneNo2 = contactMap.PhoneNo2;
                dto.LeadContact.MobileNo1 = contactMap.MobileNo1;
                dto.LeadContact.MobileNo2 = contactMap.MobileNo2;
                dto.LeadContact.AlternateEmailID1 = contactMap.AlternateEmailID1;
                dto.LeadContact.AlternateEmailID2 = contactMap.AlternateEmailID2;
            }
        }

        private void LeadCommunicationDetails(Lead leadData, LeadDTO dto)
        {
            var leadCommunicationMaps = leadData.Communications;

            if (leadCommunicationMaps != null || leadCommunicationMaps.Count > 0)
            {
                foreach (var communicationMap in leadCommunicationMaps)
                {
                    dto.LeadCommunication.Add(new LeadCommunicationDTO()
                    {
                        CommunicationIID = communicationMap.CommunicationIID,
                        CommunicationTypeID = communicationMap.CommunicationTypeID,
                        CommunicationType = communicationMap.CommunicationTypeID.HasValue ? communicationMap.CommunicationType.TypeName : null,
                        EmailTemplateID = communicationMap.EmailTemplateID,
                        EmailTemplate = communicationMap.EmailTemplateID.HasValue ? communicationMap.EmailTemplate.TemplateName : null,
                        //EmailCC = communicationMap.EmailCC,
                        Email = communicationMap.Email,
                        MobileNumber = communicationMap.MobileNumber,
                        EmailContent = communicationMap.EmailContent,
                        Notes = communicationMap.Notes,
                        CommunicationDate = communicationMap.CommunicationDate,
                        FollowUpDate = communicationMap.FollowUpDate,
                    });
                }
            }
        }

        public LeadDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            string criteriaDetails = null;
            var dtos = new LeadDTO();
            var LeadDTO = new List<LeadDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (classID != 0)
                {
                    var ageData = dbContext.AgeCriterias.Where(X => X.ClassID == classID && X.SchoolID == schoolID && X.AcademicYearID == academicID).AsNoTracking().FirstOrDefault();
                    if (ageData != null)
                    {
                        if (dob <= ageData.BirthTo && dob >= ageData.BirthFrom)
                        {
                            criteriaDetails = " ";
                        }

                        else
                        {
                            criteriaDetails = "DOB Range for the selected class is from " + ageData.BirthFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + " to " + ageData.BirthTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                    }
                }
            }
            dtos.AgeCriteriaWarningMsg = criteriaDetails;
            return dtos;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeadDTO;

            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            String emailBody = "";
            String emailSubject = "";


            if (!toDto.SchoolID.HasValue && !toDto.ClassID.HasValue)
            {
                toDto.SchoolID = byte.Parse(GetDeafaultSchool());
                toDto.AcademicYearID = GetAcademicYear(toDto.AcademicYear, toDto.SchoolID);
                var classData = GetClassID(toDto.SchoolID, toDto.ClassName);
                toDto.ClassID = int.Parse(classData.Key);
                toDto.ClassName = classData.Value;
            }
            if (toDto.AcademicYearID == null || toDto.AcademicYearID == 0)
            {
                throw new Exception("Please Select Academic Year");
            }

            if (toDto.ClassID == null || toDto.ClassID == 0)
            {
                throw new Exception("Please Select Class");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var ageData = dbContext.AgeCriterias.Where(X => X.ClassID == toDto.ClassID && X.SchoolID == toDto.SchoolID && X.AcademicYearID == toDto.AcademicYearID).AsNoTracking().FirstOrDefault();

                if (ageData != null)
                {
                    if (!(toDto.DateOfBirth.Value.Date >= ageData.BirthFrom.Value.Date && toDto.DateOfBirth.Value.Date <= ageData.BirthTo.Value.Date))
                    {
                        throw new Exception("The selected DOB doesn't meet the age criteria set for this class!! Eligible only for " + " " + ageData.BirthFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + " to " + ageData.BirthTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + " " + "For Respective Classes");
                    }
                }
            }

            if (!toDto.LeadSourceID.HasValue && !string.IsNullOrEmpty(toDto.ReferalCode))
            {
                using (var dbContext = new dbEduegateCRMContext())
                {
                    var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ISINPUTSOURCEID", "0");
                    if (settingValue != null && (string.IsNullOrEmpty(settingValue) || settingValue == "0"))
                    {
                        toDto.LeadSourceID = dbContext.Sources.Where(y => y.SourceName.ToLower() == toDto.ReferalCode.ToLower())
                            .AsNoTracking()
                            .Select(z => z.SourceID)
                            .FirstOrDefault();
                    }
                    else
                    {
                        toDto.LeadSourceID = int.Parse(toDto.ReferalCode);
                    }
                }
            }
            var isValid = ValidateField(toDto, "");

            if (isValid.Key.Equals("true"))
            {
                throw new Exception(isValid.Value);
            }

            if (toDto.LeadIID == 0 && string.IsNullOrEmpty(toDto.LeadCode))
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("Lead_" + toDto.LeadTypeID.ToString(), null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'Lead'");
                }
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Lead()
            {
                LeadIID = toDto.LeadIID,
                LeadName = toDto.LeadName,
                LeadCode = toDto.LeadIID == 0 && toDto.LeadCode == string.Empty || toDto.LeadIID == 0 && toDto.LeadCode == null ? sequence.Prefix + sequence.LastSequence : toDto.LeadCode,
                LeadTypeID = toDto.LeadTypeID,
                GenderID = toDto.GenderID,
                LeadSourceID = toDto.LeadSourceID,
                EmailAddress = toDto.EmailAddress,
                ContactID = toDto.ContactID,
                MarketSegmentID = toDto.MarketSegmentID,
                IndustryTypeID = toDto.IndustryTypeID,
                RequestTypeID = toDto.RequestTypeID,
                CompanyID = toDto.CompanyID,
                IsOrganization = toDto.IsOrganization,
                OrgnanizationName = toDto.OrgnanizationName,
                StudentName = toDto.StudentName,
                ParentName = toDto.ParentName,
                ClassID = toDto.ClassID,
                AcademicYearID = toDto.AcademicYearID,
                LeadStatusID = toDto.LeadStatusID,
                MobileNumber = toDto.MobileNumber,
                DateOfBirth = toDto.DateOfBirth,
                ClassName = toDto.ClassName,
                AcademicYear = toDto.AcademicYear,
                ReferalCode = toDto.ReferalCode,
                Remarks = toDto.Remarks,
                SchoolID = toDto.SchoolID,
                NationalityID = toDto.NationalityID,
                CurriculamID = toDto.CurriculamID == null ? null : toDto.CurriculamID,
            };

            entity.Contact = new Contact()
            {
                ContactIID = toDto.LeadContact.ContactIID,
                CivilIDNumber = toDto.LeadContact.CivilIDNumber,
                AddressName = toDto.LeadContact.AddressName,
                Flat = toDto.LeadContact.Flat,
                Block = toDto.LeadContact.Block,
                TelephoneCode = toDto.LeadContact.TelephoneCode,
                PhoneNo1 = toDto.LeadContact.PhoneNo1,
                PhoneNo2 = toDto.LeadContact.PhoneNo2,
                MobileNo1 = toDto.LeadContact.MobileNo1,
                MobileNo2 = toDto.LeadContact.MobileNo2,
                AlternateEmailID1 = toDto.LeadContact.AlternateEmailID1,
                AlternateEmailID2 = toDto.LeadContact.AlternateEmailID2,
            };

            entity.Communications = new List<Communication>();
            if (toDto.LeadEmailCommunication.CommunicationTypeID.HasValue || toDto.LeadEmailCommunication.EmailTemplateID.HasValue)
            {
                entity.Communications.Add(new Communication()
                {
                    CommunicationIID = toDto.LeadEmailCommunication.CommunicationIID,
                    CommunicationTypeID = toDto.LeadEmailCommunication.CommunicationTypeID.HasValue ? toDto.LeadEmailCommunication.CommunicationTypeID : toDto.LeadEmailCommunication.EmailTemplateID.HasValue ? 1 : (byte?)null,//For Email Communication
                    EmailTemplateID = toDto.LeadEmailCommunication.EmailTemplateID,
                    //EmailCC = toDto.LeadEmailCommunication.EmailCC,
                    Email = toDto.LeadEmailCommunication.Email,
                    EmailContent = toDto.LeadEmailCommunication.EmailContent,
                    Notes = toDto.LeadEmailCommunication.Notes,
                    CommunicationDate = DateTime.Now,
                    FollowUpDate = toDto.LeadEmailCommunication.FollowUpDate,
                });
            }

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");


            var schoolData = entity.SchoolID.HasValue ? new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(entity.SchoolID.Value) : null;
            string schoolShortName = schoolData != null ? schoolData?.SchoolShortName?.ToLower() : null;

            var mailParameters = new Dictionary<string, string>()
            {
                { "SCHOOL_SHORT_NAME", schoolShortName},
            };

            using (var dbContext = new dbEduegateCRMContext())
            {
                dbContext.Leads.Add(entity);

                if (entity.LeadIID == 0)
                {
                    entity.CreatedBy = _context?.LoginID;
                    entity.CreatedDate = DateTime.Now;

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    dbContext.SaveChanges();

                    emailBody = @"<br />
                        <p align='left'>Reference Number :" + entity.LeadCode + @"<br />
                        Dear Parent/Guardian,<br /></p>
                        Thank you for choosing Podar Pearl School.<br />
                        Incase there is a vacancy the admissions team will contact you.<br />
                        Meanwhile, if you have query please call below mentioned numbers or <br /> email to admissions@pearlschool.org.<br />
                        Have a nice day!<br />
                        Looking forward to serve you<br /><br />
                        Note : Please keep a note of the above details for future correspondences.<br /><br />
                             Regards,<br />
                             Registrar<br />
                             Phone  : (+974)4444 2555, 44145595 <br />
                             Mobile : 66923729 <br />
                             Email  : admissions@pearlschool.org <br />
                             <a href='http://www.pearlschool.org/'>www.pearlschool.org</a><br />
                             <b>Podar Pearl School </b><br />
                             P.O.Box 33032 Doha,State of Qatar.
                             <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

                    emailSubject = "Online Enquiry Aknowledgement";
                    //var emaildata = new EmailNotificationDTO();
                    try
                    {
                        string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toDto.EmailAddress, emailBody);

                        if (emailBody != "")
                        {
                            if (hostDet.ToLower() == "live")
                            {
                                new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toDto.EmailAddress, emailSubject, mailMessage, EmailTypes.LeadCreation, mailParameters);
                            }
                            else
                            {
                                new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.LeadCreation, mailParameters);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                            ? ex.InnerException?.Message : ex.Message;

                        Eduegate.Logger.LogHelper<string>.Fatal($"Lead creation Mailing failed. Error message: {errorMessage}", ex);
                    }
                }
                else
                {
                    entity.CreatedBy = toDto.CreatedBy;
                    entity.CreatedDate = toDto.CreatedDate;
                    entity.UpdatedBy = _context?.LoginID;
                    entity.UpdatedDate = DateTime.Now;

                    foreach (var communicationDetail in entity.Communications)
                    {
                        if (communicationDetail.CommunicationIID == 0)
                        {
                            dbContext.Entry(communicationDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(communicationDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (entity.Contact != null)
                    {
                        if (entity.Contact.ContactIID == 0)
                        {
                            dbContext.Entry(entity.Contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity.Contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                }
            }

            if (toDto.LeadEmailCommunication.IsSendEmail == true)
            {
                emailBody = @"<br/><p style='font-family:Helvetica;font-size:1rem; font-weight:bold;'>" + toDto.LeadEmailCommunication.EmailContent + "</p><br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                emailSubject = toDto.LeadEmailCommunication.Notes;
                //var emaildata = new EmailNotificationDTO();
                try
                {
                    string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toDto.EmailAddress, emailBody);

                    if (emailBody != "")
                    {
                        if (hostDet.ToLower() == "live")
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toDto.EmailAddress, emailSubject, mailMessage, EmailTypes.LeadCommunication, mailParameters);
                        }
                        else
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.LeadCommunication, mailParameters);
                        }
                    }

                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                        ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"Lead communication Mailing failed. Error message: {errorMessage}", ex);
                }
            }

            return ToDTOString(ToDTO(entity.LeadIID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as LeadDTO;
            var valueDTO = new KeyValueDTO();


            if (!string.IsNullOrEmpty(toDto.StudentName.ToString()))
            {
                var hasDuplicated = IsLeadDuplicated(toDto.StudentName.ToString(), toDto.EmailAddress, toDto.MobileNumber, toDto.ClassID, toDto.LeadIID, toDto.AcademicYearID);
                if (hasDuplicated)
                {
                    valueDTO.Key = "true";
                    valueDTO.Value = "Student already exists, Please try with different Student.";
                }
                else
                {
                    valueDTO.Key = "false";
                }
            }

            return valueDTO;
        }

        public bool IsLeadDuplicated(string studentName, string emailAddress, string mobileNumber, int? classID, long leadID, int? academicYearID)
        {
            var leadsStatusID = new Domain.Setting.SettingBL().GetSettingValue<int>("LEADS_LEAD_STATUS_ID");

            using (var db = new dbEduegateCRMContext())
            {
                List<Lead> stud;

                if (leadID == 0)
                {
                    stud = db.Leads.Where(x => (x.StudentName).ToUpper().Replace(" ", string.Empty) == studentName.ToUpper().Replace(" ", string.Empty) && (x.MobileNumber == mobileNumber || x.EmailAddress == emailAddress) && x.ClassID == classID && x.AcademicYearID == academicYearID && x.LeadStatusID == leadsStatusID).AsNoTracking().ToList();
                }
                else
                {
                    stud = db.Leads.Where(x => x.LeadIID != leadID && x.StudentName.ToUpper().Replace(" ", string.Empty) == studentName.ToUpper().Replace(" ", string.Empty) && (x.MobileNumber == mobileNumber || x.EmailAddress == emailAddress) && x.ClassID == classID).AsNoTracking().ToList();
                }

                if (stud.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public List<KeyValueDTO> GetLeadSource()
        {
            using (var dbContext = new dbEduegateCRMContext())
            {

                var leadSource = from srcs in dbContext.Sources.AsNoTracking()
                                 select new KeyValueDTO
                                 {
                                     Key = srcs.SourceID.ToString(),
                                     Value = srcs.SourceName
                                 };

                return leadSource.ToList();
            }

        }

        public List<KeyValueDTO> GetNationalities()
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                var nationalities = from srcs in dbContext.Nationalities.AsNoTracking()
                                    select new KeyValueDTO
                                    {
                                        Key = srcs.NationalityIID.ToString(),
                                        Value = srcs.NationalityName
                                    };

                return nationalities.ToList();
            }

        }

        private KeyValueDTO GetClassID(byte? schoolID, string ClassName)
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                var classNam = ClassName.Split('-')[0];
                var classDet = (from srcs in dbContext.Classes.AsNoTracking()
                                where srcs.ClassDescription.Replace(" ", string.Empty).ToUpper().Contains(classNam.Replace(" ", string.Empty).ToUpper())
                                && srcs.SchoolID == schoolID
                                select new KeyValueDTO
                                {
                                    Key = srcs.ClassID.ToString(),
                                    Value = srcs.ClassDescription
                                }).FirstOrDefault();

                return classDet;
            }

        }

        public string GetDeafaultSchool()
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                MutualRepository mutualRepository = new MutualRepository();
                var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SCHOOL");
                return settingValue;
            }
        }

        public int? GetAcademicYear(string academicYearCode, byte? schoolID)
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                var acadmicYearID = dbContext.AcademicYears.Where(srcs => srcs.AcademicYearCode == academicYearCode && srcs.SchoolID == schoolID)
                    .AsNoTracking()
                    .Select(s => s.AcademicYearID)
                    .FirstOrDefault();

                return acadmicYearID;
            }
        }

        public string GetDeafaultAcademicYear()
        {
            using (var dbContext = new dbEduegateCRMContext())
            {
                MutualRepository mutualRepository = new MutualRepository();
                var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ACADEMICYEAR-MESHAF");
                return settingValue;
            }
        }

    }
}