using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Services.Contracts.Metadata;

namespace Eduegate.Web.Library.ViewModels
{
    public class FilterColumnViewModel
    {
        public FilterColumnViewModel()
        {
            FilterConditions = new List<Services.Contracts.Enums.Conditions>() { new Services.Contracts.Enums.Conditions() };
        }
        public long FilterColumnID { get; set; }
        public int SequenceNo { get; set; }
        public string ColumnCaption { get; set; }
        public string ColumnName { get; set; }
        public Eduegate.Services.Contracts.Enums.DataTypes ColumnType { get; set; }
        public string DefaultValues { get; set; }
        public List<Eduegate.Services.Contracts.Enums.Conditions> FilterConditions { get; set; }
        public Eduegate.Services.Contracts.Enums.UIControlTypes FilterControlType { get; set; }

        public bool? IsQuickFilter { get; set; } 
        public int? LookUpID { get; set; }
        public bool? IsLookupLazyLoad { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }

        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public static List<FilterColumnViewModel> FromDTO(List<FilterColumnDTO> dtos)
        {
            var vm = new List<FilterColumnViewModel>();

            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    vm.Add(new FilterColumnViewModel()
                    {
                        ColumnCaption = dto.ColumnCaption,
                        ColumnName = dto.ColumnName,
                        ColumnType = dto.ColumnType,
                        DefaultValues = dto.DefaultValues,
                        FilterColumnID = dto.FilterColumnID,
                        FilterConditions = dto.FilterConditions,
                        FilterControlType = dto.FilterControlType,
                        SequenceNo = dto.SequenceNo,
                        LookUpID = dto.LookUpID,
                        IsLookupLazyLoad = dto.IsLookupLazyLoad,
                        IsQuickFilter = dto.IsQuickFilter,
                        Attribute1 = dto.Attribute1,
                        Attribute2 = dto.Attribute2,
                    });
                }
            }

            return vm;
        }
    }
}