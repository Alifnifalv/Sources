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

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StockUpdation", "CRUDModel.ViewModel")]
    [DisplayName("Stock Updation")]
    public class StockUpdationViewModel : BaseMasterViewModel
    {
        public StockUpdationViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            TransactionDate = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            DocumentStatus = new KeyValueViewModel { Key = "1", Value = "Draft" };
            StockUpdationDetail = new List<StockUpdationDetailViewModel>() { new StockUpdationDetailViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("Physical Stock")]
        [DataPicker("PhysicalStockEntryPick")]
        public string PhysicalVerTransNo { get; set; }

        public long ReferenceHeadID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction No.")]
        public string TransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        public long HeadIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Physical stock verified date")]
        public string PhysicalStockPostedDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Physical stock verified by")]
        public string PhysicalStockVerfiedBy { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2,"disabled")]
        [Select2("Branch", "Numeric", false)]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        public KeyValueViewModel Branch { get; set; }
        public long? BranchID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public string TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Employee")]
        [Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public byte? TransactionStatusID { get; set; }


        public byte? CurrentStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillDiffDatasOnly(CRUDModel.ViewModel)'")]
        [DisplayName("Fill only diff Quantity items")]
        public string FillDiffDatasOnly { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Comments")]
        //public string PostedComments { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Stock Updation Detail")]
        public List<StockUpdationDetailViewModel> StockUpdationDetail { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StockVerificationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StockUpdationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StockVerificationDTO, StockUpdationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var frmDto = dto as StockVerificationDTO;
            var vm = Mapper<StockVerificationDTO, StockUpdationViewModel>.Map(frmDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.StockUpdationDetail = new List<StockUpdationDetailViewModel>();
            vm.TransactionDate = frmDto.TransactionDate.HasValue ? frmDto.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.CurrentStatusID = frmDto.CurrentStatusID;

            foreach (var detailList in frmDto.StockVerificationMap)
            {
                vm.StockUpdationDetail.Add(new StockUpdationDetailViewModel()
                {
                    DetailIID = detailList.DetailIID,
                    HeadID = frmDto.HeadIID,
                    ProductID = detailList.ProductID,
                    ProductSKU = new KeyValueViewModel { Key = detailList.ProductSKUMapID.ToString(), Value = detailList.ProductSKU.Value },
                    Remark = detailList.Remark,
                    AvailableQuantity = detailList.AvailableQuantity,
                    PhysicalQuantity = detailList.PhysicalQuantity,
                    DifferenceQuantity = detailList.DifferQuantity,
                    BookStock = detailList.BookStock,
                    ProductSKUMapID = detailList.ProductSKUMapID,
                    Description = detailList.Description,
                    CorrectedQuantity = detailList.Quantity,
                    CreatedBy = (int?)detailList.CreatedBy,
                    UpdatedBy = (int?)detailList.UpdatedBy,
                    CreatedDate = detailList.CreatedDate,
                    UpdatedDate = detailList.UpdatedDate,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Mapper<StockUpdationViewModel, StockVerificationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = new StockVerificationDTO();

            dto.HeadIID = this.HeadIID;
            dto.TransactionNo = this.TransactionNo;
            dto.BranchID = this.Branch != null && !string.IsNullOrEmpty(this.Branch.Key) ? long.Parse(this.Branch.Key) : (long?)null;
            dto.EmployeeID = this.Employee != null && !string.IsNullOrEmpty(this.Employee.Key) ? long.Parse(this.Employee.Key) : (long?)null;
            dto.TransactionDate = string.IsNullOrEmpty(this.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(this.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
            dto.CurrentStatusID = this.CurrentStatusID;
            dto.DocStatusID = this.DocumentStatus != null && !string.IsNullOrEmpty(this.DocumentStatus.Key) ? byte.Parse(this.DocumentStatus.Key) : (byte?)null;
            dto.StockVerificationMap = new List<StockVerificationMapDTO>();

            foreach (var mapData in this.StockUpdationDetail)
            {
                if (mapData.ProductSKU != null && mapData.ProductSKU.Key != null)
                {
                    dto.StockVerificationMap.Add(new StockVerificationMapDTO()
                    {
                        ProductID = mapData.ProductID,
                        DetailIID = mapData.DetailIID,
                        StockVerficationDetailIID = mapData.StockVerficationDetailIID,
                        ProductSKUMapID = long.Parse(mapData.ProductSKU.Key),
                        Description = mapData.Description,
                        PhysicalQuantity = mapData.PhysicalQuantity,
                        Remark = mapData.Remark,
                        BookStock = mapData.BookStock,
                        CorrectedQuantity = mapData.CorrectedQuantity,
                        DifferQuantity = mapData.DifferenceQuantity,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StockVerificationDTO>(jsonString);
        }


        //to fill details via advance search
        public static StockUpdationViewModel FromStockVerificationVM(StockVerificationDTO dto)
        {
            var vm = new StockUpdationViewModel();
            vm.StockUpdationDetail = new List<StockUpdationDetailViewModel>();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (dto != null)
            {
                vm.EmployeeID = dto.EmployeeID;
                //vm.HeadIID = dto.HeadIID;
                vm.PhysicalStockPostedDate = dto.TransactionDate.HasValue ? dto.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                vm.BranchID = dto.BranchID;
                vm.PhysicalStockVerfiedBy = dto.PhysicalStockVerfiedBy;
                vm.PhysicalVerTransNo = dto.TransactionNo;
                vm.Branch = new KeyValueViewModel { Key = dto.BranchID.ToString(), Value = dto.Branch.Value };
                //vm.Employee = new KeyValueViewModel { Key = dto.Employee.ToString(), Value = dto.Employee.Value };
                if (dto.StockVerificationMap.Count > 0)
                {
                    foreach (var detailDat in dto.StockVerificationMap)
                    {
                        vm.StockUpdationDetail.Add(new StockUpdationDetailViewModel()
                        {
                            ProductID = detailDat.ProductID,
                            StockVerficationDetailIID = detailDat.DetailIID,
                            ProductSKU = new KeyValueViewModel { Key = detailDat.ProductSKU.Key.ToString(), Value = detailDat.ProductSKU.Value },
                            Description = detailDat.Description,
                            AvailableQuantity = detailDat.BookStock,
                            BookStock = detailDat.BookStock,
                            PhysicalQuantity = detailDat.PhysicalQuantity,
                            DifferenceQuantity = detailDat.PhysicalQuantity - detailDat.BookStock,
                            Remark = detailDat.Remark,
                        });
                    }
                }
            }
            return vm;
        }
    }
}
