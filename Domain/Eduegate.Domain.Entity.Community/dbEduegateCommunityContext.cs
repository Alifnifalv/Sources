using Eduegate.Domain.Entity.Community.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Community
{
    public partial class dbEduegateCommunityContext : DbContext
    {
        public dbEduegateCommunityContext()
        {
        }

        public dbEduegateCommunityContext(DbContextOptions<dbEduegateCommunityContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Mahallus> Mahallus { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<MemberHealth> MemberHealths { get; set; }
        public virtual DbSet<CreatedByType> CreatedByTypes { get; set; }
        public virtual DbSet<EducationDetail> EducationDetails { get; set; }
        public virtual DbSet<EducationType> EducationTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<MemberPartner> MemberPartners { get; set; }
        public virtual DbSet<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }
        public virtual DbSet<OccupationType> OccupationTypes { get; set; }

        public virtual DbSet<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
        public virtual DbSet<QuestionnaireAnswerType> QuestionnaireAnswerTypes { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireSet> QuestionnaireSets { get; set; }
        public virtual DbSet<QuestionnaireType> QuestionnaireTypes { get; set; }
        public virtual DbSet<RelationWithHeadOfFamily> RelationWithHeadOfFamilies { get; set; }
        public virtual DbSet<SocialService> SocialServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Family>()
            //  .HasMany(e => e.Members)
            //  .WithOptional(e => e.Family)
            //  .HasForeignKey(e => e.FamilyID);

            //modelBuilder.Entity<Member>()
            //.HasMany(e => e.MemberHealths)
            //.WithOptional(e => e.Member)
            //.HasForeignKey(e => e.MemberID);

            //modelBuilder.Entity<CreatedByType>()
            // .HasMany(e => e.Members)
            // .WithOptional(e => e.CreatedByType)
            // .HasForeignKey(e => e.CreatedBy);

            //modelBuilder.Entity<Member>()
            //   .HasMany(e => e.EducationDetails)
            //   .WithOptional(e => e.Member)
            //   .HasForeignKey(e => e.MemberID);

            //modelBuilder.Entity<Member>()
            // .HasMany(e => e.MemberPartners)
            // .WithOptional(e => e.Member)
            // .HasForeignKey(e => e.SpouseMemberID);

            //modelBuilder.Entity<QuestionnaireAnswer>()
            //   .HasMany(e => e.MemberQuestionnaireAnswerMaps)
            //   .WithOptional(e => e.QuestionnaireAnswer)
            //   .HasForeignKey(e => e.QuestionnaireAnswerID);

            //modelBuilder.Entity<Questionnaire>()
            //   .HasMany(e => e.MemberQuestionnaireAnswerMaps)
            //   .WithOptional(e => e.Questionnaire)
            //   .HasForeignKey(e => e.QuestionnaireID);

            //modelBuilder.Entity<QuestionnaireAnswer>()
            //   .HasMany(e => e.MemberQuestionnaireAnswerMaps)
            //   .WithOptional(e => e.QuestionnaireAnswer)
            //   .HasForeignKey(e => e.QuestionnaireAnswerID);

            //modelBuilder.Entity<Questionnaire>()
            //    .HasMany(e => e.QuestionnaireAnswers)
            //    .WithOptional(e => e.Questionnaire)
            //    .HasForeignKey(e => e.QuestionnaireID);

            //modelBuilder.Entity<Questionnaire>()
            //    .HasMany(e => e.MemberQuestionnaireAnswerMaps)
            //    .WithOptional(e => e.Questionnaire)
            //    .HasForeignKey(e => e.QuestionnaireID);

            //modelBuilder.Entity<Member>()
            //  .HasMany(e => e.SocialServices)
            //  .WithOptional(e => e.Member)
            //  .HasForeignKey(e => e.MemberID);
        }

    }
}