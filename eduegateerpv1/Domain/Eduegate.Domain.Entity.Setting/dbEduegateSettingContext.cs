using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Setting.Models
{
    public partial class dbEduegateSettingContext : DbContext
    {
        public dbEduegateSettingContext()
        {
        }

        public dbEduegateSettingContext(DbContextOptions<dbEduegateSettingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<IntegrationParameter> IntegrationParameters { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<Sequence> Sequences { get; set; }
        public virtual DbSet<DataFormat> DataFormats { get; set; }
        public virtual DbSet<DataFormatType> DataFormatTypes { get; set; }
        public virtual DbSet<DataHistoryEntity> DataHistoryEntities { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public virtual DbSet<FilterColumn> FilterColumns { get; set; }
        public virtual DbSet<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSettings { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
        public virtual DbSet<MenuLink> MenuLinks { get; set; }
        public virtual DbSet<MenuLinkType> MenuLinkTypes { get; set; }
        public virtual DbSet<ScreenField> ScreenFields { get; set; }
        public virtual DbSet<ScreenFieldSetting> ScreenFieldSettings { get; set; }
        public virtual DbSet<ScreenLookupMap> ScreenLookupMaps { get; set; }
        public virtual DbSet<ScreenMetadata> ScreenMetadatas { get; set; }
        public virtual DbSet<ScreenShortCut> ScreenShortCuts { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<ViewColumn> ViewColumns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.Property(e => e.SquenceID).ValueGeneratedNever();
            });

            

            modelBuilder.Entity<Claim>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    entity.HasOne(d => d.ClaimType)
                        .WithMany(p => p.Claims)
                        .HasForeignKey(d => d.ClaimTypeID)
                        .HasConstraintName("FK_Claims_ClaimTypes");
                });

                modelBuilder.Entity<ClaimLoginMap>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    entity.HasOne(d => d.Claim)
                        .WithMany(p => p.ClaimLoginMaps)
                        .HasForeignKey(d => d.ClaimID)
                        .HasConstraintName("FK_ClaimLoginMaps_ClaimLoginMaps");

                    entity.HasOne(d => d.Company)
                        .WithMany(p => p.ClaimLoginMaps)
                        .HasForeignKey(d => d.CompanyID)
                        .HasConstraintName("FK_ClaimLoginMaps_Companies");

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.ClaimLoginMaps)
                        .HasForeignKey(d => d.LoginID)
                        .HasConstraintName("FK_ClaimLoginMaps_Logins");
                });



                modelBuilder.Entity<ClaimSet>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();
                });

                modelBuilder.Entity<ClaimSetClaimMap>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                    //    .IsRowVersion()
                    //    .IsConcurrencyToken();

                    entity.HasOne(d => d.Claim)
                        .WithMany(p => p.ClaimSetClaimMaps)
                        .HasForeignKey(d => d.ClaimID)
                        .HasConstraintName("FK_ClaimSetClaimMaps_Claims");

                    entity.HasOne(d => d.ClaimSet)
                        .WithMany(p => p.ClaimSetClaimMaps)
                        .HasForeignKey(d => d.ClaimSetID)
                        .HasConstraintName("FK_ClaimSetClaimMaps_ClaimSets");
                });

            modelBuilder.Entity<ClaimSetClaimSetMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClaimSet)
                    .WithMany(p => p.ClaimSetClaimSetMapClaimSets)
                    .HasForeignKey(d => d.ClaimSetID)
                    .HasConstraintName("FK_ClaimSetClaimSetMaps_ClaimSets");

                entity.HasOne(d => d.LinkedClaimSet)
                    .WithMany(p => p.ClaimSetClaimSetMapLinkedClaimSets)
                    .HasForeignKey(d => d.LinkedClaimSetID)
                    .HasConstraintName("FK_ClaimSetClaimSetMaps_ClaimSets1");
            });

            modelBuilder.Entity<ClaimSetLoginMap>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    entity.HasOne(d => d.ClaimSet)
                        .WithMany(p => p.ClaimSetLoginMaps)
                        .HasForeignKey(d => d.ClaimSetID)
                        .HasConstraintName("FK_ClaimSetLoginMaps_ClaimSets");

                    entity.HasOne(d => d.Company)
                        .WithMany(p => p.ClaimSetLoginMaps)
                        .HasForeignKey(d => d.CompanyID)
                        .HasConstraintName("FK_ClaimSetLoginMaps_Companies");

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.ClaimSetLoginMaps)
                        .HasForeignKey(d => d.LoginID)
                        .HasConstraintName("FK_ClaimSetLoginMaps_Logins");
                });



                modelBuilder.Entity<ClaimType>(entity =>
                {
                    entity.Property(e => e.ClaimTypeID).ValueGeneratedNever();
                });


                modelBuilder.Entity<Company>(entity =>
                {
                    entity.Property(e => e.CompanyID).ValueGeneratedNever();

                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    //entity.HasOne(d => d.Currency)
                    //    .WithMany(p => p.Companies)
                    //    .HasForeignKey(d => d.BaseCurrencyID)
                    //    .HasConstraintName("FK_Companies_Currencies");

                    //entity.HasOne(d => d.CompanyGroup)
                    //    .WithMany(p => p.Companies)
                    //    .HasForeignKey(d => d.CompanyGroupID)
                    //    .HasConstraintName("FK_Companies_CompanyGroups");

                    //entity.HasOne(d => d.Country)
                    //    .WithMany(p => p.Companies)
                    //    .HasForeignKey(d => d.CountryID)
                    //    .HasConstraintName("FK_Companies_Companies");

                    //entity.HasOne(d => d.CompanyStatuses)
                    //    .WithMany(p => p.Companies)
                    //    .HasForeignKey(d => d.StatusID)
                    //    .HasConstraintName("FK_Companies_CompanyStatuses");
                });

                
                modelBuilder.Entity<DataFormat>(entity =>
                {
                    entity.Property(e => e.DataFormatID).ValueGeneratedNever();

                    entity.HasOne(d => d.DataFormatType)
                        .WithMany(p => p.DataFormats)
                        .HasForeignKey(d => d.DataFormatTypeID)
                        .HasConstraintName("FK_DataFormats_DataFormatTypes");
                });

                modelBuilder.Entity<DataFormatType>(entity =>
                {
                    entity.Property(e => e.DataFormatTypeID).ValueGeneratedNever();
                });

                modelBuilder.Entity<DataHistoryEntity>(entity =>
                {
                    entity.Property(e => e.DataHistoryEntityID).ValueGeneratedNever();
                });

                modelBuilder.Entity<FilterColumn>(entity =>
                {
                    entity.Property(e => e.FilterColumnID).ValueGeneratedNever();

                    entity.Property(e => e.IsQuickFilter).HasDefaultValueSql("((0))");

                    entity.HasOne(d => d.DataType)
                        .WithMany(p => p.FilterColumns)
                        .HasForeignKey(d => d.DataTypeID)
                        .HasConstraintName("FK_FilterColumns_DataTypes");

                    entity.HasOne(d => d.UIControlType)
                        .WithMany(p => p.FilterColumns)
                        .HasForeignKey(d => d.UIControlTypeID)
                        .HasConstraintName("FK_FilterColumns_UIControlTypes");

                    entity.HasOne(d => d.View)
                        .WithMany(p => p.FilterColumns)
                        .HasForeignKey(d => d.ViewID)
                        .HasConstraintName("FK_FilterColumns_Views");
                });

                modelBuilder.Entity<FilterColumnConditionMap>(entity =>
                {
                    entity.Property(e => e.FilterColumnConditionMapID).ValueGeneratedNever();

                    entity.HasOne(d => d.Condition)
                        .WithMany(p => p.FilterColumnConditionMaps)
                        .HasForeignKey(d => d.ConidtionID)
                        .HasConstraintName("FK_FilterColumnConditionMaps_Conditions");

                    entity.HasOne(d => d.DataType)
                        .WithMany(p => p.FilterColumnConditionMaps)
                        .HasForeignKey(d => d.DataTypeID)
                        .HasConstraintName("FK_FilterColumnConditionMaps_DataTypes");

                    entity.HasOne(d => d.FilterColumn)
                        .WithMany(p => p.FilterColumnConditionMaps)
                        .HasForeignKey(d => d.FilterColumnID)
                        .HasConstraintName("FK_FilterColumnConditionMaps_FilterColumns");
                });

                modelBuilder.Entity<FilterColumnUserValue>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    entity.HasOne(d => d.Condition)
                        .WithMany(p => p.FilterColumnUserValues)
                        .HasForeignKey(d => d.ConditionID)
                        .HasConstraintName("FK_FilterColumnUserValues_Conditions");

                    entity.HasOne(d => d.FilterColumn)
                        .WithMany(p => p.FilterColumnUserValues)
                        .HasForeignKey(d => d.FilterColumnID)
                        .HasConstraintName("FK_FilterColumnUserValues_FilterColumns");

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.FilterColumnUserValues)
                        .HasForeignKey(d => d.LoginID)
                        .HasConstraintName("FK_FilterColumnUserValues_Logins");

                    entity.HasOne(d => d.View)
                        .WithMany(p => p.FilterColumnUserValues)
                        .HasForeignKey(d => d.ViewID)
                        .HasConstraintName("FK_FilterColumnUserValues_Views");
                });

                modelBuilder.Entity<GlobalSetting>(entity =>
                {
                    entity.HasKey(e => e.GlobalSettingIID)
                        .HasName("PK_setting.GlobalSettings");
                });

            modelBuilder.Entity<IntegrationParameter>(entity =>
            {
                entity.Property(e => e.IntegrationParameterId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Login>(entity =>
                {
                    entity.Property(e => e.RequirePasswordReset).HasDefaultValueSql("((0))");

                    //entity.Property(e => e.TimeStamps)
                    //    .IsRowVersion()
                    //    .IsConcurrencyToken();
                });


                modelBuilder.Entity<Lookup>(entity =>
                {
                    entity.Property(e => e.LookupID).ValueGeneratedNever();

                    entity.Property(e => e.LookupType).IsFixedLength();
                });
                
                modelBuilder.Entity<MenuLink>(entity =>
                {
                    entity.Property(e => e.MenuLinkIID).ValueGeneratedNever();

                    //entity.Property(e => e.TimeStamps)
                    // .IsRowVersion()
                    // .IsConcurrencyToken();

                    entity.HasOne(d => d.MenuLinkType)
                        .WithMany(p => p.MenuLinks)
                        .HasForeignKey(d => d.MenuLinkTypeID)
                        .HasConstraintName("FK_MenuLinks_MenuLinkTypes");
                });

                
                modelBuilder.Entity<MenuLinkCultureData>(entity =>
                {
                    entity.HasKey(e => new { e.CultureID, e.MenuLinkID });

                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    entity.HasOne(d => d.Culture)
                        .WithMany(p => p.MenuLinkCultureDatas)
                        .HasForeignKey(d => d.CultureID)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MenuLinkCultureDatas_MenuLinkCultureDatas");

                    entity.HasOne(d => d.MenuLink)
                        .WithMany(p => p.MenuLinkCultureDatas)
                        .HasForeignKey(d => d.MenuLinkID)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MenuLinkCultureDatas_MenuLinks");
                });

                modelBuilder.Entity<Property>(entity =>
                {
                    entity.HasKey(e => e.PropertyIID)
                        .HasName("PK_ProductProperties");

                    //entity.Property(e => e.TimeStamps)
                        //.IsRowVersion()
                        //.IsConcurrencyToken();

                    //entity.HasOne(d => d.PropertyType)
                    //    .WithMany(p => p.Properties)
                    //    .HasForeignKey(d => d.PropertyTypeID)
                    //    .HasConstraintName("FK_Properties_PropertyTypes");

                    //entity.HasOne(d => d.UIControlType)
                    //    .WithMany(p => p.Properties)
                    //    .HasForeignKey(d => d.UIControlTypeID)
                    //    .HasConstraintName("FK_ProductProperties_UIControlTypes");

                    //entity.HasOne(d => d.UIControlValidation)
                    //    .WithMany(p => p.Properties)
                    //    .HasForeignKey(d => d.UIControlValidationID)
                    //    .HasConstraintName("FK_ProductProperties_UIControlValidations");
                });

                modelBuilder.Entity<ScreenField>(entity =>
                {
                    entity.Property(e => e.ScreenFieldID).ValueGeneratedNever();
                });

            modelBuilder.Entity<ScreenFieldSetting>(entity =>
            {
                entity.Property(e => e.ScreenFieldSettingID).ValueGeneratedNever();

                entity.HasOne(d => d.ScreenField)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.ScreenFieldID)
                    .HasConstraintName("FK_ScreenFieldSettings_ScreenFields");

                entity.HasOne(d => d.ScreenMetadata)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.ScreenID)
                    .HasConstraintName("FK_ScreenFieldSettings_ScreenMetadatas");

                entity.HasOne(d => d.Squence)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.SequenceID)
                    .HasConstraintName("FK_ScreenFieldSettings_Squences");

                entity.HasOne(d => d.TextTransformType)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.TextTransformTypeId)
                    .HasConstraintName("FK_ScreenFieldSettings_TextTransformTypes");
            });

            modelBuilder.Entity<ScreenLookupMap>(entity =>
                {
                    entity.Property(e => e.ScreenLookupMapID).ValueGeneratedNever();

                    entity.HasOne(d => d.ScreenMetadata)
                        .WithMany(p => p.ScreenLookupMaps)
                        .HasForeignKey(d => d.ScreenID)
                        .HasConstraintName("FK_ScreenLookupMaps_ScreenMetadatas");
                });

                modelBuilder.Entity<ScreenMetadata>(entity =>
                {
                    entity.HasKey(e => e.ScreenID)
                        .HasName("PK_CRUDMetadatas");

                    entity.Property(e => e.ScreenID).ValueGeneratedNever();

                    //entity.HasOne(d => d.View)
                    //    .WithMany(p => p.ScreenMetadatas)
                    //    .HasForeignKey(d => d.ViewID)
                    //    .HasConstraintName("FK_CRUDMetadatas_Views");
                });

               

                modelBuilder.Entity<ScreenShortCut>(entity =>
                {
                    entity.Property(e => e.ScreenShortCutID).ValueGeneratedNever();

                    //entity.HasOne(d => d.Screen)
                    //    .WithMany(p => p.ScreenShortCuts)
                    //    .HasForeignKey(d => d.ScreenID)
                    //    .HasConstraintName("FK_ScreenShortCuts_ScreenMetadatas");
                });

                //modelBuilder.Entity<Sequence>(entity =>
                //{
                //    entity.Property(e => e.SequenceID).ValueGeneratedNever();
                //});

                modelBuilder.Entity<Setting>(entity =>
                {
                    entity.HasKey(e => new { e.SettingCode, e.CompanyID })
                        .HasName("PK_TransactionHistoryArchive_TransactionID");

                    entity.HasOne(d => d.Company)
                            .WithMany(p => p.Settings)
                            .HasForeignKey(d => d.CompanyID)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_Settings_Companies");
                });

            modelBuilder.Entity<UserDataFormatMap>(entity =>
                {
                    //entity.Property(e => e.TimeStamps)
                        //.IsRowVersion()
                        //.IsConcurrencyToken();

                    entity.HasOne(d => d.DataFormat)
                        .WithMany(p => p.UserDataFormatMaps)
                        .HasForeignKey(d => d.DataFormatID)
                        .HasConstraintName("FK_UserDataFormatMaps_DataFormats");

                    entity.HasOne(d => d.DataFormatType)
                        .WithMany(p => p.UserDataFormatMaps)
                        .HasForeignKey(d => d.DataFormatTypeID)
                        .HasConstraintName("FK_UserDataFormatMaps_DataFormatTypes");

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.UserDataFormatMaps)
                        .HasForeignKey(d => d.LoginID)
                        .HasConstraintName("FK_UserDataFormatMaps_Logins");
                });

                modelBuilder.Entity<UserScreenFieldSetting>(entity =>
                {
                    entity.Property(e => e.UserScreenFieldSettingID).ValueGeneratedNever();

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.UserScreenFieldSettings)
                        .HasForeignKey(d => d.LoginID)
                        .HasConstraintName("FK_UserScreenFieldSettings_Logins");

                    entity.HasOne(d => d.ScreenField)
                        .WithMany(p => p.UserScreenFieldSettings)
                        .HasForeignKey(d => d.ScreenFieldID)
                        .HasConstraintName("FK_UserScreenFieldSettings_ScreenFields");

                    //entity.HasOne(d => d.Screen)
                    //    .WithMany(p => p.UserScreenFieldSettings)
                    //    .HasForeignKey(d => d.ScreenID)
                    //    .HasConstraintName("FK_UserScreenFieldSettings_ScreenMetadatas");
                });

                modelBuilder.Entity<UserSetting>(entity =>
                {
                    entity.HasKey(e => new { e.LoginID, e.SettingCode, e.CompanyID });

                    //entity.HasOne(d => d.Company)
                    //    .WithMany(p => p.UserSettings)
                    //    .HasForeignKey(d => d.CompanyID)
                    //    .OnDelete(DeleteBehavior.ClientSetNull)
                    //    .HasConstraintName("FK_UserSettings_Companies");

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.UserSettings)
                        .HasForeignKey(d => d.LoginID)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserSettings_Logins");
                });

                modelBuilder.Entity<UserView>(entity =>
                {
                    entity.HasKey(e => e.UserViewIID)
                        .HasName("PK_SearchUserViews");

                    //entity.Property(e => e.TimeStamps)
                    // .IsRowVersion()
                    // .IsConcurrencyToken();

                    entity.HasOne(d => d.Login)
                        .WithMany(p => p.UserViews)
                        .HasForeignKey(d => d.LoginID)
                        .HasConstraintName("FK_UserViews_UserViews");

                    entity.HasOne(d => d.View)
                        .WithMany(p => p.UserViews)
                        .HasForeignKey(d => d.ViewID)
                        .HasConstraintName("FK_SearchUserViews_SearchViews");
                });

                modelBuilder.Entity<UserViewColumnMap>(entity =>
                {
                    entity.HasKey(e => e.UserViewColumnMapIID)
                        .HasName("PK_UserViewColumnMap");

                    //entity.Property(e => e.TimeStamps)
                       // .IsRowVersion()
                       // .IsConcurrencyToken();

                    entity.HasOne(d => d.UserView)
                        .WithMany(p => p.UserViewColumnMaps)
                        .HasForeignKey(d => d.UserViewID)
                        .HasConstraintName("FK_UserViewColumnMap_UserViews");
                });

                modelBuilder.Entity<View>(entity =>
                {
                    entity.Property(e => e.ViewID).ValueGeneratedNever();

                    //entity.HasOne(d => d.ChildView)
                    //    .WithMany(p => p.InverseChildView)
                    //    .HasForeignKey(d => d.ChildViewID)
                    //    .HasConstraintName("FK_Views_Views1");

                    //entity.HasOne(d => d.FilterQueries)
                    //    .WithMany(p => p.Views)
                    //    .HasForeignKey(d => d.FilterQueriesID)
                    //    .HasConstraintName("FK_Views_FilterQueries");

                    entity.HasOne(d => d.ViewType)
                        .WithMany(p => p.Views)
                        .HasForeignKey(d => d.ViewTypeID)
                        .HasConstraintName("FK_Views_ViewTypes");
                });

                modelBuilder.Entity<ViewAction>(entity =>
                {
                    entity.Property(e => e.ViewActionID).ValueGeneratedNever();

                    entity.HasOne(d => d.View)
                        .WithMany(p => p.ViewActions)
                        .HasForeignKey(d => d.ViewID)
                        .HasConstraintName("FK_ViewActions_Views");
                });

                modelBuilder.Entity<ViewColumn>(entity =>
                {
                    entity.Property(e => e.ViewColumnID).ValueGeneratedNever();

                    entity.Property(e => e.IsExcludeForExport).HasDefaultValueSql("((1))");

                    entity.Property(e => e.IsExpression).HasDefaultValueSql("((0))");

                    entity.HasOne(d => d.View)
                        .WithMany(p => p.ViewColumns)
                        .HasForeignKey(d => d.ViewID)
                        .HasConstraintName("FK_ViewGridColumns_Views");
                });
                
                

                OnModelCreatingPartial(modelBuilder);
            }
        

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}