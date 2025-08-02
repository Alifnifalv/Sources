using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System.Linq;
using Eduegate.Services.Contracts.School.Academics;

namespace Eduegate.Web.Library.School.Academics
{
    public class ClassCoordinatorMapViewModel : BaseMasterViewModel
    {
        public ClassCoordinatorMapViewModel()
        {
            IsActive = false;
            Section = new List<KeyValueViewModel>();
            Class = new List<KeyValueViewModel>();
        }


        public long ClassCoordinatorIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true)]
        [LookUp("LookUps.AllClass")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Sections", "Numeric", true)]
        [LookUp("LookUps.AllSection")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }
        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("HM", "Numeric", false)]
        [LookUp("LookUps.HM")]
        [CustomDisplay("HM")]
        public KeyValueViewModel HeadMaster { get; set; }
        public long? HeadMasterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Teacher", "Numeric", false)]
        [LookUp("LookUps.Teacher")]
        [CustomDisplay("Coordinator")]
        public KeyValueViewModel Coordinator { get; set; }
        public long? CoordinatorID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassCoordinatorsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassCoordinatorMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassCoordinatorsDTO, ClassCoordinatorMapViewModel>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapDTO = dto as ClassCoordinatorsDTO;
            var vm = Mapper<ClassCoordinatorsDTO, ClassCoordinatorMapViewModel>.Map(mapDTO);

            //vm.Class = mapDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = mapDTO.ClassID.ToString(), Value = mapDTO.ClassName } : new KeyValueViewModel();
            vm.Coordinator = mapDTO.CoordinatorID.HasValue ? new KeyValueViewModel() { Key = mapDTO.CoordinatorID.ToString(), Value = mapDTO.EmployeeName } : new KeyValueViewModel();
            vm.HeadMaster = mapDTO.HeadMasterID.HasValue ? new KeyValueViewModel() { Key = mapDTO.HeadMasterID.ToString(), Value = mapDTO.HMName } : new KeyValueViewModel();
            vm.IsActive = mapDTO.ISACTIVE ?? false;

            vm.Section = new List<KeyValueViewModel>();
            vm.Class = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.ClassCoordinatorClassMaps)
            {
                if (map.ClassID.HasValue &&
                    !vm.Class.Any(x => x.Key == map.ClassID.Value.ToString()))
                {
                    vm.Class.Add(new KeyValueViewModel()
                    {
                        Key = map.Class.Key,
                        Value = map.Class.Value
                    });
                }

                if (map.AllClass == true)
                {
                    vm.Class.Add(new KeyValueViewModel()
                    {
                        Key = null,
                        Value = "All Classes"
                    });
                }

                if (map.SectionID.HasValue &&
                    !vm.Section.Any(x => x.Key == map.SectionID.Value.ToString()))
                {
                    vm.Section.Add(new KeyValueViewModel()
                    {
                        Key = map.Section.Key,
                        Value = map.Section.Value
                    });
                }

                if (map.AllSection == true)
                {
                    vm.Section.Add(new KeyValueViewModel()
                    {
                        Key = null,
                        Value = "All Section"
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassCoordinatorMapViewModel, ClassCoordinatorsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<ClassCoordinatorMapViewModel, ClassCoordinatorsDTO>.Map(this);

            dto.ISACTIVE = this.IsActive;
            dto.CoordinatorID = string.IsNullOrEmpty(this.Coordinator.Key) ? (int?)null : int.Parse(this.Coordinator.Key);
            dto.HeadMasterID = string.IsNullOrEmpty(this.HeadMaster.Key) ? (int?)null : int.Parse(this.HeadMaster.Key);
            //dto.AcademicYearID = string.IsNullOrEmpty(this.Acade.Key) ? (int?)null : int.Parse(this.Academic.Key);


            if (this.Class != null && this.Class.Count > 0)
            {
                if (this.Class.Any(x => x.Value.Contains("All Classes")))
                {
                    var cls = this.Class.FirstOrDefault(x => x.Value == "All Classes");

                    dto.ClassCoordinatorClassMaps.Add(new ClassCoordinatorClassMapDTO()
                    {
                        AllClass = (cls.Value == null || cls.Value == "") &&
                        cls.Value != "All Classes"
                            ? false : true,
                        Class = new KeyValueDTO() { Key = null, Value = "All Classes" }
                    });
                }
                else
                {
                    foreach (var cls in this.Class)
                    {
                        if (!string.IsNullOrEmpty(cls.Key))
                        {
                            dto.ClassCoordinatorClassMaps.Add(new ClassCoordinatorClassMapDTO()
                            {
                                ClassID = int.Parse(cls.Key),
                                Class = new KeyValueDTO() { Key = cls.Key, Value = cls.Value }
                            });
                        }
                    }
                }
            }

            if (this.Section != null && this.Section.Count > 0)
            {
                if (this.Section.Any(x => x.Value.Contains("All Section")))
                {
                    var sec = this.Section.FirstOrDefault(x => x.Value == "All Section");

                    dto.ClassCoordinatorClassMaps.Add(new ClassCoordinatorClassMapDTO()
                    {
                        AllSection = (sec.Value == null || sec.Value == "") &&
                        sec.Value != "All Section"
                            ? false : true,
                        Section = new KeyValueDTO() { Key = null, Value = "All Section" },
                    });
                }
                else
                {
                    foreach (var sec in this.Section)
                    {
                        if (!string.IsNullOrEmpty(sec.Key))
                        {
                            dto.ClassCoordinatorClassMaps.Add(new ClassCoordinatorClassMapDTO()
                            {
                                SectionID = int.Parse(sec.Key),
                                Section = new KeyValueDTO() { Key = sec.Key, Value = sec.Value }
                            });
                        }
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassCoordinatorsDTO>(jsonString);
        }

    }
}