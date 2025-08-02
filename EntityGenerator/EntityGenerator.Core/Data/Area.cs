using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Areas", Schema = "mutual")]
    public partial class Area
    {
        public Area()
        {
            AreaCultureDatas = new HashSet<AreaCultureData>();
            DeliveryTypeAllowedAreaMaps = new HashSet<DeliveryTypeAllowedAreaMap>();
            InverseParentArea = new HashSet<Area>();
            Location1 = new HashSet<Location1>();
            OrderContactMaps = new HashSet<OrderContactMap>();
        }

        [Key]
        public int AreaID { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        public short? ZoneID { get; set; }
        public int? CityID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? RouteID { get; set; }
        public int? CountryID { get; set; }
        public int? CompanyID { get; set; }
        public int? ParentAreaID { get; set; }
        [StringLength(50)]
        public string ExternalCode { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumCapAmount { get; set; }
        public long? BranchID { get; set; }

        [ForeignKey("ParentAreaID")]
        [InverseProperty("InverseParentArea")]
        public virtual Area ParentArea { get; set; }
        [InverseProperty("Area")]
        public virtual ICollection<AreaCultureData> AreaCultureDatas { get; set; }
        [InverseProperty("Area")]
        public virtual ICollection<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        [InverseProperty("ParentArea")]
        public virtual ICollection<Area> InverseParentArea { get; set; }
        [InverseProperty("Area")]
        public virtual ICollection<Location1> Location1 { get; set; }
        [InverseProperty("Area")]
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
