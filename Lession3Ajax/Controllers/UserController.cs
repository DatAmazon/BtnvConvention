using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lession3
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.GetAll = UserService.GetAllUser();
            return View(ViewBag.GetAll);
        }

        public ActionResult GetUsers()
        {
            return View(UserService.GetAllUser());
        }

        public ActionResult ListUserPatialView()
        {
            var users = UserService.GetAllUser();
            return PartialView("ListUserPatialView", users);
        }
        public ActionResult Register()
        {
            ViewBag.GetAll = UserService.GetAllUser();
            return View();
        }

        [HttpPost]
        public ActionResult Register(string accName, string pass, string userName, string email)
        {
            UserService.AddUser(accName, pass, userName, email);
            return View();
        }
        public ActionResult Delete(int id)
        {
            var a = UserService.GetUserById(id);
            return View(a);
        }

        [HttpPost, ActionName("Delete")]
        public void DeleteUser(int id)
        {
            UserService.DeleteUser(id);
        }
        public ActionResult Edit(int id)
        {
            return View(UserService.GetUserById(id));
        }

        [HttpPost]
        public void Edit(int id, string accName, string pass, string userName, string email)
        {
            UserService.EditUser(id, accName, pass, userName, email);
        }
    }
}