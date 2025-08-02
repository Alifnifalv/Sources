namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.UserDeviceMaps")]
    public partial class UserDeviceMap
    {
        [Key]
        public long UserDeviceMapIID { get; set; }

        public long? LoginID { get; set; }

        [StringLength(200)]
        public string DeviceToken { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? IsActive { get; set; }

        public long? RequestContentID { get; set; }

        public virtual Login Login { get; set; }
    }
}
