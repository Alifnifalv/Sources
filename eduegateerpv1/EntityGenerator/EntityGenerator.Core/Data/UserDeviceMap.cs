using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserDeviceMaps", Schema = "mutual")]
    public partial class UserDeviceMap
    {
        [Key]
        public long UserDeviceMapIID { get; set; }
        public long? LoginID { get; set; }
        [StringLength(200)]
        public string DeviceToken { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsActive { get; set; }
        public long? RequestContentID { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("UserDeviceMaps")]
        public virtual Login Login { get; set; }
    }
}
