using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Web.Library.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Web.Library.ViewModels.Payroll
{
 
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SectorTicketAirfare", "CRUDModel.ViewModel")]
    [DisplayName("SectorTicketAirfare")]
    public class SectorTicketAirfareViewModel : BaseMasterViewModel
    {
        public SectorTicketAirfareViewModel()
        {
            SectorTicketAirfareDetail = new List<SectorTicketAirfareDetailViewModel>() { new SectorTicketAirfareDetailViewModel() };
        }
        public long SectorTicketAirfareIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Validity From")]
        public string ValidityFromString { get; set; }
        public DateTime? ValidityFrom { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Validity To")]
        public string ValidityToString { get; set; }
        public DateTime? ValidityTo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public string Department { get; set; }
        public long? DepartmentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Sector Ticket Airfare Detail")]
        public List<SectorTicketAirfareDetailViewModel> SectorTicketAirfareDetail { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SectorTicketAirfareDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SectorTicketAirfareViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SectorTicketAirfareDTO, SectorTicketAirfareViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<SectorTicketAirfareDetailDTO, SectorTicketAirfareDetailViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var ssDto = dto as SectorTicketAirfareDTO;
            var vm = Mapper<SectorTicketAirfareDTO, SectorTicketAirfareViewModel>.Map(ssDto);            
            vm.ValidityFromString = (ssDto.ValidityFrom.HasValue ? ssDto.ValidityFrom.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.ValidityToString = (ssDto.ValidityTo.HasValue ? ssDto.ValidityTo.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.SectorTicketAirfareDetail = new List<SectorTicketAirfareDetailViewModel>();
            vm.Department = ssDto.DepartmentID.HasValue ? ssDto.DepartmentID.Value.ToString() : string.Empty;
            vm.SectorTicketAirfareDetail = new List<SectorTicketAirfareDetailViewModel>();

            foreach (var detail in ssDto.SectorTicketAirfareDetail)
            {
                if (detail != null && detail.AirportID.HasValue)
                {

                    var sectorTicketAirfareDetailDTO = new SectorTicketAirfareDetailViewModel()
                    {
                        SectorTicketAirfareIID = detail.SectorTicketAirfareIID,
                        FlightClassID = detail.FlightClassID,
                        AirportID = detail.AirportID,
                        ReturnAirportID = detail.ReturnAirportID,
                        Rate = detail.Rate,
                        IsTwoWay=detail.IsTwoWay,
                        GenerateTravelSector = detail.GenerateTravelSector,
                        Airport = KeyValueViewModel.ToViewModel(detail.Airport),
                        ReturnAirport = KeyValueViewModel.ToViewModel(detail.ReturnAirport),
                        FlightClass = KeyValueViewModel.ToViewModel(detail.FlightClass),
                    };
                    vm.SectorTicketAirfareDetail.Add(sectorTicketAirfareDetailDTO);

                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SectorTicketAirfareViewModel, SectorTicketAirfareDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<SectorTicketAirfareDetailViewModel, SectorTicketAirfareDetailDTO>.CreateMap();

            var dto = Mapper<SectorTicketAirfareViewModel, SectorTicketAirfareDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.ValidityFrom = string.IsNullOrEmpty(this.ValidityFromString) ? (DateTime?)null : DateTime.ParseExact(this.ValidityFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.ValidityTo = string.IsNullOrEmpty(this.ValidityToString) ? (DateTime?)null : DateTime.ParseExact(this.ValidityToString, dateFormat, CultureInfo.InvariantCulture);


            dto.DepartmentID = string.IsNullOrEmpty(this.Department) ? (int?)null : int.Parse(this.Department);


            dto.SectorTicketAirfareDetail = new List<SectorTicketAirfareDetailDTO>();

            foreach (var detail in this.SectorTicketAirfareDetail)
            {
                if (detail != null && detail.Airport!=null && detail.Airport.Value!=null && detail.Rate.HasValue)
                {
                    var sectorTicketAirfareDetailDTO = new SectorTicketAirfareDetailDTO()
                    {
                        SectorTicketAirfareIID = detail.SectorTicketAirfareIID,
                        IsTwoWay=detail.IsTwoWay,
                        FlightClassID = detail.FlightClass == null ||
            string.IsNullOrEmpty(detail.FlightClass.Key) ? (short?)null : short.Parse(detail.FlightClass.Key),
                        AirportID = detail.Airport == null ||
            string.IsNullOrEmpty(detail.Airport.Key) ? (long?)null : long.Parse(detail.Airport.Key),
                        ReturnAirportID = detail.ReturnAirport == null ||
            string.IsNullOrEmpty(detail.ReturnAirport.Key) ? (long?)null : long.Parse(detail.ReturnAirport.Key),
                        Rate = detail.Rate,
                        GenerateTravelSector = detail.GenerateTravelSector,
                    };
                    dto.SectorTicketAirfareDetail.Add(sectorTicketAirfareDetailDTO);

                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SectorTicketAirfareDTO>(jsonString);
        }
    }
}
