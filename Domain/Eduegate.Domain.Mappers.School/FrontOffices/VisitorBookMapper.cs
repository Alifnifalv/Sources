using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.FrontOffices
{
    public class VisitorBookMapper : DTOEntityDynamicMapper
    {
        public static VisitorBookMapper Mapper(CallContext context)
        {
            var mapper = new VisitorBookMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<VisitorBookDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private VisitorBookDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.VisitorBooks.Where(x => x.VisitorBookIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new VisitorBookDTO()
                {
                    VisitorBookIID = entity.VisitorBookIID,
                    VisitingPurposeID = entity.VisitingPurposeID,
                    VisitorName = entity.VisitorName,
                    PhoneNumber = entity.PhoneNumber,
                    IDCard = entity.IDCard,
                    NumberOfPerson = entity.NumberOfPerson,
                    Date = entity.Date,
                    InTime = entity.InTime,
                    OutTime = entity.OutTime,
                    Note = entity.Note,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as VisitorBookDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new VisitorBook()
            {
                VisitorBookIID = toDto.VisitorBookIID,
                VisitingPurposeID = toDto.VisitingPurposeID,
                VisitorName = toDto.VisitorName,
                PhoneNumber = toDto.PhoneNumber,
                IDCard = toDto.IDCard,
                NumberOfPerson = toDto.NumberOfPerson,
                Date = toDto.Date,
                InTime = toDto.InTime,
                OutTime = toDto.OutTime,
                Note = toDto.Note,
                CreatedBy = toDto.VisitorBookIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.VisitorBookIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.VisitorBookIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.VisitorBookIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.VisitorBooks.Add(entity);
                if (entity.VisitorBookIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.VisitorBookIID));
        }

    }
}