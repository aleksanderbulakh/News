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

        [HttpGet]
        [Authorize(Roles = "admin, journalist")]
        public ActionResult AddNew()
        {            
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin, journalist")]
        public ActionResult AddNew(NewsViewModel newAdd)
        {
            if (!ModelState.IsValid)
                return View(newAdd);

            string userName = User.Identity.Name;
            var newsModel = new NewsModel();
            newsModel.AddNew(userName, newAdd);
            return RedirectToAction("Yeah");
        }

        public ActionResult Yeah()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MoreInfo(Guid id)
        {
            var newsModel = new NewsModel();
            var selectedNew = newsModel.MoreInfo(id);
            return View(selectedNew);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(Guid id)
        {
            
            var newsModel = new NewsModel();
            var SelectedNew = newsModel.Edit(id);

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
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit (NewsViewModel editedData)
        {
            if (!ModelState.IsValid)
                return View(editedData);

            var newsModel = new NewsModel();
            newsModel.Edit(editedData);
            return RedirectToAction("Index");
        }



        [HttpGet]
        [Authorize(Roles = "admin, editor")]
        public ActionResult DeleteNews(Guid id)
        {
            var newsModel = new NewsModel();
            newsModel.DeleteNews(id);
            return RedirectToAction("Yeah");
        }
    }
}