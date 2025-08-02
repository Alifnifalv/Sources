using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Supports
{
    public class TicketDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TicketDTO()
        {
            TicketProductSKUs = new List<TicketProductDTO>();
            Document = new DocumentViewDTO();
        }

        [DataMember]
        public long TicketIID { get; set; }

        [DataMember]
        public string TicketNo { get; set; }

        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Description2 { get; set; }

        [DataMember]
        public Nullable<int> Source { get; set; }

        [DataMember]
        public Nullable<byte> PriorityID { get; set; }

        [DataMember]
        public byte ActionID { get; set; }

        [DataMember]
        public Nullable<byte> TicketStatusID { get; set; }

        [DataMember]
        public string TicketStatus { get; set; }

        [DataMember]
        public Nullable<long> AssingedEmployeeID { get; set; }

        [DataMember]
        public string AssignedEmployee { get; set; }

        [DataMember]
        public Nullable<long> ManagerEmployeeID { get; set; }

        [DataMember]
        public string ManagerEmployee { get; set; }

        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public Nullable<long> SupplierID { get; set; }

        [DataMember]
        public Nullable<long> EmployeeID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DueDateFrom { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DueDateTo { get; set; }
        [DataMember]
        public Nullable<long> HeadID { get; set; }
        [DataMember]
        public Nullable<bool> CustomerNotification { get; set; }

        [DataMember]
        public DocumentViewDTO Document { get; set; }

        [DataMember]
        public List<TicketProductDTO> TicketProductSKUs { get; set; }

        [DataMember]
        public TicketActionDetailMapsDTO TicketActionDetail { get; set; }

        [DataMember]
        public int CompanyID { get; set; }
    }
}
