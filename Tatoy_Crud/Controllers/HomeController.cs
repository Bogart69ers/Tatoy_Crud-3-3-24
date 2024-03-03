using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Tatoy_Crud.Controllers
{
    [Authorize(Roles = "User, Manager")]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
        
            List<User> userList = _userRepo.GetAll();
            return View(userList);
        }   
                    
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(User u)
        {
            _userRepo.Create(u);
            TempData["Msg"] = $"User {u.username} added!";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            return View(_userRepo.Get(id));
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            return View(_userRepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {
            _userRepo.Update(u.id, u);
            TempData["Msg"] = $"User {u.username} updated!";

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            _userRepo.Delete(id);
            TempData["Msg"] = $"User deleted!";

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User u)
        {
            var user = _userRepo.Table.Where(m => m.username == u.username).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Username not exist");
                return View();
            }
            if (!user.password.Equals(u.password))
            {
                ModelState.AddModelError("", "User not Exist or Incorrect Password");
                return View(u);
            }
            FormsAuthentication.SetAuthCookie(u.username, false);

            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}