using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers.Transactions
{
    public class TransactionMapper : IDTOEntityMapper<TransactionHeadDTO, TransactionHead>
    {
        private CallContext _context;

        public static TransactionMapper Mapper(CallContext context)
        {
            var mapper = new TransactionMapper();
            mapper._context = context;
            return mapper;
        }

        public TransactionHeadDTO ToDTO(TransactionHead entity)
        {
            throw new NotImplementedException();
        }

        public TransactionHead ToEntity(TransactionHeadDTO dto)
        {
            throw new NotImplementedException();
        }

        public TransactionHead ToEntity(TransactionDTO transaction, Eduegate.Services.Contracts.Enums.TransactionStatus status = Services.Contracts.Enums.TransactionStatus.New) //it's handled only branch transfer now
        {
            var transactionHead = new TransactionHead()
            {
                DocumentTypeID = transaction.TransactionHead.DocumentTypeID,
                CompanyID = transaction.TransactionHead.CompanyID.HasValue ? transaction.TransactionHead.CompanyID :
                    (_context.IsNotNull() ? (_context.CompanyID.HasValue && _context.CompanyID != default(int) ? _context.CompanyID : (int?)null) : null),
                CurrencyID = new Domain.Setting.SettingBL(_context).GetSettingValue<int>("DEFAULT_CURRENCY_ID", 61),
                TransactionDate = DateTime.Now,
                BranchID = transaction.TransactionHead.BranchID,
                ToBranchID = transaction.TransactionHead.ToBranchID,
                TransactionStatusID = (byte)status,
                TransactionNo = !string.IsNullOrEmpty(transaction.TransactionHead.TransactionNo) ? transaction.TransactionHead.TransactionNo :
                new MutualBL(_context).GetAndSaveNextTransactionNumber(Convert.ToInt32(transaction.TransactionHead.DocumentTypeID)),
                TransactionDetails = new List<TransactionDetail>(),
                CreatedBy = transaction.TransactionHead.HeadIID == 0 ? transaction.TransactionHead.CreatedBy : int.Parse(_context.LoginID.Value.ToString()),
                UpdatedBy = int.Parse(_context.LoginID.Value.ToString()),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ReferenceHeadID = transaction.TransactionHead.ReferenceHeadID,
            };

            foreach(var detail in transaction.TransactionDetails) {
                transactionHead.TransactionDetails.Add(new TransactionDetail()
                {
                    Amount = detail.Amount,
                    DiscountPercentage = detail.DiscountPercentage,
                    DetailIID = detail.DetailIID,
                    HeadID = detail.HeadID,
                    ProductID = detail.ProductID,
                    ProductSKUMapID = detail.ProductSKUMapID,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    UnitID = detail.UnitID
                });
            }

            return transactionHead;
        }

        public TransactionDTO ToDTOFromCartItem(DataTable cartItemList, long toBranchID, long sysBlockedBranchID, int sysBlockedDocID)
        {
            if (cartItemList.IsNotNull())
            {
                var transactionDTO = new TransactionDTO();
                transactionDTO.TransactionHead = new TransactionHeadDTO();
                transactionDTO.TransactionDetails = new List<TransactionDetailDTO>();

                if (cartItemList.IsNotNull())
                {
                    transactionDTO.TransactionHead.HeadIID = 0;
                    transactionDTO.TransactionHead.DocumentTypeID = sysBlockedDocID;
                    transactionDTO.TransactionHead.BranchID = toBranchID;
                    transactionDTO.TransactionHead.ToBranchID = sysBlockedBranchID;
                    transactionDTO.TransactionHead.TransactionNo = "";
                    transactionDTO.TransactionHead.TransactionDate = DateTime.Now;
                    transactionDTO.TransactionHead.IsShipment = false;
                    transactionDTO.TransactionHead.CreatedBy = null;
                    transactionDTO.TransactionHead.UpdatedBy = null;
                    transactionDTO.TransactionHead.ReferenceHeadID = null;
                }

                if (cartItemList.IsNotNull() && cartItemList.Rows.Count > 0)
                {
                    foreach (DataRow transactionDetail in cartItemList.Rows)
                    {
                        if (Convert.ToInt64(transactionDetail["SKUID"]) > 0)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.DetailIID = 0;
                            transactionDetailDTO.ProductSKUMapID = Convert.ToInt64(transactionDetail["SKUID"]);
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail["Quantity"]);
                            transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.CreatedBy = null;
                            transactionDetailDTO.UpdatedBy = null;
                            transactionDTO.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                return transactionDTO;
            }
            else
            {
                return new TransactionDTO();
            }
        }
    }
}
