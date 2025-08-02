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
    public class JobApplicationShortListViewModel : BaseMasterViewModel
    {
        public JobApplicationShortListViewModel()
        {

        }

        public long ApplicationIID { get; set; }
        public long? JobID { get; set; }
        public long? ApplicantID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }
        public bool? IsShortListed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Applied on")]
        public string AppliedDateString { get; set; }  
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Applicant")]
        public string ApplicantName { get; set; }  
        

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Education")]
        public string Education { get; set; }       

        [ControlType(Framework.Enums.ControlTypes.Label)] 
        [CustomDisplay("Total year of experience")]
        public int? TotalYearOfExperience { get; set; }


        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent,"", "ViewContent")]
        [CustomDisplay("Cv")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ContentFileIID", "")]
        public long? CVContentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.ShortList[0],CRUDModel.ViewModel.ShortList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
