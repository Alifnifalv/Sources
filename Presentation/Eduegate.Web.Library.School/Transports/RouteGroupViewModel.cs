using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.School.Transports;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RouteGroup", "CRUDModel.ViewModel")]
    [DisplayName("Route Group")]
    public class RouteGroupViewModel : BaseMasterViewModel
    {
        public RouteGroupViewModel()
        {
            IsActive = true;
            //RouteList = new List<RouteGroupRouteListViewModel>() { new RouteGroupRouteListViewModel() };
        }

        public int RouteGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width", "textleft")]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.School")]
        [CustomDisplay("School")]
        public string School { get; set; }
        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false, "")]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine2 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[CustomDisplay("Route List")]
        //public List<RouteGroupRouteListViewModel> RouteList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RouteGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RouteGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RouteGroupDTO, RouteGroupViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var grpDTO = dto as RouteGroupDTO;
            var vm = Mapper<RouteGroupDTO, RouteGroupViewModel>.Map(grpDTO);

            vm.AcademicYear = grpDTO.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = grpDTO.AcademicYear.Key, Value = grpDTO.AcademicYear.Value } : new KeyValueViewModel();
            vm.School = grpDTO.SchoolID.HasValue ? grpDTO.SchoolID.ToString() : null;

            //vm.RouteList = new List<RouteGroupRouteListViewModel>();
            //foreach (var studMap in grpDTO.HealthEntryStudentMap)
            //{
            //    vm.RouteList.Add(new RouteGroupRouteListViewModel()
            //    {
            //        StudentID = studMap.StudentID,
            //        HealthEntryStudentMapIID = studMap.HealthEntryStudentMapIID,
            //        StudentName = studMap.StudentName,
            //        Height = studMap.Height,
            //        Weight = studMap.Weight,
            //        BMS = studMap.BMS,
            //        Remarks = studMap.Remarks,
            //    });
            //}

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RouteGroupViewModel, RouteGroupDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<RouteGroupViewModel, RouteGroupDTO>.Map(this);

            dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.IsActive = this.IsActive;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RouteGroupDTO>(jsonString);
        }

    }
}