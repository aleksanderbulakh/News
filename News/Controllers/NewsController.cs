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
        public async Task<ActionResult> Index(string sortOrder, int page = 1)
        {
            ApplicationUser userData;
           
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userData = await UserManager.FindByNameAsync(User.Identity.Name);
                userId = userData.Id;
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "ByAuthor" ? "" : "ByAuthor";
            ViewBag.DateSortParm = sortOrder == "ByDate" ? "" : "ByDate";
            
            var newsModel = new NewsModel();

            var listOfNews = newsModel.NewsOnScreen();
            listOfNews = newsModel.SortNewsBy(sortOrder, listOfNews);

            int countVisibleNews = 0;
            foreach (var newsData in listOfNews)
            {
                if (newsData.IsVisible || User.IsInRole("admin") || User.IsInRole("editor") || (newsData.AuthorId == userId && User.IsInRole("journalist")))
                    countVisibleNews++;
            }

            NewsListViewModel dataList;

            if (countVisibleNews > 3)
            {
                int pageSize = 3;
                dataList = new NewsListViewModel
                {

                    UserId = userId,
                    NewsPerPages = listOfNews.Skip((page - 1) * pageSize).Take(pageSize),
                    PageData = new PageInfo
                    {
                        PageNumber = page,
                        PageSize = pageSize,
                        TotalItems = listOfNews.Count
                    }
                };
            }
            else
            {
                dataList = new NewsListViewModel
                {
                    UserId = userId,
                    NewsPerPages = listOfNews,
                    PageData = new PageInfo { PageSize = 0 }
                };
            }

            return View(dataList);
        }

        public ActionResult Yeah()
        {
            return View();
        }
    }
}