using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.BoilerPlates.Interfaces
{
    public interface IWidgetDataSource
    {
        void SetContext(CallContext context);
        Task<BoilerPlateDataSourceDTO> GetData(BoilerPlateInfo boilerPlateInfo);
    }
}
