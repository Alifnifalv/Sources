using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using System.ComponentModel;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.UserDevice;

namespace Eduegate.Web.Library.ViewModels.UserDevice
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "UserDeviceMaps", "CRUDModel.ViewModel")]
    [DisplayName("User Device Maps")]
    public class UserDeviceMapViewModel : BaseMasterViewModel
    {
        public UserDeviceMapViewModel()
        {
            IsActive = false;
        }

        public long UserDeviceMapIID { get; set; }

        public long? LoginID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("LoginUserID")]
        public string LoginUserID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("DeviceToken")]
        public string DeviceToken { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as UserDeviceMapDTO);
        }


        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<UserDeviceMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<UserDeviceMapDTO, UserDeviceMapViewModel>.CreateMap();
            var userDTO = dto as UserDeviceMapDTO;
            var vm = Mapper<UserDeviceMapDTO, UserDeviceMapViewModel>.Map(userDTO);

            vm.UserDeviceMapIID = userDTO.UserDeviceMapIID;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<UserDeviceMapViewModel, UserDeviceMapDTO>.CreateMap();
            var dto = Mapper<UserDeviceMapViewModel, UserDeviceMapDTO>.Map(this);

            dto.LoginID = this.LoginID;
            dto.DeviceToken = this.DeviceToken;
            dto.IsActive = this.IsActive;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<UserDeviceMapDTO>(jsonString);
        }

    }
}