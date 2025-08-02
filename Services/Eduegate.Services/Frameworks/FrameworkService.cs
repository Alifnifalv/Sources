using Eduegate.Domain.Frameworks;
using Eduegate.Framework.Services;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Framework;
using System.ServiceModel;

namespace Eduegate.Services.Frameworks
{
    public class FrameworkService : BaseService, IFrameworkService
    {
        public string GetScreenData(long screenID, long IID)
        {
            try
            {
                return new FrameworkBL(CallContext).GetScreenData(screenID, IID);
            }
            catch (Exception exception)
            {   
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ScreenMetadataDTO GetScreenMetadata(long screenID)
        {
            try
            {
                return new FrameworkBL(CallContext).GetScreenMetadata(screenID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ScreenDataDTO SaveScreenData(ScreenDataDTO data)
        {
            try
            {
                return new FrameworkBL(CallContext).SaveScreenData(data);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CRUDDataDTO SaveCRUDData(CRUDDataDTO data)
        {
            try
            {
                return new FrameworkBL(CallContext).SaveCRUDData(data);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public KeyValueDTO ValidateField(CRUDDataDTO data, string field)
        {
            try
            {
                return new FrameworkBL(CallContext).ValidateField(data, field);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool DeleteCRUDData(long screenID, long IID)
        {
            try
            {
                return new FrameworkBL(CallContext).DeleteCRUDData(screenID, IID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long CloneCRUDData(long screenID, long IID)
        {
            try
            {
                return new FrameworkBL(CallContext).CloneCRUDData(screenID, IID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FrameworkService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
