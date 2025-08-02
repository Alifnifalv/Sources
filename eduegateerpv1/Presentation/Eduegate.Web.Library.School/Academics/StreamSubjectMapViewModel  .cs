using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class StreamSubjectMapViewModel : BaseMasterViewModel 
    {
        public StreamSubjectMapViewModel()
        {
            Subjects = new List<KeyValueViewModel>();
            //IsOptionalSubject = false;
        }

        public long StreamSubjectMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled = CRUDModel.ViewModel.StreamSubjectMapIID!=0")]
        [LookUp("LookUps.Streams")]
        [CustomDisplay("Stream")]
        public string Stream { get; set; }
        public byte? StreamID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

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
        //public string NewLine2 { get; set; }

     
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", true, "SearchStudent($event, $element)")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("CompulsorySubjects")]
        public List<KeyValueViewModel> Subjects { get; set; }
        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("OptionalSubject", "Numeric", true, "")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("OptionalSubjects")]
        public List<KeyValueViewModel> OptionalSubject { get; set; }
        public int? OptionalSubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Optional Subject")]
       
        //public bool? IsOptionalSubject { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StreamSubjectMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StreamSubjectMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StreamSubjectMapDTO, StreamSubjectMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var sMapDTO = dto as StreamSubjectMapDTO;
            var vm = Mapper<StreamSubjectMapDTO, StreamSubjectMapViewModel>.Map(sMapDTO);

            vm.Stream = sMapDTO.StreamID.HasValue ? sMapDTO.StreamID.ToString() : null;
            //vm.IsOptionalSubject = sMapDTO.IsOptionalSubject;
            //vm.School = sMapDTO.SchoolID.HasValue ? sMapDTO.SchoolID.ToString() : null;
            //vm.AcademicYear = sMapDTO.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = sMapDTO.AcademicYearID.ToString(), Value = sMapDTO.AcademicYearName } : new KeyValueViewModel();
            vm.Subjects = new List<KeyValueViewModel>();
            foreach (var sub in sMapDTO.Subject)
            {
                if (!string.IsNullOrEmpty(sub.Key))
                {
                    vm.Subjects.Add(new KeyValueViewModel()
                    {
                        Key = sub.Key,
                        Value = sub.Value
                    });
                }
            }
            vm.OptionalSubject = new List<KeyValueViewModel>();
            foreach (var Optionalsub in sMapDTO.OptionalSubject)
            {
                if (!string.IsNullOrEmpty(Optionalsub.Key))
                {
                    vm.OptionalSubject.Add(new KeyValueViewModel()
                    {
                        Key = Optionalsub.Key,
                        Value = Optionalsub.Value
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<StreamSubjectMapViewModel, StreamSubjectMapDTO>.CreateMap();
            var dto = Mapper<StreamSubjectMapViewModel, StreamSubjectMapDTO>.Map(this);

            dto.StreamID = string.IsNullOrEmpty(this.Stream) ? (byte?)null : byte.Parse(this.Stream);
            //dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);

            dto.StreamCompulsorySubjectMap = new List<StreamCompulsorySubjectMapDTO>();

            int S_No = 1;

            foreach (var map in this.Subjects)
            {
                if (!string.IsNullOrEmpty(map.Key))
                {
                    var map1 = new StreamCompulsorySubjectMapDTO
                    {
                        OrderBy = S_No++,
                        Subject = new KeyValueDTO { Key = map.Key, Value = map.Value},
                    };

                    dto.StreamCompulsorySubjectMap.Add(map1);
                }
            }

            dto.StreamOptionalSubjectMap = new List<StreamOptionalSubjectMapDTO>();

            foreach (var optionalSub in this.OptionalSubject)
            {
                if (!string.IsNullOrEmpty(optionalSub.Key))
                {
                    var map2 = new StreamOptionalSubjectMapDTO
                    {
                        OrderBy = S_No++,
                        OptionalSubject = new KeyValueDTO { Key = optionalSub.Key, Value = optionalSub.Value },
                    };

                    dto.StreamOptionalSubjectMap.Add(map2);
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StreamSubjectMapDTO>(jsonString);
        }
    }
}