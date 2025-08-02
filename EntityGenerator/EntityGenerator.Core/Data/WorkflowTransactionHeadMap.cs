using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowTransactionHeadMaps", Schema = "workflow")]
    public partial class WorkflowTransactionHeadMap
    {
        public WorkflowTransactionHeadMap()
        {
            WorkflowTransactionHeadRuleMaps = new HashSet<WorkflowTransactionHeadRuleMap>();
        }

        [Key]
        public long WorkflowTransactionHeadMapIID { get; set; }
        public long? WorkflowID { get; set; }
        public long? TransactionHeadID { get; set; }
        public byte? WorkflowStatusID { get; set; }

        [ForeignKey("TransactionHeadID")]
        [InverseProperty("WorkflowTransactionHeadMaps")]
        public virtual TransactionHead TransactionHead { get; set; }
        [ForeignKey("WorkflowID")]
        [InverseProperty("WorkflowTransactionHeadMaps")]
        public virtual Workflow Workflow { get; set; }
        [ForeignKey("WorkflowStatusID")]
        [InverseProperty("WorkflowTransactionHeadMaps")]
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        [InverseProperty("WorkflowTransactionHeadMap")]
        public virtual ICollection<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
    }
}
