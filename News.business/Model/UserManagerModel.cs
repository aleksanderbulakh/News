using System.Web;
using Microsoft.AspNet.Identity.Owin;
using News.business.Config;
using System.Web.Mvc;

namespace News.business.Model
{
    [Authorize]
    class UserManagerModel:Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            /*get
            {
                //return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }*/
            set
            {
                _userManager = value;
            }
        }
    }
}
