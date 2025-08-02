using System;
using System.Collections.Generic;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public partial class AssetTransactionHeadViewModel
    {
        public long HeadIID { get; set; }

        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }

        public string TransactionNo { get; set; }

        public int? DocumentTypeID { get; set; }

        public DateTime? EntryDate { get; set; }

        public string Remarks { get; set; }

        public long? DocumentStatusID { get; set; }

        public byte? ProcessingStatusID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public bool? DebitOrCredit { get; set; }

        public long? AssetID { get; set; }

        public long? BranchID { get; set; }

        public long? ToBranchID { get; set; }

        public int? CompanyID { get; set; }

        public long? BatchID { get; set; }

        public decimal? CostPrice { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<AssetTransactionDetailViewModel> AssetTransactionDetails { get; set; }

        public static AssetTransactionHeadViewModel FromDTO(AssetTransactionHeadDTO dto)
        {
            Mapper<AssetTransactionHeadDTO, AssetTransactionHeadViewModel>.CreateMap();
            Mapper<AssetTransactionDetailsDTO, AssetTransactionDetailViewModel>.CreateMap();
            var vm = Mapper<AssetTransactionHeadDTO, AssetTransactionHeadViewModel>.Map(dto);

            vm.DocumentReferenceTypeID = (Eduegate.Framework.Enums.DocumentReferenceTypes)(int)dto.DocumentReferenceTypeID;

            return vm;
        }

        public static List<AssetTransactionHeadDTO> ToDTO(List<AssetTransactionHeadViewModel> VMs)
        {
            Mapper<AssetTransactionHeadViewModel, AssetTransactionHeadDTO>.CreateMap();
            Mapper<AssetTransactionDetailViewModel, AssetTransactionDetailsDTO>.CreateMap();
            return Mapper<List<AssetTransactionHeadViewModel>, List<AssetTransactionHeadDTO>>.Map(VMs);
        }

    }
}