using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using System;
using Eduegate.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ClassMapper : DTOEntityDynamicMapper
    {
        public static ClassMapper Mapper(CallContext context)
        {
            var mapper = new ClassMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {               
                var entity = dbContext.Classes.Where(X => X.ClassID == IID)
                    .Include(i => i.CostCenter)
                    .Include(i => i.ClassWorkFlowMaps)
                    .Include(i => i.ClassClassGroupMaps).ThenInclude(i => i.ClassGroup)
                    .AsNoTracking()
                    .FirstOrDefault();

                var classGroup = new ClassDTO()
                {
                    ClassID = entity.ClassID,
                    ORDERNO = entity.ORDERNO,
                    Code = entity.Code,
                    ClassDescription = entity.ClassDescription,
                    CostCenterID = entity.CostCenterID,
                    ClassGroupID = entity.ClassGroupID,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    CostCenter = entity.CostCenterID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.CostCenterID.ToString(),
                        Value = entity.CostCenter.CostCenterName
                    } : new KeyValueDTO(),
                    IsActive = entity.IsActive,
                    ShiftID = entity.ShiftID,
                    IsVisible = entity.IsVisible
                };

                classGroup.GroupDescription = new List<KeyValueDTO>();
                foreach (var group in entity.ClassClassGroupMaps)
                {
                    classGroup.GroupDescription.Add(new KeyValueDTO()
                    {
                        Key = Convert.ToString(group.ClassGroupID),
                        Value = group.ClassGroup != null ? group.ClassGroup.GroupDescription : null
                    });
                }

                foreach (var mapdatas in entity.ClassWorkFlowMaps)
                {
                    classGroup.WorkFlowListDTO.Add(new ClassWorkFlowListDTO()
                    {
                        ClassWorkFlowIID = mapdatas.ClassWorkFlowIID,
                        ClassID = mapdatas.ClassID,
                        WorkflowEntityID = mapdatas.WorkflowEntityID,
                        WorkflowID = mapdatas.WorkflowID,
                    });
                }

                return classGroup;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassDTO;

            if (toDto.CostCenterID == null || toDto.CostCenterID == 0)
            {
                throw new Exception("Please Select Cost Center!");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Class()
            {
                ClassID = toDto.ClassID,
                ORDERNO = toDto.ORDERNO,
                Code = toDto.Code,
                ClassDescription = toDto.ClassDescription,
                CostCenterID = toDto.CostCenterID,
                ClassGroupID = toDto.ClassGroupID,
                IsActive = toDto.IsActive,
                ShiftID = toDto.ShiftID,
                IsVisible = toDto.IsVisible,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.ClassID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ClassID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ClassID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ClassID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            entity.ClassClassGroupMaps = new List<ClassClassGroupMap>();
            if (toDto.GroupDescription != null)
            {
                foreach (var classGroupMap in toDto.GroupDescription)
                {
                    entity.ClassClassGroupMaps.Add(new ClassClassGroupMap()
                    {
                        ClassGroupID = byte.Parse(classGroupMap.Key),
                        ClassID = toDto.ClassID,
                        CreatedBy = toDto.ClassID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.ClassID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.ClassID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = toDto.ClassID > 0 ? DateTime.Now : dto.UpdatedDate,
                    });
                }
            }

            //delete old workflowmap entry and re-entry
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getOldmapData = dbContext.ClassWorkFlowMaps.Where(a => a.ClassID == toDto.ClassID).AsNoTracking().ToList();

                if (getOldmapData.Count > 0 && getOldmapData != null)
                {
                    foreach (var data in getOldmapData)
                    {
                        dbContext.ClassWorkFlowMaps.Remove(data);
                        dbContext.SaveChanges();
                    }
                }
            }

            foreach (var mapData in toDto.WorkFlowListDTO)
            {
                if (mapData.WorkflowID.HasValue)
                {
                    entity.ClassWorkFlowMaps.Add(new ClassWorkFlowMap()
                    {
                        ClassWorkFlowIID = mapData.ClassWorkFlowIID,
                        ClassID = mapData.ClassID,
                        WorkflowEntityID = mapData.WorkflowEntityID,
                        WorkflowID = mapData.WorkflowID,
                        CreatedBy = mapData.ClassWorkFlowIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = mapData.ClassWorkFlowIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = mapData.ClassWorkFlowIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                        UpdatedDate = mapData.ClassWorkFlowIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                        //TimeStamps = mapData.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),

                    });
                }
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.ClassID == 0)
                {                  
                    var maxGroupID = dbContext.Classes.Max(a => (int?)a.ClassID);                   
                    entity.ClassID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    var maxGroup = dbContext.Classes.Max(a => a.ORDERNO);                   
                    entity.ORDERNO = maxGroup == null ? 1 : int.Parse(maxGroup.ToString()) + 1;
                    dbContext.Classes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Classes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ClassID));
        }

        public List<KeyValueDTO> GetClassesBySchool(byte schoolID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classList = new List<KeyValueDTO>();

                classList = (from cls in dbContext.Classes
                             where cls.IsVisible == true && cls.SchoolID == schoolID && 
                                (cls.IsActive == null || cls.IsActive == true)
                             orderby cls.ORDERNO
                             select new KeyValueDTO
                             {
                                 Key = cls.ClassID.ToString(),
                                 Value = cls.ClassDescription
                             }).AsNoTracking().ToList();
                return classList;
            }
        }

        public List<ClassDTO> GetClassListBySchool(byte schoolID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classList = new List<ClassDTO>();

                var classes = dbContext.Classes
                    .AsNoTracking()
                    .Where(x => x.IsActive == true && x.IsVisible == true && x.SchoolID == schoolID).ToList();

                if(classes.Count > 0)
                {
                    classList = classes.Select(cl => new ClassDTO()
                    {
                        ClassID = cl.ClassID,
                        ClassDescription = cl.ClassDescription,
                        ORDERNO = cl.ORDERNO,
                        SchoolID = cl.SchoolID
                    }).ToList();
                }

                return classList;
            }
        }

    }
}