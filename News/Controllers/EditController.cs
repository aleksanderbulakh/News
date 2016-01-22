using News.business.Model;
using News.business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class EditController : Controller
    {
        // GET: Edit
        [HttpGet]
        [Authorize]
        public ActionResult Edit(Guid id)
        {
            var NewsModel = new NewsModel();
            var SelectedNew = NewsModel.Edit(id);

            if (User.IsInRole("journalist"))
            {
                if (User.Identity.Name == SelectedNew.Author)
                {
                    return View(SelectedNew);
                }
            }
            else
            {
                return View(SelectedNew);
            }
            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(NewsViewModel EditedData)
        {
            if (!ModelState.IsValid)
                return View(EditedData);

            var NewsModel = new NewsModel();
            NewsModel.Edit(EditedData);
            return RedirectToAction("Index", "News");
        }
    }
}