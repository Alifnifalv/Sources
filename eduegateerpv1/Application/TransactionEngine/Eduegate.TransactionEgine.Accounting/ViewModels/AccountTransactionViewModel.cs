using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class AccountTransactionViewModel
    {
        public long? DocumentTypeID { get; set; }
        public long TransactionIID { get; set; }
        public string TransactionNumber { get; set; }
        public Nullable<long> AccountID { get; set; }
        public decimal? Amount { get; set; }
        public decimal? InclusiveTaxAmount { get; set; }
        public decimal? ExclusiveTaxAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public Nullable<bool> DebitOrCredit { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public string Description { get; set; }
        public int TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? DueDate { get; set; }

        public TransactionHeadAccountMapViewModel TransactionHeadAccountMap { get; set; }

        public static List<AccountTransactionsDTO> ConvertViewModelToDTO(List<AccountTransactionViewModel> viewModel)
        {
            Mapper<AccountTransactionViewModel, AccountTransactionsDTO>.CreateMap();
            Mapper<TransactionHeadAccountMapViewModel, TransactionHeadAccountMapDTO>.CreateMap();

            List<AccountTransactionsDTO> dtos = viewModel
                .Select(accountTransactionViewModel =>
                {
                    var dto = Mapper<AccountTransactionViewModel, AccountTransactionsDTO>.Map(accountTransactionViewModel);

                    dto.TransactionHeadAccountMap = Mapper<TransactionHeadAccountMapViewModel, TransactionHeadAccountMapDTO>.Map(accountTransactionViewModel.TransactionHeadAccountMap);

                    return dto;
                })
                .ToList();

            return dtos;
        }


        public static List<AccountTransactionsDTO> ConvertViewModelToDTO_Asset(List<AccountTransactionViewModel> viewModel)
        {
            Mapper<AccountTransactionViewModel, AccountTransactionsDTO>.CreateMap();
            //Mapper<TransactionHeadAccountMapViewModel, TransactionHeadAccountMapDTO>.CreateMap();
            return Mapper<List<AccountTransactionViewModel>, List<AccountTransactionsDTO>>.Map(viewModel);
        }
    }

    public class TransactionHeadAccountMapViewModel
    {       
        public long TransactionHeadAccountMapIID { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public Nullable<long> AccountTransactionID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }




}
