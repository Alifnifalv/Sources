namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.LoginUserGroups")]
    public partial class LoginUserGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LoginUserGroupID { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
