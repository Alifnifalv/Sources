using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Workflows", Schema = "workflow")]
    public partial class Workflow
    {
        public Workflow()
        {
            ClassSubjectWorkflowEntityMaps = new HashSet<ClassSubjectWorkflowEntityMap>();
            ClassWorkFlowMaps = new HashSet<ClassWorkFlowMap>();
            DocumentTypes = new HashSet<DocumentType>();
            WorkflowLogMaps = new HashSet<WorkflowLogMap>();
            WorkflowRules = new HashSet<WorkflowRule>();
            WorkflowTransactionHeadMaps = new HashSet<WorkflowTransactionHeadMap>();
        }

        [Key]
        public long WorkflowIID { get; set; }
        [StringLength(100)]
        public string WokflowName { get; set; }
        public int? WorkflowTypeID { get; set; }
        public int? LinkedEntityTypeID { get; set; }
        public int? WorkflowApplyFieldID { get; set; }

        [ForeignKey("LinkedEntityTypeID")]
        [InverseProperty("Workflows")]
        public virtual EntityType LinkedEntityType { get; set; }
        [ForeignKey("WorkflowApplyFieldID")]
        [InverseProperty("Workflows")]
        public virtual WorkflowFiled WorkflowApplyField { get; set; }
        [ForeignKey("WorkflowTypeID")]
        [InverseProperty("Workflows")]
        public virtual WorkflowType WorkflowType { get; set; }
        [InverseProperty("workflow")]
        public virtual ICollection<ClassSubjectWorkflowEntityMap> ClassSubjectWorkflowEntityMaps { get; set; }
        [InverseProperty("Workflow")]
        public virtual ICollection<ClassWorkFlowMap> ClassWorkFlowMaps { get; set; }
        [InverseProperty("Workflow")]
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
        [InverseProperty("Workflow")]
        public virtual ICollection<WorkflowLogMap> WorkflowLogMaps { get; set; }
        [InverseProperty("Workflow")]
        public virtual ICollection<WorkflowRule> WorkflowRules { get; set; }
        [InverseProperty("Workflow")]
        public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
    }
}
