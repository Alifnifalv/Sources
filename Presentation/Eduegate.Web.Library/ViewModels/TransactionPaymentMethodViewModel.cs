using System;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.PaymentGateway;

namespace Eduegate.Web.Library.ViewModels
{
    public class TransactionPaymentMethodViewModel : BaseMasterViewModel
    {
        public long PaymentMapIID { get; set; }
        public long HeadID { get; set; }
        public short PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal? Amount { get; set; }

        public string PaymentDate { get; set; }

        public static TransactionPaymentMethodDTO ToDTO(TransactionPaymentMethodViewModel vm)
        {
            var dto = new TransactionPaymentMethodDTO();
            //Mapper<TransactionPaymentMethodViewModel, TransactionPaymentMethodDTO>.CreateMap();
            //return Mapper<TransactionPaymentMethodViewModel, TransactionPaymentMethodDTO>.Map(vm);
            dto.PaymentMapIID = vm.PaymentMapIID;
            dto.HeadID = vm.HeadID;
            dto.PaymentMethodID = (Eduegate.Services.Contracts.Enums.PaymentMethodTypes)vm.PaymentMethodID;
            dto.PaymentMethodName = vm.PaymentMethodName;
            dto.Amount = vm.Amount;
            dto.PaymentDate = string.IsNullOrWhiteSpace(vm.PaymentDate) ? DateTime.Now : Convert.ToDateTime(vm.PaymentDate);

            return dto;
        }

        public static TransactionPaymentMethodViewModel ToVM(TransactionPaymentMethodDTO dto)
        {
            var vm = new TransactionPaymentMethodViewModel();
            //Mapper<TransactionPaymentMethodDTO, TransactionPaymentMethodViewModel>.CreateMap();
            //var vm = Mapper<TransactionPaymentMethodDTO, TransactionPaymentMethodViewModel>.Map(dto);
            vm.PaymentMapIID = dto.PaymentMapIID;
            vm.HeadID = dto.HeadID;
            vm.PaymentMethodID = (short)dto.PaymentMethodID;
            vm.PaymentMethodName = dto.PaymentMethodName;
            vm.Amount = dto.Amount;
            vm.PaymentDate = dto.PaymentDate.ToString();
            return vm;
        }
    }
}