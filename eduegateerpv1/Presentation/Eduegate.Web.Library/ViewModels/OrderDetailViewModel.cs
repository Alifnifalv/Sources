using System;
using System.Collections.Generic;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class OrderDetailViewModel
    {
        public long TransactionIID { get; set; }
        public long DetailIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public string UnitPrice { get; set; }
        public string Amount { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductUrl { get; set; }
        public List<KeyValuePair<byte, string>> Properties { get; set; }
        public List<KeyValuePair<long, string>> Categories { get; set; }
        public List<PropertyViewModel> PropertiesWithName { get; set; }
        public string SerialNumber { get; set; }

        public int? Action { get; set; }
        public Nullable<decimal> CancelQuantity { get; set; }
        public Nullable<decimal> ActualQuantity { get; set; }
        public bool IsEditMode { get; set; }
        public List<EditOrderDetailViewModel> EditOrderDetails { get; set; }

        public OrderDetailViewModel()
        {
            IsEditMode = false;
            EditOrderDetails = new List<EditOrderDetailViewModel>();
        }

        public static Services.Contracts.Catalog.OrderDetailDTO ToDTO(OrderDetailViewModel vm)
        {
            Mapper<OrderDetailViewModel, OrderDetailDTO>.CreateMap();
            return Mapper<OrderDetailViewModel, Services.Contracts.Catalog.OrderDetailDTO>.Map(vm);
        }

        public static OrderDetailViewModel ToVM(OrderDetailDTO dto)
        {
            Mapper<OrderDetailDTO, OrderDetailViewModel>.CreateMap();
            return Mapper<OrderDetailDTO, OrderDetailViewModel>.Map(dto);
        }

        public static Services.Contracts.Catalog.OrderDetailDTO ToDTOFromViewModel(OrderDetailViewModel vm)
        {
            var dto = new Services.Contracts.Catalog.OrderDetailDTO();
            //if (vm.IsNotNull())
            //{
            //    dto.TransactionIID = vm.TransactionIID;
            //    dto.ProductID = vm.ProductID;
            //    dto.ProductName = vm.ProductName;
            //    dto.SerialNumber = vm.SerialNumber;
            //    dto.ProductImageUrl = vm.ProductImageUrl;
            //    dto.ProductUrl = vm.ProductUrl;
            //    dto.ProductSKUMapID = vm.ProductSKUMapID;
            //    dto.Quantity = vm.ActualQuantity;
            //    dto.UnitID = vm.UnitID;
            //    dto.DiscountPercentage = vm.DiscountPercentage;
            //    dto.UnitPrice = decimal.Parse(vm.UnitPrice);
            //    dto.Amount = decimal.Parse(vm.Amount);
            //    dto.ExchangeRate = vm.ExchangeRate;
            //    dto.Properties = vm.Properties;
            //    dto.Categories = vm.Categories;
            //    dto.Action = vm.Action;
            //    dto.DetailIID = vm.DetailIID;

            //    //foreach (var item in vm.EditOrderDetails)
            //    //{
            //    //    dto.EditOrderDetails.Add(new EditOrderDetailDTO { Action = item.Action, Quantity = item.Quantity });
            //    //}
            //}
            return dto;
        }

    }
}