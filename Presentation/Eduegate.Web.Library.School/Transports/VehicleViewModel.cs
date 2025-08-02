using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Framework.Contracts.Common;
using Newtonsoft.Json;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{

    public class VehicleViewModel : BaseMasterViewModel
    {
        public VehicleViewModel()
        {
            IsCameraEnabled = true;
            IsSecurityEnabled = true;
            IsActive = true;
        }
        public long VehicleIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("FleetCode")]
        public string FleetCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VehicleType")]
        [CustomDisplay("VehicleType")]
        public string VehicleType { get; set; }

        public short? VehicleTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VehicleOwnershipType")]
        [CustomDisplay("VehicleOwnershipType")]
        public string VehicleOwnershipType { get; set; }

        public short? VehicleOwnershipTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("VehicleRegistrationNumber")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string VehicleRegistrationNumber { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("BusNumber")]
        //[MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        //public string VehicleCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string Description { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Registration No")]
        //[MaxLength(50)]
        //public string RegistrationNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("PurchaseDate")]

        public string PurchaseDateString { get; set; }
        public System.DateTime? PurchaseDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ModelName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ModelName { get; set; }

        //[Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("YearMade")]
        public int? YearMade { get; set; }

        //[Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("VehicleAge")]
        public int? VehicleAge { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VehicleTransmissions")]
        [CustomDisplay("Transmission")]
        public string Transmission { get; set; }

        public byte? TransmissionID { get; set; }

        //[Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ManufactureName")]
     
        public string ManufactureName { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Color")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Color { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Power")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Power { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AllowSeatCapacity")]
        public int? AllowSeatingCapacity { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumSeatCapacity")]
        public int? MaximumSeatingCapacity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSecurityEnabled")]
        public bool? IsSecurityEnabled { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsCameraEnabled")]
        public bool? IsCameraEnabled { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as VehicleDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<VehicleViewModel>(jsonString);
        }


        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<VehicleDTO, VehicleViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as VehicleDTO;
            var vm = Mapper<VehicleDTO, VehicleViewModel>.Map(dto as VehicleDTO);
            vm.PurchaseDateString = sDto.PurchaseDate.HasValue ? sDto.PurchaseDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.VehicleType = sDto.VehicleTypeID.ToString();
            vm.VehicleOwnershipType = sDto.VehicleOwnershipTypeID.ToString();
            vm.Transmission = sDto.TransmissionID.ToString();
            
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<VehicleViewModel, VehicleDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<VehicleViewModel, VehicleDTO>.Map(this);
            dto.VehicleTypeID = string.IsNullOrEmpty(this.VehicleType) ? (short?)null : short.Parse(this.VehicleType);
            dto.VehicleOwnershipTypeID = string.IsNullOrEmpty(this.VehicleOwnershipType) ? (short?)null : short.Parse(this.VehicleOwnershipType);
            dto.TransmissionID = string.IsNullOrEmpty(this.Transmission) ? (byte?)null : byte.Parse(this.Transmission);
            dto.PurchaseDate = string.IsNullOrEmpty(this.PurchaseDateString) ? (DateTime?)null : DateTime.ParseExact(PurchaseDateString, dateFormat, CultureInfo.InvariantCulture);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<VehicleDTO>(jsonString);
        }
    }
}