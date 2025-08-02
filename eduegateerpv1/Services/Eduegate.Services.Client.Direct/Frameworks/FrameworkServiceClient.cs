using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Direct.Frameworks
{
    public class FrameworkServiceClient :  IFrameworkService
    {
        FrameworkService service = new FrameworkService();

        public FrameworkServiceClient(CallContext callContext = null, Action<string> logger = null)            
        {
            service.CallContext = callContext;
        }

        public ScreenMetadataDTO GetScreenMetadata(long screenID)
        {
            return service.GetScreenMetadata(screenID);
        }

        public string GetScreenData(long screenID, long IID)
        {
            return service.GetScreenData(screenID, IID);
        }

        public ScreenDataDTO SaveScreenData(ScreenDataDTO data)
        {
            return service.SaveScreenData(data);
        }

        public CRUDDataDTO SaveCRUDData(CRUDDataDTO data)
        {
            return service.SaveCRUDData(data);
        }

        public KeyValueDTO ValidateField(CRUDDataDTO data, string field)
        {
            return service.ValidateField(data, field);
        }

        public bool DeleteCRUDData(long screenID, long IID)
        {
            return service.DeleteCRUDData(screenID, IID);
        }

        public long CloneCRUDData(long screenID, long IID)
        {
            return service.CloneCRUDData(screenID, IID);
        }
    }
}
