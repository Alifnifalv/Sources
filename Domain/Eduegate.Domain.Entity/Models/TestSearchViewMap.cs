//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;

//namespace Eduegate.Domain.Entity.Models.Mapping
//{
//    public class TestSearchViewMap : EntityTypeConfiguration<TestSearchView>
//    {
//        public TestSearchViewMap()
//        {
//            // Primary Key
//            this.HasKey(t => new { t.WalletTransactionId, t.Amount, t.Description, t.TransactionRelationId, t.LanguageID, t.Expr1 });

//            // Properties
//            this.Property(t => t.WalletTransactionId)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//            this.Property(t => t.CustomerWalletTranRef)
//                .HasMaxLength(35);

//            this.Property(t => t.Amount)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//            this.Property(t => t.Description)
//                .IsRequired()
//                .HasMaxLength(50);

//            this.Property(t => t.TransactionRelationId)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//            this.Property(t => t.LanguageID)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//            this.Property(t => t.Expr1)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//            // Table & Column Mappings
//            this.ToTable("TestSearchView");
//            this.Property(t => t.WalletTransactionId).HasColumnName("WalletTransactionId");
//            this.Property(t => t.CustomerWalletTranRef).HasColumnName("CustomerWalletTranRef");
//            this.Property(t => t.Amount).HasColumnName("Amount");
//            this.Property(t => t.Description).HasColumnName("Description");
//            this.Property(t => t.TransactionRelationId).HasColumnName("TransactionRelationId");
//            this.Property(t => t.LanguageID).HasColumnName("LanguageID");
//            this.Property(t => t.Expr1).HasColumnName("Expr1");
//        }
//    }
//}
