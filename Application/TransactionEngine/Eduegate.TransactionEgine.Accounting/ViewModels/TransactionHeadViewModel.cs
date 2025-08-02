using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class TransactionHeadViewModel
    {
        public long HeadIID { get; set; }

        public Nullable<int> DocumentTypeID { get; set; }

        public string TransactionNo { get; set; }

        public string Description { get; set; }

        public Nullable<long> CustomerID { get; set; }

        public Nullable<long> SupplierID { get; set; }

        public Nullable<DateTime> TransactionDate { get; set; } 

        public Nullable<byte> TransactionStatusID { get; set; }

        public Nullable<DateTime> DueDate { get; set; }

        public Nullable<DateTime> DeliveryDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }

        public Nullable<int> CurrencyID { get; set; }
        
        public Nullable<int> CreatedBy { get; set; }

        public Nullable<int> UpdatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public Nullable<long> FeeReceiptID { get; set; }

        public Nullable<long> BranchID { get; set; }
        public Nullable<long> ToBranchID { get; set; }

        //public byte[] TimeStamps { get; set; }
        public Nullable<long> ReferenceHeadID { get; set; }
        public List<TransactionDetailViewModel> TransactionDetails { get; set; }

        public Nullable<decimal> DiscountAmount { get; set; }
        public int AccountTransactionProcessStatusId { get; set; }
        public List<TaxDetailViewModel> TaxDetails { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }

        public decimal? ForeignAmount { get; set; }
        public decimal? TotalLandingCost { get; set; }
        public decimal? LocalDiscount { get; set; }

     
        public static TransactionHeadViewModel FromDTO(TransactionHeadDTO dto)
        {
            Mapper<TransactionHeadDTO, TransactionHeadViewModel>.CreateMap();
            Mapper<TransactionDetailDTO, TransactionDetailViewModel>.CreateMap();
            Mapper<TransactionAllocationDTO, TransactionAllocationViewModel>.CreateMap();
            Mapper<TaxDetailsDTO, TaxDetailViewModel>.CreateMap();
            return Mapper<TransactionHeadDTO, TransactionHeadViewModel>.Map(dto);
        }

        public static List<TransactionHeadDTO> ToDTO(List<TransactionHeadViewModel> VMs)
        {
            Mapper<TransactionHeadViewModel, TransactionHeadDTO>.CreateMap();
            Mapper<TransactionDetailViewModel, TransactionDetailDTO>.CreateMap();
            Mapper<TransactionAllocationViewModel, TransactionAllocationDTO>.CreateMap();
            Mapper<TaxDetailViewModel, TaxDetailsDTO>.CreateMap();
            return Mapper<List<TransactionHeadViewModel>, List<TransactionHeadDTO>>.Map(VMs);
        }

        public static TransactionHeadViewModel FromDTO(AccountTransactionHeadDTO dto)
        {
            Mapper<AccountTransactionHeadDTO, TransactionHeadViewModel>.CreateMap();
            Mapper<AccountTransactionDetailsDTO, TransactionDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<TaxDetailsDTO, TaxDetailViewModel>.CreateMap();
            var mapper = Mapper<AccountTransactionHeadDTO, TransactionHeadViewModel>.Map(dto);
            mapper.HeadIID = dto.AccountTransactionHeadIID;
            mapper.TransactionNo = dto.TransactionNumber;
            mapper.Description = dto.Remarks;
            return mapper;
        }
    }
}
