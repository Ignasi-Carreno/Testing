using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLogin.IBLL;

namespace WebLogin.Site.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserModel userModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userModel"></param>
        public UserController(IUserModel userModel)
        {
            this.userModel = userModel;
        }

        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user))
                {
                    if (user.UserName == "user1")
                    {
                        if (!Roles.IsUserInRole(user.UserName, "PAGE_1"))
                            Roles.AddUserToRole(user.UserName, "PAGE_1");
                    }
                    if (user.UserName == "user2")
                    {
                        if (!Roles.IsUserInRole(user.UserName, "PAGE_1"))
                            Roles.AddUserToRole(user.UserName, "PAGE_1");

                        if (!Roles.IsUserInRole(user.UserName, "PAGE_2"))
                            Roles.AddUserToRole(user.UserName, "PAGE_2");
                    }
                    if (user.UserName == "admin")
                    {
                        if (!Roles.IsUserInRole(user.UserName, "ADMIN"))
                            Roles.AddUserToRole(user.UserName, "ADMIN");
                    }

                    //var roles = userModel.GetUserRoles(user.UserName);
                    //roles

                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public bool IsValid(Models.User user)
        {
            return userModel.IsValidUser(AutoMapper.Mapper.Map<Models.User, Objects.User>(user));
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}