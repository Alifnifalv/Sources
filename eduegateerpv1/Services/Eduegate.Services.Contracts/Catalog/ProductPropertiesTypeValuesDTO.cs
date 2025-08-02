using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    public class ProductPropertiesTypeValuesDTO
    {
        public PropertyTypeDTO PropertyType { get; set; }
        public List<PropertyDTO> SelectedProperties { get; set; }
    }
}
