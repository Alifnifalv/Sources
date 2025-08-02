using Eduegate.Services.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Web.Library.School.Kanban;
using System.Data;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Application.Mvc;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
    [Area("Frameworks")]
    public class KanbanController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Viewer(KanbanTypes type)
        {
            return View(type);
        }

        [HttpGet]
        public ActionResult GetStudentsForShifting(int classID, int sectionID)
        {
            List<ClassSectionKanbanViewModel> classSectionKanbanViewModel = new List<ClassSectionKanbanViewModel>();
            var kanbanBoardList = new List<ClassSectionKanbanViewModel>();

            var classSectionShiftingDTO = ClientFactory.SchoolServiceClient(CallContext).GetStudentsForShifting(classID, sectionID);

            kanbanBoardList = (from w in classSectionShiftingDTO.ToShiftSectionIDs
                               select new ClassSectionKanbanViewModel()
                               {

                                   id = w.Key,
                                   title = w.Value,
                                   item = (from n in classSectionShiftingDTO.ClassSectionStudentDTO
                                           where n.SectionID.ToString() == w.Key
                                           select new ClassSectionBoardItemViewModel()
                                           {
                                               id = n.StudentID.ToString(),
                                               title = n.StudentName
                                           }).ToList()

                               }).ToList();



            return Json(kanbanBoardList);
        }

        //[HttpGet]
        //public ActionResult GetStudentsForShifting(int classID, int sectionID)
        //{
        //    var ResponseModel = new ResponseModel();
        //    List<ClassSectionKanbanViewModel> classSectionKanbanViewModel = new List<ClassSectionKanbanViewModel>();
        //    var kanbanBoardList = new List<ClassSectionKanbanViewModel>();
        //    using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEduegateERPContext"].ConnectionString))
        //    {


        //        var reader = conn.QueryMultiple("[schools].[GET_CLASS_SECTION_STUDENTS]",
        //                       param: new { ClassID = classID, SectionID = sectionID, SchoolID = CallContext.SchoolID, AcademicYearID = CallContext.AcademicYearID },
        //                     commandType: CommandType.StoredProcedure);

        //        var studentList = reader.Read<ClassSectionStudents>().ToList();
        //        var sectionList = reader.Read<ClassSections>().ToList();


        //        kanbanBoardList = (from w in sectionList
        //                           select new ClassSectionKanbanViewModel()
        //                           {

        //                               id = w.SectionID.ToString(),
        //                               title = w.SectionName,
        //                               item = (from n in studentList
        //                                       where n.SectionID == w.SectionID
        //                                       select new ClassSectionBoardItemViewModel()
        //                                       {
        //                                           id = n.StudentID.ToString(),
        //                                           title = n.StudentName
        //                                       }).ToList()

        //                           }).ToList();





        //    }

        //    return Json(kanbanBoardList);
        //}

        [HttpPost]
        public ActionResult ShiftStudentSection(long studentID, int sectionID)
        {
            string returnMsg = "0#Something went wrong!";
            
            returnMsg = ClientFactory.SchoolServiceClient(CallContext).ShiftStudentSection
                         (new ClassSectionShiftingDTO()
                         {
                             ToShiftSectionID = sectionID,
                             StudentID  = studentID
                         });

            string successMsg = "";
            dynamic response = new { IsFailed = false, Message = successMsg };
            if (!string.IsNullOrEmpty(returnMsg))
            {

                string[] resp = returnMsg.Split('#');
                response = new { IsFailed = (resp[0] == "F"), Message = (resp.Length > 1 ? resp[1] : successMsg) };
            }
            else
            {
                response = new { IsFailed = true, Message = "Something went wrong!" };
            }
            return Json(response);
        }

    }
}