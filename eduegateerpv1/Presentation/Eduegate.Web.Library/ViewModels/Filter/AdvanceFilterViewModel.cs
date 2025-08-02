using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Search;

namespace Eduegate.Web.Library.ViewModels.Filter
{
    public class AdvanceFilterViewModel
    {
        public FilterViewModel FilterViewModel { get; set; }
        public SearchListViewModel SearchMetadata { get; set; }

        public string JsonModel
        {
            get
            {
                var model = new List<FilterValueViewModel>();
                    foreach (var col in FilterViewModel.Columns)
                    {
                        var userFilter = FilterViewModel.UserValues.Where(x => x.FilterColumnID == col.FilterColumnID).FirstOrDefault();

                        model.Add(new FilterValueViewModel()
                        {
                            View = FilterViewModel.View,
                            FilterColumnID = col.FilterColumnID,
                            FilterCondition = userFilter == null ? 
                            Services.Contracts.Enums.Conditions.Contains.ToString() :
                                userFilter.Condition.ToString(),
                            IsDirty = userFilter == null ? false : true,
                            FilterValue = userFilter == null ? null : userFilter.Value1,
                            FilterValue2 = userFilter == null ? null : userFilter.Value2,
                            FilterValue3 = col.LookUpID.HasValue ? (userFilter != null ? new KeyValueViewModel() { Key = userFilter.Value1, Value = userFilter.Value1 } : null) : null,
                        });
                }

                return JsonConvert.SerializeObject(model);
            }
        }
    }
}
