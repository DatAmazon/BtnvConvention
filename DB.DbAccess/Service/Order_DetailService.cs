using System.Collections.Generic;

namespace DB.DbAccess
{
    public class Order_DetailService : ICommon<Order_Detail>
    {
        public List<Order_Detail> GetAlls()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            return SqlDbHelper.ConvertDataTableToList<Order_Detail>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.GetOrderDetails, dict));
        }

        public Order_Detail GetById(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@OrderDetailId", id);
            return SqlDbHelper.ExcuteReaderAnObject<Order_Detail>(SetConst.GetOrderDetailById, dict);
        }

        public static void CreateOrderDetail(int orId, int proId, int quan, int price)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@orderId", orId);
            dict.Add("@productId", proId);
            dict.Add("@quantity", quan);
            dict.Add("@unitPrice", price);

            SqlDbHelper.ExcuteNonQueryProcedure(SetConst.CreateOrderDetail, dict);
        }

        public static List<Order_Detail> searchOrderDetailByID(int? orderID)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@orderDetailId", orderID);

            return SqlDbHelper.ConvertDataTableToList<Order_Detail>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.searchOrderDetailByID, dict));
        }
    }
}
