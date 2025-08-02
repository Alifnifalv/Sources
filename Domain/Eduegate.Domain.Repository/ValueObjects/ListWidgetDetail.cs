using System.Collections.Generic;

namespace Eduegate.Domain.Repository.ValueObjects
{
    public class ListWidgetDetail
    {
        public ListWidgetDetail()
        {
            Columns = new List<string>();
            Datas = new List<object[]>();
        }

        public List<string> Columns { get; set; }
        public List<object[]> Datas { get; set; }
    }
}