using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ReferFriendTokens", Schema = "marketing")]
    public partial class ReferFriendToken
    {
        [Key]
        public long ReferFriendTokenIID { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(50)]
        public string Token { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        public bool? IsUsed { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("ReferFriendTokens")]
        public virtual Customer Customer { get; set; }
    }
}
