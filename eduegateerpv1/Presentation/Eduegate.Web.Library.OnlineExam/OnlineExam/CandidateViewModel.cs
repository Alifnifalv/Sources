using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.School.Students;
using Eduegate.Web.Library.Common;
using System;
using System.Globalization;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class CandidateViewModel : BaseMasterViewModel
    {
        public CandidateViewModel()
        {
            CandidateExamDetails = new List<CandidateExamDetailViewModel>() { new CandidateExamDetailViewModel() };
            Student = new List<KeyValueViewModel>();
            ExceptStudentList = new List<KeyValueViewModel>();
            IsNewCandidate = true;
        }

        public long CandidateIID { get; set; }

        public short? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? StudentApplicationID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, attribs: "ng-change='NewCandidateClicks($event, $element,CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.CandidateIID != 0'")]
        [CustomDisplay("IsNewCandidate")]
        public bool? IsNewCandidate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker, attribs: "ng-disabled='CRUDModel.ViewModel.CandidateIID!=0'")]
        [CustomDisplay("FromStudentApplication")]
        [DataPicker("CandidateStudentApplicationAdvancedSearch")]
        public string ReferenceStudentApplicationNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Application No.")]
        public string ApplicationNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "ClassChanges($event, $element, CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled=CRUDModel.ViewModel.IsNewCandidate")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, attribs: "ng-change='AllStudentsCheckBox($event, $element,CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.CandidateIID != 0'")]
        [CustomDisplay("AllStudents")]
        public bool? IsAllStudents { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", true, OptionalAttribute1 = "ng-disabled=CRUDModel.ViewModel.IsNewCandidate")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public List<KeyValueViewModel> Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", true, optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.IsNewCandidate")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.ExceptStudentList")]
        [LookUp("LookUps.Students")]
        [CustomDisplay("ExceptStudents")]
        public List<KeyValueViewModel> ExceptStudentList { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.StudentClass.Key")]
        [CustomDisplay("CandidateName")]
        public string CandidateName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.IsAllStudents || CRUDModel.ViewModel.Student.length > 0 || CRUDModel.ViewModel.ExceptStudentList.length > 0'")]
        [CustomDisplay("CandidateNationalID")]
        public string NationalID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.IsAllStudents || CRUDModel.ViewModel.Student.length > 0 || CRUDModel.ViewModel.ExceptStudentList.length > 0'")]
        [CustomDisplay("Telephone")]
        public string Telephone { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.IsAllStudents || CRUDModel.ViewModel.Student.length > 0 || CRUDModel.ViewModel.ExceptStudentList.length > 0'")]
        [CustomDisplay("MobileNumber")]
        public string MobileNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.IsAllStudents || CRUDModel.ViewModel.Student.length > 0 || CRUDModel.ViewModel.ExceptStudentList.length > 0'")]
        [CustomDisplay("EmailID")]
        public string Email { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.IsAllStudents || CRUDModel.ViewModel.Student.length > 0 || CRUDModel.ViewModel.ExceptStudentList.length > 0'")]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("LoginDetails")]
        public string LoginDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.IsAllStudents || CRUDModel.ViewModel.Student.length > 0 || CRUDModel.ViewModel.ExceptStudentList.length > 0'")]
        [CustomDisplay("UserName")]
        public string UserName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Password")]
        public string Password { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CandidateStatus")]
        [CustomDisplay("Candidate Status")]
        public string CandidateStatus { get; set; }
        public byte? CandidateStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ExamDetails")]
        public List<CandidateExamDetailViewModel> CandidateExamDetails { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CandidateDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CandidateViewModel>(jsonString);
        }

        public CandidateViewModel FromStudentApplicationVM(StudentApplicationViewModel applicationVM)
        {
            var vm = new CandidateViewModel()
            {
                StudentApplicationID = applicationVM.ApplicationIID,
                ApplicationNo = applicationVM.ApplicationNumber,
                StudentClass = applicationVM.ClassID.HasValue ? new KeyValueViewModel() { Key = applicationVM.ClassID.ToString(), Value = applicationVM.StudentClass.Value } : new KeyValueViewModel(),
                MobileNumber = applicationVM.FatherMotherDetails.MobileNumber != null ? applicationVM.FatherMotherDetails.MobileNumber : null,
                NationalID = applicationVM.StudentNationalID,
                SchoolID = (short?)applicationVM.SchoolID,
                AcademicYearID = applicationVM.AcademicyearID,
                Email = applicationVM.FatherMotherDetails.EmailID != null ? applicationVM.FatherMotherDetails.EmailID : null,
                CandidateName = applicationVM.FirstName + ' ' + applicationVM.MiddleName + ' ' + applicationVM.MiddleName + ' ' + applicationVM.LastName,
                UserName = applicationVM.ApplicationNumber,
            };
            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CandidateDTO, CandidateViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var candidateDTO = dto as CandidateDTO;
            var vm = Mapper<CandidateDTO, CandidateViewModel>.Map(candidateDTO);

            var dateTimeFormatWithSec = new Domain.Setting.SettingBL().GetSettingValue<string>("24HrDateTimeFormatWithMilliSecond");

            vm.StudentApplicationID = candidateDTO.StudentApplicationID;
            vm.StudentClass = candidateDTO.ClassID.HasValue ? new KeyValueViewModel()
            {
                Key = candidateDTO.ClassID.ToString(),
                Value = candidateDTO.ClassName
            } : new KeyValueViewModel();
            vm.IsNewCandidate = candidateDTO.IsNewCandidate;
            vm.IsAllStudents = candidateDTO.IsAllStudents;
            vm.CandidateStatus = candidateDTO.CandidateStatusID.HasValue ? candidateDTO.CandidateStatusID.ToString() : null;

            vm.CandidateExamDetails = new List<CandidateExamDetailViewModel>();
            foreach (var examMap in candidateDTO.CandidateOnlineExamMaps)
            {
                vm.CandidateExamDetails.Add(new CandidateExamDetailViewModel()
                {
                    CandidateOnlinExamMapIID = (long)examMap.CandidateOnlinExamMapIID,
                    OnlineExam = examMap.OnlineExamID.HasValue ? new KeyValueViewModel()
                    {
                        Key = examMap.OnlineExamID.ToString(),
                        Value = examMap.OnlineExamName
                    } : new KeyValueViewModel(),
                    Duration = examMap.Duration,
                    AdditionalTime = examMap.AdditionalTime,
                    OnlineExamStatus = examMap.OnlineExamStatusID.HasValue ? examMap.OnlineExamStatusID.ToString() : null,
                    OnlineExamOperationStatus = examMap.OnlineExamOperationStatusID.HasValue ? examMap.OnlineExamOperationStatusID.ToString() : null,
                    StartTimeString = examMap.ExamStartTime.HasValue ? examMap.ExamStartTime.Value.ToString(dateTimeFormatWithSec, CultureInfo.InvariantCulture) : null,
                    EndTimeString = examMap.ExamEndTime.HasValue ? examMap.ExamEndTime.Value.ToString(dateTimeFormatWithSec, CultureInfo.InvariantCulture) : null,
                    OldAdditionalTime = examMap.AdditionalTime,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CandidateViewModel, CandidateDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<CandidateViewModel, CandidateDTO>.Map(this);

            var dateTimeFormatWithSec = new Domain.Setting.SettingBL().GetSettingValue<string>("24HrDateTimeFormatWithMilliSecond");

            dto.StudentApplicationID = this.StudentApplicationID;
            dto.ClassID = this.StudentClass == null || string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.IsNewCandidate = this.IsNewCandidate;
            dto.CandidateStatusID = string.IsNullOrEmpty(this.CandidateStatus) ? (byte?)null : byte.Parse(this.CandidateStatus);

            List<KeyValueDTO> expectstudentList = new List<KeyValueDTO>();
            List<KeyValueDTO> studentList = new List<KeyValueDTO>();
            if (this.Student != null)
            {
                foreach (KeyValueViewModel vm in this.Student)
                {
                    studentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.Student = studentList;
            if (this.ExceptStudentList != null)
            {
                foreach (KeyValueViewModel vm in this.ExceptStudentList)
                {
                    expectstudentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.ExceptStudentList = expectstudentList;
            dto.IsAllStudents = this.IsAllStudents;

            foreach (var examDetail in this.CandidateExamDetails)
            {
                if (examDetail.OnlineExam != null)
                {
                    if (!string.IsNullOrEmpty(examDetail.OnlineExam.Key))
                    {
                        dto.CandidateOnlineExamMaps.Add(new CandidateOnlineExamMapDTO()
                        {
                            CandidateOnlinExamMapIID = examDetail.CandidateOnlinExamMapIID,
                            OnlineExamID = string.IsNullOrEmpty(examDetail.OnlineExam.Key) ? (long?)null : long.Parse(examDetail.OnlineExam.Key),
                            Duration = examDetail.Duration,
                            AdditionalTime = examDetail.AdditionalTime,
                            OnlineExamStatusID = examDetail.OnlineExamStatus != null ? byte.Parse(examDetail.OnlineExamStatus) : (byte?)null,
                            OnlineExamOperationStatusID = examDetail.OnlineExamOperationStatus != null ? byte.Parse(examDetail.OnlineExamOperationStatus) : (byte?)null,
                            ExamStartTime = string.IsNullOrEmpty(examDetail.StartTimeString) ? (DateTime?)null : DateTime.ParseExact(examDetail.StartTimeString, dateTimeFormatWithSec, CultureInfo.InvariantCulture),
                            ExamEndTime = string.IsNullOrEmpty(examDetail.EndTimeString) ? (DateTime?)null : DateTime.ParseExact(examDetail.EndTimeString, dateTimeFormatWithSec, CultureInfo.InvariantCulture),
                            OldAdditionalTime = examDetail.OldAdditionalTime,
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CandidateDTO>(jsonString);
        }

    }
}