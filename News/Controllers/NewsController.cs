﻿using System;
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
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder== "ByAuthor" ? "" : "ByAuthor";
            ViewBag.DateSortParm = sortOrder=="ByDate" ? "" : "ByDate";
            bool adminRole = User.IsInRole("admin");
            bool editorRole = User.IsInRole("editor");
            bool journalistRole = User.IsInRole("journalist");
            string userName = User.Identity.Name;
            var NewsModel = new NewsModel();            
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            List<NewsOfListViewModel> SortedListOfNews = NewsModel.NewsOnScreen(adminRole, editorRole, journalistRole, userName);

            switch (sortOrder)
            {
                case "ByAuthor":
                    SortedListOfNews = SortedListOfNews.OrderByDescending(m => m.Author).ToList();                    
                    break;
                case "ByDate":
                    SortedListOfNews = SortedListOfNews.OrderBy(m => m.Date).ToList();
                    break;               
            }
            return View(SortedListOfNews.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Yeah()
        {
            return View();
        }
    }
}