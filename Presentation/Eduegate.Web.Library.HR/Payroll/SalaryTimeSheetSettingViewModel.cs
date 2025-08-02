using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TimeSheetSetting", "CRUDModel.ViewModel.TimeSheetSetting")]
    [DisplayName("Timesheet Settings")]
    public class SalaryTimeSheetSettingViewModel : BaseMasterViewModel
    {
        public SalaryTimeSheetSettingViewModel()
        {
            TimeSheetSalaryComponent = new KeyValueViewModel();
        }

        //[ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "fullwidth alignleft")]
        //[DisplayName(" ")]
        //public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSalaryBasedOnTimeSheet")]
        public bool? IsSalaryBasedOnTimeSheet { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Components", "Numeric", false, "")]
        [LookUp("LookUps.SalaryComponent")]
        [CustomDisplay("TimeSheetSalaryComponent")]
        public KeyValueViewModel TimeSheetSalaryComponent { get; set; }
        public int? TimeSheetSalaryComponentID { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("TimeSheetHourRate")]
        public decimal? TimeSheetHourRate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("TimeSheetLeaveEncashmentPerDay")]
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("TimeSheetMaximumBenefits")]
        public decimal? TimeSheetMaximumBenefits { get; set; }
    }
}