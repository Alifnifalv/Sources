using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Web.Library.HR.Loans;
using Eduegate.Web.Library.HR.Payroll;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Payroll;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class TicketEntitlementEntryController : BaseSearchController
    {
        // GET: Payroll/Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TicketEntitlementEntry()
        {
            var vm = new TicketEntitilementEntryViewModel();
            return View(vm);
        }
        [HttpGet]
        public ActionResult GetEmployeeAirfareDetails(long? employeeID)
        {
            var ticketEntitilementEntryViewModel = new TicketEntitilementEntryViewModel();
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var empdet = ClientFactory.EmployeeServiceClient(CallContext).GetEmployee(employeeID.Value);

                ticketEntitilementEntryViewModel.TicketEligibleFromDateString = empdet.AirFareInfo.TicketEligibleFromDate.HasValue ? Convert.ToDateTime(empdet.AirFareInfo.TicketEligibleFromDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                ticketEntitilementEntryViewModel.ISTicketEligible = empdet.AirFareInfo.ISTicketEligible;
                ticketEntitilementEntryViewModel.EmployeeNearestAirportID = empdet.AirFareInfo.EmployeeNearestAirportID;
                ticketEntitilementEntryViewModel.EmployeeNearestAirport = empdet.AirFareInfo.EmployeeNearestAirportID.HasValue ? KeyValueViewModel.ToViewModel(empdet.AirFareInfo.EmployeeNearestAirport) : new KeyValueViewModel();
                ticketEntitilementEntryViewModel.TicketEntitilementID = empdet.AirFareInfo.TicketEntitilementID;
                ticketEntitilementEntryViewModel.TicketEntitilement = empdet.AirFareInfo.TicketEntitilementID.HasValue ? KeyValueViewModel.ToViewModel(empdet.AirFareInfo.TicketEntitilement) : new KeyValueViewModel();
                ticketEntitilementEntryViewModel.GenerateTravelSector = empdet.AirFareInfo.GenerateTravelSector;
                ticketEntitilementEntryViewModel.LastTicketGivenDate = empdet.AirFareInfo.LastTicketGivenDate;
                ticketEntitilementEntryViewModel.LastTicketgivenString = empdet.AirFareInfo.LastTicketGivenDate.HasValue ? Convert.ToDateTime(empdet.AirFareInfo.LastTicketGivenDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                ticketEntitilementEntryViewModel.IsTwoWay = empdet.AirFareInfo.IsTwoWay;
                ticketEntitilementEntryViewModel.FlightClass = empdet.AirFareInfo.FlightClassID.HasValue ? KeyValueViewModel.ToViewModel(empdet.AirFareInfo.FlightClass) : new KeyValueViewModel();
                ticketEntitilementEntryViewModel.FlightClassID = empdet.AirFareInfo.FlightClassID;
                ticketEntitilementEntryViewModel.DateOfJoining = empdet.DateOfJoining;
                ticketEntitilementEntryViewModel.DepartmentID = empdet.DepartmentID;

                //ticketEntitilementEntryViewModel.BalanceBroughtForward
                ticketEntitilementEntryViewModel.DateOfJoiningString = empdet.DateOfJoining.HasValue ? Convert.ToDateTime(empdet.DateOfJoining).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                var ticketdet = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((long)Screens.EmployeeTicketEntitilement, (long)ticketEntitilementEntryViewModel.TicketEntitilementID);
                var ticketData = JsonConvert.DeserializeObject<EmployeeTicketEntitilementViewModel>(ticketdet);
                ticketEntitilementEntryViewModel.TicketEntitilementDays = ticketData.NoOfDays;
                ticketEntitilementEntryViewModel.BalanceBroughtForward = ClientFactory.EmployeeServiceClient(CallContext).GetAirfareEntry(new TicketEntitilementEntryDTO() { EmployeeID = employeeID }).BalanceBroughtForward;

            }
            catch (Exception ex)
            {

            }
            return Json(ticketEntitilementEntryViewModel);
        }
        [HttpGet]
        public ActionResult FillSettingDetails()
        {
            var ticketEntitilementEntryViewModel = new TicketEntitilementEntryViewModel();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var settingVar = ClientFactory.SettingServiceClient(CallContext)
                .GetSettingByGroup("airfare");
            bool ConvertYesNoToBool(string value) => value?.Trim().ToLower() == "yes";
            string isTwoWayValue = settingVar.FirstOrDefault(s => s.SettingCode == "IS_TICKET_FOR_TWO_WAY")?.SettingValue;
            string isTicketFareIssuedValue = settingVar.FirstOrDefault(s => s.SettingCode == "IS_TICKET_ISSUED")?.SettingValue;
            string isTicketFareReimbursedValue = settingVar.FirstOrDefault(s => s.SettingCode == "IS_TICKET_REIMBERSED")?.SettingValue;
            string ticketFareIssuedPercentage = settingVar.FirstOrDefault(s => s.SettingCode == "TICKET_FARE_PERCENTAGE")?.SettingValue;
            string ticketFareReimbursementPercentage = settingVar.FirstOrDefault(s => s.SettingCode == "TICKET_FARE_REIMBURSED_PERCENTAGE")?.SettingValue;
            string vacationDaysEveryYear = settingVar.FirstOrDefault(s => s.SettingCode == "VACATION_DAYS_EVERYYEAR")?.SettingValue;
            string vacationStartingDate = settingVar.FirstOrDefault(s => s.SettingCode == "VACATION_START_DATE")?.SettingValue;
            string ticketIssueDate = settingVar.FirstOrDefault(s => s.SettingCode == "TICKET_ISSUE_DATE")?.SettingValue;
            string travelReturnAirfare = settingVar.FirstOrDefault(s => s.SettingCode == "TRAVEL_RETURN_AIRFARE")?.SettingValue;
            string daysToBeConsideredForLOP = settingVar.FirstOrDefault(s => s.SettingCode == "DAYS_CONSIDERED_LOP")?.SettingValue;
            decimal.TryParse(ticketFareIssuedPercentage, out var fareIssuedPercentageDeci);
            decimal.TryParse(ticketFareReimbursementPercentage, out var fareReimbursementPercentageDeci);
            int.TryParse(vacationDaysEveryYear, out var vacationDaysInt);
            DateTime.TryParse(vacationStartingDate, out var vacationStartDate);
            DateTime.TryParse(ticketIssueDate, out var ticketIssueDateval);
            decimal.TryParse(travelReturnAirfare, out var travelReturnAirfareDeci);
            decimal.TryParse(daysToBeConsideredForLOP, out var daysToBeConsideredForLOPDeci);
            ticketEntitilementEntryViewModel.IsTicketFareIssued = ConvertYesNoToBool(isTicketFareIssuedValue);
            ticketEntitilementEntryViewModel.IsTicketFareReimbursed = ConvertYesNoToBool(isTicketFareReimbursedValue);
            //ticketEntitilementEntryViewModel.IsTwoWay = ConvertYesNoToBool(isTwoWayValue);
            ticketEntitilementEntryViewModel.TicketFareIssuedPercentage = fareIssuedPercentageDeci;
            ticketEntitilementEntryViewModel.TicketFareReimbursementPercentage = fareReimbursementPercentageDeci;
            ticketEntitilementEntryViewModel.VacationDaysEveryYear = vacationDaysInt;
            ticketEntitilementEntryViewModel.VacationStartingDate = vacationStartDate;
            ticketEntitilementEntryViewModel.VacationStartingDateString = Convert.ToDateTime(vacationStartDate).ToString(dateFormat, CultureInfo.InvariantCulture); ;
            ticketEntitilementEntryViewModel.TicketIssueDate = ticketIssueDateval;
            ticketEntitilementEntryViewModel.TicketIssueDateString = Convert.ToDateTime(ticketIssueDateval).ToString(dateFormat, CultureInfo.InvariantCulture);
            ticketEntitilementEntryViewModel.TravelReturnAirfare = travelReturnAirfareDeci;
            ticketEntitilementEntryViewModel.DaysToBeConsideredForLOP = daysToBeConsideredForLOPDeci;

            return Json(ticketEntitilementEntryViewModel);

        }
        [HttpPost]
        public JsonResult GetSectorTicketAirfare([FromBody] TicketEntitilementEntryViewModel ticketEntitilementEntryViewModel)
        {
            var sectorTicketAirfareData = new SectorTicketAirfareDetailDTO();
            if (ticketEntitilementEntryViewModel != null
                && (!string.IsNullOrEmpty(ticketEntitilementEntryViewModel.TicketIssueDateString))
                && (ticketEntitilementEntryViewModel.DepartmentID.HasValue)
                && (ticketEntitilementEntryViewModel.FlightClassID.HasValue)
                && !string.IsNullOrEmpty(ticketEntitilementEntryViewModel.GenerateTravelSector))
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var sectorTicketAirfareDTO = new SectorTicketAirfareDTO()
                {
                    ValidityFrom = string.IsNullOrEmpty(ticketEntitilementEntryViewModel.TicketIssueDateString) ? (DateTime?)null : DateTime.ParseExact(ticketEntitilementEntryViewModel.TicketIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                    DepartmentID = ticketEntitilementEntryViewModel.DepartmentID,
                    SectorTicketAirfareDetail = new List<SectorTicketAirfareDetailDTO>()
                 {
                     new SectorTicketAirfareDetailDTO()
                     {
                     FlightClassID=ticketEntitilementEntryViewModel.FlightClassID ,
                     GenerateTravelSector=ticketEntitilementEntryViewModel.GenerateTravelSector
                    }
                 }
                };
                sectorTicketAirfareData = ClientFactory.EmployeeServiceClient(CallContext).GetSectorTicketAirfare(sectorTicketAirfareDTO);

            }
            return Json(sectorTicketAirfareData);
        }
    }
}
