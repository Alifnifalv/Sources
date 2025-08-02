using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using Eduegate.Domain.Entity.HR.Payroll;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR
{
    public partial class dbEduegateHRContext : DbContext
    {
        public dbEduegateHRContext()
        {
        }

        public dbEduegateHRContext(DbContextOptions<dbEduegateHRContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
        public virtual DbSet<TimesheetEntryStatus> TimesheetEntryStatuses { get; set; }

        public virtual DbSet<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }
        public virtual DbSet<EmployeeTimesheetApprovalMap> EmployeeTimesheetApprovalMaps { get; set; }
        public virtual DbSet<TimesheetApprovalStatus> TimesheetApprovalStatuses { get; set; }
        public virtual DbSet<SalaryComponentVariableMap> SalaryComponentVariableMaps { get; set; }
        public virtual DbSet<EmployeeSalaryStructureVariableMap> EmployeeSalaryStructureVariableMaps { get; set; }
        public virtual DbSet<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }
        public virtual DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Eduegate.Domain.Entity.HR.Models.Setting> Settings { get; set; }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<EmployeeAdditionalInfo> EmployeeAdditionalInfos { get; set; }
        public virtual DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public virtual DbSet<PassportVisaDetail> PassportVisaDetails { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }
        public DbSet<AvailableJobTag> AvailableJobTags { get; set; }
        public DbSet<DepartmentTag> DepartmentTags { get; set; }
        public DbSet<AvailableJob> DB_Availablejobs { get; set; }
        public DbSet<DB_ApplicationForm> DB_ApplicationForms { get; set; }
        public DbSet<DB_Department> DB_Departments { get; set; }
        public DbSet<AvailableJobCultureData> AvailableJobCultureDatas { get; set; }

        public DbSet<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }
        public DbSet<LeaveBlockListEntry> LeaveBlockListEntries { get; set; }
        public DbSet<LeaveBlockList> LeaveBlockLists { get; set; }
        public DbSet<LeaveSession> LeaveSessions { get; set; }
        public DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        public virtual DbSet<PayrollFrequency> PayrollFrequencies { get; set; }
        public virtual DbSet<SalaryComponent> SalaryComponents { get; set; }
        public virtual DbSet<SalaryComponentType> SalaryComponentTypes { get; set; }

        public DbSet<SalaryMethod> SalaryMethods { get; set; }

        public DbSet<AcademicSchoolMap> AcademicSchoolMaps { get; set; }

        public virtual DbSet<SalaryPaymentMode> SalaryPaymentModes { get; set; }

        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<EmployeeRoleMap> EmployeeRoleMaps { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<JobType> JobTypes { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Departments1> Departments1 { get; set; }
        public virtual DbSet<DepartmentStatus> DepartmentStatuses { get; set; }

        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<LoginRoleMap> LoginRoleMaps { get; set; }

        public virtual DbSet<SalarySlip> SalarySlips { get; set; }
        public virtual DbSet<SalarySlipStatus> SalarySlipStatuses { get; set; }

        public virtual DbSet<SalaryStructure> SalaryStructures { get; set; }
        public virtual DbSet<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
        public virtual DbSet<SalaryStructureScaleMap> SalaryStructureScaleMaps { get; set; }

        public virtual DbSet<SalaryComponentGroup> SalaryComponentGroups { get; set; }

        public virtual DbSet<FunctionalPeriod> FunctionalPeriods { get; set; }

        public virtual DbSet<Schools> School { get; set; }

        public virtual DbSet<SalaryComponentRelationMap> SalaryComponentRelationMaps { get; set; }
        public virtual DbSet<SalaryComponentRelationType> SalaryComponentRelationTypes { get; set; }
        public virtual DbSet<ReportHeadGroup> ReportHeadGroups { get; set; }

        public virtual DbSet<AcadamicCalendar> AcadamicCalendars { get; set; }
        public virtual DbSet<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
        public virtual DbSet<AcademicYearCalendarEventType> AcademicYearCalendarEventTypes { get; set; }
        public virtual DbSet<AcademicYearCalendarStatus> AcademicYearCalendarStatus { get; set; }
        public virtual DbSet<CalendarEntry> CalendarEntries { get; set; }
        public virtual DbSet<CalendarType> CalendarTypes { get; set; }
        public virtual DbSet<LeaveGroup> LeaveGroups { get; set; }
        public virtual DbSet<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }       
        public virtual DbSet<EmployeePromotion> EmployeePromotions { get; set; }
        public virtual DbSet<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
        public virtual DbSet<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }
        public virtual DbSet<PassageType> PassageTypes { get; set; }
        public virtual DbSet<AccomodationType> AccomodationTypes { get; set; }

        public virtual DbSet<SchoolDateSettingMap> SchoolDateSettingMaps { get; set; }
        public virtual DbSet<SchoolDateSetting> SchoolDateSettings { get; set; }

        public virtual DbSet<WPSDetail> WPSDetails { get; set; }
        public virtual DbSet<EmployeeSalaryStructureLeaveSalaryMap> EmployeeSalaryStructureLeaveSalaryMaps { get; set; }

        public virtual DbSet<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
        public virtual DbSet<TicketEntitilement> TicketEntitilements { get; set; }
        public virtual DbSet<EmployeeSettlementType> EmployeeSettlementTypes { get; set; }

        public virtual DbSet<LoanDetail> LoanDetails { get; set; }
        public virtual DbSet<LoanEntryStatus> LoanEntryStatuses { get; set; }
        public virtual DbSet<LoanHead> LoanHeads { get; set; }
        public virtual DbSet<LoanRequest> LoanRequests { get; set; }
        public virtual DbSet<LoanRequestSearch> LoanRequestSearches { get; set; }
        public virtual DbSet<LoanRequestStatus> LoanRequestStatuses { get; set; }
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }

        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<FlightClass> FlightClasses { get; set; }
        public virtual DbSet<SectorTicketAirfare> SectorTicketAirfares { get; set; }
        public virtual DbSet<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
        public virtual DbSet<TicketEntitlementEntryStatus> TicketEntitlementEntryStatuses { get; set; }
        public virtual DbSet<EmployeeESBProvision> EmployeeESBProvisions { get; set; }
        public virtual DbSet<EmployeeLSProvision> EmployeeLSProvisions { get; set; }
        public virtual DbSet<EmployeeLSProvisionDetail> EmployeeLSProvisionDetails { get; set; }
        public virtual DbSet<EmployeeLSProvisionHead> EmployeeLSProvisionHeads { get; set; }
        public virtual DbSet<EmployeeESBProvisionDetail> EmployeeESBProvisionDetails { get; set; }
        public virtual DbSet<EmployeeESBProvisionHead> EmployeeESBProvisionHeads { get; set; }
        public virtual DbSet<EmployeeDepartmentAccountMap> EmployeeDepartmentAccountMaps { get; set; }
        public virtual DbSet<EmployeeJobDescription> EmployeeJobDescriptions { get; set; }
        public virtual DbSet<EmployeeJobDescriptionDetail> EmployeeJobDescriptionDetails { get; set; }
        public virtual DbSet<JobDescription> JobDescriptions { get; set; }
        public virtual DbSet<JobDescriptionDetail> JobDescriptionDetails { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                //entity.HasOne(d => d.AccountBehavoir)
                //    .WithMany(p => p.Accounts)
                //    .HasForeignKey(d => d.AccountBehavoirID)
                //    .HasConstraintName("FK_Accounts_AccountBehavoirs");

                //entity.HasOne(d => d.Group)
                //    .WithMany(p => p.Accounts)
                //    .HasForeignKey(d => d.GroupID)
                //    .HasConstraintName("FK_Accounts_Groups");

                entity.HasOne(d => d.Account1)
                    .WithMany(p => p.Accounts1)
                    .HasForeignKey(d => d.ParentAccountID)
                    .HasConstraintName("FK_Accounts_Accounts");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.DesignationID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();
            });

            modelBuilder.Entity<EmployeeLeaveAllocation>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLeaveAllocations)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmpLeaveAllocations_Employee");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.EmployeeLeaveAllocations)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_EmployeeLeaveAllocations_LeaveTypes");
            });

            modelBuilder.Entity<LeaveAllocation>(entity =>
            {
                entity.Property(e => e.LeaveAllocationIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.LeaveGroup)
                    .WithMany()
                    .HasForeignKey(d => d.LeaveGroupID)
                    .HasConstraintName("FK_LeaveAllocations_LeaveGroups");

                entity.HasOne(d => d.LeaveType)
                    .WithMany()
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_LeaveAllocations_LeaveTypes");
            });

            modelBuilder.Entity<LeaveApplication>(entity =>
            {
                entity.Property(e => e.IsLeaveWithoutPay).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LeaveApplications_Employees");

                entity.HasOne(d => d.LeaveSession)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.FromSessionID)
                    .HasConstraintName("FK_LeaveApplications_LeaveApplications");

                entity.HasOne(d => d.LeaveStatus)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveStatusID)
                    .HasConstraintName("FK_LeaveApplications_LeaveStatuses");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_LeaveApplications_LeaveTypes");

                entity.HasOne(d => d.StaffLeaveReason)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.StaffLeaveReasonID)
                    .HasConstraintName("FK_LeaveApplications_LeaveApplications1");

                entity.HasOne(d => d.LeaveSession1)
                    .WithMany(p => p.LeaveApplications1)
                    .HasForeignKey(d => d.ToSessionID)
                    .HasConstraintName("FK_LeaveApplications_LeaveSessions");
            });

            modelBuilder.Entity<LeaveApplicationApprover>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveApplicationApprovers)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LeaveApplicationApprovers_Employees");

                entity.HasOne(d => d.LeaveApplication)
                    .WithMany(p => p.LeaveApplicationApprovers)
                    .HasForeignKey(d => d.LeaveApplicationID)
                    .HasConstraintName("FK_LeaveApplicationApprovers_LeaveApplications");
            });

            modelBuilder.Entity<LeaveBlockListApprover>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveBlockListApprovers)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LeaveBlockListApprovers_Employees");

                entity.HasOne(d => d.LeaveBlockList)
                    .WithMany(p => p.LeaveBlockListApprovers)
                    .HasForeignKey(d => d.LeaveBlockListID)
                    .HasConstraintName("FK_LeaveBlockListApprovers_LeaveBlockLists");
            });

            modelBuilder.Entity<LeaveBlockListEntry>(entity =>
            {
                entity.HasOne(d => d.LeaveBlockList)
                    .WithMany(p => p.LeaveBlockListEntries)
                    .HasForeignKey(d => d.LeaveBlockListID)
                    .HasConstraintName("FK_LeaveBlockListEntries_LeaveBlockLists");
            });

            modelBuilder.Entity<LeaveGroup>(entity =>
            {
                entity.Property(e => e.LeaveGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.LeaveTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<StaffLeaveReason>(entity =>
            {
                entity.Property(e => e.StaffLeaveReasonIID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AcadamicCalendar>(entity =>
            {
                entity.HasKey(e => e.AcademicCalendarID)
                    .HasName("PK_AcadamicCalendar");

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYearCalendarStatu)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.AcademicCalendarStatusID)
                    .HasConstraintName("FK_AcadamicCalenders_AcademicYearCalenderStatus");

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.AcadamicCalendars)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_AcadamicCalenders_AcademicYears");

                entity.HasOne(d => d.CalendarType)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.CalendarTypeID)
                    .HasConstraintName("FK_Calendar_type");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcadamicCalendars_School");
            });

            modelBuilder.Entity<AcademicSchoolMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.AcademicSchoolMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_AcademicSchoolMaps_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.AcademicSchoolMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_AcademicSchoolMaps_Schools");
            });

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.Property(e => e.AcademicYearID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                //entity.HasOne(d => d.AcademicYearStatus)
                //    .WithMany(p => p.AcademicYears)
                //    .HasForeignKey(d => d.AcademicYearStatusID)
                //    .HasConstraintName("FK_AcademicYears_AcademicYearStatus");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.AcademicYears)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_AcademicYears_Schools");
            });

            modelBuilder.Entity<AcademicYearCalendarEvent>(entity =>
            {
                entity.HasKey(e => e.AcademicYearCalendarEventIID)
                    .HasName("PK_Events");

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicCalendar)
                //    .WithMany(p => p.AcademicYearCalendarEvents)
                //    .HasForeignKey(d => d.AcademicCalendarID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_AcademicYearCalendarEvents_AcadamicCalenders");

                entity.HasOne(d => d.AcademicYearCalendarEventType)
                    .WithMany(p => p.AcademicYearCalendarEvents)
                    .HasForeignKey(d => d.AcademicYearCalendarEventTypeID)
                    .HasConstraintName("FK_AcademicYearCalendarEvents_AcademicYearCalendarEventType");
            });

            modelBuilder.Entity<AcademicYearCalendarEventType>(entity =>
            {
                entity.Property(e => e.AcademicYearCalendarEventTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AcademicYearCalendarStatus>(entity =>
            {
                entity.HasKey(e => e.AcademicYearCalendarStatusID)
                    .HasName("PK_AcademicYearStatus");
            });

            modelBuilder.Entity<CalendarEntry>(entity =>
            {
                entity.Property(e => e.CalendarEntryID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //.IsRowVersion()
                //.IsConcurrencyToken();

                entity.HasOne(d => d.AcadamicCalendar)
                    .WithMany(p => p.CalendarEntries)
                    .HasForeignKey(d => d.AcademicCalendarID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalendarEntries_AcadamicCalenders");
            });

            modelBuilder.Entity<EmployeeSalary>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.EmployeeSalaries)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_EmployeeSalaries_Companies");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalaries)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeSalaries_Employees");

                //entity.HasOne(d => d.SalaryComponent)
                //    .WithMany(p => p.EmployeeSalaries)
                //    .HasForeignKey(d => d.SalaryComponentID)
                //    .HasConstraintName("FK_EmployeeSalaries_SalaryComponents");
            });

            modelBuilder.Entity<EmployeeSalaryStructure>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.EmployeeSalaryStructures)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_Accounts");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalaryStructures)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_Employees");

                entity.HasOne(d => d.SalaryPaymentMode)
                    .WithMany(p => p.EmployeeSalaryStructures)
                    .HasForeignKey(d => d.PaymentModeID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_SalaryPaymentModes");

                entity.HasOne(d => d.PayrollFrequency)
                    .WithMany(p => p.EmployeeSalaryStructures)
                    .HasForeignKey(d => d.PayrollFrequencyID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_PayrollFrequencies");

                entity.HasOne(d => d.SalaryStructure)
                    .WithMany(p => p.EmployeeSalaryStructureSalaryStructures)
                    .HasForeignKey(d => d.SalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_SalaryStructure");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeSalaryStructures)
                    .HasForeignKey(d => d.TimeSheetSalaryComponentID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_SalaryComponents");

                entity.HasOne(d => d.LeaveSalaryStructure)
                    .WithMany(p => p.EmployeeSalaryStructureLeaveSalaryStructures)
                    .HasForeignKey(d => d.LeaveSalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalaryStructures_LeaveSalaryStructure");
            });

            modelBuilder.Entity<EmployeeTimeSheet>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.Property(e => e.TimesheetEntryStatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTimeSheets)
                    .HasForeignKey(d => d.EmployeeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTimeSheets_Employees");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EmployeeTimeSheets)
                    .HasForeignKey(d => d.TaskID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTimeSheets_EmployeeTimeSheets");

                entity.HasOne(d => d.TimesheetEntryStatus)
                    .WithMany(p => p.EmployeeTimeSheets)
                    .HasForeignKey(d => d.TimesheetEntryStatusID)
                    .HasConstraintName("FK_EmployeeTimeSheet_Satus");

                entity.HasOne(d => d.TimesheetTimeType)
                    .WithMany(p => p.EmployeeTimeSheets)
                    .HasForeignKey(d => d.TimesheetTimeTypeID)
                    .HasConstraintName("FK_EmployeeTimeSheet_TimesheetTimeType");
            });

            modelBuilder.Entity<EmployeeTimeSheetApproval>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTimeSheetApprovals)
                    .HasForeignKey(d => d.EmployeeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTimeSheetAs_Employees");

                entity.HasOne(d => d.TimesheetApprovalStatus)
                    .WithMany(p => p.EmployeeTimeSheetApprovals)
                    .HasForeignKey(d => d.TimesheetApprovalStatusID)
                    .HasConstraintName("FK_EmployeeTimesheetApproval_ApprovalStatuses");

                entity.HasOne(d => d.TimesheetTimeType)
                    .WithMany(p => p.EmployeeTimeSheetApprovals)
                    .HasForeignKey(d => d.TimesheetTimeTypeID)
                    .HasConstraintName("FK_EmployeeTimeSheets_TimesheetTimeType");
            });

            modelBuilder.Entity<EmployeeTimesheetApprovalMap>(entity =>
            {
                entity.Property(e => e.EmployeeTimesheetApprovalMapIID).ValueGeneratedOnAdd();

                entity.HasOne(d => d.EmployeeTimeSheet)
                    .WithMany(p => p.EmployeeTimesheetApprovalMaps)
                    .HasForeignKey(d => d.EmployeeTimeSheetID)
                    .HasConstraintName("FK_EmployeeTimesheetApprovalMaps_EmployeeTimeSheets");

                entity.HasOne(d => d.EmployeeTimeSheetApproval)
                    .WithMany(p => p.EmployeeTimesheetApprovalMaps)
                    .HasForeignKey(d => d.EmployeeTimesheetApprovalID)
                    .HasConstraintName("FK_EmployeeTimesheetApprovalMaps_EmployeeTimeSheetApprovals");
            });

            modelBuilder.Entity<FunctionalPeriod>(entity =>
            {
                entity.Property(e => e.FunctionalPeriodID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FunctionalPeriods)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_FunctionalPeriods_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.FunctionalPeriods)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_FunctionalPeriods_School");
            });

            modelBuilder.Entity<ReportHeadGroup>(entity =>
            {
                entity.Property(e => e.ReportHeadGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalaryComponent>(entity =>
            {
                entity.Property(e => e.SalaryComponentID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //.IsRowVersion()
                //.IsConcurrencyToken();

                entity.HasOne(d => d.SalaryComponentType)
                    .WithMany(p => p.SalaryComponents)
                    .HasForeignKey(d => d.ComponentTypeID)
                    .HasConstraintName("FK_SalaryComponents_SalaryComponentTypes");

                entity.HasOne(d => d.ReportHeadGroup)
                    .WithMany(p => p.SalaryComponents)
                    .HasForeignKey(d => d.ReportHeadGroupID)
                    .HasConstraintName("FK_SalaryComponents_[ReportHeadGroups");

                entity.HasOne(d => d.SalaryComponentGroup)
                    .WithMany(p => p.SalaryComponents)
                    .HasForeignKey(d => d.SalaryComponentGroupID)
                    .HasConstraintName("FK_SalaryComponent_Group");

                entity.HasOne(d => d.StaffLedgerAccount)
                    .WithMany(p => p.SalaryComponentStaffLedgerAccounts)
                    .HasForeignKey(d => d.StaffLedgerAccountID)
                    .HasConstraintName("FK_SalaryComponents_StaffLedgerAccount");
                entity.HasOne(d => d.ExpenseLedgerAccount)
                   .WithMany(p => p.SalaryComponentExpenseLedgerAccounts)
                   .HasForeignKey(d => d.ExpenseLedgerAccountID)
                   .HasConstraintName("FK_SalaryComponents_ExpenseLedgerAccount");

                entity.HasOne(d => d.ProvisionLedgerAccount)
                    .WithMany(p => p.SalaryComponentProvisionLedgerAccounts)
                    .HasForeignKey(d => d.ProvisionLedgerAccountID)
                    .HasConstraintName("FK_SalaryComponents_ProvisionLedgerAccount");

            });

            modelBuilder.Entity<SalaryComponentRelationMap>(entity =>
            {
                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.SalaryComponentRelationMaps)
                    .HasForeignKey(d => d.RelatedComponentID)
                    .HasConstraintName("FK_SalaryComponentMaps_RelatedComponents");

                entity.HasOne(d => d.SalaryComponentRelationType)
                    .WithMany(p => p.SalaryComponentRelationMaps)
                    .HasForeignKey(d => d.RelationTypeID)
                    .HasConstraintName("FK_SalaryStructureComponentMaps_Relations");

                entity.HasOne(d => d.SalaryComponent1)
                    .WithMany(p => p.SalaryComponentRelationMaps1)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_SalaryComponentRelationMap_SalaryComponents");
            });


            modelBuilder.Entity<SalaryComponentRelationType>(entity =>
            {
                entity.Property(e => e.RelationTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalaryMethod>(entity =>
            {
                entity.Property(e => e.SalaryMethodID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalaryPaymentMode>(entity =>
            {
                entity.Property(e => e.SalaryPaymentModeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalarySlip>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_SalarySlips_Employees");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_SalarySlips_SalaryComponents");

                entity.HasOne(d => d.SalarySlipStatus)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.SalarySlipStatusID)
                    .HasConstraintName("FK_SalarySlips_SalarySlipStatuses");
            });

            modelBuilder.Entity<SalaryStructure>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalaryStructures)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_SalaryStructure_Accounts");

                entity.HasOne(d => d.PaymentMode)
                    .WithMany(p => p.SalaryStructures)
                    .HasForeignKey(d => d.PaymentModeID)
                    .HasConstraintName("FK_SalaryStructure_SalaryPaymentModes");

                entity.HasOne(d => d.PayrollFrequency)
                    .WithMany(p => p.SalaryStructures)
                    .HasForeignKey(d => d.PayrollFrequencyID)
                    .HasConstraintName("FK_SalaryStructure_PayrollFrequencies");

                entity.HasOne(d => d.TimeSheetSalaryComponent)
                    .WithMany(p => p.SalaryStructures)
                    .HasForeignKey(d => d.TimeSheetSalaryComponentID)
                    .HasConstraintName("FK_SalaryStructure_SalaryComponents");
            });

            modelBuilder.Entity<SalaryStructureComponentMap>(entity =>
            {
                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.SalaryStructureComponentMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_SalaryStructureComponentMaps_SalaryComponents");

                entity.HasOne(d => d.SalaryStructure)
                    .WithMany(p => p.SalaryStructureComponentMaps)
                    .HasForeignKey(d => d.SalaryStructureID)
                    .HasConstraintName("FK_SalaryStructureComponentMaps_SalaryStructure");
            });

            modelBuilder.Entity<SchoolDateSetting>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.SchoolDateSettings)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_SchoolDateSettings_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.SchoolDateSettings)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_SchoolDateSettings_Schools");
            });

            modelBuilder.Entity<SchoolDateSettingMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.SchoolDateSetting)
                    .WithMany(p => p.SchoolDateSettingMaps)
                    .HasForeignKey(d => d.SchoolDateSettingID)
                    .HasConstraintName("FK_SchoolDateSettingMaps_DateSetting");
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Schools)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Schools_Companies");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                //entity.HasOne(d => d.TaskPrioity)
                //    .WithMany(p => p.Tasks)
                //    .HasForeignKey(d => d.TaskPrioityID)
                //    .HasConstraintName("FK_Tasks_TaskPrioities");

                //entity.HasOne(d => d.TaskStatus)
                //    .WithMany(p => p.Tasks)
                //    .HasForeignKey(d => d.TaskStatusID)
                //    .HasConstraintName("FK_Tasks_TaskStatuses");

                //entity.HasOne(d => d.TaskType)
                //    .WithMany(p => p.Tasks)
                //    .HasForeignKey(d => d.TaskTypeID)
                //    .HasConstraintName("FK_Tasks_TaskTypes");
            });

            modelBuilder.Entity<AccomodationType>(entity =>
            {
                entity.Property(e => e.AccomodationTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AvailableJobCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.JobIID });

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();
            });

            modelBuilder.Entity<AvailableJobCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.JobIID });

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();
            });

            modelBuilder.Entity<AvailableJobTag>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.AvailableJobs)
                    .WithMany(p => p.AvailableJobTags)
                    .HasForeignKey(d => d.JobID)
                    .HasConstraintName("FK_AvailableJobTags_AvailableJobs");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.BankIID)
                    .HasName("PK_BanksNames");

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();
            });

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.HasOne(d => d.Relegion)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.RelegionID)
                    .HasConstraintName("FK_Casts_Relegions");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryID).ValueGeneratedNever();

                //entity.HasOne(d => d.Currency)
                //    .WithMany(p => p.Countries)
                //    .HasForeignKey(d => d.CurrencyID)
                //    .HasConstraintName("FK_Countries_Currencies");
            });

            modelBuilder.Entity<DepartmentTag>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentTags)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_DepartmentTags_Departments");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                //entity.Property(e => e.IsOTEligible).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOverrideLeaveGroup).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //.IsRowVersion()
                //.IsConcurrencyToken();

                entity.HasOne(d => d.AcademicCalendar)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AcademicCalendarID)
                    .HasConstraintName("FK_Employees_AcadamicCalendar");

                entity.HasOne(d => d.AccomodationType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AccomodationTypeID)
                    .HasConstraintName("FK_Employee_AccomodationType");

                //entity.HasOne(d => d.BloodGroup)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.BloodGroupID)
                //    .HasConstraintName("FK_Employee_BloodGroup");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_Employees_Branches");

                entity.HasOne(d => d.CalendarType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CalendarTypeID)
                    .HasConstraintName("FK_Emp_Calendar_type");

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

                entity.HasOne(d => d.Departments1)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_Employees_Departments");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationID)
                    .HasConstraintName("FK_Employees_Designations");

                entity.HasOne(d => d.EmployeeRole)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeRoleID)
                    .HasConstraintName("FK_Employees_EmployeeRoles");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderID)
                    .HasConstraintName("FK_Employees_Genders");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTypeID)
                    .HasConstraintName("FK_Employees_JobTypes");

                entity.HasOne(d => d.LeaveGroup)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LeaveGroupID)
                    .HasConstraintName("FK_emp_LeaveGroup");

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

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_Employees_Nationality");

                entity.HasOne(d => d.PassageType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PassageTypeID)
                    .HasConstraintName("FK_Employee_PassageType");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_Employees_Relegions");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.Employees1)
                    .HasForeignKey(d => d.ReportingEmployeeID)
                    .HasConstraintName("FK_Employees_Employees");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ResidencyCompanyId)
                    .HasConstraintName("FK_Employees_Companies");

                entity.HasOne(d => d.EmployeeCountryAirport)
                    .WithMany(p => p.EmployeeEmployeeCountryAirports)
                    .HasForeignKey(d => d.EmployeeCountryAirportID)
                    .HasConstraintName("FK_EmployeeCountry_Airport");

                entity.HasOne(d => d.EmployeeNearestAirport)
                    .WithMany(p => p.EmployeeEmployeeNearestAirports)
                    .HasForeignKey(d => d.EmployeeNearestAirportID)
                    .HasConstraintName("FK_EmployeeNearestAirport");

                entity.HasOne(d => d.TicketEntitilement)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.TicketEntitilementID)
                    .HasConstraintName("FK_EmployeeTicketEntitlement");

                entity.HasOne(d => d.FlightClass)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.FlightClassID)
                    .HasConstraintName("FK_Employee_FlightClass");

            });
            modelBuilder.Entity<EmployeeESBProvision>(entity =>
            {
                entity.HasKey(e => e.EmployeeESBProvisionIID)
                    .HasName("PK_EmployeeESBProvision");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeESBProvisions)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeESBProvisions_Employees");
            });
            modelBuilder.Entity<EmployeeLSProvision>(entity =>
            {
                entity.HasKey(e => e.EmployeeLSProvisionIID)
                    .HasName("PK_EmployeeLSProvision");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLSProvisions)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeLSProvision_Employees");
            });
            modelBuilder.Entity<EmployeeLSProvisionDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeLSProvisionDetailIID)
                    .HasName("PK_EmployeeLSProvisionDetail");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLSProvisionDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeLSProvisionDetail_Employees");

                entity.HasOne(d => d.EmployeeLSProvisionHead)
                    .WithMany(p => p.EmployeeLSProvisionDetails)
                    .HasForeignKey(d => d.EmployeeLSProvisionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeLSProvisions_Head");
            });

            modelBuilder.Entity<EmployeeLSProvisionHead>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.EmployeeLSProvisionHeads)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_EmployeeLSProvisions_BranchID");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.EmployeeLSProvisionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_EmployeeLSProvisions_DocumentType");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeLSProvisionHeads)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmployeeLSProvisions_SalaryComponent");
            });
            modelBuilder.Entity<EmployeeESBProvisionDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeESBProvisionDetailIID)
                    .HasName("PK_EmployeeESBProvisionDetail");

                entity.HasOne(d => d.EmployeeESBProvisionHead)
                    .WithMany(p => p.EmployeeESBProvisionDetails)
                    .HasForeignKey(d => d.EmployeeESBProvisionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeESBProvisions_EmployeeESBProvisionHead");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeESBProvisionDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeESBProvisionDetail_Employees");
            });

            modelBuilder.Entity<EmployeeESBProvisionHead>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.EmployeeESBProvisionHeads)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_EmployeeESBProvisionHeads_BranchID");
                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.EmployeeESBProvisionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_EmployeeESBProvisions_DocumentType");
                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeESBProvisionHeads)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EEmployeeESBProvision_SalaryComponent");
            });
            modelBuilder.Entity<EmployeeDepartmentAccountMap>(entity =>
            {
                entity.HasKey(e => e.EmployeeDepartmentAccountMaplIID)
                    .HasName("PK_EmployeeDepartmentAccountMap");

                entity.HasOne(d => d.Departments1)
                    .WithMany(p => p.EmployeeDepartmentAccountMaps)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_EmpDeptAccount_Department");

                entity.HasOne(d => d.ExpenseLedgerAccount)
                    .WithMany(p => p.EmployeeDepartmentAccountMapExpenseLedgerAccounts)
                    .HasForeignKey(d => d.ExpenseLedgerAccountID)
                    .HasConstraintName("FK_EmpDeptAccount_ExpenseLedgerAccount");

                entity.HasOne(d => d.ProvisionLedgerAccount)
                    .WithMany(p => p.EmployeeDepartmentAccountMapProvisionLedgerAccounts)
                    .HasForeignKey(d => d.ProvisionLedgerAccountID)
                    .HasConstraintName("FK_EmpDeptAccount_ProvisionLedgerAccount");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeDepartmentAccountMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmpDeptAccount_SalaryComponent");

                entity.HasOne(d => d.StaffLedgerAccount)
                    .WithMany(p => p.EmployeeDepartmentAccountMapStaffLedgerAccounts)
                    .HasForeignKey(d => d.StaffLedgerAccountID)
                    .HasConstraintName("FK_EmpDeptAccount_StaffLedgerAccount");

                entity.HasOne(d => d.TaxLedgerAccount)
                    .WithMany(p => p.EmployeeDepartmentAccountMapTaxLedgerAccounts)
                    .HasForeignKey(d => d.TaxLedgerAccountID)
                    .HasConstraintName("FK_EmpDeptAccount_TaxLedgerAccount");
            });

            modelBuilder.Entity<FlightClass>(entity =>
            {
                entity.Property(e => e.FlightClassID).ValueGeneratedNever();
            });
            modelBuilder.Entity<SectorTicketAirfare>(entity =>
            {
                entity.HasKey(e => e.SectorTicketAirfareIID)
                    .HasName("PK_SectorTicketAirfare");

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.SectorTicketAirfareAirports)
                    .HasForeignKey(d => d.AirportID)
                    .HasConstraintName("FK_SectorTicketAirfares_AirportID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SectorTicketAirfares)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_SectorTicketAirfares_Department");
                entity.HasOne(d => d.FlightClass)
                    .WithMany(p => p.SectorTicketAirfares)
                    .HasForeignKey(d => d.FlightClassID)
                    .HasConstraintName("FK_SectorTicketAirfares_FlightClass");

                entity.HasOne(d => d.ReturnAirport)
                    .WithMany(p => p.SectorTicketAirfareReturnAirports)
                    .HasForeignKey(d => d.ReturnAirportID)
                    .HasConstraintName("FK_SectorTicketAirfares_ReturnAirport");
            });
            modelBuilder.Entity<TicketEntitilementEntry>(entity =>
            {
                entity.HasKey(e => e.TicketEntitilementEntryIID)
                    .HasName("PK_TicketEntitilementEntry");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TicketEntitilementEntries)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_TicketEntitlement_Employee");

                entity.HasOne(d => d.FlightClass)
                    .WithMany(p => p.TicketEntitilementEntries)
                    .HasForeignKey(d => d.FlightClassID)
                    .HasConstraintName("FK_TicketEntitilement_FlightClass");

                entity.HasOne(d => d.TicketEntitilement)
                    .WithMany(p => p.TicketEntitilementEntries)
                    .HasForeignKey(d => d.TicketEntitilementID)
                    .HasConstraintName("FK_EmployeeTicketEntitlementEntry");
                entity.HasOne(d => d.EmployeeCountryAirport)
                    .WithMany(p => p.TicketEntitilementEntries)
                    .HasForeignKey(d => d.EmployeeCountryAirportID)
                    .HasConstraintName("FK_TicketEntitilementEntries_Airport");
                entity.HasOne(d => d.TicketEntitlementEntryStatus)
                   .WithMany(p => p.TicketEntitilementEntries)
                   .HasForeignKey(d => d.TicketEntitlementEntryStatusID)
                   .HasConstraintName("FK_TicketEntitilementEntries_Status");
            });
            modelBuilder.Entity<TicketEntitlementEntryStatus>(entity =>
            {
                entity.HasKey(e => e.StatusID)
                    .HasName("PK__TicketEn__C8EE2043D3BBFBC1");

                entity.Property(e => e.StatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EmployeeAdditionalInfo>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAdditionalInfos)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeAdditionalInfos_Employees");
            });

                

            modelBuilder.Entity<EmployeeBankDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeBankIID)
                    .HasName("PK_EmployeeBankDetail");

                ////entity.Property(e => e.TimeStamps)
                //    //.IsRowVersion()
                //    //.IsConcurrencyToken();

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_Employee_BankName");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Employee_BankDetail");
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

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeePromotions)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeePromotions_Employees");

                entity.HasOne(d => d.NewBranch)
                    .WithMany(p => p.EmployeePromotionNewBranches)
                    .HasForeignKey(d => d.NewBranchID)
                    .HasConstraintName("FK_EmployeePromotions_NewBranches");

                entity.HasOne(d => d.NewDesignation)
                    .WithMany(p => p.EmployeePromotionNewDesignations)
                    .HasForeignKey(d => d.NewDesignationID)
                    .HasConstraintName("FK_EmployeePromotions_NewDesignations");

                entity.HasOne(d => d.NewLeaveGroup)
                    .WithMany(p => p.EmployeePromotionNewLeaveGroups)
                    .HasForeignKey(d => d.NewLeaveGroupID)
                    .HasConstraintName("FK_EmployeePromotions_LeaveGroups");

                entity.HasOne(d => d.NewRole)
                    .WithMany(p => p.EmployeePromotionNewRoles)
                    .HasForeignKey(d => d.NewRoleID)
                    .HasConstraintName("FK_EmployeePromotions_NewRoles");

                entity.HasOne(d => d.NewSalaryStructure)
                    .WithMany(p => p.EmployeePromotionNewSalaryStructures)
                    .HasForeignKey(d => d.NewSalaryStructureID)
                    .HasConstraintName("FK_EmployeePromotions_NewSalaryStructure");

                entity.HasOne(d => d.OldBranch)
                    .WithMany(p => p.EmployeePromotionOldBranches)
                    .HasForeignKey(d => d.OldBranchID)
                    .HasConstraintName("FK_EmployeePromotions_OldBranches");

                entity.HasOne(d => d.OldDesignation)
                    .WithMany(p => p.EmployeePromotionOldDesignations)
                    .HasForeignKey(d => d.OldDesignationID)
                    .HasConstraintName("FK_EmployeePromotions_OldDesignations");

                entity.HasOne(d => d.OldLeaveGroup)
                    .WithMany(p => p.EmployeePromotionOldLeaveGroups)
                    .HasForeignKey(d => d.OldLeaveGroupID)
                    .HasConstraintName("FK_EmployeePromotions_OldLeaveGroups");

                entity.HasOne(d => d.OldRole)
                    .WithMany(p => p.EmployeePromotionOldRoles)
                    .HasForeignKey(d => d.OldRoleID)
                    .HasConstraintName("FK_EmployeePromotions_OldRoles");

                entity.HasOne(d => d.OldSalaryStructure)
                    .WithMany(p => p.EmployeePromotionOldSalaryStructures)
                    .HasForeignKey(d => d.OldSalaryStructureID)
                    .HasConstraintName("FK_EmployeePromotions_OldSalaryStructure");

                entity.HasOne(d => d.PaymentMode)
                    .WithMany(p => p.EmployeePromotions)
                    .HasForeignKey(d => d.PaymentModeID)
                    .HasConstraintName("FK_EmployeePromotions_SalaryPaymentModes");

                entity.HasOne(d => d.PayrollFrequency)
                    .WithMany(p => p.EmployeePromotions)
                    .HasForeignKey(d => d.PayrollFrequencyID)
                    .HasConstraintName("FK_EmployeePromotions_PayrollFrequencies");

                entity.HasOne(d => d.SalaryStructure)
                    .WithMany(p => p.EmployeePromotionSalaryStructures)
                    .HasForeignKey(d => d.SalaryStructureID)
                    .HasConstraintName("FK_EmployeePromotions_SalaryStructure");

                entity.HasOne(d => d.TimeSheetSalaryComponent)
                    .WithMany(p => p.EmployeePromotions)
                    .HasForeignKey(d => d.TimeSheetSalaryComponentID)
                    .HasConstraintName("FK_EmployeePromotions_SalaryComponents");
            });

            modelBuilder.Entity<EmployeePromotionLeaveAllocation>(entity =>
            {
                ////entity.Property(e => e.TimeStamps)
                //    //.IsRowVersion()
                //    //.IsConcurrencyToken();

                entity.HasOne(d => d.EmployeePromotion)
                    .WithMany(p => p.EmployeePromotionLeaveAllocations)
                    .HasForeignKey(d => d.EmployeePromotionID)
                    .HasConstraintName("FK_EmpLeavePromoLeavAllocations_LeaveTypes");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.EmployeePromotionLeaveAllocations)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_EmployeePromoLeaveAllocons_LeaveTypes");
            });

            modelBuilder.Entity<EmployeePromotionSalaryComponentMap>(entity =>
            {
                entity.HasOne(d => d.EmployeePromotion)
                    .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
                    .HasForeignKey(d => d.EmployeePromotionID)
                    .HasConstraintName("FK_EmployeePromotionSalaryComp_Promotion");

                entity.HasOne(d => d.EmployeeSalaryStructureComponentMap)
                    .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
                    .HasForeignKey(d => d.EmployeeSalaryStructureComponentMapID)
                    .HasConstraintName("FK_EmployeePromotion_SalaryComponentMaps");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmployeePromotionComp_SalaryComponents");
            });

            modelBuilder.Entity<EmployeeRole>(entity =>
            {
                entity.Property(e => e.EmployeeRoleID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EmployeeRoleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRoleMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeRoleMaps_Employees");

                entity.HasOne(d => d.EmployeeRole)
                    .WithMany(p => p.EmployeeRoleMaps)
                    .HasForeignKey(d => d.EmployeeRoleID)
                    .HasConstraintName("FK_EmployeeRoleMaps_EmployeeRoles");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.Property(e => e.HolidayIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                //entity.HasOne(d => d.Holidays)
                //    .WithMany(p => p.Holidays)
                //    .HasForeignKey(d => d.HolidayListID)
                //    .HasConstraintName("FK_Holidays_Holidays");

                //entity.HasOne(d => d.HolidayType)
                //    .WithMany(p => p.Holidays)
                //    .HasForeignKey(d => d.HolidayTypeID)
                //    .HasConstraintName("FK_Holidays_HolidayTypes");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.RequirePasswordReset).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                        .WithMany(p => p.Logins)
                        .HasForeignKey(d => d.RegisteredCountryID);
            });

            modelBuilder.Entity<LoginRoleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.LoginRoleMaps)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_LoginRoleMaps_Logins");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LoginRoleMaps)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_LoginRoleMaps_Roles");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PassageType>(entity =>
            {
                entity.Property(e => e.PassageTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<PassportVisaDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                    //.IsRowVersion()
                    //.IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.CountryofIssueID)
                    .HasConstraintName("FK_PassportVisa_CountryOfIssue");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.ReferenceID)
                    .HasConstraintName("FK_PassportVisa_Employee");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.SponsorID)
                    .HasConstraintName("FK_PassportVisa_Sponsor");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Setting>(entity =>
            //{
            //    entity.HasKey(e => new { e.SettingCode, e.CompanyID })
            //        .HasName("PK_TransactionHistoryArchive_TransactionID");

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.Settings)
            //        .HasForeignKey(d => d.CompanyID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Settings_Companies");
            //});

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.Property(e => e.JobTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TicketEntitilement>(entity =>
            {
                entity.Property(e => e.TicketEntitilementID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CountryAirport)
                    .WithMany(p => p.TicketEntitilements)
                    .HasForeignKey(d => d.CountryAirportID)
                    .HasConstraintName("FK_TicketEntitilements_Airport");
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.Property(e => e.AirportID).ValueGeneratedNever();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Airports_Countries");
            });


            modelBuilder.Entity<SalarySlip>(entity =>
            {
                ////entity.Property(e => e.TimeStamps)
                //    //.IsRowVersion()
                //    //.IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_SalarySlips_Employees");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_SalarySlips_SalaryComponents");

                entity.HasOne(d => d.SalarySlipStatus)
                    .WithMany(p => p.SalarySlips)
                    .HasForeignKey(d => d.SalarySlipStatusID)
                    .HasConstraintName("FK_SalarySlips_SalarySlipStatuses");
            });

            modelBuilder.Entity<Eduegate.Domain.Entity.HR.Models.Setting>(entity =>
            {
                entity.HasKey(e => new { e.SettingCode, e.CompanyID })
                    .HasName("PK_TransactionHistoryArchive_TransactionID");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Settings)
                //    .HasForeignKey(d => d.CompanyID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Settings_Companies");
            });

            modelBuilder.Entity<LeaveApplication>(entity =>
            {
                entity.Property(e => e.IsLeaveWithoutPay).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LeaveApplications_Employees");

                entity.HasOne(d => d.LeaveSession)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.FromSessionID)
                    .HasConstraintName("FK_LeaveApplications_LeaveApplications");

                entity.HasOne(d => d.LeaveStatus)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveStatusID)
                    .HasConstraintName("FK_LeaveApplications_LeaveStatuses");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_LeaveApplications_LeaveTypes");

                entity.HasOne(d => d.StaffLeaveReason)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.StaffLeaveReasonID)
                    .HasConstraintName("FK_LeaveApplications_LeaveApplications1");

                entity.HasOne(d => d.LeaveSession1)
                    .WithMany(p => p.LeaveApplications1)
                    .HasForeignKey(d => d.ToSessionID)
                    .HasConstraintName("FK_LeaveApplications_LeaveSessions");
            });

            modelBuilder.Entity<EmployeeSalaryStructureComponentMap>(entity =>
            {
                entity.HasOne(d => d.EmployeeSalaryStructure)
                    .WithMany(p => p.EmployeeSalaryStructureComponentMaps)
                    .HasForeignKey(d => d.EmployeeSalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_EmployeeSalaryStructures");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeSalaryStructureComponentMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_SalaryComponents");
            });
            modelBuilder.Entity<EmployeeSalaryStructureVariableMap>(entity =>
            {
                entity.HasOne(d => d.EmployeeSalaryStructureComponentMap)
                    .WithMany(p => p.EmployeeSalaryStructureVariableMaps)
                    .HasForeignKey(d => d.EmployeeSalaryStructureComponentMapID)
                    .HasConstraintName("FK_EmployeeSalaryStructureVariable_Structure");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeSalaryStructureVariableMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmployeeSalaryStructureVariable_Component");
            });

            modelBuilder.Entity<SalaryComponentVariableMap>(entity =>
            {
                entity.HasOne(d => d.SalaryStructureComponentMap)
                   .WithMany(p => p.SalaryComponentVariableMap)
                   .HasForeignKey(d => d.SalaryStructureComponentMapID)
                   .HasConstraintName("FK_SalaryComponentVariable_Structure");
                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.SalaryComponentVariableMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_SalaryComponentVariableMaps_Components");
            });


            modelBuilder.Entity<WPSDetail>(entity =>
            {
                entity.HasKey(e => e.WPSIID)
                    .HasName("PK_WPS_Detail");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PayerBankDetailI)
                    .WithMany(p => p.WPSDetails)
                    .HasForeignKey(d => d.PayerBankDetailIID)
                    .HasConstraintName("FK_WPSDetail_PayerBank");
            });

            modelBuilder.Entity<EmployeeSalaryStructureLeaveSalaryMap>(entity =>
            {
                entity.HasOne(d => d.EmployeeSalaryStructure)
                    .WithMany(p => p.EmployeeSalaryStructureLeaveSalaryMaps)
                    .HasForeignKey(d => d.EmployeeSalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_LeaveSalary");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeSalaryStructureLeaveSalaryMaps)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmployeeSalaryStructureLeaveSalary_Component");
            });


            modelBuilder.Entity<EmployeeSalarySettlement>(entity =>
            {
                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalarySettlements)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeSalarySettlements_Employees");

                entity.HasOne(d => d.EmployeeSalaryStructure)
                    .WithMany(p => p.EmployeeSalarySettlements)
                    .HasForeignKey(d => d.EmployeeSalaryStructureID)
                    .HasConstraintName("FK_EmployeeSalarySettlements_SalaryStructure");

                entity.HasOne(d => d.EmployeeSettlementStatus)
                    .WithMany(p => p.EmployeeSalarySettlements)
                    .HasForeignKey(d => d.EmployeeSettlementStatusID)
                    .HasConstraintName("FK_EmployeeSalarySettlements_EmployeeSettlementStatus");

                entity.HasOne(d => d.EmployeeSettlementType)
                    .WithMany(p => p.EmployeeSalarySettlements)
                    .HasForeignKey(d => d.EmployeeSettlementTypeID)
                    .HasConstraintName("FK_EmployeeSalarySettlements_EmployeeSettlementType");

                entity.HasOne(d => d.SalaryComponent)
                    .WithMany(p => p.EmployeeSalarySettlements)
                    .HasForeignKey(d => d.SalaryComponentID)
                    .HasConstraintName("FK_EmployeeSalarySettlements_SalaryComponent");
            });


            modelBuilder.Entity<LoanDetail>(entity =>
            {
                

                entity.HasOne(d => d.LoanEntryStatus)
                    .WithMany(p => p.LoanDetails)
                    .HasForeignKey(d => d.LoanEntryStatusID)
                    .HasConstraintName("FK_LoanDetail_LoanEntryStatus");

                entity.HasOne(d => d.LoanHead)
                    .WithMany(p => p.LoanDetails)
                    .HasForeignKey(d => d.LoanHeadID)
                    .HasConstraintName("FK_LoanDetail_Employee");
            });

            modelBuilder.Entity<LoanHead>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LoanHeads)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LoanHead_Employee");

                entity.HasOne(d => d.LoanStatus)
                   .WithMany(p => p.LoanHeads)
                   .HasForeignKey(d => d.LoanStatusID)
                   .HasConstraintName("FK_LoanHead_LoanStatus");

                entity.HasOne(d => d.LoanType)
                    .WithMany(p => p.LoanHeads)
                    .HasForeignKey(d => d.LoanTypeID)
                    .HasConstraintName("FK_LoanHead_LoanTypes");

                entity.HasOne(d => d.LoanRequest)
                    .WithMany(p => p.LoanHeads)
                    .HasForeignKey(d => d.LoanRequestID)
                    .HasConstraintName("FK_LoanHead_LoanRequest");
            });

            modelBuilder.Entity<LoanRequest>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LoanRequests)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LoanRequests_Employee");

                entity.HasOne(d => d.LoanRequestStatus)
                    .WithMany(p => p.LoanRequests)
                    .HasForeignKey(d => d.LoanRequestStatusID)
                    .HasConstraintName("FK_LoanRequests_LoanRequestStatus");

                entity.HasOne(d => d.LoanType)
                    .WithMany(p => p.LoanRequests)
                    .HasForeignKey(d => d.LoanTypeID)
                    .HasConstraintName("FK_LoanRequests_LoanTypes");
            });

            modelBuilder.Entity<LoanRequestSearch>(entity =>
            {
                entity.ToView("LoanRequestSearch", "payroll");
            });

            modelBuilder.Entity<LoanType>(entity =>
            {
                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.LoanTypes)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_LoanType_DocumentTypes");
            });

            modelBuilder.Entity<SalaryStructureScaleMap>(entity =>
            {
                entity.HasKey(e => e.StructureScaleID)
                    .HasName("PK_StructureScaleID");

                entity.HasOne(d => d.AccomodationType)
                    .WithMany(p => p.SalaryStructureScaleMaps)
                    .HasForeignKey(d => d.AccomodationTypeID)
                    .HasConstraintName("FK_SalaryStructureScaleMaps_Accomodation");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.SalaryStructureScaleMaps)
                    .HasForeignKey(d => d.MaritalStatusID)
                    .HasConstraintName("FK_SalaryStructureScaleMaps_MaritalStatus");

                entity.HasOne(d => d.SalaryStructure)
                    .WithMany(p => p.SalaryStructureScaleMaps)
                    .HasForeignKey(d => d.SalaryStructureID)
                    .HasConstraintName("FK_SalaryStructureScaleMaps_SalaryStructure");
            });

            modelBuilder.Entity<EmployeeJobDescription>(entity =>
            {
                entity.HasKey(e => e.JobDescriptionIID)
                    .HasName("PK_JobDescriptionIID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeJobDescriptionEmployees)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeJobDescription_Employee");

                entity.HasOne(d => d.ReportingToEmployee)
                    .WithMany(p => p.EmployeeJobDescriptionReportingToEmployees)
                    .HasForeignKey(d => d.ReportingToEmployeeID)
                    .HasConstraintName("FK_EmployeeJobDescription_ReportingToEmployee");
            });

            modelBuilder.Entity<EmployeeJobDescriptionDetail>(entity =>
            {
                entity.HasKey(e => e.JobDescriptionMapID)
                    .HasName("PK_JobDescriptionMapID");

                entity.HasOne(d => d.JobDescription)
                    .WithMany(p => p.EmployeeJobDescriptionDetails)
                    .HasForeignKey(d => d.JobDescriptionID)
                    .HasConstraintName("FK_EmployeeJobDescriptionDetail_JobDescription");
            });

            modelBuilder.Entity<JobDescription>(entity =>
            {
                entity.HasKey(e => e.JDMasterIID)
                    .HasName("PK_JDMasterIID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.JobDescriptions)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_JobDescription_Department");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.JobDescriptions)
                    .HasForeignKey(d => d.DesignationID)
                    .HasConstraintName("FK_JobDescription_Designation");

                entity.HasOne(d => d.ReportingToEmployee)
                    .WithMany(p => p.JobDescriptions)
                    .HasForeignKey(d => d.ReportingToEmployeeID)
                    .HasConstraintName("FK_JobDescription_ReportingToEmployee");
            });

            modelBuilder.Entity<JobDescriptionDetail>(entity =>
            {
                entity.HasKey(e => e.JDMapID)
                    .HasName("PK_JDMapID");

                entity.HasOne(d => d.JDMaster)
                    .WithMany(p => p.JobDescriptionDetails)
                    .HasForeignKey(d => d.JDMasterID)
                    .HasConstraintName("FK_JobDescriptionDetail_JobDescription");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}