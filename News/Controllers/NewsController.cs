using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using News.business.Model;
using News.business.ViewModel;

namespace News.Controllers
{
    public class NewsController : AccountController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<NewsOfListViewModel> News_View = new List<NewsOfListViewModel>();
            NewsOfListViewModel NewInList;
            List<NewsViewModel> All_News = NewsModel.Deserialize_All();
            foreach(var n in All_News)
            {
                if (n.IsView)
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsView = n.IsView,
                        Id = n.Id
                    };
                    News_View.Add(NewInList);
                }
                else if (User.IsInRole("editor") || User.IsInRole("admin"))
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsView = n.IsView,
                        Id = n.Id
                    };

                    News_View.Add(NewInList);
                }
                else if(User.IsInRole("journalist") && User.Identity.Name == n.Author)
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsView = n.IsView,
                        Id = n.Id
                    };
                    News_View.Add(NewInList);
                }
            }
            return View(News_View);
        }

        [HttpGet]
        [Authorize(Roles = "admin, journalist")]
        public ActionResult AddNew()
        {            
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin, journalist")]
        public async Task<ActionResult> AddNew(NewsViewModel new_add)
        {
            if (!ModelState.IsValid)
                return View(new_add);

            ApplicationUser author = new ApplicationUser();
            author = await UserManager.FindByNameAsync(User.Identity.Name);
            new_add.Date = DateTime.Now;
            new_add.Author = author.UserName;
            new_add.Id = new Guid();
            new_add.Id = Guid.NewGuid();
            List<NewsViewModel> All_News = NewsModel.Deserialize_All();   
            All_News.Add(new_add);
            NewsModel.Serialize_All(All_News);
            return RedirectToAction("Yeah");
        }

        public ActionResult Yeah()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MoreInfo (Guid Id)
        {
            NewsModel.Deserialize_All();
            NewsViewModel ThisNew = new NewsViewModel() ;
            List<NewsViewModel> All_News = NewsModel.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id==Id)
                {
                    ThisNew = new NewsViewModel(n);
                }
            }

            return View(ThisNew);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit (Guid Id)
        {            
            NewsViewModel ThisNew = new NewsViewModel();
            List<NewsViewModel> All_News = NewsModel.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id == Id)
                {
                    ThisNew = new NewsViewModel(n);
                }
            }

            if (User.IsInRole("journalist"))
            {
                if (User.Identity.Name == ThisNew.Author)
                {
                    return View(ThisNew);
                }
            }
            else
            {
                return View(ThisNew);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit (NewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            NewsModel.Deserialize_All();
            List<NewsViewModel> All_News = NewsModel.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id == model.Id)
                {
                    n.Header = model.Header;
                    n.Content = model.Content;
                    n.IsView = model.IsView;
                }
            }

            NewsModel.Serialize_All(All_News);
            return RedirectToAction("Index");
        }


        
        [Authorize(Roles = "Admin, Journalist")]
        public ActionResult DeleteNews(Guid? id)
        {
            int i = 0;
            NewsModel.Deserialize_All();
            NewsViewModel ThisNew = new NewsViewModel();
            List<NewsViewModel> All_News = NewsModel.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id == id)
                {
                    //ThisNew = new New(n); 
                    All_News.RemoveAt(i);
                }
                i++;
            }
            NewsModel.Serialize_All(All_News);
            return RedirectToAction("Yeah");
        }
    }
}