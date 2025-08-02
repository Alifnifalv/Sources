using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.SignUp.SignUps
{
    public class SignupAllocationViewModel : BaseMasterViewModel
    {
        public SignupAllocationViewModel()
        {

        }

        public long SignupSlotAllocationMapIID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Parent Name")]
        public string Parent { get; set; }
        public long? ParentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Student Name")]
        public string Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Faculty")]
        public string OrganizerEmployeeName { get; set; }
        public long? OrganizerEmployeeID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Name")]
        public string SignupName { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }
     


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SlotDate")]
        public string SlotDateString { get; set; }
        public DateTime? SlotDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("SlotTime")]
        public string SlotTimeString { get; set; }

        public long? SignupSlotMapID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ParentEmail")]
        public string GuardianEmailID { get; set; }

        public long? ParentLoginID { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.SignupSlotMapStatuses")]
        public string SlotMapStatus { get; set; }
        public byte? SlotMapStatusID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SignupSlotAllocationMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SignupAllocationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SignupSlotAllocationMapDTO, SignupAllocationViewModel>.CreateMap();
            var allocDtO = dto as SignupSlotAllocationMapDTO;
            var vm = Mapper<SignupSlotAllocationMapDTO, SignupAllocationViewModel>.Map(dto as SignupSlotAllocationMapDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");


            vm.SignupSlotAllocationMapIID = allocDtO.SignupSlotAllocationMapIID;
            vm.SlotMapStatus = allocDtO.SlotMapStatusID.HasValue ? allocDtO.SlotMapStatusID.ToString() : null;
            vm.OrganizerEmployeeID = allocDtO.OrganizerEmployeeID;
            vm.OrganizerEmployeeName = allocDtO.OrganizerEmployeeID.HasValue ? allocDtO.OrganizerEmployeeName : null;
            vm.GuardianEmailID = allocDtO.GuardianEmailID;
            vm.ParentID = allocDtO.ParentID;
            vm.Parent = allocDtO.ParentID.HasValue ? allocDtO.Parent : null;
            vm.StudentID = allocDtO.StudentID;
            vm.Student = allocDtO.StudentID.HasValue ? allocDtO.Student : null;
            vm.SignupName = allocDtO.SignupName;
            vm.SlotDateString = allocDtO.SlotDate.HasValue ? allocDtO.SlotDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.SignupSlotMapID = allocDtO.SignupSlotMapID;
            vm.SlotTimeString = allocDtO.SlotTimeString;
            vm.ParentLoginID = allocDtO.ParentLoginID;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SignupAllocationViewModel, SignupSlotAllocationMapDTO>.CreateMap();
            var dto = Mapper<SignupAllocationViewModel, SignupSlotAllocationMapDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SignupSlotAllocationMapIID = this.SignupSlotAllocationMapIID;
            dto.SlotMapStatusID = string.IsNullOrEmpty(this.SlotMapStatus) ? (byte?)null : byte.Parse(this.SlotMapStatus);
            dto.ParentID = this.ParentID;
            dto.StudentID = this.StudentID;
            dto.OrganizerEmployeeID = this.OrganizerEmployeeID;
            dto.SignupName = this.SignupName;
            dto.SlotDate = string.IsNullOrEmpty(this.SlotDateString) ? (DateTime?)null : DateTime.ParseExact(this.SlotDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.SignupSlotMapID = this.SignupSlotMapID;
            dto.SlotTimeString = this.SlotTimeString;
            dto.GuardianEmailID = this.GuardianEmailID;
            dto.SlotMapStatus = this.SlotMapStatus;
            dto.ParentLoginID=this.ParentLoginID;


            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SignupSlotAllocationMapDTO>(jsonString);
        }

    }
}