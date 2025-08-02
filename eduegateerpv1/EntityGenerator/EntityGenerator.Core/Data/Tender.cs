using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Tenders", Schema = "inventory")]
    public partial class Tender
    {
        public Tender()
        {
            BidApprovalHeads = new HashSet<BidApprovalHead>();
            RFQSupplierRequestMaps = new HashSet<RFQSupplierRequestMap>();
            TenderAuthentications = new HashSet<TenderAuthentication>();
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
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SubmissionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OpeningDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? NumOfAuthorities { get; set; }
        public long? TenderAwardedID { get; set; }

        [ForeignKey("TenderAwardedID")]
        [InverseProperty("Tenders")]
        public virtual TenderAuthentication TenderAwarded { get; set; }
        [ForeignKey("TenderStatusID")]
        [InverseProperty("Tenders")]
        public virtual TenderStatus TenderStatus { get; set; }
        [ForeignKey("TenderTypeID")]
        [InverseProperty("Tenders")]
        public virtual TenderType1 TenderType { get; set; }
        [InverseProperty("Tender")]
        public virtual ICollection<BidApprovalHead> BidApprovalHeads { get; set; }
        [InverseProperty("Tender")]
        public virtual ICollection<RFQSupplierRequestMap> RFQSupplierRequestMaps { get; set; }
        [InverseProperty("Tender")]
        public virtual ICollection<TenderAuthentication> TenderAuthentications { get; set; }
    }
}
