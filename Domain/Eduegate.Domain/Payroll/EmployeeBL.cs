using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Framework.Extensions;
using System.Data;
using Eduegate.Services.Contracts.HR;
using Eduegate.Domain.Mappers.HR;
using Eduegate.Domain.Mappers.Employment;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Eduegate.Framework.Translator;
using System.Globalization;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Domain.Mappers.HR.Employment;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.School.Models.School;


namespace Eduegate.Domain.Payroll
{
    public class EmployeeBL
    {
        private CallContext _callContext;
        EmployeeRepository repository = new EmployeeRepository();

        public EmployeeBL(CallContext context)
        {
            _callContext = context;
        }

        public List<EmployeeDTO> GetEmployees()
        {
            var dtos = new List<EmployeeDTO>();

            var employees = repository.GetEmployees();

            foreach (var employee in employees)
            {
                dtos.Add(GetEmployeeDetailsForKeyValue(employee, _callContext));
            }

            return dtos;
        }

        public EmployeeDTO GetEmployeeDetailsForKeyValue(Employee entity, CallContext context)
        {
            var dto = new EmployeeDTO()
            {
                EmployeeIID = entity.EmployeeIID,
                EmployeeAlias = entity.EmployeeAlias,
                EmployeeCode = entity.EmployeeCode,
                EmployeeName = entity.FirstName + " " + entity.MiddleName + " " + entity.LastName,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
            };

            return dto;
        }

        public List<EmployeeDTO> GetEmployeesByRoles(int roleID)
        {
            var dtos = new List<EmployeeDTO>();

            foreach (var employee in repository.GetEmployeesByRoles(roleID))
            {
                dtos.Add(FromEntity(employee, _callContext));
            }

            return dtos;
        }
        public List<KeyValueDTO> GetEmployeeByBranch(int? branchID)
        {
            var dtos = new List<KeyValueDTO>();

            var employeeList = repository.GetEmployeeByBranch(branchID);
            foreach (var employee in employeeList)
            {
                dtos.Add(new KeyValueDTO() { Key = employee.EmployeeIID.ToString() , Value = employee.EmployeeCode + " - " + employee.FirstName  + " "+ employee.MiddleName + " "+ employee.LastName });
            }
            return dtos;
        }
        public List<EmployeeDTO> GetEmployeesByDesignation(string designationCode)
        {
            var dtos = new List<EmployeeDTO>();

            var employeeList = repository.GetEmployeesByDesignation(designationCode);

            foreach (var employee in employeeList)
            {
                dtos.Add(FromEntity(employee, _callContext));
            }

            return dtos;
        }

        public List<EmployeeDTO> GetEmployeesBySkus(int roleID)
        {
            var dtos = new List<EmployeeDTO>();

            foreach (var employee in repository.GetEmployeesBySkus(roleID))
            {
                dtos.Add(FromEntity(employee, _callContext));
            }

            return dtos;
        }

        public EmployeeDTO GetEmployee(long employeeID)
        {
            return FromEntity(repository.GetEmployee(employeeID), _callContext);
        }

        public EmployeeDTO SaveEmployee(EmployeeDTO employeeDetails)
        {
            var empEntity = repository.SaveEmployee(ToEntity(employeeDetails, _callContext), _callContext);

            if (empEntity.EmployeeIID != 0 && empEntity.JobInterviewMapID.HasValue)
            {
                var jobPackageNegotiationMapper = new JobPackageNegotiationMapper();
                jobPackageNegotiationMapper.CheckAndUpdateEmployeeStructure(empEntity.EmployeeIID, empEntity.JobInterviewMapID.Value);
            }

            return FromEntity(empEntity, _callContext);
        }

        public EmployeeCatalogRelationDTO SaveEmployeeCatalogRelation(List<EmployeeCatalogRelationDTO> dtos)
        {
            if (dtos == null)
                return null;

            EmployeeCatalogRelation employeeCatalogRelation = new EmployeeCatalogRelation();
            // remove EmployeeCatalogRelation
            new EmployeeRepository().RemoveEmployeeCatalogRelationsByRelationId(EmployeeCatalogRelationMapper.ToEntity(dtos[0]));
            foreach (var dto in dtos)
            {
                dto.CreatedBy = (int)_callContext.LoginID;
                dto.CreatedDate = DateTime.Now;
                employeeCatalogRelation = new EmployeeRepository().AddEmployeeCatalogRelations(EmployeeCatalogRelationMapper.ToEntity(dto));
            }
            return EmployeeCatalogRelationMapper.ToDto(employeeCatalogRelation);
        }

        public List<EmployeeDTO> SearchEmployee(string searchText, int pageSize)
        {
            var employees = repository.GetEmployees(searchText, pageSize);

            var dtos = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                dtos.Add(FromEntity(employee, _callContext, false));
            }

            return dtos;
        }

        public List<KeyValueDTO> GetEmployeeIdNameCatalogRelation(RelationTypes relationType, long relationID)
        {
            List<Employee> lists = new EmployeeRepository().GetEmployeeIdNameCatalogRelation((short)relationType, relationID);
            List<KeyValueDTO> dtos = new List<KeyValueDTO>();
            lists.ForEach(x =>
            {
                dtos.Add(new KeyValueDTO
                {
                    Key = Convert.ToString(x.EmployeeIID),
                    Value = x.FirstName + " " + x.MiddleName + " " + x.LastName
                });
            });

            return dtos;
        }

        public static EmployeeDTO FromEntity(Employee entity, CallContext context, bool requiredAdditionalInfo = true)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var reportEmployee = entity.ReportingEmployeeID.HasValue ? employeeRepository.GetEmployeeByEmployeeID(entity.ReportingEmployeeID) : null;
            var additionalInfo = entity.EmployeeAdditionalInfos.Count > 0 ? entity.EmployeeAdditionalInfos.FirstOrDefault() : null;
            var passportvisaInfo = entity.PassportVisaDetails.Count > 0 ? entity.PassportVisaDetails.FirstOrDefault() : null;
            var employeeBank = entity.EmployeeBankDetails.Count > 0 ? entity.EmployeeBankDetails.FirstOrDefault() : null;
            var leaveAlloc = entity.EmployeeLeaveAllocations.Count > 0 ? entity.EmployeeLeaveAllocations.FirstOrDefault() : null;
            var academicdetails = entity.EmployeeQualificationMaps.Count > 0 ? entity.EmployeeQualificationMaps.FirstOrDefault() : null;
            var academexperience = entity.EmployeeExperienceDetails.Count > 0 ? entity.EmployeeExperienceDetails.FirstOrDefault() : null;



            //var additionalInfo = entity.EmployeeAdditionalInfos.Count>0?  entity.EmployeeAdditionalInfos.FirstOrDefault(): new Entity.Models.HR.EmployeeAdditionalInfo();
            //var passportvisaInfo = entity.PassportVisaDetails.Count>0? entity.PassportVisaDetails.FirstOrDefault() : new Entity.Models.HR.PassportVisaDetail();
            //var employeeBank = entity.EmployeeBankDetails.Count>0? entity.EmployeeBankDetails.FirstOrDefault() : new Entity.Models.HR.EmployeeBankDetail();
            //var payrollinfo = entity.Employee.Count > 0 ? entity.PayrollInfos.FirstOrDefault() : new Entity.Models.HR.PayrollInfo();

            var dto = new EmployeeDTO()
            {
                EmployeeIID = entity.EmployeeIID,
                EmployeeCode = entity.EmployeeCode,
                EmployeeName = entity.FirstName + " " + (string.IsNullOrEmpty(entity.MiddleName) ? "" : entity.MiddleName + " ") + entity.LastName,
                Age = entity.Age,
                BranchID = entity.BranchID,
                DateOfBirth = entity.DateOfBirth,
                DateOfJoining = entity.DateOfJoining,
                EmployeeAlias = entity.EmployeeAlias,
                DepartmentID = entity.DepartmentID,
                DesignationID = entity.DesignationID,
                EmployeePhoto = entity.EmployeePhoto,
                GenderID = entity.GenderID,
                BloodGroupID = entity.BloodGroupID,
                EmergencyContactNo = entity.EmergencyContactNo,
                JobTypeID = entity.JobTypeID,
                WorkMobileNo = entity.WorkMobileNo,
                ////TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedBy = entity.CreatedBy == null ? 0 : int.Parse(entity.CreatedBy.ToString()),
                UpdatedBy = entity.UpdatedBy == null ? 0 : int.Parse(entity.UpdatedBy.ToString()),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                WorkPhone = entity.WorkPhone,
                WorkEmail = entity.WorkEmail,
                MaritalStatusID = entity.MaritalStatusID,
                ReportingEmployeeID = entity.ReportingEmployeeID,
                ReportingEmployeeName = reportEmployee != null ? reportEmployee.EmployeeCode + " - " + reportEmployee.FirstName + " " + reportEmployee.MiddleName + " " + reportEmployee.LastName : null,
                Login = entity.Login == null ? new Services.Contracts.LoginDTO() : Mappers.LoginMapper.Mapper(context).ToDTO(entity.Login),
                EmployeeRoles = new List<KeyValueDTO>(),
                CompanyID = entity.CompanyID.IsNotNull() ? entity.CompanyID : context.CompanyID,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                NationalityID = entity.NationalityID,
                NationalityName = entity.NationalityID.HasValue ? entity.Nationality.NationalityName : null,
                AdhaarCardNo = entity.AdhaarCardNo,
                PermenentAddress = entity.PermenentAddress,
                PresentAddress = entity.PresentAddress,
                RelegionID = entity.RelegionID,
                CastID = entity.CastID,
                CommunityID = entity.CommunityID,
                LicenseNumber = entity.LicenseNumber,
                LicenseTypeID = entity.LicenseTypeID,
                SignatureContentID = entity.SignatureContentID,
                IsActive = entity.IsActive,
                LastWorkingDate = entity.LastWorkingDate,
                LeavingTypeID = entity.LeavingTypeID,
                ConfirmationDate = entity.ConfirmationDate,
                ResignationDate = entity.ResignationDate,
                Grade = entity.Grade,
                AccomodationTypeID = entity.AccomodationTypeID,
                PassageTypeID = entity.PassageTypeID,
                CBSEID = entity.CBSEID,
                InActiveDate = entity.InActiveDate,
                StatusID = entity.StatusID,
                AirFareInfo = new EmployeeAirFareDTO()
                {
                    ISTicketEligible = entity.ISTicketEligible,
                    TicketEligibleFromDate = entity.TicketEligibleFromDate,
                    LastTicketGivenDate = entity.LastTicketGivenDate,
                    EmployeeCountryAirportID = entity.EmployeeCountryAirportID,
                    EmployeeCountryAirport = entity.EmployeeCountryAirportID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.EmployeeCountryAirportID.ToString(),
                        Value = entity.EmployeeCountryAirport?.AirportName + '(' + entity.EmployeeCountryAirport?.IATA + ')'
                    } : new KeyValueDTO()
                    {
                        Key = null,
                        Value = null
                    },
                    EmployeeNearestAirportID = entity.EmployeeNearestAirportID,
                    EmployeeNearestAirport = entity.EmployeeNearestAirportID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.EmployeeNearestAirportID.ToString(),
                        Value = entity.EmployeeNearestAirport?.AirportName + '(' + entity.EmployeeNearestAirport?.IATA + ')'
                    } : new KeyValueDTO()
                    {
                        Key = null,
                        Value = null
                    },
                    TicketEntitilementID = entity.TicketEntitilementID,
                    TicketEntitilement = entity.TicketEntitilementID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TicketEntitilementID.ToString(),
                        Value = entity.TicketEntitilement?.TicketEntitilement1
                    } : new KeyValueDTO()
                    {
                        Key = null,
                        Value = null
                    },
                    GenerateTravelSector = entity.GenerateTravelSector,
                    IsTwoWay = entity.IsTwoWay,
                    FlightClassID = entity.FlightClassID,
                    FlightClass = entity.FlightClassID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.FlightClassID.ToString(),
                        Value = entity.FlightClass?.FlightClassName
                    } : new KeyValueDTO()
                    {
                        Key = null,
                        Value = null
                    },
                },
                BankDetailInfo = employeeBank == null ? null : new EmployeeBankDetailDTO()
                {
                    AccountNo = employeeBank.AccountNo != null ? employeeBank.AccountNo : null,
                    IBAN = employeeBank.IBAN != null ? employeeBank.IBAN : null,
                    EmployeeBankIID = employeeBank.EmployeeBankIID,
                    SwiftCode = employeeBank.SwiftCode != null ? employeeBank.SwiftCode : null,
                    BankID = employeeBank.BankID.HasValue ? employeeBank.BankID : null,
                    CreatedBy = entity.CreatedBy == null ? 0 : int.Parse(entity.CreatedBy.ToString()),
                    UpdatedBy = entity.UpdatedBy == null ? 0 : int.Parse(entity.UpdatedBy.ToString()),
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    ////TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                    EmployeeID = employeeBank.EmployeeID.HasValue ? employeeBank.EmployeeID : null,
                },

                PassportVisaInfo = passportvisaInfo == null ? null : new PassportVisaDetailDTO()
                {
                    ReferenceID = passportvisaInfo.ReferenceID.HasValue ? passportvisaInfo.ReferenceID : null,
                    PassportVisaIID = passportvisaInfo.PassportVisaIID,
                    PassportNo = passportvisaInfo.PassportNo != null ? passportvisaInfo.PassportNo : null,
                    CountryofIssueID = passportvisaInfo.CountryofIssueID.HasValue ? passportvisaInfo.CountryofIssueID : null,
                    CountryofIssueName = passportvisaInfo.CountryofIssueID.HasValue ? passportvisaInfo.Country.CountryName : null,
                    PlaceOfIssue = passportvisaInfo.PlaceOfIssue != null ? passportvisaInfo.PlaceOfIssue : null,
                    PassportNoIssueDate = passportvisaInfo.PassportNoIssueDate.HasValue ? passportvisaInfo.PassportNoIssueDate : null,
                    PassportNoExpiry = passportvisaInfo.PassportNoExpiry.HasValue ? passportvisaInfo.PassportNoExpiry : null,
                    VisaNo = passportvisaInfo.VisaNo != null ? passportvisaInfo.VisaNo : null,
                    VisaExpiry = passportvisaInfo.VisaExpiry.HasValue ? passportvisaInfo.VisaExpiry : null,
                    NationalIDNo = passportvisaInfo.NationalIDNo != null ? passportvisaInfo.NationalIDNo : null,
                    NationalIDNoIssueDate = passportvisaInfo.NationalIDNoIssueDate.HasValue ? passportvisaInfo.NationalIDNoIssueDate : null,
                    NationalIDNoExpiry = passportvisaInfo.NationalIDNoExpiry.HasValue ? passportvisaInfo.NationalIDNoExpiry : null,
                    //SponceredBy = passportvisaInfo.SponceredBy != null ? passportvisaInfo.SponceredBy : null,
                    SponsorID = passportvisaInfo.SponsorID != null ? passportvisaInfo.SponsorID : null,
                    UserType = passportvisaInfo.UserType != null ? passportvisaInfo.UserType : null,
                    MOIID = entity.MOIID != null ? entity.MOIID : null,
                    HealthCardNo = entity.HealthCardNo,
                    LabourCardNo = entity.LabourCardNo,
                },

                AdditionalInfo = additionalInfo == null ? null : new EmployeeAdditionalInfoDTO()
                {
                    EmployeeAdditionalInfoIID = additionalInfo.EmployeeAdditionalInfoIID,
                    AdditioanalSubjectTought = additionalInfo.AdditioanalSubjectTought != null ? additionalInfo.AdditioanalSubjectTought : null,
                    HighestAcademicQualitication = additionalInfo.HighestAcademicQualitication != null ? additionalInfo.HighestAcademicQualitication : null,
                    HighestPrefessionalQualitication = additionalInfo.HighestPrefessionalQualitication != null ? additionalInfo.HighestPrefessionalQualitication : null,
                    TotalYearsofExperience = entity.TotalYearsofExperience != null ? entity.TotalYearsofExperience : null,
                    ClassessTaught = additionalInfo.ClassessTaught != null ? additionalInfo.ClassessTaught : null,
                    IsComputerTrained = additionalInfo.IsComputerTrained != null ? additionalInfo.IsComputerTrained : null,
                    AppointedSubject = additionalInfo.AppointedSubject != null ? additionalInfo.AppointedSubject : null,
                    MainSubjectTought = additionalInfo.MainSubjectTought != null ? additionalInfo.MainSubjectTought : null,
                },

                PayrollInfo = new EmployeePayrollDTO()
                {
                    CalendarTypeID = entity.CalendarTypeID,
                    AcademicCalendarID = entity.AcademicCalendarID,
                    IsOTEligible = entity.IsOTEligible,
                    IsLeaveSalaryEligible = entity.IsLeaveSalaryEligible,
                    IsEoSBEligible = entity.IsEoSBEligible,
                },

                LeaveGroupID = entity.LeaveGroupID,
                IsOverrideLeaveGroup = entity.IsOverrideLeaveGroup,
                EmployeeLeaveAllocationInfo = new List<EmployeeLeaveAllocationDTO>()

            };
            if (entity.EmployeeLeaveAllocations.Count() != 0)
            {
                foreach (var leaveInfo in entity.EmployeeLeaveAllocations)
                {
                    dto.EmployeeLeaveAllocationInfo.Add(new EmployeeLeaveAllocationDTO()
                    {
                        LeaveAllocationIID = leaveInfo.LeaveAllocationIID,
                        EmployeeID = dto.EmployeeIID,
                        AllocatedLeaves = leaveInfo.AllocatedLeaves,
                        LeaveTypeID = leaveInfo.LeaveTypeID,
                        LeaveType = leaveInfo.LeaveTypeID.HasValue ? new KeyValueDTO()
                        {
                            Key = leaveInfo.LeaveTypeID.ToString(),
                            Value = leaveInfo.LeaveType.Description
                        } : new KeyValueDTO()
                        {
                            Key = null,
                            Value = null
                        }
                    });
                }
            }

            dto.AcademicDetails = dto.AcademicDetails ?? new List<EmployeeAcademicQualificationDTO>();


            if (entity.EmployeeQualificationMaps.Count() != 0)
            {
                foreach (var academics in entity.EmployeeQualificationMaps)
                {
                    dto.AcademicDetails.Add(new EmployeeAcademicQualificationDTO()
                    {
                        QualificationID = academics.QualificationID,
                        EmployeeQualificationMapIID = academics.EmployeeQualificationMapIID,
                        EmployeeID = dto.EmployeeIID,
                        ModeOfProgramme = academics.ModeOfProgramme,
                        TitleOfProgramme = academics.TitleOfProgramme,
                        University = academics.University,
                        MarksInPercentage = academics.MarksInPercentage,
                        Subject = academics.Subject,
                        GraduationMonth = academics.GraduationMonth,
                        GraduationYear = academics.GraduationYear
                    });
                }
            }

            dto.ExperienceDetails = dto.ExperienceDetails ?? new List<EmployeeExperienceDTO>();

            if (entity.EmployeeExperienceDetails.Count() != 0)
            {
                foreach (var experience in entity.EmployeeExperienceDetails)
                {
                    dto.ExperienceDetails.Add(new EmployeeExperienceDTO()
                    {

                        EmployeeExperienceDetailIID = experience.EmployeeExperienceDetailIID,
                        EmployeeID = dto.EmployeeIID,
                        FromDate = experience.FromDate,
                        ToDate = experience.ToDate,
                        CurriculamOrIndustry = experience.CurriculamOrIndustry,
                        NameOfOraganizationtName = experience.NameOfOraganizationtName,
                        Designation = experience.Designation,
                        SubjectTaught = experience.SubjectTaught,
                        ClassTaught = experience.ClassTaught
                    });
                }
            }
            if (requiredAdditionalInfo)
            {
                foreach (var role in entity.EmployeeRoleMaps)
                {
                    if (role.EmployeeRoleID.HasValue)
                    {
                        dto.EmployeeRoles.Add(new KeyValueDTO()
                        {
                            Key = role.EmployeeRoleID.ToString(),
                            Value = role.EmployeeRole.EmployeeRoleName
                        });
                    }
                }
            }

            dto.RelationDetails = new List<EmployeeRelationsDetailDTO>();

            if (entity.EmployeeRelationsDetails.Count() != 0)
            {
                foreach (var relation in entity.EmployeeRelationsDetails)
                {
                    dto.RelationDetails.Add(new EmployeeRelationsDetailDTO()
                    {
                        EmployeeRelationTypeID = relation.EmployeeRelationTypeID,
                        EmployeeRelationsDetailIID = relation.EmployeeRelationsDetailIID,
                        EmployeeID = dto.EmployeeIID,
                        FirstName = relation.FirstName,
                        MiddleName = relation.MiddleName,
                        LastName = relation.LastName,
                        PassportNo = relation.PassportNo,
                        ContactNo = relation.ContactNo,
                        NationalIDNo = relation.NationalIDNo,
                        EmployeeRelationType = relation.EmployeeRelationTypeID.HasValue ? new KeyValueDTO()
                        {
                            Key = relation.EmployeeRelationTypeID.ToString(),
                            Value = relation.EmployeeRelationType?.EmployeeRelationTypeName,
                        } : new KeyValueDTO(),
                    });
                }
            }

            return dto;
        }
        public static Employee ToEntity(EmployeeDTO dto, CallContext callContext)
        {


            EmployeeRepository employeeRepository = new EmployeeRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();


            MutualRepository mutualRepository = new MutualRepository();
            var TempEmployeeCode = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PREFIX_TERMPORARY_EMPLOYEE_CODE");
            var permtEmployeeCode = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PREFIX_PERMENANT_EMPLOYEE_CODE");
            var defaultMailDomain = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_DOMAIN");


            if (dto.EmployeeIID == 0)
            {
                try

                {
                    sequence = employeeRepository.GetNextSequence("EmployeeCode");
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'EmployeeCode'");
                }
            }

            var empEntity = new Employee()
            {
                EmployeeIID = dto.EmployeeIID,
                EmployeeName = dto.FirstName + " " + dto.MiddleName + " " + dto.LastName,
                JobInterviewMapID = dto.JobInterviewMapID,
                Age = dto.Age,
                BranchID = dto.BranchID,
                DateOfBirth = dto.DateOfBirth,
                DateOfJoining = dto.DateOfJoining,
                ResignationDate = dto.ResignationDate,
                DepartmentID = dto.DepartmentID,
                DesignationID = dto.DesignationID,
                EmployeeAlias = dto.EmployeeAlias,
                EmployeePhoto = dto.EmployeePhoto,
                SignatureContentID = dto.SignatureContentID,
                GenderID = dto.GenderID,
                BloodGroupID = dto.BloodGroupID,
                EmergencyContactNo = dto.EmergencyContactNo,
                JobTypeID = dto.JobTypeID,
                WorkMobileNo = dto.WorkMobileNo,
                ////TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                CreatedBy = dto.EmployeeIID == 0 ? Convert.ToInt32(callContext.LoginID) : dto.CreatedBy,
                CreatedDate = dto.EmployeeIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedBy = dto.EmployeeIID != 0 ? Convert.ToInt32(callContext.LoginID) : dto.UpdatedBy,
                UpdatedDate = dto.EmployeeIID != 0 ? DateTime.Now : dto.UpdatedDate,
                MaritalStatusID = dto.MaritalStatusID,
                ReportingEmployeeID = dto.ReportingEmployeeID,
                WorkEmail = dto.WorkEmail,
                WorkPhone = dto.WorkPhone,
                LoginID = dto.Login == null ? (long?)null : dto.Login.LoginIID,
                //Login = dto.Login == null ? null : LoginMapper.Mapper(callContext).ToEntity(dto.Login),
                CompanyID = dto.CompanyID != null ? dto.CompanyID : callContext.CompanyID,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                EmployeeCode = dto.EmployeeIID == 0 ? permtEmployeeCode + sequence.LastSequence : dto.EmployeeCode,
                NationalityID = dto.NationalityID,
                AdhaarCardNo = dto.AdhaarCardNo,
                PermenentAddress = dto.PermenentAddress,
                PresentAddress = dto.PresentAddress,
                RelegionID = dto.RelegionID,
                CastID = dto.CastID,
                CommunityID = dto.CommunityID,
                LicenseNumber = dto.LicenseNumber,
                LicenseTypeID = dto.LicenseTypeID,
                IsActive = dto.IsActive,
                CategoryID = dto.PayrollInfo.CategoryID,
                CalendarTypeID = dto.PayrollInfo.CalendarTypeID,
                IsOTEligible = dto.PayrollInfo.IsOTEligible,
                IsLeaveSalaryEligible = dto.PayrollInfo.IsLeaveSalaryEligible,
                IsEoSBEligible = dto.PayrollInfo.IsEoSBEligible,
                AcademicCalendarID = dto.PayrollInfo.AcademicCalendarID,
                LastWorkingDate = dto.LastWorkingDate,
                LeavingTypeID = dto.LeavingTypeID,
                ConfirmationDate = dto.ConfirmationDate,
                Grade = dto.Grade,
                AccomodationTypeID = dto.AccomodationTypeID,
                PassageTypeID = dto.PassageTypeID,
                LeaveGroupID = dto.LeaveGroupID,
                IsOverrideLeaveGroup = dto.IsOverrideLeaveGroup,
                CBSEID = dto.CBSEID,
                InActiveDate = dto.InActiveDate,
                TotalYearsofExperience = dto.AdditionalInfo.TotalYearsofExperience,
                MOIID = dto.PassportVisaInfo.MOIID,
                HealthCardNo = dto.PassportVisaInfo.HealthCardNo,
                LabourCardNo = dto.PassportVisaInfo.LabourCardNo,
                StatusID = dto.StatusID,

                TicketEligibleFromDate = dto.AirFareInfo.TicketEligibleFromDate,
                EmployeeCountryAirportID = dto.AirFareInfo.EmployeeCountryAirportID,
                ISTicketEligible = dto.AirFareInfo.ISTicketEligible,
                EmployeeNearestAirportID = dto.AirFareInfo.EmployeeNearestAirportID,
                TicketEntitilementID = dto.AirFareInfo.TicketEntitilementID,
                GenerateTravelSector = dto.AirFareInfo.GenerateTravelSector,
                LastTicketGivenDate = dto.AirFareInfo.LastTicketGivenDate,
                FlightClassID = dto.AirFareInfo.FlightClassID,
                IsTwoWay = dto.AirFareInfo.IsTwoWay,
                EmployeeBankDetails = dto.BankDetailInfo == null ? null : new List<Entity.Models.HR.EmployeeBankDetail>()
                {
                    new Entity.Models.HR.EmployeeBankDetail() {
                        AccountNo = dto.BankDetailInfo.AccountNo,
                        IBAN = dto.BankDetailInfo.IBAN,
                        SwiftCode = dto.BankDetailInfo.SwiftCode,
                        EmployeeBankIID = dto.BankDetailInfo.EmployeeBankIID,
                        EmployeeID = dto.EmployeeIID,
                        BankID = dto.BankDetailInfo.BankID,
                        ////TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                        CreatedBy = dto.CreatedBy,
                        UpdatedBy = dto.UpdatedBy,
                        CreatedDate = dto.CreatedDate,
                        UpdatedDate = dto.UpdatedDate,
                    }
                },

                PassportVisaDetails = dto.PassportVisaInfo == null ? null : new List<Entity.Models.HR.PassportVisaDetail>()
                {
                    new Entity.Models.HR.PassportVisaDetail() {
                        ReferenceID = dto.EmployeeIID,
                        PassportVisaIID = dto.PassportVisaInfo.PassportVisaIID,
                        PassportNo = dto.PassportVisaInfo.PassportNo,
                        CountryofIssueID = dto.PassportVisaInfo.CountryofIssueID,
                        PlaceOfIssue = dto.PassportVisaInfo.PlaceOfIssue,
                        PassportNoIssueDate = dto.PassportVisaInfo.PassportNoIssueDate,
                        PassportNoExpiry = dto.PassportVisaInfo.PassportNoExpiry,
                        VisaNo = dto.PassportVisaInfo.VisaNo,
                        VisaExpiry = dto.PassportVisaInfo.VisaExpiry,
                        NationalIDNo = dto.PassportVisaInfo.NationalIDNo,
                        NationalIDNoIssueDate = dto.PassportVisaInfo.NationalIDNoIssueDate,
                        NationalIDNoExpiry = dto.PassportVisaInfo.NationalIDNoExpiry,
                        UserType = dto.PassportVisaInfo.UserType,
                        //SponceredBy = dto.PassportVisaInfo.SponceredBy,
                        SponsorID = dto.PassportVisaInfo.SponsorID,
                    }
                },


                EmployeeAdditionalInfos = dto.AdditionalInfo == null ? null : new List<Entity.Models.HR.EmployeeAdditionalInfo>()
                {
                    new Entity.Models.HR.EmployeeAdditionalInfo() {
                        EmployeeID = dto.EmployeeIID,
                        EmployeeAdditionalInfoIID = dto.AdditionalInfo.EmployeeAdditionalInfoIID,
                        AdditioanalSubjectTought = dto.AdditionalInfo.AdditioanalSubjectTought,
                        AppointedSubject = dto.AdditionalInfo.AppointedSubject,
                        ClassessTaught = dto.AdditionalInfo.ClassessTaught,
                        HighestAcademicQualitication = dto.AdditionalInfo.HighestAcademicQualitication,
                        HighestPrefessionalQualitication = dto.AdditionalInfo.HighestPrefessionalQualitication,
                        MainSubjectTought = dto.AdditionalInfo.MainSubjectTought,
                        IsComputerTrained = dto.AdditionalInfo.IsComputerTrained,
                    }
                },
            };



            if (dto.EmployeeLeaveAllocationInfo.Count() != 0)
            {
                foreach (var leaveInfo in dto.EmployeeLeaveAllocationInfo)
                {
                    empEntity.EmployeeLeaveAllocations.Add(new EmployeeLeaveAllocation()
                    {
                        LeaveAllocationIID = leaveInfo.LeaveAllocationIID,
                        EmployeeID = dto.EmployeeIID,
                        AllocatedLeaves = leaveInfo.AllocatedLeaves,
                        LeaveTypeID = leaveInfo.LeaveTypeID
                    });
                };
            }
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var IIDs = dto.AcademicDetails
              .Select(a => a.EmployeeQualificationMapIID).ToList();

                //delete maps
                var entities = dbContext.EmployeeQualificationMaps.Where(x => x.EmployeeID == empEntity.EmployeeIID &&
                    !IIDs.Contains(x.EmployeeQualificationMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.EmployeeQualificationMaps.RemoveRange(entities);

                foreach (var academics in dto.AcademicDetails)
                {
                    empEntity.EmployeeQualificationMaps.Add(new EmployeeQualificationMap()
                    {
                        QualificationID = academics.QualificationID,
                        EmployeeQualificationMapIID = academics.EmployeeQualificationMapIID,
                        EmployeeID = dto.EmployeeIID,
                        ModeOfProgramme = academics.ModeOfProgramme,
                        TitleOfProgramme = academics.TitleOfProgramme,
                        University = academics.University,
                        MarksInPercentage = academics.MarksInPercentage,
                        Subject = academics.Subject,
                        GraduationMonth = academics.GraduationMonth,
                        GraduationYear = academics.GraduationYear
                    });
                };
            }
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var IIDs = dto.ExperienceDetails
              .Select(a => a.EmployeeExperienceDetailIID).ToList();

                //delete maps
                var entities = dbContext.EmployeeExperienceDetails.Where(x => x.EmployeeID == empEntity.EmployeeIID &&
                    !IIDs.Contains(x.EmployeeExperienceDetailIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.EmployeeExperienceDetails.RemoveRange(entities);

                foreach (var experience in dto.ExperienceDetails)
                {
                    empEntity.EmployeeExperienceDetails.Add(new EmployeeExperienceDetail()
                    {
                        EmployeeExperienceDetailIID = experience.EmployeeExperienceDetailIID,
                        EmployeeID = dto.EmployeeIID,
                        FromDate = experience.FromDate,
                        ToDate = experience.ToDate,
                        NameOfOraganizationtName = experience.NameOfOraganizationtName,
                        CurriculamOrIndustry = experience.CurriculamOrIndustry,
                        Designation = experience.Designation,
                        SubjectTaught = experience.SubjectTaught,
                        ClassTaught = experience.ClassTaught
                    });
                };
            }
            if (dto.Login != null)
            {
                if (dto.Login.LoginUserID == null)
                {
                    dto.Login.LoginUserID = empEntity.EmployeeCode;
                    dto.Login.LoginEmailID = empEntity.EmployeeCode + "." + empEntity.FirstName.ToLower() + defaultMailDomain;
                    dto.Login.IsRequired = true;
                    dto.Login.Password = "123456";
                }
                empEntity.Login = LoginMapper.Mapper(callContext).ToEntity(dto.Login);
            }

            foreach (var role in dto.EmployeeRoles)
            {
                empEntity.EmployeeRoleMaps.Add(new EmployeeRoleMap() { EmployeeRoleID = int.Parse(role.Key) });
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var IIDs = dto.RelationDetails
              .Select(a => a.EmployeeRelationsDetailIID).ToList();

                //delete maps
                var entities = dbContext.EmployeeRelationsDetails.Where(x => x.EmployeeID == empEntity.EmployeeIID &&
                    !IIDs.Contains(x.EmployeeRelationsDetailIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.EmployeeRelationsDetails.RemoveRange(entities);

                dbContext.SaveChanges();

                foreach (var relation in dto.RelationDetails)
                {
                    empEntity.EmployeeRelationsDetails.Add(new EmployeeRelationsDetail()
                    {
                        EmployeeRelationTypeID = relation.EmployeeRelationTypeID,
                        EmployeeRelationsDetailIID = relation.EmployeeRelationsDetailIID,
                        EmployeeID = dto.EmployeeIID,
                        FirstName = relation.FirstName,
                        MiddleName = relation.MiddleName,
                        LastName = relation.LastName,
                        PassportNo = relation.PassportNo,
                        ContactNo = relation.ContactNo,
                        NationalIDNo = relation.NationalIDNo,
                    });
                };
            }

            if (dto.EmployeeIID != 0 && dto.IsActive == false)
            {
                bool alreadyEmployee = false;

                using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
                {
                    alreadyEmployee = dbContext.Employees.Where(a => a.EmployeeIID == dto.EmployeeIID && a.IsActive == true).Any();

                    if (alreadyEmployee)
                    {
                        var staffTransport = dbContext.StaffRouteStopMaps.Where(a => a.StaffID == dto.EmployeeIID && a.IsActive == true).ToList();

                        if (staffTransport.Count() > 0)
                        {
                            staffTransport.ForEach(st =>
                            {
                                st.IsActive = false;
                                st.DateTo = DateTime.Now;
                                st.CancelDate = DateTime.Now;
                            });

                            dbContext.StaffRouteStopMaps.UpdateRange(staffTransport);
                        }

                        dbContext.SaveChanges();
                    }
                }

                if (alreadyEmployee)
                {
                    using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
                    {
                        var employeeStructure = dbContext.EmployeeSalaryStructures.Where(a => a.EmployeeID == dto.EmployeeIID && a.IsActive == true).ToList();

                        if (employeeStructure.Count() > 0)
                        {
                            employeeStructure.ForEach(st =>
                            {
                                st.IsActive = false;
                            });

                            dbContext.EmployeeSalaryStructures.UpdateRange(employeeStructure);
                        }

                        dbContext.SaveChanges();
                    }
                }
            }

            return empEntity;
        }

        public bool SaveEmployeeReq(EmploymentServiceDTO dto)
        {
            //var mapper = Mappers.EmploymentService.EmploymentServiceMapper.Mapper(_callContext);
            //var serviceDTO = new EmploymentServiceRepository().SaveEmploymentRequest(mapper.ToEntity(dto), false, 0);
            //if (serviceDTO.IsNotNull())
            //{
            //    return true;
            //}
            //return false;
            return true;
        }


        public KeyValueDTO ValidateEmployeeRequest(EmploymentRequestDTO dto, string fieldName = "")
        {
            string errorMessage = string.Empty;
            bool result = true;

            //switch (fieldName)
            //{
            //    case "RecruitmentType":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidateRecruitmentType(out errorMessage);
            //        break;
            //    case "CIVILID":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidateCivilID(out errorMessage);
            //        break;
            //    case "Agent":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidateAgent(out errorMessage);
            //        break;
            //    case "replacedEmployee":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidateReplacedEmployee(out errorMessage);
            //        break;
            //    case "DOB":
            //    case "DATE_OF_BIRTH":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidateDOB(out errorMessage);
            //        break;
            //    case "BasicSalary":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidateBasicSalary(out errorMessage);
            //        break;
            //    case "PASSPORT_NO":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).ValidatePassportNo(out errorMessage);
            //        break;
            //    case "":
            //        result = Validators.HR.EmploymentRequestValidator.Validator(dto).Validate(out errorMessage);
            //        break;
            //}

            return new KeyValueDTO() { Key = result.ToString(), Value = errorMessage };
        }

        public string getNextNo(int desigCode, string docType)
        {
            //return new EmploymentServiceRepository().GetNextEmployeeRequestNumberDisplay(1, desigCode.ToString(), docType, "");
            return null;
        }

        public EmploymentRequestDTO SaveEmploymentRequest(EmploymentRequestDTO dto)
        {
            //var mapper = Mappers.EmploymentService.EmploymentServiceMapper.Mapper(_callContext);
            //var allowancemapper = Mappers.EmploymentService.EmploymentAllowanceMapper.Mapper(_callContext);
            //var documentMapper = Mappers.EmploymentService.EmploymentDocumentMapper.Mapper(_callContext);

            //var proposedIncreaseMapper = Mappers.EmploymentService.EmploymentProposedIncreaseMapper.Mapper(_callContext);
            //var requestEntity = mapper.ToEntity(dto);
            //var isNewRequest = false;
            //if (dto.isNewRequest.HasValue)
            //{
            //    requestEntity.UPD_BY = "TEMPREQ";
            //    requestEntity.UPD_IP = _callContext.IPAddress;
            //    requestEntity.UPD_WEBUSER = _callContext.LoginID.ToString();
            //    requestEntity.UPD_DT = DateTime.Now;
            //    requestEntity.CRE_WEBUSER = !string.IsNullOrEmpty(dto.CRE_WEBUSER) ? dto.CRE_WEBUSER.ToString() : "";
            //    requestEntity.CRE_IP = dto.CRE_IP;
            //    requestEntity.CRE_DT = dto.CRE_DT;
            //    requestEntity.CRE_BY = dto.CRE_BY;
            //    requestEntity.REQ_DT = dto.REQ_DT;
            //}
            //else
            //{
            //    isNewRequest = true;
            //    requestEntity.CRE_WEBUSER = _callContext.LoginID.ToString();
            //    requestEntity.CRE_IP = _callContext.IPAddress;
            //    requestEntity.CRE_DT = DateTime.Now;
            //    requestEntity.CRE_BY = "TEMPREQ";
            //    requestEntity.REQ_DT = DateTime.Now;
            //    dto.CRE_WEBUSER = _callContext.LoginID.ToString();
            //    dto.CRE_IP = _callContext.IPAddress;
            //    dto.CRE_DT = DateTime.Now;
            //    dto.CRE_BY = "TEMPREQ";
            //    dto.REQ_DT = DateTime.Now;
            //    dto.EMP_REQ_NO = requestEntity.EMP_REQ_NO;
            //    dto.EMP_NO = requestEntity.EMPNO;


            //}


            //var serviceDTO = new EmploymentServiceRepository().SaveEmploymentRequest(requestEntity, dto.isNewRequest, long.Parse(dto.EmpProcessRequestStatus.Key));

            //if (serviceDTO.IsNotNull())
            //{

            //    var proposedIncrease = new List<HR_EMP_REQ_SALCHG>();

            //    foreach (var increase in dto.ProposedIncrease)
            //    {
            //        if (increase.IsNotNull())
            //        {
            //            var pEntity = proposedIncreaseMapper.ToEntity(increase);
            //            if (isNewRequest)
            //            {
            //                pEntity.CRE_WEBUSER = _callContext.LoginID.ToString();
            //                pEntity.CRE_IP = _callContext.IPAddress;
            //                pEntity.CRE_DT = DateTime.Now;
            //                pEntity.CRE_BY = "TEMPREQ";
            //                pEntity.CNTRYCD = serviceDTO.CNTRYCD;
            //                pEntity.STATUS = "";
            //                pEntity.EMP_REQ_NO = serviceDTO.EMP_REQ_NO;
            //            }
            //            else
            //            {
            //                pEntity.UPD_WEBUSER = _callContext.LoginID.ToString();
            //                pEntity.UPD_IP = _callContext.IPAddress;
            //                pEntity.UPD_DT = DateTime.Now;
            //                pEntity.UPD_BY = "TEMPREQ";

            //                pEntity.CRE_WEBUSER = increase.CRE_WEBUSER;
            //                pEntity.CRE_IP = increase.CRE_IP;
            //                pEntity.CRE_DT = increase.CRE_DT;
            //                pEntity.CRE_BY = increase.CRE_BY;
            //                pEntity.CNTRYCD = serviceDTO.CNTRYCD;
            //                pEntity.STATUS = increase.Status;
            //                pEntity.EMP_REQ_NO = serviceDTO.EMP_REQ_NO;
            //            }
            //            proposedIncrease.Add(pEntity);

            //        }
            //    }

            //    if (proposedIncrease.Count > 0)
            //    {
            //        var proposedList = new EmploymentServiceRepository().SaveEmploymentProposedIncrease(proposedIncrease);
            //    }


            //    var allowancelist = new List<HR_EMP_REQ_ALLOW>();
            //    foreach (var allowance in dto.Allowance)
            //    {
            //        if (allowance.IsNotNull())
            //        {
            //            var entity = allowancemapper.ToEntity(allowance);
            //            entity.PAYCOMP = requestEntity.PAYCOMP;
            //            entity.CNTRYCD = requestEntity.CNTRYCD;
            //            entity.EMP_REQ_NO = requestEntity.EMP_REQ_NO.Value;
            //            entity.PAYCOMP = requestEntity.PAYCOMP;
            //            entity.EMPNO = requestEntity.EMPNO;
            //            entity.STATUS = requestEntity.STATUS;
            //            if (isNewRequest)
            //            {
            //                entity.CRE_WEBUSER = _callContext.LoginID.ToString();
            //                entity.CRE_IP = _callContext.IPAddress;
            //                entity.CRE_DT = DateTime.Now;
            //                entity.CRE_BY = "TEMPREQ";
            //                entity.REQ_DT = DateTime.Now;

            //                allowance.CRE_WEBUSER = _callContext.LoginID.ToString();
            //                allowance.CRE_IP = _callContext.IPAddress;
            //                allowance.CRE_DT = DateTime.Now;
            //                allowance.CRE_BY = "TEMPREQ";
            //                allowance.REQ_DT = DateTime.Now;
            //            }
            //            else
            //            {
            //                entity.UPD_WEBUSER = _callContext.LoginID.ToString();
            //                entity.UPD_IP = _callContext.IPAddress;
            //                entity.UPD_DT = DateTime.Now;
            //                entity.UPD_BY = "TEMPREQ";

            //                entity.CRE_WEBUSER = allowance.CRE_WEBUSER;
            //                entity.CRE_IP = allowance.CRE_IP;
            //                entity.CRE_DT = allowance.CRE_DT;
            //                entity.CRE_BY = allowance.CRE_BY;
            //                entity.REQ_DT = allowance.REQ_DT;
            //            }
            //            allowancelist.Add(entity);
            //        }
            //    }
            //    if (allowancelist.Count > 0)
            //    {
            //        var list = new EmploymentServiceRepository().SaveEmploymentAllowance(allowancelist);
            //    }
            //    var documentList = new List<HR_EMP_REQ_DOCUMENTS>();
            //    foreach (var document in dto.documents)
            //    {
            //        if (document.IsNotNull())
            //        {
            //            var entity = documentMapper.ToEntity(document);
            //            entity.EMP_NO = dto.EMP_NO.Value;
            //            entity.EMP_REQ_NO = dto.EMP_REQ_NO.Value;
            //            documentList.Add(entity);
            //        }
            //    }
            //    if (documentList.Count > 0)
            //    {
            //        var doclist = new EmploymentServiceRepository().SaveEmploymentDocuments(documentList);
            //    }
            //    //if (isNewRequest)
            //    //{
            //    var isNotificationAdded = new Eduegate.Domain.Repository.NotificationRepository().AddNotificationAlert(long.Parse(dto.EMP_REQ_NO.Value.ToString()), AlertTypeEnum.EmploymentRequest, long.Parse(_callContext.LoginID.ToString()), dto.F_Name + " " + dto.M_Name + " " + dto.L_Name + ", " + dto.Designation.Value + " ," + dto.Shop.Value + " ," + dto.Location.Value);


            //    //var dtoWorkFlow = SaveWorkFlow(dto);

            //    //}

            //}
            //return GetEmploymentRequest(dto.EMP_REQ_NO.Value);
            return null;
        }

        public EmploymentRequestDTO GetEmploymentRequest(long RequestID)
        {
            //var EmpServiceRepository = new EmploymentServiceRepository();
            //var employeeService = EmpServiceRepository.GetEmploymentRequest(RequestID);
            //var allowances = EmpServiceRepository.GetEmploymentRequestAllowances(RequestID);
            //var proposedIncreases = EmpServiceRepository.GetEmploymentRequestProposedIncreases(RequestID);
            //var documents = EmpServiceRepository.GetEmploymentRequestDocuments(RequestID);
            //var mapper = Mappers.EmploymentService.EmploymentServiceMapper.Mapper(_callContext);
            //var allowancemapper = Mappers.EmploymentService.EmploymentAllowanceMapper.Mapper(_callContext);
            //var documentmapper = Mappers.EmploymentService.EmploymentDocumentMapper.Mapper(_callContext);
            //var proposedIncreasemapper = Mappers.EmploymentService.EmploymentProposedIncreaseMapper.Mapper(_callContext);
            //var dto = mapper.ToDTO(employeeService);
            //var allowanceDTOList = allowancemapper.ToDTOList(allowances);
            //var documentDTOList = documentmapper.ToDTOList(documents);
            //var proposedIncreaseDTOList = proposedIncreasemapper.ToDTOList(proposedIncreases);
            //if (allowanceDTOList.IsNotNull() && allowanceDTOList.Count > 0)
            //    dto.Allowance = allowanceDTOList;
            //if (documentDTOList.IsNotNull() && documentDTOList.Count > 0)
            //    dto.documents = documentDTOList;
            //if (proposedIncreaseDTOList.IsNotNull() && proposedIncreaseDTOList.Count > 0)
            //    dto.ProposedIncrease = proposedIncreaseDTOList;
            //return dto;
            return null;
        }

        public List<EmployementAllowanceDTO> GetEmploymentAllowance(long RequestID)
        {
            //var EmpServiceRepository = new EmploymentServiceRepository();
            //var allowances = EmpServiceRepository.GetEmploymentRequestAllowances(RequestID);
            //var allowancemapper = Mappers.EmploymentService.EmploymentAllowanceMapper.Mapper(_callContext);
            //var allowanceDTOList = allowancemapper.ToDTOList(allowances);

            //return allowanceDTOList;
            return null;
        }

        public List<HRV_CompanyMasterDTO> GetCompanyList()
        {
            //EmploymentServiceRepository EmpServiceRepository = new EmploymentServiceRepository();
            //var companyList = EmpServiceRepository.GetCompanyList();

            //var mapper = Mappers.EmploymentService.HRV_CompanyMasterMapper.Mapper(_callContext);
            //var CompanyMasterDTO = new List<HRV_CompanyMasterDTO>();
            //foreach (var item in companyList)
            //{
            //    CompanyMasterDTO.Add(mapper.ToDTO(item));
            //}

            ////var dto = mapper.ToDTO(item);
            //return CompanyMasterDTO;
            return null;
        }

        public List<KeyValueDTO> GetVisaCompany(long shopCode)
        {
            //var keyvalueList = new List<KeyValueDTO>();
            //var empServiceRepository = new EmploymentServiceRepository();
            //var companyList = empServiceRepository.GetCompanyShopLocation(shopCode);

            //foreach (var company in companyList)
            //{
            //    var visaCode = empServiceRepository.GetVisaCompany(company.COMPANY_CODE).FirstOrDefault();

            //    keyvalueList.Add(new KeyValueDTO()
            //    {
            //        Key = company.COMPANYSHOPLOCID.ToString(),
            //        Value = visaCode.COMPANY_SHORT_NAME + " - " + company.LICENCE_NUMBER + " - " + company.PACI_NUMBER.ToString()
            //    });
            //}

            //return keyvalueList;
            return null;
        }

        public KeyValueDTO GetDefaultVisaCompany(long shopCode)
        {
            //var empServiceRepository = new EmploymentServiceRepository();
            //var company = empServiceRepository.GetCompanyDefaultShopLocation(shopCode).FirstOrDefault();
            //var keyvalueDTO = new KeyValueDTO() { Key = "", Value = "" };
            //if (company.IsNotNull() && company.IsNotDefault())
            //{
            //    var visaCode = empServiceRepository.GetVisaCompany(company.COMPANY_CODE).FirstOrDefault();

            //    keyvalueDTO = new KeyValueDTO()
            //    {
            //        Key = company.COMPANYSHOPLOCID.ToString(),
            //        Value = visaCode.COMPANY_SHORT_NAME + " - " + company.LICENCE_NUMBER + " - " + company.PACI_NUMBER.ToString()
            //    };

            //}

            //return keyvalueDTO;
            return null;
        }

        public KeyValueDTO GetCompanyShopLocationByID(long requestNo)
        {
            //var keyvalueList = new List<KeyValueDTO>();
            //var empServiceRepository = new EmploymentServiceRepository();
            //var company = empServiceRepository.GetCompanyShopLocationByID(requestNo);
            //var keyvalueDTO = new KeyValueDTO() { Key = "", Value = "" };
            //if (company.IsNotNull() && company.IsNotDefault())
            //{
            //    var visaCode = empServiceRepository.GetVisaCompany(company.COMPANY_CODE).FirstOrDefault();

            //    keyvalueDTO = new KeyValueDTO()
            //   {
            //       Key = company.COMPANYSHOPLOCID.ToString(),
            //       Value = visaCode.COMPANY_SHORT_NAME + " - " + company.LICENCE_NUMBER + " - " + company.PACI_NUMBER.ToString()
            //   };
            //}

            //return keyvalueDTO;
            return null;
        }

        public EmploymentRequestDTO SaveWorkFlow(EmploymentRequestDTO dto)
        {
            //var isUpdated = new EmploymentServiceRepository().SaveWorkFlow(dto.EMP_REQ_NO.Value, dto.EMP_NO.Value, long.Parse(dto.VisaCompany.Key), long.Parse(dto.QuotaType.Key), dto.PersonalRemarks, _callContext.LoginID.HasValue ? _callContext.LoginID.Value : default(long), _callContext.IPAddress, long.Parse(dto.EmpRequestStatus.Key), long.Parse(dto.EmpProcessRequestStatus.Key));
            ////isUpdated = new EmploymentServiceRepository().SaveWorkFlowStatus(dto.EMP_REQ_NO.Value, dto.EMP_NO.Value, 1, 1, _callContext.LoginID.HasValue ? _callContext.LoginID.Value : default(long), _callContext.IPAddress); // Hardcoded the both the status ID 
            //return GetEmploymentRequest(dto.EMP_REQ_NO.Value);
            return null;
        }


        public List<KeyValueDTO> GetEmploymentRequestStatus(long empReqNo)
        {
            var keyvalueList = new List<KeyValueDTO>();
            //var empServiceRepository = new EmploymentServiceRepository();
            //var requestStatuses = empServiceRepository.GetEmploymentRequestStatus();
            //if (requestStatuses.IsNotNull() && requestStatuses.IsNotDefault())
            //{
            //    foreach (var status in requestStatuses)
            //    {
            //        keyvalueList.Add(new KeyValueDTO()
            //        {
            //            Key = status.CODE.ToString(),
            //            Value = status.STATUSNAME
            //        });
            //    }
            //}

            var claims = new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_callContext.LoginID.Value, (int)Eduegate.Framework.Security.Enums.ClaimType.Roles).FirstOrDefault();

            //var secured = new Eduegate.Framework.Security.Secured.SecuredData(claims);
            if (claims.IsNotNull() && empReqNo.IsNotNull())
            {
                if (empReqNo == 0)
                {
                    keyvalueList.Add(new KeyValueDTO()
                    {
                        Key = "1",
                        Value = "Draft"
                    });
                }
                else
                {
                    var finalClaims = claims.Split('.');

                    if (finalClaims.Length > 1)
                    {
                        switch (finalClaims[1])
                        {
                            case "L1":
                                keyvalueList.Add(new KeyValueDTO()
                                {
                                    Key = "1",
                                    Value = "Draft"
                                });
                                keyvalueList.Add(new KeyValueDTO()
                                {
                                    Key = "2",
                                    Value = "Sent for Approval"
                                });
                                break;
                            case "L2":
                                keyvalueList.Add(new KeyValueDTO()
                                {
                                    Key = "1",
                                    Value = "Draft"
                                });
                                break;
                            default:
                                keyvalueList.Add(new KeyValueDTO()
                                {
                                    Key = "",
                                    Value = ""
                                });
                                break;
                        }
                    }
                    else
                    {
                        keyvalueList.Add(new KeyValueDTO()
                        {
                            Key = "",
                            Value = ""
                        });
                    }
                }
            }
            else
            {
                keyvalueList.Add(new KeyValueDTO()
                {
                    Key = "",
                    Value = ""
                });
            }

            return keyvalueList;

        }

        //public JobOpeningDTO GetJobOpening(string jobID)
        //{
        //    var mapper = JobOpeningMapper.Mapper(_callContext);
        //    var job = new Eduegate.Domain.Repository.HR.EmploymentServiceRepository().GetJobOpening(long.Parse(jobID));
        //    var jobCultureData = new Eduegate.Domain.Repository.HR.EmploymentServiceRepository().GetAvailableJobCultureData(long.Parse(jobID));
        //    return mapper.ToDTO(job, jobCultureData);
        //    //return null;
        //}

        //public List<JobOpeningDTO> GetJobOpenings(string filter)
        //{
        //    var mapper = JobOpeningMapper.Mapper(_callContext);
        //    return mapper.ToDTO(new Eduegate.Domain.Repository.HR.EmploymentServiceRepository().GetJobOpenings(filter));
        //    //return null;
        //}

        public List<JobDepartmentDTO> GetJobDepartments(string filter)
        {
            var mapper = JobDepartmentMapper.Mapper(_callContext);
            return mapper.ToDTO(new Eduegate.Domain.Repository.HR.EmploymentServiceRepository().GetDepartments(filter));
            //return null;
        }

        //public JobOpeningDTO SaveJobOpening(JobOpeningDTO dto)
        //{
        //    var mapper = JobOpeningMapper.Mapper(_callContext);
        //    //var resultDTO = mapper.ToDTO(new Repository.HR.EmploymentServiceRepository().SaveJobOpening(mapper.ToEntity(dto)));
        //    var updatedEntiy = new Repository.HR.EmploymentServiceRepository().SaveJobOpening(mapper.ToEntity(dto));
        //    new Repository.HR.EmploymentServiceRepository().SaveJobOpeningCultureData(mapper.ToEntity(dto.CultureDatas));
        //    new Eduegate.Domain.Repository.Security.SecurityRepository().CreateOrUpdateClaimsByResource(updatedEntiy.JobIID.ToString(), dto.JobTitle, (int)Framework.Security.Enums.ClaimType.JobOpening);
        //    return GetJobOpening(updatedEntiy.JobIID.ToString());
        //}

        public bool ArchiveJobProfile(string applicationID, string status)
        {
            try
            {
                new Eduegate.Domain.Repository.HR.EmploymentServiceRepository().ArchiveJobProfile(applicationID.Split(',').Where(a => !string.IsNullOrEmpty(a)).ToList(), status);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public EmployeeDTO GetEmployeeDataByLogin(long? loginID)
        {
            var dto = new EmployeeDTO();

            var employee = repository.GetEmployeeDataByLogin(loginID);

            if (employee != null)
            {
                dto = FromEntity(employee, _callContext);
            }

            return dto;
        }

        public EmployeeDTO GetEmployeeDetailsByEmployeeID(long? employeeID)
        {
            var empDetails = new EmployeeDTO();

            var getEmpDetails = new EmployeeRepository().GetEmployeeByEmployeeID(employeeID);

            if (getEmpDetails != null)
            {
                empDetails.EmployeeIID = getEmpDetails.EmployeeIID;
                empDetails.EmployeeCode = getEmpDetails.EmployeeCode;
                empDetails.FirstName = getEmpDetails.FirstName;
                empDetails.MiddleName = getEmpDetails.MiddleName;
                empDetails.LastName = getEmpDetails.LastName;
                empDetails.DepartmentName = getEmpDetails.Department.DepartmentName;
                empDetails.DepartmentID = getEmpDetails.DepartmentID;
            }
            return empDetails;
        }

        public EmployeeDTO GetCandidateFromInterviewMap(long ID)
        {
            var dtoList = new EmployeeDTO();
            dtoList = new EmployeeRepository().GetCandidateFromInterviewMap(ID);

            return dtoList;
        }

    }
}