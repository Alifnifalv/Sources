namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("MenuLinkCultureDatas", Schema = "setting")]
    public partial class MenuLinkCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MenuLinkID { get; set; }

        [StringLength(100)]
        public string MenuName { get; set; }

        [StringLength(50)]
        public string MenuTitle { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual MenuLink MenuLink { get; set; }
    }
}
