using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Web.Library.ViewModels
{
    public class CustomerSupportTicketViewModel : BaseMasterViewModel
    {
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Telephone { get; set; }
        public string Subject { get; set; }
        public string TransactionNo { get; set; }
        public string Comments { get; set; }
        public string IPAddress { get; set; }
        public byte cultureID { get; set; }

        public long TicketIID { get; set; }

        public string CaptchaKey { get; set; }

        public static CustomerSupportTicketDTO ToDTO(CustomerSupportTicketViewModel vm)
        {
            Mapper<CustomerSupportTicketViewModel, CustomerSupportTicketDTO>.CreateMap();
            var mapper = Mapper<CustomerSupportTicketViewModel, CustomerSupportTicketDTO>.Map(vm);

            return mapper;
        }

        public CustomerSupportTicketViewModel()
        {
            Telephone = "";
            TransactionNo = "";
            TicketIID = 0;
            CreatedBy = 0;
            //CaptchaKey = ConfigurationManager.AppSettings["CaptchaKey"].ToString();
        }
    }
}