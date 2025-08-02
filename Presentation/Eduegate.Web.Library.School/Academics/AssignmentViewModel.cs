using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Assignment", "CRUDModel.ViewModel")]
    [DisplayName("Assignment")]
    public class AssignmentViewModel : BaseMasterViewModel
    {
        public AssignmentViewModel()
        {
            //Class = new KeyValueViewModel();
            //Section = new KeyValueViewModel();
            //Subject = new KeyValueViewModel();
            //Academic = new KeyValueViewModel();
            //AssignmentType = new KeyValueViewModel();
            //AssignmentStatus = new KeyValueViewModel();
            IsActive = true;
            Attachments = new List<AssignmentAttachmentViewModel>() { new AssignmentAttachmentViewModel() };
        }

        public int AssignmentIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AssignmentType", "Numeric", false, "")]
        [LookUp("LookUps.AssignmentType")]
        [CustomDisplay("AssignmentType")]
        public KeyValueViewModel AssignmentType { get; set; }
        public int? AssignmentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "ClassChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", true, "")]
        [CustomDisplay("Section")]
        [LookUp("LookUps.Section")]
        //public KeyValueViewModel Section { get; set; }
        //public int? SectionID { get; set; }
        public List<KeyValueViewModel> Sections { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [CustomDisplay("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateOfSubmission")]
        public string SubmissionDateString { get; set; }
        public System.DateTime? SubmissionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("StartDate")]
        public string StartDateString { get; set; }
        public System.DateTime? StartDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FreezeDate")]
        public string FreezeDateString { get; set; }
        public System.DateTime? FreezeDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AssignmentStatus", "Numeric", false, "")]
        [CustomDisplay("AssignmentStatus")]
        [LookUp("LookUps.AssignmentStatus")]
        public KeyValueViewModel AssignmentStatus { get; set; }

        public int? AssignmentStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }


        [Required]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        public byte? OldAssignmentStatusID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Attachment")]
        public List<AssignmentAttachmentViewModel> Attachments { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssignmentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignmentViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AssignmentDTO, AssignmentViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            //Mapper<AssignmentAttachmentMapDTO, AssignmentAttachmentViewModel>.CreateMap();
            var assignDto = dto as AssignmentDTO;
            var vm = Mapper<AssignmentDTO, AssignmentViewModel>.Map(dto as AssignmentDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.Title = assignDto.Title;
            vm.IsActive = assignDto.IsActive;
            vm.Description = assignDto.Description;
            vm.ClassID = assignDto.ClassID;
            vm.SubjectID = assignDto.SubjectID;
            //vm.SectionID = assignDto.SectionID;
            vm.AcademicYearID = assignDto.AcademicYearID;
            vm.AssignmentTypeID = assignDto.AssignmentTypeID;
            vm.AssignmentStatusID = assignDto.AssignmentStatusID;
            vm.OldAssignmentStatusID = assignDto.OldAssignmentStatusID;
            vm.StudentClass = assignDto.ClassID.HasValue ? new KeyValueViewModel() { Key = assignDto.Class.Key, Value = assignDto.Class.Value } : new KeyValueViewModel();
            vm.Subject = assignDto.SubjectID.HasValue ? new KeyValueViewModel() { Key = assignDto.Subject.Key, Value = assignDto.Subject.Value } : new KeyValueViewModel();
            //vm.Section = assignDto.SectionID.HasValue ? new KeyValueViewModel() { Key = assignDto.Section.Key.ToString(), Value = assignDto.Section.Value } : new KeyValueViewModel();
            vm.Academic = assignDto.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = assignDto.AcademicYear.Key.ToString(), Value = assignDto.AcademicYear.Value } : new KeyValueViewModel();
            vm.AssignmentType = assignDto.AssignmentTypeID.HasValue ? new KeyValueViewModel() { Key = assignDto.AssignmentType.Key, Value = assignDto.AssignmentType.Value } : new KeyValueViewModel();
            vm.AssignmentStatus = assignDto.AssignmentStatusID.HasValue ? new KeyValueViewModel() { Key = assignDto.AssignmentStatus.Key, Value = assignDto.AssignmentStatus.Value } : new KeyValueViewModel();
            vm.StartDateString =  assignDto.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) ;
            vm.FreezeDateString = assignDto.FreezeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) ;
            vm.SubmissionDateString = assignDto.DateOfSubmission.Value.ToString(dateFormat, CultureInfo.InvariantCulture);

            vm.Sections = new List<KeyValueViewModel>();

            foreach (var sec in assignDto.SectionList)
            {
                vm.Sections.Add(new KeyValueViewModel()
                {
                    Key = sec.Key,
                    Value = sec.Value
                });
            }

            vm.Attachments = new List<AssignmentAttachmentViewModel>();

            foreach (var attachmentMapDto in assignDto.AssignmentAttachmentMaps)
            {
                if (attachmentMapDto.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attachmentMapDto.AttachmentName))
                {
                    vm.Attachments.Add(new AssignmentAttachmentViewModel()
                    {
                        AssignmentAttachmentIID = attachmentMapDto.AssignmentAttachmentMapIID,
                        AssignmentID = attachmentMapDto.AssignmentID,
                        ContentFileIID = attachmentMapDto.AttachmentReferenceID,
                        ContentFileName = attachmentMapDto.AttachmentName,
                        Description = attachmentMapDto.AttachmentDescription,
                        Notes = attachmentMapDto.Notes,
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssignmentViewModel, AssignmentDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            // Mapper<AssignmentAttachmentViewModel, AssignmentAttachmentMapDTO>.CreateMap();
            var dto = Mapper<AssignmentViewModel, AssignmentDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.Title = this.Title;
            dto.IsActive = this.IsActive;
            dto.Description = this.Description;
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            //dto.SectionID = this.Section == null || string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FreezeDate = string.IsNullOrEmpty(this.FreezeDateString) ? (DateTime?)null : DateTime.ParseExact(this.FreezeDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.AssignmentTypeID = string.IsNullOrEmpty(this.AssignmentType.Key) ? (byte?)null : byte.Parse(this.AssignmentType.Key);
            dto.AssignmentStatusID = string.IsNullOrEmpty(this.AssignmentStatus.Key) ? (byte?)null : byte.Parse(this.AssignmentStatus.Key);
            dto.OldAssignmentStatusID = this.OldAssignmentStatusID.HasValue ? this.OldAssignmentStatusID : null;
            dto.DateOfSubmission = string.IsNullOrEmpty(this.SubmissionDateString) ? (DateTime?)null : DateTime.ParseExact(this.SubmissionDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.SectionList = new List<KeyValueDTO>();

            foreach (var sec in Sections)
            {
                dto.SectionList.Add(new KeyValueDTO()
                {
                    Key = sec.Key,
                    Value = sec.Value
                });
            }

            dto.AssignmentAttachmentMaps = new List<AssignmentAttachmentMapDTO>();
            
            foreach (var attachmentMap in this.Attachments)
            {
                if (attachmentMap.ContentFileIID.HasValue || !string.IsNullOrEmpty(attachmentMap.ContentFileName))
                {
                    dto.AssignmentAttachmentMaps.Add(new AssignmentAttachmentMapDTO()
                    {
                        AssignmentAttachmentMapIID = attachmentMap.AssignmentAttachmentIID,
                        AssignmentID = attachmentMap.AssignmentID,
                        AttachmentReferenceID = attachmentMap.ContentFileIID,
                        AttachmentName = attachmentMap.ContentFileName,
                        AttachmentDescription = attachmentMap.Description,
                        Notes = attachmentMap.Notes,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignmentDTO>(jsonString);
        }
    }
}

