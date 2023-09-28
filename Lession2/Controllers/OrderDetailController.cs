using DB.DbAccess;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lession2.Controllers
{
    public class OrderDetailController : Controller
    {
        public readonly ICommon<Order_Detail> _orderDetailService;
        public OrderDetailController() { }
        public OrderDetailController(ICommon<Order_Detail> orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        public ActionResult Index(int? search)
        {
            return View(search != null ? Order_DetailService.searchOrderDetailByID(search) : new Order_DetailService().GetAlls());
        }
        public ActionResult Create(int? id)
        {
            if (id != null) ViewBag.SelectedProductID = id;
            var od = new Order_Detail();
            List<Order> orders = new OrderService().GetAlls();
            List<Product> pros = new ProductService().GetAlls();
            ViewBag.Orders = new SelectList(orders, "OrderId", "OrderId", od.OrderId);
            ViewBag.Products = new SelectList(pros, "ProductID", "Name", od.ProductId);
            foreach (var item in pros)
            {
                if (item.ProductID == id) ViewBag.PricePro = item.Price;
            }
            return View(od);
        }

        [HttpPost]
        public ActionResult Create(int orderId, int productId, int quantity, int price)
        {
            Order_DetailService.CreateOrderDetail(orderId, productId, quantity, price);
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            return View(_orderDetailService.GetById(id));
        }
    }
}