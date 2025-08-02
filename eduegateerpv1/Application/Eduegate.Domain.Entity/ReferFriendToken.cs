namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.ReferFriendTokens")]
    public partial class ReferFriendToken
    {
        [Key]
        public long ReferFriendTokenIID { get; set; }

        public long? CustomerID { get; set; }

        [StringLength(50)]
        public string Token { get; set; }

        public DateTime? Created { get; set; }

        public bool? IsUsed { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
