using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.School.Circulars
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Circular", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class CircularViewModel : BaseMasterViewModel
    {
        public CircularViewModel()
        {
            IsSendPushNotification = true;
            Section = new List<KeyValueViewModel>();
            Class = new List<KeyValueViewModel>();
            Departments = new List<KeyValueViewModel>();
            CircularUserType = new List<KeyValueViewModel>();
            CircularAttachments = new List<CircularAttachmentMapViewModel>() { new CircularAttachmentMapViewModel() };
        }

        public long CircularIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Circular No.")]
        public string CircularCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CircularTypes")]
        [CustomDisplay("Circular Type")]
        public string CircularType { get; set; }
        public int? CircularTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CircularPriorities")]
        [CustomDisplay("Priority")]
        public string CircularPriority { get; set; }
        public int? PriorityID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Short Description")]
        public string ShortTitle { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", "")]
        [CustomDisplay("Circular Date")]
        public string CircularDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", "")]
        [CustomDisplay("Expiry Date")]
        public string ExpiryDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true, "")]
        [LookUp("LookUps.AllClass")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Section", "Numeric", true, "")]
        [LookUp("LookUps.AllSection")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }
        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Departments", "Numeric", true, "")]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Departments")]
        public List<KeyValueViewModel> Departments { get; set; }
        public long? DepartmentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CircularUserType", "Numeric", true, "")]
        [LookUp("LookUps.CircularUserTypes")]
        [CustomDisplay("Users")]
        public List<KeyValueViewModel> CircularUserType { get; set; }
        public byte? CircularUserTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CircularStatus")]
        [CustomDisplay("Status")]
        public string CircularStatus { get; set; }
        public byte? CircularStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendPushNotification")]
        public bool? IsSendPushNotification { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("Message")]
        public string Message { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Attachment")]
        public List<CircularAttachmentMapViewModel> CircularAttachments { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CircularDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CircularViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CircularDTO, CircularViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var cirDto = dto as CircularDTO;
            var vm = Mapper<CircularDTO, CircularViewModel>.Map(dto as CircularDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.CircularType = cirDto.CircularTypeID.HasValue ? cirDto.CircularTypeID.ToString() : null;
            vm.CircularPriority = cirDto.CircularPriorityID.HasValue ? cirDto.CircularPriorityID.ToString() : null;
            vm.AcademicYear = cirDto.AcadamicYearID.HasValue ? new KeyValueViewModel() { Key = cirDto.AcademicYear.Key, Value = cirDto.AcademicYear.Value } : new KeyValueViewModel();
            vm.CircularDateString = cirDto.CircularDate.HasValue ? cirDto.CircularDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ExpiryDateString = cirDto.ExpiryDate.HasValue ? cirDto.ExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.CircularStatus = cirDto.CircularStatusID.HasValue ? cirDto.CircularStatusID.ToString() : null;
            vm.IsSendPushNotification = false;

            foreach (var map in cirDto.CircularMaps)
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

                if (map.DepartmentID.HasValue &&
                    !vm.Departments.Any(x => x.Key == map.DepartmentID.Value.ToString()))
                {
                    vm.Departments.Add(new KeyValueViewModel()
                    {
                        Key = map.Department.Key,
                        Value = map.Department.Value
                    });
                }

                if (map.AllDepartment == true)
                {
                    vm.Departments.Add(new KeyValueViewModel()
                    {
                        Key = null,
                        Value = "All Departments"
                    });
                }
            }

            foreach (var typeMap in cirDto.CircularUserTypeMaps)
            {
                vm.CircularUserType.Add(new KeyValueViewModel()
                {
                    Key = typeMap.CircularUserType.Key,
                    Value = typeMap.CircularUserType.Value
                });
            }

            vm.CircularAttachments = new List<CircularAttachmentMapViewModel>();

            if (cirDto.CircularAttachmentMaps != null)
            {
                foreach (var attach in cirDto.CircularAttachmentMaps)
                {
                    if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                    {
                        vm.CircularAttachments.Add(new CircularAttachmentMapViewModel
                        {
                            CircularAttachmentMapIID = attach.CircularAttachmentMapIID,
                            CircularID = attach.CircularID,
                            ContentFileName = attach.AttachmentName,
                            ContentFileIID = attach.AttachmentReferenceID,
                            AttachmentDescription = attach.AttachmentDescription,
                        });
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CircularViewModel, CircularDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<CircularViewModel, CircularDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.AcadamicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.CircularPriorityID = string.IsNullOrEmpty(this.CircularPriority) ? (byte?)null : byte.Parse(this.CircularPriority);
            dto.CircularTypeID = string.IsNullOrEmpty(this.CircularType) ? (byte?)null : byte.Parse(this.CircularType);
            dto.CircularDate = string.IsNullOrEmpty(this.CircularDateString) ? (DateTime?)null : DateTime.ParseExact(this.CircularDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ExpiryDate = string.IsNullOrEmpty(this.ExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.ExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.CircularStatusID = string.IsNullOrEmpty(this.CircularStatus) ? (byte?)null : byte.Parse(this.CircularStatus);

            dto.CircularMaps = new List<CircularMapDTO>();
            if (this.Class != null && this.Class.Count > 0)
            {
                if (this.Class.Any(x => x.Value.Contains("All Classes")))
                {
                    var cls = this.Class.FirstOrDefault(x => x.Value == "All Classes");

                    dto.CircularMaps.Add(new CircularMapDTO()
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
                            dto.CircularMaps.Add(new CircularMapDTO()
                            {
                                ClassID = int.Parse(cls.Key),
                                Class =  new KeyValueDTO() { Key = cls.Key, Value = cls.Value }
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

                    dto.CircularMaps.Add(new CircularMapDTO()
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
                            dto.CircularMaps.Add(new CircularMapDTO()
                            {
                                SectionID = int.Parse(sec.Key),
                                Section = new KeyValueDTO() { Key = sec.Key, Value = sec.Value } 
                            });
                        }
                    }
                }
            }

            if (this.Departments != null && this.Departments.Any())
            {
                if (this.Departments.Any(x => x.Value.Contains("All Departments")))
                {
                    dto.CircularMaps.Add(new CircularMapDTO
                    {
                        AllDepartment = true
                    });
                }
                else
                {
                    foreach (var dep in this.Departments.Where(d => !string.IsNullOrEmpty(d.Key)))
                    {
                        if (int.TryParse(dep.Key, out int departmentId))
                        {
                            dto.CircularMaps.Add(new CircularMapDTO
                            {
                                AllDepartment = false,
                                DepartmentID = departmentId,
                                Department = new KeyValueDTO { Key = dep.Key, Value = dep.Value }
                            });
                        }
                    }
                }
            }
            else
            {
                dto.CircularMaps.Add(new CircularMapDTO
                {
                    AllDepartment = true
                });
            }

            dto.CircularUserTypeMaps = new List<CircularUserTypeMapDTO>();
            foreach (var userType in this.CircularUserType)
            {
                dto.CircularUserTypeMaps.Add(new CircularUserTypeMapDTO()
                {
                    CircularUserTypeID = string.IsNullOrEmpty(userType.Key) ? (byte?)null : byte.Parse(userType.Key),
                    CircularUserType = !string.IsNullOrEmpty(userType.Key) ? new KeyValueDTO() { Key = userType.Key, Value = userType.Value } : new KeyValueDTO() { Key = null, Value = "All User" },
                });
            }

            dto.CircularAttachmentMaps = new List<CircularAttachmentMapDTO>();

            if (this.CircularAttachments.Count > 0)
            {
                foreach (var attachment in this.CircularAttachments)
                {
                    if (attachment.ContentFileIID.HasValue || !string.IsNullOrEmpty(attachment.ContentFileName))
                    {
                        dto.CircularAttachmentMaps.Add(new CircularAttachmentMapDTO
                        {
                            CircularAttachmentMapIID = attachment.CircularAttachmentMapIID,
                            CircularID = attachment.CircularID.HasValue ? attachment.CircularID : null,
                            AttachmentReferenceID = attachment.ContentFileIID,
                            AttachmentName = attachment.ContentFileName,
                            AttachmentDescription = attachment.AttachmentDescription,
                            CreatedBy = attachment.CreatedBy,
                            UpdatedBy = attachment.UpdatedBy,
                            CreatedDate = attachment.CreatedDate,
                            UpdatedDate = attachment.UpdatedDate,
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CircularDTO>(jsonString);
        }
    }
}

