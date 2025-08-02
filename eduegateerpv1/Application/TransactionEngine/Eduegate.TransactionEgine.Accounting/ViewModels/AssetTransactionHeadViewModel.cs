using System;
using System.Collections.Generic;
//using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public partial class AssetTransactionHeadViewModel
    {
        public long HeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<byte> ProcessingStatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public List<AssetTransactionDetailViewModel> AssetTransactionDetails { get; set; }

        public Nullable<long> AccountID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> DebitOrCredit { get; set; }


        public static AssetTransactionHeadViewModel FromDTO(AssetTransactionHeadDTO dto)
        {
            Mapper<AssetTransactionHeadDTO, AssetTransactionHeadViewModel>.CreateMap();
            Mapper<AssetTransactionDetailsDTO, AssetTransactionDetailViewModel>.CreateMap();
            return Mapper<AssetTransactionHeadDTO, AssetTransactionHeadViewModel>.Map(dto);
        }

        public static List<AssetTransactionHeadDTO> ToDTO(List<AssetTransactionHeadViewModel> VMs)
        {
            Mapper<AssetTransactionHeadViewModel, AssetTransactionHeadDTO>.CreateMap();
            Mapper<AssetTransactionDetailViewModel, AssetTransactionDetailsDTO>.CreateMap();
            return Mapper<List<AssetTransactionHeadViewModel>, List<AssetTransactionHeadDTO>>.Map(VMs);
        }

    }
}
