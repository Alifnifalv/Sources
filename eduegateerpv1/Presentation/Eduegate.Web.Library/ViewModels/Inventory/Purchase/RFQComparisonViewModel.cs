using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Domain;
using Eduegate.TransactionEgineCore;
using static Eduegate.Services.Contracts.ErrorCodes;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RFQComparison", "CRUDModel.ViewModel")]
    [DisplayName("RFQ Comparison")]
    public class RFQComparisonViewModel : BaseMasterViewModel
    {
        public RFQComparisonViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            TransactionDate = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            Quotations = new List<KeyValueViewModel>();
            ItemList = new List<RFQComparisonDetailViewModel>() { new RFQComparisonDetailViewModel() };
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
        [Select2("RFQ", "Numeric", false, "RFQChanges($event, CRUDModel.ViewModel)")]
        [CustomDisplay("RFQ")]
        [LookUp("LookUps.RFQ")]
        public KeyValueViewModel RFQ { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "halfwidth")]
        [CustomDisplay("Quotations")]
        [Select2("Quotations", "Numeric", true, "QuotationChanges($event, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.Quotations")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Quotations", "LookUps.Quotations")]
        public List<KeyValueViewModel> Quotations { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Validity")]
        public string Validity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='FillItemList($event,CRUDModel.ViewModel)'")]
        [CustomDisplay("Fill Data")]
        public string FillDataBtn { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button,"", "ng-click='ComapareList($event,CRUDModel.ViewModel)'")]
        [CustomDisplay("Compare")]
        public string CompareBtn { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("Is List Compared")]
        public string IsListCompared { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("")]
        public string ListDescription { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Item list")]
        public List<RFQComparisonDetailViewModel> ItemList { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Compared list")]
        public List<RFQComparisonComparedListViewModel> ComparedList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TransactionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RFQComparisonViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TransactionDTO, RFQComparisonViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var frmDto = dto as TransactionDTO;
            var vm = Mapper<TransactionDTO, RFQComparisonViewModel>.Map(frmDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.ItemList = new List<RFQComparisonDetailViewModel>();
            vm.ComparedList = new List<RFQComparisonComparedListViewModel>();
            vm.HeadIID = frmDto.TransactionHead.HeadIID;
            vm.TransactionDate = frmDto.TransactionHead.TransactionDate.HasValue ? frmDto.TransactionHead.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Validity = frmDto.TransactionHead.DueDate.HasValue ? frmDto.TransactionHead.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Remarks = frmDto.TransactionHead.Description;

            vm.Quotations = new List<KeyValueViewModel>();

            foreach (var qtn in frmDto.TransactionHead.Quotations)
            {
                vm.Quotations.Add(new KeyValueViewModel()
                {
                    Key = qtn.Key.ToString(),
                    Value = qtn.Value.ToString()
                });
            }

            foreach(var list in frmDto.TransactionDetails)
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
            Mapper<RFQComparisonViewModel, TransactionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = new TransactionDTO();

            dto.TransactionHead = new TransactionHeadDTO();
            dto.TransactionHead.TransactionDate = string.IsNullOrEmpty(this.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(this.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
            dto.TransactionHead.DueDate = string.IsNullOrEmpty(this.Validity) ? (DateTime?)null : DateTime.ParseExact(this.Validity, dateFormat, CultureInfo.InvariantCulture);
            dto.TransactionHead.HeadIID = this.HeadIID;
            dto.TransactionHead.Description = this.Remarks;
            dto.TransactionHead.Quotations = new List<KeyValueDTO>();
            foreach (var qt in this.Quotations)
            {
                if (qt.Key != null)
                {
                    dto.TransactionHead.Quotations.Add(new KeyValueDTO()
                    {
                        Key = qt.Key.ToString(),
                        Value = qt.Value.ToString()
                    });
                }
            }

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
