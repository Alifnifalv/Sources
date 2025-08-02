using System.ComponentModel.DataAnnotations;
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

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.PersonalMobileNo)
                .HasMaxLength(20);

            this.Property(t => t.CivilIDNumber)
                .HasMaxLength(20);

            this.Property(t => t.SponsorDetails)
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(250);

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
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.PersonalMobileNo).HasColumnName("PersonalMobileNo");
            this.Property(t => t.CivilIDNumber).HasColumnName("CivilIDNumber");
            this.Property(t => t.CivilIDValidity).HasColumnName("CivilIDValidity");
            this.Property(t => t.SponsorDetails).HasColumnName("SponsorDetails");
            this.Property(t => t.SalaryMethodID).HasColumnName("SalaryMethodID");
            this.Property(t => t.EmployeeBankID).HasColumnName("EmployeeBankID");
            this.Property(t => t.AliasID).HasColumnName("AliasID");
            this.Property(t => t.PassportStatus).HasColumnName("PassportStatus");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.ResidencyCompanyId).HasColumnName("ResidencyCompanyId");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.ResidencyCompanyId);
            this.HasOptional(t => t.Department)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DepartmentID);
            this.HasOptional(t => t.Gender)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.GenderID);
            this.HasOptional(t => t.MaritalStatus)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.MaritalStatusID);
            this.HasOptional(t => t.Designation)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DesignationID);
            this.HasOptional(t => t.EmployeeBankDetail)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.EmployeeBankID);
            this.HasOptional(t => t.EmployeeRole)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.EmployeeRoleID);
            this.HasOptional(t => t.Employee1)
                .WithMany(t => t.Employees1)
                .HasForeignKey(d => d.ReportingEmployeeID);
            this.HasOptional(t => t.JobType)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.JobTypeID);
            this.HasOptional(t => t.SalaryMethod)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.SalaryMethodID);

        }
    }
}
