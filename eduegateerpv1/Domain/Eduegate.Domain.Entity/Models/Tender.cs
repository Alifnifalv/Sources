using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Tenders", Schema = "inventory")]
    public partial class Tender
    {
        public Tender()
        {
            TenderAuthentications = new HashSet<TenderAuthentication>();
            RFQSupplierRequestMaps = new HashSet<RFQSupplierRequestMap>();
            BidApprovalHeads = new HashSet<BidApprovalHead>();
        }

        [Key]
        public long TenderIID { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long? TenderTypeID { get; set; }

        public long? TenderStatusID { get; set; }

        public bool? IsOpened { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? SubmissionDate { get; set; }

        public DateTime? OpeningDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? NumOfAuthorities { get; set; }

        public long? TenderAwardedID { get; set; }

        public virtual TenderAuthentication TenderAwarded { get; set; }

        public virtual TenderStatus TenderStatus { get; set; }

        public virtual TenderType1 TenderType { get; set; }

        public virtual ICollection<TenderAuthentication> TenderAuthentications { get; set; }

        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMaps { get; set; }

        public virtual ICollection<BidApprovalHead> BidApprovalHeads { get; set; }
    }
}
