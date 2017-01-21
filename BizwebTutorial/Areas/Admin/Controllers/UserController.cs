using Models.Dao;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BizwebTutorial.Areas.Admin.Controllers
{
    
    public class UserController : Controller
    {
        private UserDao _userservice = new UserDao();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModelUser model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email,model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email,false);
                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError("","Sai Tai Khoan hay Mat Khau");
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Dashboard");
        }
    }
}