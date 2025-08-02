using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eduegate.Domain.Entity.Models.Clinic;

namespace Eduegate.Domain.Entity.Models.Mapping.Clinic
{
    public class AxDocumentMap : EntityTypeConfiguration<AxDocument>
    {
        public AxDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.AxID);

            // Properties
            this.Property(t => t.AxID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(220);

            this.Property(t => t.Extension)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Doc_Caption)
                .HasMaxLength(255);

            this.Property(t => t.Doc_AdditionalString)
                .HasMaxLength(255);

            this.Property(t => t.SourceURL)
                .HasMaxLength(1000);

            this.Property(t => t.SourceUsername)
                .HasMaxLength(100);

            this.Property(t => t.SourcePassword)
                .HasMaxLength(20);

            this.Property(t => t.KeyHeadline)
                .HasMaxLength(150);

            this.Property(t => t.KeyDescription)
                .HasMaxLength(512);

            this.Property(t => t.City)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("AxDocument");
            this.Property(t => t.AxID).HasColumnName("AxID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.FormatID).HasColumnName("FormatID");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.PublicationState).HasColumnName("PublicationState");
            this.Property(t => t.Doc_Caption).HasColumnName("Doc_Caption");
            this.Property(t => t.Doc_AdditionalString).HasColumnName("Doc_AdditionalString");
            this.Property(t => t.Doc_Description).HasColumnName("Doc_Description");
            this.Property(t => t.Doc_AdditionalDate).HasColumnName("Doc_AdditionalDate");
            this.Property(t => t.Doc_ValidFrom).HasColumnName("Doc_ValidFrom");
            this.Property(t => t.Doc_ValidTill).HasColumnName("Doc_ValidTill");
            this.Property(t => t.Doc_Keywords).HasColumnName("Doc_Keywords");
            this.Property(t => t.SourceType).HasColumnName("SourceType");
            this.Property(t => t.SourceHandle).HasColumnName("SourceHandle");
            this.Property(t => t.SourceURL).HasColumnName("SourceURL");
            this.Property(t => t.Published).HasColumnName("Published");
            this.Property(t => t.Publishable).HasColumnName("Publishable");
            this.Property(t => t.CurrentVersion).HasColumnName("CurrentVersion");
            this.Property(t => t.CurrentSubVersion).HasColumnName("CurrentSubVersion");
            this.Property(t => t.FileSize).HasColumnName("FileSize");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.SourceUsername).HasColumnName("SourceUsername");
            this.Property(t => t.SourcePassword).HasColumnName("SourcePassword");
            this.Property(t => t.CheckedOutByUser).HasColumnName("CheckedOutByUser");
            this.Property(t => t.KeyHeadline).HasColumnName("KeyHeadline");
            this.Property(t => t.KeyDescription).HasColumnName("KeyDescription");
            this.Property(t => t.City).HasColumnName("City");

            // Relationships
            this.HasOptional(t => t.AxDocument2)
                .WithMany(t => t.AxDocument1)
                .HasForeignKey(d => d.SourceID);
            //this.HasOptional(t => t.AxImageFormat)
            //    .WithMany(t => t.AxDocuments)
            //    .HasForeignKey(d => d.FormatID);
            //this.HasOptional(t => t.AxUser)
            //    .WithMany(t => t.AxDocuments)
            //    .HasForeignKey(d => d.Author);

        }
    }
}
