using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using News.business.Model;
using News.business.ViewModel;
using Ninject;
using News.business.Provider;
using PagedList;
using System.Linq;

namespace News.Controllers
{
    public class NewsController : AccountController
    {
        [AllowAnonymous]
        public async Task<ActionResult> Index(string sortOrder, int? page)
        {
            ApplicationUser UserData;
            string userId = null;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            if (User.Identity.IsAuthenticated)
            {
                UserData = await UserManager.FindByNameAsync(User.Identity.Name);
                userId = UserData.Id;
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "ByAuthor" ? "" : "ByAuthor";
            ViewBag.DateSortParm = sortOrder == "ByDate" ? "" : "ByDate";
            ViewBag.UserId = userId;

            bool adminRole = User.IsInRole("admin");
            bool editorRole = User.IsInRole("editor");
            bool journalistRole = User.IsInRole("journalist");
            
            var NewsModel = new NewsModel();            

            var ListOfNews = NewsModel.NewsOnScreen(adminRole, editorRole, journalistRole, userId);
            ListOfNews = NewsModel.SortNewsBy(sortOrder, ListOfNews);
            
            return View(ListOfNews.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Yeah()
        {
            return View();
        }
    }
}