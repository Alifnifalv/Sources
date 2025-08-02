using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Payments;
using Newtonsoft.Json;
using System;

namespace Eduegate.Web.Library.ViewModels.Payment
{
    public class PaymentMasterVisaViewModel : BaseMasterViewModel
    {
        public long TrackIID { get; set; }

        public long TrackKey { get; set; }

        public long? CustomerID { get; set; }

        public long PaymentID { get; set; }

        public DateTime? InitOn { get; set; }

        public string MerchantID { get; set; }

        public decimal? PaymentAmount { get; set; }

        public int? VirtualAmount { get; set; }

        public string PaymentCurrency { get; set; }

        public DateTime? ResponseOn { get; set; }

        public string ResponseCode { get; set; }

        public string CodeDescription { get; set; }

        public string Message { get; set; }

        public string ReceiptNumber { get; set; }

        public string TransID { get; set; }

        public long? CartID { get; set; }

        public string Response { get; set; }

        public string LogType { get; set; }

        public string BankSession { get; set; }

        public string MerchantName { get; set; }

        public string OrderDescription { get; set; }

        public string MerchantLogoURL { get; set; }

        public string MerchantCheckoutJS { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PaymentMasterVisaDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PaymentMasterVisaViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PaymentMasterVisaDTO, PaymentMasterVisaViewModel>.CreateMap();
            var vm = Mapper<PaymentMasterVisaDTO, PaymentMasterVisaViewModel>.Map(dto as PaymentMasterVisaDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<PaymentMasterVisaViewModel, PaymentMasterVisaDTO>.CreateMap();
            var dto = Mapper<PaymentMasterVisaViewModel, PaymentMasterVisaDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PaymentMasterVisaDTO>(jsonString);
        }
    }
}