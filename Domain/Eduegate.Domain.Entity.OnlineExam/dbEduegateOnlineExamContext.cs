using Eduegate.Domain.Entity.OnlineExam.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.OnlineExam
{
    public partial class dbEduegateOnlineExamContext : DbContext
    {
        public dbEduegateOnlineExamContext()
        {
        }

        public dbEduegateOnlineExamContext(DbContextOptions<dbEduegateOnlineExamContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<AnswerType> AnswerTypes { get; set; }
        
        public virtual DbSet<CandidateAnswer> CandidateAnswers { get; set; }
        
        public virtual DbSet<CandidateAssesment> CandidateAssesments { get; set; }
        
        public virtual DbSet<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
        
        public virtual DbSet<Candidate> Candidates { get; set; }
       
        public virtual DbSet<OnlineExamOperationStatus> OnlineExamOperationStatuses { get; set; }
        
        public virtual DbSet<Eduegate.Domain.Entity.OnlineExam.Models.OnlineExam> OnlineExams { get; set; }
        
        public virtual DbSet<OnlineExamStatus> OnlineExamStatuses { get; set; }
       
        public virtual DbSet<QuestionAnswerMap> QuestionAnswerMaps { get; set; }
        
        public virtual DbSet<QuestionGroup> QuestionGroups { get; set; }
        
        public virtual DbSet<QuestionOptionMap> QuestionOptionMaps { get; set; }
        
        public virtual DbSet<Question> Questions { get; set; }
        
        public virtual DbSet<Student> Students { get; set; }
        
        public virtual DbSet<QuestionSelection> QuestionSelections { get; set; }
        
        public virtual DbSet<ExamQuestionGroupMap> ExamQuestionGroupMaps { get; set; }
        
        public virtual DbSet<OnlineExamResult> OnlineExamResults { get; set; }
     
        public virtual DbSet<OnlineExamSubjectMap> OnlineExamSubjectMaps { get; set; }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }

        public virtual DbSet<StudentPassportDetail> StudentPassportDetails { get; set; }

        public virtual DbSet<OnlineExamResultSubjectMap> OnlineExamResultSubjectMaps { get; set; }

        public virtual DbSet<OnlineExamQuestion> OnlineExamQuestions { get; set; }

        public virtual DbSet<OnlineExamType> OnlineExamTypes { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<PassageQuestion> PassageQuestions { get; set; }

        public virtual DbSet<MarkGrade> MarkGrades { get; set; }

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

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcademicYears)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcademicYears_Schools");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CandidateStatus)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.CandidateStatusID)
                    .HasConstraintName("FK_Candidate_CandidateStatus");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.Candidates)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Candidate_Class");

                //entity.HasOne(d => d.StudentApplication)
                //    .WithMany(p => p.Candidates)
                //    .HasForeignKey(d => d.StudentApplicationID)
                //    .HasConstraintName("FK_Candidate_StudentApplication");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_Candidates_Students");
            });

            modelBuilder.Entity<CandidateAnswer>(entity =>
            {
                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateAnswers)
                    .HasForeignKey(d => d.CandidateID)
                    .HasConstraintName("FK_CandidateAnswers_Candidate");

                entity.HasOne(d => d.CandidateOnlineExamMap)
                    .WithMany(p => p.CandidateAnswers)
                    .HasForeignKey(d => d.CandidateOnlineExamMapID)
                    .HasConstraintName("FK_CandidateAnswers_CandidateOnlineExamMap");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.CandidateAnswers)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_CandidateAnswers_Question");

                entity.HasOne(d => d.QuestionOptionMap)
                    .WithMany(p => p.CandidateAnswers)
                    .HasForeignKey(d => d.QuestionOptionMapID)
                    .HasConstraintName("FK_CandidateAnswers_QuestionOptionMap");
            });

            modelBuilder.Entity<CandidateAssesment>(entity =>
            {
                entity.HasOne(d => d.AnswerQuestionOptionMap)
                    .WithMany(p => p.CandidateAssesmentAnswerQuestionOptionMaps)
                    .HasForeignKey(d => d.AnswerQuestionOptionMapID)
                    .HasConstraintName("FK_CandidateAssesments_QuestionOptionMaps1");

                entity.HasOne(d => d.CandidateOnlinExamMap)
                    .WithMany(p => p.CandidateAssesments)
                    .HasForeignKey(d => d.CandidateOnlinExamMapID)
                    .HasConstraintName("FK_CandidateAssesments_CandidateOnlineExamMaps");

                entity.HasOne(d => d.SelectedQuestionOptionMap)
                    .WithMany(p => p.CandidateAssesmentSelectedQuestionOptionMaps)
                    .HasForeignKey(d => d.SelectedQuestionOptionMapID)
                    .HasConstraintName("FK_CandidateAssesments_QuestionOptionMaps");
            });

            modelBuilder.Entity<CandidateOnlineExamMap>(entity =>
            {
                entity.HasKey(e => e.CandidateOnlinExamMapIID)
                    .HasName("PK_CandidateOnlinExamMaps");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateOnlineExamMaps)
                    .HasForeignKey(d => d.CandidateID)
                    .HasConstraintName("FK_CandidateOnlinExamMaps_Candidates");

                entity.HasOne(d => d.OnlineExam)
                    .WithMany(p => p.CandidateOnlineExamMaps)
                    .HasForeignKey(d => d.OnlineExamID)
                    .HasConstraintName("FK_CandidateOnlinExamMaps_OnlineExams");

                entity.HasOne(d => d.OnlineExamOperationStatus)
                    .WithMany(p => p.CandidateOnlineExamMaps)
                    .HasForeignKey(d => d.OnlineExamOperationStatusID)
                    .HasConstraintName("FK_CandidateOnlineExamMaps_OnlineExamOperationStatuses");

                entity.HasOne(d => d.OnlineExamStatus)
                    .WithMany(p => p.CandidateOnlineExamMaps)
                    .HasForeignKey(d => d.OnlineExamStatusID)
                    .HasConstraintName("FK_CandidateOnlineExamMaps_OnlineExamStatuses");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassID).ValueGeneratedNever();

                entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Classes_AcademicYear");

                //entity.HasOne(d => d.CostCenter)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.CostCenterID)
                //    .HasConstraintName("FK_Classes_CostCenters");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Classes_Schools");
            });

            modelBuilder.Entity<ExamQuestionGroupMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.OnlineExam)
                    .WithMany(p => p.ExamQuestionGroupMaps)
                    .HasForeignKey(d => d.OnlineExamID)
                    .HasConstraintName("FK_ExamQuestionGroupMap_Exam");

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.ExamQuestionGroupMaps)
                    .HasForeignKey(d => d.QuestionGroupID)
                    .HasConstraintName("FK_ExamQuestionGroupMap_QuestionGroup");

                entity.HasOne(d => d.OnlineExamType)
                    .WithMany(p => p.ExamQuestionGroupMaps)
                    .HasForeignKey(d => d.OnlineExamTypeID)
                    .HasConstraintName("FK_ExamQuestionGroupMaps_ExamTypeID");
            });

            modelBuilder.Entity<Eduegate.Domain.Entity.OnlineExam.Models.OnlineExam>(entity =>
            {
                entity.HasKey(e => e.OnlineExamIID)
                    .HasName("PK_Exams");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.OnlineExams)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_OnlineExamResults_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.OnlineExams)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_OnlineExams_Classes");

                entity.HasOne(d => d.OnlineExamType)
                    .WithMany(p => p.OnlineExams)
                    .HasForeignKey(d => d.OnlineExamTypeID)
                    .HasConstraintName("FK_Exams_OnlineExamTypes");

                entity.HasOne(d => d.QuestionSelection)
                    .WithMany(p => p.OnlineExams)
                    .HasForeignKey(d => d.QuestionSelectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exams_QuestionSelections");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.OnlineExams)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_OnlineExamResults_school");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.OnlineExams)
                    .HasForeignKey(d => d.MarkGradeID)
                    .HasConstraintName("FK_OnlineExams_MarkGrades");
            });

            modelBuilder.Entity<OnlineExamQuestion>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.OnlineExamQuestions)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_OnlineExamQuestions_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.OnlineExamQuestions)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_OnlineExamQuestions_school");
            });

            modelBuilder.Entity<OnlineExamQuestionMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.OnlineExamQuestionMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_OnlineExamQuestionMaps_AcademicYear");

                entity.HasOne(d => d.OnlineExam)
                    .WithMany(p => p.OnlineExamQuestionMaps)
                    .HasForeignKey(d => d.OnlineExamID)
                    .HasConstraintName("FK_OnlineExamQuestionMaps_OnlineExam");

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.OnlineExamQuestionMaps)
                    .HasForeignKey(d => d.QuestionGroupID)
                    .HasConstraintName("FK_OnlineExamQuestionMaps_QuestionGroup");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.OnlineExamQuestionMaps)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_OnlineExamQuestionMaps_Question");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.OnlineExamQuestionMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_OnlineExamQuestionMaps_School");
            });

            modelBuilder.Entity<OnlineExamResult>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.OnlineExamResults)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_OnlineExamResultss_AcademicYear");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.OnlineExamResults)
                    .HasForeignKey(d => d.CandidateID)
                    .HasConstraintName("FK_OnlineExamResult_Candidates");

                entity.HasOne(d => d.OnlineExam)
                    .WithMany(p => p.OnlineExamResults)
                    .HasForeignKey(d => d.OnlineExamID)
                    .HasConstraintName("FK_OnlineExam_Exams");

                entity.HasOne(d => d.ResultStatus)
                    .WithMany(p => p.OnlineExamResults)
                    .HasForeignKey(d => d.ResultStatusID)
                    .HasConstraintName("FK_OnlineExamResults_ResultStatus");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.OnlineExamResults)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_OnlineExamResultss_school");
            });

            modelBuilder.Entity<OnlineExamResultQuestionMap>(entity =>
            {
                entity.HasKey(e => e.OnlineExamResultQuestionMapIID)
                    .HasName("PK_OnlineExamResultQuestionMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.OnlineExamResult)
                    .WithMany(p => p.OnlineExamResultQuestionMaps)
                    .HasForeignKey(d => d.OnlineExamResultID)
                    .HasConstraintName("FK_OnlineExamResultQuestionMaps_OnlineExamResult");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.OnlineExamResultQuestionMaps)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_OnlineExamResultQuestionMaps_Question");
            });

            modelBuilder.Entity<OnlineExamResultSubjectMap>(entity =>
            {
                entity.HasKey(e => e.OnlineExamResultSubjectMapIID)
                    .HasName("PK_OnlineExamResultSubjectMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.OnlineExamResult)
                    .WithMany(p => p.OnlineExamResultSubjectMaps)
                    .HasForeignKey(d => d.OnlineExamResultsID)
                    .HasConstraintName("FK_OnlineExamResusSubjeMaps_OnlineExamRes");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.OnlineExamResultSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_OnlineExamResultSubjectMaps_Subjects");
            });

            modelBuilder.Entity<OnlineExamStatus>(entity =>
            {
                entity.HasKey(e => e.ExamStatusID)
                    .HasName("PK_ExamStatuses");
            });

            modelBuilder.Entity<OnlineExamSubjectMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.OnlineExam)
                    .WithMany(p => p.OnlineExamSubjectMaps)
                    .HasForeignKey(d => d.OnlineExamID)
                    .HasConstraintName("FK_OnlineExamSubjectMap_Exams");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.OnlineExamSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_OnlineExamSubjectMaps_Subjects");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AnswerType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.AnswerTypeID)
                    .HasConstraintName("FK_Questions_AnswerTypes");

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionGroupID)
                    .HasConstraintName("FK_Questions_QuestionGroups");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_Questions_Subjects");

                entity.HasOne(d => d.PassageQuestion)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.PassageQuestionID)
                    .HasConstraintName("FK_Questions_PassageQuestion");
            });

            modelBuilder.Entity<QuestionAnswerMap>(entity =>
            {
                //entity.HasOne(d => d.Question)
                //    .WithMany(p => p.QuestionAnswerMaps)
                //    .HasForeignKey(d => d.QuestionID)
                //    .HasConstraintName("FK_QuestionAnswerMaps_Questions");

                //entity.HasOne(d => d.QuestionOptionMap)
                //    .WithMany(p => p.QuestionAnswerMaps)
                //    .HasForeignKey(d => d.QuestionOptionMapID)
                //    .HasConstraintName("FK_QuestionAnswerMaps_QuestionOptionMaps");
            });

            modelBuilder.Entity<QuestionGroup>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.QuestionGroups)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_QuestionGroups_Subject");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.QuestionGroups)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_QuestionGroups_Classes");
            });

            modelBuilder.Entity<QuestionOptionMap>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptionMaps)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_QuestionOptionMaps_Questions");
            });

            modelBuilder.Entity<School>(entity =>
            {
                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Schools)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Schools_Companies");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionID).ValueGeneratedNever();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Sections)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Sections_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Sections)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Sections_School");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => new { e.SettingCode, e.CompanyID })
                    .HasName("PK_TransactionHistoryArchive_TransactionID");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Settings)
                //    .HasForeignKey(d => d.CompanyID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Settings_Companies");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1-Active\r\n2-Transferred\r\n3-Discontinue\r\n");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.StudentAcademicYears)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Students_AcademicYear");

                //entity.HasOne(d => d.Application)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.ApplicationID)
                //    .HasConstraintName("FK_Students_StudentApplications");

                //entity.HasOne(d => d.BloodGroup)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.BloodGroupID)
                //    .HasConstraintName("FK_Students_BloodGroups");

                //entity.HasOne(d => d.Cast)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.CastID)
                //    .HasConstraintName("FK_Students_Casts");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.StudentClasses)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Students_Classes");

                //entity.HasOne(d => d.Community)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.CommunityID)
                //    .HasConstraintName("FK_Students_Communitys");

                //entity.HasOne(d => d.CurrentCountry)
                //    .WithMany(p => p.StudentCurrentCountries)
                //    .HasForeignKey(d => d.CurrentCountryID)
                //    .HasConstraintName("FK_Students_Countries");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_Students_Genders");

                //entity.HasOne(d => d.Grade)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.GradeID)
                //    .HasConstraintName("FK_Students_Grades");

                //entity.HasOne(d => d.Hostel)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.HostelID)
                //    .HasConstraintName("FK_Students_Hostels");

                //entity.HasOne(d => d.HostelRoom)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.HostelRoomID)
                //    .HasConstraintName("FK_Students_HostelRooms");

                //entity.HasOne(d => d.Login)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.LoginID)
                //    .HasConstraintName("FK_Students_Logins");

                //entity.HasOne(d => d.Parent)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.ParentID)
                //    .HasConstraintName("FK_Students_Parents");

                //entity.HasOne(d => d.PermenentCountry)
                //    .WithMany(p => p.StudentPermenentCountries)
                //    .HasForeignKey(d => d.PermenentCountryID)
                //    .HasConstraintName("FK_Students_Countries1");

                //entity.HasOne(d => d.PreviousSchoolClassCompleted)
                //    .WithMany(p => p.StudentPreviousSchoolClassCompleteds)
                //    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                //    .HasConstraintName("FK_Students_Classes1");

                //entity.HasOne(d => d.PreviousSchoolSyllabus)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.PreviousSchoolSyllabusID)
                //    .HasConstraintName("FK_Students_Syllabus");

                //entity.HasOne(d => d.PrimaryContact)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.PrimaryContactID)
                //    .HasConstraintName("FK_Students_GuardianTypes");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_Students_Relegions");

                //entity.HasOne(d => d.SchoolAcademicyear)
                //    .WithMany(p => p.StudentSchoolAcademicyears)
                //    .HasForeignKey(d => d.SchoolAcademicyearID)
                //    .HasConstraintName("FK_Students_SchoolAcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Students_School");

                //entity.HasOne(d => d.SecondLang)
                //    .WithMany(p => p.StudentSecondLangs)
                //    .HasForeignKey(d => d.SecondLangID)
                //    .HasConstraintName("FK_Students_N_SecondLanguage");

                //entity.HasOne(d => d.SecoundLanguage)
                //    .WithMany(p => p.StudentSecoundLanguages)
                //    .HasForeignKey(d => d.SecoundLanguageID)
                //    .HasConstraintName("FK_Students_SecondLang");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Students_Sections");

                //entity.HasOne(d => d.Stream)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StreamID)
                //    .HasConstraintName("FK_Students_Streams");

                //entity.HasOne(d => d.StudentCategory)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StudentCategoryID)
                //    .HasConstraintName("FK_Students_StudentCategories");

                //entity.HasOne(d => d.StudentHouse)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StudentHouseID)
                //    .HasConstraintName("FK_Students_StudentHouses");

                //entity.HasOne(d => d.SubjectMap)
                //    .WithMany(p => p.StudentSubjectMaps)
                //    .HasForeignKey(d => d.SubjectMapID)
                //    .HasConstraintName("FK_Students_SubjectMap");

                //entity.HasOne(d => d.ThirdLang)
                //    .WithMany(p => p.StudentThirdLangs)
                //    .HasForeignKey(d => d.ThirdLangID)
                //    .HasConstraintName("FK_Students_N_ThirdLanguage");

                //entity.HasOne(d => d.ThridLanguage)
                //    .WithMany(p => p.StudentThridLanguages)
                //    .HasForeignKey(d => d.ThridLanguageID)
                //    .HasConstraintName("FK_Students_ThirdLang");
            });

            modelBuilder.Entity<StudentApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationIID)
                    .HasName("PK_ApplicationIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.ApplicationStatus)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.ApplicationStatusID)
                //    .HasConstraintName("FK_StudentApplications_ApplicationStatuses");

                //entity.HasOne(d => d.ApplicationType)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.ApplicationTypeID)
                //    .HasConstraintName("FK_StudentApplications_SubmitType");

                //entity.HasOne(d => d.CanYouVolunteerToHelpOne)
                //    .WithMany(p => p.StudentApplicationCanYouVolunteerToHelpOnes)
                //    .HasForeignKey(d => d.CanYouVolunteerToHelpOneID)
                //    .HasConstraintName("FK_StudentApplications_VolunteerType");

                //entity.HasOne(d => d.CanYouVolunteerToHelpTwo)
                //    .WithMany(p => p.StudentApplicationCanYouVolunteerToHelpTwoes)
                //    .HasForeignKey(d => d.CanYouVolunteerToHelpTwoID)
                //    .HasConstraintName("FK_StudentApplications_VolunteerType1");

                //entity.HasOne(d => d.Cast)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.CastID)
                //    .HasConstraintName("FK_StudentApplications_Casts");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_StudentApplications_Classes");

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.StudentApplicationCountries)
                //    .HasForeignKey(d => d.CountryID)
                //    .HasConstraintName("FK_StudentApplications_Countries");

                //entity.HasOne(d => d.Curriculam)
                //    .WithMany(p => p.StudentApplicationCurriculams)
                //    .HasForeignKey(d => d.CurriculamID)
                //    .HasConstraintName("FK_StudentApplications_Syllabus");

                //entity.HasOne(d => d.FatherCountry)
                //    .WithMany(p => p.StudentApplicationFatherCountries)
                //    .HasForeignKey(d => d.FatherCountryID)
                //    .HasConstraintName("FK_StudentApplications_FatherNat");

                //entity.HasOne(d => d.FatherPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationFatherPassportDetailNoes)
                //    .HasForeignKey(d => d.FatherPassportDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_PassportDetailMaps");

                //entity.HasOne(d => d.FatherStudentRelationShip)
                //    .WithMany(p => p.StudentApplicationFatherStudentRelationShips)
                //    .HasForeignKey(d => d.FatherStudentRelationShipID)
                //    .HasConstraintName("FK_StudentApplications_GuardianTypes");

                //entity.HasOne(d => d.FatherVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationFatherVisaDetailNoes)
                //    .HasForeignKey(d => d.FatherVisaDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_VisaDetailMaps1");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_StudentApplications_Genders");

                //entity.HasOne(d => d.GuardianNationality)
                //    .WithMany(p => p.StudentApplicationGuardianNationalities)
                //    .HasForeignKey(d => d.GuardianNationalityID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_Nationality");

                //entity.HasOne(d => d.GuardianPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationGuardianPassportDetailNoes)
                //    .HasForeignKey(d => d.GuardianPassportDetailNoID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_PassportDetail");

                //entity.HasOne(d => d.GuardianStudentRelationShip)
                //    .WithMany(p => p.StudentApplicationGuardianStudentRelationShips)
                //    .HasForeignKey(d => d.GuardianStudentRelationShipID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_RelationShip");

                //entity.HasOne(d => d.GuardianVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationGuardianVisaDetailNoes)
                //    .HasForeignKey(d => d.GuardianVisaDetailNoID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_VisaDetail");

                //entity.HasOne(d => d.Login)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.LoginID)
                //    .HasConstraintName("FK_StudentApplications_Logins");

                //entity.HasOne(d => d.MotherCountry)
                //    .WithMany(p => p.StudentApplicationMotherCountries)
                //    .HasForeignKey(d => d.MotherCountryID)
                //    .HasConstraintName("FK_StudentApplications_MotherNat");

                //entity.HasOne(d => d.MotherPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationMotherPassportDetailNoes)
                //    .HasForeignKey(d => d.MotherPassportDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_PassportDetailMaps1");

                //entity.HasOne(d => d.MotherStudentRelationShip)
                //    .WithMany(p => p.StudentApplicationMotherStudentRelationShips)
                //    .HasForeignKey(d => d.MotherStudentRelationShipID)
                //    .HasConstraintName("FK_StudentApplications_GuardianTypes1");

                //entity.HasOne(d => d.MotherVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationMotherVisaDetailNoes)
                //    .HasForeignKey(d => d.MotherVisaDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_VisaDetailMaps2");

                //entity.HasOne(d => d.Nationality)
                //    .WithMany(p => p.StudentApplicationNationalities)
                //    .HasForeignKey(d => d.NationalityID)
                //    .HasConstraintName("FK_StudentApplications_Nationalities");

                //entity.HasOne(d => d.PreviousSchoolClassCompleted)
                //    .WithMany(p => p.StudentApplicationPreviousSchoolClassCompleteds)
                //    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                //    .HasConstraintName("FK_StudentApplications_Classes1");

                //entity.HasOne(d => d.PreviousSchoolSyllabus)
                //    .WithMany(p => p.StudentApplicationPreviousSchoolSyllabus)
                //    .HasForeignKey(d => d.PreviousSchoolSyllabusID)
                //    .HasConstraintName("FK_StudentApplications_StudentApplications");

                //entity.HasOne(d => d.PrimaryContact)
                //    .WithMany(p => p.StudentApplicationPrimaryContacts)
                //    .HasForeignKey(d => d.PrimaryContactID)
                //    .HasConstraintName("FK_StudentApplications_GuardianTypes2");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_StudentApplications_Relegions");

                //entity.HasOne(d => d.SchoolAcademicyear)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.SchoolAcademicyearID)
                //    .HasConstraintName("FK_StudentApplications_AcademicYears1");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_StudentApplications_School");

                //entity.HasOne(d => d.SecondLang)
                //    .WithMany(p => p.StudentApplicationSecondLangs)
                //    .HasForeignKey(d => d.SecondLangID)
                //    .HasConstraintName("FK_StudentApplications_N_SecondLanguage");

                //entity.HasOne(d => d.SecoundLanguage)
                //    .WithMany(p => p.StudentApplicationSecoundLanguages)
                //    .HasForeignKey(d => d.SecoundLanguageID)
                //    .HasConstraintName("FK_StudentApplications_SecondLang");

                //entity.HasOne(d => d.StreamGroup)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StreamGroupID)
                //    .HasConstraintName("FK_Studentapplications_StreamGroup");

                //entity.HasOne(d => d.Stream)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StreamID)
                //    .HasConstraintName("FK_Studentapplications_Stream");

                //entity.HasOne(d => d.StudentCategory)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StudentCategoryID)
                //    .HasConstraintName("FK_StudentApplications_StudentCategories");

                //entity.HasOne(d => d.StudentCoutryOfBrith)
                //    .WithMany(p => p.StudentApplicationStudentCoutryOfBriths)
                //    .HasForeignKey(d => d.StudentCoutryOfBrithID)
                //    .HasConstraintName("FK_StudentApplications_Countries4");

                //entity.HasOne(d => d.StudentPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationStudentPassportDetailNoes)
                //    .HasForeignKey(d => d.StudentPassportDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_PassportDetailMaps2");

                //entity.HasOne(d => d.StudentVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationStudentVisaDetailNoes)
                //    .HasForeignKey(d => d.StudentVisaDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_VisaDetailMaps");

                //entity.HasOne(d => d.ThirdLang)
                //    .WithMany(p => p.StudentApplicationThirdLangs)
                //    .HasForeignKey(d => d.ThirdLangID)
                //    .HasConstraintName("FK_StudentApplications_N_ThirdLanguage");

                //entity.HasOne(d => d.ThridLanguage)
                //    .WithMany(p => p.StudentApplicationThridLanguages)
                //    .HasForeignKey(d => d.ThridLanguageID)
                //    .HasConstraintName("FK_StudentApplications_ThirdLang");
            });

            modelBuilder.Entity<StudentPassportDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.CountryofBirth)
                //    .WithMany(p => p.StudentPassportDetailCountryofBirths)
                //    .HasForeignKey(d => d.CountryofBirthID)
                //    .HasConstraintName("FK_StudentPassportDetails_Countries2");

                //entity.HasOne(d => d.CountryofIssue)
                //    .WithMany(p => p.StudentPassportDetailCountryofIssues)
                //    .HasForeignKey(d => d.CountryofIssueID)
                //    .HasConstraintName("FK_StudentPassportDetails_Countries1");

                //entity.HasOne(d => d.Nationality)
                //    .WithMany(p => p.StudentPassportDetails)
                //    .HasForeignKey(d => d.NationalityID)
                //    .HasConstraintName("FK_StudentPassportDetails_StudentNat");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPassportDetails)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentPassportDetails_Students");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectID).ValueGeneratedNever();

                entity.Property(e => e.IsLanguage).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Subjects_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Subjects_School");

                //entity.HasOne(d => d.SubjectType)
                //    .WithMany(p => p.Subjects)
                //    .HasForeignKey(d => d.SubjectTypeID)
                //    .HasConstraintName("FK_Subjects_SubjectTypes");
            });

            modelBuilder.Entity<MarkGrade>(entity =>
            {
                entity.HasKey(e => e.MarkGradeIID)
                    .HasName("PK_MarkGradeGroups");
            });


            OnModelCreatingPartial(modelBuilder);


        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}