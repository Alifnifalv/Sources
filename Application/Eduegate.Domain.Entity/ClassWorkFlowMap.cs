namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassWorkFlowMaps")]
    public partial class ClassWorkFlowMap
    {
        [Key]
        public long ClassWorkFlowIID { get; set; }

        public int? ClassID { get; set; }

        public int? WorkflowEntityID { get; set; }

        public long? WorkflowID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual WorkflowEntity WorkflowEntity { get; set; }

        public virtual Class Class { get; set; }

        public virtual Workflow Workflow { get; set; }
    }
}
