using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ShipmentDetailDTO
    {
        [DataMember]
        public long TransactionShipmentIID { get; set; }

        [DataMember]
        public Nullable<long> TransactionHeadID { get; set; }

        [DataMember]
        public Nullable<long> SupplierIDFrom { get; set; }

        [DataMember]
        public Nullable<long> SupplierIDTo { get; set; }

        [DataMember]
        public string ShipmentReference { get; set; }

        [DataMember]
        public string FreightCareer { get; set; }

        [DataMember]
        public Nullable<short> ClearanceTypeID { get; set; }

        [DataMember]
        public string AirWayBillNo { get; set; }

        [DataMember]
        public Nullable<decimal> FrieghtCharges { get; set; }

        [DataMember]
        public Nullable<decimal> BrokerCharges { get; set; }

        [DataMember]
        public Nullable<double> Weight { get; set; }

        [DataMember]
        public Nullable<int> NoOfBoxes { get; set; }

        [DataMember]
        public string BrokerAccount { get; set; }

        [DataMember]
        public Nullable<decimal> AdditionalCharges { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public Nullable<int> CreatedBy { get; set; }

        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }

        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [DataMember]
        public string TimeStamps { get; set; }
    }
}
