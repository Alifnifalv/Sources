using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class ProductInventoryViewModel
    {
        
        public long ProductSKUMapID { get; set; }
        
        public long TransactioDetailID { get; set; }
        
        public Nullable<decimal> Quantity { get; set; }
        
        public long? Batch { get; set; }
        
        public long? BranchID { get; set; }
        
        public long? ToBranchID { get; set; }
        
        public Nullable<int> CompanyID { get; set; }
        
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        
        public Nullable<decimal> CostPrice { get; set; }
        
        public Nullable<System.DateTime> CreatedDate { get; set; }
        
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        
        public Nullable<int> CreatedBy { get; set; }
        
        public Nullable<int> UpdatedBy { get; set; }
        
        


        public static List<ProductInventoryViewModel> FromDTOtoViewModel(List< ProductInventoryDTO> dtoList)
        {
            Mapper<ProductInventoryDTO, ProductInventoryViewModel>.CreateMap();
            return Mapper< List<ProductInventoryDTO>, List<ProductInventoryViewModel>>.Map(dtoList);
        }

        //public static List<TransactionHeadDTO> ToDTO(List<TransactionHeadViewModel> VMs)
        //{
        //    Mapper<TransactionHeadViewModel, TransactionHeadDTO>.CreateMap();
        //    Mapper<TransactionDetailViewModel, TransactionDetailDTO>.CreateMap();
        //    Mapper<TransactionAllocationViewModel, TransactionAllocationDTO>.CreateMap();
        //    return Mapper<List<TransactionHeadViewModel>, List<TransactionHeadDTO>>.Map(VMs);
        //}
    }
}
