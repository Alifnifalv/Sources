using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Service.Client
{
    public class SupportServiceClient : BaseClient, ISupportService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.SUPPORT_SERVICE_NAME);

        public SupportServiceClient(CallContext context = null, Action<string> logger = null)
            :base(context, logger)
        {
        }

        public TicketDTO SaveTicket(TicketDTO dtoTicket)
        {
            var uri = string.Format("{0}/SaveTicket", Service);
            return ServiceHelper.HttpPostGetRequest<TicketDTO>(uri, dtoTicket, _callContext, _logger);
        }

        public TicketDTO GetTicket(int ticketID)
        {
            var uri = string.Format("{0}/GetTicket?ticketID={1}", Service, ticketID);
            return ServiceHelper.HttpGetRequest<TicketDTO>(uri, _callContext, _logger);
        }

        public TicketDTO GenerateTicketByReference(TicketDTO ticketDTO)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO SendFeeDueMailReportToParent(long? studentID, string reportName)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO SendProformaInvoiceToParent(long? studentID, string reportName)
        {
            throw new NotImplementedException();
        }

        public List<TicketDTO> GetAllTicketsByLoginID(long? loginID)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSupportActionsByReferenceTypeID(int ticketReferenceTypeID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSupportSubCategoriesByCategoryID(int? supportCategoryID)
        {
            throw new NotImplementedException();
        }

    }
}