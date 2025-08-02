using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductSKUPriceCustomerGroupViewModel : BaseMasterViewModel
    {
        public ProductSKUPriceCustomerGroupViewModel()
        {
            QuantityPrice = new List<ProductSKUPriceCustomerGroupViewModel>();
        }

        public long ProductPriceListCustomerGroupMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string GroupName { get; set; }
        public Nullable<Decimal> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public string PriceListName { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public List<ProductSKUPriceCustomerGroupViewModel> QuantityPrice { get; set; }

        public static List<ProductSKUPriceCustomerGroupViewModel> ToViewModels(List<KeyValueViewModel> keyValueVms)
        {
            var vms = new List<ProductSKUPriceCustomerGroupViewModel>();

            foreach (var keyValue in keyValueVms)
            {
                vms.Add(new ProductSKUPriceCustomerGroupViewModel()
                {
                    CustomerGroupID = long.Parse(keyValue.Key),
                     GroupName = keyValue.Value
                });
            }

            return vms;
        }

        public static ProductSKUPriceCustomerGroupViewModel FromDTO(CustomerGroupPriceDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductSKUPriceCustomerGroupViewModel vm = new ProductSKUPriceCustomerGroupViewModel();

                vm.ProductPriceListCustomerGroupMapIID = dto.ProductPriceListCustomerGroupMapIID;
                vm.ProductPriceListID = dto.ProductPriceListID;
                vm.CustomerGroupID = dto.CustomerGroupID;
                vm.ProductID = dto.ProductID;
                vm.ProductSKUID = dto.ProductSKUMapID;
                vm.GroupName = dto.GroupName;
                vm.PriceListName = dto.PriceListName;
                vm.Price = dto.Price;
                vm.Quantity = dto.Quantity;
                vm.PricePercentage = dto.PricePercentage;
                vm.Discount = dto.Discount;
                vm.DiscountPercentage = dto.DiscountPercentage;
                vm.CreatedBy = dto.CreatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.UpdatedDate = dto.UpdatedDate;
                vm.TimeStamps = dto.TimeStamps;

                return vm;
            }
            else
            {
                return new ProductSKUPriceCustomerGroupViewModel();
            }
        }

        public static CustomerGroupPriceDTO ToDTO(ProductSKUPriceCustomerGroupViewModel vm)
        {
            if (vm.IsNotNull())
            {
                CustomerGroupPriceDTO dto = new CustomerGroupPriceDTO();

                dto.ProductPriceListCustomerGroupMapIID = vm.ProductPriceListCustomerGroupMapIID;
                dto.ProductPriceListID = vm.ProductPriceListID;
                dto.CustomerGroupID = vm.CustomerGroupID;
                dto.ProductID = vm.ProductID;
                dto.ProductSKUMapID = vm.ProductSKUID;
                dto.GroupName = vm.GroupName;
                dto.PriceListName = vm.PriceListName;
                dto.Price = vm.Price;
                dto.Quantity = vm.Quantity;
                dto.PricePercentage = vm.PricePercentage;
                dto.Discount = vm.Discount;
                dto.DiscountPercentage = vm.DiscountPercentage;
                dto.CreatedBy = vm.CreatedBy;
                dto.CreatedDate = vm.CreatedDate;
                dto.UpdatedBy = vm.UpdatedBy;
                dto.UpdatedDate = vm.UpdatedDate;
                dto.TimeStamps = vm.TimeStamps;

                return dto;
            }
            else
            {
                return new CustomerGroupPriceDTO();
            }
        }
    }
}
