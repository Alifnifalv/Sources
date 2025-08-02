using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Entity.School.Models;

namespace Eduegate.Domain.Mappers.School.School
{
    public class ParentMapper : DTOEntityDynamicMapper
    {
        public static ParentMapper Mapper(CallContext context)
        {
            var mapper = new ParentMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<GuardianDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private GuardianDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Parents.Where(x => x.ParentIID == IID)
                    .Include(i => i.Login)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Nationality)
                    .Include(i => i.Country1)
                    .Include(i => i.Nationality2)
                    .Include(i => i.Country2)
                    .Include(i => i.Nationality1)
                    .Include(i => i.Country3)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private GuardianDTO ToDTO(Parent entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var parentDTO = new GuardianDTO()
            {
                ParentIID = entity.ParentIID,
                FatherName = entity.FatherName,
                ParentCode = entity.ParentCode,
                FatherFirstName = entity.FatherFirstName,
                FatherMiddleName = entity.FatherMiddleName,
                FatherLastName = entity.FatherLastName,
                FatherCompanyName = entity.FatherCompanyName,
                FatherOccupation = entity.FatherOccupation,
                GuardianOccupation = entity.GuardianOccupation,
                PhoneNumber = entity.PhoneNumber,
                FatherWhatsappMobileNo = entity.FatherWhatsappMobileNo,
                FatherMobileNumberTwo = entity.FatherMobileNumberTwo,
                FatherEmailID = entity.FatherEmailID,
                FatherProfile = entity.FatherProfile,
                FatherCountryID = entity.FatherCountryID,
                FatherCountry = entity.FatherCountryID.HasValue ? entity.Nationality?.NationalityName : null,
                GuardianRelation = entity.GuardianRelation,
                CanYouVolunteerToHelpOneID = entity.CanYouVolunteerToHelpOneID,
                FatherPassportNumber = entity.FatherPassportNumber,
                FatherPassportCountryofIssueID = entity.FatherPassportCountryofIssueID,
                FatherPassportCountryofIssue = entity.FatherPassportCountryofIssueID.HasValue ? entity.Country1?.CountryName : null,
                FatherPassportNoIssueDate = entity.FatherPassportNoIssueDate,
                FatherPassportNoExpiryDate = entity.FatherPassportNoExpiryDate,
                FatherNationalID = entity.FatherNationalID,
                FatherNationalDNoIssueDate = entity.FatherNationalDNoIssueDate,
                FatherNationalDNoExpiryDate = entity.FatherNationalDNoExpiryDate,
                GuardianName = entity.GuardianName,
                GuardianPhoto = entity.GuardianPhoto,
                MotherFirstName = entity.MotherFirstName,
                MotherMiddleName = entity.MotherMiddleName,
                MotherLastName = entity.MotherLastName,
                MotherCompanyName = entity.MotherCompanyName,
                MotherOccupation = entity.MotherOccupation,
                MotherPhone = entity.MotherPhone,
                MotherWhatsappMobileNo = entity.MotherWhatsappMobileNo,
                MotherEmailID = entity.MotherEmailID,
                CanYouVolunteerToHelpTwoID = entity.CanYouVolunteerToHelpTwoID,
                MotherCountryID = entity.MotherCountryID,
                MotherCountry = entity.MotherCountryID.HasValue ? entity.Nationality2?.NationalityName : null,
                MotherPofile = entity.MotherPofile,
                MotherPassportNumber = entity.MotherPassportNumber,
                MotherPassportCountryofIssueID = entity.MotherPassportCountryofIssueID,
                MotherPassportCountryofIssue = entity.MotherPassportCountryofIssueID.HasValue ? entity.Country2?.CountryName : null,
                MotherPassportNoIssueDate = entity.MotherPassportNoIssueDate,
                MotherPassportNoExpiryDate = entity.MotherPassportNoExpiryDate,
                MotherNationalID = entity.MotherNationalID,
                MotherNationalDNoIssueDate = entity.MotherNationalDNoIssueDate,
                MotherNationalDNoExpiryDate = entity.MotherNationalDNoExpiryDate,
                LoginID = entity.LoginID,
                GuardianFirstName = entity.GuardianFirstName,
                GuardianMiddleName = entity.GuardianMiddleName,
                GuardianLastName = entity.GuardianLastName,
                GuardianTypeID = entity.GuardianTypeID,
                GuardianCompanyName = entity.GuardianCompanyName,
                GuardianPhone = entity.GuardianPhone,
                GuardianWhatsappMobileNo = entity.GuardianWhatsappMobileNo,
                GaurdianEmail = entity.GaurdianEmail,
                GuardianNationalityID = entity.GuardianNationalityID,
                GuardianNationality = entity.GuardianNationalityID.HasValue ? entity.Nationality1?.NationalityName : null,
                GuardianPassportNumber = entity.GuardianPassportNumber,
                GuardianCountryofIssueID = entity.GuardianCountryofIssueID,
                GuardianCountryofIssue = entity.GuardianCountryofIssueID.HasValue ? entity.Country3?.CountryName : null,
                GuardianPassportNoIssueDate = entity.GuardianPassportNoIssueDate,
                GuardianPassportNoExpiryDate = entity.GuardianPassportNoExpiryDate,
                GuardianNationalID = entity.GuardianNationalID,
                GuardianNationalIDNoIssueDate = entity.GuardianNationalIDNoIssueDate,
                GuardianNationalIDNoExpiryDate = entity.GuardianNationalIDNoExpiryDate,
                BuildingNo = entity.BuildingNo,
                FlatNo = entity.FlatNo,
                StreetNo = entity.StreetNo,
                StreetName = entity.StreetName,
                LocationNo = entity.LocationNo,
                LocationName = entity.LocationName,
                ZipNo = entity.ZipNo,
                PostBoxNo = entity.PostBoxNo,
                City = entity.City,
                CountryID = entity.CountryID,
            };

            return parentDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as GuardianDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new Parent()
                {
                    ParentIID = toDto.ParentIID,
                    FatherName = toDto.FatherName,
                    ParentCode = toDto.ParentCode,
                    FatherFirstName = toDto.FatherFirstName,
                    FatherMiddleName = toDto.FatherMiddleName,
                    FatherLastName = toDto.FatherLastName,
                    FatherCompanyName = toDto.FatherCompanyName,
                    FatherOccupation = toDto.FatherOccupation,
                    GuardianOccupation = toDto.GuardianOccupation,
                    PhoneNumber = toDto.PhoneNumber,
                    FatherWhatsappMobileNo = toDto.FatherWhatsappMobileNo,
                    FatherMobileNumberTwo = toDto.FatherMobileNumberTwo,
                    FatherEmailID = toDto.FatherEmailID,
                    FatherProfile = toDto.FatherProfile,
                    FatherCountryID = toDto.FatherCountryID,
                    GuardianRelation = toDto.GuardianRelation,
                    CanYouVolunteerToHelpOneID = toDto.CanYouVolunteerToHelpOneID,
                    FatherPassportNumber = toDto.FatherPassportNumber,
                    FatherPassportCountryofIssueID = toDto.FatherPassportCountryofIssueID,
                    FatherPassportNoIssueDate = toDto.FatherPassportNoIssueDate,
                    FatherPassportNoExpiryDate = toDto.FatherPassportNoExpiryDate,
                    FatherNationalID = toDto.FatherNationalID,
                    FatherNationalDNoIssueDate = toDto.FatherNationalDNoIssueDate,
                    FatherNationalDNoExpiryDate = toDto.FatherNationalDNoExpiryDate,
                    GuardianName = toDto.GuardianName,
                    GuardianPhoto = toDto.GuardianPhoto,
                    MotherFirstName = toDto.MotherFirstName,
                    MotherMiddleName = toDto.MotherMiddleName,
                    MotherLastName = toDto.MotherLastName,
                    MotherCompanyName = toDto.MotherCompanyName,
                    MotherOccupation = toDto.MotherOccupation,
                    MotherPhone = toDto.MotherPhone,
                    MotherWhatsappMobileNo = toDto.MotherWhatsappMobileNo,
                    MotherEmailID = toDto.MotherEmailID,
                    CanYouVolunteerToHelpTwoID = toDto.CanYouVolunteerToHelpTwoID,
                    MotherCountryID = toDto.MotherCountryID,
                    MotherPofile = toDto.MotherPofile,
                    MotherPassportNumber = toDto.MotherPassportNumber,
                    MotherPassportCountryofIssueID = toDto.MotherPassportCountryofIssueID,
                    MotherPassportNoIssueDate = toDto.MotherPassportNoIssueDate,
                    MotherPassportNoExpiryDate = toDto.MotherPassportNoExpiryDate,
                    MotherNationalID = toDto.MotherNationalID,
                    MotherNationalDNoIssueDate = toDto.MotherNationalDNoIssueDate,
                    MotherNationalDNoExpiryDate = toDto.MotherNationalDNoExpiryDate,
                    LoginID = toDto.LoginID,
                    GuardianFirstName = toDto.GuardianFirstName,
                    GuardianMiddleName = toDto.GuardianMiddleName,
                    GuardianLastName = toDto.GuardianLastName,
                    GuardianTypeID = toDto.GuardianTypeID,
                    GuardianCompanyName = toDto.GuardianCompanyName,
                    GuardianPhone = toDto.GuardianPhone,
                    GuardianWhatsappMobileNo = toDto.GuardianWhatsappMobileNo,
                    GaurdianEmail = toDto.GaurdianEmail,
                    GuardianNationalityID = toDto.GuardianNationalityID,
                    GuardianPassportNumber = toDto.GuardianPassportNumber,
                    GuardianCountryofIssueID = toDto.GuardianCountryofIssueID,
                    GuardianPassportNoIssueDate = toDto.GuardianPassportNoIssueDate,
                    GuardianPassportNoExpiryDate = toDto.GuardianPassportNoExpiryDate,
                    GuardianNationalID = toDto.GuardianNationalID,
                    GuardianNationalIDNoIssueDate = toDto.GuardianNationalIDNoIssueDate,
                    GuardianNationalIDNoExpiryDate = toDto.GuardianNationalIDNoExpiryDate,
                    BuildingNo = toDto.BuildingNo,
                    FlatNo = toDto.FlatNo,
                    StreetNo = toDto.StreetNo,
                    StreetName = toDto.StreetName,
                    LocationNo = toDto.LocationNo,
                    LocationName = toDto.LocationName,
                    ZipNo = toDto.ZipNo,
                    PostBoxNo = toDto.PostBoxNo,
                    City = toDto.City,
                    CountryID = toDto.CountryID,
                };

                dbContext.Parents.Add(entity);
                if (entity.ParentIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.ParentIID));
            }
        }

        public GuardianDTO GetParentDetailsByLoginID(long? loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var parentDTO = new GuardianDTO();

                var entity = dbContext.Parents.Where(x => x.LoginID == loginID)
                    .Include(i => i.Login)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Nationality)
                    .Include(i => i.Country1)
                    .Include(i => i.Nationality2)
                    .Include(i => i.Country2)
                    .Include(i => i.Nationality1)
                    .Include(i => i.Country3)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    parentDTO = ToDTO(entity);
                }

                return parentDTO;
            }
        }

        public List<long?> GetParentsLoginIDByClassSection(int? classID, int? sectionID = null)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var loginIDs = new List<long?>();

                var studentList = dbContext.Students.Where(s => s.ClassID == classID && (sectionID.HasValue ? s.SectionID == sectionID : s.SectionID.HasValue) && s.IsActive == true)
                    .Include(i => i.Parent).ThenInclude(i => i.Login)
                    .AsNoTracking()
                    .ToList();

                foreach (var student in studentList)
                {
                    var parentLoginID = student?.Parent?.LoginID;
                    if (parentLoginID.HasValue)
                    {
                        loginIDs.Add(parentLoginID);
                    }
                }

                return loginIDs;
            }
        }

        public long? GetParentLoginIDByStudentID(long? studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDet = dbContext.Students.Where(s => s.StudentIID == studentID)
                    .Include(i => i.Parent).ThenInclude(i => i.Login)
                    .AsNoTracking()
                    .FirstOrDefault();

                return studentDet?.Parent?.LoginID;
            }
        }

        public string GetParentEmailIDByStudentID(long? studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDet = dbContext.Students.Where(s => s.StudentIID == studentID)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .FirstOrDefault();

                return studentDet?.Parent?.GaurdianEmail;
            }
        }

        public List<string> GetParentsEmailIDByClassSection(int? classID, int? sectionID = null)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var emailIDs = new List<string>();

                var studentList = dbContext.Students.Where(s => s.ClassID == classID && (sectionID.HasValue ? s.SectionID == sectionID : s.SectionID.HasValue) && s.IsActive == true)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .ToList();

                foreach (var student in studentList)
                {
                    var emailID = student?.Parent?.GaurdianEmail;
                    if (!string.IsNullOrEmpty(emailID))
                    {
                        emailIDs.Add(emailID);
                    }
                }

                return emailIDs;
            }
        }

    }
}