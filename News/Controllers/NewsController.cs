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
            List<New> list_news = New.Deserialize_All();
            return View(list_news);
        }

        [HttpGet]
        [Authorize(Roles ="Admin, editor")]
        public ActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin, editor")]
        public ActionResult AddNew(New new_add)
        {
            if (!ModelState.IsValid)
                return View(new_add);

            new_add.Date = DateTime.Now;
            new_add.Serialize_New();

            return RedirectToAction("Yeah");
        }

        public ActionResult Yeah()
        {
            return View();
        }
    }
}