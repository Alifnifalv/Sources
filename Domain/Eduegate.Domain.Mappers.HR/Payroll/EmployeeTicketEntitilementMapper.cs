using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;

using Microsoft.EntityFrameworkCore;
using Eduegate.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Entity.HR.Payroll;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eduegate.Domain.Entity.Models.HR;
using Eduegate.Domain.Entity.HR;


namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeTicketEntitilementMapper : DTOEntityDynamicMapper
    {
        public static EmployeeTicketEntitilementMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeTicketEntitilementMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TicketEntitilementDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TicketEntitilementDTO ToDTO(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.TicketEntitilements.Include(x=>x.CountryAirport)
                    .AsNoTracking().FirstOrDefault(a => a.TicketEntitilementID == IID);

                var dto = new TicketEntitilementDTO()
                {
                    TicketEntitilementID = entity.TicketEntitilementID,
                    TicketEntitilement1 = entity.TicketEntitilement1,
                    CountryAirportID = entity.CountryAirportID,
                    CountryAirport = entity.CountryAirportID.HasValue ? new KeyValueDTO() { Key = entity.CountryAirportID.ToString(),
                        Value = entity.CountryAirport.AirportName + "(" + entity.CountryAirport.IATA + ")" } : new KeyValueDTO(),
                    CountryAirportShortName = entity.CountryAirportID.HasValue ? entity.CountryAirport.IATA : null,
                    NoOfDays = entity.NoOfDays,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TicketEntitilementDTO;

            var entity = new Entity.HR.Payroll.TicketEntitilement()
            {
                TicketEntitilementID = toDto.TicketEntitilementID,
                TicketEntitilement1 = toDto.TicketEntitilement1,
                CountryAirportID = toDto.CountryAirportID,
                NoOfDays = toDto.NoOfDays,
                CreatedBy = toDto.TicketEntitilementID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.TicketEntitilementID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.TicketEntitilementID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.TicketEntitilementID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateHRContext())
            {
                if (entity.TicketEntitilementID == 0)
                {
                    var maxGroupID = dbContext.TicketEntitilements.Max(a => (int?)a.TicketEntitilementID);
                    entity.TicketEntitilementID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.TicketEntitilements.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.TicketEntitilements.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return GetEntity(entity.TicketEntitilementID);
        }

    }
}
