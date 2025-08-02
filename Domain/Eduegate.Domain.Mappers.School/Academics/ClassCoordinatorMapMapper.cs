using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using System;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ClassCoordinatorMapMapper : DTOEntityDynamicMapper
    {

        public static ClassCoordinatorMapMapper Mapper(Eduegate.Framework.CallContext context)
        {
            var mapper = new ClassCoordinatorMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassCoordinatorsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassCoordinatorsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassCoordinators.Where(x => x.ClassCoordinatorIID == IID)
                    .Include(i => i.Employee)
                    .Include(i => i.Employee1)
                    .Include(i => i.ClassCoordinatorClassMaps).ThenInclude(i => i.Class)
                    .Include(i => i.ClassCoordinatorClassMaps).ThenInclude(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                var ClassCoordinatorMapDTO = new ClassCoordinatorsDTO()
                {
                    ClassCoordinatorIID = entity.ClassCoordinatorIID,
                    EmployeeName = entity.Employee != null ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                    CoordinatorID = entity.CoordinatorID,
                    HeadMasterID = entity.HeadMasterID,
                    HMName = entity.Employee1 != null ? entity.Employee1.EmployeeCode + " - " + entity.Employee1.FirstName + " " + entity.Employee1.MiddleName + " " + entity.Employee1.LastName : null,
                    SchoolID = entity.SchoolID,
                    ISACTIVE = entity.ISACTIVE,
                    AcademicYearID = entity.AcademicYearID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var map in entity.ClassCoordinatorClassMaps)
                {
                    if (!ClassCoordinatorMapDTO.ClassCoordinatorClassMaps.Any(x => x.ClassID == map.ClassID &&
                        x.SectionID == map.SectionID &&
                        x.AllClass == map.AllClass &&
                        x.AllSection == map.AllSection))
                    {
                        ClassCoordinatorMapDTO.ClassCoordinatorClassMaps.Add(new ClassCoordinatorClassMapDTO()
                        {
                            ClassCoordinatorClassMapIID = map.ClassCoordinatorClassMapIID,
                            ClassID = map.ClassID,
                            AllClass = map.AllClass,
                            AllSection = map.AllSection,
                            SectionID = map.SectionID,
                            Class = map.ClassID.HasValue ? new KeyValueDTO() { Key = map.ClassID.Value.ToString(), Value = map.Class.ClassDescription } : new KeyValueDTO() { Key = null, Value = "All Classes" },
                            Section = map.SectionID.HasValue ? new KeyValueDTO() { Key = map.SectionID.Value.ToString(), Value = map.Section.SectionName } : new KeyValueDTO() { Key = null, Value = "All Section" },
                            CreatedBy = map.CreatedBy,
                            CreatedDate = map.CreatedDate,
                            UpdatedBy = map.UpdatedBy,
                            UpdatedDate = map.UpdatedDate,
                        });
                    }
                }

                return ClassCoordinatorMapDTO;
            }

        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassCoordinatorsDTO;


            if (toDto.ClassCoordinatorClassMaps.Count == 0)
            {
                throw new Exception("Please select atleast one class or section");
            }

            var entity = SaveClassCoordinator(toDto);

            return ToDTOString(ToDTO(entity.ClassCoordinatorIID));
        }

        private ClassCoordinator SaveClassCoordinator(ClassCoordinatorsDTO toDto)
        {
            var entity = new ClassCoordinator()
            {
                ClassCoordinatorIID = toDto.ClassCoordinatorIID,
                SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                CoordinatorID = toDto.CoordinatorID,
                HeadMasterID = toDto.HeadMasterID,
                ISACTIVE = toDto.ISACTIVE,
            };

            foreach (var CCMap in toDto.ClassCoordinatorClassMaps)
            {
                if (!entity.ClassCoordinatorClassMaps.Any(x => x.ClassID == CCMap.ClassID &&
                   x.SectionID == CCMap.SectionID
                   && x.AllClass == CCMap.AllClass &&
                   x.AllSection == CCMap.AllSection))
                {
                    entity.ClassCoordinatorClassMaps.Add(new ClassCoordinatorClassMap()
                    {
                        ClassCoordinatorClassMapIID = CCMap.ClassCoordinatorClassMapIID,
                        ClassCoordinatorID = CCMap.ClassCoordinatorID,
                        ClassID = CCMap.ClassID,
                        AllClass = CCMap.AllClass,
                        AllSection = CCMap.AllSection,
                        SectionID = CCMap.SectionID,
                        CreatedBy = CCMap.ClassCoordinatorClassMapIID == 0 ? Convert.ToInt32(_context.LoginID) : CCMap.CreatedBy,
                        CreatedDate = CCMap.ClassCoordinatorClassMapIID == 0 ? DateTime.Now : CCMap.CreatedDate,
                        UpdatedBy = CCMap.ClassCoordinatorClassMapIID != 0 ? Convert.ToInt32(_context.LoginID) : CCMap.UpdatedBy,
                        UpdatedDate = CCMap.ClassCoordinatorClassMapIID != 0 ? DateTime.Now : CCMap.UpdatedDate,
                    });
                }
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.ClassCoordinators.Add(entity);

                if (entity.ClassCoordinatorIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    using (var dbContext1 = new dbEduegateSchoolContext())
                    {
                        //Remove old maps
                        var mapEntities = dbContext1.ClassCoordinatorClassMaps
                        .Where(x => x.ClassCoordinatorID == toDto.ClassCoordinatorIID).AsNoTracking().ToList();

                        if (mapEntities != null && mapEntities.Count > 0)
                        {
                            dbContext1.ClassCoordinatorClassMaps.RemoveRange(mapEntities);
                        }
                        dbContext1.SaveChanges();
                    }

                    if (entity.ClassCoordinatorClassMaps.Count > 0)
                    {
                        foreach (var ClassCoordinator in entity.ClassCoordinatorClassMaps)
                        {
                            if (ClassCoordinator.ClassCoordinatorClassMapIID == 0)
                            {
                                dbContext.Entry(ClassCoordinator).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(ClassCoordinator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            return entity;
        }

        public List<ClassCoordinatorsDTO> FillClassSectionWiseCoordinators(int classID, int sectionID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var coordinatorList = new List<ClassCoordinatorsDTO>();

                var getByClass = dbContext.ClassCoordinatorClassMaps
                    .Where(a => a.ClassCoordinator.SchoolID == _context.SchoolID && a.ClassCoordinator.AcademicYearID == _context.AcademicYearID && a.ClassCoordinator.ISACTIVE == true && a.AllClass == true || a.ClassID == classID)
                    .AsNoTracking().ToList();

                foreach (var cls in getByClass)
                {
                    var data = dbContext.ClassCoordinatorClassMaps
                        .Where(x => x.ClassCoordinatorID == cls.ClassCoordinatorID && x.AllSection == true || x.ClassCoordinatorID == cls.ClassCoordinatorID && x.SectionID == sectionID)
                        .Include(i => i.ClassCoordinator).ThenInclude(i => i.Employee)
                        .AsNoTracking().FirstOrDefault();

                    if (data != null)
                    {
                        coordinatorList.Add(new ClassCoordinatorsDTO
                        {
                            CoordinatorID = data.ClassCoordinator.CoordinatorID,
                            Coordinator = new KeyValueDTO() {
                                Key = data.ClassCoordinator.CoordinatorID.ToString(),
                                Value = data.ClassCoordinator.Employee?.EmployeeCode + " - " + data.ClassCoordinator.Employee?.FirstName + " " + data.ClassCoordinator.Employee.MiddleName + " " + data.ClassCoordinator.Employee?.LastName
                            },
                        });
                    }
                }

                return coordinatorList;
            }
        }

    }
}