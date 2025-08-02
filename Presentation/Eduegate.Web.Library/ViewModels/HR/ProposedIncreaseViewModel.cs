using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.HR
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ProposedIncreaseVM", "CRUDModel.Model.MasterViewModel.ProposedIncrease.ProposedIncreases", "header-list", "", "", "", true)]
    [DisplayName("ProposedIncreaseVM")]
    public class ProposedIncreaseViewModel : BaseMasterViewModel
    {

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, optionalAttribs: "ng-change='SetProposedIncreaseAmount($event,$element,$index)'")]
        [DisplayName("PROPOSED INCREASE AMOUNT")]
        public decimal? ProposedIncreaseAmount { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, optionalAttribs: "ng-blur='SetProposedIncreasePercent($event,$element,$index)'")]
        [DisplayName("Proposed Increase %")]
        public decimal? ProposedIncreasePercentage { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, optionalAttribs: "ng-disabled='true'")]
        [DisplayName("PROPOSED INCREASE")]
        public decimal? ProposedIncrease { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("SALARY CHANGE AFTER PERIOD")]
        [Select2("PeriodSalary", "Numeric", false, "", false)]
        [LookUp("LookUps.PeriodSalary")]
        public KeyValueViewModel SalaryChangeAfterPeriod { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("REMARKS")]
        public string Remarks { get; set; }

        public string CRE_BY { get; set; }

        public DateTime? CRE_DT { get; set; }

        public string CRE_PROG_ID { get; set; }

        public string CRE_IP { get; set; }

        public string CRE_WEBUSER { get; set; }

        public string UPD_BY { get; set; }

        public DateTime? UPD_DT { get; set; }

        public string UPD_PROG_ID { get; set; }

        public string UPD_IP { get; set; }

        public string UPD_WEBUSER { get; set; }


    }
}
