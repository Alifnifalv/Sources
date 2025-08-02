using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Search;

namespace Eduegate.Web.Library.ViewModels.Common
{
    public class ViewColumnViewModel
    {
        public string ColumnName { get; set; }
        public string Header { get; set; }
        public string DataType { get; set; }
        public string Format { get; set; }
        public bool IsDefault { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSortable { get; set; }
        public bool IsQuickSearchable { get; set; }

        public static ColumnDTO ToDTO(ViewColumnViewModel vm)
        {
            Mapper<ViewColumnViewModel, ColumnDTO>.CreateMap();
            return Mapper<ViewColumnViewModel, ColumnDTO>.Map(vm);
        }

        public static ViewColumnViewModel ToViewModel(ColumnDTO dto)
        {
            Mapper<ColumnDTO, ViewColumnViewModel>.CreateMap();
            return Mapper<ColumnDTO, ViewColumnViewModel>.Map(dto);
        }
    }
}
