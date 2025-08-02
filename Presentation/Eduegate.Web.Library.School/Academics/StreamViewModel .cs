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
    public class StreamViewModel : BaseMasterViewModel
    {
        public StreamViewModel()
        {

        }

        public byte StreamID { get; set; }
       
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.School")]
        //[DisplayName("School")]
        //public string School { get; set; }
        public byte? SchoolID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        //[Select2("AcademicYear", "String", false, "")]
        //[LookUp("LookUps.AcademicYear")]
        //[DisplayName("AcademicYear")]
        //public KeyValueViewModel AcademicYear{ get; set; }
        public int? AcademicYearID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Code")]
        public string Code { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("StreamDescription")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.StreamGroups")]
        [CustomDisplay("StreamGroup")]
        public string StreamGroup { get; set; }
        public byte? StreamGroupID { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool IsActive { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StreamDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StreamViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StreamDTO, StreamViewModel>.CreateMap();
            var streamDTO = dto as StreamDTO;
            var vm = Mapper<StreamDTO, StreamViewModel>.Map(streamDTO);
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            //vm.School = streamDTO.SchoolID.HasValue ? streamDTO.SchoolID.ToString() : null;
            //vm.AcademicYear = streamDTO.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = streamDTO.AcademicYearID.ToString(), Value = streamDTO.AcademicYearName } : new KeyValueViewModel();
            vm.StreamGroup = streamDTO.StreamGroupID.HasValue ? streamDTO.StreamGroupID.ToString() : null;
            vm.IsActive = streamDTO.IsActive ?? false;


            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<StreamViewModel, StreamDTO>.CreateMap();
            var dto = Mapper<StreamViewModel, StreamDTO>.Map(this);

            //dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            //dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.StreamGroupID = string.IsNullOrEmpty(this.StreamGroup) ? (byte?)null : byte.Parse(this.StreamGroup);
            dto.IsActive = this.IsActive;


            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StreamDTO>(jsonString);
        }
    }
}