using Eduegate.Domain.Entity.Worflow.WorkFlows;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    public partial class dbWorkflowERPContext : DbContext
    {
        public dbWorkflowERPContext()
        {
        }

        public dbWorkflowERPContext(DbContextOptions<dbWorkflowERPContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetWorkFlowConnectionString());
            }
        }

        public virtual DbSet<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        public virtual DbSet<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }
        public virtual DbSet<WorkflowLogMap> WorkflowLogMaps { get; set; }      

        public virtual DbSet<WorkflowCondition> WorkflowConditions { get; set; }
        public virtual DbSet<WorkflowFiled> WorkflowFileds { get; set; }
        public virtual DbSet<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }
        public virtual DbSet<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }
        public virtual DbSet<WorkflowRule> WorkflowRules { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<WorkflowStatus> WorkflowStatuses { get; set; }
        public virtual DbSet<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
        public virtual DbSet<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
        public virtual DbSet<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
        public virtual DbSet<WorkflowType> WorkflowTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WorkflowCondition>()
            //    .Property(e => e.ConditionType)
            //    .IsUnicode(false);

            //modelBuilder.Entity<WorkflowCondition>()
            //    .HasMany(e => e.WorkflowRuleConditions)
            //    .WithOptional(e => e.WorkflowCondition)
            //    .HasForeignKey(e => e.ConditionID);

            //modelBuilder.Entity<WorkflowCondition>()
            //    .HasMany(e => e.WorkflowRules)
            //    .WithOptional(e => e.WorkflowCondition)
            //    .HasForeignKey(e => e.ConditionID);

            //modelBuilder.Entity<WorkflowFiled>()
            //    .Property(e => e.PhysicalColumnName)
            //    .IsUnicode(false);

            //modelBuilder.Entity<WorkflowFiled>()
            //    .HasMany(e => e.Workflows)
            //    .WithOptional(e => e.WorkflowFiled)
            //    .HasForeignKey(e => e.WorkflowApplyFieldID);

            //modelBuilder.Entity<WorkflowRuleCondition>()
            //    .HasMany(e => e.WorkflowRuleApprovers)
            //    .WithRequired(e => e.WorkflowRuleCondition)
            //    .HasForeignKey(e => e.WorkflowRuleConditionID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<WorkflowRuleCondition>()
            //    .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
            //    .WithOptional(e => e.WorkflowRuleCondition)
            //    .HasForeignKey(e => e.WorkflowRuleConditionID);

            //modelBuilder.Entity<WorkflowRule>()
            //    .HasMany(e => e.WorkflowRuleConditions)
            //    .WithOptional(e => e.WorkflowRule)
            //    .HasForeignKey(e => e.WorkflowRuleID);

            //modelBuilder.Entity<WorkflowRule>()
            //    .HasMany(e => e.WorkflowTransactionHeadRuleMaps)
            //    .WithOptional(e => e.WorkflowRule)
            //    .HasForeignKey(e => e.WorkflowRuleID);

            //modelBuilder.Entity<Workflow>()
            //    .HasMany(e => e.WorkflowRules)
            //    .WithOptional(e => e.Workflow)
            //    .HasForeignKey(e => e.WorkflowID);

            //modelBuilder.Entity<Workflow>()
            //    .HasMany(e => e.WorkflowTransactionHeadMaps)
            //    .WithOptional(e => e.Workflow)
            //    .HasForeignKey(e => e.WorkflowID);

            //modelBuilder.Entity<WorkflowTransactionHeadMap>()
            //    .HasMany(e => e.WorkflowTransactionHeadRuleMaps)
            //    .WithOptional(e => e.WorkflowTransactionHeadMap)
            //    .HasForeignKey(e => e.WorkflowTransactionHeadMapID);

            //modelBuilder.Entity<WorkflowTransactionHeadRuleMap>()
            //    .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
            //    .WithOptional(e => e.WorkflowTransactionHeadRuleMap)
            //    .HasForeignKey(e => e.WorkflowTransactionHeadRuleMapID);

            //modelBuilder.Entity<WorkflowLogMap>()
            //  .HasMany(e => e.WorkflowLogMapRuleMaps)
            //  .WithOptional(e => e.WorkflowLogMap)
            //  .HasForeignKey(e => e.WorkflowLogMapID);

            //modelBuilder.Entity<WorkflowRule>()
            //   .HasMany(e => e.WorkflowLogMapRuleMaps)
            //   .WithOptional(e => e.WorkflowRule)
            //   .HasForeignKey(e => e.WorkflowRuleID);

            //modelBuilder.Entity<Workflow>()
            //    .HasMany(e => e.WorkflowLogMaps)
            //    .WithOptional(e => e.Workflow)
            //    .HasForeignKey(e => e.WorkflowID);      

            //modelBuilder.Entity<Workflow>()
            //.HasMany(e => e.WorkflowLogMaps)
            //.WithOptional(e => e.Workflow)
            //.HasForeignKey(e => e.WorkflowID);

            //modelBuilder.Entity<WorkflowRule>()
            //    .HasMany(e => e.WorkflowLogMapRuleMaps)
            //    .WithOptional(e => e.WorkflowRule)
            //    .HasForeignKey(e => e.WorkflowRuleID);

            //modelBuilder.Entity<WorkflowLogMapRuleMap>()
            //    .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
            //    .WithOptional(e => e.WorkflowLogMapRuleMap)
            //    .HasForeignKey(e => e.WorkflowLogMapRuleMapID);

            //modelBuilder.Entity<WorkflowLogMap>()
            //    .HasMany(e => e.WorkflowLogMapRuleMaps)
            //    .WithOptional(e => e.WorkflowLogMap)
            //    .HasForeignKey(e => e.WorkflowLogMapID);

            //modelBuilder.Entity<WorkflowRuleCondition>()
            //    .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
            //    .WithOptional(e => e.WorkflowRuleCondition)
            //    .HasForeignKey(e => e.WorkflowRuleConditionID);

            //modelBuilder.Entity<WorkflowRuleCondition>()
            //    .HasMany(e => e.WorkflowRuleApprovers)
            //    .WithRequired(e => e.WorkflowRuleCondition)
            //    .HasForeignKey(e => e.WorkflowRuleConditionID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<WorkflowRuleCondition>()
            //    .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
            //    .WithOptional(e => e.WorkflowRuleCondition)
            //    .HasForeignKey(e => e.WorkflowRuleConditionID);

            //modelBuilder.Entity<WorkflowRule>()
            //    .HasMany(e => e.WorkflowLogMapRuleMaps)
            //    .WithOptional(e => e.WorkflowRule)
            //    .HasForeignKey(e => e.WorkflowRuleID);

            //modelBuilder.Entity<WorkflowRule>()
            //    .HasMany(e => e.WorkflowRuleConditions)
            //    .WithOptional(e => e.WorkflowRule)
            //    .HasForeignKey(e => e.WorkflowRuleID);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}