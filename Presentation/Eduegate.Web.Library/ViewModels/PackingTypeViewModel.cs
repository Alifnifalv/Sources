
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;


namespace Eduegate.Web.Library.ViewModels
{
    public class PackingTypeViewModel : BaseMasterViewModel
    {
        public long PackingTypeID { get; set; }
        public string PackingTypeName { get; set; }


        public static PackingTypeViewModel ToViewModel(PackingTypeDTO dto)
        {
            if (dto != null)
            {
                return new PackingTypeViewModel()
                {
                    PackingTypeID = dto.PackingTypeID,
                    PackingTypeName = dto.PackingTypeName

                };
            }
            else return new PackingTypeViewModel();
        }

        public static PackingTypeDTO ToDto(PackingTypeViewModel vm)
        {
            if (vm != null)
            {
                return new PackingTypeDTO()
                {
                    PackingTypeID = vm.PackingTypeID,
                    PackingTypeName = vm.PackingTypeName
                };
            }
            else return new PackingTypeDTO();
        }
    }

}
