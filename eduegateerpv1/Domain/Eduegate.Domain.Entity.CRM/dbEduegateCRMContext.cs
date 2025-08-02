using Eduegate.Domain.Entity.CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.CRM
{

    public partial class dbEduegateCRMContext : DbContext
    {
        public dbEduegateCRMContext()
        {
        }

        public dbEduegateCRMContext(DbContextOptions<dbEduegateCRMContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<CRMCompany> CRMCompanies { get; set; }
        public virtual DbSet<IndustryType> IndustryTypes { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<LeadStatus> LeadStatuses { get; set; }
        public virtual DbSet<LeadType> LeadTypes { get; set; }
        public virtual DbSet<MarketSegment> MarketSegments { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
        public virtual DbSet<OpportunityFrom> OpportunityFroms { get; set; }
        public virtual DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public virtual DbSet<OpportunityType> OpportunityTypes { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Sequence> Sequences { get; set; }
        public virtual DbSet<Syllabu> Syllabus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassID).ValueGeneratedNever();

                //entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Classes_AcademicYear");

                //entity.HasOne(d => d.CostCenter)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.CostCenterID)
                //    .HasConstraintName("FK_Classes_CostCenters");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Classes_Schools");
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.HasKey(e => e.CommunicationIID)
                    .HasName("PK_LeadCommunications");

                entity.HasOne(d => d.CommunicationType)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.CommunicationTypeID)
                    .HasConstraintName("FK_LeadCommunications_CommunicationTypes");

                entity.HasOne(d => d.EmailTemplate)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.EmailTemplateID)
                    .HasConstraintName("FK_LeadCommunications_EmailTemplates");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.LeadID)
                    .HasConstraintName("FK_Communications_Leads");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<CRMCompany>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.EmailTemplateID).ValueGeneratedNever();
            });

            modelBuilder.Entity<IndustryType>(entity =>
            {
                entity.Property(e => e.IndustryTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Leads_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Leads_Classes");

                entity.HasOne(d => d.CRMCompany)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Leads_CRMCompanies");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.ContactID)
                    .HasConstraintName("FK_Leads_Contacts");

                entity.HasOne(d => d.Syllabu)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.CurriculamID)
                    .HasConstraintName("FK_Leads_Syllabus");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.GenderID)
                    .HasConstraintName("FK_Leads_Genders");

                entity.HasOne(d => d.IndustryType)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.IndustryTypeID)
                    .HasConstraintName("FK_Leads_IndustryTypes");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.LeadSourceID)
                    .HasConstraintName("FK_Leads_LeadSources");

                entity.HasOne(d => d.LeadStatus)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.LeadStatusID)
                    .HasConstraintName("FK_Leads_LeadStatus");

                entity.HasOne(d => d.LeadType)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.LeadTypeID)
                    .HasConstraintName("FK_Leads_LeadTypes");

                entity.HasOne(d => d.MarketSegment)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.MarketSegmentID)
                    .HasConstraintName("FK_Leads_MarketSegments");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_Lead_Nationality");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.RequestTypeID)
                    .HasConstraintName("FK_Leads_RequestTypes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Leads)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_leads_School");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Opportunity>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CRMCompany)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Opportunities_CRMCompanies");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.LeadID)
                    .HasConstraintName("FK_Opportunities_Leads");

                entity.HasOne(d => d.OpportunityFrom)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.OpportunityFromID)
                    .HasConstraintName("FK_Opportunities_OpportunityFroms");

                entity.HasOne(d => d.OpportunityStatus)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.OpportunityStatusID)
                    .HasConstraintName("FK_Opportunities_OpportunityStatuses");

                entity.HasOne(d => d.OpportunityType)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.OpportunityTypeID)
                    .HasConstraintName("FK_Opportunities_OpportunityTypes");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.SourcesID)
                    .HasConstraintName("FK_Opportunities_Sources");
            });

            modelBuilder.Entity<OpportunityType>(entity =>
            {
                entity.Property(e => e.OpportunityTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<School>(entity =>
            {
                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Schools)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Schools_Companies");
            });

            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.Property(e => e.SequenceID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.SourceID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Syllabu>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Syllabus)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Syllabus_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Syllabus)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Syllabus_School");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}