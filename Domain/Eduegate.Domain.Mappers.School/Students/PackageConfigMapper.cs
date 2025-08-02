using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class PackageConfigMapper : DTOEntityDynamicMapper
    {
        public static PackageConfigMapper Mapper(CallContext context)
        {
            var mapper = new PackageConfigMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PackageConfigDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public PackageConfigDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.PackageConfigs.Where(a => a.PackageConfigIID == IID)
                    .Include(i => i.Account)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.PackageConfigStudentMaps).ThenInclude(i => i.Student)
                    .Include(i => i.PackageConfigStudentGroupMaps).ThenInclude(i => i.StudentGroup)
                    .Include(i => i.PackageConfigClassMaps).ThenInclude(i => i.Class)
                    .Include(i => i.PackageConfigFeeStructureMaps).ThenInclude(i => i.FeeStructure)
                    .AsNoTracking()
                    .FirstOrDefault();

                var packageConfigDTO = new PackageConfigDTO()
                {
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    Description = entity.Description,
                    PackageConfigIID = entity.PackageConfigIID,
                    SchoolID = entity.SchoolID,
                    IsAutoCreditNote = entity.IsAutoCreditNote,
                    CreditNoteAccountID = !entity.CreditNoteAccountID.HasValue ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO()
                    {
                        Key = entity.CreditNoteAccountID.ToString(),
                        Value = entity.Account.AccountName
                    },
                    Academic = entity.AcadamicYearID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AcadamicYearID.ToString(),
                        Value = entity.AcademicYear.Description + " ( " + Convert.ToDateTime(entity.AcademicYear.StartDate).Year + " - " + Convert.ToDateTime(entity.AcademicYear.EndDate).Year + ")"
                    } : new KeyValueDTO(),
                };

                if (entity.PackageConfigFeeStructureMaps != null && entity.PackageConfigFeeStructureMaps.Count > 0)
                {
                    foreach (var entityFeeMaps in entity.PackageConfigFeeStructureMaps)
                    {
                        if (entityFeeMaps.IsActive == entity.IsActive)
                        {
                            packageConfigDTO.FeeStructure.Add(new KeyValueDTO
                            {
                                Key = entityFeeMaps.FeeStructureID.ToString(),
                                Value = entityFeeMaps.FeeStructure.Name
                            });
                        }
                    }
                }

                if (entity.PackageConfigStudentMaps != null && entity.PackageConfigStudentMaps.Count > 0)
                {
                    foreach (var entityStudentMaps in entity.PackageConfigStudentMaps)
                    {
                        if (entityStudentMaps.IsActive == entity.IsActive)
                        {
                            packageConfigDTO.Student.Add(new KeyValueDTO
                            {
                                Key = entityStudentMaps.StudentID.ToString(),
                                Value = entityStudentMaps.Student.AdmissionNumber + '-' + entityStudentMaps.Student.FirstName + ' ' + entityStudentMaps.Student.MiddleName + ' ' + entityStudentMaps.Student.LastName
                            });
                        }
                    }
                }

                if (entity.PackageConfigClassMaps != null && entity.PackageConfigClassMaps.Count > 0)
                {
                    foreach (var entityClassMaps in entity.PackageConfigClassMaps)
                    {
                        if (entityClassMaps.IsActive == entity.IsActive)
                        {
                            packageConfigDTO.Class.Add(new KeyValueDTO
                            {
                                Key = entityClassMaps.ClassID.ToString(),
                                Value = entityClassMaps.Class.ClassDescription
                            });
                        }
                    }
                }

                if (entity.PackageConfigStudentGroupMaps != null && entity.PackageConfigStudentGroupMaps.Count > 0)
                {
                    foreach (var entityStudGrpMaps in entity.PackageConfigStudentGroupMaps)
                    {
                        if (entityStudGrpMaps.IsActive == entity.IsActive)
                        {
                            packageConfigDTO.StudentGroup.Add(new KeyValueDTO
                            {
                                Key = entityStudGrpMaps.StudentGroupID.ToString(),
                                Value = entityStudGrpMaps.StudentGroup.GroupName
                            });
                        }
                    }
                }

                return packageConfigDTO;
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PackageConfigDTO;
            if (toDto.Academic == null || string.IsNullOrEmpty(toDto.Academic.Key))
            {
                throw new Exception("Academic Year cannot be left empty!");
            }
            if (toDto.FeeStructure == null || toDto.FeeStructure.Count == 0)
            {
                throw new Exception("Fee structure should be selected!");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new PackageConfig()
                {
                    AcadamicYearID = string.IsNullOrEmpty(toDto.Academic.Key) ? (int?)null : int.Parse(toDto.Academic.Key),
                    Name = toDto.Name,
                    IsActive = toDto.IsActive,
                    Description = toDto.Description,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    PackageConfigIID = toDto.PackageConfigIID,
                    IsAutoCreditNote = toDto.IsAutoCreditNote,
                    CreditNoteAccountID = toDto.CreditNoteAccountID == null ? (long?)null : long.Parse(toDto.CreditNoteAccountID.Key),
                    CreatedBy = toDto.PackageConfigIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.PackageConfigIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.PackageConfigIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.PackageConfigIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                if (toDto.PackageConfigIID != 0)
                {
                    var parentStudIIDs = toDto.Student.Select(s => s.Key).ToList();

                    //get existing parent entities
                    var parentStudEntities = dbContext.PackageConfigStudentMaps.Where(x =>
                        x.PackageConfigID == entity.PackageConfigIID &&
                        !parentStudIIDs.Contains(x.StudentID.Value.ToString())).AsNoTracking().ToList();

                    if (parentStudEntities != null && parentStudEntities.Count > 0)
                    {
                        dbContext.PackageConfigStudentMaps.RemoveRange(parentStudEntities);
                    }

                    var parentStudGrpIIDs = toDto.StudentGroup.Select(s => s.Key).ToList();

                    //get existing parent entities
                    var parentStudGrpEntities = dbContext.PackageConfigStudentGroupMaps.Where(x =>
                        x.PackageConfigID == entity.PackageConfigIID &&
                        !parentStudGrpIIDs.Contains(x.StudentGroupID.Value.ToString())).AsNoTracking().ToList();

                    if (parentStudGrpEntities != null && parentStudGrpEntities.Count > 0)
                    {
                        dbContext.PackageConfigStudentGroupMaps.RemoveRange(parentStudGrpEntities);
                    }

                    var parentClassIIDs = toDto.Class.Select(s => s.Key).ToList();

                    //get existing parent entities
                    var parentClassEntities = dbContext.PackageConfigClassMaps.Where(x =>
                        x.PackageConfigID == entity.PackageConfigIID &&
                        !parentClassIIDs.Contains(x.ClassID.Value.ToString())).AsNoTracking().ToList();

                    if (parentClassEntities != null && parentClassEntities.Count > 0)
                    {
                        dbContext.PackageConfigClassMaps.RemoveRange(parentClassEntities);
                    }

                    var parentFeeStructureIIDs = toDto.FeeStructure.Select(s => s.Key).ToList();

                    //get existing parent entities
                    var parentFeeStructureEntities = dbContext.PackageConfigFeeStructureMaps.Where(x =>
                        x.PackageConfigID == entity.PackageConfigIID &&
                        !parentFeeStructureIIDs.Contains(x.FeeStructureID.Value.ToString())).AsNoTracking().ToList();

                    if (parentFeeStructureEntities != null && parentFeeStructureEntities.Count > 0)
                    {
                        dbContext.PackageConfigFeeStructureMaps.RemoveRange(parentFeeStructureEntities);
                    }
                }

                entity.PackageConfigStudentMaps = new List<PackageConfigStudentMap>();
                if (toDto.Student != null && toDto.Student.Count > 0)
                {
                    foreach (var ObjStud in toDto.Student)
                    {
                        if (!string.IsNullOrEmpty(ObjStud.Key))
                        {
                            var packageConfigStudentMapIID = dbContext.PackageConfigStudentMaps.Where(i => i.StudentID == int.Parse(ObjStud.Key) && i.PackageConfigID == toDto.PackageConfigIID).OrderByDescending(o => o.PackageConfigStudentMapIID).AsNoTracking().Select(x => x.PackageConfigStudentMapIID).FirstOrDefault();

                            var entityStudentMap = new PackageConfigStudentMap()
                            {
                                IsActive = toDto.IsActive,
                                StudentID = long.Parse(ObjStud.Key),
                                PackageConfigID = toDto.PackageConfigIID,
                                PackageConfigStudentMapIID = packageConfigStudentMapIID
                            };

                            var detach = dbContext.Set<PackageConfigStudentMap>().Local.FirstOrDefault(c => c.PackageConfigStudentMapIID == packageConfigStudentMapIID);
                            if (detach != null)
                            {
                                dbContext.Entry(detach).State = EntityState.Detached;
                                dbContext.Entry(entityStudentMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            entity.PackageConfigStudentMaps.Add(entityStudentMap);
                        }
                    }
                }

                entity.PackageConfigStudentGroupMaps = new List<PackageConfigStudentGroupMap>();
                if (toDto.StudentGroup != null && toDto.StudentGroup.Count > 0)
                {
                    foreach (var ObjStud in toDto.StudentGroup)
                    {
                        if (!string.IsNullOrEmpty(ObjStud.Key))
                        {
                            var packageConfigStudentGroupMapIID = dbContext.PackageConfigStudentGroupMaps.Where(i => i.StudentGroupID == int.Parse(ObjStud.Key) && i.PackageConfigID == toDto.PackageConfigIID).OrderByDescending(o => o.PackageConfigStudentGroupMapIID).AsNoTracking().Select(x => x.PackageConfigStudentGroupMapIID).FirstOrDefault();
                            var entityStudentGrpMap = new PackageConfigStudentGroupMap()
                            {
                                IsActive = toDto.IsActive,
                                StudentGroupID = int.Parse(ObjStud.Key),
                                PackageConfigID = toDto.PackageConfigIID,
                                PackageConfigStudentGroupMapIID = packageConfigStudentGroupMapIID
                            };
                            var detach = dbContext.Set<PackageConfigStudentGroupMap>().Local.FirstOrDefault(c => c.PackageConfigStudentGroupMapIID == packageConfigStudentGroupMapIID);
                            if (detach != null)
                            {
                                dbContext.Entry(detach).State = EntityState.Detached;
                                dbContext.Entry(entityStudentGrpMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            entity.PackageConfigStudentGroupMaps.Add(entityStudentGrpMap);
                        }
                    }
                }

                entity.PackageConfigClassMaps = new List<PackageConfigClassMap>();
                if (toDto.Class != null && toDto.Class.Count > 0)
                {
                    foreach (var Objclass in toDto.Class)
                    {
                        if (!string.IsNullOrEmpty(Objclass.Key))
                        {
                            var PackageConfigClassMapIID = dbContext.PackageConfigClassMaps.Where(i => i.ClassID == int.Parse(Objclass.Key) && i.PackageConfigID == toDto.PackageConfigIID).OrderByDescending(o => o.PackageConfigClassMapIID).AsNoTracking().Select(x => x.PackageConfigClassMapIID).FirstOrDefault();
                            var entityclass = new PackageConfigClassMap()
                            {
                                IsActive = toDto.IsActive,
                                ClassID = int.Parse(Objclass.Key),
                                PackageConfigID = toDto.PackageConfigIID,
                                PackageConfigClassMapIID = PackageConfigClassMapIID == 0 ? 0 : PackageConfigClassMapIID
                            };
                            var detach = dbContext.Set<PackageConfigClassMap>().Local.FirstOrDefault(c => c.PackageConfigClassMapIID == PackageConfigClassMapIID);
                            if (detach != null)
                            {
                                dbContext.Entry(detach).State = EntityState.Detached;
                                dbContext.Entry(entityclass).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            entity.PackageConfigClassMaps.Add(entityclass);
                        }
                    }
                }

                entity.PackageConfigFeeStructureMaps = new List<PackageConfigFeeStructureMap>();
                if (toDto.FeeStructure != null && toDto.FeeStructure.Count > 0)
                {
                    foreach (var Objfee in toDto.FeeStructure)
                    {
                        if (!string.IsNullOrEmpty(Objfee.Key))
                        {
                            var packageConfigFeeStructureMapIID = dbContext.PackageConfigFeeStructureMaps.Where(i => i.FeeStructureID == int.Parse(Objfee.Key) && i.PackageConfigID == toDto.PackageConfigIID).OrderByDescending(o => o.PackageConfigFeeStructureMapIID).AsNoTracking().Select(x => x.PackageConfigFeeStructureMapIID).FirstOrDefault();
                            var entityFee = new PackageConfigFeeStructureMap()
                            {
                                IsActive = toDto.IsActive,
                                FeeStructureID = int.Parse(Objfee.Key),
                                PackageConfigID = toDto.PackageConfigIID,
                                PackageConfigFeeStructureMapIID = packageConfigFeeStructureMapIID == 0 ? 0 : packageConfigFeeStructureMapIID
                            };

                            var detach = dbContext.Set<PackageConfigFeeStructureMap>().Local.FirstOrDefault(c => c.PackageConfigFeeStructureMapIID == packageConfigFeeStructureMapIID);
                            if (detach != null)
                            {
                                dbContext.Entry(detach).State = EntityState.Detached;
                                dbContext.Entry(entityFee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            entity.PackageConfigFeeStructureMaps.Add(entityFee);
                        }
                    }
                }

                dbContext.PackageConfigs.Add(entity);
                if (entity.PackageConfigIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                toDto.PackageConfigIID = entity.PackageConfigIID;
            }

            return ToDTOString(toDto);
        }

    }
}