namespace Eduegate.Services.Contracts.PaymentGateway
{
    public class PaymentResponseDTO
    {
        public int resposeCode { get; set; }
        public string description { get; set; }
        public string referenceNumber { get; set; }
        public string remarks { get; set; }
        public string mailID { get; set; }
        public long? id { get; set; }
        public byte? schoolID { get; set; }

        public string admissionNumber { get; set; }

        public string receiptNo { get; set; }


    }
    public class PaymentMainResponseDTO
    {
        public int resposeCode { get; set; }
        public string description { get; set; }
        public string referenceNumber { get; set; }
        public string remarks { get; set; }
       
    }
}
