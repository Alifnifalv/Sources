using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Repository
{
    public interface IPropertyRepository
    {
        Property GetPropertyDetail(long propertyID);
    }
}