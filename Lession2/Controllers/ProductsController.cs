using DB.DbAccess;
using System.Web.Mvc;

namespace Lession2.Controllers
{
    public class ProductsController : Controller
    {
        public readonly ICommon<Product> _productService;
        public ProductsController() { }
        public ProductsController(ICommon<Product> productService)
        {
            _productService = productService;
        }
        public ActionResult Index(string search)
        {
            var products = string.IsNullOrEmpty(search) ? _productService.GetAlls() : ProductService.SearchProByName(search);
            return View(products);
        }
   
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string name, int quantity, float price)
        {
            ProductService.CreateProduct(name, quantity, price);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(_productService.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(int id, string name, int quantity, float price)
        {
            ProductService.EditProduct(id, name, quantity, price);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(_productService.GetById(id));
        }
        public ActionResult Delete(int id)
        {
            return View(_productService.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            ProductService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
