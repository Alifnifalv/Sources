using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeDueDetails", "CRUDModel.ViewModel.FeeDueDetails")]
    [DisplayName("Fee Due Students List")]
    public class MailFeeDueStatementReportDetailViewModel : BaseMasterViewModel
    {
        public MailFeeDueStatementReportDetailViewModel()
        {
            IsSelected = true;
        }
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width")]
        [CustomDisplay("")]
        public bool? IsSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        public long? StudentID { get; set; }

        public string AsOnDate { get; set; }

        public string SchoolName { get; set; }
        public long? ParentLoginID { get; set; }

        public string Class { get; set; }

        public int? SchoolID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Admission No")]
        public string AdmissionNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Name")]
        public string StudentName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Parent Conct No.")]
        public string ParentContact { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Parent EmailID")]
        public string ParentEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='SendReportMail(gridModel)'")]
        [CustomDisplay("Send Mail")]
        public string SendMail { get; set; }

    }
}
