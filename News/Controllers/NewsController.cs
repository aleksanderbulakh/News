using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using News.business.Model;
using News.business.ViewModel;
using Ninject;
using News.business.Provider;
using PagedList;

namespace News.Controllers
{
    public class NewsController : AccountController
    {
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            bool adminRole = User.IsInRole("admin");
            bool editorRole = User.IsInRole("editor");
            bool journalistRole = User.IsInRole("journalist");
            string userName = User.Identity.Name;
            var newsModel = new NewsModel();
            var NewsList = newsModel.NewsOnScreen(adminRole, editorRole, journalistRole, userName);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(NewsList.ToPagedList(pageNumber, pageSize));
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