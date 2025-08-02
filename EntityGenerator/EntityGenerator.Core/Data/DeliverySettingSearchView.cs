using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DeliverySettingSearchView
    {
        public int DeliveryTypeID { get; set; }
        [StringLength(100)]
        public string DeliveryTypeName { get; set; }
        public int? Priority { get; set; }
        public byte? Description { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string TimeFrom { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string TimeTo { get; set; }
        public int? CompanyID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
    }
}
