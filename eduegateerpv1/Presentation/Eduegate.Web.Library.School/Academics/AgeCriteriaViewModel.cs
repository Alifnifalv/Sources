using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AgeCriteria", "CRUDModel.ViewModel")]
    [DisplayName("Age Criteria")]
    public class AgeCriteriaViewModel : BaseMasterViewModel
    {
        public AgeCriteriaViewModel()
        {
            AgeCriteriaMap = new List<AgeCriteriaMapViewModel>() { new AgeCriteriaMapViewModel() };

        }

        public long AgeCriteriaIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Curriculam")]
        [LookUp("LookUps.SchoolSyllabus")]

        public string CurriculamString { get; set; }
        public byte? CurriculamID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]

        public string Academicyear { get; set; }
        public int? AcademicyearID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("AgeCriteria")]
        public List<AgeCriteriaMapViewModel> AgeCriteriaMap { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AgeCriteriaDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AgeCriteriaViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AgeCriteriaDTO, AgeCriteriaViewModel>.CreateMap();
            Mapper<AgeCriteriaMapDTO, AgeCriteriaMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var mpdto = dto as AgeCriteriaDTO;
            var vm = Mapper<AgeCriteriaDTO, AgeCriteriaViewModel>.Map(dto as AgeCriteriaDTO);
            vm.CurriculamString = mpdto.CurriculumID.ToString();
            vm.Academicyear = mpdto.AcademicYearID.ToString();
            vm.AgeCriteriaIID = mpdto.AgeCriteriaIID;
            vm.AgeCriteriaMap = new List<AgeCriteriaMapViewModel>();
            foreach (var age in mpdto.AgeCriteriaMap)
            {
                vm.AgeCriteriaMap.Add(new AgeCriteriaMapViewModel()
                {
                    StudentClass = age.ClassID.HasValue ? new KeyValueViewModel() { Key = age.Class.Key, Value = age.Class.Value } : null,
                    BirthFromString = age.BirthFrom.HasValue ? age.BirthFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    BirthToString = age.BirthTo.HasValue ? age.BirthTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    MinAge = age.MinAge,
                    MaxAge = age.MaxAge,
                    IsActive = age.IsActive,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AgeCriteriaViewModel, AgeCriteriaDTO>.CreateMap();
            Mapper<AgeCriteriaMapViewModel, AgeCriteriaMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, AgeCriteriaMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<AgeCriteriaViewModel, AgeCriteriaDTO>.Map(this);
            dto.AgeCriteriaMap = new List<AgeCriteriaMapDTO>();
            dto.AgeCriteriaIID = this.AgeCriteriaIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academicyear) ? (int?)null : int.Parse(this.Academicyear);
            dto.CurriculumID = string.IsNullOrEmpty(this.CurriculamString) ? (byte?)null : byte.Parse(this.CurriculamString);
            foreach (var agemapdto in this.AgeCriteriaMap)
            {
                if (agemapdto.StudentClass != null)
                {
                    dto.AgeCriteriaMap.Add(new AgeCriteriaMapDTO()
                    {
                        BirthFrom = string.IsNullOrEmpty(agemapdto.BirthFromString) ? (DateTime?)null : DateTime.ParseExact(agemapdto.BirthFromString, dateFormat, CultureInfo.InvariantCulture),
                        BirthTo = string.IsNullOrEmpty(agemapdto.BirthToString) ? (DateTime?)null : DateTime.ParseExact(agemapdto.BirthToString, dateFormat, CultureInfo.InvariantCulture),
                        IsActive = agemapdto.IsActive,
                        MinAge = agemapdto.MinAge,
                        MaxAge = agemapdto.MaxAge,
                        ClassID = agemapdto.StudentClass != null ? int.Parse(agemapdto.StudentClass.Key) : 0, 
                    });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AgeCriteriaDTO>(jsonString);
        }
    }
}

