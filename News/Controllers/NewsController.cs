using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using News.business.Model;
using News.business.ViewModel;
using Ninject;
using News.business.Provider;

namespace News.Controllers
{
    public class NewsController : AccountController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            bool adminRole = User.IsInRole("admin");
            bool editorRole = User.IsInRole("editor");
            bool journalistRole = User.IsInRole("journalist");
            string userName = User.Identity.Name;
            var newsModel = new NewsModel();
            var NewsList = newsModel.NewsOnScreen(adminRole, editorRole, journalistRole, userName);
            return View(NewsList);
        }

        public ActionResult Yeah()
        {
            return View();
        }

        public ActionResult Sorting(Guid id)
        {
            
            var newsModel = new NewsModel();

            return View();
        }
    }
}