namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ClaimTypes", Schema = "admin")]
    public partial class ClaimType
    {
        public ClaimType()
        {
            Claims = new HashSet<Claim>();
        }

        [Key]
        public int ClaimTypeID { get; set; }
        [StringLength(50)]
        public string ClaimTypeName { get; set; }

        //[InverseProperty("ClaimType")]
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
