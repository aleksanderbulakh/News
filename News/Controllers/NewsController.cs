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
        public async Task<ActionResult> Index(string sortOrder, int page = 1)
        {
            ApplicationUser UserData;
           
            string userId = null;
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
            
            var newsModel = new NewsModel();


            var listOfNews = newsModel.NewsOnScreen(adminRole, editorRole, journalistRole, userId);
            listOfNews = newsModel.SortNewsBy(sortOrder, listOfNews);



            int pageSize = 3;
            var dataList = new NewsListViewModel {
                SortOrder = sortOrder,
                NewsPerPages = listOfNews.Skip((page - 1) * pageSize).Take(pageSize),
                PageData = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = listOfNews.Count },
            };
            return View(dataList);
        }

        public ActionResult Yeah()
        {
            return View();
        }
    }
}