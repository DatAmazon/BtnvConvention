using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DbAccess
{
    public class OrderService
    {
        public static List<Order> GetOrders()
        {
            var list = new List<Order>();

            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcGetListOrder";
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                list.Add(new Order
                                {
                                    OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
                                    CustomerName = rd["CustomerName"] != DBNull.Value ? rd["CustomerName"].ToString() : string.Empty,
                                    DateTime = (DateTime)rd["DateTime"]
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
            return list;
        }

        public static Order GetOrderById(int id)
        {
            Order Or = null;
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcGetOrderByID";
                    cmd.Parameters.AddWithValue("@OrderId", id);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    Or = new Order
                                    {
                                        OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
                                        CustomerName = rd["CustomerName"] != DBNull.Value ? rd["CustomerName"].ToString() : string.Empty,
                                        DateTime = (DateTime)rd["DateTime"]
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
            return Or;
        }

        public static void DeleteOrder(int id)
        {
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcDeleteOrderById";
                    cmd.Parameters.AddWithValue("@orderId", id);
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

        public static void CreateOrder(string customerName, DateTime dt)
        {
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcCreateOrder";
                    cmd.Parameters.AddWithValue("@customer", customerName);
                    cmd.Parameters.AddWithValue("@datetime", dt);
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


        public static List<Order> searchOrderByID(string orderID)
        {
            List<Order> lstOr = new List<Order>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "searchOrderById";
                    cmd.Parameters.AddWithValue("@orderId", orderID);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                lstOr.Add(new Order
                                {
                                    OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
                                    CustomerName = rdr["customerName"] != DBNull.Value ? rdr["customerName"].ToString() : string.Empty,
                                    DateTime = (DateTime)rdr["DateTime"]
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
        public static List<Order> searchOrderByCustomerName(string cus)
        {
            List<Order> lstOr = new List<Order>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcGetOrderByCustomerName";
                    cmd.Parameters.AddWithValue("@customerName", cus);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                lstOr.Add(new Order
                                {
                                    OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
                                    CustomerName = rdr["customerName"] != DBNull.Value ? rdr["customerName"].ToString() : string.Empty,
                                    DateTime = (DateTime)rdr["DateTime"]
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
        public static List<Order> searchOrderByTime(DateTime start, DateTime end)
        {
            List<Order> lstOrder = new List<Order>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ProcSearchOrderByTime";
                    cmd.Parameters.AddWithValue("@timeStart", start);
                    cmd.Parameters.AddWithValue("@timeEnd", end);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                lstOrder.Add(new Order
                                {
                                    OrderId = Convert.ToInt32(rd["OrderId"]),
                                    CustomerName = rd["CustomerName"] != DBNull.Value ? rd["CustomerName"].ToString() : string.Empty,
                                    DateTime = (DateTime)rd["DateTime"]
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
            return lstOrder;
        }

        public static List<Order_Detail> DetailAllProductInOrderId(int orderID)
        {
            List<Order_Detail> lstOr = new List<Order_Detail>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DetailAllProductInOrderId";
                    cmd.Parameters.AddWithValue("@orderId", orderID);
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
                                    UnitPrice = Convert.ToInt32(rdr["UnitPrice"].ToString()),
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
