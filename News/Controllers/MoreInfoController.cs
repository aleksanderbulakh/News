using News.business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class MoreInfoController : Controller
    {
        // GET: MoreInfo
        [AllowAnonymous]
        public ActionResult MoreInfo(Guid id)
        {
            var newsModel = new NewsModel();
            var SelectedNew = newsModel.MoreInfo(id);
            if (SelectedNew.IsVisible == false)
                RedirectToRoute(new { controller = "News", action = "Index" });
            return View(SelectedNew);
        }
    }
}