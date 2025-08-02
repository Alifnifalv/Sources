using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserViews", Schema = "setting")]
    public partial class UserView
    {
        public UserView()
        {
            UserViewColumnMaps = new HashSet<UserViewColumnMap>();
        }

        [Key]
        public long UserViewIID { get; set; }
        [StringLength(50)]
        public string UserViewName { get; set; }
        public long? LoginID { get; set; }
        public long? ViewID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("UserViews")]
        public virtual Login Login { get; set; }
        [ForeignKey("ViewID")]
        [InverseProperty("UserViews")]
        public virtual View View { get; set; }
        [InverseProperty("UserView")]
        public virtual ICollection<UserViewColumnMap> UserViewColumnMaps { get; set; }
    }
}
