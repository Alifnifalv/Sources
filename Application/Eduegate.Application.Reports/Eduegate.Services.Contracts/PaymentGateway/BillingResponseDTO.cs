using System;

namespace Eduegate.Services.Contracts.PaymentGateway
{
    public class BillingResponseDTO
    {
        public string RequestID { get; set; }
        public string DeliveryChannelCtrlID { get; set; }
        public DateTime LocalTxnDtTime { get; set; }
        public string ActCode { get; set; } = "1";
        public int ResposeCode { get; set; } = 1;        
        public string ActDescription { get; set; } = "Fee Due";
        public string Description { get; set; } = "Fee Due";
        public string SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string StudentName { get; set; }
        public string ClassSection { get; set; }
        public string Term { get; set; }
        public string RollNo { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal BilledAmount { get; set; }
        public string Remarks { get; set; }
    }
}
