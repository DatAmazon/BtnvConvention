using DB.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Lession2.Controllers
{
    public class OrdersController : Controller
    {

        public ActionResult Index(string search)
        {
            var list = OrderService.GetOrders();

            if (!String.IsNullOrEmpty(search))
            {
                list = OrderService.searchOrderByCustomerName(search);

            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(DateTime timeStart, DateTime timeEnd)
        {
            List<Order> lst = new List<Order>();
            lst = OrderService.GetOrders();
            if (timeStart != timeEnd)
            {
                lst = OrderService.searchOrderByTime(timeStart, timeEnd);
            }
            return View(lst);
        }
        public ActionResult Detail(int id)
        {
            List<Order_Detail> lstOd = new List<Order_Detail>();
            lstOd = OrderService.DetailAllProductInOrderId(id);
            return View(lstOd);
        }
        public ActionResult Edit(int id)
        {
            Order ord = new Order();
            ord = OrderService.GetOrderById(id);
            return View(ord);
        }

        public ActionResult Delete(int id)
        {
            Order ord = new Order();
            ord = OrderService.GetOrderById(id);
            return View(ord);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            OrderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string customerName, DateTime dt)
        {
            OrderService.CreateOrder(customerName, dt);
            return RedirectToAction("Index");
        }

    }
}