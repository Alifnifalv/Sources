using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;
using System.Globalization;


namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ShortList", "CRUDModel.ViewModel.ShortList")]
    [DisplayName("Shor List")]
    public class JobInterviewEvaluationApplicantsViewModel : BaseMasterViewModel
    {
        public JobInterviewEvaluationApplicantsViewModel()
        {
            RoundMaps = new List<JobInterviewEvaluationApplicantRoundMapViewModel>() { new JobInterviewEvaluationApplicantRoundMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        public long? ApplicantID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Applicant Name")]
        public string ApplicantName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("")]
        public string space1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("")]
        public string space2 { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("")]
        public string space3 { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("")]
        public string space4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Total")]
        public int? TotalRating { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Total Ratings Earned")]
        public int? TotalRatingGot { get; set; }

        public bool? IsSelected { get; set; } 
        
        public bool? InterviewAccepted { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "roundMaps", Attributes2 = "colspan=10")]
        [DisplayName("")]
        public List<JobInterviewEvaluationApplicantRoundMapViewModel> RoundMaps { get; set; }
    }
}
