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
            List<Product> productList = ProductService.GetProducts();

            if (!String.IsNullOrEmpty(search))
            {
                productList = ProductService.SearchProByName(search);
            }
            ViewBag.Products = productList;
            return View();
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
            Product pro = new Product();
            pro = ProductService.GetProductById(id);
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, string name, int quantity, float price)
        {
            ProductService.EditProduct(id, name, quantity, price);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Product pro = new Product();
            pro = ProductService.GetProductById(id);
            return View(pro);
        }
        public ActionResult Delete(int id)
        {
            Product pro = new Product();
            pro = ProductService.GetProductById(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Product pro = new Product();
            ProductService.DeleteProduct(id);
            return RedirectToAction("Index");
        }



    }
}
