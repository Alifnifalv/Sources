using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Supports;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System;

namespace Eduegate.Web.Library.Support.CustomerSupport
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Tickets", "CRUDModel.ViewModel")]
    [DisplayName("Ticket")]
    public class TicketingViewModel : BaseMasterViewModel
    {
        public TicketingViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            DueDateFromString = string.IsNullOrEmpty(dateFormat) ? null : DateTime.Now.Date.ToString(dateFormat, CultureInfo.InvariantCulture);
            Customer = new KeyValueViewModel();
            Parent = new KeyValueViewModel();
            Student = new KeyValueViewModel();
            AssignedEmployee = new KeyValueViewModel();
            SupportSubCategory = new KeyValueViewModel();
            IsAutoCreation = false;
            IsSendCustomerNotification = false;
            IsSendMailToAssignedEmployee = false;
            TicketCommunication = new TicketingCommunicationViewModel();
            TicketFeeDueMaps = new List<TicketingFeeDueMapViewModel> { new TicketingFeeDueMapViewModel() };
        }

        public long TicketIID { get; set; }

        public bool IsAutoCreation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", "ng-change='GetNextTransactionNumberByDocumentType(CRUDModel.ViewModel.DocumentType)'")]
        [CustomDisplay("Type")]
        [LookUp("LookUps.TicketDocumentTypes")]
        public string DocumentType { get; set; }
        public int? DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CaseNo")]
        public string TransactionNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine1 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Customer", "Numeric", false, "", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.TicketIID>0 || CRUDModel.ViewModel.Parent.Key || CRUDModel.ViewModel.Student.Key || CRUDModel.ViewModel.IsAutoCreation'")]
        //[LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR?defaultBlank=false", "LookUps.Customer")]
        //[CustomDisplay("Customer")]
        public KeyValueViewModel Customer { get; set; }
        public long? CustomerID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Parent", "Numeric", false, "ParentChanges(CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.TicketIID>0 || CRUDModel.ViewModel.Customer.Key || CRUDModel.ViewModel.IsAutoCreation'")]
        [LookUp("LookUps.Parent")]
        [CustomDisplay("Parent")]
        public KeyValueViewModel Parent { get; set; }
        public long? ParentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", false, "StudentChanges(CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.TicketIID>0 || CRUDModel.ViewModel.Customer.Key || CRUDModel.ViewModel.IsAutoCreation'")]
        [LookUp("LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='TicketTypeChanges(CRUDModel.ViewModel)'")]
        [CustomDisplay("TicketType")]
        [LookUp("LookUps.TicketTypes")]
        public string TicketType { get; set; }
        public int? TicketTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='FacultyTypeChanges(CRUDModel.ViewModel)'")]
        [CustomDisplay("FacultyType")]
        [LookUp("LookUps.FacultyTypes")]
        public string FacultyType { get; set; }
        public int? FacultyTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Department")]
        [LookUp("LookUps.TicketDepartments")]
        public string Department { get; set; }
        public long? DepartmentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, attribs: "ng-click=\"FillEmployees(CRUDModel.ViewModel)\"")]
        [CustomDisplay("FillEmployees")]
        public string FillEmployeeButton { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("AssignedEmployee")]
        [Select2("Manager", "Numeric", false)]
        [LookUp("LookUps.SupportAssignedEmployees")]
        public KeyValueViewModel AssignedEmployee { get; set; }
        public long? AssignedEmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine6 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SupportCategory", "Numeric", false, "SupportCategoryChanges(CRUDModel.ViewModel)")]
        [LookUp("LookUps.SupportCategories")]
        [CustomDisplay("SupportCategory")]
        public KeyValueViewModel SupportCategory { get; set; }
        public int? SupportCategoryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SupportSubCategory", "Numeric", false)]
        [LookUp("LookUps.SupportSubCategories")]
        [CustomDisplay("SupportSubCategory")]
        public KeyValueViewModel SupportSubCategory { get; set; }
        public int? SupportSubCategoryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine7 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("SendNotificationToAssignedEmployee")]
        public bool? IsSendMailToAssignedEmployee { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("SendNotificationToParent")]
        public bool? IsSendCustomerNotification { get; set; }
        public bool? OldIsSendCustomerNotification { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("IsNotifiedToParent")]
        public string NotifiedToParent { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine8 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Subject")]
        [MaxLength(100)]
        public string Subject { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine9 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("Problem")]
        public string ProblemDescription { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine10 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("ActionNeeded")]
        public string ActionNeededDescription { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine11 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("TicketDate")]
        public string DueDateFromString { get; set; }
        public DateTime? DueDateFrom { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("TicketResolutionDate")]
        public string DueDateToString { get; set; }
        public DateTime? DueDateTo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine12 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Priority")]
        [LookUp("LookUps.TicketPriorities")]
        public string Priority { get; set; }
        public byte? PriorityID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [Select2("Status", "Numeric", false)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.TicketStatuses")]
        public string Status { get; set; }
        public byte? TicketStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine13 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", "ng-change='TicketReferenceTypeChanges(CRUDModel.ViewModel)'")]
        [CustomDisplay("ReferenceType")]
        [LookUp("LookUps.TicketReferenceTypes")]
        public string ReferenceType { get; set; }
        public byte? ReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("SupportAction")]
        [LookUp("LookUps.SupportActions")]
        public string Action { get; set; }
        public byte? ActionID { get; set; }

        public long? LoginID { get; set; }
        public string CustomerEmailID { get; set; }
        public long? ReferenceID { get; set; }

        public long? OldAssignedEmployeeID { get; set; }
        public string AssignedEmployeeEmailID { get; set; }

        public byte? StudentSchoolID { get; set; }
        public int? StudentAcademicYearID { get; set; }
        public int? StudentClassID { get; set; }
        public int? StudentSectionID { get; set; }

        public bool? IsAcademicTicketType { get; set; }
        public bool? IsNonAcademicTicketType { get; set; }
        public bool? IsGeneralTicketType { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "TicketCommunications", "TicketCommunications")]
        [CustomDisplay("Communications")]
        public TicketingCommunicationViewModel TicketCommunication { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeDueDetails")]
        public List<TicketingFeeDueMapViewModel> TicketFeeDueMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TicketDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketingViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TicketDTO, TicketingViewModel>.CreateMap();
            var tkDTO = dto as TicketDTO;
            var vm = Mapper<TicketDTO, TicketingViewModel>.Map(tkDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.TicketIID = tkDTO.TicketIID;
            vm.DocumentType = tkDTO.DocumentTypeID.HasValue ? tkDTO.DocumentTypeID.ToString() : null;
            vm.TransactionNo = tkDTO.TicketNo;
            vm.Customer = tkDTO.CustomerID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.CustomerID.ToString(),
                Value = tkDTO.CustomerName
            } : new KeyValueViewModel();
            vm.Parent = tkDTO.ParentID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.ParentID.ToString(),
                Value = tkDTO.ParentName
            } : new KeyValueViewModel();
            vm.Subject = tkDTO.Subject;
            vm.ProblemDescription = tkDTO.Description;
            vm.ActionNeededDescription = tkDTO.Description2;
            vm.AssignedEmployee = tkDTO.AssignedEmployeeID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.AssignedEmployeeID.ToString(),
                Value = tkDTO.AssignedEmployeeName
            } : new KeyValueViewModel();
            vm.OldAssignedEmployeeID = tkDTO.AssignedEmployeeID;
            vm.Status = tkDTO.TicketStatusID.HasValue ? tkDTO.TicketStatusID.ToString() : null;
            vm.DueDateFromString = tkDTO.DueDateFrom.HasValue ? tkDTO.DueDateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.DueDateToString = tkDTO.DueDateTo.HasValue ? tkDTO.DueDateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Priority = tkDTO.PriorityID.HasValue ? tkDTO.PriorityID.ToString() : null;
            vm.Action = tkDTO.ActionID.HasValue ? tkDTO.ActionID.ToString() : null;
            vm.ReferenceType = tkDTO.ReferenceTypeID.HasValue ? tkDTO.ReferenceTypeID.ToString() : null;
            vm.Department = tkDTO.DepartmentID.HasValue ? tkDTO.DepartmentID.ToString() : null;
            vm.IsSendMailToAssignedEmployee = tkDTO.IsSendMailToAssignedEmployee ?? false;
            vm.AssignedEmployeeEmailID = tkDTO.AssignedEmployeeEmailID;
            vm.TicketType = tkDTO.TicketTypeID.HasValue ? tkDTO.TicketTypeID.ToString() : null;
            vm.SupportCategory = tkDTO.SupportCategoryID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.SupportCategoryID.ToString(),
                Value = tkDTO.SupportCategoryName
            } : new KeyValueViewModel();
            vm.SupportSubCategory = tkDTO.SupportSubCategoryID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.SupportSubCategoryID.ToString(),
                Value = tkDTO.SupportSubCategoryName
            } : new KeyValueViewModel();
            vm.FacultyType = tkDTO.FacultyTypeID.HasValue ? tkDTO.FacultyTypeID.ToString() : null;
            vm.Student = tkDTO.StudentID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.StudentID.ToString(),
                Value = tkDTO.StudentName
            } : new KeyValueViewModel();
            vm.StudentSchoolID = tkDTO.StudentSchoolID;
            vm.StudentAcademicYearID = tkDTO.StudentAcademicYearID;
            vm.StudentClassID = tkDTO.StudentClassID;
            vm.StudentSectionID = tkDTO.StudentSectionID;

            vm.IsSendCustomerNotification = false;
            vm.OldIsSendCustomerNotification = tkDTO.OldIsSendCustomerNotification;
            vm.NotifiedToParent = tkDTO.OldIsSendCustomerNotification == true ? "Notified" : "Not Yet Notified";

            vm.CreatedBy = dto.CreatedBy;
            vm.CreatedDate = dto.CreatedDate;
            vm.UpdatedBy = dto.UpdatedBy;
            vm.UpdatedDate = dto.UpdatedDate;

            vm.TicketCommunication = new TicketingCommunicationViewModel();

            vm.TicketCommunication.CommunicationGrid = new List<TicketingCommunicationGridViewModel>();
            foreach (var communication in tkDTO.TicketCommunications)
            {
                vm.TicketCommunication.CommunicationGrid.Add(new TicketingCommunicationGridViewModel()
                {
                    TicketCommunicationIID = communication.TicketCommunicationIID,
                    TicketID = communication.TicketID,
                    LoginID = communication.LoginID,
                    LoginUserID = communication.LoginUserID,
                    Notes = communication.Notes,
                    CommunicationDateString = communication.CommunicationDate.HasValue ? communication.CommunicationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    FollowUpDateString = communication.FollowUpDate.HasValue ? communication.FollowUpDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    CreatedBy = communication.CreatedBy,
                    CreatedDate = communication.CreatedDate,
                    UpdatedBy = communication.UpdatedBy,
                    UpdatedDate = communication.UpdatedDate,
                });
            }

            vm.TicketFeeDueMaps = new List<TicketingFeeDueMapViewModel>();
            if (tkDTO.ReferenceTypeID == (byte?)TicketReferenceTypes.FeeDue)
            {
                foreach (var feeDueMap in tkDTO.TicketFeeDueMapDTOs)
                {
                    if (feeDueMap.StudentFeeDueID != null)
                    {
                        vm.TicketFeeDueMaps.Add(new TicketingFeeDueMapViewModel()
                        {
                            TicketFeeDueMapIID = feeDueMap.TicketFeeDueMapIID,
                            TicketID = feeDueMap.TicketID,
                            StudentFeeDueID = feeDueMap.StudentFeeDueID,
                            DueAmount = feeDueMap.DueAmount,
                            InvoiceNo = feeDueMap.InvoiceNo,
                            InvoiceDateString = feeDueMap.InvoiceDate.HasValue ? feeDueMap.InvoiceDate.Value.ToString(dateFormat) : null,
                            FeeMasterID = feeDueMap.FeeMasterID,
                            FeeMaster = feeDueMap.FeeMaster,
                            CreatedBy = feeDueMap.CreatedBy,
                            CreatedDate = feeDueMap.CreatedDate,
                            UpdatedBy = feeDueMap.UpdatedBy,
                            UpdatedDate = feeDueMap.UpdatedDate,
                        });
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TicketingViewModel, TicketDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<TicketingViewModel, TicketDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.TicketIID = this.TicketIID;
            dto.DocumentTypeID = string.IsNullOrEmpty(this.DocumentType) ? (int?)null : int.Parse(this.DocumentType);
            dto.TicketNo = this.TransactionNo;
            dto.CustomerID = string.IsNullOrEmpty(this.Customer.Key) ? (long?)null : long.Parse(this.Customer.Key);
            dto.ParentID = string.IsNullOrEmpty(this.Parent.Key) ? (long?)null : long.Parse(this.Parent.Key);
            dto.Subject = this.Subject;
            dto.Description = this.ProblemDescription;
            dto.Description2 = this.ActionNeededDescription;
            dto.AssignedEmployeeID = this.AssignedEmployee == null || string.IsNullOrEmpty(this.AssignedEmployee.Key) ? (long?)null : long.Parse(this.AssignedEmployee.Key);
            dto.TicketStatusID = string.IsNullOrEmpty(this.Status) ? (byte?)null : byte.Parse(this.Status);
            dto.IsSendCustomerNotification = this.IsSendCustomerNotification;
            dto.OldIsSendCustomerNotification = this.OldIsSendCustomerNotification != true ? this.IsSendCustomerNotification ?? false : this.OldIsSendCustomerNotification ?? false;
            dto.DueDateFrom = string.IsNullOrEmpty(this.DueDateFromString) ? (DateTime?)null : DateTime.ParseExact(this.DueDateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DueDateTo = string.IsNullOrEmpty(this.DueDateToString) ? (DateTime?)null : DateTime.ParseExact(this.DueDateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.PriorityID = string.IsNullOrEmpty(this.Priority) ? (byte?)null : byte.Parse(this.Priority);
            dto.ActionID = string.IsNullOrEmpty(this.Action) ? (byte?)null : byte.Parse(this.Action);
            dto.ReferenceTypeID = string.IsNullOrEmpty(this.ReferenceType) ? (byte?)null : byte.Parse(this.ReferenceType);
            dto.IsSendMailToAssignedEmployee = this.IsSendMailToAssignedEmployee ?? false;
            dto.AssignedEmployeeEmailID = this.AssignedEmployeeEmailID;
            dto.TicketTypeID = string.IsNullOrEmpty(this.TicketType) ? (int?)null : int.Parse(this.TicketType);
            dto.SupportCategoryID = string.IsNullOrEmpty(this.SupportCategory.Key) ? (int?)null : int.Parse(this.SupportCategory.Key);
            dto.SupportSubCategoryID = string.IsNullOrEmpty(this.SupportSubCategory.Key) ? (int?)null : int.Parse(this.SupportSubCategory.Key);
            dto.FacultyTypeID = string.IsNullOrEmpty(this.FacultyType) ? (int?)null : int.Parse(this.FacultyType);
            dto.DepartmentID = string.IsNullOrEmpty(this.Department) ? (long?)null : int.Parse(this.Department);
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (int?)null : int.Parse(this.Student.Key);

            dto.StudentSchoolID = this.StudentSchoolID;
            dto.StudentAcademicYearID = this.StudentAcademicYearID;
            dto.StudentClassID = this.StudentClassID;
            dto.StudentSectionID = this.StudentSectionID;

            dto.CreatedBy = this.CreatedBy;
            dto.CreatedDate = this.CreatedDate;
            dto.UpdatedBy = this.UpdatedBy;
            dto.UpdatedDate = this.UpdatedDate;

            dto.TicketCommunications = new List<TicketCommunicationDTO>();
            foreach (var communication in this.TicketCommunication.CommunicationGrid)
            {
                if (!string.IsNullOrEmpty(communication.Notes))
                {
                    dto.TicketCommunications.Add(new TicketCommunicationDTO()
                    {
                        TicketCommunicationIID = communication.TicketCommunicationIID,
                        TicketID = communication.TicketID,
                        LoginID = communication.LoginID,
                        LoginUserID = communication.LoginUserID,
                        Notes = communication.Notes,
                        CommunicationDate = string.IsNullOrEmpty(communication.CommunicationDateString) ? (DateTime?)null : DateTime.ParseExact(communication.CommunicationDateString, dateFormat, CultureInfo.InvariantCulture),
                        FollowUpDate = string.IsNullOrEmpty(communication.FollowUpDateString) ? (DateTime?)null : DateTime.ParseExact(communication.FollowUpDateString, dateFormat, CultureInfo.InvariantCulture),
                        CreatedBy = communication.CreatedBy,
                        CreatedDate = communication.CreatedDate,
                        UpdatedBy = communication.UpdatedBy,
                        UpdatedDate = communication.UpdatedDate,
                    });
                }
            }

            if (!string.IsNullOrEmpty(this.TicketCommunication.Notes))
            {
                dto.TicketCommunications.Add(new TicketCommunicationDTO()
                {
                    TicketCommunicationIID = this.TicketCommunication.TicketCommunicationIID,
                    TicketID = this.TicketIID,
                    Notes = this.TicketCommunication.Notes,
                });
            }

            dto.TicketFeeDueMapDTOs = new List<TicketFeeDueMapDTO>();
            if (!string.IsNullOrEmpty(this.ReferenceType))
            {
                if (byte.Parse(this.ReferenceType) == (byte?)TicketReferenceTypes.FeeDue)
                {
                    foreach (var feeDueMap in this.TicketFeeDueMaps)
                    {
                        if (feeDueMap.StudentFeeDueID != null)
                        {
                            dto.TicketFeeDueMapDTOs.Add(new TicketFeeDueMapDTO()
                            {
                                TicketFeeDueMapIID = feeDueMap.TicketFeeDueMapIID,
                                TicketID = feeDueMap.TicketID,
                                StudentFeeDueID = feeDueMap.StudentFeeDueID,
                                DueAmount = feeDueMap.DueAmount,
                                CreatedBy = feeDueMap.CreatedBy,
                                CreatedDate = feeDueMap.CreatedDate,
                                UpdatedBy = feeDueMap.UpdatedBy,
                                UpdatedDate = feeDueMap.UpdatedDate,
                            });
                        }
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketDTO>(jsonString);
        }

    }
}