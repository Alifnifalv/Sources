using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Organizations", Schema = "communities")]
    public partial class Organization
    {
        public Organization()
        {
            InverseParentOrganization = new HashSet<Organization>();
        }

        [Key]
        public int OrganizationID { get; set; }
        [StringLength(100)]
        public string OrganizationName { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        public int? ParentOrganizationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RegistrationID { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ParentOrganizationID")]
        [InverseProperty("InverseParentOrganization")]
        public virtual Organization ParentOrganization { get; set; }
        [InverseProperty("ParentOrganization")]
        public virtual ICollection<Organization> InverseParentOrganization { get; set; }
    }
}
