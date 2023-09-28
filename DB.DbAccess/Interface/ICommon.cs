using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbAccess
{
    public interface ICommon<T>
    {
        List<T> GetAlls();
        T GetById(int id);
    }

    //public interface IOrder
    //{
    //    List<Order> GetOrders();
    //    Order GetOrderById(int id);
    //    void DeleteOrder(int id);
    //    void CreateOrder(string customerName, DateTime dt);
    //    List<Order> SearchOrderByID(string orderID);
    //    List<Order> SearchOrderByCustomerName(string customerName);
    //    List<Order> SearchOrderByTime(DateTime start, DateTime end);
    //    List<Order_Detail> DetailAllProductInOrderId(int orderID);
    //}

    //public interface IProduct
    //{
    //    List<Product> GetProducts();
    //    Product GetProductById(int id);
    //    void CreateProduct(string name, int quantity, float price);
    //    void EditProduct(int id, string name, int quantity, float price);
    //    void DeleteProduct(int id);
    //    List<Product> SearchProByName(string search);
    //}
    //public interface IOrderDetails
    //{
    //    List<Order_Detail> GetOrderDetails();
    //    Order_Detail GetOrderDetailById(int id);
    //    void CreateOrderDetail(int orId, int proId, int quan, int price);
    //    List<Order_Detail> SearchOrderDetailById(int? orderID);
    //}
}
