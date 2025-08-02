using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Fees
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeAssign")]
    public class FeeCollectionHistoryStudentViewModel : BaseMasterViewModel
    {
        public FeeCollectionHistoryStudentViewModel()
        {
            FeeCollectionTypeHistories = new List<FeeCollectionHistoryFeeTypeViewModel>() { new FeeCollectionHistoryFeeTypeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Fee Receipt No")]
        public string FeeReceiptNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [DisplayName("Student")]
        public long? StudentID { get; set; }
        public string StudentName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Collection Date")]
        public string CollectionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        public int? ClassID { get; set; }
        public string ClassName { get; set; }

        public int? SectionID { get; set; }
        public string SectionName { get; set; }

        public byte? SchoolID { get; set; }
        public string SchoolName { get; set; }

        public int? AcademicYearID { get; set; }
        public string AcademicYear { get; set; }

        public int? FeeCollectionStatusID { get; set; }
        public string FeeCollectionStatus { get; set; }

        public int? FeeCollectionDraftStatusID { get; set; }

        public int? FeeCollectionCollectedStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Fee Collection Types")]
        public List<FeeCollectionHistoryFeeTypeViewModel> FeeCollectionTypeHistories { get; set; }

    }
}