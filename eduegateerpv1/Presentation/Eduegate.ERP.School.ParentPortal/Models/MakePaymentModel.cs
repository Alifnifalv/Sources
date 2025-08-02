namespace Eduegate.ERP.Admin.Models
{
    public class MakePaymentModel
    {
        public string agentId { get; set; }
        public string token { get; set; }
        public string transactionId { get; set; }
        public string StudentRollNumber { get; set; }
        public string ChildQID { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string remarks { get; set; }
    }
}
