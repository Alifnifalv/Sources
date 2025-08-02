namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Groups")]
    public partial class Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            Accounts = new HashSet<Account>();
        }

        public int GroupID { get; set; }

        [StringLength(50)]
        public string GroupCode { get; set; }

        [StringLength(100)]
        public string GroupName { get; set; }

        public int? Parent_ID { get; set; }

        public int Affect_ID { get; set; }

        [StringLength(50)]
        public string Default_Side { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public bool? IsSystemDefined { get; set; }

        public bool? AllowUserDelete { get; set; }

        public bool? AllowUserEdit { get; set; }

        public bool? AllowAddSubGroup { get; set; }

        public bool? AllowUserRename { get; set; }

        public bool? IsHidden { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
