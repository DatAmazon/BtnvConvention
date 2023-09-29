using DB.DbAccess;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lession2.Controllers
{
    public class OrderDetailController : Controller
    {
        public readonly ICommon<OrderDetail> _orderDetailService;
        public OrderDetailController() { }
        public OrderDetailController(ICommon<OrderDetail> orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        public ActionResult Index(int? search)
        {
            return View(search != null ? OrderDetailService.searchOrderDetailByID(search) : _orderDetailService.GetAlls());
        }
        public ActionResult Create(int? id)
        {
            if (id != null) ViewBag.SelectedProductID = id;
            var od = new OrderDetail();
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
            OrderDetailService.CreateOrderDetail(orderId, productId, quantity, price);
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            return View(_orderDetailService.GetById(id));
        }
    }
}