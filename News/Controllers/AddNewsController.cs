using News.business.Model;
using News.business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class AddNewsController : Controller
    {
        // GET: AddNews
        [HttpGet]
        [Authorize(Roles = "admin, journalist")]
        public ActionResult AddNew()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin, journalist")]
        public ActionResult AddNew(NewsViewModel NewAdd)
        {
            if (!ModelState.IsValid)
                return View(NewAdd);

            string userName = User.Identity.Name;
            var newsModel = new NewsModel();
            newsModel.AddNew(userName, NewAdd);
            return RedirectToRoute(new { controller = "News", action = "Yeah" });
        }
    }
}