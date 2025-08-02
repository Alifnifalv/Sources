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
using Microsoft.VisualBasic;
using System.Globalization;
using Eduegate.Domain.Setting;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeTicketEntitilementEntryMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static EmployeeTicketEntitilementEntryMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeTicketEntitilementEntryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TicketEntitilementEntryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TicketEntitilementEntryDTO ToDTO(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.TicketEntitilementEntries
                    .Include(x => x.Employee)
                    .Include(x => x.FlightClass)
                    .Include(x => x.TicketEntitilement)
                    .Include(x => x.EmployeeCountryAirport)
                    .AsNoTracking().FirstOrDefault(a => a.TicketEntitilementEntryIID == IID);

                var dto = new TicketEntitilementEntryDTO()
                {
                    BalanceCarriedForwardPer = entity.BalanceCarriedForwardPer,
                    BalanceTicketAmountPayable = entity.BalanceTicketAmountPayable,
                    EmployeeID = entity.EmployeeID,
                    EmployeeCountryAirportID = entity.EmployeeCountryAirportID,
                    FlightClassID = entity.FlightClassID,
                    ISTicketEligible = entity.ISTicketEligible,
                    IsTicketFareIssued = entity.IsTicketFareIssued,
                    IsTicketFareReimbursed = entity.IsTicketFareReimbursed,
                    IsTwoWay = entity.IsTwoWay,
                    DateOfJoining = entity.Employee?.DateOfJoining,
                    TicketEligibleFromDate = entity.TicketEligibleFromDate,
                    TicketEntitilementID = entity.TicketEntitilementID,
                    TicketEntitilementDays = entity.TicketEntitilementDays,
                    TicketEntitilementEntryIID = entity.TicketEntitilementEntryIID,
                    TicketEntitilementPer = entity.TicketEntitilementPer,
                    TicketFareIssuedPercentage = entity.TicketFareIssuedPercentage,
                    TicketfarePayable = entity.TicketfarePayable,
                    TicketFareReimbursementPercentage = entity.TicketFareReimbursementPercentage,
                    TicketIssueDate = entity.TicketIssueDate,
                    TicketIssuedOrFareReimbursed = entity.TicketIssuedOrFareReimbursed,
                    TravelReturnAirfare = entity.TravelReturnAirfare,
                    LastTicketGivenDate = entity.Employee?.LastTicketGivenDate,
                    VacationDaysEveryYear = entity.VacationDaysEveryYear,
                    VacationStartingDate = entity.VacationStartingDate,
                    GenerateTravelSector = entity.GenerateTravelSector,
                    LOPforTicketEntitilement = entity.LOPforTicketEntitilement,
                    IsConsidereLOP = entity.IsConsidereLOP,
                    DaysToBeConsideredForLOP = entity.DaysToBeConsideredForLOP,
                    BalanceBroughtForward=entity.BalanceBroughtForward,
                    FlightClass = entity.FlightClassID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.FlightClassID.ToString(),
                        Value = entity.FlightClass.FlightClassName
                    } : new KeyValueDTO(),

                    TicketEntitilement = entity.TicketEntitilementID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TicketEntitilementID.ToString(),
                        Value = entity.TicketEntitilement.TicketEntitilement1
                    } : new KeyValueDTO(),

                    Employee = new KeyValueDTO()
                    {
                        Value = entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName,
                        Key = entity.EmployeeID.ToString()
                    },
                    EmployeeCountryAirport = entity.EmployeeCountryAirportID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.EmployeeCountryAirportID.ToString(),
                        Value = entity.EmployeeCountryAirport.AirportName + "(" + entity.EmployeeCountryAirport.IATA + ")"
                    } : new KeyValueDTO(),
                    TicketEntitlementEntryStatusID = entity.TicketEntitlementEntryStatusID.HasValue ? entity.TicketEntitlementEntryStatusID : (short?)null,
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TicketEntitilementEntryDTO;

            if (!toDto.TicketIssuedOrFareReimbursed.HasValue || toDto.TicketIssuedOrFareReimbursed == 0)
            {
                throw new Exception("Ticket Issued Or Fare Reimbursed Amount is null or invalid. So cannot be saved!");
            }
            if (toDto.IsTicketFareIssued == true && toDto.IsTicketFareReimbursed == true)
            {
                throw new Exception("Both 'Ticket Fare Issued' and 'Ticket Fare Reimbursed' cannot be selected at the same time!");
            }
            if (toDto.IsTicketFareIssued != true && toDto.IsTicketFareReimbursed != true)
            {
                throw new Exception("Both 'Ticket Fare Issued' and 'Ticket Fare Reimbursed' cannot be unchecked at the same time!");
            }
            var settingBL = new Domain.Setting.SettingBL(_context);
            var finalSettlementTypeID = settingBL.GetSettingValue<int?>("EMPLOYEE_FINAL_SETTLEMENT_TYPE");
            using (var dbContext = new dbEduegateHRContext())
            {
                var dRepSettlementdet = dbContext.EmployeeSalarySettlements.Where(x => x.EmployeeID == toDto.EmployeeID).AsNoTracking().FirstOrDefault();
                if (dRepSettlementdet != null && dRepSettlementdet.EmployeeSettlementTypeID == finalSettlementTypeID)
                {
                    throw new Exception("The employee has completed the final settlement , therefore cannot be saved!");
                }
                var dRepTicketEntitilementEn = dbContext.TicketEntitilementEntries.Where(x => x.EmployeeID == toDto.EmployeeID && x.TicketEntitilementEntryIID != toDto.TicketEntitilementEntryIID && x.TicketIssueDate.Value.Month == toDto.TicketIssueDate.Value.Month && x.TicketIssueDate.Value.Year == toDto.TicketIssueDate.Value.Year).AsNoTracking().FirstOrDefault();
                if (dRepTicketEntitilementEn != null && dRepTicketEntitilementEn.TicketEntitilementEntryIID != 0)
                {
                    throw new Exception("The employee has already entry exist for the same month, therefore cannot be saved!");
                }

                var entity = new Entity.HR.Payroll.TicketEntitilementEntry()
                {
                    BalanceCarriedForwardPer = toDto.BalanceCarriedForwardPer,
                    BalanceTicketAmountPayable = toDto.BalanceTicketAmountPayable,
                    EmployeeID = toDto.EmployeeID,
                    EmployeeCountryAirportID = toDto.EmployeeCountryAirportID,
                    FlightClassID = toDto.FlightClassID,
                    ISTicketEligible = toDto.ISTicketEligible,
                    IsTicketFareIssued = toDto.IsTicketFareIssued,
                    IsTicketFareReimbursed = toDto.IsTicketFareReimbursed,
                    IsTwoWay = toDto.IsTwoWay,
                    TicketEligibleFromDate = toDto.TicketEligibleFromDate,
                    TicketEntitilementID = toDto.TicketEntitilementID,
                    TicketEntitilementDays = toDto.TicketEntitilementDays,
                    TicketEntitilementEntryIID = toDto.TicketEntitilementEntryIID,
                    TicketEntitilementPer = toDto.TicketEntitilementPer,
                    TicketFareIssuedPercentage = toDto.TicketFareIssuedPercentage,
                    TicketfarePayable = toDto.TicketfarePayable,
                    TicketFareReimbursementPercentage = toDto.TicketFareReimbursementPercentage,
                    TicketIssueDate = toDto.TicketIssueDate,
                    TicketIssuedOrFareReimbursed = toDto.TicketIssuedOrFareReimbursed,
                    TravelReturnAirfare = toDto.TravelReturnAirfare,
                    LastTicketGivenDate = toDto.LastTicketGivenDate,
                    VacationDaysEveryYear = toDto.VacationDaysEveryYear,
                    VacationStartingDate = toDto.VacationStartingDate,
                    GenerateTravelSector = toDto.GenerateTravelSector,
                    LOPforTicketEntitilement = toDto.LOPforTicketEntitilement,
                    IsConsidereLOP = toDto.IsConsidereLOP,
                    BalanceBroughtForward = toDto.BalanceBroughtForward,
                    DaysToBeConsideredForLOP = toDto.DaysToBeConsideredForLOP,
                    TicketEntitlementEntryStatusID = toDto.TicketEntitlementEntryStatusID,

                };
                if (toDto.TicketEntitilementEntryIID == 0)
                {
                    entity.CreatedBy = (int)_context.LoginID;
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.UpdatedBy = (int)_context.LoginID;
                    entity.UpdatedDate = DateTime.Now;
                }

                if (entity.TicketEntitilementEntryIID == 0)
                {

                    dbContext.TicketEntitilementEntries.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.TicketEntitilementEntries.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
                return GetEntity(entity.TicketEntitilementEntryIID);
            }


        }

        public TicketEntitilementEntryDTO GetAirfareEntry(TicketEntitilementEntryDTO ticketEntitilementEntryDTO)
        {
            var entryDTO = new TicketEntitilementEntryDTO();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entryData = dbContext.TicketEntitilementEntries
                            .Where(a => a.EmployeeID == ticketEntitilementEntryDTO.EmployeeID && 
                             a.TicketEntitilementEntryIID != ticketEntitilementEntryDTO.TicketEntitilementEntryIID && a.IsTicketFareReimbursed==true)
                            .OrderByDescending(x => x.TicketEntitilementEntryIID)
                            .FirstOrDefault();
                if (!entryData.IsNull())
                {
                    entryDTO = new TicketEntitilementEntryDTO()
                    {
                        TicketEntitilementEntryIID = entryData.TicketEntitilementEntryIID,
                        BalanceBroughtForward = ((entryData.BalanceTicketAmountPayable ?? 0) < 0) ? -1 * entryData.BalanceTicketAmountPayable : 0,
                    };
                }

                return entryDTO;
            }
        }
    }
}

