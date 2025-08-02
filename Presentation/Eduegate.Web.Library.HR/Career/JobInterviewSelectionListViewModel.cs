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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SelectionList", "CRUDModel.ViewModel.SelectionList")]
    [DisplayName("Selection List")]
    public class JobInterviewSelectionListViewModel : BaseMasterViewModel
    {
        public JobInterviewSelectionListViewModel()
        {
            IsSelected = false;
        }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        public long? ApplicantID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Name")]
        public string ApplicantName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Total Rounds")]
        public int? TotalRounds { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Completed Rounds")]
        public int? RoundsCompleted { get; set; } 

        //[ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        //[CustomDisplay("Total")]
        public int? TotalRating { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Ratings Earned")]
        public string TotalRatingEarned { get; set; }
        public int? TotalRatingGot { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSelected")]
        public bool? IsSelected { get; set; } 
        
        public bool? InterviewAccepted { get; set; }
    }
}
