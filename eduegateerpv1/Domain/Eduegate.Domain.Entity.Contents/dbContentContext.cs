using Eduegate.Domain.Entity.Contents.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Contents
{
    public partial class dbContentContext : DbContext
    {
        public dbContentContext()
        {
        }

        public dbContentContext(DbContextOptions<dbContentContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetContentConnectionString());
            }
        }

        public virtual DbSet<ContentFile> ContentFiles { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentFile>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.ContentFiles)
                    .HasForeignKey(d => d.ContentTypeID)
                    .HasConstraintName("FK_ContentFiles_ContentTypes");
            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.Property(e => e.ContentTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.Property(e => e.ISActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Galleries)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Galleries_AcademicYears");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Galleries)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Galleries_Schools");
            });

            modelBuilder.Entity<GalleryAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.GalleryAttachmentMaps)
                    .HasForeignKey(d => d.AttachmentContentID)
                    .HasConstraintName("FK_GalleryAttachmentMap_Content");

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.GalleryAttachmentMaps)
                    .HasForeignKey(d => d.GalleryID)
                    .HasConstraintName("FK_GalleryAttachmentMap_Gallery");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}