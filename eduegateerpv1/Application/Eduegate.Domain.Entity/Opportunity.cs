namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("crm.Opportunities")]
    public partial class Opportunity
    {
        [Key]
        public long OpportunityIID { get; set; }

        public long? LeadID { get; set; }

        public string Description { get; set; }

        public byte? OpportunityFromID { get; set; }

        public byte? OpportunityStatusID { get; set; }

        public int? OpportunityTypeID { get; set; }

        public int? SourcesID { get; set; }

        public int? CompanyID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual CRMCompany CRMCompany { get; set; }

        public virtual Lead Lead { get; set; }

        public virtual OpportunityFrom OpportunityFrom { get; set; }

        public virtual OpportunityStatus OpportunityStatus { get; set; }

        public virtual OpportunityType OpportunityType { get; set; }

        public virtual Source Source { get; set; }
    }
}
