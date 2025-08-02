using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Attendences;

namespace Eduegate.Web.Library.School.Academics
{
    public class TimeTableList : BaseMasterViewModel
    {
        public TimeTableList()
        {
            Academic = new KeyValueViewModel();
            IsActive = false;
        }

        
        public string AcademicYearCode { get; set; }

        public int ClassID { get; set; }

        public string ClassDescription { get; set; }


        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }


        public int TimeTableID { get; set; }
        public string TimeTableDescription { get; set; }

        public int StaffNos { get; set; }

        public int SubjectNos { get; set; }
        
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TimeTableDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TimeTableList>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var tableDTO = dto as TimeTableDTO;
            Mapper<TimeTableDTO, TimeTableList>.CreateMap();
            var vm = Mapper<TimeTableDTO, TimeTableList>.Map(tableDTO);
            vm.TimeTableDescription = tableDTO.TimeTableDescription;
            vm.IsActive = tableDTO.IsActivice;
            vm.TimeTableID = tableDTO.TimeTableID;
            vm.AcademicYearID = tableDTO.AcademicYearID;
            vm.Academic = new KeyValueViewModel() { Key = tableDTO.AcademicYearID.ToString(), Value = tableDTO.AcademicYear.Value };
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TimeTableList, TimeTableDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<TimeTableList, TimeTableDTO>.Map(this);
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academic.Key) ? 0 : int.Parse(this.Academic.Key);
            dto.TimeTableDescription = this.TimeTableDescription;
            dto.TimeTableID = this.TimeTableID;
            dto.IsActivice = this.IsActive;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TimeTableDTO>(jsonString);
        }
    }
}
