using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Opportunities", Schema = "crm")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("Opportunities")]
        public virtual CRMCompany Company { get; set; }
        [ForeignKey("LeadID")]
        [InverseProperty("Opportunities")]
        public virtual Lead Lead { get; set; }
        [ForeignKey("OpportunityFromID")]
        [InverseProperty("Opportunities")]
        public virtual OpportunityFrom OpportunityFrom { get; set; }
        [ForeignKey("OpportunityStatusID")]
        [InverseProperty("Opportunities")]
        public virtual OpportunityStatus OpportunityStatus { get; set; }
        [ForeignKey("OpportunityTypeID")]
        [InverseProperty("Opportunities")]
        public virtual OpportunityType OpportunityType { get; set; }
        [ForeignKey("SourcesID")]
        [InverseProperty("Opportunities")]
        public virtual Source Sources { get; set; }
    }
}
