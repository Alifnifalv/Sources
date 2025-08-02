using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Mutual;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Models.HR;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Security.Secured;
using Eduegate.Framework.Contracts.Common.PageRender;
using System.ServiceModel;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Mappers.School.Mutual
{
    public class SchoolsMapper : DTOEntityDynamicMapper
    {
        public static SchoolsMapper Mapper(CallContext context)
        {
            var mapper = new SchoolsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SchoolsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public SchoolsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.School
                    .Include(x => x.SchoolPayerBankDetailMaps)
                    .ThenInclude(y => y.Bank)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.SchoolID == IID);

                var toDto = new SchoolsDTO();

                toDto = new SchoolsDTO()
                {
                    SchoolID = entity.SchoolID,
                    SchoolName = entity.SchoolName,
                    SponsorID = entity.SponsorID,
                    Description = entity.Description,
                    Place = entity.Place,
                    SchoolCode = entity.SchoolCode,
                    Address1 = entity.Address1,
                    Address2 = entity.Address2,
                    RegistrationID = entity.RegistrationID,
                    CompanyID = entity.CompanyID,
                    EmployerEID = entity.EmployerEID,
                    PayerEID = entity.PayerEID,
                    PayerQID = entity.PayerQID,
                    SchoolShortName = entity.SchoolShortName,
                    ProfileContentID = entity.SchoolProfileID,
                    SchoolSealContentID = entity.SchoolSealID,
                    SchoolLogoContentID = entity.SchoolLogoContentID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };

                foreach (var item in entity.SchoolPayerBankDetailMaps)
                {
                    toDto.PayerBankDTO.Add(new SchoolPayerBankDTO()
                    {
                        PayerBankDetailIID = item.PayerBankDetailIID,
                        SchoolID = item.SchoolID,
                        BankID = item.BankID,

                        Bank = item.BankID.HasValue ? new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                        {
                            Key = item.BankID.ToString(),
                            Value = item.Bank?.BankName
                        } : new KeyValueDTO(),

                        IsMainOperating = item.IsMainOperating,
                        PayerBankShortName = item.PayerBankShortName,
                        PayerIBAN = item.PayerIBAN,
                    });
                }

                return toDto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SchoolsDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new Schools()
                {
                    SchoolID = toDto.SchoolID,
                    SponsorID = toDto.SponsorID,
                    SchoolName = toDto.SchoolName,
                    Description = toDto.Description,
                    Address1 = toDto.Address1,
                    Address2 = toDto.Address2,
                    EmployerEID = toDto.EmployerEID,
                    PayerEID = toDto.PayerEID,
                    PayerQID = toDto.PayerQID,
                    RegistrationID = toDto.RegistrationID,
                    CompanyID = toDto.CompanyID,
                    SchoolCode = toDto.SchoolCode,
                    Place = toDto.Place,
                    SchoolShortName = toDto.SchoolShortName,
                    SchoolProfileID = toDto.ProfileContentID,
                    SchoolSealID = toDto.SchoolSealContentID,
                    SchoolLogoContentID = toDto.SchoolLogoContentID,
                    CreatedBy = toDto.SchoolID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SchoolID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SchoolID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SchoolID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                var IIDs = toDto.PayerBankDTO
                    .Select(a => a.PayerBankDetailIID).ToList();

                //delete maps
                var entities = dbContext.SchoolPayerBankDetailMaps.Where(x =>
                    x.SchoolID == entity.SchoolID &&
                    !IIDs.Contains(x.PayerBankDetailIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.SchoolPayerBankDetailMaps.RemoveRange(entities);

                foreach (var item in toDto.PayerBankDTO)
                {
                    entity.SchoolPayerBankDetailMaps.Add(new SchoolPayerBankDetailMap()
                    {
                        PayerBankDetailIID = item.PayerBankDetailIID,
                        SchoolID = item.SchoolID,
                        BankID = item.BankID,
                        IsMainOperating = item.IsMainOperating,
                        PayerBankShortName = item.PayerBankShortName,
                        PayerIBAN = item.PayerIBAN,
                        CreatedBy = item.PayerBankDetailIID == 0 ? (int)_context.LoginID : item.CreatedBy,
                        CreatedDate = item.PayerBankDetailIID == 0 ? DateTime.Now : item.CreatedDate,
                        UpdatedBy = item.PayerBankDetailIID == 0 ? item.UpdatedBy : (int)_context.LoginID,
                        UpdatedDate = item.PayerBankDetailIID == 0 ? item.UpdatedDate : DateTime.Now,
                    });
                }

                if (entity.SchoolID == 0)
                {
                    var school = dbContext.School.ToList();
                    var maxSchoolID = school.Max(a => a.SchoolID);
                    entity.SchoolID = (byte)(maxSchoolID + 1);

                    dbContext.School.Add(entity);

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.SchoolPayerBankDetailMaps)
                    {
                        if (map.PayerBankDetailIID == 0)
                        {
                            dbContext.Entry(map).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(map).State = EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.SchoolID));
            }
        }

        public List<KeyValueDTO> GetSchoolsByParentLoginID(long? loginID)
        {
            var schoolLookupList = new List<KeyValueDTO>();

            var schoolDataList = new List<KeyValueDTO>();

            if (!loginID.HasValue)
            {
                loginID = _context.LoginID.HasValue ? _context.LoginID : 0;
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var students = dbContext.Students.Where(x => x.Parent.LoginID == loginID)
                    .Include(i => i.Parent)
                    .Include(i => i.School)
                    .AsNoTracking()
                    .ToList();

                foreach (var stud in students)
                {
                    if (stud.School != null)
                    {
                        schoolDataList.Add(new KeyValueDTO()
                        {
                            Key = stud.School.SchoolID.ToString(),
                            Value = stud.School.SchoolName
                        });
                    }
                }

                //Get distinct data of schools lookup data start
                if (schoolDataList.Count > 0)
                {
                    var distinctSchoolList = new List<KeyValueDTO>();
                    var start = false;
                    var count = 0;

                    for (var j = 0; j < schoolDataList.Count; j++)
                    {
                        for (var k = 0; k < distinctSchoolList.Count; k++)
                        {
                            if (schoolDataList[j].Key == distinctSchoolList[k].Key)
                            {
                                start = true;
                            }
                        }
                        count++;
                        if (count == 1 && start == false)
                        {
                            distinctSchoolList.Add(schoolDataList[j]);
                        }
                        start = false;
                        count = 0;
                    }

                    foreach (var school in distinctSchoolList)
                    {
                        if (school.Key != null)
                        {
                            schoolLookupList.Add(new KeyValueDTO()
                            {
                                Key = school.Key,
                                Value = school.Value
                            });
                        }
                    }
                    //Get distinct data of schools lookup data end
                }

                return schoolLookupList;
            }
        }

        public BankAccountDTO GetBankDetailsByBankID(long bankID)
        {
            var bankDetails = new BankAccountDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getBanks = dbContext.Banks
                    .AsNoTracking()
                    .FirstOrDefault(b => b.BankIID == bankID);

                if (getBanks != null)
                {
                    bankDetails = new BankAccountDTO()
                    {
                        BankShortName = getBanks.ShortName,
                    };
                }
                else
                {
                    return null;
                }
            }
            return bankDetails;
        }

        public List<SchoolPayerBankDTO> FillPayerBanksBySchoolID(long schoolID)
        {
            var bankDetails = new List<SchoolPayerBankDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getBanks = dbContext.SchoolPayerBankDetailMaps
                    .Include(b => b.Bank)
                    .Include(b => b.School)
                    .AsNoTracking()
                    .Where(b => b.SchoolID == schoolID).ToList();

                if (getBanks != null)
                {
                    foreach (var b in getBanks)
                    {
                        bankDetails.Add(new SchoolPayerBankDTO()
                        {
                            PayerBankDetailIID = b.PayerBankDetailIID,
                            BankID = b.BankID,
                            Bank = b.BankID.HasValue ? new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                            {
                                Key = b.BankID.ToString(),
                                Value = b.Bank?.BankName
                            } : new KeyValueDTO(),
                            PayerIBAN = b.PayerIBAN,
                            PayerBankShortName = b.PayerBankShortName,
                            EmployerEID = b.School?.EmployerEID,
                            PayerQID = b.School?.PayerQID,
                            PayerEID = b.School?.PayerEID,
                            School = b.SchoolID.HasValue ? new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                            {
                                Key = b.SchoolID.ToString(),
                                Value = b.School?.SchoolName
                            } : new KeyValueDTO(),
                        });
                    }
                }
                else
                {
                    return null;
                }
            }
            return bankDetails;
        }

        public List<SchoolsDTO> GetSchoolsProfileWithAcademicYear()
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var schoolDTOs = new List<SchoolsDTO>();

                var schools = dbContext.School.Include(i => i.AcademicYears).AsNoTracking().ToList();
                var secured1 = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_context.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.School));

                if (schools != null)
                {
                    foreach (var scl in schools)
                    {
                        var hasClaims = secured1.HasAccess(scl.SchoolID, (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.School.ToString()));
                        if (hasClaims) 
                        {
                            var academicYears = new List<KeyValueDTO>();

                            foreach (var ay in scl.AcademicYears)
                            {
                                if (ay.IsActive == true)
                                {
                                    academicYears.Add(new KeyValueDTO()
                                    {
                                        Key = ay.AcademicYearID.ToString(),
                                        Value = ay.Description + ' ' + '(' + ay.AcademicYearCode + ')'
                                    });
                                }
                            }

                            schoolDTOs.Add(new SchoolsDTO()
                            {
                                SchoolID = scl.SchoolID,
                                SchoolName = scl.SchoolName,
                                ProfileContentID = scl.SchoolProfileID,
                                AcademicYears = academicYears,
                            });
                        }
                    }
                }

                return schoolDTOs;
            }
        }

        public List<KeyValueDTO> GetSchoolsByLoginIDActiveStuds(long? loginID)
        {
            var schoolLookupList = new List<KeyValueDTO>();

            var schoolDataList = new List<KeyValueDTO>();

            if (!loginID.HasValue)
            {
                loginID = _context.LoginID.HasValue ? _context.LoginID : 0;
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var students = dbContext.Students.Where(x => x.Parent.LoginID == loginID && x.IsActive == true)
                    .Include(i => i.Parent)
                    .Include(i => i.School)
                    .AsNoTracking()
                    .ToList();

                foreach (var stud in students)
                {
                    if (stud.School != null)
                    {
                        schoolDataList.Add(new KeyValueDTO()
                        {
                            Key = stud.School.SchoolID.ToString(),
                            Value = stud.School.SchoolName
                        });
                    }
                }

                //Get distinct data of schools lookup data start
                if (schoolDataList.Count > 0)
                {
                    var distinctSchoolList = new List<KeyValueDTO>();
                    var start = false;
                    var count = 0;

                    for (var j = 0; j < schoolDataList.Count; j++)
                    {
                        for (var k = 0; k < distinctSchoolList.Count; k++)
                        {
                            if (schoolDataList[j].Key == distinctSchoolList[k].Key)
                            {
                                start = true;
                            }
                        }
                        count++;
                        if (count == 1 && start == false)
                        {
                            distinctSchoolList.Add(schoolDataList[j]);
                        }
                        start = false;
                        count = 0;
                    }

                    foreach (var school in distinctSchoolList)
                    {
                        if (school.Key != null)
                        {
                            schoolLookupList.Add(new KeyValueDTO()
                            {
                                Key = school.Key,
                                Value = school.Value
                            });
                        }
                    }
                    //Get distinct data of schools lookup data end
                }

                return schoolLookupList;
            }
        }

        public KeyValueDTO GetCurrentAcademicYearBySchoolID(long schoolID)
        {
            var academicYear = new KeyValueDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("CURRENT_ACADEMIC_YEAR_STATUSID");

                var academicYearDatas = dbContext.AcademicYears.Where(a => a.SchoolID == schoolID && a.AcademicYearStatusID == currentAcademicStatusID && a.IsActive == true).AsNoTracking().FirstOrDefault();

                return new KeyValueDTO() { 
                    Key = academicYearDatas?.AcademicYearID.ToString(),
                    Value = academicYearDatas?.Description,
                };
            }
        }
        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            var toDto = new PowerBiDashBoardDTO();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var dat = db.Dashboards.FirstOrDefault(x => x.PageID == pageID);

                if (dat != null)
                {
                    toDto.MenuLinkID = dat.MenuLinkID;
                    toDto.PageID = dat.PageID;
                    toDto.ReportID = dat.ReportID;
                    toDto.ClientID = dat.ClientID;
                    toDto.WorkspaceID = dat.WorkspaceID;
                    toDto.Dsh_ID = dat.Dsh_ID;
                    toDto.ClientSecret = dat.ClientSecret;
                    toDto.TenantID = dat.TenantID;
                }
            }

            return toDto;
        }
    }

}