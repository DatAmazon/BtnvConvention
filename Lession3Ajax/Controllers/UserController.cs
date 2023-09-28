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
            return View(UserService.GetAllUser());
        }
        public ActionResult ListUserPatialView()
        {

            return PartialView(UserService.GetAllUser());
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string accName, string pass, string userName, string email)
        {
            UserService.AddUser(accName, pass, userName, email);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(UserService.GetUserById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteUser(int id)
        {
            UserService.DeleteUser(id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(UserService.GetUserById(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string accName, string pass, string userName, string email)
        {
            UserService.EditUser(id, accName, pass, userName, email);
            return RedirectToAction("Index");
        }
    }
}