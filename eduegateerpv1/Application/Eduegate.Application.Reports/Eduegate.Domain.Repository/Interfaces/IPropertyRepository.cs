using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Repository
{
    public interface IPropertyRepository
    {
        /// <summary>
        /// Get proprty detail by passing user propertyid
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        Property GetPropertyDetail(long propertyID);
    }
}