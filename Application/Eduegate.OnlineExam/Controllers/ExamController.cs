using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.OnlineExam.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Exam()
        {
            return View();
        }
        public IActionResult Result()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult Questions()
        {
            return View();
        }
    }
}
