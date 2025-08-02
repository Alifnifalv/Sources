using Eduegate.Domain.Mappers.BoilerPlates.Interfaces;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using System;

namespace Eduegate.Domain.Mappers.BoilerPlates
{
    public class DataSourceFactory
    {
        public static IWidgetDataSource Create(string widget)
        {
            string templateClassName = $"Eduegate.Domain.Mappers.BoilerPlates.Data.{widget}";

            // Use reflection to dynamically create an instance of the template class
            Type templateType = Type.GetType(templateClassName);

            if (templateType == null)
            {
                return null;
            }

            return (IWidgetDataSource)Activator.CreateInstance(templateType);
        }
    }
}
