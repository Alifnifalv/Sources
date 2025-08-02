using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using System;
using System.Linq;
using Eduegate.Framework;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentHouseMapper : DTOEntityDynamicMapper
    {         
        public static  StudentHouseMapper Mapper(CallContext context)
        {
            var mapper = new  StudentHouseMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentHouseDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentHouses.Where(a => a.StudentHouseID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new StudentHouseDTO()
                {
                    StudentHouseID = entity.StudentHouseID,
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentHouseDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new StudentHouse()
            {
                StudentHouseID = toDto. StudentHouseID,
                Description = toDto. Description,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.StudentHouseID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.StudentHouseID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.StudentHouseID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.StudentHouseID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.StudentHouseID == 0)
                {                  
                    var maxGroupID = dbContext.StudentHouses.Max(a => (int?)a.StudentHouseID);
                    entity.StudentHouseID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.StudentHouses.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.StudentHouses.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new StudentHouseDTO()
            {
                StudentHouseID = entity. StudentHouseID,
                Description = entity. Description,
                   
            });
        }

    }
}