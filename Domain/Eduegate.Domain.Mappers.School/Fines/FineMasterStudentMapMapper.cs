using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Services.Contracts.School.Fines;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Fines
{
    public class FineMasterStudentMapMapper : DTOEntityDynamicMapper
    {
        public static FineMasterStudentMapMapper Mapper(CallContext context)
        {
            var mapper = new FineMasterStudentMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FineMasterStudentMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FineMasterStudentMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FineMasterStudentMaps.Where(x => x.FineMasterStudentMapIID == IID)
                    .Include(i => i.FineMaster)
                    .Include(i => i.Student)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new FineMasterStudentMapDTO()
                {
                    FineMasterStudentMapIID = entity.FineMasterStudentMapIID,
                    FineMasterID = entity.FineMasterID,
                    StudentId = entity.StudentId,
                    Amount = entity.Amount,
                    //IsCollected = entity.IsCollected,
                    FineMapDate = entity.FineMapDate,
                    Remarks = entity.Remarks,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    Student = entity.StudentId.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.StudentId.ToString(),
                        Value = entity.Student.AdmissionNumber + '-' + entity.Student.FirstName + ' ' + entity.Student.MiddleName + ' ' + entity.Student.LastName,
                    } : new KeyValueDTO(),
                    FineMaster = entity.FineMasterID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.FineMasterID.ToString(),
                        Value = entity.FineMaster.FineName,
                    } : new KeyValueDTO(),

                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FineMasterStudentMapDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FineMasterStudentMap()
            {
                FineMasterStudentMapIID = toDto.FineMasterStudentMapIID,
                FineMasterID = toDto.FineMasterID,
                StudentId = toDto.StudentId,
                Amount = toDto.Amount,
                //IsCollected = toDto.IsCollected,
                FineMapDate = toDto.FineMapDate,
                Remarks = toDto.Remarks,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.FineMasterStudentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FineMasterStudentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FineMasterStudentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FineMasterStudentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.FineMasterStudentMaps.Add(entity);
                if (entity.FineMasterStudentMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.FineMasterStudentMapIID));
        }

        public FineMasterStudentMapDTO GetFineAmount(int fineMasterID)
        {
            var  fineMasterDTO = new FineMasterStudentMapDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var fineMaster = dbContext.FineMasters.Where(f => f.FineMasterID == fineMasterID)
                    .OrderByDescending(o => o.FineMasterID)
                    .AsNoTracking()
                    .FirstOrDefault();

                fineMasterDTO.Amount = fineMaster != null ? fineMaster.Amount : 0;

                return fineMasterDTO;
            }
        }

    }
}