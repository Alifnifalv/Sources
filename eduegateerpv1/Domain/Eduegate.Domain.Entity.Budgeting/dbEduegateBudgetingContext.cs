using Eduegate.Domain.Entity.Budgeting.Models;
using Microsoft.EntityFrameworkCore;


namespace Eduegate.Domain.Entity.Budgeting
{
    public partial class  dbEduegateBudgetingContext : DbContext
    {
        public dbEduegateBudgetingContext()
        {
        }

        public dbEduegateBudgetingContext(DbContextOptions<dbEduegateBudgetingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }


        public virtual DbSet<Budget1> Budgets1 { get; set; }
        public virtual DbSet<BudgetEntry> BudgetEntries { get; set; }
        public virtual DbSet<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }
        public virtual DbSet<BudgetEntryAllocation> BudgetEntryAllocations { get; set; }
        public virtual DbSet<BudgetEntryFeeMap> BudgetEntryFeeMaps { get; set; }
        public virtual DbSet<BudgetStatus> BudgetStatuses { get; set; }
        public virtual DbSet<BudgetSuggestion> BudgetSuggestions { get; set; }
        public virtual DbSet<BudgetType> BudgetTypes { get; set; }
        public virtual DbSet<FeeMaster> FeeMasters { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<FiscalYear> FiscalYears { get; set; }

        public virtual DbSet<AuditType> AuditTypes { get; set; }

        public virtual DbSet<BudgetEntryCostCenterMap> BudgetEntryCostCenterMaps { get; set; }

        public virtual DbSet<Departments1> Departments1 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget1>(entity =>
            {
                entity.Property(e => e.BudgetID).ValueGeneratedNever();

                entity.HasOne(d => d.BudgetGroup)
                    .WithMany(p => p.Budget1)
                    .HasForeignKey(d => d.BudgetGroupID)
                    .HasConstraintName("FK_Budgets_Groups");

                entity.HasOne(d => d.BudgetStatus)
                    .WithMany(p => p.Budget1)
                    .HasForeignKey(d => d.BudgetStatusID)
                    .HasConstraintName("FK_BudgetEntry_BudgetStatus");

                entity.HasOne(d => d.BudgetType)
                    .WithMany(p => p.Budget1)
                    .HasForeignKey(d => d.BudgetTypeID)
                    .HasConstraintName("FK_Budget_BudgetType");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Budget1)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_BudgetEntry_Currency");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Budget1)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_BudgetEntry_Department");

                entity.HasOne(d => d.Company)
                  .WithMany(p => p.Budget1)
                  .HasForeignKey(d => d.CompanyID)
                  .HasConstraintName("FK_Budgets_Company");

                entity.HasOne(d => d.FinancialYear)
                  .WithMany(p => p.Budget1)
                  .HasForeignKey(d => d.FinancialYearID)
                  .HasConstraintName("FK_Budgets_FiscalYear");
            });

            modelBuilder.Entity<BudgetEntry>(entity =>
            {
                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.BudgetEntries)
                    .HasForeignKey(d => d.BudgetID)
                    .HasConstraintName("FK_BudgetEntry_Budget");

                entity.HasOne(d => d.BudgetSuggestion)
                    .WithMany(p => p.BudgetEntries)
                    .HasForeignKey(d => d.BudgetSuggestionID)
                    .HasConstraintName("FK_BudgetEntry_BudgetSuggestion");

               
            });

            modelBuilder.Entity<BudgetEntryAccountMap>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BudgetEntryAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_BudgetEntryAccountMaps_Accounts");

                entity.HasOne(d => d.BudgetEntry)
                    .WithMany(p => p.BudgetEntryAccountMaps)
                    .HasForeignKey(d => d.BudgetEntryID)
                    .HasConstraintName("FK_BudgetEntryAccountMaps_BudgetEntry");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.BudgetEntryAccountMaps)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_BudgetEntryAccountMaps_CostCenter");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.BudgetEntryAccountMaps)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_BudgetEntryAccountMaps_Groups");
            });

            modelBuilder.Entity<BudgetEntryCostCenterMap>(entity =>
            {
                entity.HasOne(d => d.BudgetEntry)
                    .WithMany(p => p.BudgetEntryCostCenterMaps)
                    .HasForeignKey(d => d.BudgetEntryID)
                    .HasConstraintName("FK_BudgetEntryCostCenterMaps_BudgetEntries");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.BudgetEntryCostCenterMaps)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_BudgetEntryCostCenter_CostCenter");
            });

            modelBuilder.Entity<BudgetEntryAllocation>(entity =>
            {
                entity.HasOne(d => d.BudgetEntry)
                    .WithMany(p => p.BudgetEntryAllocations)
                    .HasForeignKey(d => d.BudgetEntryID)
                    .HasConstraintName("FK_BudgetEntryAllocations_BudgetEntry");
            });

            modelBuilder.Entity<BudgetEntryFeeMap>(entity =>
            {
                entity.HasOne(d => d.BudgetEntry)
                    .WithMany(p => p.BudgetEntryFeeMaps)
                    .HasForeignKey(d => d.BudgetEntryID)
                    .HasConstraintName("FK_BudgetEntryFeeMaps_BudgetEntry");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.BudgetEntryFeeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_BudgetEntryFeeMaps_FeeMaster");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_Accounts_Groups");

                entity.HasOne(d => d.ParentAccount)
                    .WithMany(p => p.InverseParentAccount)
                    .HasForeignKey(d => d.ParentAccountID)
                    .HasConstraintName("FK_Accounts_Accounts");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.BaseCurrency)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.BaseCurrencyID)
                    .HasConstraintName("FK_Companies_Currencies");

                //entity.HasOne(d => d.CompanyGroup)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.CompanyGroupID)
                //    .HasConstraintName("FK_Companies_CompanyGroups");

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.CountryID)
                //    .HasConstraintName("FK_Companies_Companies");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Companies_CompanyStatuses");
            });

            modelBuilder.Entity<AuditType>(entity =>
            {
                entity.Property(e => e.AuditTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<FiscalYear>(entity =>
            {
                entity.HasOne(d => d.AuditTypes)
                    .WithMany(p => p.FiscalYears)
                    .HasForeignKey(d => d.AuditType)
                    .HasConstraintName("FK_FiscalYear_AuditTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}