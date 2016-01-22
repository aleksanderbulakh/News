using News.business.Model;
using News.business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class AddNewsController : AccountController
    {
        // GET: AddNews
        [HttpGet]
        [Authorize(Roles = "admin, journalist")]
        public ActionResult AddNew()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "admin, journalist")]
        public async System.Threading.Tasks.Task<ActionResult> AddNew(NewsViewModel NewAdd)
        {
            if (!ModelState.IsValid)
                return View(NewAdd);

            var UserData = await UserManager.FindByNameAsync(User.Identity.Name);

            var NewsModel = new NewsModel();

            NewAdd.AuthorId = UserData.Id;
            NewAdd.Author = UserData.UserName;

            NewsModel.AddNew(NewAdd);
            
            return RedirectToRoute(new { controller = "News", action = "Yeah" });
        }
    }
}