using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Web.Library.ViewModels.Scheduler;

namespace Eduegate.Web.Library.ViewModels.Settings
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DocumentType", "CRUDModel.ViewModel")]
    [DisplayName("Document Details")]
    public class DocumentTypeViewModel : BaseMasterViewModel
    {
        public DocumentTypeViewModel()
        {
            AccountSettings = new Accounts.AccountSettingsViewModel();
            SecuritySettings = new Security.SecuritySettingsViewModel();
            SchedulerInfo = new Scheduler.SchedulerInfoViewModel();
            DocumentMap = new DocumentDocumentMapViewModel();
            DocumentNumber = new DocumentNoViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Document Type ID")]
        public int DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string TransactionTypeName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ReferenceType")]
        [DisplayName("Reference Type")]
        public string ReferenceType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Transaction Number Prefix")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string TransactionNoPrefix { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.SequenceTypes")]
        [DisplayName("Transaction Sequence Type")]
        public string TransactionSequenceType { get; set; }

        public int Regular { get; set; }
        public int Monthly { get; set; }
        public int Yearly { get; set; }
        
       // [ControlType(Framework.Enums.ControlTypes.TextBox,"", "")]

        [ControlType(Framework.Enums.ControlTypes.TextBox,"", "ng-disabled= 'CRUDModel.ViewModel.TransactionSequenceType!=1' ")]
        [DisplayName("Last document no")]
        public string LastTransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Ignore the inventory check")]
        public Nullable<bool> IgnoreInventoryCheck { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TaxTemplates")]
        [DisplayName("Tax Template")]
        public string TaxTemplate { get; set; }

        public Nullable<int> TaxTemplateID { get; set; }

        public Nullable<int> ReferenceTypeID { get; set; }
        public string System { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AccountSettings", "CRUDModel.ViewModel.AccountSettings")]
        [DisplayName("Account Settings")]
        public Accounts.AccountSettingsViewModel AccountSettings { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SecuritySettings", "CRUDModel.ViewModel.SecuritySettings")]
        [DisplayName("Security")]
        public Security.SecuritySettingsViewModel SecuritySettings { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SchedulerInfo", "CRUDModel.ViewModel.SchedulerInfo")]
        [DisplayName("Schedulers")]
        public Scheduler.SchedulerInfoViewModel SchedulerInfo { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SchedulerInfo", "CRUDModel.ViewModel.DocumentMap")]
        [DisplayName("Document Workflow")]
        public DocumentDocumentMapViewModel DocumentMap { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DocumentNumbers", "CRUDModel.ViewModel.DocumentNumber", "ng-disabled='CRUDModel.ViewModel.TransactionSequenceType!=1'")]
        [DisplayName("Document No")]
        public DocumentNoViewModel DocumentNumber { get; set; }

        public static DocumentTypeViewModel ToVM(DocumentTypeDTO dto)
        {
            if (dto == null) return new DocumentTypeViewModel();

            Mapper<DocumentTypeDTO, DocumentTypeViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mapper = Mapper<DocumentTypeDTO, DocumentTypeViewModel>.Map(dto);
            mapper.ReferenceType = dto.ReferenceTypeID.ToString();
            mapper.AccountSettings.GLAccountID = dto.GLAccountID;
            //mapper.SecuritySettings.Claims = KeyValueViewModel.FromDTO(dto.Claims);
            mapper.SecuritySettings.ClaimSets = KeyValueViewModel.FromDTO(dto.ClaimSets);
            mapper.SchedulerInfo.Schedulers = SchedulerGridViewModel.FromDTO(dto.Schedulers);
            mapper.DocumentMap.DocumentMaps = DocumentMapViewModel.FromDTO(dto.DocumentMaps);
            mapper.DocumentNumber.DocumentNos = DocumentNosViewModel.FromDTO(dto.DocumentTypeTransactionNumbers);
            mapper.DocumentMap.ApprovalWorkflow = KeyValueViewModel.ToViewModel(dto.ApprovalWorkflow);
            mapper.TaxTemplate = dto.TaxTamplateID.HasValue ? dto.TaxTamplateID.Value.ToString() : null;
            return mapper;
        }

        public static DocumentTypeDTO ToDTO(DocumentTypeViewModel vm, SchedulerTypes? type = null)
        {
            Mapper<DocumentTypeViewModel, DocumentTypeDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapper = Mapper<DocumentTypeViewModel, DocumentTypeDTO>.Map(vm);
            mapper.ReferenceTypeID = int.Parse(vm.ReferenceType);
            mapper.GLAccountID = vm.AccountSettings.GLAccountID;
            //mapper.Claims = KeyValueViewModel.ToDTO(vm.SecuritySettings.Claims);
            mapper.ClaimSets = KeyValueViewModel.ToDTO(vm.SecuritySettings.ClaimSets);
            mapper.ApprovalWorkflow = KeyValueViewModel.ToDTO(vm.DocumentMap.ApprovalWorkflow);

            if (type.HasValue)
            {
                mapper.Schedulers = SchedulerGridViewModel.ToDTO(vm.SchedulerInfo.Schedulers, type.Value, mapper.DocumentTypeID.ToString());
            }
            mapper.DocumentMaps = DocumentMapViewModel.ToDTO(vm.DocumentMap.DocumentMaps,mapper.DocumentTypeID);
            mapper.DocumentTypeTransactionNumbers = DocumentNosViewModel.ToDTO(vm.DocumentNumber.DocumentNos, mapper.DocumentTypeID);
            mapper.TaxTamplateID = !string.IsNullOrEmpty(vm.TaxTemplate) ? int.Parse(vm.TaxTemplate.ToString()) : (int?)null; 
            return mapper;
        }

        public static KeyValueViewModel ToKeyValueVM(DocumentTypeDTO dto)
        {
            Mapper<DocumentTypeDTO, KeyValueViewModel>.CreateMap();
            var mapper = Mapper<DocumentTypeDTO, KeyValueViewModel>.Map(dto);
            mapper.Key = dto.DocumentTypeID.ToString();
            mapper.Value = dto.TransactionTypeName;
            return mapper;
        }

        public static List<KeyValueViewModel> ToKeyValueVMs(List<DocumentTypeDTO> dto)
        {
            return dto.Select(x => DocumentTypeViewModel.ToKeyValueVM(x)).ToList(); ;
        }
    }
}
