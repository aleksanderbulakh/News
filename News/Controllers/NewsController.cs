using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using News.Models;

namespace News.Controllers
{
    public class NewsController : AccountController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<NewsOfListViewModel> News_View = new List<NewsOfListViewModel>();
            NewsOfListViewModel NewInList;
            List<New> All_News = New.Deserialize_All();
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
        public async Task<ActionResult> AddNew(New new_add)
        {
            if (!ModelState.IsValid)
                return View(new_add);

            ApplicationUser author = new ApplicationUser();
            author = await UserManager.FindByNameAsync(User.Identity.Name);
            new_add.Date = DateTime.Now;
            new_add.Author = author.UserName;
            new_add.Id = new Guid();
            new_add.Id = Guid.NewGuid();
            List<New> All_News = New.Deserialize_All();            
            //List<New> All_News = new List<New>();
            All_News.Add(new_add);
            New.Serialize_All(All_News);
            return RedirectToAction("Yeah");
        }

        public ActionResult Yeah()
        {
            return View();
        }

        public ActionResult MoreInfo (Guid Id)
        {
            New.Deserialize_All();
            New ThisNew = new New() ;
            List<New> All_News = New.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id==Id)
                {
                    ThisNew = new New(n);
                }
            }

            return View(ThisNew);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit (Guid Id)
        {            
            New ThisNew = new New();
            List<New> All_News = New.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id == Id)
                {
                    ThisNew = new New(n);
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
        public ActionResult Edit (New model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            New.Deserialize_All();
            List<New> All_News = New.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id == model.Id)
                {
                    n.Header = model.Header;
                    n.Content = model.Content;
                    n.IsView = model.IsView;
                }
            }

            New.Serialize_All(All_News);
            return RedirectToAction("Index");
        }
    }
}