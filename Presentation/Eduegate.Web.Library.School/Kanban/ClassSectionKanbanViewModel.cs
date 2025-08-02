using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Kanban
{
    public class ResponseModel
    {
       
        public string Message { set; get; }
        public object Data { set; get; }
        public bool isError { get; set; }
    }

    public class ClassSections
    {
        public int SectionID { get; set; }
        public string SectionName { get; set; }

    }
    public class ClassSectionStudents
    {
        public int ClassID { get; set; }
        public int SectionID { get; set; }
        public long StudentID { get; set; }
        public string StudentName { get; set; }

    }

    public class ClassSectionKanbanViewModel
    {
        public string id { get; set; }//SectionID
        public string title { get; set; }//SectionName
        public List<ClassSectionBoardItemViewModel> item { get; set; }

    }
    public class ClassSectionBoardItemViewModel
{

    public string id { get; set; }//StudentID
    public string title { get; set; }//StudentName
    }
   
}


