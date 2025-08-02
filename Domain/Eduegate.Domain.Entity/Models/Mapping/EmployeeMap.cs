using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeIID);

            // Properties
            this.Property(t => t.EmployeeAlias)
                .HasMaxLength(50);

            this.Property(t => t.EmployeeName)
                .HasMaxLength(100);

            this.Property(t => t.EmployeePhoto)
                .HasMaxLength(500);

            this.Property(t => t.WorkEmail)
                .HasMaxLength(50);

            this.Property(t => t.WorkPhone)
                .HasMaxLength(20);

            this.Property(t => t.WorkMobileNo)
                .HasMaxLength(20);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Employees", "payroll");
            this.Property(t => t.EmployeeIID).HasColumnName("EmployeeIID");
            this.Property(t => t.EmployeeAlias).HasColumnName("EmployeeAlias");
            this.Property(t => t.EmployeeName).HasColumnName("EmployeeName");
            this.Property(t => t.EmployeeRoleID).HasColumnName("EmployeeRoleID");
            this.Property(t => t.DesignationID).HasColumnName("DesignationID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.EmployeePhoto).HasColumnName("EmployeePhoto");
            this.Property(t => t.WorkEmail).HasColumnName("WorkEmail");
            this.Property(t => t.WorkPhone).HasColumnName("WorkPhone");
            this.Property(t => t.WorkMobileNo).HasColumnName("WorkMobileNo");
            this.Property(t => t.DateOfJoining).HasColumnName("DateOfJoining");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.JobTypeID).HasColumnName("JobTypeID");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.MaritalStatusID).HasColumnName("MaritalStatusID");
            this.Property(t => t.ReportingEmployeeID).HasColumnName("ReportingEmployeeID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.BranchID);
            //this.HasOptional(t => t.Department)
            //    .WithMany(t => t.Employees)
            //    .HasForeignKey(d => d.DepartmentID);
            this.HasOptional(t => t.Gender)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.GenderID);
            //this.HasOptional(t => t.MaritalStatus)
            //    .WithMany(t => t.Employees)
            //    .HasForeignKey(d => d.MaritalStatusID);
            this.HasOptional(t => t.Designation)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DesignationID);
            this.HasOptional(t => t.EmployeeRole)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.EmployeeRoleID);
            this.HasOptional(t => t.Employee1)
                .WithMany(t => t.Employees1)
                .HasForeignKey(d => d.ReportingEmployeeID);
            this.HasOptional(t => t.JobType)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.JobTypeID);

            this.HasMany(e => e.WorkflowRuleApprovers)
             .WithOptional(e => e.Employee)
             .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
               .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
               .WithOptional(e => e.Employee)
               .HasForeignKey(e => e.EmployeeID);

            this
              .HasMany(e => e.Tickets)
             .WithOptional(e => e.Employee)
             .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.Tickets1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ManagerEmployeeID);

            this
                .HasMany(e => e.Tickets2)
                .WithOptional(e => e.Employee2)
                .HasForeignKey(e => e.AssingedEmployeeID);

            this
                .HasMany(e => e.InventoryVerifications)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.TransactionHeads1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.StaffID);

            this
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.EmployeeRoleMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ReportingEmployeeID);

            this
                .HasMany(e => e.EmployeeSalaries)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.WorkflowRuleApprovers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
                .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            this
              .HasMany(e => e.Suppliers)
              .WithOptional(e => e.Employee)
              .HasForeignKey(e => e.EmployeeID);
        }
    }
}
