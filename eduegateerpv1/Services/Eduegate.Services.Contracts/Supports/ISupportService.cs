using Eduegate.Services.Contracts.Commons;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Supports
{
    public interface ISupportService
    {
        TicketDTO SaveTicket(TicketDTO dtoTicket);

        TicketDTO GetTicket(int ticketID);

        public TicketDTO GenerateTicketByReference(TicketDTO ticketDTO);

        public OperationResultDTO SendFeeDueMailReportToParent(long? studentID, string reportName);

        public OperationResultDTO SendProformaInvoiceToParent(long? studentID, string reportName);

        public List<TicketDTO> GetAllTicketsByLoginID(long? loginID);

        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO);
    }
}