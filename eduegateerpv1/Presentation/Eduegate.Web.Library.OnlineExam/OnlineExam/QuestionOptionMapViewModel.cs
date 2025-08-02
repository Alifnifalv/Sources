using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "QuestionOptionMaps", "CRUDModel.ViewModel.QuestionOptionMaps")]
    [DisplayName("Question Option")]
    public class QuestionOptionMapViewModel : BaseMasterViewModel
    {
        public QuestionOptionMapViewModel()
        {
            IsCorrectAnswer = false;
        }

        public long QuestionOptionMapIID { get; set; }

        public long? QuestionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Option text")]
        public string OptionText { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[DisplayName("Option Image")]
        //[FileUploadInfo("Content/UploadContents", Framework.Enums.EduegateImageTypes.UserProfile, "ProfileUrl")]
        //public string ImageName { get; set; }
        //public long? ContentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [DisplayName("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ContentFileIID", "")]
        public long? ContentFileIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string ContentFileName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, attribs: "ng-change='AnswerSelectionClick(CRUDModel.ViewModel, gridModel)'")]
        [DisplayName("Is correct")]
        public bool? IsCorrectAnswer { get; set; }

        public int? OrderNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.QuestionOptionMaps[0], CRUDModel.ViewModel.QuestionOptionMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.QuestionOptionMaps[0],CRUDModel.ViewModel.QuestionOptionMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}