using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Supports
{
    [DataContract]
    public class CustomerSupportTicketDTO
    {
        //[DataMember]
        //public long? TicketIID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string TransactionNo { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public byte CultureID { get; set; }
        [DataMember]
        public long? CreatedBy { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
    }
}