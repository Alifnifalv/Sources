namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserViewColumnMaps", Schema = "setting")]
    public partial class UserViewColumnMap
    {
        [Key]
        public long UserViewColumnMapIID { get; set; }

        public long? UserViewID { get; set; }

        public long? ViewColumnID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual UserView UserView { get; set; }
    }
}
