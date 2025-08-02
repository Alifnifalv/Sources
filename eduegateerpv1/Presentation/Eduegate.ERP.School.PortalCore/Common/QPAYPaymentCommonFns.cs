using System.Text;
using System;
using System.Security.Cryptography;
using System.Linq;

namespace Eduegate.ERP.School.PortalCore.Common
{
    public static class QPAYPaymentCommonFns
    {
        public static string GenerateSecureHash(String secretKey, String extraField,
            String merchantId, String pun, String amount, String description,
            String transactionRequestDate, String bankId, String nationalId,
            String lan,string currencyCode)
        {
            StringBuilder hashBuilder = new StringBuilder();
            StringBuilder requestBuilder = new StringBuilder();
            hashBuilder.Append(secretKey);
            requestBuilder.Append("Action=0&");
            hashBuilder.Append("0");
            requestBuilder.Append("Amount=" + amount + "&");
            hashBuilder.Append(amount);
            requestBuilder.Append("BankID=" + bankId + "&");
            hashBuilder.Append(bankId);
            requestBuilder.Append("CurrencyCode=" + currencyCode + "&");
            hashBuilder.Append(currencyCode);
            requestBuilder.Append("ExtraFields_f14=" + extraField + "&");
            hashBuilder.Append(extraField);
            requestBuilder.Append("Lang=" + lan + "&");
            hashBuilder.Append(lan);
            requestBuilder.Append("MerchantID=" + merchantId + "&");
            hashBuilder.Append(merchantId);
            requestBuilder.Append("MerchantModuleSessionID=" + pun + "&");
            hashBuilder.Append(pun);
            requestBuilder.Append("NationalID=" + nationalId + "&");
            hashBuilder.Append(nationalId);
            requestBuilder.Append("PUN=" + pun + "&");
            hashBuilder.Append(pun);
            requestBuilder.Append("PaymentDescription=" + description +
           "&"); hashBuilder.Append(description);
            requestBuilder.Append("Quantity=1&"); hashBuilder.Append("1");
            requestBuilder.Append("TransactionRequestDate=" + transactionRequestDate + "&");
            hashBuilder.Append(transactionRequestDate);
            String hashBuilderString = hashBuilder.ToString();
            Console.WriteLine("hashBuilderString: " + hashBuilderString);
            String secureHash = ComputeSha256Hash(hashBuilderString);
            Console.WriteLine("SecureHash: " + secureHash);
            requestBuilder.Append("SecureHash=" + secureHash);
            Console.WriteLine("Payment Request: " + requestBuilder);
            return secureHash;
        }
        public static string GenerateHash(string data)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                var hash = sha256.ComputeHash(bytes);
                return string.Concat(hash.Select(b => b.ToString("x2")));
            }
        }

        static string ComputeSha256Hash(string  
        rawData)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
