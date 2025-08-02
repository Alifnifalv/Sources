using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public partial class ProductInventorySKUConfigMapViewModel : BaseMasterViewModel
    {

        public ProductInventorySKUConfigMapViewModel()
        {
            ProductInventoryConfig = new ProductInventoryConfigViewModel();
        }

        public long ProductInventoryConfigID { get; set; }
        public long ProductSKUMapID { get; set; }
        //public Nullable<int> CreatedBy { get; set; }
        //public Nullable<int> UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public virtual ProductInventoryConfigViewModel ProductInventoryConfig { get; set; }
        //public virtual ProductSKUMap ProductSKUMap { get; set; }



        public static ProductInventorySKUConfigMapDTO ToDTO(ProductInventorySKUConfigMapViewModel vm)
        {
            Mapper<ProductInventoryConfigViewModel, ProductInventoryConfigDTO>.CreateMap();
            var mapper = Mapper<ProductInventorySKUConfigMapViewModel, ProductInventorySKUConfigMapDTO>.Map(vm);
            return mapper;
        }

        public static ProductInventorySKUConfigMapViewModel ToVM(ProductInventorySKUConfigMapDTO dto)
        {
            Mapper<ProductInventoryConfigDTO, ProductInventoryConfigViewModel>.CreateMap();
            var mapper = Mapper<ProductInventorySKUConfigMapDTO, ProductInventorySKUConfigMapViewModel>.Map(dto);
           
            return mapper;
        }

    }
}
