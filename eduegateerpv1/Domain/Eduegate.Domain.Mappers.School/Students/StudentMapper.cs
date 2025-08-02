using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Globalization;
using Eduegate.Services.Contracts.Contents;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity.Contents;
using System.IO.Compression;


namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentMapper : DTOEntityDynamicMapper
    {

        List<string> validationFields = new List<string>() { "PassportNo", "VisaNo", "NationalIDNo" };
        public static StudentMapper Mapper(CallContext context)
        {
            var mapper = new StudentMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Students.Where(x => x.StudentIID == IID)
                    .Include(i => i.Parent).ThenInclude(i => i.Login).ThenInclude(i => i.LoginRoleMaps)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Grade)
                    .Include(i => i.Stream)
                    .Include(i => i.Gender)
                    .Include(i => i.StudentCategory)
                    .Include(i => i.Cast)
                    .Include(i => i.Relegion)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AcademicYear1)
                    .Include(i => i.Login)
                    .Include(i => i.GuardianType)
                    .Include(i => i.Community)
                    .Include(i => i.Subject2)
                    .Include(i => i.Class1)
                    .Include(i => i.StudentSiblingMaps).ThenInclude(i => i.Student1)
                    .Include(i => i.StudentApplicationDocumentMaps)
                    .Include(i => i.StudentStreamOptionalSubjectMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.StudentGroupMaps).ThenInclude(i => i.StudentGroup)
                    .Include(i => i.StudentStaffMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.StudentPassportDetails).ThenInclude(i => i.Nationality)
                    .Include(i => i.StudentPassportDetails).ThenInclude(i => i.Country)
                    .Include(i => i.StudentPassportDetails).ThenInclude(i => i.Country1)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = ToDTO(entity);

                return ToDTOString(dto);
            }
        }

        //To Get Parent Details
        public GuardianDTO GetParentDetails(string emailID)
        {
            var dto = new GuardianDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var login = dbContext.Logins.Where(x => x.LoginEmailID == emailID).AsNoTracking().FirstOrDefault();
                var parent = login != null ? dbContext.Parents.Where(s => s.LoginID == login.LoginIID).AsNoTracking().FirstOrDefault() : null;
                if (parent != null)
                {
                    dto = new GuardianDTO()
                    {
                        GuardianAddress = parent.GuardianAddress,
                        GuardianName = parent.GuardianName,
                        GuardianPhone = parent.GuardianPhone,
                        GaurdianEmail = emailID,
                        ParentIID = parent.ParentIID
                    };
                }
            }
            return dto;
        }

        public GuardianDTO GetParentDetailsByParentCode(string parentCode, UserDTO user)
        {
            var dto = new GuardianDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var parent = dbContext.Parents.Where(s => s.LoginID != null && s.ParentCode == parentCode)
                    .Include(x => x.Login)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (parent != null)
                {
                    dto = new GuardianDTO()
                    {
                        GuardianAddress = parent.GuardianAddress,
                        GuardianName = parent.GuardianName,
                        GuardianPhone = parent.GuardianPhone,
                        GaurdianEmail = parent.Login.LoginEmailID,
                        ParentIID = parent.ParentIID
                    };

                    user.LoginID = parent.LoginID.ToString();
                    user.LoginEmailID = parent.Login.LoginEmailID;
                }
            }
            return dto;
        }
        //End To Get Parent Details

        public string GetParentLoginDetailsByParentCode(string parentCode)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var parent = dbContext.Parents.Where(s => s.ParentCode == parentCode)
                    .Include(x => x.Login)
                    .AsNoTracking()
                    .FirstOrDefault();
                //var loginDet = parent != null ? dbContext.Logins.FirstOrDefault(l => l.LoginIID == parent.LoginID) : null;
                if (parent != null)
                {
                    return parent.Login.LoginEmailID;
                }
                else
                {
                    return null;
                }
            }
        }
        //End To Get Parent Login EmailID

        private void StudentSiblingsInfo(Student entity, StudentDTO dto)
        {
            var siblingsDetail = entity.StudentSiblingMaps;

            if (siblingsDetail != null)
            {
                foreach (StudentSiblingMap siblimap in siblingsDetail)
                {
                    dto.StudentSiblings.Add(new KeyValueDTO()
                    {
                        Key = siblimap.SiblingID.ToString(),
                        Value = siblimap == null ? null : siblimap.Student1.AdmissionNumber + "-" + siblimap.Student1.FirstName + " " + siblimap.Student1.MiddleName + " " + siblimap.Student1.LastName,

                    });

                }
            }
        }

        private void OptionalSubjectsInfo(Student entity, StudentDTO dto)
        {
            var studentOptionalSubjects = entity.StudentStreamOptionalSubjectMaps.ToList();
            if (studentOptionalSubjects != null)
            {
                foreach (var optSubject in studentOptionalSubjects)
                {
                    if (optSubject.SubjectID.HasValue)
                    {
                        dto.OptionalSubjects.Add(new KeyValueDTO()
                        {
                            Key = optSubject.SubjectID.ToString(),
                            Value = optSubject?.Subject?.SubjectName,
                        });
                    }
                }
            }
        }

        private void StudentMiscInfo(Student entity, StudentDTO dto)
        {
            var groupMaps = entity.StudentGroupMaps;

            foreach (var studentgroups in groupMaps)
            {
                if (studentgroups.StudentGroup != null)
                {
                    dto.AdditionalInfo.StudentGroups.Add(new KeyValueDTO()
                    {
                        Value = studentgroups.StudentGroup.GroupName,
                        Key = studentgroups.StudentGroup.StudentGroupID.ToString()
                    });
                }
            }
        }

        private void StudentStaffMapInfo(Student entity, StudentDTO dto)
        {
            var studentStaffMap = entity.StudentStaffMaps.ToList();
            if (studentStaffMap != null)
            {
                foreach (var staffMap in studentStaffMap)
                {
                    dto.StudentStaffMaps.Add(new KeyValueDTO()
                    {
                        Key = staffMap.StaffID.ToString(),
                        Value = staffMap == null ? null : staffMap.Employee.FirstName + " " + staffMap.Employee.MiddleName + " " + staffMap.Employee.LastName,

                    });

                }
            }
        }

        private void GuardianInfo(Student entity, StudentDTO dto)
        {
            var parentMap = entity.Parent;

            if (parentMap != null)
            {
                //dto.Guardian.FatherName = parentMap.FatherName;
                dto.Guardian.ParentIID = parentMap.ParentIID;
                dto.Guardian.ParentCode = parentMap.ParentCode;
                dto.Guardian.FatherFirstName = parentMap.FatherFirstName;
                dto.Guardian.FatherMiddleName = parentMap.FatherMiddleName;
                dto.Guardian.FatherLastName = parentMap.FatherLastName;
                dto.Guardian.FatherCompanyName = parentMap.FatherCompanyName;
                dto.Guardian.FatherOccupation = parentMap.FatherOccupation;
                dto.Guardian.GuardianOccupation = parentMap.GuardianOccupation;
                dto.Guardian.PhoneNumber = parentMap.PhoneNumber;
                dto.Guardian.FatherWhatsappMobileNo = parentMap.FatherWhatsappMobileNo;
                dto.Guardian.FatherMobileNumberTwo = parentMap.FatherMobileNumberTwo;
                dto.Guardian.FatherEmailID = parentMap.FatherEmailID;
                dto.Guardian.FatherProfile = parentMap.FatherProfile;
                dto.Guardian.FatherCountryID = parentMap.FatherCountryID;
                dto.Guardian.FatherCountry = parentMap.FatherCountryID.HasValue ? parentMap.FatherCountryID.ToString() : null;
                //dto.Guardian.FatherCountry = parentMap.Country1.CountryName;
                dto.Guardian.GuardianRelation = parentMap.GuardianRelation;
                dto.Guardian.CanYouVolunteerToHelpOneID = parentMap.CanYouVolunteerToHelpOneID;

                dto.Guardian.FatherPassportNumber = parentMap.FatherPassportNumber;
                dto.Guardian.FatherPassportCountryofIssueID = parentMap.FatherPassportCountryofIssueID;
                dto.Guardian.FatherPassportCountryofIssue = parentMap.FatherPassportCountryofIssueID.HasValue ? parentMap.FatherPassportCountryofIssueID.ToString() : null;
                //dto.Guardian.FatherPassportCountryofIssue = parentMap.Country2.CountryName;
                dto.Guardian.FatherPassportNoIssueDate = parentMap.FatherPassportNoIssueDate;
                dto.Guardian.FatherPassportNoExpiryDate = parentMap.FatherPassportNoExpiryDate;

                dto.Guardian.FatherNationalID = parentMap.FatherNationalID;
                dto.Guardian.FatherNationalDNoIssueDate = parentMap.FatherNationalDNoIssueDate;
                dto.Guardian.FatherNationalDNoExpiryDate = parentMap.FatherNationalDNoExpiryDate;

                //dto.Guardian.GuardianAddress = parentMap.GuardianAddress;
                dto.Guardian.GuardianName = parentMap.GuardianName;
                dto.Guardian.GuardianPhoto = parentMap.GuardianPhoto;


                //dto.Guardian.MotherName = parentMap.MotherName;
                dto.Guardian.MotherFirstName = parentMap.MotherFirstName;
                dto.Guardian.MotherMiddleName = parentMap.MotherMiddleName;
                dto.Guardian.MotherLastName = parentMap.MotherLastName;
                dto.Guardian.MotherCompanyName = parentMap.MotherCompanyName;
                dto.Guardian.MotherOccupation = parentMap.MotherOccupation;
                dto.Guardian.MotherPhone = parentMap.MotherPhone;
                dto.Guardian.MotherWhatsappMobileNo = parentMap.MotherWhatsappMobileNo;
                dto.Guardian.MotherEmailID = parentMap.MotherEmailID;
                dto.Guardian.CanYouVolunteerToHelpTwoID = parentMap.CanYouVolunteerToHelpTwoID;
                dto.Guardian.MotherCountryID = parentMap.MotherCountryID;
                dto.Guardian.MotherCountry = parentMap.MotherCountryID.HasValue ? parentMap.MotherCountryID.ToString() : null;
                dto.Guardian.MotherPofile = parentMap.MotherPofile;


                dto.Guardian.MotherPassportNumber = parentMap.MotherPassportNumber;
                dto.Guardian.MotherPassportCountryofIssueID = parentMap.MotherPassportCountryofIssueID;
                dto.Guardian.MotherPassportCountryofIssue = parentMap.MotherPassportCountryofIssueID.HasValue ? parentMap.MotherPassportCountryofIssueID.ToString() : null;
                //dto.Guardian.MotherPassportCountryofIssue = parentMap.Country4.CountryName;
                dto.Guardian.MotherPassportNoIssueDate = parentMap.MotherPassportNoIssueDate;
                dto.Guardian.MotherPassportNoExpiryDate = parentMap.MotherPassportNoExpiryDate;


                dto.Guardian.MotherNationalID = parentMap.MotherNationalID;
                dto.Guardian.MotherNationalDNoIssueDate = parentMap.MotherNationalDNoIssueDate;
                dto.Guardian.MotherNationalDNoExpiryDate = parentMap.MotherNationalDNoExpiryDate;

                dto.Guardian.ParentIID = parentMap.ParentIID;
                dto.Guardian.LoginID = parentMap.LoginID;

                //GuardianDetails
                dto.Guardian.GuardianFirstName = parentMap.GuardianFirstName;
                dto.Guardian.GuardianMiddleName = parentMap.GuardianMiddleName;
                dto.Guardian.GuardianLastName = parentMap.GuardianLastName;
                dto.Guardian.GuardianTypeID = parentMap.GuardianTypeID;
                dto.Guardian.GuardianOccupation = parentMap.GuardianOccupation;
                dto.Guardian.GuardianCompanyName = parentMap.GuardianCompanyName;
                dto.Guardian.GuardianPhone = parentMap.GuardianPhone;
                dto.Guardian.GuardianWhatsappMobileNo = parentMap.GuardianWhatsappMobileNo;
                dto.Guardian.GaurdianEmail = parentMap.GaurdianEmail;
                dto.Guardian.GuardianNationalityID = parentMap.GuardianNationalityID;
                dto.Guardian.GuardianNationality = parentMap.GuardianNationalityID.HasValue ? parentMap.GuardianNationalityID.ToString() : null;
                dto.Guardian.GuardianPassportNumber = parentMap.GuardianPassportNumber;
                dto.Guardian.GuardianCountryofIssueID = parentMap.GuardianCountryofIssueID;
                dto.Guardian.GuardianCountryofIssue = parentMap.GuardianCountryofIssueID.HasValue ? parentMap.GuardianCountryofIssueID.ToString() : null;
                dto.Guardian.GuardianPassportNoIssueDate = parentMap.GuardianPassportNoIssueDate;
                dto.Guardian.GuardianPassportNoExpiryDate = parentMap.GuardianPassportNoExpiryDate;
                dto.Guardian.GuardianNationalID = parentMap.GuardianNationalID;
                dto.Guardian.GuardianNationalIDNoIssueDate = parentMap.GuardianNationalIDNoIssueDate;
                dto.Guardian.GuardianNationalIDNoExpiryDate = parentMap.GuardianNationalIDNoExpiryDate;

                dto.Guardian.BuildingNo = parentMap.BuildingNo;
                dto.Guardian.FlatNo = parentMap.FlatNo;
                dto.Guardian.StreetNo = parentMap.StreetNo;
                dto.Guardian.StreetName = parentMap.StreetName;
                dto.Guardian.LocationNo = parentMap.LocationNo;
                dto.Guardian.LocationName = parentMap.LocationName;
                dto.Guardian.ZipNo = parentMap.ZipNo;
                dto.Guardian.PostBoxNo = parentMap.PostBoxNo;
                dto.Guardian.City = parentMap.City;
                //dto.Guardian.Country = parentMap.CountryID.HasValue ? parentMap.CountryID.Value.ToString() : null;
                dto.Guardian.CountryID = parentMap.CountryID;
            }
        }

        private void StudentPassportInfo(Student entity, StudentDTO dto)
        {
            var passportMap = entity.StudentPassportDetails.FirstOrDefault();

            if (passportMap != null)
            {
                dto.StudentPassportDetails.StudentPassportDetailsIID = passportMap.StudentPassportDetailsIID;
                dto.StudentPassportDetails.StudentID = passportMap.StudentID;
                dto.StudentPassportDetails.NationalityID = passportMap.NationalityID;
                dto.StudentPassportDetails.National = passportMap.NationalityID.HasValue ? new KeyValueDTO()
                {
                    Key = passportMap.NationalityID.ToString(),
                    Value = passportMap.Nationality.NationalityName
                } : new KeyValueDTO();
                dto.StudentPassportDetails.PassportNo = passportMap.PassportNo;
                dto.StudentPassportDetails.AdhaarCardNo = passportMap.AdhaarCardNo;
                dto.StudentPassportDetails.CountryofIssueID = passportMap.CountryofIssueID;
                dto.StudentPassportDetails.CountryofIssue = passportMap.CountryofIssueID.HasValue ? new KeyValueDTO()
                {
                    Key = passportMap.CountryofIssueID.ToString(),
                    Value = passportMap.Country.CountryName
                } : new KeyValueDTO();
                dto.StudentPassportDetails.CountryofBirthID = passportMap.CountryofBirthID;
                dto.StudentPassportDetails.CountryofBirth = passportMap.CountryofBirthID.HasValue ? new KeyValueDTO()
                {
                    Key = passportMap.CountryofBirthID.ToString(),
                    Value = passportMap.Country1.CountryName
                } : new KeyValueDTO();
                dto.StudentPassportDetails.VisaNo = passportMap.VisaNo;
                dto.StudentPassportDetails.NationalIDNo = passportMap.NationalIDNo;
                dto.StudentPassportDetails.PassportNoExpiry = passportMap.PassportNoExpiry;
                dto.StudentPassportDetails.NationalIDNoExpiry = passportMap.NationalIDNoExpiry;
                dto.StudentPassportDetails.VisaExpiry = passportMap.VisaExpiry;
            }
        }

        private void StudentAddtionalInfo(Student entity, StudentDTO dto)
        {
            if (entity != null)
            {
                dto.AdditionalInfo.PermenentBuildingNo = entity.PermenentBuildingNo;
                dto.AdditionalInfo.PermenentFlatNo = entity.PermenentFlatNo;
                dto.AdditionalInfo.PermenentStreetNo = entity.PermenentStreetNo;
                dto.AdditionalInfo.PermenentStreetName = entity.PermenentStreetName;
                dto.AdditionalInfo.PermenentLocationNo = entity.PermenentLocationNo;
                dto.AdditionalInfo.PermenentLocationName = entity.PermenentLocationName;
                dto.AdditionalInfo.PermenentZipNo = entity.PermenentZipNo;
                dto.AdditionalInfo.PermenentPostBoxNo = entity.PermenentPostBoxNo;
                dto.AdditionalInfo.PermenentCity = entity.PermenentCity;
                dto.AdditionalInfo.PermenentCountryID = entity.PermenentCountryID;
                dto.AdditionalInfo.IsCurrentAddresIsGuardian = entity.IsAddressIsCurrentAddress;
                dto.AdditionalInfo.IsPermenentAddresIsCurrent = entity.IsAddressIsPermenentAddress;
            }
        }

        private void StudentLoginInfo(Student entity, StudentDTO dto)
        {
            var login = entity.Login;
            if (login != null)
            {
                var roleMap = entity.Login.LoginRoleMaps?.FirstOrDefault();

                dto.Login.LoginIID = login.LoginIID;
                dto.Login.LoginUserID = login.LoginUserID;
                dto.Login.LoginEmailID = login.LoginEmailID;
                dto.Login.LastLoginDate = login.LastLoginDate;
                dto.Login.Password = login.Password;
                dto.Login.PasswordHint = login.PasswordHint;
                dto.Login.StatusID = login.StatusID.IsNotNull() ? (Eduegate.Services.Contracts.School.Common.LoginUserStatus)login.StatusID : 0;
                dto.Login.LastLoginDate = login.LastLoginDate;

                if (roleMap != null)
                {
                    dto.Login.LoginRoleMaps.LoginRoleMapIID = roleMap.LoginRoleMapIID;
                    dto.Login.LoginRoleMaps.LoginID = roleMap.LoginID;
                    dto.Login.LoginRoleMaps.RoleID = roleMap.RoleID;
                }
            }
        }

        private void StudenParentLoginInfo(Student entity, StudentDTO dto)
        {
            var LogMap = entity.Parent?.Login;
            var rolemap = entity.Parent?.Login?.LoginRoleMaps?.FirstOrDefault();

            if (LogMap != null)
            {
                dto.ParentLogin.LoginIID = LogMap.LoginIID;
                dto.ParentLogin.LoginUserID = LogMap.LoginUserID;
                dto.ParentLogin.LoginEmailID = LogMap.LoginEmailID;
                dto.ParentLogin.LastLoginDate = LogMap.LastLoginDate;
                dto.ParentLogin.Password = LogMap.Password;
                dto.ParentLogin.PasswordSalt = LogMap.PasswordSalt;
                dto.ParentLogin.PasswordHint = LogMap.PasswordHint;
                dto.ParentLogin.StatusID = LogMap.StatusID.IsNotNull() ? (Eduegate.Services.Contracts.School.Common.LoginUserStatus)LogMap.StatusID : 0;
                dto.ParentLogin.LastLoginDate = LogMap.LastLoginDate;
            }
            if (rolemap != null)
            {
                dto.ParentLogin.LoginRoleMaps.LoginRoleMapIID = rolemap.LoginRoleMapIID;
                dto.ParentLogin.LoginRoleMaps.LoginID = rolemap.LoginID;
                dto.ParentLogin.LoginRoleMaps.RoleID = rolemap.RoleID;
            }
        }

        private StudentDTO ToDTO(Student entity)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var status = dbContext.StudentStatuses.Where(a => a.StudentStatusID == entity.Status).AsNoTracking().FirstOrDefault();
                string streamClass = "11,12";
                string[] streamClasses = streamClass.Split(',');

                var dto = new StudentDTO()
                {
                    StudentIID = entity.StudentIID,
                    AdmissionNumber = entity.AdmissionNumber,
                    RollNumber = entity.RollNumber,
                    ApplicationID = entity.ApplicationID.HasValue ? entity.ApplicationID : null,
                    ClassID = entity.ClassID,
                    ClassName = entity.Class != null ? entity.Class.ClassDescription : null,
                    SectionID = entity.SectionID,
                    SectionName = entity.Section != null ? entity.Section.SectionName : null,
                    GradID = entity.GradeID,
                    GradeName = entity.Grade != null ? entity.Grade.GradeName : null,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    GenderID = entity.GenderID,
                    StreamID = entity.StreamID,
                    StreamName = entity.Stream != null ? entity.Stream.Description : null,
                    GenderName = entity.Gender != null ? entity.Gender.Description : null,
                    DateOfBirth = entity.DateOfBirth,
                    InactiveDate = entity.InactiveDate,
                    CategoryID = entity.StudentCategoryID,
                    CategoryName = entity.StudentCategory != null ? entity.StudentCategory.Description : null,
                    CastID = entity.CastID,
                    CastName = entity.Cast != null ? entity.Cast.CastDescription : null,
                    RelegionID = entity.RelegionID,
                    RelegionName = entity.Relegion != null ? entity.Relegion.RelegionName : null,
                    MobileNumber = entity.MobileNumber,
                    EmailID = entity.EmailID,
                    AdmissionDate = entity.AdmissionDate,
                    StudentProfile = entity.StudentProfile,
                    BloodGroupID = entity.BloodGroupID,
                    StudentHouseID = entity.StudentHouseID,
                    // StudentHouse = entity.StudentHouse != null ? entity.StudentHouse.Description : null,
                    Height = entity.Height,
                    Weight = entity.Weight,
                    AsOnDate = entity.AsOnDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolAcademicyearID = entity.SchoolAcademicyearID,
                    SchoolAcademicYearName = entity.AcademicYear1 != null ? entity.AcademicYear1.Description + ' ' + '(' + entity.AcademicYear1.AcademicYearCode + ')' : null,
                    SchoolID = entity.SchoolID,
                    SchoolName = entity.School != null ? entity.School.SchoolName : null,
                    ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    HostelID = entity.HostelID,
                    RoomID = entity.HostelRoomID,
                    LoginID = entity.Login.IsNotNull() ? entity.Login.LoginIID : (long?)null,
                    ParentID = entity.StudentIID == 0 ? entity.Parent.ParentIID : entity.ParentID,
                    FeeStartDate = entity.FeeStartDate,
                    IsActive = entity.IsActive,
                    IsMinority = entity.IsMinority,
                    IsOnlyChildofParent = entity.IsOnlyChildofParent,
                    IsStudentStudiedBefore = entity.IsStudentStudiedBefore,
                    PrimaryContactID = entity.PrimaryContactID,
                    PrimaryContact = entity.GuardianType != null ? entity.GuardianType.TypeName : null,
                    CommunityID = entity.CommunityID,
                    Community = entity.Community != null ? entity.Community.CommunityDescription : null,
                    //SecoundLanguageID = entity.SecoundLanguageID,
                    //SecoundLanguage = entity.Language != null ? entity.Language.LanguageName : null,
                    SecoundLanguageID = entity.SecondLangID,
                    SecoundLanguage = entity.SecondLangID != null ? entity.SecondLangID.ToString() : null,
                    //ThridLanguageID = entity.ThridLanguageID,
                    //ThridLanguage = entity.Language1 != null ? entity.Language1.LanguageName : null,
                    ThridLanguageID = entity.ThirdLangID,
                    ThridLanguage = entity.ThirdLangID != null ? entity.ThirdLangID.ToString() : null,
                    SubjectMapID = entity.SubjectMapID,
                    SubjectMapString = entity.Subject2 != null ? entity.Subject2.SubjectName : null,
                    PreviousSchoolName = entity.PreviousSchoolName,
                    PreviousSchoolAcademicYear = entity.PreviousSchoolAcademicYear,
                    PreviousSchoolAddress = entity.PreviousSchoolAddress,
                    PreviousSchoolClassCompletedID = entity.PreviousSchoolClassCompletedID,
                    Status = entity.Status,
                    StatusString = entity.Status != null ? status.Description : null,
                    PreviousSchoolClassCompleted = entity.PreviousSchoolClassCompletedID.HasValue ? new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                    {
                        Key = entity.PreviousSchoolClassCompletedID.ToString(),
                        Value = entity.Class1.ClassDescription
                    } : null,
                    AcademicYear = entity.AcademicYear.Description + "(" + entity.AcademicYear.AcademicYearCode + ")",
                    PreviousSchoolSyllabusID = entity.PreviousSchoolSyllabusID,
                    PreviousSchoolSyllabus = entity.PreviousSchoolSyllabusID.HasValue ? entity.PreviousSchoolSyllabusID.ToString() : null,
                    //onStreams = streamClasses.Any(c => c == entity.Class.ClassDescription) ? false : true,
            };

                StudentSiblingsInfo(entity, dto);
                StudentMiscInfo(entity, dto);
                GuardianInfo(entity, dto);
                GetStudentDocuments(entity, dto);
                StudentPassportInfo(entity, dto);
                StudentLoginInfo(entity, dto);
                if (dto.ParentID.HasValue)
                {
                    StudenParentLoginInfo(entity, dto);
                }
                StudentAddtionalInfo(entity, dto);
                OptionalSubjectsInfo(entity, dto);
                StudentStaffMapInfo(entity, dto);

                foreach (var item in streamClasses)
                {
                    if (entity.Class.ClassDescription.Contains(item))
                    {
                        dto.onStreams = false;
                    }
                };


                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentDTO;
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            Entity.Models.Settings.Sequence sequenc = new Entity.Models.Settings.Sequence();

            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            String emailDetails = "";
            String emailSub = "";


            
            if (toDto.ClassID == 0 || toDto.ClassID == null)
            {
                throw new Exception("Please Select Class!");
            }

            if (toDto.StreamID != null && toDto.OptionalSubjects.Count >= 3)
            {
                throw new Exception("Select 2 optional subjects only !");
            }
            else if (toDto.StreamID != null && toDto.OptionalSubjects.Count < 1)
            {
                throw new Exception("Please select atleast 1 optional subject !");
            }


            var errorMessage = string.Empty;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            if (toDto.StudentSiblings.Count > 0)
            {
                foreach (var sibli in toDto.StudentSiblings)
                {
                    var sibid = int.Parse(sibli.Key);
                    SiblingParentInfo(sibid, toDto);
                }
            }

            if (string.IsNullOrEmpty(toDto.ParentLogin.LoginUserID) || string.IsNullOrEmpty(toDto.ParentLogin.LoginEmailID))
            {
                throw new Exception("Please fill Parent Login details!");
            }

            if (toDto.ClassID != null)
            {
                toDto.ClassName = ClassDetail(toDto.ClassID).ClassName;
            }
            if (toDto.SectionID != null)
            {
                toDto.SectionName = SectionDetail(toDto.SectionID).SectionName;
            }


            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.SchoolAcademicyearID.HasValue)
                {
                    var getData = dbContext.AcademicYears.Where(X => X.AcademicYearID == toDto.SchoolAcademicyearID).AsNoTracking().FirstOrDefault();
                    if (toDto.FeeStartDate.HasValue && toDto.StudentIID == 0)
                    {
                        if (toDto.FeeStartDate < getData.StartDate || toDto.FeeStartDate > getData.EndDate)
                        {
                            throw new Exception("Fee Start Date is not in between the Academic Start and End Dates Please Check!");
                        }
                    }

                    var academicData = dbContext.AcademicYears.Where(X => X.AcademicYearID == _context.AcademicYearID).AsNoTracking().FirstOrDefault();
                    if (int.Parse(getData.AcademicYearCode) > int.Parse(academicData.AcademicYearCode))
                    {
                        toDto.AcademicYearID = toDto.SchoolAcademicyearID;
                    }
                    else
                    {
                        toDto.AcademicYearID = _context.AcademicYearID;
                    }
                }
                else
                {
                    toDto.AcademicYearID = _context.AcademicYearID;
                }

                if (toDto.StudentIID == 0)
                {
                    try
                    {
                        sequence = mutualRepository.GetNextSequence("AdmissionNumber", null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please generate sequence with 'AdmissionNumber'");
                    }
                }

                if (toDto.Guardian.ParentIID == 0 && toDto.Guardian.ParentCode == null || toDto.ParentID == 0 && toDto.Guardian.ParentCode == null)
                {
                    try
                    {
                        sequenc = mutualRepository.GetNextSequence("ParentCode", null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please generate sequence with 'ParentCode'");
                    }
                }

                //convert the dto to entity and pass to the repository.
                var entity = new Student()
                {
                    StudentIID = toDto.StudentIID,
                    AdmissionNumber = toDto.StudentIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.AdmissionNumber,
                    RollNumber = toDto.RollNumber,
                    ApplicationID = toDto.ApplicationID,
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    GradeID = toDto.GradID,
                    FirstName = toDto.FirstName,
                    MiddleName = toDto.MiddleName,
                    LastName = toDto.LastName,
                    GenderID = toDto.GenderID,
                    DateOfBirth = toDto.DateOfBirth,
                    InactiveDate = toDto.IsActive == false ? toDto.InactiveDate : null,
                    StudentCategoryID = toDto.CategoryID,
                    CastID = toDto.CastID,
                    RelegionID = toDto.RelegionID,
                    MobileNumber = toDto.MobileNumber,
                    EmailID = toDto.EmailID,
                    AdmissionDate = toDto.AdmissionDate,
                    StudentProfile = toDto.StudentProfile,
                    BloodGroupID = toDto.BloodGroupID,
                    StudentHouseID = toDto.StudentHouseID,
                    Height = toDto.Height,
                    StreamID = toDto.StreamID,
                    Weight = toDto.Weight,
                    AsOnDate = toDto.AsOnDate,
                    HostelID = toDto.HostelID,
                    HostelRoomID = toDto.RoomID,
                    FeeStartDate = toDto.FeeStartDate,
                    IsActive = toDto.IsActive,
                    ParentID = toDto.ParentID,
                    LoginID = toDto.LoginID,
                    PermenentBuildingNo = toDto.AdditionalInfo.PermenentBuildingNo,
                    PermenentFlatNo = toDto.AdditionalInfo.PermenentFlatNo,
                    PermenentStreetNo = toDto.AdditionalInfo.PermenentStreetNo,
                    PermenentStreetName = toDto.AdditionalInfo.PermenentStreetName,
                    PermenentLocationNo = toDto.AdditionalInfo.PermenentLocationNo,
                    PermenentLocationName = toDto.AdditionalInfo.PermenentLocationName,
                    PermenentZipNo = toDto.AdditionalInfo.PermenentZipNo,
                    PermenentPostBoxNo = toDto.AdditionalInfo.PermenentPostBoxNo,
                    PermenentCity = toDto.AdditionalInfo.PermenentCity,
                    PermenentCountryID = toDto.AdditionalInfo.PermenentCountryID,
                    IsAddressIsCurrentAddress = toDto.AdditionalInfo.IsCurrentAddresIsGuardian,
                    IsAddressIsPermenentAddress = toDto.AdditionalInfo.IsPermenentAddresIsCurrent,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID,
                    SchoolAcademicyearID = toDto.SchoolAcademicyearID,
                    IsMinority = toDto.IsMinority,
                    IsOnlyChildofParent = toDto.IsOnlyChildofParent,
                    IsStudentStudiedBefore = toDto.IsStudentStudiedBefore,
                    PrimaryContactID = toDto.PrimaryContactID,
                    CommunityID = toDto.CommunityID,
                    SecondLangID = toDto.SecoundLanguageID,
                    ThirdLangID = toDto.ThridLanguageID,
                    SubjectMapID = toDto.SubjectMapID,
                    PreviousSchoolName = toDto.PreviousSchoolName,
                    PreviousSchoolAcademicYear = toDto.PreviousSchoolAcademicYear,
                    PreviousSchoolAddress = toDto.PreviousSchoolAddress,
                    PreviousSchoolClassCompletedID = toDto.PreviousSchoolClassCompletedID,
                    PreviousSchoolSyllabusID = toDto.PreviousSchoolSyllabusID,
                    Status = toDto.Status,

                    CreatedBy = toDto.StudentIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StudentIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StudentIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StudentIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };
                dbContext.Students.Add(entity);

                entity.StudentPassportDetails = new List<StudentPassportDetail>();
                if (toDto.StudentPassportDetails != null)
                {
                    entity.StudentPassportDetails.Add(new StudentPassportDetail()
                    {
                        StudentPassportDetailsIID = toDto.StudentPassportDetails.StudentPassportDetailsIID,
                        //StudentID = toDto.StudentPassportDetails.StudentID,
                        StudentID = toDto.StudentIID,
                        NationalityID = toDto.StudentPassportDetails.NationalityID,
                        PassportNo = toDto.StudentPassportDetails.PassportNo,
                        AdhaarCardNo = toDto.StudentPassportDetails.AdhaarCardNo,
                        CountryofIssueID = toDto.StudentPassportDetails.CountryofIssueID,
                        PassportNoExpiry = toDto.StudentPassportDetails.PassportNoExpiry,
                        CountryofBirthID = toDto.StudentPassportDetails.CountryofBirthID,
                        VisaNo = toDto.StudentPassportDetails.VisaNo,
                        VisaExpiry = toDto.StudentPassportDetails.VisaExpiry,
                        NationalIDNo = toDto.StudentPassportDetails.NationalIDNo,
                        NationalIDNoExpiry = toDto.StudentPassportDetails.NationalIDNoExpiry,
                    });
                }

                entity.StudentSiblingMaps = new List<StudentSiblingMap>();

                if (toDto.StudentSiblings.Count > 0)
                {
                    foreach (KeyValueDTO sibli in toDto.StudentSiblings)
                    {
                        entity.StudentSiblingMaps.Add(new StudentSiblingMap()
                        {
                            StudentID = toDto.StudentIID,
                            SiblingID = long.Parse(sibli.Key),
                        });
                    }
                }

                entity.StudentStaffMaps = new List<StudentStaffMap>();

                if (toDto.StudentStaffMaps.Count > 0)
                {
                    foreach (KeyValueDTO staff in toDto.StudentStaffMaps)
                    {
                        entity.StudentStaffMaps.Add(new StudentStaffMap()
                        {
                            StudentID = toDto.StudentIID,
                            StaffID = long.Parse(staff.Key),
                        });
                    }
                }

                entity.StudentGroupMaps = new List<StudentGroupMap>();
                if (toDto.AdditionalInfo?.StudentGroups.Count > 0)
                {
                    foreach (KeyValueDTO keyval in toDto.AdditionalInfo.StudentGroups)
                    {
                        entity.StudentGroupMaps.Add(new StudentGroupMap()
                        {
                            StudentGroupID = int.Parse(keyval.Key),
                            StudentID = toDto.StudentIID,
                        });
                    }
                }

                if (toDto.OptionalSubjects.Count > 0)
                {
                    foreach (KeyValueDTO subj in toDto.OptionalSubjects)
                    {
                        entity.StudentStreamOptionalSubjectMaps.Add(new StudentStreamOptionalSubjectMap()
                        {
                            StudentID = toDto.StudentIID,
                            SubjectID = int.Parse(subj.Key),
                            StreamID = toDto.StreamID,
                        });
                    }
                }

                if (toDto.ParentID != 0 || toDto.ParentID != null)
                {
                    entity.Parent = new Parent()
                    {
                        ParentCode = toDto.Guardian.ParentIID == 0 ? sequenc.Prefix + sequence.LastSequence : toDto.Guardian.ParentCode,
                        FatherName = toDto.Guardian.FatherName,
                        MotherOccupation = toDto.Guardian.MotherOccupation,
                        FatherOccupation = toDto.Guardian.FatherOccupation,
                        FatherProfile = toDto.Guardian.FatherProfile,
                        FatherEmailID = toDto.Guardian.FatherEmailID,
                        GuardianAddress = toDto.Guardian.GuardianAddress,
                        GuardianName = toDto.Guardian.GuardianName,
                        GuardianPhoto = toDto.Guardian.GuardianPhoto,
                        GuardianRelation = toDto.Guardian.GuardianRelation,
                        MotherName = toDto.Guardian.MotherName,
                        MotherPhone = toDto.Guardian.MotherPhone,
                        MotherWhatsappMobileNo = toDto.Guardian.MotherWhatsappMobileNo,
                        MotherPofile = toDto.Guardian.MotherPofile,
                        ParentIID = toDto.Guardian.ParentIID,
                        PhoneNumber = toDto.Guardian.PhoneNumber,
                        FatherWhatsappMobileNo = toDto.Guardian.FatherWhatsappMobileNo,
                        LoginID = toDto.Guardian.LoginID,
                        BuildingNo = toDto.Guardian.BuildingNo,
                        FlatNo = toDto.Guardian.FlatNo,
                        StreetNo = toDto.Guardian.StreetNo,
                        StreetName = toDto.Guardian.StreetName,
                        LocationNo = toDto.Guardian.LocationNo,
                        LocationName = toDto.Guardian.LocationName,
                        ZipNo = toDto.Guardian.ZipNo,
                        PostBoxNo = toDto.Guardian.PostBoxNo,
                        City = toDto.Guardian.City,
                        CountryID = toDto.Guardian.CountryID == null ? null : toDto.Guardian.CountryID,

                        FatherFirstName = toDto.Guardian.FatherFirstName,
                        FatherMiddleName = toDto.Guardian.FatherMiddleName,
                        FatherLastName = toDto.Guardian.FatherLastName,
                        FatherCompanyName = toDto.Guardian.FatherCompanyName,
                        FatherMobileNumberTwo = toDto.Guardian.FatherMobileNumberTwo,
                        FatherCountryID = toDto.Guardian.FatherCountryID,
                        CanYouVolunteerToHelpOneID = toDto.Guardian.CanYouVolunteerToHelpOneID,

                        FatherPassportNumber = toDto.Guardian.FatherPassportNumber,
                        FatherPassportCountryofIssueID = toDto.Guardian.FatherPassportCountryofIssueID,
                        FatherPassportNoIssueDate = toDto.Guardian.FatherPassportNoIssueDate,
                        FatherPassportNoExpiryDate = toDto.Guardian.FatherPassportNoExpiryDate,

                        FatherNationalID = toDto.Guardian.FatherNationalID,
                        FatherNationalDNoIssueDate = toDto.Guardian.FatherNationalDNoIssueDate,
                        FatherNationalDNoExpiryDate = toDto.Guardian.FatherNationalDNoExpiryDate,

                        MotherFirstName = toDto.Guardian.MotherFirstName,
                        MotherMiddleName = toDto.Guardian.MotherMiddleName,
                        MotherLastName = toDto.Guardian.MotherLastName,
                        MotherCompanyName = toDto.Guardian.MotherCompanyName,
                        MotherCountryID = toDto.Guardian.MotherCountryID,
                        MotherEmailID = toDto.Guardian.MotherEmailID,
                        CanYouVolunteerToHelpTwoID = toDto.Guardian.CanYouVolunteerToHelpTwoID,

                        MotherPassportNumber = toDto.Guardian.MotherPassportNumber,
                        MotherPassportCountryofIssueID = toDto.Guardian.MotherPassportCountryofIssueID,
                        MotherPassportNoIssueDate = toDto.Guardian.MotherPassportNoIssueDate,
                        MotherPassportNoExpiryDate = toDto.Guardian.MotherPassportNoExpiryDate,

                        MotherNationalID = toDto.Guardian.MotherNationalID,
                        MotherNationalDNoIssueDate = toDto.Guardian.MotherNationalDNoIssueDate,
                        MotherNationalDNoExpiryDate = toDto.Guardian.MotherNationalDNoExpiryDate,

                        //Guardian Details Saving
                        GuardianFirstName = toDto.Guardian.GuardianFirstName,
                        GuardianMiddleName = toDto.Guardian.GuardianMiddleName,
                        GuardianLastName = toDto.Guardian.GuardianLastName,
                        GuardianTypeID = toDto.Guardian.GuardianTypeID,
                        GuardianOccupation = toDto.Guardian.GuardianOccupation,
                        GuardianCompanyName = toDto.Guardian.GuardianCompanyName,
                        GuardianPhone = toDto.Guardian.GuardianPhone,
                        GuardianWhatsappMobileNo = toDto.Guardian.GuardianWhatsappMobileNo,
                        GaurdianEmail = toDto.Guardian.GaurdianEmail,
                        GuardianNationalityID = toDto.Guardian.GuardianNationalityID,
                        GuardianNationalID = toDto.Guardian.GuardianNationalID,
                        GuardianNationalIDNoIssueDate = toDto.Guardian.GuardianNationalIDNoIssueDate,
                        GuardianNationalIDNoExpiryDate = toDto.Guardian.GuardianNationalIDNoExpiryDate,
                        GuardianPassportNumber = toDto.Guardian.GuardianPassportNumber,
                        GuardianCountryofIssueID = toDto.Guardian.GuardianCountryofIssueID,
                        GuardianPassportNoIssueDate = toDto.Guardian.GuardianPassportNoIssueDate,
                        GuardianPassportNoExpiryDate = toDto.Guardian.GuardianPassportNoExpiryDate,
                    };
                }

                if (entity.ParentID == null || entity.ParentID == 0)
                {
                    if (toDto.StudentSiblings.Count() > 0)
                    {

                        var sib = toDto.StudentSiblings.FirstOrDefault();

                        var sibid = sib == null && string.IsNullOrEmpty(sib.Key) ? 0 : int.Parse(sib.Key);

                        var studentDet = dbContext.Students.Where(x => x.StudentIID == sibid).AsNoTracking().FirstOrDefault();

                        entity.ParentID = studentDet?.ParentID;
                    }
                }

                if (string.IsNullOrEmpty(toDto.Login.LoginUserID) || (toDto.Login.LoginIID == toDto.ParentLogin.LoginIID))
                {
                    var mailDomain = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_DOMAIN");

                    toDto.Login.LoginUserID = entity.AdmissionNumber;
                    toDto.Login.LoginEmailID = entity.AdmissionNumber.ToLower() + "." + entity.FirstName.Trim().ToLower() + mailDomain;
                    toDto.Login.LoginIID = 0;
                }

                if (toDto.Login.LoginUserID != null)
                {
                    entity.Login = new Login();

                    entity.Login.LoginIID = toDto.Login.LoginIID;
                    entity.Login.LoginUserID = toDto.Login.LoginUserID;
                    entity.Login.LoginEmailID = toDto.Login.LoginEmailID;
                    entity.Login.LastLoginDate = toDto.Login.LastLoginDate;
                    entity.Login.Password = toDto.Login.Password;
                    entity.Login.PasswordHint = toDto.Login.PasswordHint;
                    entity.Login.PasswordSalt = toDto.Login.PasswordSalt;
                    entity.Login.StatusID = toDto.Login.StatusID > 0 ? (byte)toDto.Login.StatusID : (byte?)null;
                    // manage Password Flag
                    if (toDto.Login.IsRequired)
                    {
                        entity.Login.PasswordSalt = PasswordHash.CreateHash(toDto.Login.Password);
                        //encryt the value to save in the DB as Password
                        entity.Login.Password = StringCipher.Encrypt(toDto.Login.Password, entity.Login.PasswordSalt);
                    }
                    if (toDto.StudentIID == 0)
                    {
                        entity.Login.CreatedBy = (int)_context.LoginID;
                        entity.Login.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.Login.UpdatedBy = (int)_context.LoginID;

                        entity.Login.UpdatedDate = DateTime.Now;
                    }
                }

                if (entity.Parent != null)
                {
                    if (toDto.ParentLogin.LoginUserID != null)
                    {
                        entity.Parent.Login = new Login();

                        entity.Parent.Login.LoginIID = toDto.ParentLogin.LoginIID;
                        entity.Parent.Login.UserName = toDto.ParentLogin.LoginEmailID;
                        entity.Parent.Login.LoginUserID = toDto.ParentLogin.LoginUserID;
                        entity.Parent.Login.LoginEmailID = toDto.ParentLogin.LoginEmailID;
                        entity.Parent.Login.LastLoginDate = toDto.ParentLogin.LastLoginDate;
                        entity.Parent.Login.Password = toDto.ParentLogin.Password;
                        entity.Parent.Login.PasswordHint = toDto.ParentLogin.PasswordHint;

                        // manage Password Flag
                        if (toDto.ParentLogin.IsRequired)
                        {
                            entity.Parent.Login.PasswordSalt = PasswordHash.CreateHash(toDto.ParentLogin.Password);
                            //encryt the value to save in the DB as Password
                            entity.Parent.Login.Password =
                                    StringCipher.Encrypt(toDto.ParentLogin.Password, entity.Parent.Login.PasswordSalt);

                            entity.Parent.Login.RequirePasswordReset = true;
                        }

                        if (toDto.ParentLogin.IsRequired == false)
                        {
                            entity.Parent.Login.RequirePasswordReset = false;
                            entity.Parent.Login.PasswordSalt = toDto.ParentLogin.PasswordSalt;
                        }
                        if (toDto.ParentLogin.LoginIID == 0)
                        {
                            entity.Parent.Login.CreatedBy = (int)_context.LoginID;
                            entity.Parent.Login.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            entity.Parent.Login.UpdatedBy = (int)_context.LoginID;

                            entity.Parent.Login.UpdatedDate = DateTime.Now;
                        }

                        if (toDto.ParentID != 0)
                        {
                            var students = dbContext.Students.Where(a => a.ParentID == entity.Parent.ParentIID && a.IsActive == true && a.StudentIID != entity.StudentIID).AsNoTracking().ToList();

                            if (students.Count() == 0)
                            {
                                entity.Parent.Login.StatusID = toDto.IsActive == false ? (byte?)0 : (toDto.ParentLogin.StatusID > 0 ?
                               (byte)toDto.ParentLogin.StatusID : (byte?)0);
                            }
                            else
                                entity.Parent.Login.StatusID = toDto.ParentLogin.StatusID > 0 ?
                               (byte)toDto.ParentLogin.StatusID : (byte?)0;

                            var loginRoleMap = dbContext.LoginRoleMaps.Where(a => a.LoginID == toDto.ParentLogin.LoginIID)
                                .OrderByDescending(o => o.LoginRoleMapIID)
                                .AsNoTracking().FirstOrDefault();

                            entity.Parent.Login.LoginRoleMaps.Add(new LoginRoleMap()
                            {
                                LoginRoleMapIID = loginRoleMap == null ? 0 : loginRoleMap.LoginRoleMapIID,
                                LoginID = toDto.ParentLogin.LoginIID,
                                RoleID = 2,
                                CreatedBy = toDto.ParentLogin.LoginRoleMaps.LoginRoleMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = toDto.ParentLogin.LoginRoleMaps.LoginRoleMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = toDto.ParentLogin.LoginRoleMaps.LoginRoleMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                UpdatedDate = toDto.ParentLogin.LoginRoleMaps.LoginRoleMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            });
                        }
                    }
                }

                entity.StudentApplicationDocumentMaps = new List<StudentApplicationDocumentMap>();
                if (toDto.StudentDocUploads != null)
                {
                    entity.StudentApplicationDocumentMaps.Add(new StudentApplicationDocumentMap()
                    {
                        ApplicationDocumentIID = toDto.StudentDocUploads.ApplicationDocumentIID,
                        ApplicationID = toDto.StudentDocUploads.ApplicationID,
                        BirthCertificateReferenceID = toDto.StudentDocUploads.BirthCertificateReferenceID,
                        BirthCertificateAttach = toDto.StudentDocUploads.BirthCertificateAttach,
                        StudentPassportReferenceID = toDto.StudentDocUploads.StudentPassportReferenceID,
                        StudentPassportAttach = toDto.StudentDocUploads.StudentPassportAttach,
                        TCReferenceID = toDto.StudentDocUploads.TCReferenceID,
                        TCAttach = toDto.StudentDocUploads.TCAttach,
                        FatherQIDReferenceID = toDto.StudentDocUploads.FatherQIDReferenceID,
                        FatherQIDAttach = toDto.StudentDocUploads.FatherQIDAttach,
                        MotherQIDReferenceID = toDto.StudentDocUploads.MotherQIDReferenceID,
                        MotherQIDAttach = toDto.StudentDocUploads.MotherQIDAttach,
                        StudentQIDReferenceID = toDto.StudentDocUploads.StudentQIDReferenceID,
                        StudentQIDAttach = toDto.StudentDocUploads.StudentQIDAttach,
                        CreatedBy = toDto.StudentIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.StudentIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.StudentIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = toDto.StudentIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        StudentID = toDto.StudentIID,
                    });
                }

                if (entity.Parent != null)
                {
                    //dbContext.Students.Add(entity);

                    if (entity.StudentIID == 0)
                    {

                        foreach (var studAppDoc in entity.StudentApplicationDocumentMaps)
                        {
                            if (studAppDoc.ApplicationDocumentIID == 0)
                            {
                                dbContext.Entry(studAppDoc).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(studAppDoc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }


                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();

                        toDto.StudentIID = entity.StudentIID;

                        UpdateApplicationStatus(entity.ApplicationID);
                        UpdateLeadStatus(entity.ApplicationID);
                        UpdateLoginUserID(entity.ApplicationID);

                        if (toDto.StudentIID != 0)
                        {
                            emailDetails = @"<br />

                                                                <p align='left'>Welcome to Pearl School Family!! <br />Your Candidate Admission Process is Completed.</p><br />
                        <table align='left'>
                                <tr>
                                    <th>Student Details:</th>
                                </tr>
                                <tr>
                                    <td> Admission No:</td>

                                          <td>" + entity.AdmissionNumber + @"</td>
                                   </tr>

                                   <tr>
                        				<td>Student Name :</td>

                                       <td>" + entity.FirstName + " " + entity.MiddleName + " " + entity.LastName + @"</td>  

                                      </tr>
                                   <tr>

                                          <td>Class :</td>
                                            <td>" + toDto.ClassName + @"</td>


                                         </tr>
                          <tr>

                                          <td>Section:</td>
                                            <td>" + toDto.SectionName + @"</td>                                 
                                         </tr>

                                         <tr>

                                             <td> Admission Date :</td>

                                                <td>" + (entity.AdmissionDate == null ? "" : entity.AdmissionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture)) + @"</td>
                                        </tr>

                                           </table> <br /> <br /><br /><br /> <br /><br /><br /> <br />
                        				   Now You can access the Parent Portal, please click on the link below: <br />
                        				   Link : <a href='https://parent.pearlschool.org/Account/LogIn'>Visit Parent Portal!</a> <br />
                                                please use your registered UserID/EmailID and password for Login. <br />
                             Regards,<br />
                             Pearl School <br />
<a href='https://parent.pearlschool.org/Documents/WelcomeMessagetoNewAdmissions.pdf' > <img src='https://parent.pearlschool.org/Images/filetypes/pdf.png' width='40' height='40' border='0' style='padding-top:10px;' / ><br/>click here to view the school policy</a >
                             <br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                            emailSub = "Automatic reply: Admission Details";
                        }
                    }
                    else
                    {
                        using (dbEduegateSchoolContext dbContext1 = new dbEduegateSchoolContext())
                        {
                            //remove old stream subject maps
                            var streamSubMaps = toDto.StudentIID != 0 ? dbContext1.StudentStreamOptionalSubjectMaps.Where(X => X.StudentID == toDto.StudentIID).AsNoTracking().ToList() : null;
                            if (streamSubMaps != null)
                            {
                                dbContext1.StudentStreamOptionalSubjectMaps.RemoveRange(streamSubMaps);
                            }

                            //remove old sibling maps
                            var oldSiblingMaps = dbContext1.StudentSiblingMaps.Where(x => x.StudentID == toDto.StudentIID).AsNoTracking().ToList();
                            if (oldSiblingMaps != null || oldSiblingMaps.Count > 0)
                            {
                                dbContext1.StudentSiblingMaps.RemoveRange(oldSiblingMaps);
                            }

                            //remove old staff maps
                            var oldStaffMaps = dbContext1.StudentStaffMaps.Where(x => x.StudentID == toDto.StudentIID).AsNoTracking().ToList();
                            if (oldStaffMaps != null || oldStaffMaps.Count > 0)
                            {
                                foreach (var deleteItem in oldStaffMaps)
                                {
                                    dbContext1.StudentStaffMaps.Remove(deleteItem);

                                    dbContext1.Entry(deleteItem).State = EntityState.Deleted;
                                }
                            }

                            //delete all mapping and recreate
                            var oldStudentGroupMaps = dbContext1.StudentGroupMaps.Where(x => x.StudentID == toDto.StudentIID).ToList();
                            if (oldStudentGroupMaps != null || oldStudentGroupMaps.Count > 0)
                            {
                                dbContext1.StudentGroupMaps.RemoveRange(oldStudentGroupMaps);
                            }

                            dbContext1.SaveChanges();
                        }

                        foreach (var miscDetail in entity.StudentMiscDetails)
                        {
                            if (miscDetail.StudentMiscDetailsIID == 0)
                            {
                                dbContext.Entry(miscDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(miscDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        foreach (var passPortDetail in entity.StudentPassportDetails)
                        {
                            if (passPortDetail.StudentPassportDetailsIID == 0)
                            {
                                dbContext.Entry(passPortDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(passPortDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        foreach (var group in entity.StudentGroupMaps)
                        {
                            if (group.StudentGroupMapIID == 0)
                            {
                                dbContext.Entry(group).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(group).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        foreach (var studAppDoc in entity.StudentApplicationDocumentMaps)
                        {
                            if (studAppDoc.ApplicationDocumentIID == 0)
                            {
                                dbContext.Entry(studAppDoc).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(studAppDoc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        if (entity.Parent.ParentIID == 0)
                        {
                            dbContext.Entry(entity.Parent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            if (entity.Parent.Login != null)
                            {
                                if (entity.Parent.Login != null && entity.Parent.Login.LoginIID == 0)
                                {
                                    dbContext.Entry(entity.Parent.Login).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    foreach (var role in entity.Parent.Login.LoginRoleMaps)
                                    {
                                        if (role.LoginRoleMapIID == 0)
                                        {
                                            dbContext.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                        else
                                        {
                                            dbContext.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        }
                                    }

                                    dbContext.Entry(entity.Parent.Login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            dbContext.Entry(entity.Parent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        if (entity.Login != null && entity.Login.LoginIID == 0)
                        {
                            dbContext.Entry(entity.Login).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity.Login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        foreach (var sibli in entity.StudentSiblingMaps)
                        {
                            var sibliMap = dbContext.StudentSiblingMaps.Where(x => x.SiblingID == sibli.SiblingID && x.StudentID == toDto.StudentIID)
                                .OrderByDescending(o => o.StudentSiblingMapIID).AsNoTracking().FirstOrDefault();

                            if (sibliMap != null)
                            {
                                sibli.StudentSiblingMapIID = sibliMap.StudentSiblingMapIID;

                                dbContext.Entry(sibli).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(sibli).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }

                            //dbContext.SaveChanges();
                        }

                        foreach (var staff in entity.StudentStaffMaps)
                        {
                            //var staffMap = dbContext.StudentStaffMaps.Where(x => x.StaffID == staff.StaffID && x.StudentID == toDto.StudentIID)
                            //    .OrderByDescending(o => o.StudentStaffMapIID).AsNoTracking().FirstOrDefault();

                            if (staff.StudentStaffMapIID == 0)
                            {
                                dbContext.Entry(staff).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(staff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //dbContext.SaveChanges();
                    }

                    dbContext.SaveChanges();

                }
                //Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(1, entity.StudentIID);

                var emaildata = new EmailNotificationDTO();
                try
                {
                    var toMailID = toDto.Guardian.GaurdianEmail != null ? toDto.Guardian.GaurdianEmail : toDto.EmailID;

                    var schoolShortName = entity?.School?.SchoolShortName?.ToLower();

                    if (string.IsNullOrEmpty(schoolShortName))
                    {
                        if (entity.SchoolID.HasValue)
                        {
                            var data = new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(entity.SchoolID.Value);

                            schoolShortName = data?.SchoolShortName?.ToLower();
                        }
                    }

                    var mailParameters = new Dictionary<string, string>()
                {
                    { "SCHOOL_SHORT_NAME", schoolShortName},
                };

                    string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toMailID, emailDetails);

                    var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                    string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                    if (emailDetails != "")
                    {
                        if (hostDet.ToLower() == "live")
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toMailID, emailSub, mailMessage, EmailTypes.StudentAdmission, mailParameters);
                        }
                        else
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.StudentAdmission, mailParameters);
                        }
                    }

                }
                catch { }

                // to save student image in physical folder
                if (!string.IsNullOrEmpty(entity.StudentProfile))
                {
                    ResizeAndSaveStudentImage(long.Parse(entity.StudentProfile), entity.StudentIID);
                }

                return GetEntity(entity.StudentIID);
            }
        }

        private void ResizeAndSaveStudentImage(long? studentProfile, long stundentID)
        {
            using (var dbContext = new dbContentContext())
            {
                var thumbnailPathSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("StudentThumbnailImagePath");
                var thumbnailWidthSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ThumbnailWidth");
                var thumbnailWidth = thumbnailWidthSetting != null ? !string.IsNullOrEmpty(thumbnailWidthSetting) ? thumbnailWidthSetting : "800" : "800";

                var content = dbContext.ContentFiles.Where(c => c.ContentFileIID == studentProfile).AsNoTracking().FirstOrDefault();

                if (content != null)
                {
                    var fileName = content.ContentFileName;
                    var contentData = content.ContentData;

                    //string serverPath = new Domain.Setting.SettingBL(null).GetSettingValue<string>("StudentThumbnailImagePath");// @"C:\Vineetha\eduegateerpv1\Presentation\Eduegate.ERP.Admin"; //ConfigurationManager.AppSettings["StudentThumnailImagePath"]// use appconfig key
                    string serverPath = thumbnailPathSetting != null ? thumbnailPathSetting : "C:\\Softop_Eduegate";
                    string dirPath = $"{serverPath}\\StudentProfile\\{stundentID}\\Thumbnail";
                    string strFilePath = dirPath + "\\" + fileName;
                    if (File.Exists(strFilePath) == false)
                    {
                        if (Directory.Exists(dirPath) == false)
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        //save byte array to image in a physical path
                        //File.WriteAllBytes(dirPath + "\\" + fileName, ResizeImage(contentData, int.Parse(ConfigurationManager.AppSettings["ThumbnailWidth"])));// Compress(content)
                        File.WriteAllBytes(dirPath + "\\" + fileName, ResizeImage(contentData, int.Parse(thumbnailWidth)));// Compress(content)
                    }
                    else
                    {
                        File.Delete(strFilePath);

                        File.WriteAllBytes(dirPath + "\\" + fileName, ResizeImage(contentData, int.Parse(thumbnailWidth)));// Compress(content)
                    }
                }
            }
        }

        private byte[] ResizeImage(byte[] data, int width)
        {
            byte[] decompressedData = Decompress(data);

            if (width == 0)
                return data;

            using (var stream = new MemoryStream(decompressedData))
            {
                using (var image = SixLabors.ImageSharp.Image.Load(stream))
                {
                    var height = (width * image.Height) / image.Width;

                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Max // You can change the mode as needed
                    }));

                    using (var thumbnailStream = new MemoryStream())
                    {
                        image.Save(thumbnailStream, new JpegEncoder());
                        return thumbnailStream.ToArray();
                    }
                }
            }
        }

        private StudentDTO ClassDetail(long? classID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new StudentDTO();

                var classDetail = dbContext.Classes.Where(a => a.ClassID == classID)
                    .OrderByDescending(o => o.ClassID)
                    .AsNoTracking().FirstOrDefault();

                dtos.ClassName = classDetail?.ClassDescription;

                return dtos;
            }
        }

        private StudentDTO SectionDetail(long? SectionID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new StudentDTO();

                var sectionDetail = dbContext.Sections.Where(a => a.SectionID == SectionID)
                    .OrderByDescending(o => o.SectionID)
                    .AsNoTracking().FirstOrDefault();

                dtos.SectionName = sectionDetail.SectionName;

                return dtos;
            }
        }

        public List<KeyValueDTO> GetClassStudents(int classId, int sectionId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classStudentList = new List<KeyValueDTO>();

                var entities = dbContext.Students
                    .Where(a => a.ClassID == classId && (sectionId == 0 || a.SectionID == sectionId) && a.IsActive == true && (a.Status == 1) && a.SchoolID == _context.SchoolID)
                    .AsNoTracking()
                    .OrderBy(z => z.AdmissionNumber)
                    .ToList();

                foreach (var classStud in entities)
                {
                    classStudentList.Add(new KeyValueDTO
                    {
                        Key = classStud.StudentIID.ToString(),
                        Value = classStud.AdmissionNumber + '-' + ' ' + classStud.FirstName + ' ' + classStud.MiddleName + ' ' + classStud.LastName
                    });
                }

                return classStudentList;
            }
        }

        public List<KeyValueDTO> GetClassStudentsAll(int academicYearID, List<int> classList, List<int> sectionList)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classStudentList = new List<KeyValueDTO>();

                var entities = dbContext.Students
                    .Where(a => (classList.Contains(0) || classList.Contains(a.ClassID ?? 0)) && (sectionList.Contains(0) || sectionList.Contains(a.SectionID ?? 0)) &&
                        a.IsActive == true && (a.Status ?? 0) != 3 && a.AcademicYearID == academicYearID)
                    .AsNoTracking()
                    .OrderBy(z => z.AdmissionNumber)
                    .ToList();

                foreach (var classstud in entities)
                {
                    classStudentList.Add(new KeyValueDTO
                    {
                        Key = classstud.StudentIID.ToString(),
                        Value = classstud.AdmissionNumber + '-' + ' ' + classstud.FirstName + ' ' + classstud.MiddleName + ' ' + classstud.LastName
                    });
                }

                return classStudentList;
            }
        }

        public List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var activeStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_ACTIVE_STATUSID");
                int? activeStatusID = int.Parse(activeStatus);

                //var currentAcademicYearID = _context.AcademicYearID;

                var studentList = (from s in dbContext.Students
                                   join a in dbContext.AcademicYears on s.AcademicYearID equals a.AcademicYearID
                                   where s.ClassID == classId && s.IsActive == true && s.SectionID == sectionId
                                   && a.AcademicYearStatusID == currentAcademicYearStatusID
                                   && s.Status == activeStatusID
                                   select new StudentDTO
                                   {
                                       StudentIID = s.StudentIID,
                                       StudentProfile = s.StudentProfile,
                                       AdmissionNumber = s.AdmissionNumber,
                                       FirstName = s.FirstName,
                                       MiddleName = s.MiddleName,
                                       LastName = s.LastName,
                                       StudentFullName = s.FirstName + " " + s.MiddleName + " " + s.LastName,
                                       AdmissionDate = s.AdmissionDate,
                                       FeeStartDate = s.FeeStartDate,
                                       ParentID = s.ParentID
                                   }).OrderBy(z => z.AdmissionNumber)
                                   .AsNoTracking().ToList();

                var studentProfileIDs = studentList.Select(x => x.StudentProfile).ToList();

                using (var contentDBContext = new dbContentContext())
                {
                    var contentFiles = contentDBContext.ContentFiles
                    .Where(cf => studentProfileIDs.Contains(cf.ContentFileIID.ToString()))
                    .AsNoTracking()
                    .Select(cf => new ContentFileDTO
                    {
                        ContentFileIID = cf.ContentFileIID,
                        ContentFileName = cf.ContentFileName
                    }).ToList();


                    if (studentList.Any())
                    {
                        studentList.All(x =>
                        {
                            x.StudentProfileName = contentFiles.Where(y => y.ContentFileIID.ToString() == x.StudentProfile).Select(w => w.ContentFileName).FirstOrDefault();
                            return true;
                        });
                    }
                }

                return studentList;
            }
        }

        public List<KeyValueDTO> GetStudentByExamClass(List<ExamClassDTO> Examdto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classStudentList = new List<KeyValueDTO>();
                var entities = new List<Student>();

                foreach (var examClassDTO in Examdto)
                {
                    entities = dbContext.Students
                        .Where(a => a.ClassID == examClassDTO.ClassID && ((examClassDTO.SectionID == 0 || examClassDTO.SectionID == null) || a.SectionID == examClassDTO.SectionID) && (a.IsActive == true))
                        .OrderBy(a => a.FirstName + ' ' + a.MiddleName + ' ' + a.LastName)
                        .AsNoTracking().ToList();

                    foreach (var entStudent in entities)
                    {
                        classStudentList.Add(new KeyValueDTO
                        {
                            Key = entStudent.StudentIID.ToString(),
                            Value = entStudent.FirstName + ' ' + entStudent.MiddleName + ' ' + entStudent.LastName
                        });
                    }
                }

                return classStudentList;
            }
        }

        public List<StudentDTO> GetStudentsSiblings(long loginID)
        {
            var studentDTO = new List<StudentDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var students = dbContext.Students.Where(s => s.Parent.LoginID == loginID && s.IsActive == true)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.BloodGroup)
                    .Include(i => i.Gender)
                    .Include(i => i.Relegion)
                    .Include(i => i.Subject)
                    .Include(i => i.Subject1)
                    .Include(i => i.GuardianType)
                    .Include(i => i.StudentPassportDetails).ThenInclude(i => i.Country1)
                    .Include(i => i.StudentPassportDetails).ThenInclude(i => i.Nationality)
                    .Include(i => i.AllergyStudentMaps).ThenInclude(i => i.Allergy)
                    .AsNoTracking()
                    .ToList();

                foreach (var stud in students)
                {
                    var passportDetails = stud.StudentPassportDetails.OrderByDescending(p => p.StudentPassportDetailsIID).FirstOrDefault();

                    var studentPassportDetailsDTO = new StudentPassportDetailDTO();
                    var additionalInfo = new AdditionalInfoDTO();

                    if (passportDetails != null)
                    {
                        studentPassportDetailsDTO = new StudentPassportDetailDTO()
                        {
                            CountryofBirthID = passportDetails.CountryofBirthID,
                            CountryofBirth = passportDetails.CountryofBirthID.HasValue ? new KeyValueDTO() { Key = passportDetails.CountryofBirthID.ToString(), Value = passportDetails.Country1?.CountryName } : new KeyValueDTO(),
                            NationalityID = passportDetails.NationalityID,
                            National = passportDetails.NationalityID.HasValue ? new KeyValueDTO() { Key = passportDetails.NationalityID.ToString(), Value = passportDetails.Nationality?.NationalityName } : new KeyValueDTO(),
                            PassportNo = passportDetails.PassportNo,
                            PassportNoExpiry = passportDetails.PassportNoExpiry,
                            PassportExpiryStringDate = passportDetails.PassportNoExpiry.HasValue ? passportDetails.PassportNoExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            VisaNo = passportDetails.VisaNo,
                            VisaExpiry = passportDetails.VisaExpiry,
                            VisaExpiryStringDate = passportDetails.VisaExpiry.HasValue ? passportDetails.VisaExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            NationalIDNo = passportDetails.NationalIDNo,
                            NationalIDNoExpiry = passportDetails.NationalIDNoExpiry,
                            NationalIDExpiryStringDate = passportDetails.NationalIDNoExpiry.HasValue ? passportDetails.NationalIDNoExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        };
                    }

                    additionalInfo = new AdditionalInfoDTO()
                    {
                        PermenentPostBoxNo = stud.PermenentPostBoxNo,
                        PermenentAddress = $"{stud.PermenentBuildingNo}, {stud.PermenentFlatNo}, {stud.PermenentStreetNo}, {stud.PermenentStreetName}, {stud.PermenentCity}, {stud.Country1?.CountryName}"
                    };

                    var allergyStudentMap = stud.AllergyStudentMaps.FirstOrDefault();

                    var allergy = new AllergyStudentDTO()
                    {
                        StudentID = allergyStudentMap?.StudentID,
                        AllergyID = allergyStudentMap?.AllergyID,
                        AllergyName = allergyStudentMap?.Allergy?.AllergyName,
                    };

                    string initials = "";
                    if (string.IsNullOrEmpty(stud.FirstName))
                    {
                        Console.WriteLine("Warning: FirstName is empty or null. Skipping.");
                    }
                    else
                    {
                        initials += stud.FirstName[0];
                    }

                    if (!string.IsNullOrEmpty(stud.MiddleName))
                    {
                        initials += stud.MiddleName[0];
                    }
                    else if (!string.IsNullOrEmpty(stud.LastName))
                    {
                        initials += stud.LastName[0];
                    }
                    studentDTO.Add(new StudentDTO()
                    {
                        StudentIID = stud.StudentIID,
                        ParentID = stud.ParentID,
                        AdmissionNumber = stud.AdmissionNumber,
                        FirstName = stud.FirstName,
                        MiddleName = stud.MiddleName,
                        LastName = stud.LastName,
                        StudentInitials = initials,
                        StudentFullName = stud.AdmissionNumber + " - " + stud.FirstName + (string.IsNullOrEmpty(stud.MiddleName) ? " " : " " + stud.MiddleName + " ") + stud.LastName,
                        ClassID = stud.ClassID,
                        SectionID = stud.SectionID,
                        SchoolID = stud.SchoolID,
                        AcademicYearID = stud.AcademicYearID,
                        ClassName = stud.ClassID.HasValue ? stud.Class?.ClassDescription : null,
                        SectionName = stud.SectionID.HasValue ? stud.Section?.SectionName : null,
                        SchoolName = stud.SchoolID.HasValue ? stud.School?.SchoolName : null,
                        AcademicYear = stud.AcademicYearID.HasValue ? stud.AcademicYear?.Description + "(" + stud.AcademicYear?.AcademicYearCode + ")" : null,
                        IsSelected = false,
                        StudentProfile = stud.StudentProfile,
                        ParentEmailID = stud?.Parent?.GaurdianEmail,
                        AdmissionDate = stud.AdmissionDate,
                        AdmissionDateString = stud.AdmissionDate.HasValue ? stud.AdmissionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        BloodGroupID = stud.BloodGroupID,
                        BloodGroupName = stud.BloodGroupID.HasValue ? stud.BloodGroup?.BloodGroupName : null,
                        GenderID = stud.GenderID,
                        GenderName = stud.GenderID.HasValue ? stud.Gender?.Description : null,
                        RelegionID = stud.RelegionID,
                        RelegionName = stud.RelegionID.HasValue ? stud.Relegion?.RelegionName : null,
                        DateOfBirth = stud.DateOfBirth,
                        DateOfBirthString = stud.DateOfBirth.HasValue ? stud.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        SecoundLanguageID = stud.SecondLangID,
                        SecoundLanguage = stud.SecondLangID.HasValue ? stud.Subject?.SubjectName : null,
                        ThridLanguageID = stud.ThirdLangID,
                        ThridLanguage = stud.ThirdLangID.HasValue ? stud.Subject1?.SubjectName : null,
                        PrimaryContactID = stud.PrimaryContactID,
                        PrimaryContact = stud.PrimaryContactID.HasValue ? stud.GuardianType?.TypeName : null,
                        StudentPassportDetails = studentPassportDetailsDTO,
                        AdditionalInfo = additionalInfo,
                        Allergy = allergy,
                    });
                }
            }

            if (studentDTO != null && studentDTO.Count > 0)
            {
                studentDTO[0].IsSelected = true;
            }

            return studentDTO;
        }

        public int GetStudentsSiblingsCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var details = dbContext.Students.Where(x => x.Parent.LoginID == loginID).AsNoTracking().ToList();

                return details.Count();
            }
        }

        public int GetStudentsCount()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var data = dbContext.Students.Where(s => s.IsActive == true).AsNoTracking().ToList();

                return data != null ? data.Count() : 0;
            }
        }

        public long GetParentID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.Parents.Where(psm => psm.LoginID == loginID)
                    .AsNoTracking()
                    .Select(psm => psm.ParentIID)
                    .FirstOrDefault();
            }
        }

        public List<StudentDTO> GetStudentDetails(long studentId)
        {
            var studentDTOList = new List<StudentDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDTO = dbContext.Students.Where(s => s.StudentIID == studentId)
                                  .Include(i => i.Gender)
                                  .Include(i => i.Section)
                                  .Include(i => i.Class)
                                  .Include(i => i.Cast)
                                  .Include(i => i.StudentCategory)
                                  .Include(i => i.Relegion)
                                  .Include(i => i.AcademicYear1)
                                  .Include(i => i.Community)
                                  .Include(i => i.BloodGroup)
                                  .Include(i => i.StudentHouse)
                                  .Include(i => i.School)
                                  .Include(i => i.Parent)
                                  .Include(i => i.StudentPassportDetails)
                                  .OrderByDescending(o => o.StudentIID)
                                  .AsNoTracking()
                                  .ToList();

                studentDTOList = studentDTO.Select(studentProfileGroup => new StudentDTO()
                {
                    StudentIID = studentProfileGroup.StudentIID,
                    StudentProfile = studentProfileGroup.StudentProfile,
                    FirstName = studentProfileGroup.FirstName,
                    MiddleName = studentProfileGroup.MiddleName,
                    LastName = studentProfileGroup.LastName,
                    ClassID = studentProfileGroup.ClassID,
                    SectionID = studentProfileGroup.SectionID,
                    SchoolAcademicyearID = studentProfileGroup.SchoolAcademicyearID,
                    AdmissionNumber = studentProfileGroup.AdmissionNumber,
                    AsOnDate = studentProfileGroup.AsOnDate == null ? null : studentProfileGroup.AsOnDate,
                    BloodGroupID = studentProfileGroup.BloodGroupID,
                    CastID = studentProfileGroup.CastID,
                    EmailID = studentProfileGroup.EmailID,
                    GenderID = studentProfileGroup.GenderID,
                    CategoryID = studentProfileGroup.StudentCategoryID,
                    RelegionID = studentProfileGroup.RelegionID,
                    CommunityID = studentProfileGroup.CommunityID,
                    MobileNumber = studentProfileGroup.MobileNumber,
                    StudentHouseID = studentProfileGroup.StudentHouseID,
                    SchoolID = studentProfileGroup.SchoolID,
                    Height = studentProfileGroup.Height ?? "NA",
                    Weight = studentProfileGroup.Weight ?? "NA",
                    DateOfBirth = studentProfileGroup.DateOfBirth.HasValue ? studentProfileGroup.DateOfBirth : (DateTime?)null,
                    AdmissionDate = studentProfileGroup.AdmissionDate.HasValue ? studentProfileGroup.AdmissionDate : (DateTime?)null,
                    GenderName = studentProfileGroup.GenderID.HasValue ? studentProfileGroup.Gender.Description ?? "NA" : "NA",
                    SectionName = studentProfileGroup.SectionID.HasValue ? studentProfileGroup.Section.SectionName ?? "NA" : "NA",
                    ClassName = studentProfileGroup.ClassID.HasValue ? studentProfileGroup.Class.ClassDescription ?? "NA" : "NA",
                    ClassCode = studentProfileGroup.ClassID.HasValue ? studentProfileGroup.Class.Code ?? "NA" : "NA",
                    CastName = studentProfileGroup.CastID.HasValue ? studentProfileGroup.Cast.CastDescription ?? "NA" : "NA",
                    CategoryName = studentProfileGroup.StudentCategoryID.HasValue ? studentProfileGroup.StudentCategory.Description ?? "NA" : "NA",
                    RelegionName = studentProfileGroup.RelegionID.HasValue ? studentProfileGroup.Relegion.RelegionName ?? "NA" : "NA",
                    SchoolAcademicYearName = studentProfileGroup.SchoolAcademicyearID.HasValue ? studentProfileGroup.AcademicYear1.Description + '(' + studentProfileGroup.AcademicYear1.AcademicYearCode + ')' : null,
                    Community = studentProfileGroup.CommunityID.HasValue ? studentProfileGroup.Community.CommunityDescription ?? "NA" : "NA",
                    BloodGroupName = studentProfileGroup.BloodGroupID.HasValue ? studentProfileGroup.BloodGroup.BloodGroupName ?? "NA" : "NA",
                    StudentHouse = studentProfileGroup.StudentHouseID.HasValue ? studentProfileGroup.StudentHouse.Description ?? "NA" : "NA",
                    SchoolName = studentProfileGroup.SchoolID.HasValue ? studentProfileGroup.School.SchoolName ?? "NA" : "NA",
                    Guardian = new GuardianDTO()
                    {
                        FatherFirstName = studentProfileGroup.Parent.FatherFirstName,
                        FatherMiddleName = studentProfileGroup.Parent.FatherMiddleName,
                        FatherLastName = studentProfileGroup.Parent.FatherLastName,
                        MotherFirstName = studentProfileGroup.Parent.MotherFirstName,
                        MotherMiddleName = studentProfileGroup.Parent.MotherMiddleName,
                        MotherLastName = studentProfileGroup.Parent.MotherLastName,
                        GuardianPhone = studentProfileGroup.Parent.GuardianPhone ?? "NA",
                        MotherPhone = studentProfileGroup.Parent.MotherPhone ?? "NA",
                        GaurdianEmail = studentProfileGroup.Parent.GaurdianEmail ?? "NA",
                        MotherEmailID = studentProfileGroup.Parent.MotherEmailID ?? "NA",
                    },
                    StudentPassportDetails = studentProfileGroup.StudentPassportDetails.Select(passportDetail => new StudentPassportDetailDTO()
                    {
                        AdhaarCardNo = passportDetail.AdhaarCardNo ?? "NA",
                        PassportNo = passportDetail.PassportNo ?? "NA",
                        PassportNoExpiry = passportDetail.PassportNoExpiry != null ? passportDetail.PassportNoExpiry : (DateTime?)null,
                        VisaNo = passportDetail.VisaNo ?? "NA",
                        VisaExpiry = passportDetail.VisaExpiry != null ? passportDetail.VisaExpiry : (DateTime?)null,
                        NationalIDNo = passportDetail.NationalIDNo ?? "NA",
                        NationalIDNoExpiry = passportDetail.NationalIDNoExpiry != null ? passportDetail.NationalIDNoExpiry : (DateTime?)null,
                    }).FirstOrDefault(),

                }).ToList();
            }

            return studentDTOList;
        }

        public StudentDTO GetStudentDetailsByStudentID(long studentId)
        {
            var studentDTO = new StudentDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDet = dbContext.Students.Where(x => x.StudentIID == studentId)
                    .Include(i => i.Gender)
                    .Include(i => i.Section)
                    .Include(i => i.Class)
                    .Include(i => i.Cast)
                    .Include(i => i.StudentCategory)
                    .Include(i => i.Relegion)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AcademicYear1)
                    .Include(i => i.Community)
                    .Include(i => i.BloodGroup)
                    .Include(i => i.StudentHouse)
                    .Include(i => i.School)
                    .Include(i => i.Parent)
                    .Include(i => i.StudentPassportDetails)
                    .AsNoTracking()
                    .FirstOrDefault();

                var siblingList = dbContext.Students.Where(y => y.ParentID == studentDet.ParentID && y.StudentIID != studentDet.StudentIID)
                    .Include(i => i.Class)
                    .AsNoTracking()
                    .ToList();

                studentDTO = FillStudentDTO(studentDet, siblingList);
            }

            return studentDTO;
        }

        public bool IsPassportNoDuplicated(string PassportNo, long StudentPassportDetailsIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentPassportDetail> passport;

                if (StudentPassportDetailsIID == 0)
                {
                    passport = db.StudentPassportDetails.Where(x => x.PassportNo == PassportNo).AsNoTracking().ToList();
                }
                else
                {
                    passport = db.StudentPassportDetails.Where(x => x.StudentPassportDetailsIID != StudentPassportDetailsIID && x.PassportNo == PassportNo).AsNoTracking().ToList();
                }

                if (passport.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsVisaNoDuplicated(string VisaNo, long StudentPassportDetailsIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentPassportDetail> visa;

                if (StudentPassportDetailsIID == 0)
                {
                    visa = db.StudentPassportDetails.Where(x => x.VisaNo == VisaNo).AsNoTracking().ToList();
                }
                else
                {
                    visa = db.StudentPassportDetails.Where(x => x.StudentPassportDetailsIID != StudentPassportDetailsIID && x.VisaNo == VisaNo).AsNoTracking().ToList();
                }

                if (visa.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsNationalIDNoDuplicated(string NationalIDNo, long StudentPassportDetailsIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentPassportDetail> visa;

                if (StudentPassportDetailsIID == 0)
                {
                    visa = db.StudentPassportDetails.Where(x => x.NationalIDNo == NationalIDNo).AsNoTracking().ToList();
                }
                else
                {
                    visa = db.StudentPassportDetails.Where(x => x.StudentPassportDetailsIID != StudentPassportDetailsIID && x.NationalIDNo == NationalIDNo).AsNoTracking().ToList();
                }

                if (visa.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public StudentDTO GetStudentDetailFromStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new StudentDTO();

                //Get Student details
                var stud = dbContext.Students.Where(x => x.StudentIID == studentID)
                    .Include(x => x.Class)
                    .Include(x => x.Section)
                    .Include(x => x.School)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.Parent)
                    .Include(x => x.StudentHouse)
                    .OrderByDescending(o => o.StudentIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var studentDTO = new StudentDTO()
                {
                    StudentIID = stud.StudentIID,
                    AdmissionNumber = stud.AdmissionNumber,
                    StudentFullName = stud.AdmissionNumber + "-" + stud.FirstName + " " + stud.MiddleName + " " + stud.LastName,
                    ClassID = stud.ClassID,
                    SectionID = stud.SectionID,
                    ClassName = stud.Class.ClassDescription,
                    SectionName = stud.Section.SectionName,
                    AcademicYearID = stud.AcademicYearID,
                    SchoolID = stud.SchoolID,
                    SchoolName = stud.School.SchoolName,
                    AcademicYear = stud.AcademicYear.Description + "(" + stud.AcademicYear.AcademicYearCode + ")",
                    ParentID = stud.ParentID,
                    EmailID = stud.Parent?.GaurdianEmail,
                    StudentHouse = stud.StudentHouse?.Description
                };

                return studentDTO;
            }
        }

        private void SiblingParentInfo(long siblingID, StudentDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var siblingDetails = dbContext.Students.Where(x => x.StudentIID == siblingID)
                    .Include(x => x.Parent)
                    .OrderByDescending(o => o.StudentIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (siblingDetails.Parent == null)
                {
                    throw new Exception("Selected sibling's parent login is missing! Please assign parent login access for that student.");
                }
                else
                {
                    if (dto.StudentIID != 0)
                    {
                        if (dto.ParentID != siblingDetails.ParentID)
                        {
                            throw new Exception("Selected Sibling Parent is different!");
                        }
                    }
                    else
                    {
                        if (dto.ParentLogin.LoginIID != siblingDetails.Parent.LoginID)
                        {
                            throw new Exception("Selected Sibling Parent is different!");
                        }
                    }
                }
            }
        }

        public GuardianDTO GetGuardianDetails(long studentId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var guardianDTO = new GuardianDTO();

                var parentMap = new Parent();

                if (studentId != 0)
                {
                    var stud = dbContext.Students.Where(s => s.StudentIID == studentId)
                        .Include(i => i.Parent).ThenInclude(i => i.Country)
                        .OrderByDescending(o => o.StudentIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    //parentMap = dbContext.Parents.Where(a => a.ParentIID == stud.ParentID)
                    //    .Include(i => i.Country)
                    //    .OrderByDescending(o => o.ParentIID)
                    //    .AsNoTracking()
                    //    .FirstOrDefault();
                    parentMap = stud.Parent;
                }
                else
                {
                    parentMap = dbContext.Parents.Where(a => a.LoginID == _context.LoginID)
                        .Include(i => i.Country)
                        .OrderByDescending(o => o.ParentIID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }

                if (parentMap != null)
                {
                    guardianDTO = new GuardianDTO()
                    {
                        ParentIID = parentMap.ParentIID,
                        ParentCode = parentMap.ParentCode,
                        FatherFirstName = parentMap.FatherFirstName,
                        FatherMiddleName = parentMap.FatherMiddleName,
                        FatherLastName = parentMap.FatherLastName,
                        MotherFirstName = parentMap.MotherFirstName,
                        MotherMiddleName = parentMap.MotherMiddleName,
                        MotherLastName = parentMap.MotherLastName,
                        GuardianFirstName = parentMap.GuardianFirstName,
                        GuardianMiddleName = parentMap.GuardianMiddleName,
                        GuardianLastName = parentMap.GuardianLastName,
                        GuardianPhone = parentMap.GuardianPhone,
                        MotherPhone = parentMap.MotherPhone,
                        PhoneNumber = parentMap.PhoneNumber,
                        FatherMobileNumberTwo = parentMap.FatherMobileNumberTwo,
                        FatherWhatsappMobileNo = parentMap.FatherWhatsappMobileNo,
                        MotherWhatsappMobileNo = parentMap.MotherWhatsappMobileNo,
                        GuardianWhatsappMobileNo = parentMap.GuardianWhatsappMobileNo,
                        FatherEmailID = parentMap.FatherEmailID,
                        GaurdianEmail = parentMap.GaurdianEmail,
                        MotherEmailID = parentMap.MotherEmailID,
                        FatherPassportNumber = parentMap.FatherPassportNumber,
                        MotherPassportNumber = parentMap.MotherPassportNumber,
                        FatherNationalID = parentMap.FatherNationalID,
                        MotherNationalID = parentMap.MotherNationalID,
                        FatherPassportNoExpiryDate = parentMap.FatherPassportNoExpiryDate.HasValue ? parentMap.FatherPassportNoExpiryDate : (DateTime?)null,
                        FatherPassportNoIssueDate = parentMap.FatherPassportNoIssueDate.HasValue ? parentMap.FatherPassportNoIssueDate : (DateTime?)null,
                        FatherNationalDNoExpiryDate = parentMap.FatherNationalDNoExpiryDate.HasValue ? parentMap.FatherNationalDNoExpiryDate : (DateTime?)null,
                        FatherNationalDNoIssueDate = parentMap.FatherNationalDNoIssueDate.HasValue ? parentMap.FatherNationalDNoIssueDate : (DateTime?)null,
                        MotherPassportNoExpiryDate = parentMap.MotherPassportNoExpiryDate.HasValue ? parentMap.MotherPassportNoExpiryDate : (DateTime?)null,
                        MotherPassportNoIssueDate = parentMap.MotherPassportNoIssueDate.HasValue ? parentMap.MotherPassportNoIssueDate : (DateTime?)null,
                        MotherNationalDNoExpiryDate = parentMap.MotherNationalDNoExpiryDate.HasValue ? parentMap.MotherNationalDNoExpiryDate : (DateTime?)null,
                        MotherNationalDNoIssueDate = parentMap.MotherNationalDNoIssueDate.HasValue ? parentMap.MotherNationalDNoIssueDate : (DateTime?)null,
                        FatherOccupation = parentMap.FatherOccupation,
                        MotherOccupation = parentMap.MotherOccupation,
                        MotherCompanyName = parentMap.MotherCompanyName,
                        FatherCompanyName = parentMap.FatherCompanyName,
                        LocationName = parentMap.LocationName,
                        LocationNo = parentMap.LocationNo,
                        BuildingNo = parentMap.BuildingNo,
                        FlatNo = parentMap.FlatNo,
                        StreetName = parentMap.StreetName,
                        StreetNo = parentMap.StreetNo,
                        ZipNo = parentMap.ZipNo,
                        PostBoxNo = parentMap.PostBoxNo,
                        City = parentMap.City,
                        CountryID = parentMap.CountryID,
                        Country = parentMap.CountryID.HasValue ? parentMap.Country.CountryName : null,
                        FatherCountryID = parentMap.FatherCountryID,
                        MotherCountryID = parentMap.MotherCountryID,
                        GuardianTypeID = parentMap.GuardianTypeID,
                    };
                }

                return guardianDTO;
            }
        }

        public ClassSectionShiftingDTO GetStudentsForShifting(int classID, int sectionID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var advancedAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ADVANCE_ACADEMIC_YEAR_STATUSID");
                int? advancedAcademicYearStatusID = int.Parse(advancedAcademicYearStatus);

                var entityStudent = (from s in dbContext.Students
                                     join a in dbContext.AcademicYears on s.SchoolAcademicyearID equals a.AcademicYearID
                                     where (s.ClassID == classID && s.SectionID == sectionID && s.IsActive == true && s.SchoolID == _context.SchoolID
                                     && (a.AcademicYearStatusID != advancedAcademicYearStatusID || (a.AcademicYearStatusID == advancedAcademicYearStatusID && s.SchoolAcademicyearID == _context.AcademicYearID))
                                     && s.AcademicYearID == _context.AcademicYearID)
                                     select new ClassSectionStudentDTO { SectionID = s.SectionID, StudentID = s.StudentIID, StudentName = (s.AdmissionNumber + "-" + s.FirstName + " " + s.MiddleName + " " + s.LastName) })
                                    .OrderBy(z => z.StudentID)
                                    .AsNoTracking().ToList();

                var entitySection = dbContext.Sections.Where(x => x.SchoolID == _context.SchoolID)
                    .AsNoTracking()
                    .Select(x => new KeyValueDTO
                    {
                        Key = x.SectionID.ToString(),
                        Value = x.SectionName
                    }).ToList();

                var classSectionShiftingDTO = new ClassSectionShiftingDTO()
                {
                    ClassSectionStudentDTO = entityStudent,
                    ToShiftSectionIDs = entitySection
                };

                return classSectionShiftingDTO;
            }
        }

        public string ShiftStudentSection(ClassSectionShiftingDTO toDto)
        {
            string rtnMessage = "T#Updated Successfully!";
            try
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    var entity = dbContext.Students.Where(x => x.StudentIID == toDto.StudentID).AsNoTracking().FirstOrDefault();
                    if (entity == null)
                    {
                        return "F#Student is not found!";
                    }

                    UpdateHelthEntryDatas(toDto.ToShiftSectionID, toDto.StudentID, entity.ClassID, entity.SectionID);
                    UpdateRemarkEntryData(toDto.ToShiftSectionID, toDto.StudentID, entity.ClassID, entity.SectionID);
                    UpdateTransportDatas(toDto.ToShiftSectionID, toDto.StudentID);

                    entity.SectionID = toDto.ToShiftSectionID;

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    //Update Student Attendance details when section changes
                    var studntAttendance = dbContext.StudentAttendences.Where(a => a.StudentID == toDto.StudentID && a.AcademicYearID == _context.AcademicYearID && a.SchoolID == _context.SchoolID).AsNoTracking().ToList();
                    if (studntAttendance != null)
                    {
                        foreach (var datas in studntAttendance)
                        {
                            datas.SectionID = toDto.ToShiftSectionID;
                            dbContext.Entry(datas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }

                    return rtnMessage;
                }
            }
            catch (Exception ex)
            {
                return "F#" + ex.Message;
            }
        }


        public string UpdateHelthEntryDatas(int? shiftToSectionID, long studentID, int? classID, int? sectionID)
        {
            //Update Student HealthEntry datas section when class section changes
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var existingData = dbContext.HealthEntryStudentMaps
                                    .Where(m => m.StudentID == studentID && m.HealthEntry.ClassID == classID && m.HealthEntry.SectionID == sectionID)
                                    .Include(i => i.HealthEntry).ToList();

                foreach (var datas in existingData)
                {
                    //find same class,tosection,academic,examgroup healthentry data for IID  -- then modified
                    var mainTableData = dbContext.HealthEntries
                        .Where(h => h.ClassID == datas.HealthEntry.ClassID && h.SectionID == shiftToSectionID && h.AcademicYearID == datas.HealthEntry.AcademicYearID && h.ExamGroupID == datas.HealthEntry.ExamGroupID)
                        .OrderByDescending(o => o.HealthEntryIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (mainTableData != null)
                    {
                        datas.HealthEntryID = mainTableData.HealthEntryIID;
                        dbContext.Entry(datas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    //case when class,section,academic,examgroup not match then create new one
                    else
                    {
                        var mainData = new HealthEntry()
                        {
                            ClassID = datas.HealthEntry.ClassID,
                            SectionID = shiftToSectionID,
                            ExamGroupID = datas.HealthEntry.ExamGroupID,
                            TeacherID = _context.EmployeeID.HasValue ? _context.EmployeeID : null,
                            SchoolID = _context.SchoolID.HasValue ? Convert.ToByte(_context.SchoolID) : (byte?)null,
                            AcademicYearID = datas.HealthEntry.AcademicYearID.HasValue ? datas.HealthEntry.AcademicYearID : _context.AcademicYearID,
                            CreatedBy = Convert.ToInt32(_context.LoginID),
                            CreatedDate = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedDate = null,
                        };

                        var mapData = new HealthEntryStudentMap()
                        {
                            HealthEntryID = mainData.HealthEntryIID,
                            StudentID = datas.StudentID,
                            Height = datas.Height,
                            Weight = datas.Weight,
                            BMS = datas.BMS,
                            Remarks = datas.Remarks,
                            Vision = datas.Vision,
                            BMI = datas.BMI,
                        };

                        dbContext.HealthEntries.Add(mainData);
                        dbContext.HealthEntryStudentMaps.Remove(datas);
                    }
                    dbContext.SaveChanges();
                }
            }

            return null;
        }

        public string UpdateRemarkEntryData(int? shiftToSectionID, long studentID, int? classID, int? sectionID)
        {
            //Update Student RemarkEntries when class section changes
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var existingData = dbContext.RemarksEntryStudentMaps
                                    .Where(m => m.StudentID == studentID && m.RemarksEntry.ClassID == classID && m.RemarksEntry.SectionID == sectionID)
                                    .Include(i => i.RemarksEntry).ToList();

                foreach (var datas in existingData)
                {
                    //find same class,tosection,academic,examgroup healthentry data for IID
                    var mainTableData = dbContext.RemarksEntries
                        .Where(h => h.ClassID == datas.RemarksEntry.ClassID && h.SectionID == shiftToSectionID
                         && h.AcademicYearID == datas.RemarksEntry.AcademicYearID && h.ExamGroupID == datas.RemarksEntry.ExamGroupID)
                        .OrderByDescending(o => o.RemarksEntryIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (mainTableData != null)
                    {
                        datas.RemarksEntryID = mainTableData.RemarksEntryIID;
                        dbContext.Entry(datas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    //case when class,section,academic,examgroup not match then create new one
                    else
                    {
                        var mainData = new RemarksEntry()
                        {
                            ClassID = datas.RemarksEntry.ClassID,
                            SectionID = shiftToSectionID,
                            ExamGroupID = datas.RemarksEntry.ExamGroupID,
                            TeacherID = _context.EmployeeID.HasValue ? _context.EmployeeID : null,
                            SchoolID = _context.SchoolID.HasValue ? Convert.ToByte(_context.SchoolID) : (byte?)null,
                            AcademicYearID = datas.RemarksEntry.AcademicYearID.HasValue ? datas.RemarksEntry.AcademicYearID : _context.AcademicYearID,
                            CreatedBy = Convert.ToInt32(_context.LoginID),
                            CreatedDate = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedDate = null,
                        };

                        var mapData = new RemarksEntryStudentMap()
                        {
                            RemarksEntryID = mainData.RemarksEntryIID,
                            StudentID = datas.StudentID,
                            Remarks1 = datas.Remarks1,
                            Remarks2 = datas.Remarks2
                        };

                        dbContext.RemarksEntries.Add(mainData);
                        dbContext.RemarksEntryStudentMaps.Remove(datas);
                    }
                    dbContext.SaveChanges();
                }
            }
            return null;
        }

        public string UpdateApplicationStatus(long? applicationID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var admittedStstID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STUDENT_ADMITTED_STATUS");

                //For Change Student Application Status
                if (applicationID != null)
                {
                    var application = dbContext.StudentApplications.Where(X => X.ApplicationIID == applicationID).AsNoTracking().FirstOrDefault();
                    var studentdetail = dbContext.Students.Where(X => X.ApplicationID == application.ApplicationIID).AsNoTracking().FirstOrDefault();

                    if (application != null)
                    {
                        application.ApplicationStatusID = admittedStstID;
                        application.UpdatedBy = studentdetail.ApplicationID > 0 ? (int)_context.LoginID : studentdetail.CreatedBy;
                        application.UpdatedDate = studentdetail.ApplicationID > 0 ? DateTime.Now : studentdetail.CreatedDate;
                    }

                    dbContext.Entry(application).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }

            return null;
        }

        public string UpdateLoginUserID(long? applicationID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                //For Change Update LoginUserID
                if (applicationID != null)
                {
                    var appDetail = dbContext.StudentApplications.Where(X => X.ApplicationIID == applicationID).AsNoTracking().FirstOrDefault();
                    var logdetail = dbContext.Logins.Where(X => X.LoginIID == appDetail.LoginID).AsNoTracking().FirstOrDefault();
                    var parentdetail = dbContext.Parents.Where(X => X.LoginID == logdetail.LoginIID).AsNoTracking().FirstOrDefault();
                    if (parentdetail != null)
                    {
                        logdetail.LoginUserID = parentdetail.ParentCode;
                    }

                    dbContext.Entry(logdetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            return null;
        }

        public string UpdateLeadStatus(long? applicationID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var leadClosedID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("LEAD_CLOSED_STATUS_ID");

                //For Change Lead Application Status
                if (applicationID != null)
                {
                    var studapplication = dbContext.StudentApplications.Where(X => X.ApplicationIID == applicationID).AsNoTracking().FirstOrDefault();
                    var studentdetail = dbContext.Students.Where(X => X.ApplicationID == studapplication.ApplicationIID).AsNoTracking().FirstOrDefault();

                    if (studapplication != null && studapplication.ProspectNumber != null)
                    {
                        var leads = dbContext.Leads.Where(X => X.LeadCode == studapplication.ProspectNumber).AsNoTracking().FirstOrDefault();
                        if (leads != null)
                        {
                            leads.LeadStatusID = leadClosedID;
                            leads.UpdatedBy = studentdetail.ApplicationID > 0 ? (int)_context.LoginID : studentdetail.CreatedBy;
                            leads.UpdatedDate = studentdetail.ApplicationID > 0 ? DateTime.Now : studentdetail.CreatedDate;

                            dbContext.Entry(leads).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }
                }
            }

            return null;
        }

        public List<KeyValueDTO> GetSectionsBySchool(byte schoolID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sectionsList = new List<KeyValueDTO>();

                var entities = dbContext.Sections.Where(x => x.SchoolID == schoolID).OrderBy(o => o.Code).AsNoTracking().ToList();

                foreach (var sections in entities)
                {
                    sectionsList.Add(new KeyValueDTO
                    {
                        Key = sections.SectionID.ToString(),
                        Value = sections.SectionName
                    });
                }

                return sectionsList;
            }
        }

        public string GetProgressReportName(long schoolID, int? classID)
        {

            var reportName = string.Empty;
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT [SCHOOLS].[FNS_PROGRESS_REPORT_BY_CLASS](@SCHOOLID,@CLASSID)", conn))
                {
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
                    sqlCommand.Parameters["@SCHOOLID"].Value = schoolID;

                    sqlCommand.Parameters.Add(new SqlParameter("@CLASSID", SqlDbType.Int));
                    sqlCommand.Parameters["@CLASSID"].Value = classID;

                    try
                    {
                        conn.Open();
                        reportName = Convert.ToString(sqlCommand.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something Wrong! Please check after sometime");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return reportName;

        }

        public List<KeyValueDTO> GetAdvancedAcademicYearBySchool(int schoolID)
        {
            if (schoolID == 0)
            {
                schoolID = Convert.ToInt32(_context.SchoolID);
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var advancestatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ADVANCE_ACADEMIC_YEAR_STATUSID");

                var academicyearList = dbContext.AcademicYears
                    .Where(srcs => srcs.AcademicYearStatusID == advancestatusID && srcs.SchoolID == (byte?)schoolID)
                    .AsNoTracking()
                    .Select(srcs => new KeyValueDTO
                    {
                        Key = srcs.AcademicYearID.ToString(),
                        Value = srcs.Description + " " + "(" + srcs.AcademicYearCode + ")"
                    }).ToList();

                return academicyearList;
            }
        }

        private void GetStudentDocuments(Student entity, StudentDTO dto)
        {
            var documentsMap = entity.StudentApplicationDocumentMaps.FirstOrDefault();

            if (documentsMap != null)
            {
                dto.StudentDocUploads.ApplicationDocumentIID = documentsMap.ApplicationDocumentIID;
                dto.StudentDocUploads.ApplicationID = documentsMap.ApplicationID;
                dto.StudentDocUploads.BirthCertificateReferenceID = documentsMap.BirthCertificateReferenceID.HasValue ? documentsMap.BirthCertificateReferenceID : null;
                dto.StudentDocUploads.BirthCertificateAttach = documentsMap.BirthCertificateAttach != null ? documentsMap.BirthCertificateAttach : null;
                dto.StudentDocUploads.StudentPassportReferenceID = documentsMap.StudentPassportReferenceID.HasValue ? documentsMap.StudentPassportReferenceID : null;
                dto.StudentDocUploads.StudentPassportAttach = documentsMap.StudentPassportAttach != null ? documentsMap.StudentPassportAttach : null;
                dto.StudentDocUploads.TCReferenceID = documentsMap.TCReferenceID.HasValue ? documentsMap.TCReferenceID : null;
                dto.StudentDocUploads.TCAttach = documentsMap.TCAttach != null ? documentsMap.TCAttach : null;
                dto.StudentDocUploads.FatherQIDReferenceID = documentsMap.FatherQIDReferenceID.HasValue ? documentsMap.FatherQIDReferenceID : null;
                dto.StudentDocUploads.FatherQIDAttach = documentsMap.FatherQIDAttach != null ? documentsMap.FatherQIDAttach : null;
                dto.StudentDocUploads.MotherQIDReferenceID = documentsMap.MotherQIDReferenceID.HasValue ? documentsMap.MotherQIDReferenceID : null;
                dto.StudentDocUploads.MotherQIDAttach = documentsMap.MotherQIDAttach != null ? documentsMap.MotherQIDAttach : null;
                dto.StudentDocUploads.StudentQIDReferenceID = documentsMap.StudentQIDReferenceID.HasValue ? documentsMap.StudentQIDReferenceID : null;
                dto.StudentDocUploads.StudentQIDAttach = documentsMap.StudentQIDAttach != null ? documentsMap.StudentQIDAttach : null;
                dto.StudentDocUploads.CreatedBy = documentsMap.CreatedBy;
                dto.StudentDocUploads.UpdatedBy = documentsMap.UpdatedBy;
                dto.StudentDocUploads.CreatedDate = documentsMap.CreatedDate;
                dto.StudentDocUploads.UpdatedDate = documentsMap.UpdatedDate;
            }
        }

        public GuardianDTO GetParentDetailFromParentID(long parentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new GuardianDTO();

                //Get Parent details
                var getParentDatas = dbContext.Parents.Where(x => x.ParentIID == parentID)
                    .AsNoTracking().FirstOrDefault();

                var studentDTO = new GuardianDTO()
                {
                    #region store parentDatas from ParentTable using parentID
                    ParentIID = getParentDatas.ParentIID,
                    ParentCode = getParentDatas.ParentCode,
                    FatherFirstName = getParentDatas.FatherFirstName,
                    FatherMiddleName = getParentDatas.FatherMiddleName,
                    FatherLastName = getParentDatas.FatherLastName,
                    FatherCompanyName = getParentDatas.FatherCompanyName,
                    FatherOccupation = getParentDatas.FatherOccupation,
                    GuardianOccupation = getParentDatas.GuardianOccupation,
                    PhoneNumber = getParentDatas.PhoneNumber,
                    FatherMobileNumberTwo = getParentDatas.FatherMobileNumberTwo,
                    FatherEmailID = getParentDatas.FatherEmailID,
                    FatherProfile = getParentDatas.FatherProfile,
                    FatherCountryID = getParentDatas.FatherCountryID,
                    FatherCountry = getParentDatas.FatherCountryID.HasValue ? getParentDatas.FatherCountryID.ToString() : null,
                    GuardianRelation = getParentDatas.GuardianRelation,
                    CanYouVolunteerToHelpOneID = getParentDatas.CanYouVolunteerToHelpOneID,
                    FatherPassportNumber = getParentDatas.FatherPassportNumber,
                    FatherPassportCountryofIssueID = getParentDatas.FatherPassportCountryofIssueID,
                    FatherPassportCountryofIssue = getParentDatas.FatherPassportCountryofIssueID.HasValue ? getParentDatas.FatherPassportCountryofIssueID.ToString() : null,
                    FatherPassportNoIssueDate = getParentDatas.FatherPassportNoIssueDate,
                    FatherPassportNoExpiryDate = getParentDatas.FatherPassportNoExpiryDate,
                    FatherNationalID = getParentDatas.FatherNationalID,
                    FatherNationalDNoIssueDate = getParentDatas.FatherNationalDNoIssueDate,
                    FatherNationalDNoExpiryDate = getParentDatas.FatherNationalDNoExpiryDate,
                    GuardianName = getParentDatas.GuardianName,
                    GuardianPhoto = getParentDatas.GuardianPhoto,
                    MotherFirstName = getParentDatas.MotherFirstName,
                    MotherMiddleName = getParentDatas.MotherMiddleName,
                    MotherLastName = getParentDatas.MotherLastName,
                    MotherCompanyName = getParentDatas.MotherCompanyName,
                    MotherOccupation = getParentDatas.MotherOccupation,
                    MotherPhone = getParentDatas.MotherPhone,
                    MotherEmailID = getParentDatas.MotherEmailID,
                    CanYouVolunteerToHelpTwoID = getParentDatas.CanYouVolunteerToHelpTwoID,
                    MotherCountryID = getParentDatas.MotherCountryID,
                    MotherCountry = getParentDatas.MotherCountryID.HasValue ? getParentDatas.MotherCountryID.ToString() : null,
                    MotherPofile = getParentDatas.MotherPofile,
                    MotherPassportNumber = getParentDatas.MotherPassportNumber,
                    MotherPassportCountryofIssueID = getParentDatas.MotherPassportCountryofIssueID,
                    MotherPassportCountryofIssue = getParentDatas.MotherPassportCountryofIssueID.HasValue ? getParentDatas.MotherPassportCountryofIssueID.ToString() : null,
                    MotherPassportNoIssueDate = getParentDatas.MotherPassportNoIssueDate,
                    MotherPassportNoExpiryDate = getParentDatas.MotherPassportNoExpiryDate,
                    MotherNationalID = getParentDatas.MotherNationalID,
                    MotherNationalDNoIssueDate = getParentDatas.MotherNationalDNoIssueDate,
                    MotherNationalDNoExpiryDate = getParentDatas.MotherNationalDNoExpiryDate,
                    LoginID = getParentDatas.LoginID,
                    #endregion
                };

                return studentDTO;
            }
        }

        public void UpdateTransportDatas(int? sectionID, long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDetails = dbContext.Students.Where(a => a.StudentIID == studentID).AsNoTracking().FirstOrDefault();

                if (studentDetails != null)
                {
                    var transportDetail = dbContext.StudentRouteStopMaps
                        .Where(a => a.StudentID == studentID && a.AcademicYearID == studentDetails.AcademicYearID && a.SchoolID == studentDetails.SchoolID && a.IsActive == studentDetails.IsActive)
                        .OrderByDescending(o => o.StudentRouteStopMapIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (transportDetail != null)
                    {
                        transportDetail.SectionID = sectionID;
                        transportDetail.ClassID = studentDetails.ClassID;
                        dbContext.Entry(transportDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
        }


        public List<KeyValueDTO> GetStudentsByParameters(int academicYearID, int? classID, int? sectionID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var query = dbContext.Students
                    .Where(a => a.AcademicYearID == academicYearID && a.IsActive == true && a.Status == 1);

                if (classID.HasValue)
                {
                    query = query.Where(a => a.ClassID == classID);
                }

                if (sectionID.HasValue)
                {
                    query = query.Where(a => a.SectionID == sectionID);
                }

                var studentList = query
                    .AsNoTracking()
                    .OrderBy(z => z.AdmissionNumber)
                    .Select(stud => new KeyValueDTO
                    {
                        Key = stud.StudentIID.ToString(),
                        Value = $"{stud.AdmissionNumber} - {stud.FirstName} {stud.MiddleName} {stud.LastName}"
                    })
                    .ToList();

                return studentList;
            }
        }

        public List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("PROGRESS_REPORT_PUBLISH_STATUS_PB", 2);

                List<byte> schoolIDs = new List<byte>();
                List<KeyValueDTO> academicyearLists = new List<KeyValueDTO>();

                var dData = (from n in dbContext.ProgressReports
                             join m in dbContext.AcademicYears on n.AcademicYearID equals m.AcademicYearID
                             where n.StudentId == studentID && n.PublishStatusID == publishStatusID
                             select new KeyValueDTO()
                             {
                                 Key = m.AcademicYearID.ToString(),
                                 Value = m.Description + " " + "(" + m.AcademicYearCode + ")"
                             }).AsNoTracking().ToList();

                if (dData.Any())
                    academicyearLists.AddRange(dData);

                return academicyearLists;
            }
        }


        public string GetStudentLoginDetailsByStudentCode(string studentCode)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var student = dbContext.Students.Where(s => s.IsActive == true && s.AdmissionNumber == studentCode)
                    .Include(x => x.Login)
                    .Include(x => x.Parent)
                    .AsNoTracking()
                    .FirstOrDefault();
                //var loginDet = parent != null ? dbContext.Logins.FirstOrDefault(l => l.LoginIID == parent.LoginID) : null;
                if (student != null)
                {
                    return student.Login.LoginEmailID;
                }
                else
                {
                    return null;
                }
            }
        }

        public StudentDTO GetStudentDetails(string emailID)
        {
            var dto = new StudentDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var login = dbContext.Logins.Where(x => x.LoginEmailID == emailID).AsNoTracking().FirstOrDefault();
                var student = login != null ? dbContext.Students.Where(s => s.LoginID == login.LoginIID).AsNoTracking().FirstOrDefault() : null;
                if (student != null)
                {
                    dto = new StudentDTO()
                    {
                        StudentIID = (long)student.LoginID
                        //GuardianName = student.GuardianName,
                        //GuardianPhone = student.GuardianPhone,
                        //GaurdianEmail = emailID,
                        //ParentIID = student.ParentIID
                    };
                }
            }
            return dto;
        }

        public StudentDTO FillStudentDTO(Student studentDet, List<Student> siblingList)
        {
            var studentDTO = new StudentDTO();

            studentDTO = new StudentDTO()
            {
                StudentIID = studentDet.StudentIID,
                StudentProfile = studentDet.StudentProfile,
                FirstName = studentDet.FirstName,
                MiddleName = studentDet.MiddleName,
                LastName = studentDet.LastName,
                ClassID = studentDet.ClassID,
                SectionID = studentDet.SectionID,
                SchoolAcademicyearID = studentDet.SchoolAcademicyearID,
                AdmissionNumber = studentDet.AdmissionNumber,
                AsOnDate = studentDet.AsOnDate == null ? null : studentDet.AsOnDate,
                BloodGroupID = studentDet.BloodGroupID,
                CastID = studentDet.CastID,
                EmailID = studentDet.EmailID,
                GenderID = studentDet.GenderID,
                CategoryID = studentDet.StudentCategoryID,
                RelegionID = studentDet.RelegionID,
                CommunityID = studentDet.CommunityID,
                MobileNumber = studentDet.MobileNumber,
                StudentHouseID = studentDet.StudentHouseID,
                SchoolID = studentDet.SchoolID,
                Height = studentDet.Height ?? "NA",
                Weight = studentDet.Weight ?? "NA",
                DateOfBirth = studentDet.DateOfBirth.HasValue ? studentDet.DateOfBirth : (DateTime?)null,
                AdmissionDate = studentDet.AdmissionDate.HasValue ? studentDet.AdmissionDate : (DateTime?)null,
                GenderName = studentDet.GenderID.HasValue ? studentDet.Gender.Description ?? "NA" : "NA",
                SectionName = studentDet.SectionID.HasValue ? studentDet.Section.SectionName ?? "NA" : "NA",
                ClassName = studentDet.ClassID.HasValue ? studentDet.Class.ClassDescription ?? "NA" : "NA",
                ClassCode = studentDet.ClassID.HasValue ? studentDet.Class.Code ?? "NA" : "NA",
                CastName = studentDet.CastID.HasValue ? studentDet.Cast.CastDescription ?? "NA" : "NA",
                CategoryName = studentDet.StudentCategoryID.HasValue ? studentDet.StudentCategory.Description ?? "NA" : "NA",
                RelegionName = studentDet.RelegionID.HasValue ? studentDet.Relegion.RelegionName ?? "NA" : "NA",
                AcademicYearID = studentDet.AcademicYearID,
                AcademicYear = studentDet.AcademicYearID.HasValue ? studentDet.AcademicYear.Description + '(' + studentDet.AcademicYear.AcademicYearCode + ')' : null,
                SchoolAcademicYearName = studentDet.SchoolAcademicyearID.HasValue ? studentDet.AcademicYear1.Description + '(' + studentDet.AcademicYear1.AcademicYearCode + ')' : null,
                Community = studentDet.CommunityID.HasValue ? studentDet.Community.CommunityDescription ?? "NA" : "NA",
                BloodGroupName = studentDet.BloodGroupID.HasValue ? studentDet.BloodGroup.BloodGroupName ?? "NA" : "NA",
                StudentHouse = studentDet.StudentHouseID.HasValue ? studentDet.StudentHouse.Description ?? "NA" : "NA",
                SchoolName = studentDet.SchoolID.HasValue ? studentDet.School.SchoolName ?? "NA" : "NA",
                ParentID = studentDet.ParentID,
                ParentEmailID = studentDet.Parent?.GaurdianEmail,
                SchoolShortName = studentDet?.School?.SchoolShortName,
                Guardian = new GuardianDTO()
                {
                    FatherFirstName = studentDet.Parent.FatherFirstName,
                    FatherMiddleName = studentDet.Parent.FatherMiddleName,
                    FatherLastName = studentDet.Parent.FatherLastName,
                    MotherFirstName = studentDet.Parent.MotherFirstName,
                    MotherMiddleName = studentDet.Parent.MotherMiddleName,
                    MotherLastName = studentDet.Parent.MotherLastName,
                    GuardianPhone = studentDet.Parent.GuardianPhone ?? "NA",
                    MotherPhone = studentDet.Parent.MotherPhone ?? "NA",
                    GaurdianEmail = studentDet.Parent.GaurdianEmail ?? "NA",
                    MotherEmailID = studentDet.Parent.MotherEmailID ?? "NA",
                    LoginID = studentDet.Parent.LoginID
                },
                StudentPassportDetails = studentDet.StudentPassportDetails.Select(passportDetail => new StudentPassportDetailDTO()
                {
                    AdhaarCardNo = passportDetail.AdhaarCardNo ?? "NA",
                    PassportNo = passportDetail.PassportNo ?? "NA",
                    PassportNoExpiry = passportDetail.PassportNoExpiry != null ? passportDetail.PassportNoExpiry : (DateTime?)null,
                    VisaNo = passportDetail.VisaNo ?? "NA",
                    VisaExpiry = passportDetail.VisaExpiry != null ? passportDetail.VisaExpiry : (DateTime?)null,
                    NationalIDNo = passportDetail.NationalIDNo ?? "NA",
                    NationalIDNoExpiry = passportDetail.NationalIDNoExpiry != null ? passportDetail.NationalIDNoExpiry : (DateTime?)null,
                }).FirstOrDefault(),
            };

            foreach (var sib in siblingList)
            {
                studentDTO.StudentSiblings.Add(new KeyValueDTO()
                {
                    Key = sib.StudentIID.ToString(),
                    Value = sib.AdmissionNumber + " - " + sib.FirstName + " " + sib.MiddleName + " " + sib.LastName,
                });
            }

            return studentDTO;
        }

        public StudentDTO GetStudentDetailsByAdmissionNumber(string admissionNumber)
        {
            var studentDTO = new StudentDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDet = dbContext.Students.Where(x => x.AdmissionNumber == admissionNumber)
                    .Include(i => i.Gender)
                    .Include(i => i.Section)
                    .Include(i => i.Class)
                    .Include(i => i.Cast)
                    .Include(i => i.StudentCategory)
                    .Include(i => i.Relegion)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AcademicYear1)
                    .Include(i => i.Community)
                    .Include(i => i.BloodGroup)
                    .Include(i => i.StudentHouse)
                    .Include(i => i.School)
                    .Include(i => i.Parent)
                    .Include(i => i.StudentPassportDetails)
                    .AsNoTracking()
                    .FirstOrDefault();

                var siblingList = dbContext.Students.Where(y => y.ParentID == studentDet.ParentID && y.StudentIID != studentDet.StudentIID)
                    .Include(i => i.Class)
                    .AsNoTracking()
                    .ToList();

                studentDTO = FillStudentDTO(studentDet, siblingList);
            }

            return studentDTO;
        }

        public List<KeyValueDTO> GetStudentsByParentAndClass(long parentID, int classID)
        {
            var studentList = new List<KeyValueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entities = dbContext.Students.Where(x => x.ParentID == parentID && x.ClassID == classID && x.IsActive == true).AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    studentList.Add(new KeyValueDTO()
                    {
                        Key = entity.StudentIID.ToString(),
                        Value = entity.AdmissionNumber + " - " + entity.FirstName + (string.IsNullOrEmpty(entity.MiddleName) ? " " : " " + entity.MiddleName + " ") + entity.LastName,
                    });
                }
            }

            return studentList;
        }

        public List<KeyValueDTO> GetStudentsByParent(long parentID)
        {
            var studentList = new List<KeyValueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entities = dbContext.Students.Where(x => x.ParentID == parentID && x.IsActive == true).AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    studentList.Add(new KeyValueDTO()
                    {
                        Key = entity.StudentIID.ToString(),
                        Value = entity.AdmissionNumber + " - " + entity.FirstName + " " + (string.IsNullOrEmpty(entity.MiddleName) ? "" : entity.MiddleName + " ") + entity.LastName,
                    });
                }
            }

            return studentList;
        }

        public List<StudentDTO> GetStudentsDetailsByParent(long parentID)
        {
            var studentList = new List<StudentDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entities = dbContext.Students.Where(x => x.ParentID == parentID && x.IsActive == true)
                    .Include(x => x.Class)
                    .Include(x => x.Section)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    studentList.Add(new StudentDTO()
                    {
                        StudentIID = entity.StudentIID,
                        AdmissionNumber = entity.AdmissionNumber,
                        StudentFullName = entity.AdmissionNumber + " - " + entity.FirstName + " " + (string.IsNullOrEmpty(entity.MiddleName) ? "" : entity.MiddleName + " ") + entity.LastName,
                        ClassID = entity.ClassID,
                        ClassName = entity.ClassID.HasValue ? entity.Class?.ClassDescription : null,
                        SectionID = entity.SectionID,
                        SectionName = entity.SectionID.HasValue ? entity.Section?.SectionName : null,
                    });
                }
            }

            return studentList;
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(compressedData))
            {
                using (GZipStream decompressionStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    using (MemoryStream decompressedStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }

    }
}