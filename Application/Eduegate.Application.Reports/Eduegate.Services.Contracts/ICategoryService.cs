using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;


namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICategoryService" in both code and config file together.
    [ServiceContract]
    public interface ICategoryService 
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllCategoriesHomePage")]
        List<CategoryDTO> GetAllCategoriesHomePage();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllCategoriesHomePageWithBrand")]
        List<CategoryDTO> GetAllCategoriesHomePageWithBrand();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTopLevelCategories")]
        List<CategoryDTO> GetTopLevelCategories();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSubCategoriesDTO?categoryID={categoryID}")]
        List<CategoryDTO> GetSubCategoriesDTO(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryBlocks?categoryID={categoryID}")]
        List<CategoryDTO> GetCategoryBlocks(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryByCode?categoryCode={categoryCode}")]
        CategoryDTO GetCategoryByCode(string categoryCode);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SearchCategories?searchText={searchText}")]
        List<CategoryDTO> SearchCategories(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryTags")]
        List<KeyValueDTO> GetCategoryTags();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryByID?categoryID={categoryID}")]
        CategoryDTO GetCategoryByID(long categoryID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategorySettings?categoryID={categoryID}")]
        List<CategorySettingDTO> GetCategorySettings(long categoryID);
    }
}
