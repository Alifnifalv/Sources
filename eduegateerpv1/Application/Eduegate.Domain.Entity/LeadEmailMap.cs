namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("crm.LeadEmailMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual EmailTemplate EmailTemplate { get; set; }

        public virtual Lead Lead { get; set; }
    }
}
