using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "GeneratePO", "CRUDModel.ViewModel")]
    [DisplayName("Generate PO")]
    public class BidToPurchaseOrderViewModel : BaseMasterViewModel
    {
        public BidToPurchaseOrderViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            TransactionDate = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            ComparedList = new List<RFQComparisonComparedListViewModel>() { new RFQComparisonComparedListViewModel() };
        }

        public long HeadIID { get; set; }
        public long? TenderIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public string TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ClosedRFQ", "Numeric", false, "BidRFQChanges($event, CRUDModel.ViewModel)")]
        [CustomDisplay("RFQ")]
        [LookUp("LookUps.ClosedRFQ")]
        public KeyValueViewModel ClosedRFQ { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Bids", "Numeric", false, "BidChanges($event, CRUDModel.ViewModel)")]
        [CustomDisplay("Bid")]
        [LookUp("LookUps.Bids")]
        public KeyValueViewModel Bids { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("")]
        public string ListDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Validity")]
        public string Validity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("To PO list")]
        public List<RFQComparisonComparedListViewModel> ComparedList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TransactionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<BidToPurchaseOrderViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TransactionDTO, BidToPurchaseOrderViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var frmDto = dto as TransactionDTO;
            var vm = Mapper<TransactionDTO, BidToPurchaseOrderViewModel>.Map(frmDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.ComparedList = new List<RFQComparisonComparedListViewModel>();
            vm.HeadIID = frmDto.TransactionHead.HeadIID;
            vm.TransactionDate = frmDto.TransactionHead.TransactionDate.HasValue ? frmDto.TransactionHead.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Validity = frmDto.TransactionHead.DueDate.HasValue ? frmDto.TransactionHead.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Remarks = frmDto.TransactionHead.Description;
            vm.Bids = frmDto.TransactionHead.BidID.HasValue ? new KeyValueViewModel() { Key = frmDto.TransactionHead.BidID.ToString(), Value = frmDto.TransactionHead.Bid } : new KeyValueViewModel();
            vm.ClosedRFQ = frmDto.TransactionHead.RFQID.HasValue ? new KeyValueViewModel() { Key = frmDto.TransactionHead.RFQID.ToString(), Value = frmDto.TransactionHead.RFQ } : new KeyValueViewModel();


            foreach (var list in frmDto.TransactionDetails)
            {
                vm.ComparedList.Add(new RFQComparisonComparedListViewModel()
                {
                    QuotationNo = list.QuotationNo,
                    ProductCode = list.ProductCode,
                    Description = list.Description,
                    Quantity = list.Quantity,
                    Unit = list.Unit,
                    UnitPrice = list.UnitPrice,
                    Fraction = list.Fraction,
                    ForeignRate = list.ForeignRate,
                    Amount = list.Amount,
                    Supplier = list.Supplier,
                    SKUID = new KeyValueViewModel() { Key = list.SKUID.Key.ToString(), Value = list.SKUID.Value.ToString() }
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Mapper<BidToPurchaseOrderViewModel, TransactionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = new TransactionDTO();

            dto.TransactionHead = new TransactionHeadDTO();
            dto.TransactionHead.TransactionDate = string.IsNullOrEmpty(this.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(this.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
            dto.TransactionHead.DueDate = string.IsNullOrEmpty(this.Validity) ? (DateTime?)null : DateTime.ParseExact(this.Validity, dateFormat, CultureInfo.InvariantCulture);
            dto.TransactionHead.HeadIID = this.HeadIID;
            dto.TransactionHead.Description = this.Remarks;
            dto.TransactionHead.BidID = this.Bids == null || string.IsNullOrEmpty(this.Bids.Key) ? (long?)null : long.Parse(this.Bids.Key);
            dto.TransactionHead.Bid = this.Bids.Value; 
            dto.TransactionHead.RFQID = this.ClosedRFQ == null || string.IsNullOrEmpty(this.ClosedRFQ.Key) ? (long?)null : long.Parse(this.ClosedRFQ.Key);
            dto.TransactionHead.RFQ = this.ClosedRFQ.Value;

            if (this.ComparedList != null)
            {
                dto.TransactionDetails = this.ComparedList
                    .Where(list => list.ProductID != null)
                    .Select(list => new TransactionDetailDTO
                    {
                        HeadID = list.HeadID,
                        QuotationNo = list.QuotationNo,
                        ProductCode = list.ProductCode,
                        Unit = list.Unit,
                        SupplierID = list.SupplierID,
                        ProductID = list.ProductID,
                        Description = list.Description,
                        Supplier = list.Supplier,
                        ProductSKUMapID = long.Parse(list.SKUID.Key),
                        SKUID = new KeyValueDTO () {Key = list.SKUID.Key.ToString(), Value = list.SKUID.Value.ToString() },
                        Quantity = list.Quantity,
                        UnitID = list.UnitID,
                        UnitPrice = list.UnitPrice,
                        Amount = list.Amount,
                        Fraction = list.Fraction,
                        ForeignRate = list.ForeignRate,
                        UnitGroupID = list.UnitGroupID
                    })
                    .ToList();
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TransactionDTO>(jsonString);
        }

    }
}
