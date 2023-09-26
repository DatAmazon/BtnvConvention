using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.DbAccess;

namespace DB.DbAccess
{
    public class ProductService
    {
        public static List<Product> GetProducts()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            return SqlDbHelper.ConvertDataTable<Product>(SqlDbHelper.ExcuteReaderProc(SetConst.GetListProduct, dict));
        }

        public static Product GetProductById(int id)
        {
            Product pro = null;
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "ProcGetProByID";
                    cmd.Parameters.AddWithValue("@productId", id);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                pro = new Product
                                {
                                    ProductID = Convert.ToInt32(rd["ProductID"]),
                                    Name = rd["Name"] != DBNull.Value ? rd["Name"].ToString() : string.Empty,
                                    Price = Convert.ToDouble(rd["Price"]),
                                    Quantity = Convert.ToInt32(rd["Quantity"])
                                };
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return pro;
        }

        public static void CreateProduct(string name, int quantity, float price)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@proName", name);
            dict.Add("@quatity", quantity);
            dict.Add("@price", price);
            SqlDbHelper.ExcuteNonQueryProc("ProcCreateProduct", dict);
        }

        public static void EditProduct(int id, string name, int quantity, float price)
        {
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "ProcEditProById";
                    cmd.Parameters.AddWithValue("@productId", id);
                    cmd.Parameters.AddWithValue("@proName", name);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@quatity", quantity);
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

        public static void DeleteProduct(int id)
        {
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "ProcDeleteById";
                    cmd.Parameters.AddWithValue("@productId", id);
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

        public static List<Product> SearchProByName(string search)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection cnn = new SqlConnection(SetConst.connectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "searchProByName";
                    cmd.Parameters.AddWithValue("@proName", search);
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                    products.Add(new Product
                                    {
                                        ProductID = Convert.ToInt32(rd["ProductID"].ToString()),
                                        Name = rd["Name"] != DBNull.Value ? rd["Name"].ToString() : string.Empty,
                                        Price = Convert.ToDouble(rd["Price"]),
                                        Quantity = Convert.ToInt32(rd["Quantity"].ToString())
                                    });
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
            return products;
        }
    }
}
