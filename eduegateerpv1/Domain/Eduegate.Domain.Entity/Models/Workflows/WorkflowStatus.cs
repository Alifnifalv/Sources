using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Workflows
{
    [Table("WorkflowStatuses", Schema = "workflow")]
    public partial class WorkflowStatus
    {
        public WorkflowStatus()
        {
            WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
            WorkflowLogMapRuleMaps = new HashSet<WorkflowLogMapRuleMap>();
            WorkflowLogMaps = new HashSet<WorkflowLogMap>();
            WorkflowTransactionHeadMaps = new HashSet<WorkflowTransactionHeadMap>();
            WorkflowTransactionHeadRuleMaps = new HashSet<WorkflowTransactionHeadRuleMap>();
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
        }

        [Key]
        public byte WorkflowStatusID { get; set; }
        public int? WorkflowTypeID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        public virtual WorkflowType WorkflowType { get; set; }

        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }

        public virtual ICollection<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }

        public virtual ICollection<WorkflowLogMap> WorkflowLogMaps { get; set; }

        public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }

        public virtual ICollection<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }

        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}