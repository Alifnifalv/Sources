using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class StockVerificationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StockVerificationDTO()
        {
            Branch = new KeyValueDTO();
            Employee = new KeyValueDTO();
            StockVerificationMap = new List<StockVerificationMapDTO>();
            TransactionStatus = new KeyValueDTO();
        }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public KeyValueDTO Branch { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string VerificationDate { get; set; }
        [DataMember]
        public long HeadIID { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public int? DocumentTypeID { get; set; }
        [DataMember]
        public string TransactionNo { get; set; }
        [DataMember]
        public DateTime? TransactionDate { get; set; }
        [DataMember]
        public byte? TransactionStatusID { get; set; }
        [DataMember]
        public long? ReferenceHeadID { get; set; }
        [DataMember]
        public int? CurrencyID { get; set; }
        [DataMember]
        public decimal? ExchangeRate { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        [DataMember]
        public byte[] DocumentStatusID { get; set; }

        [DataMember]
        public KeyValueDTO TransactionStatus { get; set; }

        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public DateTime? DocumentCancelledDate { get; set; }
        [DataMember]
        public string ExternalReference1 { get; set; }
        [DataMember]
        public string ExternalReference2 { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public decimal? PhysicalTotalAmount { get; set; }
        [DataMember]
        public decimal? BookTotalAmount { get; set; }
        [DataMember]
        public decimal? DifferTotalAmount { get; set; }
        [DataMember]
        public long? CanceledBy { get; set; }
        [DataMember]
        public DateTime? CanceledDate { get; set; }
        [DataMember]
        public string CanceledReason { get; set; }
        [DataMember]
        public int? FiscalYear_ID { get; set; }
        [DataMember]
        public bool? IsPosted { get; set; }
        [DataMember]
        public int? PostedBy { get; set; }
        [DataMember]
        public DateTime? PostedDate { get; set; }
        [DataMember]
        public string PostedComments { get; set; }

        [DataMember]
        public string PhysicalStockPostedDate { get; set; }

        [DataMember]
        public string PhysicalStockVerfiedBy { get; set; }

        [DataMember]
        public byte? CurrentStatusID { get; set; }

        [DataMember]
        public KeyValueDTO DocumentStatus { get; set; }

        [DataMember]
        public byte? DocStatusID { get; set; }

        [DataMember]
        public string PhysicalVerTransNo { get; set; }

        [DataMember]
        public List<StockVerificationMapDTO> StockVerificationMap { get; set; }
    }
}
