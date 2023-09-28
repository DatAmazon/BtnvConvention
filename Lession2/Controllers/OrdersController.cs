using DB.DbAccess;
using System;
using System.Web.Mvc;

namespace Lession2.Controllers
{
    public class OrdersController : Controller
    {
        public readonly ICommon<Order> _orderService;
        public OrdersController() { }
        public OrdersController(ICommon<Order> orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index(string search)
        {
            return View(!String.IsNullOrEmpty(search) ? OrderService.searchOrderByCustomerName(search) : new OrderService().GetAlls());
        }
        [HttpPost]
        public ActionResult Index(DateTime timeStart, DateTime timeEnd)
        {
            return View(timeStart != timeEnd ? OrderService.searchOrderByTime(timeStart, timeEnd) : _orderService.GetAlls());
        }
        public ActionResult Detail(int id)
        {
            return View(OrderService.DetailAllProductInOrderId(id));
        }
        public ActionResult Edit(int id)
        {
            return View(_orderService.GetById(id));
        }

        public ActionResult Delete(int id)
        {
            return View(_orderService.GetById(id));
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