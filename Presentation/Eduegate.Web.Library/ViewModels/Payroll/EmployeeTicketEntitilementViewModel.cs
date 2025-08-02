using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    public class EmployeeTicketEntitilementViewModel  : BaseMasterViewModel
    {
        public EmployeeTicketEntitilementViewModel()
        {
            CountryAirport = new KeyValueViewModel();           
        }
       
        public int TicketEntitilementID { get; set; }       
       
       
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Ticket Entitilement")]
        public string TicketEntitilement { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CountryAirport", "Numeric", false, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Airports", "LookUps.Airports")]
        [CustomDisplay("Country Airport")]
        public KeyValueViewModel CountryAirport { get; set; }
        public long? CountryAirportID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 5!")]
        [CustomDisplay("No. Of Days")]
        public int? NoOfDays { get; set; }



        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TicketEntitilementDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeTicketEntitilementViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TicketEntitilementDTO, EmployeeTicketEntitilementViewModel>.CreateMap();

            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as TicketEntitilementDTO;
            var vm = Mapper<TicketEntitilementDTO, EmployeeTicketEntitilementViewModel>.Map(stDtO);       
            
            vm.NoOfDays = stDtO.NoOfDays;
            vm.TicketEntitilement = stDtO.TicketEntitilement1;
            vm.TicketEntitilementID = stDtO.TicketEntitilementID;          
            vm.CountryAirport = new KeyValueViewModel() { Key = stDtO.CountryAirport.Key, Value = stDtO.CountryAirport.Value };

            return vm;

        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeTicketEntitilementViewModel, TicketEntitilementDTO>.CreateMap();
            var dto = Mapper<EmployeeTicketEntitilementViewModel, TicketEntitilementDTO>.Map(this);            
            dto.NoOfDays = this.NoOfDays;
            dto.TicketEntitilement1 = this.TicketEntitilement;
            dto.TicketEntitilementID = this.TicketEntitilementID;
            dto.CountryAirportID = CountryAirport == null || string.IsNullOrEmpty(CountryAirport.Key) ? (int?)null : int.Parse(CountryAirport.Key);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketEntitilementDTO>(jsonString);
        }

    }
}