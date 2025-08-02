using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassWorkFlowMaps", Schema = "schools")]
    public partial class ClassWorkFlowMap
    {
        [Key]
        public long ClassWorkFlowIID { get; set; }
        public int? ClassID { get; set; }
        public int? WorkflowEntityID { get; set; }
        public long? WorkflowID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("ClassWorkFlowMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("WorkflowID")]
        [InverseProperty("ClassWorkFlowMaps")]
        public virtual Workflow Workflow { get; set; }
        [ForeignKey("WorkflowEntityID")]
        [InverseProperty("ClassWorkFlowMaps")]
        public virtual WorkflowEntity WorkflowEntity { get; set; }
    }
}
