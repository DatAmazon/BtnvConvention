using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DB.DbAccess
{
    public static class SqlDbHelper
    {
        static string connectionString = SetConst.connectionString;
        public static SqlConnection Connection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            return conn;
        }

        public static void ExcuteNonQueryProc(string procedure, Dictionary<string, object> param)
        {
            SqlCommand cmd = null;
            using (cmd = Connection().CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedure;
                foreach (KeyValuePair<string, object> item in param)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                cmd.ExecuteNonQuery();
            }

        }
        public static DataTable ExcuteReaderProc(string procedure, Dictionary<string, object> param)
        {
            SqlCommand cmd = Connection().CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            foreach (KeyValuePair<string, object> item in param)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }
            var dataTable = new DataTable();
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                dataTable.Load(rd);
            }
            return dataTable;
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
//-------------------------Product----------------------
        //public static List<Product> GetProducts()
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    return ConvertDataTable<Product>(ExcuteReaderProc(SetConst.GetListProduct, dict));
        //}

        //public static Product GetProductById(int id)
        //{
        //    Product pro = null;
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcGetProByID";
        //            cmd.Parameters.AddWithValue("@productId", id);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    while (rd.Read())
        //                    {
        //                        pro = new Product
        //                        {
        //                            ProductID = Convert.ToInt32(rd["ProductID"]),
        //                            Name = rd["Name"] != DBNull.Value ? rd["Name"].ToString() : string.Empty,
        //                            Price = Convert.ToDouble(rd["Price"]),
        //                            Quantity = Convert.ToInt32(rd["Quantity"])
        //                        };
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return pro;
        //}

        //public static void CreateProduct(string name, int quantity, float price)
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    dict.Add("@proName", name);
        //    dict.Add("@quatity", quantity);
        //    dict.Add("@price", price);
        //    ExcuteNonQueryProc("ProcCreateProduct", dict);
        //}

        //public static void EditProduct(int id, string name, int quantity, float price)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcEditProById";
        //            cmd.Parameters.AddWithValue("@productId", id);
        //            cmd.Parameters.AddWithValue("@proName", name);
        //            cmd.Parameters.AddWithValue("@price", price);
        //            cmd.Parameters.AddWithValue("@quatity", quantity);
        //            try
        //            {
        //                cnn.Open();
        //                int check = cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //}

        //public static void DeleteProduct(int id)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcDeleteById";
        //            cmd.Parameters.AddWithValue("@productId", id);
        //            try
        //            {
        //                cnn.Open();
        //                int check = cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //}
        //public static List<Product> SearchProByName(string search)
        //{
        //    List<Product> products = new List<Product>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "searchProByName";
        //            cmd.Parameters.AddWithValue("@proName", search);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    if (rd.HasRows)
        //                    {
        //                        while (rd.Read())
        //                        {
        //                            products.Add(new Product
        //                            {
        //                                ProductID = Convert.ToInt32(rd["ProductID"].ToString()),
        //                                Name = rd["Name"] != DBNull.Value ? rd["Name"].ToString() : string.Empty,
        //                                Price = Convert.ToDouble(rd["Price"]),
        //                                Quantity = Convert.ToInt32(rd["Quantity"].ToString())
        //                            });
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //    return products;
        //}

        //-------------------------Order----------------------

        //public static List<Order> GetOrders()
        //{
        //    var list = new List<Order>();

        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcGetListOrder";
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    while (rd.Read())
        //                    {
        //                        list.Add(new Order
        //                        {
        //                            OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
        //                            CustomerName = rd["CustomerName"] != DBNull.Value ? rd["CustomerName"].ToString() : string.Empty,
        //                            DateTime = (DateTime)rd["DateTime"]
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return list;
        //}

        //public static Order GetOrderById(int id)
        //{
        //    Order Or = null;
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcGetOrderByID";
        //            cmd.Parameters.AddWithValue("@OrderId", id);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    if (rd.HasRows)
        //                    {
        //                        while (rd.Read())
        //                        {
        //                            Or = new Order
        //                            {
        //                                OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
        //                                CustomerName = rd["CustomerName"] != DBNull.Value ? rd["CustomerName"].ToString() : string.Empty,
        //                                DateTime = (DateTime)rd["DateTime"]
        //                            };
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return Or;
        //}

        //public static void DeleteOrder(int id)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcDeleteOrderById";
        //            cmd.Parameters.AddWithValue("@orderId", id);
        //            try
        //            {
        //                cnn.Open();
        //                int check = cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //}

        //public static void CreateOrder(string customerName, DateTime dt)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcCreateOrder";
        //            cmd.Parameters.AddWithValue("@customer", customerName);
        //            cmd.Parameters.AddWithValue("@datetime", dt);
        //            try
        //            {
        //                cnn.Open();
        //                int check = cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }

        //        }
        //    }
        //}


        //public static List<Order> searchOrderByID(string orderID)
        //{
        //    List<Order> lstOr = new List<Order>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "searchOrderById";
        //            cmd.Parameters.AddWithValue("@orderId", orderID);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        lstOr.Add(new Order
        //                        {
        //                            OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
        //                            CustomerName = rdr["customerName"] != DBNull.Value ? rdr["customerName"].ToString() : string.Empty,
        //                            DateTime = (DateTime)rdr["DateTime"]
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //    return lstOr;
        //}
        //public static List<Order> searchOrderByCustomerName(string cus)
        //{
        //    List<Order> lstOr = new List<Order>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcGetOrderByCustomerName";
        //            cmd.Parameters.AddWithValue("@customerName", cus);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        lstOr.Add(new Order
        //                        {
        //                            OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
        //                            CustomerName = rdr["customerName"] != DBNull.Value ? rdr["customerName"].ToString() : string.Empty,
        //                            DateTime = (DateTime)rdr["DateTime"]
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //    return lstOr;
        //}
        //public static List<Order> searchOrderByTime(DateTime start, DateTime end)
        //{
        //    List<Order> lstOrder = new List<Order>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcSearchOrderByTime";
        //            cmd.Parameters.AddWithValue("@timeStart", start);
        //            cmd.Parameters.AddWithValue("@timeEnd", end);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    while (rd.Read())
        //                    {
        //                        lstOrder.Add(new Order
        //                        {
        //                            OrderId = Convert.ToInt32(rd["OrderId"]),
        //                            CustomerName = rd["CustomerName"] != DBNull.Value ? rd["CustomerName"].ToString() : string.Empty,
        //                            DateTime = (DateTime)rd["DateTime"]
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return lstOrder;
        //}

        //public static List<Order_Detail> DetailAllProductInOrderId(int orderID)
        //{
        //    List<Order_Detail> lstOr = new List<Order_Detail>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "DetailAllProductInOrderId";
        //            cmd.Parameters.AddWithValue("@orderId", orderID);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        lstOr.Add(new Order_Detail
        //                        {
        //                            OrderDetailId = Convert.ToInt32(rdr["OrderDetailId"].ToString()),
        //                            OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
        //                            ProductId = Convert.ToInt32(rdr["ProductId"].ToString()),
        //                            Quantity = Convert.ToInt32(rdr["Quantity"].ToString()),
        //                            UnitPrice = Convert.ToInt32(rdr["UnitPrice"].ToString()),
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //    return lstOr;
        //}

        //-------------------------OrderDeatil----------------------

        //public static List<Order_Detail> GetOrderDetails()
        //{
        //    var list = new List<Order_Detail>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcGetListOrder_Details";
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    while (rd.Read())
        //                    {
        //                        list.Add(new Order_Detail
        //                        {
        //                            OrderDetailId = Convert.ToInt32(rd["OrderDetailId"].ToString()),
        //                            OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
        //                            ProductId = Convert.ToInt32(rd["ProductId"].ToString()),
        //                            Quantity = Convert.ToInt32(rd["Quantity"].ToString()),
        //                            UnitPrice = Convert.ToInt32(rd["UnitPrice"].ToString())
        //                        }); ;
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return list;
        //}

        //public static Order_Detail GetOrderDetailById(int id)
        //{
        //    Order_Detail Od = null;
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcGetOrder_DetailByID";
        //            cmd.Parameters.AddWithValue("@OrderDetailId", id);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    if (rd.HasRows)
        //                    {
        //                        while (rd.Read())
        //                        {
        //                            Od = new Order_Detail
        //                            {
        //                                OrderDetailId = Convert.ToInt32(rd["OrderDetailId"].ToString()),
        //                                OrderId = Convert.ToInt32(rd["OrderId"].ToString()),
        //                                ProductId = Convert.ToInt32(rd["ProductId"].ToString()),
        //                                Quantity = Convert.ToInt32(rd["Quantity"].ToString()),
        //                                UnitPrice = Convert.ToInt32(rd["UnitPrice"].ToString())
        //                            };
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return Od;
        //}

        //public static void CreateOrderDetail(int orId, int proId, int quan, int price)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ProcCreateOrderDetail";
        //            cmd.Parameters.AddWithValue("@orderId", orId);
        //            cmd.Parameters.AddWithValue("@productId", proId);
        //            cmd.Parameters.AddWithValue("@quantity", quan);
        //            cmd.Parameters.AddWithValue("@unitPrice", price);
        //            try
        //            {
        //                cnn.Open();
        //                int check = cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }

        //        }
        //    }
        //}

        //public static List<Order_Detail> searchOrderDetailByID(int? orderID)
        //{
        //    List<Order_Detail> lstOr = new List<Order_Detail>();
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "searchOrder_DetailById";
        //            cmd.Parameters.AddWithValue("@orderDetailId", orderID);
        //            try
        //            {
        //                cnn.Open();
        //                using (SqlDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        lstOr.Add(new Order_Detail
        //                        {
        //                            OrderDetailId = Convert.ToInt32(rdr["OrderDetailId"].ToString()),
        //                            OrderId = Convert.ToInt32(rdr["OrderId"].ToString()),
        //                            ProductId = Convert.ToInt32(rdr["ProductId"].ToString()),
        //                            Quantity = Convert.ToInt32(rdr["Quantity"].ToString()),
        //                            UnitPrice = Convert.ToInt32(rdr["UnitPrice"].ToString())
        //                        });
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //    return lstOr;
        //}
    }

}

