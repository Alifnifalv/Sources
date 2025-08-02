using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AcademicClassMap", "CRUDModel.ViewModel")]
    [DisplayName("Academic Class Map")]
    public class AcademicClassMapViewModel : BaseMasterViewModel
    {
        public AcademicClassMapViewModel()
        {
            AcademicYear = new KeyValueViewModel();
            Class = new List<KeyValueViewModel>();
            WorkingDayMaps = new List<AcademicClassMapWorkingDaysViewModel>() { new AcademicClassMapWorkingDaysViewModel() };
        }

        public long AcademicClassMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false, "AcademicYearChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("WorkingDayMaps")]
        public List<AcademicClassMapWorkingDaysViewModel> WorkingDayMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AcademicClassMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicClassMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AcademicClassMapDTO, AcademicClassMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<AcademicClassMapDTO, AcademicClassMapWorkingDaysViewModel>.CreateMap();

            var vm = Mapper<AcademicClassMapDTO, AcademicClassMapViewModel>.Map(dto as AcademicClassMapDTO);
            var mpdto = dto as AcademicClassMapDTO;

            vm.AcademicYear = mpdto.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = mpdto.AcademicYear.Key.ToString(), Value = mpdto.AcademicYear.Value } : new KeyValueViewModel();

            vm.WorkingDayMaps = new List<AcademicClassMapWorkingDaysViewModel>();

            foreach (var map in mpdto.AcademicClassMapWorkingDayDTO)
            {
                vm.WorkingDayMaps.Add(new AcademicClassMapWorkingDaysViewModel()
                {
                    MonthID = map.MonthID,
                    YearID = map.YearID,
                    MonthName = map.MonthName,
                    Description = map.Description,
                    TotalWorkingDays = map.TotalWorkingDays,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AcademicClassMapViewModel, AcademicClassMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<AcademicClassMapWorkingDaysViewModel, AcademicClassMapWorkingDayDTO>.CreateMap();

            var dto = Mapper<AcademicClassMapViewModel, AcademicClassMapDTO>.Map(this);

            dto.AcademicClassMapIID = this.AcademicClassMapIID;
            dto.AcademicYearID = this.AcademicYear == null || string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);

            List<KeyValueDTO> classList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.Class)
            {
                classList.Add(new KeyValueDTO()
                {
                    Key = vm.Key,
                    Value = vm.Value
                });
            }

            dto.Class = classList;

            dto.AcademicClassMapWorkingDayDTO = new List<AcademicClassMapWorkingDayDTO>();

            foreach (var map in this.WorkingDayMaps)
            {
                if (map.MonthID.HasValue)
                {
                    dto.AcademicClassMapWorkingDayDTO.Add(new AcademicClassMapWorkingDayDTO()
                    {
                        MonthID = map.MonthID,
                        YearID = map.YearID,
                        Description = map.Description,
                        TotalWorkingDays = map.TotalWorkingDays,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicClassMapDTO>(jsonString);
        }

    }
}