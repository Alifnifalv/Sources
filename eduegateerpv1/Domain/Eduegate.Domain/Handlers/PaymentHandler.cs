using Eduegate.Framework;

namespace Eduegate.Domain.Handlers
{
    public class PaymentHandler
    {
        CallContext _callContext;
        Eduegate.Domain.Repository.OrderRepository orderRepository = new Eduegate.Domain.Repository.OrderRepository();

        public static PaymentHandler Handler(CallContext _callContext)
        {
            var handler = new PaymentHandler();
            handler._callContext = _callContext;
            return handler;
        }

        public decimal HandleVoucherAmount(long transactionHeadID, decimal cartAmount, decimal voucherAmount)
        {
            decimal remainingAmount = cartAmount;

            //update data in TransactionHeadEntitlementMap
            if (cartAmount > voucherAmount)
            {
                cartAmount = cartAmount - voucherAmount;
                remainingAmount = cartAmount;
                //save in table 1 entries -- 1)entitlement(cartamount) 
                if (voucherAmount != 0)
                {
                    //save in table 1 entries   2) voucher (voucher amt)
                    orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Voucher, voucherAmount);
                }

                voucherAmount = 0;
            }

            if (cartAmount == voucherAmount)
            {
                //save in table 1 entry for voucher 
                remainingAmount = 0;
                orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Voucher, voucherAmount);
                voucherAmount = 0;
            }

            if (cartAmount < voucherAmount)
            {
                voucherAmount = voucherAmount - cartAmount;
                remainingAmount = 0;
                orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Voucher, cartAmount);
                //save in table 1 entry for voucher from cartamount
            }

            return remainingAmount;
        }

        //public decimal HandleRedeemLoyalty(long transactionHeadID, decimal cartAmount, decimal loyaltyAmount)
        //{
        //    decimal remainingAmount = cartAmount;

        //    //update data in TransactionHeadEntitlementMap
        //    if (cartAmount > loyaltyAmount)
        //    {
        //        cartAmount = cartAmount - loyaltyAmount;
        //        remainingAmount = cartAmount;
        //        //save in table 1 entries -- 1)entitlement(cartamount) 
        //        if (loyaltyAmount != 0)
        //        {
        //            //save in table 1 entries   2) voucher (voucher amt)
        //            orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Loyalty, loyaltyAmount);
        //        }

        //        loyaltyAmount = 0;
        //    }

        //    if (cartAmount == loyaltyAmount)
        //    {
        //        //save in table 1 entry for voucher 
        //        remainingAmount = 0;
        //        orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Loyalty, loyaltyAmount);
        //        loyaltyAmount = 0;
        //    }

        //    if (cartAmount < loyaltyAmount)
        //    {
        //        remainingAmount = 0;
        //        orderRepository.AddTransactionHeadEntitlementMap(transactionHeadID, Framework.Payment.EntitlementType.Loyalty, cartAmount);
        //        //save in table 1 entry for voucher from cartamount
        //    }

        //    return remainingAmount;
        //}
    }
}
