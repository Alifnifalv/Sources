using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Service.Client.Direct
{
    public class SupportServiceClient : BaseClient, ISupportService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.SUPPORT_SERVICE_NAME);

        SupportService service = new SupportService();

        public SupportServiceClient(CallContext context = null, Action<string> logger = null)
            :base(context, logger)
        {
        }

        public TicketDTO SaveTicket(TicketDTO dtoTicket)
        {
            var uri = string.Format("{0}/SaveTicket", Service);
            return Eduegate.Framework.Services.ServiceHelper.HttpPostGetRequest<TicketDTO>(uri, dtoTicket, _callContext, _logger);
        }

        public TicketDTO GetTicket(int ticketID)
        {
            var uri = string.Format("{0}/GetTicket?ticketID={1}", Service, ticketID);
            return Eduegate.Framework.Services.ServiceHelper.HttpGetRequest<TicketDTO>(uri, _callContext, _logger);
        }

        public TicketDTO GenerateTicketByReference(TicketDTO ticketDTO)
        {
            return service.GenerateTicketByReference(ticketDTO);
        }

        public OperationResultDTO SendFeeDueMailReportToParent(long? studentID, string reportName)
        {
            return service.SendFeeDueMailReportToParent(studentID, reportName);
        }

        public OperationResultDTO SendProformaInvoiceToParent(long? studentID, string reportName)
        {
            return service.SendProformaInvoiceToParent(studentID, reportName);
        }

        public List<TicketDTO> GetAllTicketsByLoginID(long? loginID)
        {
            return service.GetAllTicketsByLoginID(loginID);
        }

        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO)
        {
            return service.SaveTicketCommunication(ticketCommunicationDTO);
        }

    }
}