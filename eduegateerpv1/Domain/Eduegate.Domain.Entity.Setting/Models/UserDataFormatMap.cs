namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("UserDataFormatMaps", Schema = "setting")]
    public partial class UserDataFormatMap
    {
        [Key]
        public long UserDataFormatIID { get; set; }

        public long? LoginID { get; set; }

        public short? DataFormatTypeID { get; set; }

        public int? DataFormatID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Login Login { get; set; }

        public virtual DataFormat DataFormat { get; set; }

        public virtual DataFormatType DataFormatType { get; set; }
    }
}
