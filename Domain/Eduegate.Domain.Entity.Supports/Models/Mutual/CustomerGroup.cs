using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models.Mutual
{
    [Table("CustomerGroups", Schema = "mutual")]
    public partial class CustomerGroup
    {
        public CustomerGroup()
        {
            //CustomerGroupDeliveryTypeMaps = new HashSet<CustomerGroupDeliveryTypeMap>();
            Customers = new HashSet<Customer>();
            //ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            //ProductPriceListSkumaps = new HashSet<ProductPriceListSkumap>();
        }

        [Key]
        public long CustomerGroupIID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PointLimit { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }

        //public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        //public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        //public virtual ICollection<ProductPriceListSkumap> ProductPriceListSkumaps { get; set; }
    }
}