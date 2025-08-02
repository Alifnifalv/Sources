using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class SalesVoucherViewModel : BaseMasterViewModel
    {
        public SalesVoucherViewModel()
        {
            DetailViewModel = new List<SalesVoucherDetailViewModel>() { new SalesVoucherDetailViewModel() };
            MasterViewModel = new SalesVoucherMasterViewModel();
        }

        public List<SalesVoucherDetailViewModel> DetailViewModel { get; set; }
        public SalesVoucherMasterViewModel MasterViewModel { get; set; }

        public override string AsDTOString(BaseMasterDTO dto)
        {
            return base.AsDTOString(dto);
        }

        public override BaseMasterDTO ToDTO()
        {
            var dto = new AccountTransactionHeadDTO();
            dto.AccountTransactionHeadIID = this.MasterViewModel.TransactionHeadIID;
            dto.CurrencyID = this.MasterViewModel.Currency == null ? (int?)null : Convert.ToInt32(this.MasterViewModel.Currency.Key);
            dto.AccountID = this.MasterViewModel.GLAccount == null ? (long?)null : Convert.ToInt64(this.MasterViewModel.GLAccount.Key);
            dto.DocumentStatusID = Convert.ToInt64(this.MasterViewModel.DocumentStatus.Key);
            dto.Remarks = this.MasterViewModel.Remarks;
            dto.TransactionDate = Convert.ToDateTime(this.MasterViewModel.TransactionDate);
            dto.TransactionNumber = this.MasterViewModel.TransactionNo;
            dto.Reference = this.MasterViewModel.Reference;
            dto.DiscountAmount = this.MasterViewModel.Discount;

            dto.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (var detail in this.DetailViewModel)
            {
                if (detail.Amount.HasValue)
                {
                    dto.AccountTransactionDetails.Add(new AccountTransactionDetailsDTO()
                    {
                        Description = detail.Description,
                        AccountTransactionDetailIID = detail.TransactionDetailID,
                        Amount = detail.Amount,
                        ReferenceRate = Convert.ToDecimal(detail.UnitPrice),
                        ReferenceQuantity = Convert.ToDecimal(detail.Quantity),
                        ExternalReference1 = detail.ExternalReference1,
                        ExternalReference2 = detail.ExternalReference2,
                        ExternalReference3 = detail.ExternalReference3,
                        DiscountAmount = detail.DiscountAmount
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountTransactionHeadDTO>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var headDto = dto as AccountTransactionHeadDTO;
            var salesVoucherVM = new SalesVoucherViewModel();
            salesVoucherVM.MasterViewModel = new SalesVoucherMasterViewModel()
            {
                GLAccount = new KeyValueViewModel() { Key = headDto.Account.Key, Value = headDto.Account.Value },
                TransactionHeadIID = headDto.AccountTransactionHeadIID,
                TransactionNo = headDto.TransactionNumber,
                DocumentTypeID = headDto.DocumentTypeID,
                //PaymentModeID = headDto.PaymentModeID,
                //CurrencyID = headDto.CurrencyID,
                //ExchangeRate = headDto.ExchangeRate,
                Remarks = headDto.Remarks,
                //Amount =headDto.Amount,
                //DocumentStatusID =headDto.DocumentStatusID,
                DocumentStatus = new KeyValueViewModel() { Key = headDto.DocumentStatus.Key, Value = headDto.DocumentStatus.Value },
                TransactionDate = !headDto.TransactionDate.HasValue ? string.Empty : headDto.TransactionDate.Value.ToString(new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat")),
                TransactionStatus = new KeyValueViewModel() { Key = headDto.TransactionStatus.Key, Value = headDto.TransactionStatus.Value },
                Reference = headDto.Reference,
                Discount = headDto.DiscountAmount,
            };

            salesVoucherVM.DetailViewModel = new List<SalesVoucherDetailViewModel>();

            foreach(var detailDTO in headDto.AccountTransactionDetails)
            {
                salesVoucherVM.DetailViewModel.Add(new SalesVoucherDetailViewModel()
                {
                    Amount = detailDTO.Amount,
                    Description = detailDTO.Description,
                    Quantity = Convert.ToDouble(detailDTO.ReferenceQuantity),
                    UnitPrice = Convert.ToDouble(detailDTO.ReferenceRate),
                    TransactionDetailID = detailDTO.AccountTransactionDetailIID,
                    DiscountAmount = detailDTO.DiscountAmount,
                });
            }

            return salesVoucherVM;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalesVoucherViewModel>(jsonString);
        }
    }
}
