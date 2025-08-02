using System.ServiceModel;
using Eduegate.Domain.Mappers.CustomerSupport;
using Eduegate.Domain.Support;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Services
{
    public class SupportService : BaseService, ISupportService
    {
        public TicketDTO GetTicket(int ticketID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SupportService>.Info("Service Result : " + ticketID.ToString());
                return new SupportBL(CallContext).GetTicket(ticketID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupportService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public TicketDTO SaveTicket(TicketDTO dtoTicket)
        {
            try
            {
                Eduegate.Logger.LogHelper<SupportService>.Info("Service Result : " + dtoTicket);
                return new SupportBL(CallContext).SaveTicket(dtoTicket);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupportService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public TicketDTO GenerateTicketByReference(TicketDTO ticketDTO)
        {
            return TicketingMapper.Mapper(CallContext).GenerateTicketByReference(ticketDTO);
        }

        public OperationResultDTO SendFeeDueMailReportToParent(long? studentID, string reportName)
        {
            return new SupportBL(CallContext).SendFeeDueMailReportToParent(studentID, reportName);
        }

        public OperationResultDTO SendProformaInvoiceToParent(long? studentID, string reportName)
        {
            return new SupportBL(CallContext).SendProformaInvoiceToParent(studentID, reportName);
        }

        public List<TicketDTO> GetAllTicketsByLoginID(long? loginID)
        {
            return new SupportBL(CallContext).GetAllTicketsByLoginID(loginID);
        }

        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO)
        {
            return TicketingMapper.Mapper(CallContext).SaveTicketCommunication(ticketCommunicationDTO);
        }

    }
}