using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Web.Library.ViewModels.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.School.Academics;
using Newtonsoft.Json;
using System.Globalization;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TicketDetails", "CRUDModel.ViewModel", "class='alignleft two-column-header'")]
    [DisplayName("Ticket Details")]
    public class TicketViewModel : BaseMasterViewModel
    {
        public TicketViewModel()
        {
            //TicketProductSKUs = new List<TicketProductViewModel>() { new TicketProductViewModel() };
            ActionTab = new ActionTabViewModel();
        }

        public long TicketIID { get; set; }

        public long DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left", "ng-change='DocumentTypeChange($event, $element)'")]
        [DisplayName("Type")]
        [LookUp("LookUps.DocumentType")]
        public string DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName("Case no")]
        public string TicketNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR?defaultBlank=false", "LookUps.Customer")]
        [Select2("Customer", "Numeric", false)]
        [DisplayName("Customer")]
        [QuickSmartView("Customer")]
        [QuickCreate("Create,Frameworks/CRUD/Create?screen=Customer, $event,Create Customer, Customer")]
        public KeyValueViewModel Customer { get; set; }

        //[DataPicker("SalesOrderAdvanceSearch", invoker: "TicketController", runtimeFilter: "'CustomerID='+CRUDModel.ViewModel.Customer.Key")]
        //[ControlType(Framework.Enums.ControlTypes.DataPicker, "onecol-header-left")]
        //[DisplayName("Transaction")]
        //public string Transaction { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        //[DisplayName("SKUs")]
        public List<TicketProductViewModel> TicketProductSKUs { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "onecol-header-left")]
        [DisplayName("Short description")]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Problem")]
        public string Description1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Action needed")]
        public string Description2 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Assigned")]
        //[Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        public KeyValueViewModel AssignedTo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("PM")]
        [Select2("Manager", "Numeric", false)]
        [LookUp("LookUps.SupportingManagers")]
        public KeyValueViewModel Manager { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Status", "Numeric", false)]
        [DisplayName("Status")]
        [LookUp("LookUps.TicketStatuses")]
        public KeyValueViewModel Status { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Send email to customer")]
        public bool CustomerNotification { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Ticket date")]
        public String DueDateFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Ticket resolution date")]
        public String DueDateTo { get; set; }

        public Nullable<long> HeadID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Priority")]
        [LookUp("LookUps.TicketPriorities")]
        public string Priority { get; set; }

        [Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='TicketActionChange($event, $element,CRUDModel.ViewModel.Action)'")]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[Select2("Action", "Numeric", false)]
        [DisplayName("Ticket related to")]
        [LookUp("LookUps.SupportActions")]
        public string Action { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HorizontalTab, css: "ActionTabClass", attribs: "ng-if='CRUDModel.ViewModel.Action != null &amp;&amp; CRUDModel.ViewModel.Action != undefined'")]
        //[DisplayName("Action Tab")]
        public ActionTabViewModel ActionTab { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Documents", "Documents")]
        [DisplayName("Documents")]
        [LazyLoad("Mutual/DocumentFile", "Mutual/GetDocumentFiles", "CRUDModel.ViewModel.Document")]
        public DocumentViewViewModel Document { get; set; }

        public static TicketViewModel FromDTO(TicketDTO dto)
        {
            TicketViewModel ticketVM = new TicketViewModel();
            //ticketVM.TicketProductSKUs = new List<TicketProductViewModel>();
            ticketVM.Manager = new KeyValueViewModel();
            ticketVM.AssignedTo = new KeyValueViewModel();
            ticketVM.Customer = new KeyValueViewModel();
            ticketVM.Status = new KeyValueViewModel();

            if (dto.IsNotNull())
            {
                ticketVM.TicketIID = dto.TicketIID;
                ticketVM.TicketNo = dto.TicketNo;
                ticketVM.DocumentTypeID = Convert.ToInt32(dto.DocumentTypeID);
                ticketVM.DocumentType = dto.DocumentTypeID.ToString();
                ticketVM.Subject = dto.Subject;
                ticketVM.Description1 = dto.Description;
                ticketVM.Description2 = dto.Description2;
                ticketVM.Priority = dto.PriorityID.ToString();
                ticketVM.Action = dto.ActionID.ToString();
                // add action value
                ticketVM.Status.Key = dto.TicketStatusID.ToString();
                ticketVM.Status.Value = dto.TicketStatus;
                ticketVM.AssignedTo.Key = dto.AssingedEmployeeID.ToString(); ;
                ticketVM.AssignedTo.Value = dto.AssignedEmployee;
                ticketVM.Manager.Key = dto.ManagerEmployeeID.ToString();
                ticketVM.Manager.Value = dto.ManagerEmployee;
                ticketVM.Customer.Key = dto.CustomerID.ToString();
                ticketVM.Customer.Value = dto.CustomerName;
                ticketVM.DueDateFrom = Convert.ToString(dto.DueDateFrom);
                ticketVM.DueDateTo = Convert.ToString(dto.DueDateTo);
                ticketVM.HeadID = dto.HeadID;
                ticketVM.CustomerNotification = Convert.ToBoolean(dto.CustomerNotification);
                ticketVM.CreatedBy = dto.CreatedBy;
                ticketVM.CreatedDate = dto.CreatedDate;
                ticketVM.UpdatedBy = dto.UpdatedBy;
                ticketVM.UpdatedDate = dto.UpdatedDate;
                ticketVM.TimeStamps = dto.TimeStamps;

                //if (dto.TicketProductSKUs.IsNull() || dto.TicketProductSKUs.Count == 0)
                //{
                //    ticketVM.TicketProductSKUs.Add(new TicketProductViewModel());
                //}

                //if (dto.TicketProductSKUs.IsNotNull() && dto.TicketProductSKUs.Count > 0)
                //{
                //    TicketProductViewModel tpVM = null;

                //    foreach (TicketProductDTO tpDTO in dto.TicketProductSKUs)
                //    {
                //        tpVM = new TicketProductViewModel();
                //        tpVM.SKUID = new KeyValueViewModel();

                //        tpVM.TicketProductMapID = tpDTO.TicketProductMapIID;
                //        tpVM.ProductID = tpDTO.ProductID;
                //        tpVM.SKUID.Key = tpDTO.SKUID.ToString();
                //        tpVM.SKUID.Value = tpDTO.SKUName;
                //        tpVM.Reason = tpDTO.ReasonID.ToString();
                //        tpVM.Quantity = tpDTO.Quantity;
                //        tpVM.Narration = tpDTO.Narration;
                //        tpVM.TicketID = tpDTO.TicketID;
                //        tpVM.CreatedBy = tpDTO.CreatedBy;
                //        tpVM.CreatedDate = tpDTO.CreatedDate;
                //        tpVM.UpdatedBy = tpDTO.UpdatedBy;
                //        tpVM.UpdatedDate = tpDTO.UpdatedDate;
                //        tpVM.TimeStamps = tpDTO.TimeStamps;

                //        ticketVM.TicketProductSKUs.Add(tpVM);
                //    }
                //}


                if (dto.TicketActionDetail.IsNotNull())
                {

                    ticketVM.ActionTab = new ActionTabViewModel();
                    ticketVM.ActionTab.ActionDetailMapIID = dto.TicketActionDetail.TicketActionDetailIID;
                    ticketVM.ActionTab.CreatedBy = dto.TicketActionDetail.CreatedBy.IsNotNull() ? Convert.ToInt32(dto.TicketActionDetail.CreatedBy) : (int?)null;
                    ticketVM.ActionTab.UpdatedBy = dto.TicketActionDetail.UpdatedBy.IsNotNull() ? Convert.ToInt32(dto.TicketActionDetail.UpdatedBy) : (int?)null;
                    ticketVM.ActionTab.CreatedDate = dto.TicketActionDetail.CreatedDate;
                    ticketVM.ActionTab.UpdatedDate = dto.TicketActionDetail.UpdatedDate;
                    ticketVM.ActionTab.TimeStamps = dto.TicketActionDetail.Timestamps;

                    // Get Tab detail using selected action
                    switch ((Services.Contracts.Enums.TicketActions)Enum.Parse(typeof(Services.Contracts.Enums.TicketActions), dto.ActionID.ToString()))
                    {
                        case Services.Contracts.Enums.TicketActions.Refund:
                            ticketVM.ActionTab.RefundType = new KeyValueViewModel();
                            ticketVM.ActionTab.RefundType.Key = Convert.ToString(dto.TicketActionDetail.RefundTypeID) ?? null;
                            ticketVM.ActionTab.RefundType.Value = Convert.ToString(dto.TicketActionDetail.SubActionName) ?? null;
                            ticketVM.ActionTab.RefundAmount = dto.TicketActionDetail.RefundAmount;
                            ticketVM.ActionTab.Reason = dto.TicketActionDetail.Reason;
                            ticketVM.ActionTab.Remarks = dto.TicketActionDetail.Remark;
                            ticketVM.ActionTab.ReturnNumber = dto.TicketActionDetail.ReturnNumber;

                            break;
                        case Services.Contracts.Enums.TicketActions.CollectItem:
                            ticketVM.ActionTab.CollectItem.Remarks = dto.TicketActionDetail.Remark;
                            ticketVM.ActionTab.CollectItem.GiveItemTo.Key = dto.TicketActionDetail.GiveItemTo.IsNotNull() ? Convert.ToString(dto.TicketActionDetail.GiveItemTo) : null;
                            ticketVM.ActionTab.CollectItem.GiveItemTo.Value = dto.TicketActionDetail.GiveItemTo.IsNotNull() ? Convert.ToString(dto.TicketActionDetail.SubActionName) : null;

                            break;
                        case Services.Contracts.Enums.TicketActions.DirectReplacement:
                            ticketVM.ActionTab.DirectReplacement.GiveItemTo.Key = dto.TicketActionDetail.GiveItemTo.IsNotNull() ? Convert.ToString(dto.TicketActionDetail.GiveItemTo) : null;
                            ticketVM.ActionTab.DirectReplacement.GiveItemTo.Value = dto.TicketActionDetail.GiveItemTo.IsNotNull() ? Convert.ToString(dto.TicketActionDetail.SubActionName) : null;

                            break;
                        case Services.Contracts.Enums.TicketActions.Arrangement:
                            // In case of arrangement, fill TicketDetailDetailMapDTO too
                            if (dto.TicketActionDetail.TicketActionDetailDetailMaps.IsNotNull() && dto.TicketActionDetail.TicketActionDetailDetailMaps.Count > 0)
                            {

                                foreach (var item in dto.TicketActionDetail.TicketActionDetailDetailMaps)
                                {
                                    ticketVM.ActionTab.Arrangement.Notify.Add(new KeyValueViewModel { Key = item.TicketActionDetailDetailMapIID.ToString(), Value = ((Services.Contracts.Enums.TicketActions)item.TicketActionDetailDetailMapIID).ToString() });

                                }
                            }
                            break;
                        case Services.Contracts.Enums.TicketActions.DigitalCard:
                            ticketVM.ActionTab.DigitalCard.IssueType.Key = dto.TicketActionDetail.IssueType.IsNotNull() ? Convert.ToString(dto.TicketActionDetail.IssueType) : null;
                            ticketVM.ActionTab.DigitalCard.IssueType.Key = dto.TicketActionDetail.IssueType.IsNotNull() ? Convert.ToString(dto.TicketActionDetail.SubActionName) : null;
                            ticketVM.ActionTab.DigitalCard.Employees.Key = dto.TicketActionDetail.AssignedEmployee.Key;
                            ticketVM.ActionTab.DigitalCard.Employees.Value = dto.TicketActionDetail.AssignedEmployee.Value;
                            break;
                        case Services.Contracts.Enums.TicketActions.AmountCapture:
                            // TODO
                            break;
                        default:
                            break;
                    }

                }

            }

            if (dto.Document != null && dto.Document.Documents != null && dto.Document.Documents.Count > 0)
            {
                ticketVM.Document.Documents = DocumentFileViewModel.FromDTO(dto.Document.Documents);
            }

            if (ticketVM.Document == null)
            {
                ticketVM.Document = new DocumentViewViewModel();
            }

            if (ticketVM.Document.Documents == null)
            {
                ticketVM.Document.Documents = new List<DocumentFileViewModel>();
            }

            if (ticketVM.Document.Documents.Count == 0)
            {
                ticketVM.Document.Documents.Add(new DocumentFileViewModel());
            }

            return ticketVM;
        }

        public static TicketDTO ToDTO(TicketViewModel vm)
        {
            TicketDTO ticketDTO = new TicketDTO();
            ticketDTO.TicketProductSKUs = new List<TicketProductDTO>();

            if (vm.IsNotNull())
            {
                ticketDTO.TicketIID = vm.TicketIID;
                ticketDTO.TicketNo = vm.TicketNo;
                ticketDTO.DocumentTypeID = Convert.ToInt32(vm.DocumentType);
                ticketDTO.Subject = vm.Subject;
                ticketDTO.Description = vm.Description1;
                ticketDTO.Description2 = vm.Description2;
                ticketDTO.PriorityID = Convert.ToByte(vm.Priority);
                ticketDTO.ActionID = Convert.ToByte(vm.Action);
                ticketDTO.TicketStatusID = Convert.ToByte(vm.Status.Key);
                ticketDTO.AssingedEmployeeID = vm.AssignedTo.IsNotNull() ? Convert.ToInt32(vm.AssignedTo.Key) : (long?)null;
                ticketDTO.ManagerEmployeeID = vm.Manager.IsNotNull() ? Convert.ToInt32(vm.Manager.Key) : (long?)null;
                ticketDTO.CustomerID = vm.Customer.IsNotNull() ? Convert.ToInt32(vm.Customer.Key) : (long?)null;
                ticketDTO.DueDateFrom = Convert.ToDateTime(vm.DueDateFrom);
                ticketDTO.DueDateTo = Convert.ToDateTime(vm.DueDateTo);
                ticketDTO.HeadID = vm.HeadID.IsNotNull() ? vm.HeadID : null;
                ticketDTO.CustomerNotification = vm.CustomerNotification;

                ticketDTO.CreatedBy = vm.CreatedBy;
                ticketDTO.CreatedDate = vm.CreatedDate;
                ticketDTO.UpdatedBy = vm.UpdatedBy;
                ticketDTO.UpdatedDate = vm.UpdatedDate;
                ticketDTO.TimeStamps = vm.TimeStamps;

                /** Please uncomment below section when wanted to save Tab Actions **/
                #region Ticket Action Tabs Section

                //// Check selected action and fill dto
                //ticketDTO.TicketActionDetail = new TicketActionDetailMapsDTO();
                //ticketDTO.TicketActionDetail.TicketActionDetailIID = vm.ActionTab.ActionDetailMapIID;
                //ticketDTO.TicketActionDetail.CreatedBy = vm.ActionTab.CreatedBy;
                //ticketDTO.TicketActionDetail.UpdatedBy = vm.ActionTab.UpdatedBy;
                //ticketDTO.TicketActionDetail.CreatedDate = vm.ActionTab.CreatedDate;
                //ticketDTO.TicketActionDetail.UpdatedDate = vm.ActionTab.UpdatedDate;
                //ticketDTO.TicketActionDetail.Timestamps = vm.ActionTab.TimeStamps;



                //switch ((Services.Contracts.Enums.TicketActions)Enum.Parse(typeof(Services.Contracts.Enums.TicketActions), vm.Action))
                //{
                //    case Services.Contracts.Enums.TicketActions.Refund:
                //        ticketDTO.TicketActionDetail.RefundTypeID = vm.ActionTab.RefundType != null ? Convert.ToInt32(vm.ActionTab.RefundType.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.RefundAmount = vm.ActionTab.RefundAmount;
                //        ticketDTO.TicketActionDetail.Reason = vm.ActionTab.Reason;
                //        ticketDTO.TicketActionDetail.Remark = vm.ActionTab.Remarks;
                //        ticketDTO.TicketActionDetail.ReturnNumber = vm.ActionTab.ReturnNumber;

                //        break;
                //    case Services.Contracts.Enums.TicketActions.CollectItem:
                //        ticketDTO.TicketActionDetail.GiveItemTo = vm.ActionTab.CollectItem.GiveItemTo != null ? Convert.ToInt32(vm.ActionTab.CollectItem.GiveItemTo.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.Remark = vm.ActionTab.CollectItem.Remarks;


                //        break;
                //    case Services.Contracts.Enums.TicketActions.DirectReplacement:

                //        ticketDTO.TicketActionDetail.GiveItemTo = vm.ActionTab.DirectReplacement.GiveItemTo != null ? Convert.ToInt32(vm.ActionTab.DirectReplacement.GiveItemTo.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.Remark = vm.ActionTab.DirectReplacement.Remarks;
                //        break;
                //    case Services.Contracts.Enums.TicketActions.Arrangement:
                //        // In case of arrangement, fill TicketDetailDetailMapDTO too
                //        if (vm.ActionTab.Arrangement.Notify != null && vm.ActionTab.Arrangement.Notify.Count > 0)
                //        {
                //            foreach (var item in vm.ActionTab.Arrangement.Notify)
                //            {
                //                ticketDTO.TicketActionDetail.TicketActionDetailDetailMaps.Add(new TicketActionDetailDetailMapDTO()
                //                {
                //                    TicketActionDetailDetailMapIID = vm.ActionTab.Arrangement.TicketActionDetailDetailMapIID,
                //                    Notify = Convert.ToInt32(item.Key),
                //                });
                //            }
                //        }

                //        break;
                //    case Services.Contracts.Enums.TicketActions.DigitalCard:

                //        ticketDTO.TicketActionDetail.IssueType = vm.ActionTab.DigitalCard.IssueType != null ? Convert.ToInt32(vm.ActionTab.DigitalCard.IssueType.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.AssignedEmployee = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = vm.ActionTab.DigitalCard.Employees.Key, Value = vm.ActionTab.DigitalCard.Employees.Value }; // later change it to list 
                //        break;
                //    case Services.Contracts.Enums.TicketActions.AmountCapture:
                //        break;
                //    default:
                //        break;
                //}
                #endregion
                /** Please uncomment above section when wanted to save Tab Actions **/


                //if (vm.TicketProductSKUs.IsNotNull() && vm.TicketProductSKUs.Count > 0)
                //{
                //    TicketProductDTO tpDTO = null;

                //    foreach (TicketProductViewModel tpVM in vm.TicketProductSKUs)
                //    {
                //        tpDTO = new TicketProductDTO();

                //        tpDTO.TicketProductMapIID = tpVM.TicketProductMapID;
                //        tpDTO.ProductID = tpVM.ProductID;
                //        tpDTO.SKUID = tpVM.SKUID.IsNotNull() ? Convert.ToInt32(tpVM.SKUID.Key) : 0;
                //        tpDTO.ReasonID = Convert.ToInt32(tpVM.Reason);
                //        tpDTO.Narration = tpVM.Narration;
                //        tpDTO.Quantity = tpVM.Quantity;
                //        tpDTO.TicketID = vm.TicketIID;
                //        tpDTO.CreatedBy = tpVM.CreatedBy;
                //        tpDTO.CreatedDate = tpVM.CreatedDate;
                //        tpDTO.CreatedDate = tpVM.CreatedDate;
                //        tpDTO.UpdatedBy = tpVM.UpdatedBy;
                //        tpDTO.UpdatedDate = tpVM.UpdatedDate;
                //        tpDTO.TimeStamps = tpVM.TimeStamps;

                //        ticketDTO.TicketProductSKUs.Add(tpDTO);
                //    }
                //}
            }

            if (vm.Document != null && vm.Document.Documents != null && vm.Document.Documents.Count > 0)
            {
                ticketDTO.Document.Documents = DocumentFileViewModel.ToDTO(vm.Document.Documents);
            }

            return ticketDTO;
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TicketDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TicketDTO, TicketViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mpdto = dto as TicketDTO;
            var vm = Mapper<TicketDTO, TicketViewModel>.Map(mpdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //vm.LessonPlanIID = mpdto.LessonPlanIID;
            //vm.Subject = mpdto.SubjectID.HasValue ? new KeyValueViewModel() { Key = mpdto.Subject.Key.ToString(), Value = mpdto.Subject.Value } : new KeyValueViewModel();
            //vm.LessonPlanStatus = new KeyValueViewModel() { Key = mpdto.LessonPlanStatus.Key.ToString(), Value = mpdto.LessonPlanStatus.Value };
            //vm.Date1String = mpdto.Date1.HasValue ? mpdto.Date1.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.Date2String = mpdto.Date2.HasValue ? mpdto.Date2.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.Month = mpdto.MonthID.HasValue ? mpdto.MonthID.ToString() : null;
            //vm.AcademicYear = mpdto.AcademicYearID.HasValue ? mpdto.AcademicYearID.ToString() : null;
            //vm.IsSendPushNotification = mpdto.IsSendPushNotification;
            //vm.IsSyllabusCompleted = mpdto.IsSyllabusCompleted;
            //vm.HideActionPlan = mpdto.IsSyllabusCompleted == true ? true : false;
            //vm.ActionPlan = mpdto.ActionPlan;

            

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TicketViewModel, TicketDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<TicketViewModel, TicketDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //dto.LessonPlanIID = this.LessonPlanIID;
            //dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            ////dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            //dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            ////dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            //dto.LessonPlanStatusID = string.IsNullOrEmpty(this.LessonPlanStatus.Key) ? (byte?)null : byte.Parse(this.LessonPlanStatus.Key);
            //dto.Date1 = string.IsNullOrEmpty(this.Date1String) ? (DateTime?)null : DateTime.ParseExact(this.Date1String, dateFormat, CultureInfo.InvariantCulture);
            //dto.Date2 = string.IsNullOrEmpty(this.Date2String) ? (DateTime?)null : DateTime.ParseExact(this.Date2String, dateFormat, CultureInfo.InvariantCulture);
            //dto.MonthID = string.IsNullOrEmpty(this.Month) ? (byte?)null : byte.Parse(this.Month);
            ////dto.TeachingAidID = string.IsNullOrEmpty(this.TeachingAid) ? (byte?)null : byte.Parse(this.TeachingAid);
            //dto.IsSendPushNotification = this.IsSendPushNotification;
            //dto.IsSyllabusCompleted = this.IsSyllabusCompleted;
            //dto.ActionPlan = this.ActionPlan;


            var vm = this;
            TicketDTO ticketDTO = new TicketDTO();
            ticketDTO.TicketProductSKUs = new List<TicketProductDTO>();

            if (vm.IsNotNull())
            {
                ticketDTO.TicketIID = vm.TicketIID;
                ticketDTO.TicketNo = vm.TicketNo;
                ticketDTO.DocumentTypeID = Convert.ToInt32(vm.DocumentType);
                ticketDTO.Subject = vm.Subject;
                ticketDTO.Description = vm.Description1;
                ticketDTO.Description2 = vm.Description2;
                ticketDTO.PriorityID = Convert.ToByte(vm.Priority);
                ticketDTO.ActionID = Convert.ToByte(vm.Action);
                ticketDTO.TicketStatusID = Convert.ToByte(vm.Status.Key);
                ticketDTO.AssingedEmployeeID = vm.AssignedTo.IsNotNull() ? Convert.ToInt32(vm.AssignedTo.Key) : (long?)null;
                ticketDTO.ManagerEmployeeID = vm.Manager.IsNotNull() ? Convert.ToInt32(vm.Manager.Key) : (long?)null;
                ticketDTO.CustomerID = vm.Customer.IsNotNull() ? Convert.ToInt32(vm.Customer.Key) : (long?)null;
                ticketDTO.DueDateFrom = Convert.ToDateTime(vm.DueDateFrom);
                ticketDTO.DueDateTo = Convert.ToDateTime(vm.DueDateTo);
                ticketDTO.HeadID = vm.HeadID.IsNotNull() ? vm.HeadID : null;
                ticketDTO.CustomerNotification = vm.CustomerNotification;

                ticketDTO.CreatedBy = vm.CreatedBy;
                ticketDTO.CreatedDate = vm.CreatedDate;
                ticketDTO.UpdatedBy = vm.UpdatedBy;
                ticketDTO.UpdatedDate = vm.UpdatedDate;
                ticketDTO.TimeStamps = vm.TimeStamps;

                /** Please uncomment below section when wanted to save Tab Actions **/
                #region Ticket Action Tabs Section

                //// Check selected action and fill dto
                //ticketDTO.TicketActionDetail = new TicketActionDetailMapsDTO();
                //ticketDTO.TicketActionDetail.TicketActionDetailIID = vm.ActionTab.ActionDetailMapIID;
                //ticketDTO.TicketActionDetail.CreatedBy = vm.ActionTab.CreatedBy;
                //ticketDTO.TicketActionDetail.UpdatedBy = vm.ActionTab.UpdatedBy;
                //ticketDTO.TicketActionDetail.CreatedDate = vm.ActionTab.CreatedDate;
                //ticketDTO.TicketActionDetail.UpdatedDate = vm.ActionTab.UpdatedDate;
                //ticketDTO.TicketActionDetail.Timestamps = vm.ActionTab.TimeStamps;



                //switch ((Services.Contracts.Enums.TicketActions)Enum.Parse(typeof(Services.Contracts.Enums.TicketActions), vm.Action))
                //{
                //    case Services.Contracts.Enums.TicketActions.Refund:
                //        ticketDTO.TicketActionDetail.RefundTypeID = vm.ActionTab.RefundType != null ? Convert.ToInt32(vm.ActionTab.RefundType.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.RefundAmount = vm.ActionTab.RefundAmount;
                //        ticketDTO.TicketActionDetail.Reason = vm.ActionTab.Reason;
                //        ticketDTO.TicketActionDetail.Remark = vm.ActionTab.Remarks;
                //        ticketDTO.TicketActionDetail.ReturnNumber = vm.ActionTab.ReturnNumber;

                //        break;
                //    case Services.Contracts.Enums.TicketActions.CollectItem:
                //        ticketDTO.TicketActionDetail.GiveItemTo = vm.ActionTab.CollectItem.GiveItemTo != null ? Convert.ToInt32(vm.ActionTab.CollectItem.GiveItemTo.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.Remark = vm.ActionTab.CollectItem.Remarks;


                //        break;
                //    case Services.Contracts.Enums.TicketActions.DirectReplacement:

                //        ticketDTO.TicketActionDetail.GiveItemTo = vm.ActionTab.DirectReplacement.GiveItemTo != null ? Convert.ToInt32(vm.ActionTab.DirectReplacement.GiveItemTo.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.Remark = vm.ActionTab.DirectReplacement.Remarks;
                //        break;
                //    case Services.Contracts.Enums.TicketActions.Arrangement:
                //        // In case of arrangement, fill TicketDetailDetailMapDTO too
                //        if (vm.ActionTab.Arrangement.Notify != null && vm.ActionTab.Arrangement.Notify.Count > 0)
                //        {
                //            foreach (var item in vm.ActionTab.Arrangement.Notify)
                //            {
                //                ticketDTO.TicketActionDetail.TicketActionDetailDetailMaps.Add(new TicketActionDetailDetailMapDTO()
                //                {
                //                    TicketActionDetailDetailMapIID = vm.ActionTab.Arrangement.TicketActionDetailDetailMapIID,
                //                    Notify = Convert.ToInt32(item.Key),
                //                });
                //            }
                //        }

                //        break;
                //    case Services.Contracts.Enums.TicketActions.DigitalCard:

                //        ticketDTO.TicketActionDetail.IssueType = vm.ActionTab.DigitalCard.IssueType != null ? Convert.ToInt32(vm.ActionTab.DigitalCard.IssueType.Key) : (int?)null;
                //        ticketDTO.TicketActionDetail.AssignedEmployee = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = vm.ActionTab.DigitalCard.Employees.Key, Value = vm.ActionTab.DigitalCard.Employees.Value }; // later change it to list 
                //        break;
                //    case Services.Contracts.Enums.TicketActions.AmountCapture:
                //        break;
                //    default:
                //        break;
                //}
                #endregion
                /** Please uncomment above section when wanted to save Tab Actions **/


                //if (vm.TicketProductSKUs.IsNotNull() && vm.TicketProductSKUs.Count > 0)
                //{
                //    TicketProductDTO tpDTO = null;

                //    foreach (TicketProductViewModel tpVM in vm.TicketProductSKUs)
                //    {
                //        tpDTO = new TicketProductDTO();

                //        tpDTO.TicketProductMapIID = tpVM.TicketProductMapID;
                //        tpDTO.ProductID = tpVM.ProductID;
                //        tpDTO.SKUID = tpVM.SKUID.IsNotNull() ? Convert.ToInt32(tpVM.SKUID.Key) : 0;
                //        tpDTO.ReasonID = Convert.ToInt32(tpVM.Reason);
                //        tpDTO.Narration = tpVM.Narration;
                //        tpDTO.Quantity = tpVM.Quantity;
                //        tpDTO.TicketID = vm.TicketIID;
                //        tpDTO.CreatedBy = tpVM.CreatedBy;
                //        tpDTO.CreatedDate = tpVM.CreatedDate;
                //        tpDTO.CreatedDate = tpVM.CreatedDate;
                //        tpDTO.UpdatedBy = tpVM.UpdatedBy;
                //        tpDTO.UpdatedDate = tpVM.UpdatedDate;
                //        tpDTO.TimeStamps = tpVM.TimeStamps;

                //        ticketDTO.TicketProductSKUs.Add(tpDTO);
                //    }
                //}
            }

            if (vm.Document != null && vm.Document.Documents != null && vm.Document.Documents.Count > 0)
            {
                ticketDTO.Document.Documents = DocumentFileViewModel.ToDTO(vm.Document.Documents);
            }

            return ticketDTO;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TicketDTO>(jsonString);
        }

    }
}