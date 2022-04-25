using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Controllers
{
    public class NewMenuController : Controller
    {
        public IActionResult home()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult aboutsub1()
        {
            return View();
        }
        public IActionResult aboutsub2()
        {
            return View();
        }
        public IActionResult aboutsub3()
        {
            return View();
        }
        public IActionResult aboutsub4()
        {
            return View();
        }
    }
}
