using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "RoundMaps", "gridModel.RoundMaps")]
    [DisplayName("")]
    public class JobInterviewEvaluationApplicantRoundMapViewModel : BaseMasterViewModel
    {
        public int? RoundID { get; set; }

        public long? ApplicantID { get; set; }

        public long? InterviewID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Round")]
        public string Round { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width")]
        [CustomDisplay("Held on")]
        public string HeldOnDateString { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Maximum Rating")]
        public int? MaximumRating { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Rating")]
        public int? Rating { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        //[CustomDisplay("Grade")]
        //public string Grade { get; set; }

    }
}