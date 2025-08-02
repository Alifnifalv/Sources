using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using Eduegate.Services.Contracts.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentApplicationMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "StudentPassportNo", "StudentVisaNo", "StudentNationalID" };
        public static StudentApplicationMapper Mapper(CallContext context)
        {
            var mapper = new StudentApplicationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentApplicationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public StudentApplicationDTO GetApplication(long IID)
        {
            return ToDTO(IID);
        }

        public void DeleteApplication(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentApplications.Where(a => a.ApplicationIID == IID).AsNoTracking().FirstOrDefault();
                if (entity != null)
                {
                    dbContext.StudentApplications.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public void DeleteSiblingApplication(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentApplication = dbContext.StudentApplications.Where(a => a.ApplicationIID == IID).AsNoTracking().FirstOrDefault();
                if (studentApplication != null)
                {
                    dbContext.StudentApplications.Remove(studentApplication);
                    dbContext.SaveChanges();
                }

                var sibApplication = dbContext.StudentApplicationSiblingMaps.Where(a => a.ApplicationID == IID).AsNoTracking().FirstOrDefault();
                if (sibApplication != null)
                {
                    dbContext.StudentApplicationSiblingMaps.Remove(sibApplication);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<StudentApplicationDTO> GetStudentApplicationsByLogin(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new List<StudentApplicationDTO>();

                var applications = dbContext.StudentApplications.Where(a => a.LoginID == loginID)
                    .Include(x => x.StreamGroup)
                    .Include(x => x.Stream)
                    .Include(x => x.Subject)
                    .Include(x => x.Subject1)
                    .Include(x => x.Login)
                    .Include(x => x.School)
                    .Include(x => x.Country)
                    .Include(x => x.Country1)
                    .Include(x => x.Gender)
                    .Include(x => x.Nationality)
                    .Include(x => x.Nationality1)
                    .Include(x => x.Nationality2)
                    .Include(x => x.Nationality3)
                    .Include(x => x.Language)
                    .Include(x => x.Language1)
                    .Include(x => x.Relegion)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.ApplicationStatus)
                    .Include(x => x.Cast)
                    .Include(x => x.Class)
                    .Include(x => x.Class1)
                    .Include(x => x.GuardianType)
                    .Include(x => x.GuardianType1)
                    .Include(x => x.GuardianType2)
                    .Include(x => x.GuardianType3)
                    .Include(x => x.PassportDetailMap)
                    .Include(x => x.PassportDetailMap1)
                    .Include(x => x.PassportDetailMap2)
                    .Include(x => x.PassportDetailMap3).ThenInclude(y => y.Country)
                    .Include(x => x.Syllabu)
                    .Include(x => x.StudentCategory)
                    .Include(x => x.Syllabu1)
                    .Include(x => x.VisaDetailMap)
                    .Include(x => x.VisaDetailMap1)
                    .Include(x => x.VisaDetailMap2)
                    .Include(x => x.VisaDetailMap3)
                    .Include(x => x.VolunteerType)
                    .Include(x => x.VolunteerType1)
                    .Include(x => x.StudentApplicationDocumentMaps)
                    .AsNoTracking()
                    .ToList();

                foreach (var application in applications)
                {
                    dtos.Add(ToDTO(application));
                }

                return dtos;
            }
        }

        public int GetStudentApplicationsByLoginCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var applications = dbContext.StudentApplications.Where(a => a.LoginID == loginID).AsNoTracking().ToList();

                var totalTransportApplicationCount = applications != null ? applications.Count() : 0;

                return totalTransportApplicationCount;
            }
        }

        public int GetApplicationsByLoginCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentApplications = dbContext.StudentApplications.Where(a => a.LoginID == loginID).AsNoTracking().ToList();
                var transportApplications = dbContext.TransportApplications.Where(a => a.LoginID == loginID).AsNoTracking().ToList();

                var totalStudentApplicationCount = studentApplications != null ? studentApplications.Count() : 0;
                var totalTransportApplicationCount = transportApplications != null ? transportApplications.Count() : 0;
                var totalApplicationCount = totalStudentApplicationCount + totalTransportApplicationCount;

                return totalApplicationCount;
            }
        }

        private StudentApplicationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentApplications
                    .Include(x => x.StreamGroup)
                    .Include(x => x.Stream)
                    .Include(x => x.Subject)
                    .Include(x => x.Subject1)
                    .Include(x => x.Login)
                    .Include(x => x.School)
                    .Include(x => x.Country)
                    .Include(x => x.Country1)
                    .Include(x => x.Gender)
                    .Include(x => x.Nationality)
                    .Include(x => x.Nationality1)
                    .Include(x => x.Nationality2)
                    .Include(x => x.Nationality3)
                    .Include(x => x.Language)
                    .Include(x => x.Language1)
                    .Include(x => x.Relegion)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.ApplicationStatus)
                    .Include(x => x.Cast)
                    .Include(x => x.Class)
                    .Include(x => x.Class1)
                    .Include(x => x.GuardianType)
                    .Include(x => x.GuardianType1)
                    .Include(x => x.GuardianType2)
                    .Include(x => x.GuardianType3)
                    .Include(x => x.PassportDetailMap)
                    .Include(x => x.PassportDetailMap1)
                    .Include(x => x.PassportDetailMap2)
                    .Include(x => x.PassportDetailMap3).ThenInclude(y => y.Country)
                    .Include(x => x.Syllabu)
                    .Include(x => x.StudentCategory)
                    .Include(x => x.Syllabu1)
                    .Include(x => x.VisaDetailMap)
                    .Include(x => x.VisaDetailMap1)
                    .Include(x => x.VisaDetailMap2)
                    .Include(x => x.VisaDetailMap3)
                    .Include(x => x.VolunteerType)
                    .Include(x => x.VolunteerType1)
                    .Include(x => x.StudentApplicationDocumentMaps)
                    .Include(x => x.StudentApplicationOptionalSubjectMaps).ThenInclude(x => x.Subject)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ApplicationIID == IID);

                return ToDTO(entity);
            }
        }

        private void StudentSiblingsInfo(StudentApplication entity, StudentApplicationDTO dto)
        {
            var siblingsDetail = entity.StudentApplicationSiblingMaps.Count > 0 ? entity.StudentApplicationSiblingMaps : null;

            if (siblingsDetail != null)
            {
                foreach (StudentApplicationSiblingMap siblimap in siblingsDetail)
                {
                    dto.StudentSiblings.Add(new KeyValueDTO()
                    {
                        Key = siblimap.SiblingID.ToString(),
                        Value = siblimap.Sibling.AdmissionNumber + "-" + siblimap.Sibling.FirstName + " " + siblimap.Sibling.MiddleName + " " + siblimap.Sibling.LastName,

                    });
                }
            }
        }

        private void OptionalSubjectsInfo(StudentApplication entity, StudentApplicationDTO dto)
        {

            var optionalSubjects = entity.StudentApplicationOptionalSubjectMaps.Count > 0 ? entity.StudentApplicationOptionalSubjectMaps : null;

            if (optionalSubjects != null)
            {
                foreach (StudentApplicationOptionalSubjectMap optionalSub in optionalSubjects)
                {
                    dto.OptionalSubjects.Add(new KeyValueDTO()
                    {
                        Key = optionalSub.SubjectID.ToString(),
                        Value = optionalSub.Subject.SubjectName,

                    });
                }
            }
        }

        private void ParentLoginInfo(long? loginID, StudentApplicationDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var loginDetail = dbContext.Logins.Where(a => a.LoginIID == loginID)
                    .Include(i => i.Parents)
                    .AsNoTracking()
                    .FirstOrDefault();
                //var parentCodeDat = loginDetail != null ? dbContext.Parents.Where(X => X.LoginID == loginDetail.LoginIID).AsNoTracking().FirstOrDefault() : null;
                var parentCodeDat = loginDetail.Parents != null ? loginDetail.Parents.FirstOrDefault() : null;

                if (loginDetail != null)
                {
                    dto.ParentLoginUserID = loginDetail.LoginUserID;
                    dto.ParentLoginEmailID = loginDetail.LoginEmailID;
                    dto.ParentLoginPassword = loginDetail.Password;
                    dto.ParentLoginPasswordSalt = loginDetail.PasswordSalt;
                }
                if (parentCodeDat != null)
                {
                    dto.ParentCode = parentCodeDat.ParentCode;
                    dto.ParentID = parentCodeDat.ParentIID;
                }
            }
        }

        private void StudentPassportInfo(StudentApplication entity, StudentApplicationDTO dto)
        {
            if (entity.PassportDetailMap3 != null)
            {
                dto.StudentPassportDetails.PassportDetailsIID = entity.PassportDetailMap3.PassportDetailsIID;
                dto.StudentPassportDetails.PassportNo = entity.PassportDetailMap3.PassportNo;
                dto.StudentPassportDetails.PassportNoIssueDate = entity.PassportDetailMap3.PassportNoIssueDate;
                dto.StudentPassportDetails.PassportNoExpiryDate = entity.PassportDetailMap3.PassportNoExpiryDate;
                dto.StudentPassportDetails.CountryofIssueID = entity.PassportDetailMap3.CountryofIssueID;
                dto.StudentPassportDetails.StudentCountryofIssue = entity.PassportDetailMap3.CountryofIssueID.HasValue ? entity.PassportDetailMap3.CountryofIssueID.Value.ToString() : null;

                dto.StudentPassportDetails.CountryofIssue = entity.PassportDetailMap3.CountryofIssueID.HasValue ? new KeyValueDTO()
                {
                    Key = dto.StudentPassportDetails.CountryofIssueID.ToString(),
                    Value = entity.PassportDetailMap3.Country.CountryName
                } : new KeyValueDTO();
            }
        }

        private void FatherPassportInfo(StudentApplication entity, StudentApplicationDTO dto)
        {
            if (entity.PassportDetailMap1 != null)
            {
                dto.FatherPassportDetails.PassportDetailsIID = entity.PassportDetailMap1.PassportDetailsIID;
                dto.FatherPassportDetails.PassportNo = entity.PassportDetailMap1.PassportNo;
                dto.FatherPassportDetails.PassportNoIssueDate = entity.PassportDetailMap1.PassportNoIssueDate;
                dto.FatherPassportDetails.PassportNoExpiryDate = entity.PassportDetailMap1.PassportNoExpiryDate;
                dto.FatherPassportDetails.FatherCountryofIssueID = entity.PassportDetailMap1.CountryofIssueID;
                dto.FatherPassportDetails.FatherCountryofIssue = entity.PassportDetailMap1.CountryofIssueID.HasValue ? entity.PassportDetailMap1.CountryofIssueID.Value.ToString() : null;
            }
        }

        private void GuardianPassportInfo(StudentApplication entity, StudentApplicationDTO dto)
        {

            if (entity.PassportDetailMap != null)
            {

                dto.GuardianPassportDetails.PassportDetailsIID = entity.PassportDetailMap.PassportDetailsIID;
                dto.GuardianPassportDetails.GuardianPassportNumber = entity.PassportDetailMap.PassportNo;
                dto.GuardianPassportDetails.PassportNoIssueDate = entity.PassportDetailMap.PassportNoIssueDate;
                dto.GuardianPassportDetails.PassportNoExpiryDate = entity.PassportDetailMap.PassportNoExpiryDate;
                dto.GuardianPassportDetails.CountryofIssueID = entity.PassportDetailMap.CountryofIssueID;
                dto.GuardianPassportDetails.GuardianCountryofIssue = entity.PassportDetailMap.CountryofIssueID.HasValue ? entity.PassportDetailMap.CountryofIssueID.Value.ToString() : null;
            }

        }
        private void MotherPassportInfo(StudentApplication entity, StudentApplicationDTO dto)
        {

            if (entity.PassportDetailMap2 != null)
            {

                dto.MotherPassportDetails.PassportDetailsIID = entity.PassportDetailMap2.PassportDetailsIID;
                dto.MotherPassportDetails.PassportNo = entity.PassportDetailMap2.PassportNo;
                dto.MotherPassportDetails.PassportNoIssueDate = entity.PassportDetailMap2.PassportNoIssueDate;
                dto.MotherPassportDetails.PassportNoExpiryDate = entity.PassportDetailMap2.PassportNoExpiryDate;
                dto.MotherPassportDetails.MotherCountryofIssueID = entity.PassportDetailMap2.CountryofIssueID;
                dto.MotherPassportDetails.MotherCountryofIssue = entity.PassportDetailMap2.CountryofIssueID.HasValue ? entity.PassportDetailMap2.CountryofIssueID.Value.ToString() : null;
            }
        }

        private void StudentVisaInfo(StudentApplication entity, StudentApplicationDTO dto)
        {
            if (entity.VisaDetailMap1 != null)
            {
                dto.StudentVisaDetails.VisaDetailsIID = entity.VisaDetailMap1.VisaDetailsIID;
                dto.StudentVisaDetails.VisaNo = entity.VisaDetailMap1.VisaNo;
                dto.StudentVisaDetails.VisaIssueDate = entity.VisaDetailMap1.VisaIssueDate;
                dto.StudentVisaDetails.VisaExpiryDate = entity.VisaDetailMap1.VisaExpiryDate;
            }
        }

        private void GetStudentApplicationDocuments(StudentApplication entity, StudentApplicationDTO dto)
        {
            var documentsMap = entity.StudentApplicationDocumentMaps.Count > 0 ? entity.StudentApplicationDocumentMaps.FirstOrDefault() : null;

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

        //private void FatherVisaInfo(long studentID, StudentApplicationDTO dto)
        //{
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var repository = new EntityRepository<StudentApplication, dbEduegateSchoolContext>(dbContext);
        //        var FathervisaMap = repository.Get(a => a.ApplicationIID == studentID).LastOrDefault();

        //        if (FathervisaMap != null && FathervisaMap.VisaDetailMap1 != null)
        //        {

        //            dto.FatherVisaDetails.VisaDetailsIID = FathervisaMap.VisaDetailMap1.VisaDetailsIID;
        //            dto.FatherVisaDetails.VisaNo = FathervisaMap.VisaDetailMap1.VisaNo;
        //            dto.FatherVisaDetails.VisaIssueDate = FathervisaMap.VisaDetailMap1.VisaIssueDate;
        //            dto.FatherVisaDetails.VisaExpiryDate = FathervisaMap.VisaDetailMap1.VisaExpiryDate;

        //        }
        //    }

        //}
        //private void MotherVisaInfo(long studentID, StudentApplicationDTO dto)
        //{
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var repository = new EntityRepository<StudentApplication, dbEduegateSchoolContext>(dbContext);
        //        var MothervisaMap = repository.Get(a => a.ApplicationIID == studentID).LastOrDefault();

        //        if (MothervisaMap != null && MothervisaMap.VisaDetailMap2 != null)
        //        {

        //            dto.MotherVisaDetails.VisaDetailsIID = MothervisaMap.VisaDetailMap2.VisaDetailsIID;
        //            dto.MotherVisaDetails.VisaNo = MothervisaMap.VisaDetailMap2.VisaNo;
        //            dto.MotherVisaDetails.VisaIssueDate = MothervisaMap.VisaDetailMap2.VisaIssueDate;
        //            dto.MotherVisaDetails.VisaExpiryDate = MothervisaMap.VisaDetailMap2.VisaExpiryDate;

        //        }
        //    }

        //}

        public StudentApplicationDTO GetApplicationByLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new StudentApplicationDTO();
                var studentApplicationDTO = new StudentApplicationDTO();

                //Get Application details
                var application = dbContext.StudentApplications.Where(a => a.LoginID == loginID)
                    .Include(i => i.Nationality)
                    .Include(i => i.GuardianType)
                    .Include(i => i.GuardianType1)
                    .Include(i => i.GuardianType2)
                    .Include(i => i.VolunteerType)
                    .Include(i => i.VolunteerType1)
                    .Include(i => i.Country)
                    .Include(i => i.PassportDetailMap1).ThenInclude(y => y.Country)
                    .Include(i => i.PassportDetailMap2).ThenInclude(y => y.Country)
                    .OrderByDescending(o => o.ApplicationIID)
                    .AsNoTracking().FirstOrDefault();

                if (application != null)
                {
                    //Get Passport details
                    var fatherPassportDetails = application.PassportDetailMap1;
                    var motherPassportDetails = application.PassportDetailMap2;

                    studentApplicationDTO = new StudentApplicationDTO()
                    {
                        //Father Details
                        FatherFirstName = application.FatherFirstName,
                        FatherMiddleName = application.FatherMiddleName,
                        FatherLastName = application.FatherLastName,
                        MobileNumber = application.MobileNumber,
                        FatherWhatsappMobileNo = application.FatherWhatsappMobileNo,
                        EmailID = application.EmailID,
                        FatherCountryID = application.FatherCountryID,
                        FatherCountry = application.FatherCountryID.HasValue ? application.Nationality.NationalityName : null,
                        FatherOccupation = application.FatherOccupation,
                        FatherPassportNumber = application.FatherPassportNumber,
                        FatherStudentRelationShipID = application.FatherStudentRelationShipID,
                        FatherStudentRelationShip = application.FatherStudentRelationShipID.HasValue ? application.GuardianType.TypeName : null,
                        FatherCompanyName = application.FatherCompanyName,
                        FatherMobileNumberTwo = application.FatherMobileNumberTwo,
                        CanYouVolunteerToHelpOneID = application.CanYouVolunteerToHelpOneID,
                        CanYouVolunteerToHelpOne = application.CanYouVolunteerToHelpOneID.HasValue ? application.VolunteerType.Name : null,
                        PrimaryContactID = application.PrimaryContactID,
                        PrimaryContact = application.PrimaryContactID.HasValue ? application.GuardianType2.TypeName : null,
                        FatherNationalID = application.FatherNationalID,
                        FatherNationalDNoIssueDate = application.FatherNationalDNoIssueDate,
                        FatherNationalDNoExpiryDate = application.FatherNationalDNoExpiryDate,

                        //Mother Details
                        MotherFirstName = application.MotherFirstName,
                        MotherMiddleName = application.MotherMiddleName,
                        MotherLastName = application.MotherLastName,
                        MotherCountryID = application.MotherCountryID,
                        MotherCountry = application.MotherCountryID.HasValue ? application.MotherCountryID.Value.ToString() : null,
                        MotherPassportNumber = application.MotherPassportNumber,
                        MotherEmailID = application.MotherEmailID,
                        MotherMobileNumber = application.MotherMobileNumber,
                        MotherWhatsappMobileNo = application.MotherWhatsappMobileNo,
                        MotherOccupation = application.MotherOccupation,
                        MotherCompanyName = application.MotherCompanyName,
                        MotherStudentRelationShipID = application.MotherStudentRelationShipID,
                        MotherStudentRelationShip = application.MotherStudentRelationShipID.HasValue ? application.GuardianType1.TypeName : null,
                        CanYouVolunteerToHelpTwoID = application.CanYouVolunteerToHelpTwoID,
                        CanYouVolunteerToHelpTwo = application.CanYouVolunteerToHelpTwoID.HasValue ? application.VolunteerType1.Name : null,
                        MotherNationalID = application.MotherNationalID,
                        MotherNationalDNoIssueDate = application.MotherNationalDNoIssueDate,
                        MotherNationaIDNoExpiryDate = application.MotherNationaIDNoExpiryDate,


                        //Address Details
                        BuildingNo = application.BuildingNo,
                        FlatNo = application.FlatNo,
                        StreetNo = application.StreetNo,
                        StreetName = application.StreetName,
                        LocationNo = application.LocationNo,
                        LocationName = application.LocationName,
                        ZipNo = application.ZipNo,
                        City = application.City,
                        PostBoxNo = application.PostBoxNo,
                        CountryID = application.CountryID,
                        Country = application.CountryID.HasValue ? application.Country.CountryName : null,

                    };

                    //Father passport details
                    if (fatherPassportDetails != null)
                    {
                        studentApplicationDTO.FatherPassportDetails.PassportNo = fatherPassportDetails.PassportNo != null ? fatherPassportDetails.PassportNo : null;
                        studentApplicationDTO.FatherPassportDetails.FatherCountryofIssueID = fatherPassportDetails.CountryofIssueID.HasValue ? fatherPassportDetails.CountryofIssueID : null;
                        studentApplicationDTO.FatherPassportDetails.FatherCountryofIssue = fatherPassportDetails.Country.CountryName != null ? fatherPassportDetails.Country.CountryName : null;
                        studentApplicationDTO.FatherPassportDetails.PassportNoIssueDate = fatherPassportDetails.PassportNoIssueDate != null ? fatherPassportDetails.PassportNoIssueDate : null;
                        studentApplicationDTO.FatherPassportDetails.PassportNoExpiryDate = fatherPassportDetails.PassportNoExpiryDate != null ? fatherPassportDetails.PassportNoExpiryDate : null;
                    }

                    //Mother passport details
                    if (motherPassportDetails != null)
                    {
                        studentApplicationDTO.MotherPassportDetails.PassportNo = motherPassportDetails.PassportNo != null ? motherPassportDetails.PassportNo : null;
                        studentApplicationDTO.MotherPassportDetails.MotherCountryofIssueID = motherPassportDetails.CountryofIssueID.HasValue ? motherPassportDetails.CountryofIssueID : null;
                        studentApplicationDTO.MotherPassportDetails.MotherCountryofIssue = motherPassportDetails.Country != null ? motherPassportDetails.Country.CountryName : null;
                        studentApplicationDTO.MotherPassportDetails.PassportNoIssueDate = motherPassportDetails.PassportNoIssueDate != null ? motherPassportDetails.PassportNoIssueDate : null;
                        studentApplicationDTO.MotherPassportDetails.PassportNoExpiryDate = motherPassportDetails.PassportNoExpiryDate != null ? motherPassportDetails.PassportNoExpiryDate : null;
                    }

                    //Get Sibling details
                    var parentDetails = dbContext.Parents.Where(x => x.LoginID == loginID)
                        .Include(i => i.Students)
                        .OrderByDescending(o => o.ParentIID).FirstOrDefault();

                    if (parentDetails != null)
                    {
                        var studentDetails = parentDetails?.Students;

                        foreach (Student siblimap in studentDetails)
                        {
                            studentApplicationDTO.StudentSiblings.Add(new KeyValueDTO()
                            {
                                Key = siblimap == null || siblimap.StudentIID == 0 ? null : siblimap.StudentIID.ToString(),
                                Value = siblimap == null ? null : siblimap.AdmissionNumber + "-" + siblimap.FirstName + " " + siblimap.MiddleName + " " + siblimap.LastName,
                            });
                        }
                    }
                }
                return studentApplicationDTO;

            }
        }

        private StudentApplicationDTO ToDTO(StudentApplication entity)
        {
            var dto = new StudentApplicationDTO();
            try
            {
                dto = new StudentApplicationDTO()
                {
                    ApplicationIID = entity.ApplicationIID,
                    ApplicationNumber = entity.ApplicationNumber,
                    ClassID = entity.ClassID.HasValue ? entity.ClassID : null,
                    Class = entity.ClassID.HasValue ? new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription } : null,
                    ClassName = entity.ClassID.HasValue ? entity.ClassID.Value.ToString() : null,
                    FirstName = entity.FirstName != null ? entity.FirstName : null,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName != null ? entity.LastName : null,
                    GenderID = entity.GenderID,
                    DateOfBirth = entity.DateOfBirth != null ? entity.DateOfBirth : null,
                    CastID = entity.CastID.HasValue ? entity.CastID : null,
                    Cast = entity.CastID.HasValue ? entity.CastID.Value.ToString() : null,
                    //SecoundLanguageID = entity.SecoundLanguageID,
                    //SecoundLanguage = entity.SecoundLanguageID.HasValue ? entity.SecoundLanguageID.Value.ToString() : null,
                    SecoundLanguageID = entity.SecondLangID,
                    SecoundLanguage = entity.SecondLangID.HasValue ? entity.SecondLangID.Value.ToString() : null,
                    ThridLanguageID = entity.ThirdLangID,
                    ThridLanguage = entity.ThirdLangID.HasValue ? entity.ThirdLangID.Value.ToString() : null,
                    //ThridLanguageID = entity.ThridLanguageID,
                    //ThridLanguage = entity.ThridLanguageID.HasValue ? entity.ThridLanguageID.Value.ToString() : null,
                    CanYouVolunteerToHelpOneID = entity.CanYouVolunteerToHelpOneID,
                    CanYouVolunteerToHelpOne = entity.CanYouVolunteerToHelpOneID.HasValue ? entity.CanYouVolunteerToHelpOneID.Value.ToString() : null,
                    CanYouVolunteerToHelpTwoID = entity.CanYouVolunteerToHelpTwoID,
                    CanYouVolunteerToHelpTwo = entity.CanYouVolunteerToHelpTwoID.HasValue ? entity.CanYouVolunteerToHelpTwoID.Value.ToString() : null,
                    CommunityID = entity.CommunityID,
                    Community = entity.CommunityID.HasValue ? entity.CommunityID.Value.ToString() : null,
                    RelegionID = entity.RelegionID,
                    StreamGroupID = entity.StreamGroupID.HasValue ? entity.StreamGroupID : null,
                    StreamGroup = entity.StreamGroupID.HasValue ? new KeyValueDTO() { Key = entity.StreamGroupID.ToString(), Value = entity.StreamGroup.Description } : null,
                    StreamID = entity.StreamID.HasValue ? entity.StreamID : null,
                    Stream = entity.StreamID.HasValue ? new KeyValueDTO() { Key = entity.StreamID.ToString(), Value = entity.Stream.Description } : null,
                    MobileNumber = entity.MobileNumber != null ? entity.MobileNumber : null,
                    FatherWhatsappMobileNo = entity.FatherWhatsappMobileNo,
                    EmailID = entity.EmailID != null ? entity.EmailID : null,
                    ProfileContentID = entity.ProfileContentID,
                    //ParmenentAddress = entity.ParmenentAddress,
                    //CurrentAddress = entity.CurrentAddress,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    SchoolID = entity.SchoolID.HasValue ? entity.SchoolID : null,
                    CategoryID = entity.StudentCategoryID,
                    FatherCountry = entity.FatherCountryID.HasValue ? entity.FatherCountryID.ToString() : null,
                    FatherFirstName = entity.FatherFirstName != null ? entity.FatherFirstName : null,
                    FatherLastName = entity.FatherLastName,
                    FatherMiddleName = entity.FatherMiddleName != null ? entity.FatherMiddleName : null,
                    FatherNationalID = entity.FatherNationalID,
                    FatherPassportNumber = entity.FatherPassportNumber != null ? entity.FatherPassportNumber : null,
                    MotherCountry = entity.MotherCountryID.HasValue ? entity.MotherCountryID.Value.ToString() : null,
                    MotherFirstName = entity.MotherFirstName != null ? entity.MotherFirstName : null,
                    MotherLastName = entity.MotherLastName,
                    MotherMiddleName = entity.MotherMiddleName != null ? entity.MotherMiddleName : null,
                    MotherNationalID = entity.MotherNationalID,
                    MotherPassportNumber = entity.MotherPassportNumber,
                    Nationality = entity.NationalityID.HasValue ? entity.NationalityID.Value.ToString() : null,
                    ////TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                    FatherCountryID = entity.FatherCountryID,
                    FatherOccupation = entity.FatherOccupation,
                    MotherCountryID = entity.MotherCountryID,
                    ApplicationStatusID = entity.ApplicationStatusID.HasValue ? entity.ApplicationStatusID : null,
                    ApplicationStatusDescription = entity.ApplicationStatusID != null ? entity.ApplicationStatus.Description : "pending",
                    //ApplicationTypeID = entity.ApplicationTypeID.HasValue ? entity.ApplicationTypeID : null,
                    LoginID = entity.LoginID,
                    PreviousSchoolName = entity.PreviousSchoolName,
                    StudentPassportNo = entity.StudentPassportNo != null ? entity.StudentPassportNo : null,
                    StudentNationalID = entity.StudentNationalID != null ? entity.StudentNationalID : null,
                    MotherEmailID = entity.MotherEmailID,
                    MotherMobileNumber = entity.MotherMobileNumber,
                    MotherWhatsappMobileNo = entity.MotherWhatsappMobileNo,
                    MotherOccupation = entity.MotherOccupation,
                    PreviousSchoolSyllabusID = entity.PreviousSchoolSyllabusID,
                    PreviousSchoolSyllabus = entity.PreviousSchoolSyllabusID.HasValue ? entity.PreviousSchoolSyllabusID.ToString() : null,
                    CurriculamID = entity.CurriculamID,
                    Curriculam = entity.Syllabu1 != null ? entity.Syllabu1.SyllabusDescription : null,
                    PrimaryContactID = entity.PrimaryContactID,
                    PrimaryContact = entity.PrimaryContactID.HasValue ? entity.PrimaryContactID.Value.ToString() : null,
                    PreviousSchoolAcademicYear = entity.PreviousSchoolAcademicYear,
                    FatherStudentRelationShipID = entity.FatherStudentRelationShipID,
                    MotherStudentRelationShipID = entity.MotherStudentRelationShipID,
                    SchoolAcademicyearID = entity.SchoolAcademicyearID,
                    AcademicyearID = entity.SchoolAcademicyearID,
                    SchoolAcademicyear = entity.AcademicYear != null ? entity.AcademicYear.Description + " " + '(' + entity.AcademicYear.AcademicYearCode + ')' : null,
                    Academicyear = entity.AcademicYear != null ? entity.AcademicYear.Description + " " + '(' + entity.AcademicYear.AcademicYearCode + ')' : null,
                    PreviousSchoolClassCompletedID = entity.PreviousSchoolClassCompletedID,
                    PreviousSchoolClassCompleted = entity.PreviousSchoolClassCompletedID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.PreviousSchoolClassCompletedID.ToString(),
                        Value = entity.Class1.ClassDescription
                    } : null,
                    BuildingNo = entity.BuildingNo,
                    FlatNo = entity.FlatNo,
                    StreetNo = entity.StreetNo,
                    StreetName = entity.StreetName,
                    LocationNo = entity.LocationNo,
                    LocationName = entity.LocationName,
                    ZipNo = entity.ZipNo,
                    PostBoxNo = entity.PostBoxNo,
                    FatherCompanyName = entity.FatherCompanyName,
                    MotherCompanyName = entity.MotherCompanyName,
                    PreviousSchoolAddress = entity.PreviousSchoolAddress,
                    City = entity.City,
                    CountryID = entity.CountryID,
                    Country = entity.CountryID.HasValue ? entity.CountryID.Value.ToString() : null,
                    AdhaarCardNo = entity.AdhaarCardNo,
                    StudentNationalIDNoIssueDate = entity.StudentNationalIDNoIssueDate,
                    StudentNationalIDNoExpiryDate = entity.StudentNationalIDNoExpiryDate,
                    IsMinority = entity.IsMinority,
                    IsOnlyChildofParent = entity.IsOnlyChildofParent,
                    BloodGroupID = entity.BloodGroupID,
                    BloodGroup = entity.BloodGroupID.HasValue ? entity.BloodGroupID.Value.ToString() : null,
                    StudentCoutryOfBrith = entity.StudentCoutryOfBrithID.HasValue ? entity.StudentCoutryOfBrithID.Value.ToString() : null,
                    FatherMobileNumberTwo = entity.FatherMobileNumberTwo,
                    FatherNationalDNoIssueDate = entity.FatherNationalDNoIssueDate,
                    FatherNationalDNoExpiryDate = entity.FatherNationalDNoExpiryDate,
                    MotherNationalDNoIssueDate = entity.MotherNationalDNoIssueDate,
                    MotherNationaIDNoExpiryDate = entity.MotherNationaIDNoExpiryDate,
                    IsStudentStudiedBefore = entity.IsStudentStudiedBefore,
                    ProspectNumber = entity.ProspectNumber != null ? entity.ProspectNumber : null,
                    Remarks = entity.Remarks,
                    StudentNationality = entity.NationalityID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.NationalityID.ToString(),
                        Value = entity.Nationality3?.NationalityName
                    } : new KeyValueDTO(),
                    CoutryOfBrith = entity.StudentCoutryOfBrithID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.StudentCoutryOfBrithID.ToString(),
                        Value = entity.Country1?.CountryName
                    } : new KeyValueDTO(),
                    GuardianVisaDetailNoID = entity.GuardianVisaDetailNoID,
                    GuardianPassportDetailNoID = entity.GuardianPassportDetailNoID,
                    GuardianFirstName = entity.GuardianFirstName,
                    GuardianMiddleName = entity.GuardianMiddleName,
                    GuardianLastName = entity.GuardianLastName,
                    GuardianStudentRelationShipID = entity.GuardianStudentRelationShipID,
                    GuardianStudentRelationShip = entity.GuardianStudentRelationShipID.HasValue ? entity.GuardianStudentRelationShipID.Value.ToString() : null,
                    GuardianOccupation = entity.GuardianOccupation,
                    GuardianCompanyName = entity.GuardianCompanyName,
                    GuardianMobileNumber = entity.GuardianMobileNumber,
                    GuardianWhatsappMobileNo = entity.GuardianWhatsappMobileNo,
                    GuardianEmailID = entity.GuardianEmailID,
                    GuardianNationalityID = entity.GuardianNationalityID,
                    GuardianNationality = entity.GuardianNationalityID.HasValue ? entity.GuardianNationalityID.Value.ToString() : null,
                    GuardianNationalID = entity.GuardianNationalID,
                    GuardianNationalIDNoIssueDate = entity.GuardianNationalIDNoIssueDate,
                    GuardianNationalIDNoExpiryDate = entity.GuardianNationalIDNoExpiryDate,
                    //Prospectus = new ProspectusDTO()
                    //{
                    //    ProsNo  = entity.ProspectNumber,
                    //    ProsFee = entity.ProspectFee,
                    //},
                    onStreams = entity.StreamID.HasValue ? true : false,

                };
                StudentPassportInfo(entity, dto);
                FatherPassportInfo(entity, dto);
                MotherPassportInfo(entity, dto);
                StudentVisaInfo(entity, dto);
                GetStudentApplicationDocuments(entity, dto);
                //FatherVisaInfo(dto.ApplicationIID, dto);
                //MotherVisaInfo(dto.ApplicationIID, dto);
                StudentSiblingsInfo(entity, dto);
                OptionalSubjectsInfo(entity, dto);
                //dto.AgeCriteriaWarningMsg = GetAgeCriteriaDetails(dto.ClassID, dto.AcademicyearID, dto.SchoolID, dto.DateOfBirth).AgeCriteriaWarningMsg;
                //For purpose of Studentpicking fill Parent login details
                if (dto.LoginID.HasValue)
                {
                    ParentLoginInfo(dto.LoginID, dto);
                }
                GuardianPassportInfo(entity, dto);
            }
            catch { }
            return dto;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            var toDto = dto as StudentApplicationDTO;
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            String emailBody = "";
            String emailSubject = "";

            if (toDto.ClassID == 0 || toDto.ClassID == null)
            {
                throw new Exception("Please Select Class!");
            }

            //if (toDto.onStreams == true && toDto.OptionalSubjects.Count >= 3 || toDto.onStreams == true && toDto.OptionalSubjects.Count <= 1)
            //{
            //    throw new Exception("Please select Two Optional Subjects !");
            //}

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var ageData = dbContext.AgeCriterias.Where(X => X.ClassID == toDto.ClassID && X.SchoolID == toDto.SchoolID && X.AcademicYearID == toDto.SchoolAcademicyearID).AsNoTracking().FirstOrDefault();

                if (ageData != null)
                {
                    if (toDto.DateOfBirth < ageData.BirthFrom || toDto.DateOfBirth > ageData.BirthTo)
                    {
                        throw new Exception("The selected DOB doesn't meet the age criteria set for this class!! Eligible only for " + " " + ageData.BirthFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + " to " + ageData.BirthTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + " " + "For Respective Classes");
                    }
                }
            }

            //// Date Passport Different Check
            //if (toDto.StudentPassportDetails.PassportNoIssueDate >= toDto.StudentPassportDetails.PassportNoExpiryDate)
            //{
            //    throw new Exception("Select Student Passport Date Properlly!!");
            //}
            //// Date NationalID Different Check
            //if (toDto.StudentNationalIDNoIssueDate >= toDto.StudentNationalIDNoExpiryDate)
            //{
            //    throw new Exception("Select Student NationalID Date Properlly!!");
            //}
            //// Date Visa Different Check
            //if (toDto.StudentVisaDetails.VisaIssueDate >= toDto.StudentVisaDetails.VisaExpiryDate)
            //{
            //    throw new Exception("Select Student visa Date Properlly!!");
            //}
            //// Date FatherPassport Different Check
            //if (toDto.FatherPassportDetails.PassportNoIssueDate >= toDto.FatherPassportDetails.PassportNoExpiryDate)
            //{
            //    throw new Exception("Select Father Passport Date Properlly!!");
            //}
            //// Date FatherVisa Different Check
            //if (toDto.FatherVisaDetails.VisaIssueDate >= toDto.FatherVisaDetails.VisaIssueDate)
            //{
            //    throw new Exception("Select Father Visa Date Properlly!!");
            //}
            //// Date FatherNational Different Check
            //if (toDto.FatherNationalDNoIssueDate >= toDto.FatherNationalDNoExpiryDate)
            //{
            //    throw new Exception("Select Father NationalID Date Properlly!!");
            //}
            //// Date MotherPassport Different Check
            //if (toDto.MotherPassportDetails.PassportNoIssueDate >= toDto.MotherPassportDetails.PassportNoExpiryDate)
            //{
            //    throw new Exception("Select Mother Passport Date Properlly!!");
            //}
            //// Date MotherVisa Different Check
            //if (toDto.MotherVisaDetails.VisaIssueDate >= toDto.MotherVisaDetails.VisaIssueDate)
            //{
            //    throw new Exception("Select Mother Visa Date Properlly!!");
            //}
            //// Date MotherNational Different Check
            //if (toDto.MotherNationalDNoIssueDate >= toDto.MotherNationaIDNoExpiryDate)
            //{
            //    throw new Exception("Select Mother NationalID Date Properlly!!");
            //}
            var errorMessage = string.Empty;

            ////validate first
            //foreach (var field in validationFields)
            //{
            //    var isValid = ValidateField(toDto, field);

            //    if (isValid.Key.Equals("true"))
            //    {
            //        errorMessage = string.Concat(errorMessage, "-", isValid.Value, "<br>");
            //    }
            //}

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            if (toDto.ApplicationIID == 0)
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("ApplicationNumber", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'ApplicationNumber'");
                }
            }

            if (toDto.ClassID != null)
            {
                toDto.ClassName = ClassDetail(toDto.ClassID).ClassName;
            }

            //convert the dto to entity and pass to the repository.
            var entity = new StudentApplication()
            {
                ApplicationIID = toDto.ApplicationIID,
                //ApplicationNumber = toDto.ApplicationNumber,
                ApplicationNumber = toDto.ApplicationIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.ApplicationNumber,
                ClassID = toDto.ClassID,
                FirstName = toDto.FirstName,
                MiddleName = toDto.MiddleName,
                LastName = toDto.LastName,
                GenderID = toDto.GenderID,
                DateOfBirth = toDto.DateOfBirth,
                //CastID = !string.IsNullOrEmpty(toDto.Cast) || toDto.Cast != "?" ? byte.Parse(toDto.Cast) : (byte?)null,
                CastID = toDto.CastID == null ? null : toDto.CastID,
                //SecoundLanguageID = toDto.SecoundLanguageID == null ? null : toDto.SecoundLanguageID,
                //SecoundLanguageID = !string.IsNullOrEmpty(toDto.SecoundLanguage) ? int.Parse(toDto.SecoundLanguage) : (int?)null,
                SecondLangID = toDto.SecoundLanguageID == null ? null : toDto.SecoundLanguageID,
                ThirdLangID = toDto.ThridLanguageID == null ? null : toDto.ThridLanguageID,
                //ThridLanguageID = toDto.ThridLanguageID == null ? null : toDto.ThridLanguageID,
                //ThridLanguageID = !string.IsNullOrEmpty(toDto.ThridLanguage) ? int.Parse(toDto.ThridLanguage) : (int?)null,
                CanYouVolunteerToHelpOneID = toDto.CanYouVolunteerToHelpOneID.HasValue ? toDto.CanYouVolunteerToHelpOneID : null,
                CanYouVolunteerToHelpTwoID = toDto.CanYouVolunteerToHelpTwoID.HasValue ? toDto.CanYouVolunteerToHelpTwoID : null,
                //CommunityID = !string.IsNullOrEmpty(toDto.Community) || toDto.Community != "?" ? byte.Parse(toDto.Community) : (byte?)null,
                CommunityID = toDto.CommunityID == null ? null : toDto.CommunityID,
                RelegionID = toDto.RelegionID == null ? null : toDto.RelegionID,
                StreamID = toDto.StreamID == null ? null : toDto.StreamID,
                StreamGroupID = toDto.StreamGroupID != null ? toDto.StreamGroupID : null,
                MobileNumber = toDto.MobileNumber,
                FatherWhatsappMobileNo = toDto.FatherWhatsappMobileNo,
                EmailID = toDto.EmailID.ToLower(),
                ProfileContentID = toDto.ProfileContentID,
                //ParmenentAddress = toDto.ParmenentAddress,
                //CurrentAddress = toDto.CurrentAddress,
                LoginID = _context != null ? _context.LoginID : (long?)null,
                SchoolID = toDto.SchoolID,
                StudentCategoryID = toDto.CategoryID,
                FatherCountryID = toDto.FatherCountryID != null ? toDto.FatherCountryID : null,
                FatherFirstName = toDto.FatherFirstName,
                FatherLastName = toDto.FatherLastName,
                FatherMiddleName = toDto.FatherMiddleName,
                FatherNationalID = toDto.FatherNationalID,
                FatherPassportNumber = toDto.FatherPassportNumber,
                MotherCountryID = toDto.MotherCountryID != null ? toDto.MotherCountryID : null,
                MotherFirstName = toDto.MotherFirstName,
                MotherLastName = toDto.MotherLastName,
                MotherMiddleName = toDto.MotherMiddleName,
                MotherNationalID = toDto.MotherNationalID,
                MotherPassportNumber = toDto.MotherPassportNumber,
                NationalityID = !string.IsNullOrEmpty(toDto.Nationality) ? int.Parse(toDto.Nationality) : (int?)null,
                FatherOccupation = toDto.FatherOccupation,
                ApplicationStatusID = toDto.ApplicationIID == 0 ? 1 : toDto.ApplicationStatusID,
                //ApplicationTypeID = toDto.ApplicationTypeID,
                PreviousSchoolName = toDto.PreviousSchoolName,
                StudentPassportNo = toDto.StudentPassportNo,
                StudentNationalID = toDto.StudentNationalID,
                MotherEmailID = toDto.MotherEmailID != null ? toDto.MotherEmailID.ToLower() : null,
                MotherMobileNumber = toDto.MotherMobileNumber,
                MotherWhatsappMobileNo = toDto.MotherWhatsappMobileNo,
                MotherOccupation = toDto.MotherOccupation,
                PreviousSchoolSyllabusID = toDto.PreviousSchoolSyllabusID == null ? null : toDto.PreviousSchoolSyllabusID,
                //PreviousSchoolSyllabusID = !string.IsNullOrEmpty(toDto.PreviousSchoolSyllabus) && toDto.PreviousSchoolSyllabus != "?" ? byte.Parse(toDto.PreviousSchoolSyllabus) : (byte?)null,
                CurriculamID = toDto.CurriculamID == null ? null : toDto.CurriculamID,
                //CurriculamID = !string.IsNullOrEmpty(toDto.Curriculam) && toDto.Curriculam != "?" ? byte.Parse(toDto.Curriculam) : (byte?)null,
                PreviousSchoolAcademicYear = toDto.PreviousSchoolAcademicYear,
                FatherStudentRelationShipID = !string.IsNullOrEmpty(toDto.FatherStudentRelationShip) ? byte.Parse(toDto.FatherStudentRelationShip) : (byte?)null,
                MotherStudentRelationShipID = !string.IsNullOrEmpty(toDto.MotherStudentRelationShip) ? byte.Parse(toDto.MotherStudentRelationShip) : (byte?)null,
                SchoolAcademicyearID = toDto.SchoolAcademicyearID,
                PreviousSchoolClassCompletedID = toDto.PreviousSchoolClassCompletedID == null ? null : toDto.PreviousSchoolClassCompletedID,
                BuildingNo = toDto.BuildingNo,
                FlatNo = toDto.FlatNo,
                StreetNo = toDto.StreetNo,
                StreetName = toDto.StreetName,
                LocationNo = toDto.LocationNo,
                LocationName = toDto.LocationName,
                ZipNo = toDto.ZipNo,
                PostBoxNo = toDto.PostBoxNo,
                FatherCompanyName = toDto.FatherCompanyName,
                MotherCompanyName = toDto.MotherCompanyName,
                PreviousSchoolAddress = toDto.PreviousSchoolAddress,
                City = toDto.City,
                //CountryID = !string.IsNullOrEmpty(toDto.Country) ? int.Parse(toDto.Country) : (int?)null,
                CountryID = toDto.CountryID == null ? null : toDto.CountryID,
                StudentPassportDetailNoID = toDto.StudentPassportDetails.PassportDetailsIID,
                FatherPassportDetailNoID = toDto.FatherPassportDetails.PassportDetailsIID,
                MotherPassportDetailNoID = toDto.MotherPassportDetails.PassportDetailsIID,
                StudentVisaDetailNoID = toDto.StudentVisaDetails.VisaDetailsIID,
                //FatherVisaDetailNoID = toDto.FatherVisaDetails.VisaDetailsIID,
                //MotherVisaDetailNoID = toDto.MotherVisaDetails.VisaDetailsIID,
                AdhaarCardNo = toDto.AdhaarCardNo,
                StudentNationalIDNoIssueDate = toDto.StudentNationalIDNoIssueDate,
                StudentNationalIDNoExpiryDate = toDto.StudentNationalIDNoExpiryDate,
                IsMinority = toDto.IsMinority,
                IsOnlyChildofParent = toDto.IsOnlyChildofParent,
                //BloodGroupID = !string.IsNullOrEmpty(toDto.BloodGroup) ? int.Parse(toDto.BloodGroup) : (int?)null,
                BloodGroupID = toDto.BloodGroupID == null ? null : toDto.BloodGroupID,
                StudentCoutryOfBrithID = !string.IsNullOrEmpty(toDto.StudentCoutryOfBrith) ? int.Parse(toDto.StudentCoutryOfBrith) : (int?)null,
                FatherMobileNumberTwo = toDto.FatherMobileNumberTwo,
                FatherNationalDNoIssueDate = toDto.FatherNationalDNoIssueDate,
                FatherNationalDNoExpiryDate = toDto.FatherNationalDNoExpiryDate,
                MotherNationalDNoIssueDate = toDto.MotherNationalDNoIssueDate,
                MotherNationaIDNoExpiryDate = toDto.MotherNationaIDNoExpiryDate,
                IsStudentStudiedBefore = toDto.IsStudentStudiedBefore,
                GuardianNationalityID = toDto.GuardianNationalityID != null ? toDto.GuardianNationalityID : null,
                GuardianVisaDetailNoID = toDto.GuardianVisaDetailNoID,
                GuardianPassportDetailNoID = toDto.GuardianPassportDetailNoID,
                GuardianFirstName = toDto.GuardianFirstName,
                GuardianMiddleName = toDto.GuardianMiddleName,
                GuardianLastName = toDto.GuardianLastName,
                GuardianStudentRelationShipID = toDto.GuardianStudentRelationShipID,
                GuardianOccupation = toDto.GuardianOccupation,
                GuardianCompanyName = toDto.GuardianCompanyName,
                GuardianMobileNumber = toDto.GuardianMobileNumber,
                GuardianWhatsappMobileNo = toDto.GuardianWhatsappMobileNo,
                GuardianEmailID = toDto.GuardianEmailID.ToLower(),
                GuardianNationalID = toDto.GuardianNationalID,
                GuardianNationalIDNoIssueDate = toDto.GuardianNationalIDNoIssueDate,
                GuardianNationalIDNoExpiryDate = toDto.GuardianNationalIDNoExpiryDate,
                //PrimaryContactID = !string.IsNullOrEmpty(toDto.PrimaryContact) ? byte.Parse(toDto.PrimaryContact) : (byte?)null,
                PrimaryContactID = toDto.PrimaryContactID == null ? null : toDto.PrimaryContactID,
                //ProspectNumber = toDto.Prospectus != null || toDto.Prospectus.ProsNo != null ? toDto.Prospectus.ProsNo : null,
                //ProspectFee = toDto.Prospectus != null || toDto.Prospectus.ProsFee != null ? toDto.Prospectus.ProsFee : null,
                ProspectNumber = toDto.ProspectNumber,
                Remarks = toDto.Remarks,
                CreatedBy = toDto.ApplicationIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ApplicationIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ApplicationIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ApplicationIID > 0 ? DateTime.Now : dto.UpdatedDate,
                ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            try
            {
                if (entity.ApplicationStatusID == null)
                {
                    entity.ApplicationStatusID = 1;
                }
            }
            catch { }

            entity.PassportDetailMap3 = new PassportDetailMap()
            {
                PassportDetailsIID = toDto.StudentPassportDetails.PassportDetailsIID,
                PassportNo = toDto.StudentPassportDetails.PassportNo,
                CountryofIssueID = toDto.StudentPassportDetails.CountryofIssueID,
                PassportNoIssueDate = toDto.StudentPassportDetails.PassportNoIssueDate,
                PassportNoExpiryDate = toDto.StudentPassportDetails.PassportNoExpiryDate,

            };

            entity.PassportDetailMap = new PassportDetailMap()
            {
                PassportDetailsIID = toDto.GuardianPassportDetails.PassportDetailsIID,
                PassportNo = toDto.GuardianPassportDetails.GuardianPassportNumber,
                CountryofIssueID = toDto.GuardianPassportDetails.CountryofIssueID,
                PassportNoIssueDate = toDto.GuardianPassportDetails.PassportNoIssueDate,
                PassportNoExpiryDate = toDto.GuardianPassportDetails.PassportNoExpiryDate,

            };

            entity.PassportDetailMap1 = new PassportDetailMap()
            {
                PassportDetailsIID = toDto.FatherPassportDetails.PassportDetailsIID,
                PassportNo = toDto.FatherPassportDetails.PassportNo,
                CountryofIssueID = toDto.FatherPassportDetails.FatherCountryofIssueID,
                PassportNoIssueDate = toDto.FatherPassportDetails.PassportNoIssueDate,
                PassportNoExpiryDate = toDto.FatherPassportDetails.PassportNoExpiryDate,

            };
            entity.PassportDetailMap2 = new PassportDetailMap()
            {
                PassportDetailsIID = toDto.MotherPassportDetails.PassportDetailsIID,
                PassportNo = toDto.MotherPassportDetails.PassportNo,
                CountryofIssueID = toDto.MotherPassportDetails.MotherCountryofIssueID,
                PassportNoIssueDate = toDto.MotherPassportDetails.PassportNoIssueDate,
                PassportNoExpiryDate = toDto.MotherPassportDetails.PassportNoExpiryDate,

            };

            entity.VisaDetailMap1 = new VisaDetailMap()
            {
                VisaDetailsIID = toDto.StudentVisaDetails.VisaDetailsIID,
                VisaNo = toDto.StudentVisaDetails.VisaNo,
                VisaIssueDate = toDto.StudentVisaDetails.VisaIssueDate,
                VisaExpiryDate = toDto.StudentVisaDetails.VisaExpiryDate,

            };

            //entity.VisaDetailMap1 = new VisaDetailMap()
            //{
            //    VisaDetailsIID = toDto.FatherVisaDetails.VisaDetailsIID,
            //    VisaNo = toDto.FatherVisaDetails.VisaNo,
            //    VisaIssueDate = toDto.FatherVisaDetails.VisaIssueDate,
            //    VisaExpiryDate = toDto.FatherVisaDetails.VisaExpiryDate,
            //};

            //entity.VisaDetailMap2 = new VisaDetailMap()
            //{
            //    VisaDetailsIID = toDto.MotherVisaDetails.VisaDetailsIID,
            //    VisaNo = toDto.MotherVisaDetails.VisaNo,
            //    VisaIssueDate = toDto.MotherVisaDetails.VisaIssueDate,
            //    VisaExpiryDate = toDto.MotherVisaDetails.VisaExpiryDate,

            //};

            //toDto.StudentApplicationSiblingMaps = new List<StudentApplicationSiblingMap>();
            entity.StudentApplicationDocumentMaps = new List<StudentApplicationDocumentMap>();
            if (toDto.StudentDocUploads != null)
            {
                entity.StudentApplicationDocumentMaps.Add(new StudentApplicationDocumentMap()
                {
                    ApplicationDocumentIID = toDto.StudentDocUploads.ApplicationDocumentIID,
                    ApplicationID = toDto.StudentDocUploads.ApplicationID.HasValue ? toDto.StudentDocUploads.ApplicationID : entity.ApplicationIID,
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
                    CreatedBy = toDto.StudentDocUploads.ApplicationDocumentIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StudentDocUploads.ApplicationDocumentIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StudentDocUploads.ApplicationDocumentIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StudentDocUploads.ApplicationDocumentIID > 0 ? DateTime.Now : dto.UpdatedDate,

                });
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var mapEntity = toDto.ApplicationIID != 0 ? dbContext.StudentApplicationOptionalSubjectMaps.Where(X => X.ApplicationID == toDto.ApplicationIID).AsNoTracking().ToList() : null;
                if (mapEntity != null)
                {
                    foreach (var delDat in mapEntity)
                    {
                        if (mapEntity != null || mapEntity.Count > 0)
                        {
                            dbContext.StudentApplicationOptionalSubjectMaps.Remove(delDat);
                        }
                        dbContext.SaveChanges();
                    }
                }

                if (toDto.OptionalSubjects.Count > 0)
                {
                    foreach (KeyValueDTO subj in toDto.OptionalSubjects)
                    {
                        entity.StudentApplicationOptionalSubjectMaps.Add(new StudentApplicationOptionalSubjectMap()
                        {
                            ApplicationID = toDto.ApplicationIID,
                            SubjectID = int.Parse(subj.Key),
                            StreamID = toDto.StreamID,
                        });
                    }
                }
            }

            if (toDto.StudentSiblings.Count > 0)
            {
                foreach (KeyValueDTO sibli in toDto.StudentSiblings)
                {
                    entity.StudentApplicationSiblingMaps.Add(new StudentApplicationSiblingMap()
                    {
                        //StudentID = toDto.StudentIID,
                        ApplicationID = toDto.ApplicationIID,
                        SiblingID = long.Parse(sibli.Key),
                    });
                }
            }
            else
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    //Get Sibling details
                    var parentDetails = dbContext.Parents.Where(x => x.LoginID == toDto.LoginID)
                        .Include(i => i.Students)
                        .OrderByDescending(o => o.ParentIID)
                        .AsNoTracking().FirstOrDefault();

                    if (parentDetails != null)
                    {
                        if (parentDetails.LoginID != null)
                        {
                            var studentDetails = parentDetails?.Students;
                            if (studentDetails != null)
                            {
                                foreach (Student siblimap in studentDetails)
                                {
                                    entity.StudentApplicationSiblingMaps.Add(new StudentApplicationSiblingMap()
                                    {
                                        //StudentID = toDto.StudentIID,
                                        ApplicationID = toDto.ApplicationIID,
                                        SiblingID = siblimap.StudentIID,
                                    });
                                }
                            }
                        }
                    }
                }
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //Duplicate check for Parentportal and ERP
                var checkDuplicate1 = toDto.ApplicationIID == 0 ? dbContext.StudentApplications.Where(x => x.DateOfBirth == toDto.DateOfBirth && x.StudentNationalID == toDto.StudentNationalID).AsNoTracking().FirstOrDefault() : null;
                if (checkDuplicate1 != null)
                {
                    throw new Exception("Please Check! whether the application is submitted before for this student.");
                }

                var existingEntity = dbContext.StudentApplications.Where(x => x.ApplicationIID == entity.ApplicationIID).AsNoTracking().FirstOrDefault();

                entity.LoginID = existingEntity == null ? this._context.LoginID : existingEntity.LoginID;
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var oldSiblingMaps = dbContext.StudentApplicationSiblingMaps.Where(x => x.ApplicationID == toDto.ApplicationIID).AsNoTracking().ToList();

                if (oldSiblingMaps != null || oldSiblingMaps.Count > 0)
                {
                    dbContext.StudentApplicationSiblingMaps.RemoveRange(oldSiblingMaps);
                }

                
                dbContext.SaveChanges();
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.StudentApplications.Add(entity);
                if (entity.ApplicationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    UpdateLeadStatusID(entity.ProspectNumber);

                    if (entity.ApplicationStatusID == 1)
                    {
                        emailBody = @"<br/><p style='font-family:Helvetica;font-size:1rem; font-weight:bold;'><b>Thank you for choosing Pearl School</b><br /><b>Your application has been submitted successfully.Incase there is a vacancy the admissions team will contact you.</b></p> <table align='left'> <tr > <th colspan='2' align='left' > Application Details:</th > </tr > <tr> <td>Student Name :</td> <td>" + toDto.FirstName + " " + toDto.MiddleName + " " + toDto.LastName + @"</td> </tr> <tr> <td> Application No:</td> <td>" + entity.ApplicationNumber + @"</td> </tr> <tr> <td>Applied Date :</td> <td>" + entity.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + @"</td> </tr> <tr> <td>Admission for:</td> <td>" + toDto.ClassName + @"</td> </tr> <tr> <td> Mobile No :</td> <td>" + toDto.MobileNumber + @"</td> </tr> </table><br /><br /><br /><br /><br /><br /><br /><p>Regards,<br />Podar Pearl School</p><br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                        emailSubject = "Automatic reply: Received your application";
                    }
                }
                else
                {
                    if (entity.PassportDetailMap3.PassportDetailsIID == 0)
                    {
                        dbContext.Entry(entity.PassportDetailMap3).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity.PassportDetailMap3).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    if (entity.PassportDetailMap2.PassportDetailsIID == 0)
                    {
                        dbContext.Entry(entity.PassportDetailMap2).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity.PassportDetailMap2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    if (entity.PassportDetailMap1.PassportDetailsIID == 0)
                    {
                        dbContext.Entry(entity.PassportDetailMap1).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity.PassportDetailMap1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    if (entity.PassportDetailMap.PassportDetailsIID == 0)
                    {
                        dbContext.Entry(entity.PassportDetailMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity.PassportDetailMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    if (entity.VisaDetailMap1.VisaDetailsIID == 0)
                    {
                        dbContext.Entry(entity.VisaDetailMap1).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity.VisaDetailMap1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    foreach(var map in entity.StudentApplicationDocumentMaps)
                    {
                        if (map.ApplicationDocumentIID == 0)
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    foreach (var sibli in entity.StudentApplicationSiblingMaps)
                    {
                        var sibliMap = dbContext.StudentApplicationSiblingMaps.Where(a => a.SiblingID == sibli.SiblingID && a.ApplicationID == toDto.ApplicationIID)
                            .OrderByDescending(o => o.StudentApplicationSiblingMapIID)
                            .AsNoTracking().FirstOrDefault();

                        if (sibliMap != null && sibliMap.StudentApplicationSiblingMapIID != 0)
                        {
                            dbContext.Entry(sibliMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(sibli).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    //foreach (var map in entity.StudentApplicationOptionalSubjectMaps)
                    //{
                    //    if (map.StudentApplicationOptionalSubjectMapIID == 0)
                    //    {
                    //        dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //    }
                    //    else
                    //    {
                    //        dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //    }
                    //}

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                }
            }

            var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_APPLICATION_WORKFLOW_ID");

            Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(long.Parse(settingValue), entity.ApplicationIID);

            var emaildata = new EmailNotificationDTO();
            try
            {

                //if (entity.ApplicationStatusID == 3)
                //{
                //    emailBody = @"<br/><p style='font-family:Helvetica;font-size:1rem; font-weight:bold;'>Congratulations!! <br /> Your Candidate Application taken into Acceptance.we will contact you soon...</p><br /><p>Regards,<br />Pearl School</p><br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                //    emailSubject = "Application Status";
                //}

                //if (entity.ApplicationStatusID == 7)
                //{
                //    emailBody = @"<br/><p style='font-family:Helvetica;font-size:1rem; font-weight:bold;'>Sorry!! <br /> Your Candidate Application Rejected. for further please contact our Office...</p><br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                //    emailSubject = "Application Status";
                //}

                string schoolShortName = string.Empty;
                if (entity.SchoolID.HasValue)
                {
                    schoolShortName = entity.School?.SchoolShortName?.ToLower();
                    if (string.IsNullOrEmpty(schoolShortName))
                    {
                        var schoolData = new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(entity.SchoolID.Value);

                        schoolShortName = schoolData?.SchoolShortName?.ToLower();
                    }
                }

                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toDto.EmailID, emailBody);

                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                var mailParameters = new Dictionary<string, string>()
                {
                    { "SCHOOL_SHORT_NAME", schoolShortName},
                };

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toDto.EmailID, emailSubject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                var eXErrorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Student application Mailing failed. Error message: {eXErrorMessage}", ex);
            }

            return ToDTOString(ToDTO(entity.ApplicationIID));
        }

        private StudentApplicationDTO ClassDetail(long? classID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new StudentApplicationDTO();

                var classDetail = dbContext.Classes.Where(x => x.ClassID == classID).AsNoTracking().FirstOrDefault();

                dtos.ClassName = classDetail?.ClassDescription;

                return dtos;
            }
        }

        public List<StudentDTO> GetStudentsSiblings(long parentId)
        {
            List<StudentDTO> studentDTO = new List<StudentDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //studentDTO = (from s in dbContext.Students
                //              join c in dbContext.Classes on s.ClassID equals c.ClassID
                //              join sc in dbContext.Sections on s.SectionID equals sc.SectionID
                //              join p in dbContext.Parents on s.ParentID equals p.ParentIID
                //              where (p.LoginID == parentId)/* OrderBy(psm.StudentID)*/
                //              orderby s.StudentIID ascending
                //              select new StudentDTO()
                //              {
                //                  StudentIID = s.StudentIID,
                //                  FirstName = s.FirstName,
                //                  MiddleName = s.MiddleName,
                //                  LastName = s.LastName,
                //                  ClassID = s.ClassID,
                //                  SectionID = s.SectionID,
                //                  ClassName = s.Class.ClassDescription,
                //                  SectionName = sc.SectionName,
                //                  IsSelected = false,
                //                  StudentProfile = s.StudentProfile,
                //              }).ToList();

                studentDTO = dbContext.Students.Where(s => s.ParentID == parentId)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .OrderBy(o => o.StudentIID)
                    .Select(s => new StudentDTO()
                    {
                        StudentIID = s.StudentIID,
                        FirstName = s.FirstName,
                        MiddleName = s.MiddleName,
                        LastName = s.LastName,
                        ClassID = s.ClassID,
                        SectionID = s.SectionID,
                        ClassName = s.Class.ClassDescription,
                        SectionName = s.Section.SectionName,
                        IsSelected = false,
                        StudentProfile = s.StudentProfile,
                    }).ToList();
            }

            if (studentDTO != null && studentDTO.Count > 0)
            {
                studentDTO[0].IsSelected = true;
            }

            return studentDTO;
        }

        //public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        //{
        //    var toDto = dto as StudentApplicationDTO;
        //    var valueDTO = new KeyValueDTO();

        //    switch (field)
        //    {
        //        case "StudentPassportNo":
        //            if (!string.IsNullOrEmpty(toDto.StudentPassportDetails.PassportNo))
        //            {
        //                var hasDuplicated = IsPassportNoDuplicated(toDto.StudentPassportDetails.PassportNo, toDto.ApplicationIID);
        //                if (hasDuplicated)
        //                {
        //                    valueDTO.Key = "true";
        //                    valueDTO.Value = "Student Passport No already exists, Please try with different Student Passport No.";
        //                }
        //                else
        //                {
        //                    valueDTO.Key = "false";
        //                }
        //            }
        //            else
        //            {
        //                valueDTO.Key = "false";
        //            }
        //            break;
        //        case "StudentVisaNo":
        //            if (!string.IsNullOrEmpty(toDto.StudentVisaDetails.VisaNo))
        //            {
        //                var hasDuplicated = IsVisaNoDuplicated(toDto.StudentVisaDetails.VisaNo, toDto.StudentVisaDetails.VisaDetailsIID);
        //                if (hasDuplicated)
        //                {
        //                    valueDTO.Key = "true";
        //                    valueDTO.Value = "Stduent Visa No already exists, Please try with different Student Visa No.";
        //                }
        //                else
        //                {
        //                    valueDTO.Key = "false";
        //                }
        //            }
        //            else
        //            {
        //                valueDTO.Key = "false";
        //            }
        //            break;
        //        case "StudentNationalID":
        //            if (!string.IsNullOrEmpty(toDto.StudentNationalID))
        //            {
        //                var hasDuplicated = IsNationalIDNoDuplicated(toDto.StudentNationalID, toDto.ApplicationIID);
        //                if (hasDuplicated)
        //                {
        //                    valueDTO.Key = "true";
        //                    valueDTO.Value = "Stduent National ID No already exists, Please try with different Student National ID No.";
        //                }
        //                else
        //                {
        //                    valueDTO.Key = "false";
        //                }
        //            }
        //            else
        //            {
        //                valueDTO.Key = "false";
        //            }
        //            break;
        //    }

        //    return valueDTO;
        //}

        public bool IsPassportNoDuplicated(string StudentPassportNo, long ApplicationIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentApplication> passport;

                if (ApplicationIID == 0)
                {
                    passport = db.StudentApplications.Where(x => x.StudentPassportNo == StudentPassportNo).AsNoTracking().ToList();
                }
                else
                {
                    passport = db.StudentApplications.Where(x => x.ApplicationIID != ApplicationIID && x.StudentPassportNo == StudentPassportNo).AsNoTracking().ToList();
                }

                if (passport.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsVisaNoDuplicated(string StudentVisaNo, long VisaDetailsID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<VisaDetailMap> visa;

                if (VisaDetailsID == 0)
                {
                    visa = db.VisaDetailMaps.Where(x => x.VisaNo == StudentVisaNo).AsNoTracking().ToList();
                }
                else
                {
                    visa = db.VisaDetailMaps.Where(x => x.VisaDetailsIID != VisaDetailsID && x.VisaNo == StudentVisaNo).AsNoTracking().ToList();
                }

                if (visa.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsNationalIDNoDuplicated(string StudentNationalID, long ApplicationIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<StudentApplication> national;

                if (ApplicationIID == 0)
                {
                    national = db.StudentApplications.Where(x => x.StudentNationalID == StudentNationalID).AsNoTracking().ToList();
                }
                else
                {
                    national = db.StudentApplications.Where(x => x.ApplicationIID != ApplicationIID && x.StudentNationalID == StudentNationalID).AsNoTracking().ToList();
                }

                if (national.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public List<KeyValueDTO> GetCastByRelegion(int relegionID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var castList = new List<KeyValueDTO>();

                var entities = dbContext.Casts.Where(x => x.RelegionID == relegionID || x.RelegionID == null).AsNoTracking().ToList();

                foreach (var cast in entities)
                {
                    castList.Add(new KeyValueDTO
                    {
                        Key = cast.CastID.ToString(),
                        Value = cast.CastDescription
                    });
                }
                return castList;
            }
        }

        public List<KeyValueDTO> GetStreamByStreamGroup(byte? streamGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var streamList = new List<KeyValueDTO>();
                var streams = dbContext.Streams.Where(x => x.StreamGroupID == streamGroupID).AsNoTracking().ToList();

                foreach (var dat in streams)
                {
                    streamList.Add(new KeyValueDTO
                    {
                        Key = dat.StreamID.ToString(),
                        Value = dat.Code + " - " + dat.Description
                    });
                }
                return streamList;
            }
        }

        public List<KeyValueDTO> GetAcademicYearBySchool(int schoolID)
        {
            if (schoolID == 0)
            {
                schoolID = Convert.ToInt32(_context.SchoolID);
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearList = dbContext.AcademicYears
                                       .Where(srcs => (srcs.AcademicYearStatusID == 2 || srcs.AcademicYearStatusID == 3) && srcs.SchoolID == (byte?)schoolID)
                                       .AsNoTracking()
                                       .Select(srcs => new KeyValueDTO
                                       {
                                           Key = srcs.AcademicYearID.ToString(),
                                           Value = srcs.Description + " " + "(" + srcs.AcademicYearCode + ")"
                                       }).ToList();

                return academicyearList;
            }
        }

        public List<KeyValueDTO> GetAcademicYearCodeBySchool(int schoolID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearList = dbContext.AcademicYears
                                       .Where(srcs => (srcs.AcademicYearStatusID == 2 || srcs.AcademicYearStatusID == 3) && srcs.SchoolID == (byte?)schoolID)
                                       .AsNoTracking()
                                       .Select(srcs => new KeyValueDTO
                                       {
                                           Key = srcs.AcademicYearID.ToString(),
                                           Value = srcs.AcademicYearCode
                                       }).ToList();

                return academicyearList;
            }
        }

        public List<KeyValueDTO> GetClasseByAcademicyear(int academicyearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classList = new List<KeyValueDTO>();

                classList = dbContext.Classes
                             .Where(cls => cls.IsVisible == true && cls.AcademicYearID == academicyearID)
                             .AsNoTracking()
                             .OrderBy(o => o.Code)
                             .Select(cls => new KeyValueDTO
                             {
                                 Key = cls.ClassID.ToString(),
                                 Value = cls.ClassDescription
                             }).ToList();

                return classList;
            }
        }

        public string SaveStudentApplication(StudentApplicationDTO studentApplication)
        {
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequencenew = new Entity.Models.Settings.Sequence();

            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            String emailBody = "";
            String emailSubject = "";

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var applicationData = dbContext.StudentApplications.Where(X => X.ProspectNumber == studentApplication.ProspectNumber).AsNoTracking().FirstOrDefault();

                if (applicationData != null)
                {
                    return "It's Already done!";
                    throw new Exception("Can't Move to Admission.It's Already done!");
                }

                //duplicate check with DOB and parent emailID
                //var checkDuplicate = dbContext.StudentApplications.FirstOrDefault(y => y.DateOfBirth == studentApplication.DateOfBirth && y.StudentPassportNo == studentApplication.StudentPassportNo);
                //if ( checkDuplicate != null)
                //{
                //    return "Student already exist in Student Application Please check!";
                //}
            }

            if (studentApplication.ApplicationIID == 0)
            {
                try
                {
                    sequencenew = mutualRepository.GetNextSequence("ApplicationNumber", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'ApplicationNumber'");
                }
            }

            Login loginData = null;
            var logindetail = new Login();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entrenceTestID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_APPLICATION_ENTRANCETEST_SHEDULED");
                var moveToApplicationID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAD_MOVE_TO_APPLICATION");

                loginData = dbContext.Logins.Where(X => X.LoginEmailID == studentApplication.EmailID).AsNoTracking().FirstOrDefault();

                var application = new StudentApplication()
                {
                    ProspectNumber = studentApplication.ProspectNumber,
                    EmailID = studentApplication.EmailID.ToLower(),
                    FirstName = studentApplication.FirstName,
                    MobileNumber = studentApplication.MobileNumber,
                    FatherFirstName = studentApplication.FatherFirstName,
                    GenderID = studentApplication.GenderID,
                    DateOfBirth = studentApplication.DateOfBirth,
                    ClassID = studentApplication.ClassID,
                    ApplicationStatusID = byte.Parse(entrenceTestID),
                    CurriculamID = studentApplication.CurriculamID,
                    SchoolID = studentApplication.SchoolID != null ? studentApplication.SchoolID : null,
                    SchoolAcademicyearID = studentApplication.SchoolAcademicyearID,
                    ApplicationNumber = studentApplication.ApplicationIID == 0 ? sequencenew.Prefix + sequencenew.LastSequence : studentApplication.ApplicationNumber,
                    CreatedBy = studentApplication.ApplicationIID == 0 ? (int)_context.LoginID : studentApplication.CreatedBy,
                    UpdatedBy = studentApplication.ApplicationIID > 0 ? (int)_context.LoginID : studentApplication.UpdatedBy,
                    CreatedDate = studentApplication.ApplicationIID == 0 ? DateTime.Now : studentApplication.CreatedDate,
                    UpdatedDate = studentApplication.ApplicationIID > 0 ? DateTime.Now : studentApplication.UpdatedDate,
                };

                if (loginData == null)
                {
                    logindetail = new Login()
                    {
                        LoginEmailID = studentApplication.EmailID.ToLower(),
                        UserName = studentApplication.EmailID.ToLower(),
                        LoginUserID = studentApplication.EmailID.ToLower(),
                        Password = "123456",
                        StatusID = 1,
                        SchoolID = studentApplication.SchoolID != null ? studentApplication.SchoolID : null,
                        CreatedBy = studentApplication.ApplicationIID == 0 ? (int)_context.LoginID : studentApplication.CreatedBy,
                        UpdatedBy = studentApplication.ApplicationIID > 0 ? (int)_context.LoginID : studentApplication.UpdatedBy,
                        CreatedDate = studentApplication.ApplicationIID == 0 ? DateTime.Now : studentApplication.CreatedDate,
                        UpdatedDate = studentApplication.ApplicationIID > 0 ? DateTime.Now : studentApplication.UpdatedDate,
                    };

                    application.Login = logindetail;
                }
                else
                {
                    application.LoginID = loginData == null ? logindetail.LoginIID : loginData.LoginIID;
                }


                if (application.ApplicationIID == 0)
                {
                    dbContext.Entry(application).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                if (loginData == null)
                {
                    logindetail.PasswordSalt = PasswordHash.CreateHash(logindetail.Password);
                    logindetail.Password = StringCipher.Encrypt(logindetail.Password, logindetail.PasswordSalt);
                    dbContext.Entry(logindetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                if (application.ProspectNumber != null)
                {
                    var leadData = dbContext.Leads.FirstOrDefault(X => X.LeadCode == application.ProspectNumber);

                    if (leadData != null)
                    {
                        leadData.LeadStatusID = byte.Parse(moveToApplicationID);
                        leadData.UpdatedBy = leadData.LeadIID > 0 ? (int)_context.LoginID : leadData.CreatedBy;
                        leadData.UpdatedDate = leadData.LeadIID > 0 ? DateTime.Now : leadData.CreatedDate;
                    }
                    dbContext.Entry(leadData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_APPLICATION_WORKFLOW_ID");

                Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(long.Parse(settingValue), application.ApplicationIID);

                if (loginData == null && application.ApplicationIID != 0)
                {
                    emailBody = @"<br />
                        <p align='left'>Dear Parent/Guardian,<br />Greetings from Pearl School..</p><br />
                        Application Reference Number :" + application.ApplicationNumber + @"<br />
                        We have reviewed your enquiry in the above referred application.<br />
                        Please find the below link for application for admission for your ward.<br />
                        Please ensure that the details entered are accurate and confirms with the relevent official documents.<br /><br />
                        Link : <a href='https://parent.pearlschool.org/Account/Login'>Click here to login</a><br /><br />
                        Username : " + application.EmailID + @"<br />
                        Password : " + 123456 + @"<br />
                        Alternatively, you can visit the school and meet our Admission Coordiantor on any working day<br />
                        (Saturday to Thursday from 8.00 am to 7.00 pm,Saturday 8.00 am to 1.00 pm)<br />
                        For furthur information on the admission process.please refer to our schoool website or contact our Admission coordinator in the numbers given below.<br /><br />
                             Regards,<br />
                             Phone  : (+974)4444 2555, 44145595 <br />
                             Mobile : 66923729 <br />
                             Email  : admissions@pearlschool.org <br />
                             <a href='http://www.pearlschool.org/'>www.pearlschool.org</a><br />
                             <b>Pearl School </b><br />
                             P.O.Box 33032 Doha,State of Qatar.
                            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

                    emailSubject = "Login Credentials for Admission portal";
                }

                if (loginData != null && application.ApplicationIID != 0)
                {
                    emailBody = @"<br />
                        <p align='left'>Dear Parent/Guardian,<br />Greetings from Pearl School..</p><br />
                        Application Reference Number :" + application.ApplicationNumber + @"<br />
                        We have reviewed your enquiry in the above referred application.<br />
                        Please find the below link for application for admission for your ward.<br />
                        Please ensure that the details entered are accurate and confirms with the relevent official documents.<br /><br />
                        Link : <a href='https://parent.pearlschool.org/Account/Login'>Click here to login</a><br /><br />
                        If you are already a user please login with your credentials,otherwise signup with the below credentials<br />
                        Username : " + application.EmailID + @"<br />
                        Password : " + 123456 + @"<br />
                        Alternatively, you can visit the school and meet our Admission Coordiantor on any working day<br />
                        (Saturday to Thursday from 8.00 am to 7.00 pm,Saturday 8.00 am to 1.00 pm)<br />
                        For furthur information on the admission process.please refer to our schoool website or contact our Admission coordinator in the numbers given below.<br /><br />
                             Regards,<br />
                             Phone  : (+974)4444 2555, 44145595 <br />
                             Mobile : 66923729 <br />
                             Email  : admissions@pearlschool.org <br />
                             <a href='http://www.pearlschool.org/'>www.pearlschool.org</a><br />
                             <b>Pearl School </b><br />
                             P.O.Box 33032 Doha,State of Qatar.
                            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
                    emailSubject = "Automatic reply: Received your application";
                }

                //var emaildata = new EmailNotificationDTO();
                try
                {
                    string schoolShortName = string.Empty;
                    if (application.SchoolID.HasValue)
                    {
                        schoolShortName = application.School?.SchoolShortName?.ToLower();
                        if (string.IsNullOrEmpty(schoolShortName))
                        {
                            var schoolData = new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(application.SchoolID.Value);

                            schoolShortName = schoolData?.SchoolShortName?.ToLower();
                        }
                    }

                    var mailParameters = new Dictionary<string, string>()
                    {
                        { "SCHOOL_SHORT_NAME", schoolShortName},
                    };

                    var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                    string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                    string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(application.EmailID, emailBody);

                    if (emailBody != "")
                    {
                        if (hostDet.ToLower() == "live")
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(application.EmailID, emailSubject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                        }
                        else
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                        }
                    }

                }
                catch { }
            }

            return "Successfully Moved";
        }

        public string UpdateLeadStatusID(string prospectNumber)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var moveToApplicationID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAD_MOVE_TO_APPLICATION");

                //For Change Lead Application Status
                if (prospectNumber != null)
                {
                    var leadData = dbContext.Leads.Where(X => X.LeadCode == prospectNumber).AsNoTracking().FirstOrDefault();
                    var studapplication = dbContext.StudentApplications.Where(X => X.ProspectNumber == leadData.LeadCode).AsNoTracking().FirstOrDefault();
                    if (leadData != null)
                    {
                        leadData.LeadStatusID = byte.Parse(moveToApplicationID);
                        leadData.UpdatedBy = studapplication.ApplicationIID > 0 ? (int)_context.LoginID : studapplication.CreatedBy;
                        leadData.UpdatedDate = studapplication.ApplicationIID > 0 ? DateTime.Now : studapplication.CreatedDate;
                    }
                    dbContext.Entry(leadData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            return null;
        }

        public string ResentFromLeadLoginCredentials(StudentApplicationDTO studtApplication)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            String emailBody = "";
            String emailSubject = "";

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var leadData = dbContext.Leads.Where(X => X.LeadCode == studtApplication.ProspectNumber).AsNoTracking().FirstOrDefault();
                var applicationData = dbContext.StudentApplications
                    .Where(X => X.ProspectNumber == leadData.LeadCode)
                    .Include(i => i.School)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (applicationData != null)
                {
                    emailBody = @"<br />
                        <p align='left'>Dear Parent/Guardian,<br />Greetings from Pearl School..</p><br />
                        Application Reference Number :" + applicationData.ApplicationNumber + @"<br />
                        We have reviewed your enquiry in the above referred application.<br />
                        Please find the below link for application for admission for your ward.<br />
                        Please ensure that the details entered are accurate and confirms with the relevent official documents.<br /><br />
                        Link : <a href='https://parent.pearlschool.org/Account/Login'>Click here to login</a><br /><br />
                        Username : " + leadData.EmailAddress + @"<br />
                        Password : " + 123456 + @"<br />
                        Alternatively, you can visit the school and meet our Admission Coordiantor on any working day<br />
                        (Saturday to Thursday from 8.00 am to 7.00 pm,Saturday 8.00 am to 1.00 pm)<br />
                        For furthur information on the admission process.please refer to our schoool website or contact our Admission coordinator in the numbers given below.<br /><br />
                             Regards,<br />
                             Phone  : (+974)4444 2555, 44145595 <br />
                             Mobile : 66923729 <br />
                             Email  : admissions@pearlschool.org <br />
                             <a href='http://www.pearlschool.org/'>www.pearlschool.org</a><br />
                             <b>Pearl School </b><br />
                             P.O.Box 33032 Doha,State of Qatar.
                            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

                    emailSubject = "Login Credentials for Admission portal";
                }

                else
                {
                    return "false";
                }

                //var emaildata = new EmailNotificationDTO();
                try
                {
                    string schoolShortName = string.Empty;
                    if (applicationData.SchoolID.HasValue)
                    {
                        schoolShortName = applicationData.School?.SchoolShortName?.ToLower();
                        if (string.IsNullOrEmpty(schoolShortName))
                        {
                            var schoolData = new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(applicationData.SchoolID.Value);

                            schoolShortName = schoolData?.SchoolShortName?.ToLower();
                        }
                    }

                    var mailParameters = new Dictionary<string, string>()
                    {
                        { "SCHOOL_SHORT_NAME", schoolShortName},
                    };

                    var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                    string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                    string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(leadData.EmailAddress, emailBody);

                    if (emailBody != "")
                    {
                        if (hostDet.ToLower() == "live")
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(leadData.EmailAddress, emailSubject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                        }
                        else
                        {
                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                        }
                    }

                }
                catch { }
            }
            return "Login Credentials Successfully Sent";
        }

        public StudentApplicationDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            string criteriaDetails = null;
            var dtos = new StudentApplicationDTO();
            var LeadDTO = new List<StudentApplicationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (classID != 0)
                {
                    var ageData = dbContext.AgeCriterias.Where(X => X.ClassID == classID && X.SchoolID == schoolID && X.AcademicYearID == academicID).AsNoTracking().FirstOrDefault();
                    if (ageData != null)
                    {
                        if (dob <= ageData.BirthTo && dob >= ageData.BirthFrom)
                        {
                            criteriaDetails = "";
                        }
                        else
                        {
                            criteriaDetails = "The selected DOB doesn't meet the age criteria set for this class!! Eligible only for" + " " + ageData.BirthFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + " to " + ageData.BirthTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                    }
                }
            }
            dtos.AgeCriteriaWarningMsg = criteriaDetails;

            return dtos;
        }

    }
}