using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;

namespace Eduegate.Domain
{
    public class WebsiteOrderBL
    {
        private CallContext _context;

        public WebsiteOrderBL(CallContext context)
        {
            _context = context;
        }

        public TransactionHeadDTO SaveWebsiteOrder(CheckoutPaymentDTO dto)
        {
            //add to transaction tables
            var settingDTO = new SettingRepository().GetSettingDetail("ONLINESALESDOCTTYPEID");
            var transactionNo = new MutualBL(_context).GetNextTransactionNumber(long.Parse(settingDTO.SettingValue));
            var transactionHead = new OrderRepository().SaveWebsiteOrder(dto, settingDTO.SettingValue, transactionNo, (long)_context.LoginID);
            var entity = new MutualBL(_context).UpdateLastTransactionNo(Convert.ToInt32(transactionHead.DocumentTypeID), transactionHead.TransactionNo);
            //quantity deduction
            var settingDTObranch = new SettingBL().GetSettingDetail("ONLINEBRANCHID");
            foreach (var item in transactionHead.TransactionDetails)
            {
                var check = new ProductCatalogRepository().reduceQuantity((long)item.ProductSKUMapID, (long)item.Quantity, long.Parse(settingDTObranch.SettingValue));
            }

            //voucher check
            if (!string.IsNullOrEmpty(dto.VoucherNo) && dto.VoucherAmount != 0)
            {
                var voucherCheck = new VoucherRepository().VoucherTransaction(dto);
            }

            //billing Address & delivery address
            if(string.IsNullOrEmpty(dto.SelectedBillingAddress))
            {
                //delivery address<>billing address
                var addresscheck = new AccountRepository().SaveWebsiteOrderAddress(long.Parse(dto.SelectedShippingAddress), false, (long)_context.LoginID,transactionHead.HeadIID);
            }
            else
            {
                var addresscheck = new AccountRepository().SaveWebsiteOrderAddress(long.Parse(dto.SelectedShippingAddress), true, (long)_context.LoginID, transactionHead.HeadIID);
            }

            return TransactionHeadMapper.Mapper(_context).ToDTO(transactionHead);
        }
    }
}
