using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
   public class AssetViewModel : BaseMasterViewModel
    {
        public AssetViewModel()
        {
           DetailViewModel = new List<AssetDetailViewModel>();
           MasterViewModel = new AssetMasterViewModel();
        }

        public List<AssetDetailViewModel> DetailViewModel { get; set; }
        public AssetMasterViewModel MasterViewModel { get; set; }

        public List<AssetDTO> ToAssetDTO(List<AssetDetailViewModel> detailViewModelList)
        {
            List<AssetDTO> dtoList = new List<AssetDTO>();
            foreach(AssetDetailViewModel viewModelItem in  detailViewModelList)
            {
                AssetDTO dtoItem = new AssetDTO();
                dtoItem.AssetIID = viewModelItem.AssetIID;
                dtoItem.AccumulatedDepGLAccID = viewModelItem.AccumulatedDepGLAccount != null ? !string.IsNullOrEmpty(viewModelItem.AccumulatedDepGLAccount.Key) ? Convert.ToInt32(viewModelItem.AccumulatedDepGLAccount.Key) : (long?)null : (long?)null;  
                dtoItem.AssetCategoryID = viewModelItem.AssetCategory != null ? !string.IsNullOrEmpty(viewModelItem.AssetCategory.Key) ? Convert.ToInt32(viewModelItem.AssetCategory.Key) : (long?)null : (long?)null; 
                dtoItem.AssetCode = viewModelItem.AssetCode;
                dtoItem.AssetGlAccID = viewModelItem.AssetGlAccount != null ? !string.IsNullOrEmpty(viewModelItem.AssetGlAccount.Key) ? Convert.ToInt32(viewModelItem.AssetGlAccount.Key) : (long?)null : (long?)null;  
                dtoItem.DepreciationExpGLAccId = viewModelItem.DepreciationExpGLAccount != null ? !string.IsNullOrEmpty(viewModelItem.DepreciationExpGLAccount.Key) ? Convert.ToInt32(viewModelItem.DepreciationExpGLAccount.Key) : (long?)null : (long?)null;
                dtoItem.DepreciationYears =viewModelItem.DepreciationYears;
                dtoItem.Description = viewModelItem.Description;


                dtoList.Add(dtoItem);
            }
            return dtoList;
        }


        public AssetDetailViewModel ToViewModel(AssetDTO  dto)
        {
            Mapper<AssetDTO , AssetDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var assetDetailViewModel = Mapper<AssetDTO, AssetDetailViewModel>.Map(dto);
            return assetDetailViewModel;
        }
           
        }

    }

    