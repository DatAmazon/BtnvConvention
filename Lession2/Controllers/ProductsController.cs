using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DB.DbAccess;

namespace Lession2.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Index(string search)
        {
            return View(string.IsNullOrEmpty(search) ? ProductService.GetProducts() : ProductService.SearchProByName(search));
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
            return View(ProductService.GetProductById(id));
        }
        [HttpPost]
        public ActionResult Edit(int id, string name, int quantity, float price)
        {
            ProductService.EditProduct(id, name, quantity, price);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(ProductService.GetProductById(id));
        }
        public ActionResult Delete(int id)
        {
            return View(ProductService.GetProductById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            ProductService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
