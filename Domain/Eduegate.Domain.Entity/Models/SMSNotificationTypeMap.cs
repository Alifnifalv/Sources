//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;

//namespace Eduegate.Domain.Entity.Models.Mapping
//{
//    public class SMSNotificationTypeMap : EntityTypeConfiguration<SMSNotificationType>
//    {
//        public SMSNotificationTypeMap()
//        {
//            // Primary Key
//            this.HasKey(t => t.SMSNotificationTypeID);

//            // Properties
//            this.Property(t => t.SMSNotificationTypeID)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//            this.Property(t => t.Name)
//                .IsRequired()
//                .HasMaxLength(150);

//            this.Property(t => t.Description)
//                .HasMaxLength(500);

//            this.Property(t => t.TemplateFilePath)
//                .IsRequired()
//                .HasMaxLength(500);

//            this.Property(t => t.TimeStamp)
//                .IsFixedLength()
//                .HasMaxLength(8)
//                .IsRowVersion();

//            this.Property(t => t.CreatedBy)
//                .HasMaxLength(100);

//            this.Property(t => t.ModifiedBy)
//                .HasMaxLength(100);

//            this.Property(t => t.Subject)
//                .HasMaxLength(500);

//            this.Property(t => t.ToCC)
//                .HasMaxLength(200);

//            this.Property(t => t.ToBCC)
//                .HasMaxLength(200);

//            // Table & Column Mappings
//            this.ToTable("SMSNotificationTypes", "notification");
//            this.Property(t => t.SMSNotificationTypeID).HasColumnName("SMSNotificationTypeID");
//            this.Property(t => t.Name).HasColumnName("Name");
//            this.Property(t => t.Description).HasColumnName("Description");
//            this.Property(t => t.TemplateFilePath).HasColumnName("TemplateFilePath");
//            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
//            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
//            this.Property(t => t.TimeStamp).HasColumnName("TimeStamp");
//            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
//            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
//            this.Property(t => t.Subject).HasColumnName("Subject");
//            this.Property(t => t.ToCC).HasColumnName("ToCC");
//            this.Property(t => t.ToBCC).HasColumnName("ToBCC");
//        }
//    }
//}
