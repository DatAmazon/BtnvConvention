using System;
using System.Collections.Generic;

namespace DB.DbAccess
{
    public class OrderService : ICommon<Order>
    {
        public List<Order> GetAlls()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            return SqlDbHelper.ConvertDataTableToList<Order>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.GetOrders, dict));
        }

        public Order GetById(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@OrderId", id);
            return SqlDbHelper.ExcuteReaderAnObject<Order>(SetConst.GetOrderById, dict);
        }

        public static void DeleteOrder(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@orderId", id);
            SqlDbHelper.ExcuteNonQueryProcedure(SetConst.DeleteOrder, dict);
        }

        public static void CreateOrder(string customerName, DateTime dt)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@customer", customerName);
            dict.Add("@datetime", dt);
            SqlDbHelper.ExcuteNonQueryProcedure(SetConst.CreateOrder, dict);
        }

        public static List<Order> searchOrderByID(string orderID)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@orderId", orderID);
            return SqlDbHelper.ConvertDataTableToList<Order>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.searchOrderByID, dict));
        }
        public static List<Order> searchOrderByCustomerName(string cus)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@customerName", cus);
            return SqlDbHelper.ConvertDataTableToList<Order>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.searchOrderByCustomerName, dict));
        }
        public static List<Order> searchOrderByTime(DateTime start, DateTime end)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@timeStart", start);
            dict.Add("@timeEnd", end);
            return SqlDbHelper.ConvertDataTableToList<Order>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.searchOrderByTime, dict));
        }

        public static List<OrderDetail> DetailAllProductInOrderId(int orderID)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@orderId", orderID);
            return SqlDbHelper.ConvertDataTableToList<OrderDetail>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.DetailAllProductInOrderId, dict));
        }
    }
}
