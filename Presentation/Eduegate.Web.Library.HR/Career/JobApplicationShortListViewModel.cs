using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.HR.Career
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ShortList", "CRUDModel.ViewModel.ShortList")]
    [DisplayName("Short List")]
    public class JobInterviewShortListViewModel : BaseMasterViewModel
    {
        public JobInterviewShortListViewModel()
        {

        }

        public long ApplicationIID { get; set; }
        public long? JobID { get; set; }
        public long? ApplicantID { get; set; }
        public string ApplicantMailID { get; set; }
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Applicant")]
        public string ApplicantName { get; set; }  
        

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Education")]
        public string Education { get; set; }       

        public int? TotalYearOfExperience { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("Start Time")]
        public string StartTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("End Time")]
        public string EndTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.ShortList[0],CRUDModel.ViewModel.ShortList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
