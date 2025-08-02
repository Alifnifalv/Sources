using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("DepreciationTypes", Schema = "asset")]
    public partial class DepreciationType
    {
        public DepreciationType()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public int DepreciationTypeID { get; set; }

        [StringLength(100)]
        public string TypeName { get; set; }

        [StringLength(10)]
        public string TypeShortName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}