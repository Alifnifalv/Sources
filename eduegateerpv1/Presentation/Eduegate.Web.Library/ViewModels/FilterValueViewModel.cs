using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Services.Contracts.Metadata;

namespace Eduegate.Web.Library.ViewModels
{
    public class FilterValueViewModel
    {
        public FilterValueViewModel()
        {
            FilterValue3 = new KeyValueViewModel();
        }

        public long FilterColumnID { get; set; }
        public string FilterCondition { get; set; }
        public string FilterValue { get; set; }
        public string FilterValue2 { get; set; }
        public KeyValueViewModel FilterValue3 { get; set; }
        public bool IsDirty { get; set; }
        public Eduegate.Infrastructure.Enums.SearchView View  {get;set;}

        public static List<FilterValueViewModel> FromModel(Eduegate.Infrastructure.Enums.SearchView view, List<FilterColumnViewModel> filterColumns, List<FilterUserValueDTO> userValues)
        {
            var model = new List<FilterValueViewModel>();

            if (filterColumns != null)
            {
                foreach (var col in filterColumns)
                {
                    var userFilter = userValues == null ? null : userValues.Where(x => x.FilterColumnID == col.FilterColumnID).FirstOrDefault();

                    model.Add(new FilterValueViewModel()
                    {
                        View = view,
                        FilterColumnID = col.FilterColumnID,
                        FilterCondition =  userFilter == null ?
                            (col.ColumnType.ToString().ToUpper().Equals("STRING") ? col.FilterConditions.ElementAtOrDefault(1).ToString() : col.FilterConditions[0].ToString()) : userFilter.Condition.ToString(),
                        IsDirty = userFilter == null ? false : true,
                        FilterValue = userFilter == null ? null : userFilter.Value1,
                        FilterValue2 = userFilter == null ? null : userFilter.Value2,
                        FilterValue3 = col.LookUpID.HasValue && userFilter != null ? new KeyValueViewModel() { Key = userFilter.Value1, Value = userFilter.Value1 } : null,
                    });
                }
            }

            return model;
        }
    }
}