using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Supports;
using System.Globalization;
using System;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Tickets", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class TicketingViewModel : BaseMasterViewModel
    {
        public TicketingViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            CustomerNotification = false;
            IsSendMail = false;
            DueDateFromString = string.IsNullOrEmpty(dateFormat) ? null : DateTime.Now.Date.ToString(dateFormat, CultureInfo.InvariantCulture);
            Customer = new KeyValueViewModel();
            Parent = new KeyValueViewModel();
            Manager = new KeyValueViewModel();
            IsAutoCreation = false;
            TicketCommunication = new TicketingCommunicationViewModel();
            TicketFeeDueMaps = new List<TicketingFeeDueMapViewModel> { new TicketingFeeDueMapViewModel() };
        }

        public long TicketIID { get; set; }

        public bool IsAutoCreation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", "ng-change='GetNextTransactionNumberByDocumentType(CRUDModel.ViewModel.DocumentType)'")]
        [DisplayName("Type")]
        [LookUp("LookUps.TicketDocumentTypes")]
        public string DocumentType { get; set; }
        public int DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Case no")]
        public string TransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine1 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Customer", "Numeric", false, "", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.TicketIID>0 || CRUDModel.ViewModel.Parent.Key || CRUDModel.ViewModel.IsAutoCreation'")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR?defaultBlank=false", "LookUps.Customer")]
        [DisplayName("Customer")]
        public KeyValueViewModel Customer { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Parent", "Numeric", false, "", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.TicketIID>0 || CRUDModel.ViewModel.Customer.Key || CRUDModel.ViewModel.IsAutoCreation'")]
        [LookUp("LookUps.Parent")]
        [DisplayName("Parent")]
        public KeyValueViewModel Parent { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Parent Name")]
        public string ParentName { get; set; }
        public long? ParentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Short description")]
        [MaxLength(100)]
        public string Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [DisplayName("Problem")]
        public string Description1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine4 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Action needed")]
        public string Description2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine5 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Assigned")]
        //[Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        public KeyValueViewModel AssignedTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Department")]
        [LookUp("LookUps.Department")]
        public string Department { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, attribs: "ng-click=\"FillEmployees(CRUDModel.ViewModel)\"")]
        [DisplayName("Fill Employees")]
        public string FillEmployeeButton { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Supporting Employee")]
        [Select2("Manager", "Numeric", false)]
        [LookUp("LookUps.SupportingManagers")]
        public KeyValueViewModel Manager { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [Select2("Status", "Numeric", false)]
        [DisplayName("Status")]
        [LookUp("LookUps.TicketStatuses")]
        public string Status { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Send email")]
        public bool? IsSendMail { get; set; }
        public bool? CustomerNotification { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine7 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Ticket date")]
        public string DueDateFromString { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Ticket resolution date")]
        public string DueDateToString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine8 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Priority")]
        [LookUp("LookUps.TicketPriorities")]
        public string Priority { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Ticket related to")]
        [LookUp("LookUps.SupportActions")]
        public string Action { get; set; }

        public long? LoginID { get; set; }

        public string CustomerEmailID { get; set; }

        public long? ReferenceID { get; set; }

        public byte? ReferenceTypeID { get; set; }

        public long? StudentID { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "TicketCommunications", "TicketCommunications")]
        [CustomDisplay("Communications")]
        public TicketingCommunicationViewModel TicketCommunication { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Fee due details")]
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
            vm.Description1 = tkDTO.Description;
            vm.Description2 = tkDTO.Description2;
            vm.Manager = tkDTO.ManagerEmployeeID.HasValue ? new KeyValueViewModel()
            {
                Key = tkDTO.ManagerEmployeeID.ToString(),
                Value = tkDTO.ManagerEmployee
            } : new KeyValueViewModel();
            vm.Status = tkDTO.TicketStatusID.HasValue ? tkDTO.TicketStatusID.ToString() : null;
            vm.CustomerNotification = tkDTO.CustomerNotification;
            vm.DueDateFromString = tkDTO.DueDateFrom.HasValue ? tkDTO.DueDateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.DueDateToString = tkDTO.DueDateTo.HasValue ? tkDTO.DueDateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Priority = tkDTO.PriorityID.HasValue ? tkDTO.PriorityID.ToString() : null;
            vm.Action = tkDTO.ActionID.ToString();
            vm.IsSendMail = tkDTO.IsSendMail;
            vm.ReferenceTypeID = tkDTO.ReferenceTypeID;
            vm.Department = tkDTO.DepartmentID.HasValue ? tkDTO.DepartmentID.ToString() : null;

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

            //if (this.IsAutoCreation == true && this.ParentID.HasValue)
            //{
            //    dto.ParentID = this.ParentID;
            //}
            //else
            //{
            //    dto.ParentID = string.IsNullOrEmpty(this.Parent.Key) ? (long?)null : long.Parse(this.Parent.Key);
            //}

            dto.ParentID = string.IsNullOrEmpty(this.Parent.Key) ? (long?)null : long.Parse(this.Parent.Key);

            dto.Subject = this.Subject;
            dto.Description = this.Description1;
            dto.Description2 = this.Description2;
            dto.ManagerEmployeeID = string.IsNullOrEmpty(this.Manager.Key) ? (int?)null : int.Parse(this.Manager.Key);
            dto.TicketStatusID = string.IsNullOrEmpty(this.Status) ? (byte?)null : byte.Parse(this.Status);
            dto.CustomerNotification = this.CustomerNotification == null ? false : this.CustomerNotification;
            dto.DueDateFrom = string.IsNullOrEmpty(this.DueDateFromString) ? (DateTime?)null : DateTime.ParseExact(this.DueDateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DueDateTo = string.IsNullOrEmpty(this.DueDateToString) ? (DateTime?)null : DateTime.ParseExact(this.DueDateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.PriorityID = string.IsNullOrEmpty(this.Priority) ? (byte?)null : byte.Parse(this.Priority);
            dto.ActionID = byte.Parse(this.Action);
            dto.IsSendMail = this.IsSendMail == null ? false : this.IsSendMail;
            dto.ReferenceTypeID = this.ReferenceTypeID;

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

            if (this.ReferenceTypeID == (byte?)TicketReferenceTypes.FeeDue)
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

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketDTO>(jsonString);
        }

    }
}