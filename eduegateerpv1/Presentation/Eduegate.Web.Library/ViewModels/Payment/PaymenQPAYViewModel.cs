using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Payments;
using Newtonsoft.Json;
using System;

namespace Eduegate.Web.Library.ViewModels.Payment
{
    public class PaymenQPAYViewModel : BaseMasterViewModel
    {

        public String secretKey { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public string transactionRequestDate { get; set; }
        public string ActionID { get; set; }
        public int? Amount { get; set; }
        public decimal? ActualAmount { get; set; }
        public string BankID { get; set; }

        public string NationalID { get; set; }
        public string PUN { get; set; }
        public string MerchantModuleSessionID { get; set; }
        public string MerchantID { get; set; }
        public string Lang { get; set; }
        public string CurrencyCode { get; set; }
        public string ExtraFields_f14 { get; set; }
        public int Quantity { get; set; }

        public string MerchantGatewayUrl { get; set; }

        public string PaymentDescription { get; set; }
        public string SecureHash { get; set; }

        public string QPayCardTypeID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PaymentQPAYDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PaymenQPAYViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PaymentQPAYDTO, PaymenQPAYViewModel>.CreateMap();
            var vm = Mapper<PaymentQPAYDTO, PaymenQPAYViewModel>.Map(dto as PaymentQPAYDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<PaymenQPAYViewModel, PaymentQPAYDTO>.CreateMap();
            var dto = Mapper<PaymenQPAYViewModel, PaymentQPAYDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PaymentQPAYDTO>(jsonString);
        }
    }
}