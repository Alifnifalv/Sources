using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeadEmailMaps", Schema = "crm")]
    public partial class LeadEmailMap
    {
        [Key]
        public long LeadEmailMapIID { get; set; }
        public long? LeadID { get; set; }
        public int? EmailTemplateID { get; set; }
        public string EmaiContent { get; set; }
        [StringLength(50)]
        public string FromEmailID { get; set; }
        [StringLength(50)]
        public string ToEmailID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmailTemplateID")]
        [InverseProperty("LeadEmailMaps")]
        public virtual EmailTemplate EmailTemplate { get; set; }
        [ForeignKey("LeadID")]
        [InverseProperty("LeadEmailMaps")]
        public virtual Lead Lead { get; set; }
    }
}
