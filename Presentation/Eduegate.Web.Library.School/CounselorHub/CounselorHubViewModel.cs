using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.CounselorHub;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Exams;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.School.CounselorHub
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CounselorHub", "CRUDModel.ViewModel")]
    [DisplayName("Counselor")]
    public class CounselorHubViewModel : BaseMasterViewModel
    {
        public CounselorHubViewModel()
        {
            IsSendPushNotification = true;
            Section = new List<KeyValueViewModel>();
            Class = new List<KeyValueViewModel>();
            Student = new List<KeyValueViewModel>();
            CounselorHubAttachments = new List<CounselorHubAttachmentMapViewModel>() { new CounselorHubAttachmentMapViewModel() };
            StudentList = new List<CounselorHubStudentListViewModel>() { new CounselorHubStudentListViewModel() };
            IsActive = true;
        }

        public long CounselorHubIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        [DataMember]
        public string CounselorHubEntryDateString { get; set; }
        public DateTime? CounselorHubEntryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay(" ExpiryDate")]
        [DataMember]
        public string CounselorHubExpiryDateString { get; set; }

        public DateTime? CounselorHubExpiryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

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
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("Message")]
        public string Message { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CounselorStatuses")]
        [CustomDisplay("Status")]
        public string CounselorHubStatus { get; set; }
        public byte? CounselorHubStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendPushNotification")]
        public bool? IsSendPushNotification { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AllClass", "Numeric", true, "ClassSectionChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.AllClass")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AllSection", "Numeric", true, "ClassSectionChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.AllSection")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }
        public int? SectionID { get; set; }




        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='ClassSectionChanges($event, $element, CRUDModel.ViewModel)' ")]
        //[CustomDisplay("Fill")]
        //public bool Fill { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine7 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", true, "StudentChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.AllStudents")]
        [CustomDisplay("Student")]
        public List<KeyValueViewModel> Student { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("StudentList")]
        public List<CounselorHubStudentListViewModel> StudentList { get; set; }

        public bool IsSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Attachment")]
        public List<CounselorHubAttachmentMapViewModel> CounselorHubAttachments { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CounselorHubDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CounselorHubViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CounselorHubDTO, CounselorHubViewModel>.CreateMap();
            Mapper<CounselorHubMapDTO, CounselorHubStudentListViewModel>.CreateMap();


            var counsDto = dto as CounselorHubDTO;
            var vm = Mapper<CounselorHubDTO, CounselorHubViewModel>.Map(dto as CounselorHubDTO);
            //var mapp = Mapper<CounselorHubMapDTO, CounselorHubMapDTO>.Map(dto as CounselorHubMapDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.CounselorHubIID = counsDto.CounselorHubIID;
            vm.Message = counsDto.Message;
            vm.Title = counsDto.Title;
            vm.ShortTitle = counsDto.ShortTitle;
            vm.AcademicYear = counsDto.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = counsDto.AcademicYear.Key, Value = counsDto.AcademicYear.Value } : new KeyValueViewModel();
            vm.IsSendPushNotification = counsDto.IsSendNotification ?? false;
            vm.IsActive = counsDto.IsActive;
            vm.CounselorHubStatus = counsDto.CounselorHubStatusID.HasValue ? counsDto.CounselorHubStatusID.ToString() : null;
            vm.CounselorHubEntryDateString = counsDto.CounselorHubEntryDate.HasValue ? counsDto.CounselorHubEntryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.CounselorHubExpiryDateString = counsDto.CounselorHubExpiryDate.HasValue ? counsDto.CounselorHubExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.IsSelected = true;

            foreach (var map in counsDto.CounselorHubMaps)
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

                if (map.StudentID.HasValue &&
              !vm.Student.Any(x => x.Key == map.StudentID.Value.ToString()))
                {
                    vm.Student.Add(new KeyValueViewModel()
                    {
                        Key = map.Student.Key,
                        Value = map.Student.Value
                    });
                }

                if (map.AllStudent == true)
                {
                    vm.Student.Add(new KeyValueViewModel()
                    {
                        Key = null,
                        Value = "All Students"
                    });
                }
            }

            vm.CounselorHubAttachments = new List<CounselorHubAttachmentMapViewModel>();
            if (counsDto.CounselorHubAttachments != null)
            {
                foreach (var attach in counsDto.CounselorHubAttachments)
                {
                    if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                    {
                        vm.CounselorHubAttachments.Add(new CounselorHubAttachmentMapViewModel
                        {
                            //CounselorHubAttachmentMapIID = attach.CounselorHubAttachmentMapIID,
                            CounselorHubID = attach.CounselorHubID,
                            ContentFileName = attach.AttachmentName,
                            ContentFileIID = attach.AttachmentReferenceID,
                            AttachmentDescription = attach.AttachmentDescription,
                            StudentID = attach.StudentID,
                        });
                    }
                }
            }


            vm.StudentList = new List<CounselorHubStudentListViewModel>();
            foreach (var studMap in counsDto.CounselorHubMaps)
            {
                if (studMap.StudentID.HasValue)
                {
                    vm.StudentList.Add(new CounselorHubStudentListViewModel()
                    {
                        CounselorHubMapIID = studMap.CounselorHubMapIID,
                        StudentID = studMap.StudentID,
                        StudentName = studMap.StudentID.HasValue ? studMap.Student.Value : null,
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CounselorHubViewModel, CounselorHubDTO>.CreateMap();

            var dto = Mapper<CounselorHubViewModel, CounselorHubDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.ShortTitle = this.ShortTitle;
            dto.Message = this.Message;
            dto.Title = this.Title;
            dto.CounselorHubStatusID = string.IsNullOrEmpty(this.CounselorHubStatus) ? (byte?)null : byte.Parse(this.CounselorHubStatus);
            dto.CounselorHubEntryDate = string.IsNullOrEmpty(this.CounselorHubEntryDateString) ? (DateTime?)null : DateTime.ParseExact(this.CounselorHubEntryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.CounselorHubExpiryDate = string.IsNullOrEmpty(this.CounselorHubExpiryDateString) ?(DateTime?)null : DateTime.ParseExact(this.CounselorHubExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.IsSendNotification = this.IsSendPushNotification;
            dto.IsSelected = this.IsSelected;
            dto.IsActive = this.IsActive;

            dto.CounselorHubMaps = new List<CounselorHubMapDTO>();
            if (this.Class != null && this.Class.Count > 0)
            {
                if (this.Class.Any(x => x.Value.Contains("All Classes")))
                {
                    var cls = this.Class.FirstOrDefault(x => x.Value == "All Classes");

                    dto.CounselorHubMaps.Add(new CounselorHubMapDTO()
                    {
                        CounselorHubID = this.CounselorHubIID,
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
                            dto.CounselorHubMaps.Add(new CounselorHubMapDTO()
                            {
                                CounselorHubID = this.CounselorHubIID,
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

                    dto.CounselorHubMaps.Add(new CounselorHubMapDTO()
                    {
                        CounselorHubID = this.CounselorHubIID,
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
                            dto.CounselorHubMaps.Add(new CounselorHubMapDTO()
                            {
                                CounselorHubID = this.CounselorHubIID,
                                SectionID = int.Parse(sec.Key),
                                Section = new KeyValueDTO() { Key = sec.Key, Value = sec.Value }
                            });
                        }
                    }
                }
            }
            if (this.Student != null && this.Student.Count > 0)
            {
                if (this.Student.Any(x => x.Value.Contains("All Students")))
                {
                    var sec = this.Student.FirstOrDefault(x => x.Value == "All Students");

                    dto.CounselorHubMaps.Add(new CounselorHubMapDTO()
                    {   CounselorHubID = this.CounselorHubIID,
                        AllStudent = (sec.Value == null || sec.Value == "") &&
                        sec.Value != "All Students"
                            ? false : true,
                        Student = new KeyValueDTO() { Key = null, Value = "All Students" },
                    });
                }
                else
                {
                    foreach (var sec in this.Student)
                    {
                        if (!string.IsNullOrEmpty(sec.Key))
                        {
                            dto.CounselorHubMaps.Add(new CounselorHubMapDTO()
                            {   CounselorHubID = this.CounselorHubIID,
                                StudentID = int.Parse(sec.Key),
                                Student = new KeyValueDTO() { Key = sec.Key, Value = sec.Value }
                            });
                        }
                    }
                }
            }
            //foreach (var studList in this.StudentList)
            //{
            //    if (studList.StudentID.HasValue)
            //    {
            //        dto.CounselorHubMaps.Add(new CounselorHubMapDTO
            //        {
            //            CounselorHubMapIID = studList.CounselorHubMapIID,
            //            StudentID = studList.StudentID,
            //            StudentName = studList.StudentName
            //        });
            //    }
            //}
            dto.CounselorHubAttachments = new List<CounselorHubAttachmentMapDTO>();

            if (this.CounselorHubAttachments.Count > 0)
            {
                foreach (var attachment in this.CounselorHubAttachments)
                {
                    if (attachment.ContentFileIID.HasValue || !string.IsNullOrEmpty(attachment.ContentFileName))
                    {
                        dto.CounselorHubAttachments.Add(new CounselorHubAttachmentMapDTO
                        {
                            //CounselorHubAttachmentMapIID = attachment.CounselorHubAttachmentMapIID,
                            CounselorHubID = attachment.CounselorHubID,
                            AttachmentReferenceID = attachment.ContentFileIID,
                            AttachmentName = attachment.ContentFileName,
                            AttachmentDescription = attachment.AttachmentDescription,
                            StudentID = attachment.StudentID,
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
            return JsonConvert.DeserializeObject<CounselorHubDTO>(jsonString);
        }

    }
}