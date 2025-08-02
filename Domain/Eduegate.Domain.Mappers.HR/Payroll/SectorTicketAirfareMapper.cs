using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Payroll;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SectorTicketAirfareMapper : DTOEntityDynamicMapper
    {
        public static SectorTicketAirfareMapper Mapper(CallContext context)
        {
            var mapper = new SectorTicketAirfareMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SectorTicketAirfareDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SectorTicketAirfareDTO ToDTO(long IID)
        {
            var dto = new SectorTicketAirfareDTO();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entitySec = dbContext.SectorTicketAirfares
                    .AsNoTracking().FirstOrDefault(a => a.SectorTicketAirfareIID == IID);

                var entity = dbContext.SectorTicketAirfares
                   .AsNoTracking().Where(a => a.DepartmentID == entitySec.DepartmentID && a.ValidityFrom.Value.Date == entitySec.ValidityFrom.Value.Date
                   && a.ValidityTo.Value.Date == entitySec.ValidityTo.Value.Date)
                   .Include(x => x.Department)
                   .Include(x => x.Airport)
                   .Include(x => x.ReturnAirport)
                   .Include(x => x.FlightClass).ToList();
                if (entity != null && entity.ToList().Any())
                {
                    dto = new SectorTicketAirfareDTO()
                    {
                        DepartmentID = entity[0].DepartmentID,
                        SectorTicketAirfareIID = entity[0].SectorTicketAirfareIID,
                        ValidityFrom = entity[0].ValidityFrom,
                        ValidityTo = entity[0].ValidityTo,
                        //CreatedBy = entity[0].CreatedBy,
                        //CreatedDate = entity[0].CreatedDate,
                        //UpdatedBy = entity[0].UpdatedBy,
                        //UpdatedDate = entity[0].UpdatedDate,
                    };
                    dto.SectorTicketAirfareDetail = new List<SectorTicketAirfareDetailDTO>();
                    foreach (var detail in entity)
                    {
                        var sectorTicketAirfareDetailDTO = new SectorTicketAirfareDetailDTO()
                        {
                            IsTwoWay = detail.IsTwoWay,
                            AirportID = detail.AirportID,
                            ReturnAirportID = detail.ReturnAirportID,
                            FlightClassID = detail.FlightClassID,
                            Rate = detail.Rate,
                            GenerateTravelSector = detail.GenerateTravelSector,
                            Airport = detail.AirportID.HasValue ? new KeyValueDTO()
                            {
                                Key = detail.AirportID.ToString(),
                                Value = detail.Airport.AirportName + "(" + detail.Airport.IATA + ")"
                            } : new KeyValueDTO(),
                            ReturnAirport = detail.ReturnAirportID.HasValue ? new KeyValueDTO()
                            {
                                Key = detail.ReturnAirportID.ToString(),
                                Value = detail.ReturnAirport.AirportName + "(" + detail.ReturnAirport.IATA + ")"
                            } : new KeyValueDTO(),
                            FlightClass = detail.FlightClassID.HasValue ? new KeyValueDTO()
                            {
                                Key = detail.FlightClassID.ToString(),
                                Value = detail.FlightClass.FlightClassName
                            } : new KeyValueDTO(),
                        };
                        dto.SectorTicketAirfareDetail.Add(sectorTicketAirfareDetailDTO);
                    }

                }
            }
            return dto;

        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SectorTicketAirfareDTO;
            var entity = new SectorTicketAirfare();

            using (var dbContext = new dbEduegateHRContext())
            {
                //delete maps
                var entities = dbContext.SectorTicketAirfares.Where(x =>
                    x.ValidityFrom.Value.Date == toDto.ValidityFrom.Value.Date && x.ValidityTo.Value.Date == toDto.ValidityTo.Value.Date &&
                    x.DepartmentID == toDto.DepartmentID).AsNoTracking().ToList();

                if (entities.IsNotNull() && entities.Count()>0)
                    dbContext.SectorTicketAirfares.RemoveRange(entities);

                foreach (var detail in toDto.SectorTicketAirfareDetail)
                {
                    entity = new SectorTicketAirfare()
                    {
                        IsTwoWay = detail.IsTwoWay,
                        AirportID = detail.AirportID,
                        ReturnAirportID = detail.ReturnAirportID,
                        FlightClassID = detail.FlightClassID,
                        Rate = detail.Rate,
                        DepartmentID = toDto.DepartmentID,
                        ValidityFrom = toDto.ValidityFrom,
                        ValidityTo = toDto.ValidityTo,
                        GenerateTravelSector = detail.GenerateTravelSector,
                    };
                    dbContext.SectorTicketAirfares.Add(entity);
                }

                dbContext.SaveChanges();
            }

            return GetEntity(entity.SectorTicketAirfareIID);
        }

        public SectorTicketAirfareDetailDTO GetSectorTicketAirfare(SectorTicketAirfareDTO sectorTicketAirfareDTO)
        {
            var sectorTicketAirfareDetailDTO = new SectorTicketAirfareDetailDTO();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var sectorTicketAirfare = dbContext.SectorTicketAirfares.Where(a => a.DepartmentID == sectorTicketAirfareDTO.DepartmentID
                && a.ValidityFrom.Value.Date <= sectorTicketAirfareDTO.ValidityFrom.Value.Date
                && a.ValidityTo.Value.Date >= sectorTicketAirfareDTO.ValidityFrom.Value.Date
                && a.FlightClassID == sectorTicketAirfareDTO.SectorTicketAirfareDetail[0].FlightClassID
                && a.GenerateTravelSector == sectorTicketAirfareDTO.SectorTicketAirfareDetail[0].GenerateTravelSector).AsNoTracking().FirstOrDefault();

                if (!sectorTicketAirfare.IsNull())
                {
                    sectorTicketAirfareDetailDTO = new SectorTicketAirfareDetailDTO()
                    {
                        FlightClassID = sectorTicketAirfare.FlightClassID,
                        Rate = sectorTicketAirfare.Rate,
                    };
                }
                return sectorTicketAirfareDetailDTO;
            }
        }
    }
}
