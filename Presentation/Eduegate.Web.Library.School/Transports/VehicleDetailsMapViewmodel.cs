using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.School.Transports
{
    public class VehicleDetailsMapViewmodel : BaseMasterViewModel
    {
        public VehicleDetailsMapViewmodel()
        {
            //Vehicle = new KeyValueViewModel();
            IsActive = true;
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Vehicle")]
        [Select2("VehicleDetails", "Numeric", false)]
        [LookUp("LookUps.VehicleDetails")]

        public KeyValueViewModel Vehicle { get; set; }
        public long? VehicleID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("RegistrationRenewedDate")]

        public string RegistrationDateString { get; set; }
        public DateTime? RegistrationDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("RegistrationExpiryDate")]

        public string RegistrationExpiryDateString { get; set; }
        public DateTime? RegistrationExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("InsuranceRenewedDate")]

        public string InsuranceIssueDateString { get; set; }
        public DateTime? InsuranceIssueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("InsuranceExpiryDate")]

        public string InsuranceExpiryDateString { get; set; }
        public DateTime? InsuranceExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as VehicleDetailsMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<VehicleDetailsMapViewmodel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<VehicleDetailsMapDTO, VehicleDetailsMapViewmodel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as VehicleDetailsMapDTO;
            var vm = Mapper<VehicleDetailsMapDTO, VehicleDetailsMapViewmodel>.Map(dto as VehicleDetailsMapDTO);
            vm.RegistrationDateString = sDto.RegistrationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.RegistrationExpiryDateString = sDto.RegistrationExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.InsuranceIssueDateString = sDto.InsuranceIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.InsuranceExpiryDateString = sDto.InsuranceExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.Vehicle = new KeyValueViewModel()
            {
                Key = sDto.VehicleID.ToString(),
                Value = sDto.Vehicle.Value
            };
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<VehicleDetailsMapViewmodel, VehicleDetailsMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<VehicleDetailsMapViewmodel, VehicleDetailsMapDTO>.Map(this);
            dto.VehicleID = string.IsNullOrEmpty(this.Vehicle.Key) ? (long?)null : long.Parse(this.Vehicle.Key);
            dto.RegistrationDate = string.IsNullOrEmpty(this.RegistrationDateString) ? (DateTime?)null : DateTime.ParseExact(RegistrationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.RegistrationExpiryDate = string.IsNullOrEmpty(this.RegistrationExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(RegistrationExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.InsuranceIssueDate = string.IsNullOrEmpty(this.InsuranceIssueDateString) ? (DateTime?)null : DateTime.ParseExact(InsuranceIssueDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.InsuranceExpiryDate = string.IsNullOrEmpty(this.InsuranceExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(InsuranceExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            return dto;
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<VehicleDetailsMapDTO>(jsonString);
        }
    }
}



