using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.Kanban
{
    public class ResponseModel
    {
       
        public string Message { set; get; }
        public object Data { set; get; }
        public bool isError { get; set; }
    }
    public class ClassSectionKanbanViewModel
    {
        public string id { get; set; }//SectionID
        public string title { get; set; }//SectionName
        public ClassSectionBoardItemViewModel item { get; set; }

    }
    public class ClassSectionBoardItemViewModel
{

    public string id { get; set; }//StudentID
    public string title { get; set; }//StudentName
    }
   
}


