using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbAccess
{
    public class Order_DetailService
    {
        public static List<Order_Detail> GetOrderDetails()
        {
            var list = new List<Order_Detail>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcGetListOrder_Details";
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                list.Add(new Order_Detail
                                {
                                    OrderDetailId = Convert.ToInt32(rd["OrderDetailId"].ToString()),
                                    OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
                                    ProductId = Convert.ToInt32(rd["ProductId"].ToString()),
                                    Quantity = Convert.ToInt32(rd["Quantity"].ToString()),
                                    UnitPrice = Convert.ToInt32(rd["UnitPrice"].ToString())
                                }); ;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return list;
        }

        public static Order_Detail GetOrderDetailById(int id)
        {
            Order_Detail Od = null;
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcGetOrder_DetailByID";
                    cmd.Parameters.AddWithValue("@OrderDetailId", id);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    Od = new Order_Detail
                                    {
                                        OrderDetailId = Convert.ToInt32(rd["OrderDetailId"].ToString()),
                                        OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
                                        ProductId = Convert.ToInt32(rd["ProductId"].ToString()),
                                        Quantity = Convert.ToInt32(rd["Quantity"].ToString()),
                                        UnitPrice = Convert.ToInt32(rd["UnitPrice"].ToString())
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return Od;
        }

        public static void CreateOrderDetail(int orId, int proId, int quan, int price)
        {
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcCreateOrderDetail";
                    cmd.Parameters.AddWithValue("@orderId", orId);
                    cmd.Parameters.AddWithValue("@productId", proId);
                    cmd.Parameters.AddWithValue("@quantity", quan);
                    cmd.Parameters.AddWithValue("@unitPrice", price);
                    try
                    {
                        cnn.Open();
                        int check = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }

        public static List<Order_Detail> searchOrderDetailByID(int? orderID)
        {
            List<Order_Detail> lstOr = new List<Order_Detail>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "searchOrder_DetailById";
                    cmd.Parameters.AddWithValue("@orderDetailId", orderID);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                lstOr.Add(new Order_Detail
                                {
                                    OrderDetailId = Convert.ToInt32(rdr["OrderDetailId"].ToString()),
                                    OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
                                    ProductId = Convert.ToInt32(rdr["ProductId"].ToString()),
                                    Quantity = Convert.ToInt32(rdr["Quantity"].ToString()),
                                    UnitPrice = Convert.ToInt32(rdr["UnitPrice"].ToString())
                                });
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return lstOr;
        }
    }
}
