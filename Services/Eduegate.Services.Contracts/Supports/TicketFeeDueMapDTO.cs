using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Supports
{
    public class TicketFeeDueMapDTO : BaseMasterDTO
    {
        public TicketFeeDueMapDTO()
        {
        }

        [DataMember]
        public long TicketFeeDueMapIID { get; set; }

        [DataMember]
        public long? TicketID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public string InvoiceDateString { get; set; }        

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public string FeeMaster { get; set; }

        [DataMember]
        public decimal? DueAmount { get; set; }

    }
}