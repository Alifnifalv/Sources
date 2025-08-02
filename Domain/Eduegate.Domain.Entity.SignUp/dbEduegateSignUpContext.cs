using Eduegate.Domain.Entity.SignUp.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.SignUp
{
    public partial class dbEduegateSignUpContext : DbContext
    {
        public dbEduegateSignUpContext()
        {
        }

        public dbEduegateSignUpContext(DbContextOptions<dbEduegateSignUpContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<Signup> Signups { get; set; }
        public virtual DbSet<SignupAudienceMap> SignupAudienceMaps { get; set; }
        public virtual DbSet<SignupCategory> SignupCategories { get; set; }
        public virtual DbSet<SignupGroup> SignupGroups { get; set; }
        public virtual DbSet<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        public virtual DbSet<SignupSlotMap> SignupSlotMaps { get; set; }
        public virtual DbSet<SignupSlotType> SignupSlotTypes { get; set; }
        public virtual DbSet<SignupType> SignupTypes { get; set; }
        public virtual DbSet<SignupStatus> SignupStatuses { get; set; }
        public virtual DbSet<SlotMapStatus> SlotMapStatuses { get; set; }
        public virtual DbSet<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }

        public virtual DbSet<MeetingRequest> MeetingRequests { get; set; }
        public virtual DbSet<MeetingRequestStatus> MeetingRequestStatuses { get; set; }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AcademicYearStatu> AcademicYearStatus { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Signup>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Signups_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Signups_Class");

                entity.HasOne(d => d.OrganizerEmployee)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.OrganizerEmployeeID)
                    .HasConstraintName("FK_MeetingSlotMaps_Employee");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Signups_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Signups_Section");

                entity.HasOne(d => d.SignupCategory)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SignupCategoryID)
                    .HasConstraintName("FK_Signups_Category");

                entity.HasOne(d => d.SignupGroup)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SignupGroupID)
                    .HasConstraintName("FK_Signups_SignupGroup");

                entity.HasOne(d => d.SignupStatus)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SignupStatusID)
                    .HasConstraintName("FK_Signups_SignupStatus");

                entity.HasOne(d => d.SignupType)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SignupTypeID)
                    .HasConstraintName("FK_Signups_SignupType");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_Signups_Student");
            });

            modelBuilder.Entity<SignupAudienceMap>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TeacherSignupAudience_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_SignupAudience_EmployeeIID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_SignupAudience_Parent");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_TeacherSignupAudience_School");

                entity.HasOne(d => d.Signup)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.SignupID)
                    .HasConstraintName("FK_SignupAudience_Signup");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_SignupAudience_Student");
            });

            modelBuilder.Entity<SignupCategory>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SignupSlotAllocationMap>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TeacherSignupSlotAllocation_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_SignupSlotAllocation_EmployeeIID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_SignupSlotAllocation_Parent");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_TeacherSignupSlotAllocation_School");

                entity.HasOne(d => d.SignupSlotMap)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.SignupSlotMapID)
                    .HasConstraintName("FK_SignupSlotAllocation_Signup");

                entity.HasOne(d => d.SlotMapStatus)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.SlotMapStatusID)
                    .HasConstraintName("FK_SignupSlotAllocationMap_SlotMapStatus");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_SignupSlotAllocation_Student");
            });

            modelBuilder.Entity<SignupSlotMap>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_SignupSlotMaps_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_SignupSlotMap_School");

                entity.HasOne(d => d.Signup)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.SignupID)
                    .HasConstraintName("FK_SignupSlotMaps_Signups");

                entity.HasOne(d => d.SignupSlotType)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.SignupSlotTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SignupSlotMap_SlotType");

                entity.HasOne(d => d.SlotMapStatus)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.SlotMapStatusID)
                    .HasConstraintName("FK_SignupSlotMaps_SlotMapStatus");
            });

            modelBuilder.Entity<SignupSlotType>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SignupType>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.Property(e => e.AcademicYearID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYearStatus)
                    .WithMany(p => p.AcademicYears)
                    .HasForeignKey(d => d.AcademicYearStatusID)
                    .HasConstraintName("FK_AcademicYears_AcademicYearStatus");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcademicYears)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcademicYears_Schools");
            });

            modelBuilder.Entity<AcademicYearStatu>(entity =>
            {
                entity.HasKey(e => e.AcademicYearStatusID)
                    .HasName("PK_AcademYearStatus");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassID).ValueGeneratedNever();

                entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Classes_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Classes_Schools");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.IsOTEligible).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOverrideLeaveGroup).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Employees_Logins");

                entity.HasOne(d => d.ReportingEmployee)
                    .WithMany(p => p.InverseReportingEmployee)
                    .HasForeignKey(d => d.ReportingEmployeeID)
                    .HasConstraintName("FK_Employees_Employees");

                entity.HasOne(d => d.ResidencyCompany)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ResidencyCompanyId)
                    .HasConstraintName("FK_Employees_Companies");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.RequirePasswordReset).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Parents_AcademicYear");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Parents_Logins");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Parents_School");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Schools_Companies");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionID).ValueGeneratedNever();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Sections_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Sections_School");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1-Active\r\n2-Transferred\r\n3-Discontinue\r\n");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentAcademicYears)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Students_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Students_Classes");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Students_Logins");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_Students_Parents");

                entity.HasOne(d => d.PreviousSchoolClassCompleted)
                    .WithMany(p => p.StudentPreviousSchoolClassCompleteds)
                    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                    .HasConstraintName("FK_Students_Classes1");

                entity.HasOne(d => d.SchoolAcademicyear)
                    .WithMany(p => p.StudentSchoolAcademicyears)
                    .HasForeignKey(d => d.SchoolAcademicyearID)
                    .HasConstraintName("FK_Students_SchoolAcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Students_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Students_Sections");
            });

            modelBuilder.Entity<SignupGroup>(entity =>
            {
                entity.Property(e => e.SignupGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SignupSlotRemarkMap>(entity =>
            {
                entity.HasOne(d => d.Signup)
                    .WithMany(p => p.SignupSlotRemarkMaps)
                    .HasForeignKey(d => d.SignupID)
                    .HasConstraintName("FK_SignupSlotRemarkMap_Signup");

                entity.HasOne(d => d.SignupSlotAllocationMap)
                    .WithMany(p => p.SignupSlotRemarkMaps)
                    .HasForeignKey(d => d.SignupSlotAllocationMapID)
                    .HasConstraintName("FK_SignupSlotRemarkMap_SignupSlotAllocation");

                entity.HasOne(d => d.SignupSlotMap)
                    .WithMany(p => p.SignupSlotRemarkMaps)
                    .HasForeignKey(d => d.SignupSlotMapID)
                    .HasConstraintName("FK_SignupSlotRemarkMap_SignupSlotMap");
            });

            modelBuilder.Entity<MeetingRequest>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_MeetingRequest_AcademicYear");

                entity.HasOne(d => d.ApprovedSignupSlotMap)
                    .WithMany(p => p.MeetingRequestApprovedSignupSlotMaps)
                    .HasForeignKey(d => d.ApprovedSignupSlotMapID)
                    .HasConstraintName("FK_MeetingRequest_ApprovedSignupSlotMap");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_MeetingRequest_Class");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.FacultyID)
                    .HasConstraintName("FK_MeetingRequest_EmployeeIID");

                entity.HasOne(d => d.MeetingRequestStatus)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.MeetingRequestStatusID)
                    .HasConstraintName("FK_MeetingRequest_MeetingRequestStatus");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_MeetingRequest_Parent");

                entity.HasOne(d => d.RequestedSignupSlotMap)
                    .WithMany(p => p.MeetingRequestRequestedSignupSlotMaps)
                    .HasForeignKey(d => d.RequestedSignupSlotMapID)
                    .HasConstraintName("FK_MeetingRequest_RequestedSignupSlotMap");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_MeetingRequest_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_MeetingRequest_Section");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.MeetingRequests)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_MeetingRequest_Student");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}