using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class PersonalAreaController : Controller
    {
        // GET: PersonalArea
        public ActionResult Index()
        {
            return View();
        }
    }
}