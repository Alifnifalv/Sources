using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Subject", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class SubjectViewModel : BaseMasterViewModel
    {
        public SubjectViewModel()
        {
            //SubjectType = new KeyValueViewModel();
        }

       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Subject ID")]
        public int  SubjectID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("SubjectType", "String", false)]
        [CustomDisplay("SubjectType")]
        [LookUp("LookUps.SubjectType")]
        public KeyValueViewModel  SubjectType { get; set; }

        public byte? SubjectTypeID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-change=NameChanges(CRUDModel.ViewModel)")]
        [CustomDisplay("SubjectName")]
        public string  SubjectName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SubjectText")]
        public string SubjectText { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Color Code")]
        public string HexColorCode { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("IconFileName")]
        public string IconFileName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Progress Report Header")]
        public string ProgressReportHeader { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SubjectDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SubjectDTO, SubjectViewModel>.CreateMap();
            var subjectDTO = dto as SubjectDTO;
            var vm = Mapper<SubjectDTO, SubjectViewModel>.Map(subjectDTO);

            vm.SubjectType = new KeyValueViewModel() { Key = subjectDTO.SubjectTypeID.ToString(), Value = subjectDTO.SubjectTypeName };
            vm.ProgressReportHeader = subjectDTO.ProgressReportHeader;
            vm.HexColorCode = subjectDTO.HexColorCode;
            vm.IconFileName = subjectDTO.IconFileName;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SubjectViewModel, SubjectDTO>.CreateMap();
            var dto = Mapper<SubjectViewModel, SubjectDTO>.Map(this);

            dto.SubjectTypeID = Convert.ToByte(this.SubjectType.Key);
            dto.ProgressReportHeader = this.ProgressReportHeader;
            dto.HexColorCode = this.HexColorCode;
            dto.IconFileName = this.IconFileName;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectDTO>(jsonString);
        }
    }
}