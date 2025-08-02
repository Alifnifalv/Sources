using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetManualEntry", "CRUDModel.ViewModel")]
    [DisplayName("Asset Manual Entry")]
    public class AssetManualEntryViewModel : BaseMasterViewModel
    {
        public AssetManualEntryViewModel()
        {
            DocumentReferenceTypeID = DocumentReferenceTypes.AssetEntryManual;
            AssetSerialEntry = new AssetEntrySerialViewModel();
            //DepreciationInformations = new AssetDepreciationInfoViewModel();
            //TechnicalInformations = new AssetTechnicalInfoViewModel();
            AssetLocations = new AssetLocationViewModel();
            AssetReferences = new AssetReferenceViewModel();
            IsRequiredSerialNumber = false;
            IsTransactionCompleted = false;
        }

        public long AssetEntryHeadIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("Asset", "String", false, "AssetChanges(CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled='CRUDModel.ViewModel.AssetEntryHeadIID > 0'")]
        [CustomDisplay("Asset")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Assets", "LookUps.Assets")]
        //[LookUp("LookUps.Assets")]
        public KeyValueViewModel Asset { get; set; }
        public long? AssetID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AssetDescription")]
        public string AssetDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AssetPrefix")]
        public string AssetPrefix { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AssetLastSequenceNumber")]
        public long? AssetLastSequenceNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CategoryName")]
        public string AssetCategoryName { get; set; }
        public long? AssetCategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CategoryDepreciationRate")]
        public decimal? AssetCategoryDepreciationRate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Branch", "String", false)]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        public KeyValueViewModel Branch { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Rate")]
        public decimal? Amount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CostAmount(Net)")]
        public decimal? CostAmount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("User")]
        public string UserName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("TransactionNumber")]
        public string TransactionNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetSerialEntries", "AssetSerialEntries")]
        [CustomDisplay("SerialEntries")]
        public AssetEntrySerialViewModel AssetSerialEntry { get; set; }


        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "DepreciationInformations", "DepreciationInformations")]
        //[CustomDisplay("Depreciation Info")]
        //public AssetDepreciationInfoViewModel DepreciationInformations { get; set; }


        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "TechnicalInformations", "TechnicalInformations")]
        //[CustomDisplay("Technical Information")]
        //public AssetTechnicalInfoViewModel TechnicalInformations { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetLocations", "AssetLocations")]
        [CustomDisplay("Location")]
        public AssetLocationViewModel AssetLocations { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetReferences", "AssetReferences")]
        [CustomDisplay("ReferenceAndNotes")]
        public AssetReferenceViewModel AssetReferences { get; set; }

        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }

        public KeyValueViewModel DocumentType { get; set; }
        public int? DocumentTypeID { get; set; }

        public string EntryDateString { get; set; }

        public byte? ProcessingStatusID { get; set; }

        public bool? IsRequiredSerialNumber { get; set; }

        public bool IsTransactionCompleted { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssetTransactionHeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetManualEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AssetTransactionHeadDTO, AssetManualEntryViewModel>.CreateMap();
            var assetDTO = dto as AssetTransactionHeadDTO;
            var vm = Mapper<AssetTransactionHeadDTO, AssetManualEntryViewModel>.Map(dto as AssetTransactionHeadDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.AssetEntryHeadIID = assetDTO.HeadIID;
            vm.TransactionNo = assetDTO.TransactionNo;
            vm.EntryDateString = assetDTO.EntryDate.HasValue ? assetDTO.EntryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Branch = assetDTO.BranchID.HasValue ? new KeyValueViewModel()
            {
                Key = assetDTO.BranchID.ToString(),
                Value = assetDTO.BranchName
            } : new KeyValueViewModel();
            vm.DocumentReferenceTypeID = assetDTO.DocumentReferenceTypeID;
            vm.DocumentType = assetDTO.DocumentTypeID.HasValue ? new KeyValueViewModel()
            {
                Key = assetDTO.DocumentTypeID.ToString(),
                Value = assetDTO.DocumentTypeName,
            } : new KeyValueViewModel();
            vm.DocumentStatus = assetDTO.DocumentStatusID.HasValue ? new KeyValueViewModel()
            {
                Key = assetDTO.DocumentStatusID.ToString(),
                Value = assetDTO.DocumentStatusName
            } : new KeyValueViewModel();
            vm.Asset = assetDTO.AssetID.HasValue ? new KeyValueViewModel()
            {
                Key = assetDTO.AssetID.ToString(),
                Value = assetDTO.AssetCode
            } : new KeyValueViewModel();
            vm.AssetDescription = assetDTO.AssetName;
            vm.ProcessingStatusID = assetDTO.ProcessingStatusID;
            vm.IsRequiredSerialNumber = assetDTO.IsRequiredSerialNumber;
            vm.IsTransactionCompleted = assetDTO.IsTransactionCompleted;
            vm.AssetPrefix = assetDTO.AssetPrefix;
            vm.AssetLastSequenceNumber = assetDTO.AssetLastSequenceNumber;
            vm.AssetCategoryID = assetDTO.AssetCategoryID;
            vm.AssetCategoryName = assetDTO.AssetCategoryName;
            vm.AssetCategoryDepreciationRate = assetDTO.AssetCategoryDepreciationRate;

            vm.AssetLocations = new AssetLocationViewModel()
            {
                Department = assetDTO.DepartmentID.HasValue ? assetDTO.DepartmentID.ToString() : null,
                AssetLocation = assetDTO.AssetLocation,
                SubLocation = assetDTO.SubLocation,
                AssetFloor = assetDTO.AssetFloor,
                RoomNumber = assetDTO.RoomNumber,
            };

            vm.AssetReferences = new AssetReferenceViewModel()
            {
                Reference = assetDTO.Reference
            };            

            var transDetailDTO = assetDTO.AssetTransactionDetails?.FirstOrDefault();
            if (transDetailDTO != null)
            {
                vm.Amount = transDetailDTO.Amount;
                vm.CostAmount = transDetailDTO.CostAmount;

                var serialEntries = new List<AssetSerialEntryGridViewModel>
                {
                    new AssetSerialEntryGridViewModel()
                    {
                        AssetTransactionSerialMapIID = 0,
                        IsRowDisabled = true
                    }
                };
                var sLNo = 0;
                foreach (var serialEntry in transDetailDTO.AssetTransactionSerialMaps)
                {
                    sLNo += 1;
                    serialEntries.Add(new AssetSerialEntryGridViewModel()
                    {
                        SLNo = sLNo,
                        AssetTransactionSerialMapIID = serialEntry.AssetTransactionSerialMapIID,
                        AssetID = serialEntry.AssetID,
                        FirstUseDateString = serialEntry.DateOfFirstUse.HasValue ? serialEntry.DateOfFirstUse.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        AssetSequenceCode = serialEntry.AssetSequenceCode,
                        AssetSerialNumber = serialEntry.SerialNumber,
                        AssetTag = serialEntry.AssetTag,
                        CostPrice = serialEntry.CostPrice,
                        ExpectedLife = serialEntry.ExpectedLife ?? 0,
                        DepreciationRate = serialEntry.DepreciationRate,
                        ExpectedScrapValue = serialEntry.ExpectedScrapValue ?? 0,
                        AccumulatedDepreciationAmount = serialEntry.AccumulatedDepreciationAmount ?? 0,
                        BillNumber = serialEntry.BillNumber,
                        BillDateString = serialEntry.BillDate.HasValue ? serialEntry.BillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        Supplier = serialEntry.SupplierID.HasValue ? new KeyValueViewModel()
                        {
                            Key = serialEntry.SupplierID.ToString(),
                            Value = serialEntry.SupplierName
                        } : new KeyValueViewModel(),
                        IsRowDisabled = false,
                        CreatedBy = serialEntry.CreatedBy,
                        UpdatedBy = serialEntry.UpdatedBy,
                        CreatedDate = serialEntry.CreatedDate,
                        UpdatedDate = serialEntry.UpdatedDate,
                    });
                }

                vm.AssetSerialEntry = new AssetEntrySerialViewModel()
                {
                    TransactionDetailIID = transDetailDTO.DetailIID,
                    AssetID = transDetailDTO.AssetID,
                    Quantity = transDetailDTO.Quantity,
                    CreatedBy = assetDTO.CreatedBy,
                    CreatedDate = assetDTO.CreatedDate,
                    UpdatedBy = assetDTO.UpdatedBy,
                    UpdatedDate = assetDTO.UpdatedDate,
                    SerialEntryGrids = serialEntries,
                };
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssetManualEntryViewModel, AssetTransactionHeadDTO>.CreateMap();
            var dto = Mapper<AssetManualEntryViewModel, AssetTransactionHeadDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.HeadIID = this.AssetEntryHeadIID;
            dto.OldTransactionHeadID = this.AssetEntryHeadIID;
            dto.TransactionNo = this.TransactionNo;
            dto.EntryDate = string.IsNullOrEmpty(this.EntryDateString) ? DateTime.Now : DateTime.ParseExact(this.EntryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.BranchID = this.Branch == null || string.IsNullOrEmpty(this.Branch.Key) ? (long?)null : long.Parse(this.Branch.Key);
            dto.DocumentTypeID = this.DocumentType == null || string.IsNullOrEmpty(this.DocumentType.Key) ? 200 : int.Parse(this.DocumentType.Key);
            dto.DocumentStatusID = this.DocumentStatus == null || string.IsNullOrEmpty(this.DocumentStatus.Key) ? (byte?)null : byte.Parse(this.DocumentStatus.Key);
            dto.AssetID = this.Asset == null || string.IsNullOrEmpty(this.Asset.Key) ? (long?)null : long.Parse(this.Asset.Key);
            dto.ProcessingStatusID = this.ProcessingStatusID;
            dto.IsRequiredSerialNumber = this.IsRequiredSerialNumber;
            dto.IsTransactionCompleted = this.IsTransactionCompleted;

            dto.DepartmentID = string.IsNullOrEmpty(this.AssetLocations.Department) ? (long?)null : long.Parse(this.AssetLocations.Department);
            dto.AssetLocation = this.AssetLocations.AssetLocation;
            dto.SubLocation = this.AssetLocations.SubLocation;
            dto.AssetFloor = this.AssetLocations.AssetFloor;
            dto.RoomNumber = this.AssetLocations.RoomNumber;
            
            dto.Reference = this.AssetReferences.Reference;

            dto.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
            var assetTransSerialMaps = new List<AssetTransactionSerialMapDTO>();
            foreach (var serialMap in this.AssetSerialEntry.SerialEntryGrids)
            {
                if (serialMap.DepreciationRate.HasValue)
                {
                    assetTransSerialMaps.Add(new AssetTransactionSerialMapDTO()
                    {
                        AssetTransactionSerialMapIID = serialMap.AssetTransactionSerialMapIID,
                        TransactionDetailID = this.AssetSerialEntry.TransactionDetailIID,
                        AssetID = dto.AssetID,
                        AssetSequenceCode = serialMap.AssetSequenceCode,
                        DateOfFirstUse = string.IsNullOrEmpty(serialMap.FirstUseDateString) ? (DateTime?)null : DateTime.ParseExact(serialMap.FirstUseDateString, dateFormat, CultureInfo.InvariantCulture),
                        SerialNumber = serialMap.AssetSerialNumber,
                        AssetTag = serialMap.AssetTag,
                        CostPrice = serialMap.CostPrice,
                        ExpectedLife = serialMap.ExpectedLife,
                        DepreciationRate = serialMap.DepreciationRate,
                        ExpectedScrapValue = serialMap.ExpectedScrapValue,
                        AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                        SupplierID = serialMap.Supplier == null || string.IsNullOrEmpty(serialMap.Supplier.Key) ? (long?)null : long.Parse(serialMap.Supplier.Key),
                        BillNumber = serialMap.BillNumber,
                        BillDate = string.IsNullOrEmpty(serialMap.BillDateString) ? (DateTime?)null : DateTime.ParseExact(serialMap.BillDateString, dateFormat, CultureInfo.InvariantCulture),
                        CreatedBy = serialMap.CreatedBy,
                        UpdatedBy = serialMap.UpdatedBy,
                        CreatedDate = serialMap.CreatedDate,
                        UpdatedDate = serialMap.UpdatedDate,
                    });
                }
            }

            dto.AssetTransactionDetails.Add(new AssetTransactionDetailsDTO()
            {
                DetailIID = this.AssetSerialEntry.TransactionDetailIID,
                AssetID = dto.AssetID,
                Quantity = this.AssetSerialEntry.Quantity,
                Amount = this.Amount,
                CostAmount = this.CostAmount,
                AssetTransactionSerialMaps = assetTransSerialMaps,
                CreatedBy = this.AssetSerialEntry.CreatedBy,
                CreatedDate = this.AssetSerialEntry.CreatedDate,
                UpdatedBy = this.AssetSerialEntry.UpdatedBy,
                UpdatedDate = this.AssetSerialEntry.UpdatedDate,
            });

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetTransactionHeadDTO>(jsonString);
        }

    }
}