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
            var NewsModel = new NewsModel();
            var SelectedNew = NewsModel.MoreInfo(id);
            if (!SelectedNew.IsVisible && User.IsInRole("journalist"))
                return RedirectToRoute(new { controller = "News", action = "Index" });
            return View(SelectedNew);
        }
    }
}