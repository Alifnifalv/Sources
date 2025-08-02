using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Supports.Models.Mutual
{
    [Table("Cultures", Schema = "mutual")]
    public partial class Culture
    {
        public Culture()
        {
            CultureDatas = new HashSet<CultureData>();
            CustomerJustAsks = new HashSet<CustomerJustAsk>();
            CustomerSupportTickets = new HashSet<CustomerSupportTicket>();
        }

        [Key]
        public byte CultureID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string CultureCode { get; set; }

        [StringLength(100)]
        public string CultureName { get; set; }

        public virtual ICollection<CultureData> CultureDatas { get; set; }

        public virtual ICollection<CustomerJustAsk> CustomerJustAsks { get; set; }

        public virtual ICollection<CustomerSupportTicket> CustomerSupportTickets { get; set; }
    }
}