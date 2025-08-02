using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Accounts.Models.Budgets;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Catalog;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Domain.Entity.Accounts.Models.Payrolls;
using Eduegate.Domain.Entity.Accounts.Models.Schools;
using Eduegate.Domain.Entity.Accounts.Models.AI;
using Eduegate.Domain.Entity.Accounts.Models.Jobs;

namespace Eduegate.Domain.Entity.Accounts
{
    public partial class dbEduegateAccountsContext : DbContext
    {
        public dbEduegateAccountsContext()
        {
        }

        public dbEduegateAccountsContext(DbContextOptions<dbEduegateAccountsContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; }
        public virtual DbSet<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public virtual DbSet<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual DbSet<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<RuleSet> RuleSets { get; set; }
        public virtual DbSet<RuleType> RuleTypes { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankAccountStatus> BankAccountStatuses { get; set; }
        public virtual DbSet<BankAccountType> BankAccountTypes { get; set; }
        public virtual DbSet<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        public virtual DbSet<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public virtual DbSet<Payable> Payables { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<Receivable> Receivables { get; set; }
        public virtual DbSet<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public virtual DbSet<TransactionAllocationDetail> TransactionAllocationDetails { get; set; }
        public virtual DbSet<TransactionAllocationHead> TransactionAllocationHeads { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetCategory> AssetCategories { get; set; }
        public virtual DbSet<AssetGroup> AssetGroups { get; set; }
        public virtual DbSet<AssetInventory> AssetInventories { get; set; }
        public virtual DbSet<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
        public virtual DbSet<AssetProductMap> AssetProductMaps { get; set; }
        public virtual DbSet<AssetSerialMap> AssetSerialMaps { get; set; }
        public virtual DbSet<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public virtual DbSet<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual DbSet<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
        public virtual DbSet<AssetTransactionSerialMap> AssetTransactionSerialMaps { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }

        public virtual DbSet<DepreciationPeriod> DepreciationPeriods { get; set; }
        public virtual DbSet<DepreciationType> DepreciationTypes { get; set; }

        public virtual DbSet<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSKUMap> ProductSKUMaps { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<TaxStatus> TaxStatuses { get; set; }
        public virtual DbSet<TaxTemplate> TaxTemplates { get; set; }
        public virtual DbSet<TaxTemplateItem> TaxTemplateItems { get; set; }
        public virtual DbSet<TaxTransaction> TaxTransactions { get; set; }
        public virtual DbSet<TaxType> TaxTypes { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Department1> Departments1 { get; set; }
        public virtual DbSet<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        public virtual DbSet<DocumentReferenceType> DocumentReferenceTypes { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EntitlementMap> EntitlementMaps { get; set; }
        public virtual DbSet<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<EmployeePromotion> EmployeePromotions { get; set; }
        public virtual DbSet<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }
        public virtual DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        public virtual DbSet<LeaveGroup> LeaveGroups { get; set; }
        public virtual DbSet<LeaveSession> LeaveSessions { get; set; }
        public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<SalaryStructure> SalaryStructures { get; set; }

        public virtual DbSet<FeeMaster> FeeMasters { get; set; }
        public virtual DbSet<FineMaster> FineMasters { get; set; }
        public virtual DbSet<PackageConfig> PackageConfigs { get; set; }
        public virtual DbSet<PackageConfigClassMap> PackageConfigClassMaps { get; set; }
        public virtual DbSet<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }
        public virtual DbSet<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }
        public virtual DbSet<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }

        public virtual DbSet<AccountTransactionReceivablesMap> AccountTransactionReceivablesMaps { get; set; }

        public virtual DbSet<AuditData> AuditDatas { get; set; }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<JobActivity> JobActivities { get; set; }
        public virtual DbSet<JobEntryDetail> JobEntryDetails { get; set; }
        public virtual DbSet<JobEntryHead> JobEntryHeads { get; set; }
        public virtual DbSet<JobOperationStatus> JobOperationStatuses { get; set; }
        public virtual DbSet<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        public virtual DbSet<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
        public virtual DbSet<JobSize> JobSizes { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AccountBehavoir)
                //    .WithMany(p => p.Accounts)
                //    .HasForeignKey(d => d.AccountBehavoirID)
                //    .HasConstraintName("FK_Accounts_AccountBehavoirs");

                //entity.HasOne(d => d.Group)
                //    .WithMany(p => p.Accounts)
                //    .HasForeignKey(d => d.GroupID)
                //    .HasConstraintName("FK_Accounts_Groups");

                entity.HasOne(d => d.ParentAccount)
                    .WithMany(p => p.InverseParentAccount)
                    .HasForeignKey(d => d.ParentAccountID)
                    .HasConstraintName("FK_Accounts_Accounts");
            });

            modelBuilder.Entity<AccountTaxTransaction>(entity =>
            {
                entity.HasOne(d => d.Accound)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.AccoundID)
                    .HasConstraintName("FK_AccountTaxTransactions_Accounts");

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_AccountTaxTransactions_TransactionHead");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_AccountTaxTransactions_TaxTemplates");

                entity.HasOne(d => d.TaxTemplateItem)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateItemID)
                    .HasConstraintName("FK_AccountTaxTransactions_TaxTemplateItems");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_AccountTaxTransactions_TaxTypes");
            });

            modelBuilder.Entity<AccountTransaction>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactions_Accounts");

                //entity.HasOne(d => d.Budget)
                //    .WithMany(p => p.AccountTransactions)
                //    .HasForeignKey(d => d.BudgetID)
                //    .HasConstraintName("FK_AccountTran_Budgets");

                //entity.HasOne(d => d.CostCenter)
                //    .WithMany(p => p.AccountTransactions)
                //    .HasForeignKey(d => d.CostCenterID)
                //    .HasConstraintName("FK_AccountTransactions_CostCenters");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AccountTransactions_DocumentTypes");
            });

            modelBuilder.Entity<AccountTransactionDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactionDetails_Accounts");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_AccountTransactionDetails_AccountTransactionHeads");

                //entity.HasOne(d => d.Budget)
                //    .WithMany(p => p.AccountTransactionDetails)
                //    .HasForeignKey(d => d.BudgetID)
                //    .HasConstraintName("FK_AccountTransac_Budgets");

                //entity.HasOne(d => d.Department)
                //    .WithMany(p => p.AccountTransactionDetails)
                //    .HasForeignKey(d => d.DepartmentID)
                //    .HasConstraintName("FK_AccountTransactionDet_Departments");

                //entity.HasOne(d => d.ProductSKU)
                //    .WithMany(p => p.AccountTransactionDetails)
                //    .HasForeignKey(d => d.ProductSKUId)
                //    .HasConstraintName("FK_AccountTransactionDetails_ProductSKUMaps");

                //entity.HasOne(d => d.SubLedger)
                //    .WithMany(p => p.AccountTransactionDetails)
                //    .HasForeignKey(d => d.SubLedgerID)
                //    .HasConstraintName("FK_AccountTransactionDetails_Accounts_SubLedger");
            });

            modelBuilder.Entity<AccountTransactionHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactionHeads_Accounts");

                //entity.HasOne(d => d.Branch)
                //    .WithMany(p => p.AccountTransactionHeads)
                //    .HasForeignKey(d => d.BranchID)
                //    .HasConstraintName("FK_AccountTransactionHeads_Branches");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_AccountTransactionHeads_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AccountTransactionHeads_DocumentTypes");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_AccountTransactionHeads_TransactionStatuses");
            });

            modelBuilder.Entity<AccountTransactionHeadAccountMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionHeadAccountMaps_AccountTransactionHeads");

                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.AccountTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionHeadAccountMaps_AccountTransactions");
            });
            modelBuilder.Entity<AccountTransactionReceivablesMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionReceivablesMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionReceivablesMap_TransactionHead");

                entity.HasOne(d => d.Receivable)
                    .WithMany(p => p.AccountTransactionReceivablesMaps)
                    .HasForeignKey(d => d.ReceivableID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionReceivablesMap_Receivables");
            });
            modelBuilder.Entity<Rule>(entity =>
            {
                entity.Property(e => e.RuleID).ValueGeneratedNever();

                entity.HasOne(d => d.PatternType)
                    .WithMany(p => p.Rules)
                    .HasForeignKey(d => d.PatternTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rule_PatternType");

                entity.HasOne(d => d.RuleSet)
                    .WithMany(p => p.Rules)
                    .HasForeignKey(d => d.RuleSetID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rule_RuleSet");

                entity.HasOne(d => d.RuleType)
                    .WithMany(p => p.Rules)
                    .HasForeignKey(d => d.RuleTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rule_RuleType");
            });

            modelBuilder.Entity<RuleSet>(entity =>
            {
                entity.Property(e => e.RuleSetID).ValueGeneratedNever();
            });

            modelBuilder.Entity<RuleType>(entity =>
            {
                entity.Property(e => e.RuleTypeID).ValueGeneratedNever();
            });
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.Balance).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyID).HasDefaultValueSql("((0))");

                entity.Property(e => e.InterestRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJointAccount).HasDefaultValueSql("((0))");

                entity.Property(e => e.OverdraftLimit).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_BankAccounts_Accounts");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.AccountTypeID)
                    .HasConstraintName("FK_BankAccounts_BankAccountTypes");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_BankAccounts_Banks");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_BankAccounts_Currencies");

                entity.HasOne(d => d.DataExtractionRuleSet)
                     .WithMany(p => p.BankAccountDataExtractionRuleSets)
                     .HasForeignKey(d => d.DataExtractionRuleSetID)
                     .HasConstraintName("FK_BankAccounts_DataExtractionRuleSet");

                entity.HasOne(d => d.MatchingRuleSet)
                    .WithMany(p => p.BankAccountMatchingRuleSets)
                    .HasForeignKey(d => d.MatchingRuleSetID)
                    .HasConstraintName("FK_BankAccounts_MatchingRuleSet");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_BankAccounts_BankAccountStatuses");
            });

            modelBuilder.Entity<BankAccountStatus>(entity =>
            {
                entity.HasKey(e => e.StatusID)
                    .HasName("PK__BankAcco__C8EE20431C8BF0A7");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<BankAccountType>(entity =>
            {
                entity.HasKey(e => e.AccountTypeID)
                    .HasName("PK__BankAcco__8F95854FDC775D39");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ChartOfAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ChartOfAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_ChartOfAccountMaps_Accounts");

                //entity.HasOne(d => d.ChartOfAccount)
                //    .WithMany(p => p.ChartOfAccountMaps)
                //    .HasForeignKey(d => d.ChartOfAccountID)
                //    .HasConstraintName("FK_ChartOfAccountMaps_ChartOfAccounts");

                //entity.HasOne(d => d.ChartRowType)
                //    .WithMany(p => p.ChartOfAccountMaps)
                //    .HasForeignKey(d => d.ChartRowTypeID)
                //    .HasConstraintName("FK_ChartOfAccountMaps_ChartRowTypes");
            });

            modelBuilder.Entity<CustomerAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CustomerAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_CustomerAccountMaps_Accounts");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.CustomerAccountMaps)
                //    .HasForeignKey(d => d.CustomerID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_CustomerAccountMaps_Customers");

                entity.HasOne(d => d.Entitlement)
                    .WithMany(p => p.CustomerAccountMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_CustomerAccountMaps_EntityTypeEntitlements");
            });

            modelBuilder.Entity<Payable>(entity =>
            {
                entity.HasKey(e => e.PayableIID)
                    .HasName("PK_Receivable1");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_Payables_Accounts");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_Payables_Currencies");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_Payables_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_Payables_Payables1");

                entity.HasOne(d => d.ReferencePayables)
                    .WithMany(p => p.InverseReferencePayables)
                    .HasForeignKey(d => d.ReferencePayablesID)
                    .HasConstraintName("FK_Payables_Payables");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_Payables_TransactionStatuses");
            });

            modelBuilder.Entity<PaymentMode>(entity =>
            {
                entity.Property(e => e.PaymentModeID).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.PaymentModes)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_PaymentModes_Accounts");

                //entity.HasOne(d => d.TenderType)
                //    .WithMany(p => p.PaymentModes)
                //    .HasForeignKey(d => d.TenderTypeID)
                //    .HasConstraintName("FK_PaymentModes_TenderTypes");
            });

            modelBuilder.Entity<Receivable>(entity =>
            {
                entity.HasKey(e => e.ReceivableIID)
                    .HasName("PK_Receivable");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_Receivables_Accounts");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_Receivables_Currencies");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_Receivables_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_Receivables_DocumentTypes");

                entity.HasOne(d => d.ReferenceReceivables)
                    .WithMany(p => p.InverseReferenceReceivables)
                    .HasForeignKey(d => d.ReferenceReceivablesID)
                    .HasConstraintName("FK_Receivables_Receivables");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_Receivables_TransactionStatuses");
            });

            modelBuilder.Entity<SupplierAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SupplierAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_SupplierAccountMaps_Accounts");

                entity.HasOne(d => d.Entitlement)
                    .WithMany(p => p.SupplierAccountMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_SupplierAccountMaps_EntityTypeEntitlements");

                //entity.HasOne(d => d.Supplier)
                //    .WithMany(p => p.SupplierAccountMaps)
                //    .HasForeignKey(d => d.SupplierID)
                //    .HasConstraintName("FK_SupplierAccountMaps_Suppliers");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasOne(d => d.AccumulatedDepGLAcc)
                    .WithMany(p => p.AssetAccumulatedDepGLAccs)
                    .HasForeignKey(d => d.AccumulatedDepGLAccID)
                    .HasConstraintName("FK_Assets_Accounts1");

                entity.HasOne(d => d.AssetCategory)
                    .WithMany(p => p.AssetAssetCategories)
                    .HasForeignKey(d => d.AssetCategoryID)
                    .HasConstraintName("FK_Asset_AssetCategory");

                entity.HasOne(d => d.AssetGlAcc)
                    .WithMany(p => p.AssetAssetGlAccs)
                    .HasForeignKey(d => d.AssetGlAccID)
                    .HasConstraintName("FK_Assets_Accounts");

                entity.HasOne(d => d.AssetGroup)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetGroupID)
                    .HasConstraintName("FK_Asset_AssetGroups");

                entity.HasOne(d => d.AssetSubCategory)
                    .WithMany(p => p.AssetAssetSubCategories)
                    .HasForeignKey(d => d.AssetSubCategoryID)
                    .HasConstraintName("FK_Asset_AssetSubCategories");

                entity.HasOne(d => d.DepreciationExpGLAcc)
                    .WithMany(p => p.AssetDepreciationExpGLAccs)
                    .HasForeignKey(d => d.DepreciationExpGLAccId)
                    .HasConstraintName("FK_Assets_Accounts2");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_Asset_Units");
            });

            modelBuilder.Entity<AssetCategory>(entity =>
            {
                entity.Property(e => e.AssetCategoryID).ValueGeneratedNever();

                entity.HasOne(d => d.DepreciationPeriod)
                    .WithMany(p => p.AssetCategories)
                    .HasForeignKey(d => d.DepreciationPeriodID)
                    .HasConstraintName("FK_AssetCategory_DepreciationPeriod");
            });

            modelBuilder.Entity<AssetGroup>(entity =>
            {
                entity.Property(e => e.AssetGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AssetSerialMap>(entity =>
            {
                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetSerialMaps)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetSerialMaps_Asset");

                entity.HasOne(d => d.AssetInventory)
                    .WithMany(p => p.AssetSerialMaps)
                    .HasForeignKey(d => d.AssetInventoryID)
                    .HasConstraintName("FK_AssetSerialMaps_AssetInventory");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.AssetSerialMaps)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_AssetSerialMaps_Supplier");
            });

            modelBuilder.Entity<AssetTransactionDetail>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AssetTransactionDetails_Account");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetTransactionDetails_Assets");

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_AssetTransactionDetails_AssetTransactionHead");
            });

            modelBuilder.Entity<AssetTransactionHead>(entity =>
            {
                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.AssetTransactionHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_AssetTransactionHead_DocumentReferenceStatusMap");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AssetTransactionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AssetTransactionHead_DocumentTypes");

                entity.HasOne(d => d.ProcessingStatus)
                    .WithMany(p => p.AssetTransactionHeads)
                    .HasForeignKey(d => d.ProcessingStatusID)
                    .HasConstraintName("FK_AssetTransactionHead_TransactionStatuses");
            });

            modelBuilder.Entity<AssetTransactionHeadAccountMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.AssetTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransactionHeadAccountMaps_AccountTransactions");

                entity.HasOne(d => d.AssetTransactionHead)
                    .WithMany(p => p.AssetTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AssetTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransactionHeadAccountMaps_AssetTransactionHead");
            });

            modelBuilder.Entity<AssetTransactionSerialMap>(entity =>
            {
                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetTransactionSerialMaps)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetTransactionSerialMaps_Asset");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.AssetTransactionSerialMaps)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_AssetTransactionSerialMaps_Supplier");

                entity.HasOne(d => d.TransactionDetail)
                    .WithMany(p => p.AssetTransactionSerialMaps)
                    .HasForeignKey(d => d.TransactionDetailID)
                    .HasConstraintName("FK_AssetTransactionSerialMaps_AssetTransactionDetail");
            });

            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.Property(e => e.AssetTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<BudgetEntryAccountMap>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BudgetEntryAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_BudgetEntryAccountMaps_Accounts");

                //entity.HasOne(d => d.BudgetEntry)
                //    .WithMany(p => p.BudgetEntryAccountMaps)
                //    .HasForeignKey(d => d.BudgetEntryID)
                //    .HasConstraintName("FK_BudgetEntryAccountMaps_BudgetEntry");

                //entity.HasOne(d => d.CostCenter)
                //    .WithMany(p => p.BudgetEntryAccountMaps)
                //    .HasForeignKey(d => d.CostCenterID)
                //    .HasConstraintName("FK_BudgetEntryAccountMaps_CostCenter");

                //entity.HasOne(d => d.Group)
                //    .WithMany(p => p.BudgetEntryAccountMaps)
                //    .HasForeignKey(d => d.GroupID)
                //    .HasConstraintName("FK_BudgetEntryAccountMaps_Groups");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.SEOMetadata)
                //    .WithMany(p => p.Brands)
                //    .HasForeignKey(d => d.SEOMetadataID)
                //    .HasConstraintName("FK_Brands_SEOMetaDataID");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Brands)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Brands_BrandStatuses");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_Products_Brands");

                entity.HasOne(d => d.GLAccount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.GLAccountID)
                    .HasConstraintName("FK_Products_GLAccount");

                //entity.HasOne(d => d.ProductFamily)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.ProductFamilyID)
                //    .HasConstraintName("FK_Products_ProductFamilies");

                //entity.HasOne(d => d.PurchaseUnitGroup)
                //    .WithMany(p => p.ProductPurchaseUnitGroups)
                //    .HasForeignKey(d => d.PurchaseUnitGroupID)
                //    .HasConstraintName("FK_Products_PurchaseUnitGroup");

                entity.HasOne(d => d.Unit)
                                  .WithMany(p => p.ProductPurchaseUnits)
                                  .HasForeignKey(d => d.PurchaseUnitID)
                                  .HasConstraintName("FK_Products_PurchaseUnit");


                entity.HasOne(d => d.Unit1)
                    .WithMany(p => p.ProductSellingUnits)
                    .HasForeignKey(d => d.SellingUnitID)
                    .HasConstraintName("FK_Products_SellingUnit");
                //entity.HasOne(d => d.SeoMetadataI)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.SeoMetadataIID)
                //    .HasConstraintName("FK_Products_SeoMetadatas");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Products_ProductStatus");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_Products_TaxTemplates");

                //entity.HasOne(d => d.UnitGroup)
                //    .WithMany(p => p.ProductUnitGroups)
                //    .HasForeignKey(d => d.UnitGroupID)
                //    .HasConstraintName("FK_Products_UnitGroups");

                //entity.HasOne(d => d.Unit)
                //    .WithMany(p => p.ProductUnits)
                //    .HasForeignKey(d => d.UnitID)
                //    .HasConstraintName("FK_Products_Units");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.Property(e => e.TaxID).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_Taxes_Accounts");

                entity.HasOne(d => d.TaxStatus)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.TaxStatusID)
                    .HasConstraintName("FK_Taxes_TaxStatuses");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_Taxes_TaxTypes");
            });

            modelBuilder.Entity<TaxStatus>(entity =>
            {
                entity.Property(e => e.TaxStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TaxTemplate>(entity =>
            {
                entity.Property(e => e.TaxTemplateID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TaxTemplateItem>(entity =>
            {
                entity.Property(e => e.TaxTemplateItemID).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TaxTemplateItems)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_TaxTemplateItems_Accounts");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.TaxTemplateItems)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_TaxTemplateItems_TaxTemplates");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.TaxTemplateItems)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_TaxTemplateItems_TaxTypes");
            });

            modelBuilder.Entity<TaxTransaction>(entity =>
            {
                entity.HasOne(d => d.Accound)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.AccoundID)
                    .HasConstraintName("FK_TaxTransactions_Accounts");

                //entity.HasOne(d => d.Head)
                //    .WithMany(p => p.TaxTransactions)
                //    .HasForeignKey(d => d.HeadID)
                //    .HasConstraintName("FK_TaxTransactions_TransactionHead");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_TaxTransactions_TaxTemplates");

                entity.HasOne(d => d.TaxTemplateItem)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateItemID)
                    .HasConstraintName("FK_TaxTransactions_TaxTemplateItems");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_TaxTransactions_TaxTypes");
            });

            modelBuilder.Entity<TaxType>(entity =>
            {
                entity.Property(e => e.TaxTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.BankIID)
                    .HasName("PK_BanksNames");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryID).ValueGeneratedNever();

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_Countries_Currencies");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyID).ValueGeneratedNever();

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Currencies)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Currencies_Companies");
            });

            modelBuilder.Entity<DocumentReferenceStatusMap>(entity =>
            {
                entity.Property(e => e.DocumentReferenceStatusMapID).ValueGeneratedNever();

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.DocumentReferenceStatusMaps)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentReferenceStatusMap_DocumentStatuses");

                entity.HasOne(d => d.ReferenceType)
                    .WithMany(p => p.DocumentReferenceStatusMaps)
                    .HasForeignKey(d => d.ReferenceTypeID)
                    .HasConstraintName("FK_DocumentReferenceStatusMap_DocumentReferenceTypes");
            });

            modelBuilder.Entity<DocumentReferenceType>(entity =>
            {
                entity.HasKey(e => e.ReferenceTypeID)
                    .HasName("PK_InventoryTypes");

                entity.Property(e => e.ReferenceTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.Property(e => e.DocumentStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DocumentTypeID).ValueGeneratedNever();

                entity.Property(e => e.IsExternal).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ReferenceType)
                    .WithMany(p => p.DocumentTypes)
                    .HasForeignKey(d => d.ReferenceTypeID)
                    .HasConstraintName("FK_DocumentTypes_DocumentReferenceTypes");

                //entity.HasOne(d => d.TaxTemplate)
                //    .WithMany(p => p.DocumentTypes)
                //    .HasForeignKey(d => d.TaxTemplateID)
                //    .HasConstraintName("FK_DocumentTypes_TaxTemplates");

                //entity.HasOne(d => d.Workflow)
                //    .WithMany(p => p.DocumentTypes)
                //    .HasForeignKey(d => d.WorkflowID)
                //    .HasConstraintName("FK_DocumentTypes_Workflows");
            });

            modelBuilder.Entity<EntitlementMap>(entity =>
            {
                entity.HasKey(e => e.EntitlementMapIID)
                    .HasName("PK_EntityTypeEntitlementMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Entitlement)
                    .WithMany(p => p.EntitlementMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_EntitlementMaps_EntityTypeEntitlements");
            });

            modelBuilder.Entity<EntityTypeEntitlement>(entity =>
            {
                entity.HasKey(e => e.EntitlementID)
                    .HasName("PK_Entitlements");

                //entity.HasOne(d => d.EntityType)
                //    .WithMany(p => p.EntityTypeEntitlements)
                //    .HasForeignKey(d => d.EntityTypeID)
                //    .HasConstraintName("FK_Entitlements_EntityTypes");
            });

            modelBuilder.Entity<EmployeePromotion>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.EmployeePromotions)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_EmployeePromotions_Accounts");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.EmployeePromotions)
                //    .HasForeignKey(d => d.EmployeeID)
                //    .HasConstraintName("FK_EmployeePromotions_Employees");

                //entity.HasOne(d => d.NewBranch)
                //    .WithMany(p => p.EmployeePromotionNewBranches)
                //    .HasForeignKey(d => d.NewBranchID)
                //    .HasConstraintName("FK_EmployeePromotions_NewBranches");

                //entity.HasOne(d => d.NewDesignation)
                //    .WithMany(p => p.EmployeePromotionNewDesignations)
                //    .HasForeignKey(d => d.NewDesignationID)
                //    .HasConstraintName("FK_EmployeePromotions_NewDesignations");

                entity.HasOne(d => d.NewLeaveGroup)
                    .WithMany(p => p.EmployeePromotionNewLeaveGroups)
                    .HasForeignKey(d => d.NewLeaveGroupID)
                    .HasConstraintName("FK_EmployeePromotions_LeaveGroups");

                //entity.HasOne(d => d.NewRole)
                //    .WithMany(p => p.EmployeePromotionNewRoles)
                //    .HasForeignKey(d => d.NewRoleID)
                //    .HasConstraintName("FK_EmployeePromotions_NewRoles");

                entity.HasOne(d => d.NewSalaryStructure)
                    .WithMany(p => p.EmployeePromotionNewSalaryStructures)
                    .HasForeignKey(d => d.NewSalaryStructureID)
                    .HasConstraintName("FK_EmployeePromotions_NewSalaryStructure");

                //entity.HasOne(d => d.OldBranch)
                //    .WithMany(p => p.EmployeePromotionOldBranches)
                //    .HasForeignKey(d => d.OldBranchID)
                //    .HasConstraintName("FK_EmployeePromotions_OldBranches");

                //entity.HasOne(d => d.OldDesignation)
                //    .WithMany(p => p.EmployeePromotionOldDesignations)
                //    .HasForeignKey(d => d.OldDesignationID)
                //    .HasConstraintName("FK_EmployeePromotions_OldDesignations");

                entity.HasOne(d => d.OldLeaveGroup)
                    .WithMany(p => p.EmployeePromotionOldLeaveGroups)
                    .HasForeignKey(d => d.OldLeaveGroupID)
                    .HasConstraintName("FK_EmployeePromotions_OldLeaveGroups");

                //entity.HasOne(d => d.OldRole)
                //    .WithMany(p => p.EmployeePromotionOldRoles)
                //    .HasForeignKey(d => d.OldRoleID)
                //    .HasConstraintName("FK_EmployeePromotions_OldRoles");

                entity.HasOne(d => d.OldSalaryStructure)
                    .WithMany(p => p.EmployeePromotionOldSalaryStructures)
                    .HasForeignKey(d => d.OldSalaryStructureID)
                    .HasConstraintName("FK_EmployeePromotions_OldSalaryStructure");

                //entity.HasOne(d => d.PaymentMode)
                //    .WithMany(p => p.EmployeePromotions)
                //    .HasForeignKey(d => d.PaymentModeID)
                //    .HasConstraintName("FK_EmployeePromotions_SalaryPaymentModes");

                //entity.HasOne(d => d.PayrollFrequency)
                //    .WithMany(p => p.EmployeePromotions)
                //    .HasForeignKey(d => d.PayrollFrequencyID)
                //    .HasConstraintName("FK_EmployeePromotions_PayrollFrequencies");

                entity.HasOne(d => d.SalaryStructure)
                    .WithMany(p => p.EmployeePromotionSalaryStructures)
                    .HasForeignKey(d => d.SalaryStructureID)
                    .HasConstraintName("FK_EmployeePromotions_SalaryStructure");

                //entity.HasOne(d => d.TimeSheetSalaryComponent)
                //    .WithMany(p => p.EmployeePromotions)
                //    .HasForeignKey(d => d.TimeSheetSalaryComponentID)
                //    .HasConstraintName("FK_EmployeePromotions_SalaryComponents");
            });

            modelBuilder.Entity<EmployeePromotionLeaveAllocation>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EmployeePromotion)
                    .WithMany(p => p.EmployeePromotionLeaveAllocations)
                    .HasForeignKey(d => d.EmployeePromotionID)
                    .HasConstraintName("FK_EmpLeavePromoLeavAllocations_LeaveTypes");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.EmployeePromotionLeaveAllocations)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_EmployeePromoLeaveAllocons_LeaveTypes");
            });

            modelBuilder.Entity<EmployeeSalaryStructure>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.EmployeeSalaryStructures)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_Accounts");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.EmployeeSalaryStructures)
                //    .HasForeignKey(d => d.EmployeeID)
                //    .HasConstraintName("FK_EmployeeSalaryStructures_Employees");

                entity.HasOne(d => d.LeaveSalaryStructure)
                    .WithMany(p => p.EmployeeSalaryStructureLeaveSalaryStructures)
                    .HasForeignKey(d => d.LeaveSalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_LeaveSalaryStructure");

                //entity.HasOne(d => d.PaymentMode)
                //    .WithMany(p => p.EmployeeSalaryStructures)
                //    .HasForeignKey(d => d.PaymentModeID)
                //    .HasConstraintName("FK_EmployeeSalaryStructures_SalaryPaymentModes");

                //entity.HasOne(d => d.PayrollFrequency)
                //    .WithMany(p => p.EmployeeSalaryStructures)
                //    .HasForeignKey(d => d.PayrollFrequencyID)
                //    .HasConstraintName("FK_EmployeeSalaryStructures_PayrollFrequencies");

                entity.HasOne(d => d.SalaryStructure)
                    .WithMany(p => p.EmployeeSalaryStructureSalaryStructures)
                    .HasForeignKey(d => d.SalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_SalaryStructure");

                //entity.HasOne(d => d.TimeSheetSalaryComponent)
                //    .WithMany(p => p.EmployeeSalaryStructures)
                //    .HasForeignKey(d => d.TimeSheetSalaryComponentID)
                //    .HasConstraintName("FK_EmployeeSalaryStructures_SalaryComponents");
            });

            modelBuilder.Entity<LeaveGroup>(entity =>
            {
                entity.Property(e => e.LeaveGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.LeaveTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalaryStructure>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalaryStructures)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_SalaryStructure_Accounts");

                //entity.HasOne(d => d.PaymentMode)
                //    .WithMany(p => p.SalaryStructures)
                //    .HasForeignKey(d => d.PaymentModeID)
                //    .HasConstraintName("FK_SalaryStructure_SalaryPaymentModes");

                //entity.HasOne(d => d.PayrollFrequency)
                //    .WithMany(p => p.SalaryStructures)
                //    .HasForeignKey(d => d.PayrollFrequencyID)
                //    .HasConstraintName("FK_SalaryStructure_PayrollFrequencies");

                //entity.HasOne(d => d.TimeSheetSalaryComponent)
                //    .WithMany(p => p.SalaryStructures)
                //    .HasForeignKey(d => d.TimeSheetSalaryComponentID)
                //    .HasConstraintName("FK_SalaryStructure_SalaryComponents");
            });

            modelBuilder.Entity<FeeMaster>(entity =>
            {
                entity.Property(e => e.FeeMasterID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.FeeMasters)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_FeeMasters_AcademicYear");

                entity.HasOne(d => d.AdvanceAccount)
                    .WithMany(p => p.FeeMasterAdvanceAccounts)
                    .HasForeignKey(d => d.AdvanceAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts3");

                entity.HasOne(d => d.AdvanceTaxAccount)
                    .WithMany(p => p.FeeMasterAdvanceTaxAccounts)
                    .HasForeignKey(d => d.AdvanceTaxAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts5");

                //entity.HasOne(d => d.FeeCycle)
                //    .WithMany(p => p.FeeMasters)
                //    .HasForeignKey(d => d.FeeCycleID)
                //    .HasConstraintName("FK__FeeMaster__FeeCy__1A9FBED1");

                //entity.HasOne(d => d.FeeType)
                //    .WithMany(p => p.FeeMasters)
                //    .HasForeignKey(d => d.FeeTypeID)
                //    .HasConstraintName("FK_FeeMasters_FeeTypes");

                entity.HasOne(d => d.LedgerAccount)
                    .WithMany(p => p.FeeMasterLedgerAccounts)
                    .HasForeignKey(d => d.LedgerAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts");

                entity.HasOne(d => d.OSTaxAccount)
                    .WithMany(p => p.FeeMasterOSTaxAccounts)
                    .HasForeignKey(d => d.OSTaxAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts4");

                entity.HasOne(d => d.OutstandingAccount)
                    .WithMany(p => p.FeeMasterOutstandingAccounts)
                    .HasForeignKey(d => d.OutstandingAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts2");

                entity.HasOne(d => d.ProvisionforAdvanceAccount)
                    .WithMany(p => p.FeeMasterProvisionforAdvanceAccounts)
                    .HasForeignKey(d => d.ProvisionforAdvanceAccountID)
                    .HasConstraintName("FK_FeeMasters_ProvisforAdvanAcc");

                entity.HasOne(d => d.ProvisionforOutstandingAccount)
                    .WithMany(p => p.FeeMasterProvisionforOutstandingAccounts)
                    .HasForeignKey(d => d.ProvisionforOutstandingAccountID)
                    .HasConstraintName("FK_FeeMasters_ProvisforOutSAcc");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.FeeMasters)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_FeeMasters_School");

                entity.HasOne(d => d.TaxLedgerAccount)
                    .WithMany(p => p.FeeMasterTaxLedgerAccounts)
                    .HasForeignKey(d => d.TaxLedgerAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts1");
            });

            modelBuilder.Entity<FineMaster>(entity =>
            {
                entity.Property(e => e.FineMasterID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.FineMasters)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_FineMasters_AcademicYear");

                //entity.HasOne(d => d.FeeFineType)
                //    .WithMany(p => p.FineMasters)
                //    .HasForeignKey(d => d.FeeFineTypeID)
                //    .HasConstraintName("FK_FineMasters_FeeFineTypes");

                entity.HasOne(d => d.LedgerAccount)
                    .WithMany(p => p.FineMasters)
                    .HasForeignKey(d => d.LedgerAccountID)
                    .HasConstraintName("FK_FineMasters_Accounts");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.FineMasters)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_FineMasters_School");
            });

            modelBuilder.Entity<PackageConfig>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcadamicYear)
                //    .WithMany(p => p.PackageConfigs)
                //    .HasForeignKey(d => d.AcadamicYearID)
                //    .HasConstraintName("FK_PackageConfig_AcademicYears");

                entity.HasOne(d => d.CreditNoteAccount)
                    .WithMany(p => p.PackageConfigs)
                    .HasForeignKey(d => d.CreditNoteAccountID)
                    .HasConstraintName("FK_PackageConfig_Account");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.PackageConfigs)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_PackageConfig_School");
            });

            modelBuilder.Entity<PackageConfigClassMap>(entity =>
            {
                entity.HasKey(e => e.PackageConfigClassMapIID)
                    .HasName("PK_PackageConfigClassMap");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.PackageConfigClassMaps)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_PackageConfigClassMaps_Classes");

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigClassMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigClassMaps_PackageConfig");
            });

            modelBuilder.Entity<PackageConfigFeeStructureMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.FeeStructure)
                //    .WithMany(p => p.PackageConfigFeeStructureMaps)
                //    .HasForeignKey(d => d.FeeStructureID)
                //    .HasConstraintName("FK_PackageConfigFeeStructureMaps_FeeStructures");

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigFeeStructureMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigFeeStructureMaps_PackageConfig");
            });

            modelBuilder.Entity<PackageConfigStudentGroupMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigStudentGroupMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigStudentGroupMaps_PackageConfig");

                //entity.HasOne(d => d.StudentGroup)
                //    .WithMany(p => p.PackageConfigStudentGroupMaps)
                //    .HasForeignKey(d => d.StudentGroupID)
                //    .HasConstraintName("FK_PackageConfigStudentGroupMaps_StudentGroups");
            });

            modelBuilder.Entity<PackageConfigStudentMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigStudentMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigStudentMaps_PackageConfig");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.PackageConfigStudentMaps)
                //    .HasForeignKey(d => d.StudentID)
                //    .HasConstraintName("FK_PackageConfigStudentMaps_Students");
            });

            modelBuilder.Entity<Department1>(entity =>
            {
                entity.Property(e => e.DepartmentID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Department1)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Departments_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Department1)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Departments_School");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.UnitGroup)
                //    .WithMany(p => p.Units)
                //    .HasForeignKey(d => d.UnitGroupID)
                //    .HasConstraintName("FK_Units_UnitGroup");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.BlockedBranch)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.BlockedBranchID)
                //    .HasConstraintName("FK_Suppliers_Branches");

                //entity.HasOne(d => d.BusinessType)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.BusinessTypeID)
                //    .HasConstraintName("FK_Supplier_BusinessType");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Suppliers_CompanyID");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.EmployeeID)
                //    .HasConstraintName("FK_Suppliers_Employees");

                //entity.HasOne(d => d.Login)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.LoginID)
                //    .HasConstraintName("FK_Suppliers_Logins");

                //entity.HasOne(d => d.ReceivingMethod)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.ReceivingMethodID)
                //    .HasConstraintName("FK_Suppliers_ReceivingMethods");

                //entity.HasOne(d => d.ReturnMethod)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.ReturnMethodID)
                //    .HasConstraintName("FK_Suppliers_ReturnMethods");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Suppliers_SupplierStatuses");

                entity.HasOne(d => d.TaxJurisdictionCountry)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.TaxJurisdictionCountryID)
                    .HasConstraintName("FK_Supplier_TaxJurisdictionCountry");
            });

            modelBuilder.Entity<AssetProductMap>(entity =>
            {
                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetProductMaps)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetProductMaps_Asset");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.AssetProductMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_AssetProductMaps_ProductSKUMap");
            });

            modelBuilder.Entity<ProductSKUMap>(entity =>
            {
                entity.Property(e => e.ProductSKUMapIIDTEXT).HasComputedColumnSql("(CONVERT([varchar](20),[ProductSKUMapIID],(0)))", true);

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSKUMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductSKUMaps_ProductSKUMaps");

                //entity.HasOne(d => d.SeoMetadata)
                //    .WithMany(p => p.ProductSKUMaps)
                //    .HasForeignKey(d => d.SeoMetadataID)
                //    .HasConstraintName("FK_ProductSKUMaps_SEOMetaDataID");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.ProductSKUMaps)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_ProductSKUMaps_ProductStatus");
            });

            modelBuilder.Entity<AssetInventory>(entity =>
            {
                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetInventories)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetInventories_Asset");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AssetInventories)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_AssetInventories_Branch");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AssetInventories)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_AssetInventories_Companies");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.BranchGroup)
                //    .WithMany(p => p.Branches)
                //    .HasForeignKey(d => d.BranchGroupID)
                //    .HasConstraintName("FK_Branches_BranchGroups");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Branches)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Branches_BranchStatuses");

                //entity.HasOne(d => d.Warehouse)
                //    .WithMany(p => p.Branches)
                //    .HasForeignKey(d => d.WarehouseID)
                //    .HasConstraintName("FK_Branches_Warehouses");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.BaseCurrency)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.BaseCurrencyID)
                //    .HasConstraintName("FK_Companies_Currencies");

                //entity.HasOne(d => d.CompanyGroup)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.CompanyGroupID)
                //    .HasConstraintName("FK_Companies_CompanyGroups");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Companies_Companies");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Companies_CompanyStatuses");
            });

            modelBuilder.Entity<DepreciationPeriod>(entity =>
            {
                entity.Property(e => e.PeriodID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DepreciationType>(entity =>
            {
                entity.Property(e => e.DepreciationTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AssetInventoryTransaction>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetInventoryTransactions)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetInventoryTransactions_Asset");

                entity.HasOne(d => d.AssetSerialMap)
                    .WithMany(p => p.AssetInventoryTransactions)
                    .HasForeignKey(d => d.AssetSerialMapID)
                    .HasConstraintName("FK_AssetInventoryTransactions_AssetSerialMap");

                entity.HasOne(d => d.AssetTransactionHead)
                    .WithMany(p => p.AssetInventoryTransactions)
                    .HasForeignKey(d => d.AssetTransactionHeadID)
                    .HasConstraintName("FK_AssetInventoryTransactions_AssetTransactionHead");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AssetInventoryTransactions)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_AssetInventoryTransactions_Branch");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AssetInventoryTransactions)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_AssetInventoryTransactions_Company");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AssetInventoryTransactions)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AssetInventoryTransactions_DocumentType");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.BasketID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<JobActivity>(entity =>
            {
                entity.Property(e => e.JobActivityID).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobEntryDetail>(entity =>
            {
                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.JobEntryHead)
                    .WithMany(p => p.JobEntryDetailJobEntryHeads)
                    .HasForeignKey(d => d.JobEntryHeadID)
                    .HasConstraintName("FK_JobEntryDetails_JobEntryHeads");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.JobEntryDetails)
                    .HasForeignKey(d => d.JobStatusID)
                    .HasConstraintName("FK_JobEntryDetails_JobStatuses");

                entity.HasOne(d => d.ParentJobEntryHead)
                    .WithMany(p => p.JobEntryDetailParentJobEntryHeads)
                    .HasForeignKey(d => d.ParentJobEntryHeadID)
                    .HasConstraintName("FK_JobEntryDetails_JobEntryHeads1");

                //entity.HasOne(d => d.ProductSKU)
                //    .WithMany(p => p.JobEntryDetails)
                //    .HasForeignKey(d => d.ProductSKUID)
                //    .HasConstraintName("FK_JobEntryDetails_ProductSKUMaps");
            });

            modelBuilder.Entity<JobEntryHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.BasketID)
                    .HasConstraintName("FK_JobEntryHeads_Baskets");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_JobEntryHeads_Branches");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.JobEntryHeadDocumentTypes)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_JobEntryHeads_DocumentTypes1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_JobEntryHeads_Employees");

                entity.HasOne(d => d.JobActivity)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobActivityID)
                    .HasConstraintName("FK_JobEntryHeads_JobActivities");

                entity.HasOne(d => d.JobOperationStatus)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobOperationStatusID)
                    .HasConstraintName("FK_JobEntryHeads_JobOperationStatuses");

                entity.HasOne(d => d.JobSize)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobSizeID)
                    .HasConstraintName("FK_JobEntryHeads_JobSizes");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobStatusID)
                    .HasConstraintName("FK_JobEntryHeads_JobStatuses");

                //entity.HasOne(d => d.OrderContactMap)
                //    .WithMany(p => p.JobEntryHeads)
                //    .HasForeignKey(d => d.OrderContactMapID)
                //    .HasConstraintName("FK_JobEntryHeads_OrderContactMaps");

                entity.HasOne(d => d.ParentJobEntryHead)
                    .WithMany(p => p.InverseParentJobEntryHead)
                    .HasForeignKey(d => d.ParentJobEntryHeadId)
                    .HasConstraintName("FK_JobEntryHeads_JobEntryHeads");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.PriorityID)
                    .HasConstraintName("FK_JobEntryHeads_Priorities");

                entity.HasOne(d => d.ReferenceDocumentType)
                    .WithMany(p => p.JobEntryHeadReferenceDocumentTypes)
                    .HasForeignKey(d => d.ReferenceDocumentTypeID)
                    .HasConstraintName("FK_JobEntryHeads_DocumentTypes");

                //entity.HasOne(d => d.TransactionHead)
                //    .WithMany(p => p.JobEntryHeads)
                //    .HasForeignKey(d => d.TransactionHeadID)
                //    .HasConstraintName("FK_JobEntryHeads_TransactionHead");

                //entity.HasOne(d => d.Vehicle)
                //    .WithMany(p => p.JobEntryHeads)
                //    .HasForeignKey(d => d.VehicleID)
                //    .HasConstraintName("FK_JobEntryHeads_Vehicles");
            });

            modelBuilder.Entity<JobsEntryHeadPayableMap>(entity =>
            {
                entity.HasOne(d => d.JobEntryHead)
                    .WithMany(p => p.JobsEntryHeadPayableMaps)
                    .HasForeignKey(d => d.JobEntryHeadID)
                    .HasConstraintName("FK_JobsEntryHeadPayableMaps_JobEntryHeads");

                entity.HasOne(d => d.Payable)
                    .WithMany(p => p.JobsEntryHeadPayableMaps)
                    .HasForeignKey(d => d.PayableID)
                    .HasConstraintName("FK_JobsEntryHeadPayableMaps_Payables");
            });

            modelBuilder.Entity<JobsEntryHeadReceivableMap>(entity =>
            {
                entity.HasOne(d => d.JobEntryHead)
                    .WithMany(p => p.JobsEntryHeadReceivableMaps)
                    .HasForeignKey(d => d.JobEntryHeadID)
                    .HasConstraintName("FK_JobsEntryHeadReceivableMaps_JobEntryHeads");

                entity.HasOne(d => d.Receivable)
                    .WithMany(p => p.JobsEntryHeadReceivableMaps)
                    .HasForeignKey(d => d.ReceivableID)
                    .HasConstraintName("FK_JobsEntryHeadReceivableMaps_Receivables");
            });

            modelBuilder.Entity<JobSize>(entity =>
            {
                entity.Property(e => e.JobSizeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.Property(e => e.JobStatusID).ValueGeneratedNever();

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.JobStatus)
                    .HasForeignKey(d => d.JobTypeID)
                    .HasConstraintName("FK_JobStatuses_JobTypes");
            });
            modelBuilder.Entity<TransactionAllocationHead>(entity =>
            {
                entity.HasKey(e => e.TransactionAllocationIID)
                    .HasName("PK__Transact__2DF3FD4E4857A462");

                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });


            modelBuilder.Entity<TransactionAllocationDetail>(entity =>
            {
                entity.HasKey(e => e.TransactionAllocationDetailIID)
                    .HasName("PK__Transact__993A786B76360AF9");

                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TransactionAllocationDetailAccounts)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_TransactionAllocationDetails_Accounts");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.TransactionAllocationDetails)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_TransactionAllocationDetails_AccountTransactionHeads");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.TransactionAllocationDetails)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_TransactionAllocationDetails_CostCenters");

                entity.HasOne(d => d.GL_Account)
                    .WithMany(p => p.TransactionAllocationDetailGL_Account)
                    .HasForeignKey(d => d.GL_AccountID)
                    .HasConstraintName("FK_TransactionAllocationDetails_Accounts_SubLedger");

                entity.HasOne(d => d.TransactionAllocation)
                    .WithMany(p => p.TransactionAllocationDetails)
                    .HasForeignKey(d => d.TransactionAllocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionAllocationDetails_TransactionAllocationHead");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}