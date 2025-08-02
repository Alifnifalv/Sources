using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Hostel;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Hostels
{
    public class HostelRoomMapper : DTOEntityDynamicMapper
    {
        public static HostelRoomMapper Mapper(CallContext context)
        {
            var mapper = new HostelRoomMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<HostelRoomDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private HostelRoomDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.HostelRooms.Where(x => x.HostelRoomIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new HostelRoomDTO()
                {
                    HostelRoomIID = entity.HostelRoomIID,
                    RoomNumber = entity.RoomNumber,
                    HostelID = entity.HostelID,
                    RoomTypeID = entity.RoomTypeID,
                    NumberOfBed = entity.NumberOfBed,
                    CostPerBed = entity.CostPerBed,
                    Description = entity.Description,
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
            var toDto = dto as HostelRoomDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new HostelRoom()
            {
                HostelRoomIID = toDto.HostelRoomIID,
                RoomNumber = toDto.RoomNumber,
                HostelID = toDto.HostelID,
                RoomTypeID = toDto.RoomTypeID,
                NumberOfBed = toDto.NumberOfBed,
                CostPerBed = toDto.CostPerBed,
                Description = toDto.Description,
                CreatedBy = toDto.HostelRoomIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.HostelRoomIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.HostelRoomIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.HostelRoomIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.HostelRoomIID == 0)
                {
                    var maxGroupID = dbContext.HostelRooms.Max(a => (long?)a.HostelRoomIID);
                    entity.HostelRoomIID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;

                    dbContext.HostelRooms.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.HostelRooms.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.HostelRoomIID));
        }

    }
}