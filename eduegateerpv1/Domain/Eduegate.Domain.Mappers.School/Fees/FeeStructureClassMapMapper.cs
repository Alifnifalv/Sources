using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeStructureClassMapMapper : DTOEntityDynamicMapper
    {
        public static FeeStructureClassMapMapper Mapper(CallContext context)
        {
            var mapper = new FeeStructureClassMapMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeStructureClassMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public FeeStructureClassMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var entity = dbContext.ClassFeeStructureMaps.Where(x => x.ClassFeeStructureMapIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.FeeStructure)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                var FeeStructureClassMapDTO = new FeeStructureClassMapDTO()
                {
                    ClassID = entity.ClassID,
                    IsActive = entity.IsActive,
                    SchoolID = entity.SchoolID,
                    Academic = entity.AcadamicYearID == null ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO()
                    {
                        Key = entity.AcadamicYearID.ToString(),
                        Value = entity.AcademicYear.Description + " ( " + Convert.ToDateTime(entity.AcademicYear.StartDate).Year
                        + " - " + Convert.ToDateTime(entity.AcademicYear.EndDate).Year + ")"
                    },

                };

                FeeStructureClassMapDTO.Class.Add(new KeyValueDTO()
                {
                    Key = entity.ClassID.ToString(),
                    Value = entity.Class.ClassDescription
                });

                var feeStructurelist = (from fs in dbContext.ClassFeeStructureMaps
                                        where (fs.ClassID == entity.ClassID && fs.IsActive == entity.IsActive && fs.AcadamicYearID == entity.AcadamicYearID)
                                        select fs)
                                        .Include(i => i.FeeStructure)
                                        .AsNoTracking().ToList();

                foreach (var feeStr in feeStructurelist)
                {
                    FeeStructureClassMapDTO.FeeStructureClassMapList.Add(new FeeStructureClassMapListDTO()
                    {
                        ClassFeeStructureMapIID = feeStr.ClassFeeStructureMapIID
                    });

                    FeeStructureClassMapDTO.FeeStructure.Add(new KeyValueDTO()
                    {
                        Key = feeStr.FeeStructureID.ToString(),
                        Value = feeStr.FeeStructure.Name
                    });
                }

                return FeeStructureClassMapDTO;
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeStructureClassMapDTO;
            if (toDto.Academic == null || string.IsNullOrEmpty(toDto.Academic.Key))
            {
                throw new Exception("Please select academic year");
            }
            if (toDto.Class == null || toDto.Class.Count == 0)
            {
                throw new Exception("Class should be selected!");
            }
            if (toDto.FeeStructure == null || toDto.FeeStructure.Count == 0)
            {
                throw new Exception("Fee structure should be selected!");
            }
            var entityFee = new ClassFeeStructureMap();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicID = int.Parse(toDto.Academic.Key);

                if (toDto.FeeStructureClassMapList != null && toDto.FeeStructureClassMapList.Count > 0)
                {
                    var classFeeStructureMapIIDs = new List<long>();
                    toDto.FeeStructureClassMapList.ForEach(x => classFeeStructureMapIIDs.Add(x.ClassFeeStructureMapIID));

                    var splitEntities = dbContext.ClassFeeStructureMaps.Where(x =>
                     classFeeStructureMapIIDs.Contains(x.ClassFeeStructureMapIID)).AsNoTracking().ToList();

                    if (splitEntities.IsNotNull())
                        dbContext.ClassFeeStructureMaps.RemoveRange(splitEntities);
                }

                #region Duplication checking
                var classList = toDto.Class.Select(x => x.Key).ToList();
                var feestructureList = toDto.FeeStructure.Select(x => x.Key).ToList();
                var feestructureClassMapList = toDto.FeeStructureClassMapList.Select(x => x.ClassFeeStructureMapIID).ToList();

                if (dbContext.ClassFeeStructureMaps.Where(x => !feestructureClassMapList.Contains(x.ClassFeeStructureMapIID)
                   && classList.Contains(x.ClassID.HasValue ? x.ClassID.ToString() : "0")
                   && x.AcadamicYearID == academicID && x.IsActive == true
                   && feestructureList.Contains(x.FeeStructureID.HasValue ? x.FeeStructureID.ToString() : "0")).Count() > 0)

                    throw new Exception("Fee structure already mapped to the selected class");

                #endregion

              
                if (toDto.Class != null && toDto.Class.Count > 0)
                {
                    foreach (var ObjCls in toDto.Class)
                    {
                        if (toDto.FeeStructure != null && toDto.FeeStructure.Count > 0)
                        {
                            foreach (var Objfee in toDto.FeeStructure)
                            {
                                if (!string.IsNullOrEmpty(Objfee.Key))
                                {
                                    entityFee = new ClassFeeStructureMap()
                                    {
                                        IsActive = toDto.IsActive,
                                        FeeStructureID = int.Parse(Objfee.Key),
                                        ClassID = int.Parse(ObjCls.Key),
                                        AcadamicYearID = int.Parse(toDto.Academic.Key),
                                        ClassFeeStructureMapIID = 0,
                                        SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                        CreatedBy = toDto.ClassFeeStructureMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                        UpdatedBy = toDto.ClassFeeStructureMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                        CreatedDate = toDto.ClassFeeStructureMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                        UpdatedDate = toDto.ClassFeeStructureMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                    };
                                    if (entityFee.ClassFeeStructureMapIID != 0)
                                    {
                                        var detach = dbContext.Set<ClassFeeStructureMap>().Local.FirstOrDefault(c => c.ClassFeeStructureMapIID == entityFee.ClassFeeStructureMapIID);
                                        if (detach != null)
                                        {
                                            dbContext.Entry(detach).State = EntityState.Detached;
                                            dbContext.Entry(entityFee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        }
                                    }

                                    dbContext.ClassFeeStructureMaps.Add(entityFee);
                                    if (entityFee.ClassFeeStructureMapIID == 0)
                                    {
                                        dbContext.Entry(entityFee).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    }
                                    else
                                    {
                                        dbContext.Entry(entityFee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                    dbContext.SaveChanges();

                                }
                            }
                        }
                    }
                }
            }

            return ToDTOString(ToDTO(entityFee.ClassFeeStructureMapIID));
        }

    }
}