using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowLogMaps", Schema = "workflow")]
    [Index("ReferenceID", Name = "IDX_WorkflowLogMaps_ReferenceID_")]
    [Index("ReferenceID", Name = "IDX_WorkflowLogMaps_ReferenceID_WorkflowStatusID")]
    public partial class WorkflowLogMap
    {
        public WorkflowLogMap()
        {
            WorkflowLogMapRuleMaps = new HashSet<WorkflowLogMapRuleMap>();
        }

        [Key]
        public long WorkflowLogMapIID { get; set; }
        public long? WorkflowID { get; set; }
        public long? ReferenceID { get; set; }
        public byte? WorkflowStatusID { get; set; }

        [ForeignKey("WorkflowID")]
        [InverseProperty("WorkflowLogMaps")]
        public virtual Workflow Workflow { get; set; }
        [ForeignKey("WorkflowStatusID")]
        [InverseProperty("WorkflowLogMaps")]
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        [InverseProperty("WorkflowLogMap")]
        public virtual ICollection<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }
    }
}
