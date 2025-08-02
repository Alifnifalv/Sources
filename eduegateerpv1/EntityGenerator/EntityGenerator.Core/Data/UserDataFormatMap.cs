using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("DataFormatID")]
        [InverseProperty("UserDataFormatMaps")]
        public virtual DataFormat DataFormat { get; set; }
        [ForeignKey("DataFormatTypeID")]
        [InverseProperty("UserDataFormatMaps")]
        public virtual DataFormatType DataFormatType { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("UserDataFormatMaps")]
        public virtual Login Login { get; set; }
    }
}
