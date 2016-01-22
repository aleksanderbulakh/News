using News.business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class DeleteNewsController : Controller
    {
        // GET: DeleteNews
        [HttpGet]
        [Authorize(Roles = "admin, editor, journalist")]
        public ActionResult DeleteNews(Guid id)
        {
            var NewsModel = new NewsModel();
            NewsModel.DeleteNews(id);
            return RedirectToRoute(new { controller = "News", action = "Yeah" } );
        }
    }
}