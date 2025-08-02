namespace Eduegate.Services.Contracts.PaymentGateway
{
    public class BankBillPaymentDTO
    {
        public string AgentId { get; set; }
        public string Token { get; set; }
        public string TransactionId { get; set; }
        public string StudentRollNumber { get; set; }
        public string ChildQID { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Remarks { get; set; }
    }
}
