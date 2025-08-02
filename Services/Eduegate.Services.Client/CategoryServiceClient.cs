using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Service.Client
{
    public class CategoryServiceClient : BaseClient, ICategoryService
    {
        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _categoryService = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.CATEGORY_SERVICE_NAME);
        public CategoryServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public List<CategoryDTO> GetAllCategoriesHomePage()
        {
            var url = string.Format("{0}/{1}", _categoryService, "GetAllCategoriesHomePage");
            return ServiceHelper.HttpGetRequest<List<CategoryDTO>>(url, _callContext, _logger);
        }

        public List<CategoryDTO> GetAllCategoriesHomePageWithBrand()
        {
            var url = string.Format("{0}/{1}", _categoryService, "GetAllCategoriesHomePageWithBrand");
            return ServiceHelper.HttpGetRequest<List<CategoryDTO>>(url, _callContext, _logger);
        }

        public List<CategoryDTO> GetTopLevelCategories()
        {
            var url = string.Format("{0}/{1}", _categoryService, "GetTopLevelCategories");
            return ServiceHelper.HttpGetRequest<List<CategoryDTO>>(url, _callContext, _logger);
        }

        public List<CategoryDTO> GetSubCategoriesDTO(long categoryID)
        {
            var url = string.Format("{0}/{1}?categoryID={2}", _categoryService, "GetSubCategoriesDTO", categoryID);
            return ServiceHelper.HttpGetRequest<List<CategoryDTO>>(url, _callContext, _logger);
        }
        public List<CategoryDTO> GetCategoryBlocks(long categoryID)
        {
            var url = string.Format("{0}/{1}?categoryID={2}", _categoryService, "GetCategoryBlocks", categoryID);
            return ServiceHelper.HttpGetRequest<List<CategoryDTO>>(url, _callContext, _logger);
        }

        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            var url = string.Format("{0}/{1}?categoryCode={2}", _categoryService, "GetCategoryByCode", categoryCode);
            return ServiceHelper.HttpGetRequest<CategoryDTO>(url, _callContext, _logger);
        }

        public List<CategoryDTO> SearchCategories(string searchText)
        {
            var url = string.Format("{0}/{1}?searchText={2}", _categoryService, "SearchCategories", searchText);
            return ServiceHelper.HttpGetRequest<List<CategoryDTO>>(url, _callContext, _logger);
        }

        public List<KeyValueDTO> GetCategoryTags()
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(_categoryService + "GetCategoryTags", _callContext);
        }

        public CategoryDTO GetCategoryByID(long categoryID)
        {
            var url = string.Format("{0}/{1}?categoryID={2}", _categoryService, "GetCategoryByID", categoryID);
            return ServiceHelper.HttpGetRequest<CategoryDTO>(url, _callContext, _logger);
        }

        public List<CategorySettingDTO> GetCategorySettings(long categoryID)
        {
            var url = string.Format("{0}/{1}?categoryID={2}", _categoryService, "GetCategorySettings", categoryID);
            return ServiceHelper.HttpGetRequest<List<CategorySettingDTO>>(url, _callContext, _logger);
        }
    }
}
