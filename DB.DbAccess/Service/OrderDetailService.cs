﻿using System.Collections.Generic;

namespace DB.DbAccess
{
    public class OrderDetailService : ICommon<OrderDetail>
    {
        public List<OrderDetail> GetAlls()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            return SqlDbHelper.ConvertDataTableToList<OrderDetail>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.GetOrderDetails, dict));
        }

        public OrderDetail GetById(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@OrderDetailId", id);
            return SqlDbHelper.ExcuteReaderAnObject<OrderDetail>(SetConst.GetOrderDetailById, dict);
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

        public static List<OrderDetail> searchOrderDetailByID(int? orderID)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@orderDetailId", orderID);

            return SqlDbHelper.ConvertDataTableToList<OrderDetail>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.searchOrderDetailByID, dict));
        }
    }
}
