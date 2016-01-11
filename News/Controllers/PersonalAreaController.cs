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
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
                return RedirectToAction("AdminHomeAction");
            if (User.IsInRole("editor") && !User.IsInRole("admin"))
                return RedirectToAction("EditorHomeAction");
            if (User.IsInRole("journalist") && !User.IsInRole("admin"))
                return RedirectToAction("JournalistHomeAction");
            return RedirectToAction("Index", "News");
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminHomeAction()
        {
            return View();
        }

        [Authorize(Roles = "editor")]
        public ActionResult EditorHomeAction()
        {
            return View();
        }

        [Authorize(Roles = "journalist")]
        public ActionResult JournalistHomeAction()
        {
            return View();
        }
    }
}