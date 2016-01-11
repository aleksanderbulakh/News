using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using News.Models;

namespace News.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            New.Deserialize_All();
            return View(New.All_News);
        }

        [HttpGet]
        [Authorize(Roles ="admin, editor")]
        public ActionResult AddNew()
        {
            New.Deserialize_All();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin, editor")]
        public ActionResult AddNew(New new_add)
        {
            if (!ModelState.IsValid)
                return View(new_add);

            new_add.Date = DateTime.Now;
            New.All_News.Add(new_add);
            New.Serialize_New();

            return RedirectToAction("Yeah");
        }

        public ActionResult Yeah()
        {
            return View();
        }
    }
}