using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class UserDeviceMapSearchView
    {
        public long UserDeviceMapIID { get; set; }
        public long? LoginID { get; set; }
        [StringLength(100)]
        public string LoginUserID { get; set; }
        [StringLength(200)]
        public string DeviceToken { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        public long? RequestContentID { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
