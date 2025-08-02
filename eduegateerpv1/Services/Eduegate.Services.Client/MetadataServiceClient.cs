using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Services;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Service.Client
{
    public class MetadataServiceClient : BaseClient, IMetadataService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.METADATA_SERVICE_NAME);

        public MetadataServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public List<Services.Contracts.Metadata.FilterColumnDTO> GetFilterMetadata(Services.Contracts.Enums.SearchView view)
        {
            return ServiceHelper.HttpGetRequest<List<FilterColumnDTO>>
                (Service + "FilterMetadata?view=" + view.ToString(), _callContext);
        }

        public Services.Contracts.Metadata.UserFilterValueDTO SaveUserFilterMetadata(Services.Contracts.Metadata.UserFilterValueDTO value)
        {
            return ServiceHelper.HttpPostGetRequest<UserFilterValueDTO>(Service + "SaveUserFilterMetadata", value, _callContext);
        }

        public List<Services.Contracts.Metadata.FilterUserValueDTO> GetUserFilterMetadata(Services.Contracts.Enums.SearchView view)
        {
            return ServiceHelper.HttpGetRequest<List<FilterUserValueDTO>>(Service + "GetUserFilterMetadata?view=" + view.ToString(), _callContext);
        }

        public Services.Contracts.DocumentTypeDTO GetDocumentType(long documentTypeID, string type = null)
        {
            string uri;
            
            if(type != null) {
                uri = string.Format("{0}/GetDocumentType?documentTypeID={1}&type={2}", Service, documentTypeID, type.ToString());
            }
            else {
                uri = string.Format("{0}/GetDocumentType?documentTypeID={1}", Service, documentTypeID);
            }

            return ServiceHelper.HttpGetRequest<DocumentTypeDTO>(uri, _callContext, _logger);
        }

        public Services.Contracts.DocumentTypeDTO SaveDocumentType(Services.Contracts.DocumentTypeDTO dto)
        {
            var uri = string.Format("{0}/SaveDocumentType", Service);
            return ServiceHelper.HttpPostGetRequest<DocumentTypeDTO>(uri, dto, _callContext, _logger);
        }

        bool IMetadataService.SaveUserFilterMetadata(Services.Contracts.Metadata.UserFilterValueDTO value)
        {
            //TDODO save always should return the same updated data, not boolean
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Search.ColumnDTO> AvailableViewColumns(Services.Contracts.Enums.SearchView view)
        {
            var uri = string.Format("{0}/AvailableViewColumns?view={1}", Service, view);
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.Search.ColumnDTO>>(uri, _callContext, _logger);
        }


        public LocationDTO GetLocation(long locationID)
        {
            var uri = string.Format("{0}/GetLocation?locationID={1}", Service, locationID);
            return ServiceHelper.HttpGetRequest<LocationDTO>(uri, _callContext, _logger);
        }

        public LocationDTO SaveLocation(LocationDTO location)
        {
            var uri = string.Format("{0}/SaveLocation", Service);
            return ServiceHelper.HttpPostGetRequest<LocationDTO>(uri, location, _callContext, _logger);
        }
    }
}
