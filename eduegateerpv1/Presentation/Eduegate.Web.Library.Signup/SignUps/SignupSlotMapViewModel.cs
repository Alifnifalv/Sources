using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System;

namespace Eduegate.Web.Library.SignUp.SignUps
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SignupSlotMaps", "CRUDModel.ViewModel.SignupSlotMaps")]
    [DisplayName("Slot Details")]
    public class SignupSlotMapViewModel : BaseMasterViewModel
    {
        public SignupSlotMapViewModel()
        {
            SignupSlotType = new Domain.Setting.SettingBL().GetSettingValue<string>("SIGNUP_SLOT_TYPEID_SINGLE", 1);
            SlotMapStatus = new Domain.Setting.SettingBL().GetSettingValue<string>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);
        }

        public long SignupSlotMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", "", "", "ng-disabled='gridModel.SignupSlotMapIID >= 0'")]
        [CustomDisplay("Type")]
        [LookUp("LookUps.SignupSlotTypes")]
        public string SignupSlotType { get; set; }
        public byte SignupSlotTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change='GridDateChanges(CRUDModel.ViewModel, gridModel)' ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID && gridModel.SignupSlotMapIID > 0'")]
        [CustomDisplay("Date")]
        public string SlotDateString { get; set; }
        public DateTime? SlotDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker, Attributes = "ng-change='GridTimeChanges(CRUDModel.ViewModel, gridModel)' ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID && gridModel.SignupSlotMapIID > 0'")]
        [CustomDisplay("StartTime")]
        public string StartTimeString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker, Attributes = "ng-change='GridTimeChanges(CRUDModel.ViewModel, gridModel)' ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID && gridModel.SignupSlotMapIID > 0'")]
        [CustomDisplay("EndTime")]
        public string EndTimeString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Duration")]
        public decimal? Duration { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.SignupSlotMapStatuses")]
        public string SlotMapStatus { get; set; }
        public byte? SlotMapStatusID { get; set; }

        public byte? OldSlotMapStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.SignupSlotMaps[0], CRUDModel.ViewModel.SignupSlotMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SignupSlotMaps[0],CRUDModel.ViewModel.SignupSlotMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}