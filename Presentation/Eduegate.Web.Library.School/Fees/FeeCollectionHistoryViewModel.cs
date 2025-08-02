using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.School.Fees
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeAssign")]
    public class FeeCollectionHistoryViewModel : BaseMasterViewModel
    {
        public FeeCollectionHistoryViewModel()
        {
            StudentHistories = new List<FeeCollectionHistoryStudentViewModel>() { new FeeCollectionHistoryStudentViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        public string TransactionNumber { get; set; }

        public string CollectionDate { get; set; }

        public decimal? Amount { get; set; }

        public int? FeeCollectionStatusID { get; set; }
        public string FeeCollectionStatus { get; set; }

        public int? FeeCollectionDraftStatusID { get; set; }

        public int? FeeCollectionCollectedStatusID { get; set; }

        public string ParentEmailID { get; set; }

        public int? FeePaymentModeID { get; set; }

        public string FeePaymentMode { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Fee Collection Types")]
        public List<FeeCollectionHistoryStudentViewModel> StudentHistories { get; set; }

    }
}