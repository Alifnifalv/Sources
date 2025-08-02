using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Domain.Entity.Supports.Models.Catalogs;
using Eduegate.Domain.Entity.Supports.Models.Mutual;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Supports
{
    public partial class dbEduegateSupportContext : DbContext
    {
        public dbEduegateSupportContext()
        {
        }

        public dbEduegateSupportContext(DbContextOptions<dbEduegateSupportContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketActionDetailDetailMap> TicketActionDetailDetailMaps { get; set; }
        public virtual DbSet<TicketActionDetailMap> TicketActionDetailMaps { get; set; }
        public virtual DbSet<TicketBankDetail> TicketBankDetails { get; set; }
        public virtual DbSet<TicketCashDetail> TicketCashDetails { get; set; }
        public virtual DbSet<TicketCommunication> TicketCommunications { get; set; }
        public virtual DbSet<TicketFeeDueMap> TicketFeeDueMaps { get; set; }
        public virtual DbSet<TicketPriority> TicketPriorities { get; set; }
        public virtual DbSet<TicketProcessingStatus> TicketProcessingStatuses { get; set; }
        public virtual DbSet<TicketProductMap> TicketProductMaps { get; set; }
        public virtual DbSet<TicketReason> TicketReasons { get; set; }
        public virtual DbSet<TicketReferenceType> TicketReferenceTypes { get; set; }
        public virtual DbSet<TicketRefundDetail> TicketRefundDetails { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }
        public virtual DbSet<TicketTag> TicketTags { get; set; }

        public virtual DbSet<SupportAction> SupportActions { get; set; }
        public virtual DbSet<CustomerJustAsk> CustomerJustAsks { get; set; }
        public virtual DbSet<CustomerSupportTicket> CustomerSupportTickets { get; set; }

        public virtual DbSet<DocumentReferenceTicketStatusMap> DocumentReferenceTicketStatusMaps { get; set; }
        public virtual DbSet<DocumentReferenceType> DocumentReferenceTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSKUMap> ProductSKUMaps { get; set; }

        public virtual DbSet<StudentFeeDue> StudentFeeDues { get; set; }
        public virtual DbSet<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        public virtual DbSet<FeeMaster> FeeMasters { get; set; }

        public virtual DbSet<DocumentDepartmentMap> DocumentDepartmentMaps { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ActionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_SupportActions");

                entity.HasOne(d => d.AssingedEmployee)
                    .WithMany(p => p.TicketAssingedEmployees)
                    .HasForeignKey(d => d.AssingedEmployeeID)
                    .HasConstraintName("FK_Tickets_Employees2");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_Tickets_Customers");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_Tickets_DocumentTypes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TicketEmployees)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Tickets_Employees");

                entity.HasOne(d => d.ManagerEmployee)
                    .WithMany(p => p.TicketManagerEmployees)
                    .HasForeignKey(d => d.ManagerEmployeeID)
                    .HasConstraintName("FK_Tickets_Employees1");

                entity.HasOne(d => d.ReferenceTicket)
                    .WithMany(p => p.InverseReferenceTicket)
                    .HasForeignKey(d => d.ReferenceTicketID)
                    .HasConstraintName("FK_Tickets_Tickets");

                entity.HasOne(d => d.ReferenceType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ReferenceTypeID)
                    .HasConstraintName("FK_Tickets_ReferenceType");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_Tickets_Suppliers");

                entity.HasOne(d => d.TicketProcessingStatus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketProcessingStatusID)
                    .HasConstraintName("FK_Tickets_TicketProcessingStatuses");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketStatusID)
                    .HasConstraintName("FK_Tickets_TicketStatuses");
            });

            modelBuilder.Entity<TicketActionDetailDetailMap>(entity =>
            {
                entity.Property(e => e.TicketActionDetailDetailMapIID).ValueGeneratedNever();

                //entity.Property(e => e.Timestamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.TicketActionDetailMap)
                    .WithMany(p => p.TicketActionDetailDetailMaps)
                    .HasForeignKey(d => d.TicketActionDetailMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketActionDetailDetailMaps_TicketActionDetailMaps");
            });

            modelBuilder.Entity<TicketActionDetailMap>(entity =>
            {
                //entity.Property(e => e.Timestamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketActionDetailMaps)
                    .HasForeignKey(d => d.TicketID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketActionDetailMaps_Tickets");
            });

            modelBuilder.Entity<TicketProcessingStatus>(entity =>
            {
                entity.HasKey(e => e.TicketProcessingStatusIID)
                    .HasName("PK_TicketClaimStatuses");

                entity.Property(e => e.TicketProcessingStatusIID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TicketProductMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TicketProductMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_TicketProductMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.TicketProductMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_TicketProductMaps_ProductSKUMaps");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketProductMaps)
                    .HasForeignKey(d => d.TicketID)
                    .HasConstraintName("FK_TicketProductMaps_Tickets");
            });

            modelBuilder.Entity<TicketReason>(entity =>
            {
                entity.Property(e => e.TicketReasonID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TicketTag>(entity =>
            {
                entity.Property(e => e.TicketTagsID).ValueGeneratedNever();
            });

            modelBuilder.Entity<CustomerJustAsk>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.CustomerJustAsks)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerJustAsk_Cultures");
            });

            modelBuilder.Entity<CustomerSupportTicket>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.CustomerSupportTickets)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSupportTicket_Cultures");
            });

            modelBuilder.Entity<DocumentReferenceTicketStatusMap>(entity =>
            {
                entity.HasOne(d => d.ReferenceType)
                    .WithMany(p => p.DocumentReferenceTicketStatusMaps)
                    .HasForeignKey(d => d.ReferenceTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentReferenceTicketStatusMap_DocumentReferenceTypes");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.DocumentReferenceTicketStatusMaps)
                    .HasForeignKey(d => d.TicketStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentReferenceTicketStatusMap_TicketStatuses");
            });

            modelBuilder.Entity<DocumentReferenceType>(entity =>
            {
                entity.HasKey(e => e.ReferenceTypeID)
                    .HasName("PK_InventoryTypes");

                entity.Property(e => e.ReferenceTypeID).ValueGeneratedNever();
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

            modelBuilder.Entity<Customer>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.DefaultStudent)
                //    .WithMany(p => p.Customers)
                //    .HasForeignKey(d => d.DefaultStudentID)
                //    .HasConstraintName("FK_customers_Students");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Customers_Logins");
            });

            modelBuilder.Entity<CustomerGroup>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.IsOTEligible).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOverrideLeaveGroup).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicCalendar)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.AcademicCalendarID)
                //    .HasConstraintName("FK_Employees_AcadamicCalendar");

                //entity.HasOne(d => d.AccomodationType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.AccomodationTypeID)
                //    .HasConstraintName("FK_Employee_AccomodationType");

                //entity.HasOne(d => d.BloodGroup)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.BloodGroupID)
                //    .HasConstraintName("FK_Employee_BloodGroup");

                //entity.HasOne(d => d.Branch)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.BranchID)
                //    .HasConstraintName("FK_Employees_Branches");

                //entity.HasOne(d => d.CalendarType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CalendarTypeID)
                //    .HasConstraintName("FK_Emp_Calendar_type");

                //entity.HasOne(d => d.Cast)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CastID)
                //    .HasConstraintName("FK_Employees_Casts");

                //entity.HasOne(d => d.Category)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CategoryID)
                //    .HasConstraintName("FK_Employee_Category");

                //entity.HasOne(d => d.Community)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CommunityID)
                //    .HasConstraintName("FK_Employees_Communitys");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_Employees_Departments");

                //entity.HasOne(d => d.Designation)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.DesignationID)
                //    .HasConstraintName("FK_Employees_Designations");

                //entity.HasOne(d => d.EmployeeRole)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.EmployeeRoleID)
                //    .HasConstraintName("FK_Employees_EmployeeRoles");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_Employees_Genders");

                //entity.HasOne(d => d.JobType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.JobTypeID)
                //    .HasConstraintName("FK_Employees_JobTypes");

                //entity.HasOne(d => d.LeaveGroup)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.LeaveGroupID)
                //    .HasConstraintName("FK_emp_LeaveGroup");

                //entity.HasOne(d => d.LeavingType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.LeavingTypeID)
                //    .HasConstraintName("FK_employees_LeavingType");

                //entity.HasOne(d => d.LicenseType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.LicenseTypeID)
                //    .HasConstraintName("FK_Employee_LicenseType");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Employees_Logins");

                //entity.HasOne(d => d.MaritalStatus)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.MaritalStatusID)
                //    .HasConstraintName("FK_Employees_MaritalStatuses");

                //entity.HasOne(d => d.Nationality)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.NationalityID)
                //    .HasConstraintName("FK_Employees_Nationality");

                //entity.HasOne(d => d.PassageType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.PassageTypeID)
                //    .HasConstraintName("FK_Employee_PassageType");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_Employees_Relegions");

                entity.HasOne(d => d.ReportingEmployee)
                    .WithMany(p => p.InverseReportingEmployee)
                    .HasForeignKey(d => d.ReportingEmployeeID)
                    .HasConstraintName("FK_Employees_Employees");

                //entity.HasOne(d => d.ResidencyCompany)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.ResidencyCompanyId)
                //    .HasConstraintName("FK_Employees_Companies");

                //entity.HasOne(d => d.SalaryMethod)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.SalaryMethodID)
                //    .HasConstraintName("FK_Employees_SalaryMethod");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.RequirePasswordReset).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
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

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Suppliers)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Suppliers_CompanyID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Suppliers_Employees");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Suppliers_Logins");

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
            });

            modelBuilder.Entity<Product>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Brand)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.BrandID)
                //    .HasConstraintName("FK_Products_Brands");

                //entity.HasOne(d => d.GLAccount)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.GLAccountID)
                //    .HasConstraintName("FK_Products_GLAccount");

                //entity.HasOne(d => d.ProductFamily)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.ProductFamilyID)
                //    .HasConstraintName("FK_Products_ProductFamilies");

                //entity.HasOne(d => d.PurchaseUnitGroup)
                //    .WithMany(p => p.ProductPurchaseUnitGroups)
                //    .HasForeignKey(d => d.PurchaseUnitGroupID)
                //    .HasConstraintName("FK_Products_PurchaseUnitGroup");

                //entity.HasOne(d => d.PurchaseUnit)
                //    .WithMany(p => p.ProductPurchaseUnits)
                //    .HasForeignKey(d => d.PurchaseUnitID)
                //    .HasConstraintName("FK_Products_PurchaseUnit");

                //entity.HasOne(d => d.SellingUnitGroup)
                //    .WithMany(p => p.ProductSellingUnitGroups)
                //    .HasForeignKey(d => d.SellingUnitGroupID)
                //    .HasConstraintName("FK_Products_SellingUnitGroup");

                //entity.HasOne(d => d.SellingUnit)
                //    .WithMany(p => p.ProductSellingUnits)
                //    .HasForeignKey(d => d.SellingUnitID)
                //    .HasConstraintName("FK_Products_SellingUnit");

                //entity.HasOne(d => d.SeoMetadataI)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.SeoMetadataIID)
                //    .HasConstraintName("FK_Products_SeoMetadatas");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Products_ProductStatus");

                //entity.HasOne(d => d.TaxTemplate)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.TaxTemplateID)
                //    .HasConstraintName("FK_Products_TaxTemplates");

                //entity.HasOne(d => d.UnitGroup)
                //    .WithMany(p => p.ProductUnitGroups)
                //    .HasForeignKey(d => d.UnitGroupID)
                //    .HasConstraintName("FK_Products_UnitGroups");

                //entity.HasOne(d => d.Unit)
                //    .WithMany(p => p.ProductUnits)
                //    .HasForeignKey(d => d.UnitID)
                //    .HasConstraintName("FK_Products_Units");
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

            modelBuilder.Entity<TicketCommunication>(entity =>
            {
                entity.HasOne(d => d.Login)
                    .WithMany(p => p.TicketCommunications)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_TicketCommunications_Login");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketCommunications)
                    .HasForeignKey(d => d.TicketID)
                    .HasConstraintName("FK_TicketCommunications_Ticket");
            });

            modelBuilder.Entity<TicketFeeDueMap>(entity =>
            {
                entity.HasOne(d => d.StudentFeeDue)
                    .WithMany(p => p.TicketFeeDueMaps)
                    .HasForeignKey(d => d.StudentFeeDueID)
                    .HasConstraintName("FK_TicketFeeDueMaps_StudentFeeDue");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketFeeDueMaps)
                    .HasForeignKey(d => d.TicketID)
                    .HasConstraintName("FK_TicketFeeDueMaps_Ticket");
            });

            modelBuilder.Entity<TicketReferenceType>(entity =>
            {
                entity.HasKey(e => e.ReferenceTypeID)
                    .HasName("PK_ReferenceTypes");
            });

            modelBuilder.Entity<StudentFeeDue>(entity =>
            {
                entity.HasKey(e => e.StudentFeeDueIID)
                    .HasName("PK_FeeDueStudentMapIID");

                entity.Property(e => e.IsAccountPost).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FeeDueFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.FeeDueFeeTypeMapsIID)
                    .HasName("PK_FeeDueFeeTypeMapsIID");

                entity.Property(e => e.CollectedAmount).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_Feemaster_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.StudentFeeDue)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.StudentFeeDueID)
                    .HasConstraintName("FK_StudentFeeDue_FeeDueFeeTypeMaps1");
            });

            modelBuilder.Entity<FeeMaster>(entity =>
            {
                entity.Property(e => e.FeeMasterID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentDepartmentMap>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DocumentDepartmentMaps)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_DocumentDepartmentMaps_Department");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentDepartmentMaps)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_DocumentDepartmentMaps_DocumentType");
            });

            modelBuilder.Entity<Department>(entity =>
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}