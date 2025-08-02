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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AcademicSchoolMap", "CRUDModel.ViewModel")]
    [DisplayName("Academic School Map")]
    public class AcademicSchoolMapViewModel : BaseMasterViewModel
    {
        public AcademicSchoolMapViewModel()
        {
            SchoolWorkingDayMaps = new List<AcademicSchoolMapWorkingDaysViewModel>() { new AcademicSchoolMapWorkingDaysViewModel() };
        }

        public long SchoolDateSettingIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel)'")]
        [LookUp("LookUps.School")]
        [CustomDisplay("School")]
        public string School { get; set; }
        public int? SchoolID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='AcademicYearChanges($event, $element, CRUDModel.ViewModel)'")]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("WorkingDayMaps")]
        public List<AcademicSchoolMapWorkingDaysViewModel> SchoolWorkingDayMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SchoolDateSettingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicSchoolMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var DTO = dto as SchoolDateSettingDTO;
            Mapper<SchoolDateSettingDTO, AcademicSchoolMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var vm = Mapper<SchoolDateSettingDTO, AcademicSchoolMapViewModel>.Map(DTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.AcademicYearID = DTO.AcademicYearID;
            vm.AcademicYear = DTO.AcademicYearID.HasValue ? DTO.AcademicYearID.ToString() : null;
            vm.School = DTO.SchoolID.HasValue ? DTO.SchoolID.ToString() : null;

            vm.SchoolWorkingDayMaps = new List<AcademicSchoolMapWorkingDaysViewModel>();

            foreach (var map in DTO.SchoolDateSettingMaps)
            {
                if (map.MonthID.HasValue)
                {
                    vm.SchoolWorkingDayMaps.Add(new AcademicSchoolMapWorkingDaysViewModel()
                    {
                        MonthID = map.MonthID,
                        YearID = map.YearID,
                        MonthName = map.MonthName,
                        Description = map.Description,
                        TotalWorkingDays = map.TotalWorkingDays,
                        PayrollCutOffDateString = map.PayrollCutoffDate.HasValue ? map.PayrollCutoffDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AcademicSchoolMapViewModel, SchoolDateSettingDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<AcademicSchoolMapViewModel, SchoolDateSettingDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SchoolDateSettingIID = this.SchoolDateSettingIID;
            dto.AcademicYearID = this.AcademicYear == null || string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);

            dto.SchoolDateSettingMaps = new List<SchoolDateSettingMapDTO>();

            if (this.SchoolWorkingDayMaps.Count > 0)
            {
                foreach (var dateSettings in this.SchoolWorkingDayMaps)
                {
                    if (dateSettings.MonthID.HasValue) 
                    {
                        dto.SchoolDateSettingMaps.Add(new SchoolDateSettingMapDTO
                        {
                            SchoolDateSettingMapsIID = dateSettings.SchoolDateSettingMapsIID,
                            SchoolDateSettingID = dateSettings.SchoolDateSettingID,
                            MonthID = dateSettings.MonthID,
                            YearID = dateSettings.YearID,
                            Description = dateSettings.Description,
                            TotalWorkingDays = dateSettings.TotalWorkingDays,
                            PayrollCutoffDate = string.IsNullOrEmpty(dateSettings.PayrollCutOffDateString) ? (DateTime?)null : DateTime.ParseExact(dateSettings.PayrollCutOffDateString, dateFormat, CultureInfo.InvariantCulture),
                            //AttachmentDescription = attachment.AttachmentDescription,
                            CreatedBy = dateSettings.CreatedBy,
                            UpdatedBy = dateSettings.UpdatedBy,
                            CreatedDate = dateSettings.CreatedDate,
                            UpdatedDate = dateSettings.UpdatedDate,
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SchoolDateSettingDTO>(jsonString);
        }

    }
}