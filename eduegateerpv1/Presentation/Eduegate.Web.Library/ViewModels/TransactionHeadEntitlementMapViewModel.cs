using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TransactionHeadEntitlementMaps", "CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps")]
    [DisplayName("Payments")]
    public class TransactionHeadEntitlementMapViewModel : BaseMasterViewModel
    {
        public TransactionHeadEntitlementMapViewModel()
        { 
        IsCreditNoteDisabled = true;
        }
        public long TransactionHeadEntitlementMapID { get; set; }
        public long TransactionHeadID { get; set; }
        public byte EntitlementID { get; set; }
        public bool IsCreditNoteDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("Types")]
        [LookUp("LookUps.Entitlement")]
        [Select2("Entitlements", "Numeric", false, "OnEntitlementChange(gridModel)", false, "ng-click=LoadEntitlement($index)")]
        public KeyValueViewModel Entitlement { get; set; }       

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [DisplayName("ReferenceNo")]
        public string ReferenceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Amount")]
        [DisplayName("Amount")]
        public Nullable<decimal> Amount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", Attributes = "ng-show='gridModel.Entitlement.Key == 12'")] // 12 for voucher
        //[DisplayName("VoucherCode")]
        public string VoucherCode { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.MasterViewModel.TransactionHeadEntitlementMaps[0], CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.MasterViewModel.TransactionHeadEntitlementMaps[0], CRUDModel.Model.MasterViewModel.TransactionHeadEntitlementMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public static TransactionHeadEntitlementMapViewModel ToVm(TransactionHeadEntitlementMapDTO dto)
        {
            //Mapper<TransactionHeadEntitlementMapDTO, TransactionHeadEntitlementMapViewModel>.CreateMap();
            //var vm = Mapper<TransactionHeadEntitlementMapDTO, TransactionHeadEntitlementMapViewModel>.Map(dto);
            //return vm;
            var vm = new TransactionHeadEntitlementMapViewModel();
            if (dto != null)
            {
                vm.TransactionHeadEntitlementMapID = dto.TransactionHeadEntitlementMapID;
                vm.TransactionHeadID = dto.TransactionHeadID;
                vm.EntitlementID = dto.EntitlementID;
                vm.Entitlement = new KeyValueViewModel() { Key = dto.EntitlementID.ToString(), Value = dto.EntitlementName };
                vm.Amount = dto.Amount;
                vm.ReferenceNo = dto.ReferenceNo;
                vm.VoucherCode = dto.VoucherCode;

                return vm;
            }
            else return null;
        }

        public static TransactionHeadEntitlementMapDTO ToDto(TransactionHeadEntitlementMapViewModel vm)
        {
            //Mapper<TransactionHeadEntitlementMapViewModel, TransactionHeadEntitlementMapDTO>.CreateMap();
            //var dto = Mapper<TransactionHeadEntitlementMapViewModel, TransactionHeadEntitlementMapDTO>.Map(vm);
            //return dto;
            var dto = new TransactionHeadEntitlementMapDTO();
            if (vm != null)
            {
                dto.TransactionHeadEntitlementMapID = vm.TransactionHeadEntitlementMapID;
                dto.TransactionHeadID = vm.TransactionHeadID;
                dto.EntitlementID = vm.Entitlement != null ? Convert.ToByte(vm.Entitlement.Key) : default(byte);
                dto.EntitlementName = vm.Entitlement != null ? vm.Entitlement.Value : null;
                dto.Amount = vm.Amount;
                dto.ReferenceNo = vm.ReferenceNo;
                dto.VoucherCode = vm.VoucherCode;

                return dto;
            }
            else return null;
        }
    }
}
