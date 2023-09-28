using System.Collections.Generic;

namespace DB.DbAccess
{
    public class ProductService : ICommon<Product>
    {
        public List<Product> GetAlls()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            return SqlDbHelper.ConvertDataTableToList<Product>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.GetListProduct, dict));
        }

        public Product GetById(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@productId", id);
            return SqlDbHelper.ExcuteReaderAnObject<Product>(SetConst.GetProductById, dict);
        }

        public static void CreateProduct(string name, int quantity, float price)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@proName", name);
            dict.Add("@quatity", quantity);
            dict.Add("@price", price);
            SqlDbHelper.ExcuteNonQueryProcedure(SetConst.CreateProduct, dict);
        }

        public static void EditProduct(int id, string name, int quantity, float price)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@productId", id);
            dict.Add("@proName", name);
            dict.Add("@price", price);
            dict.Add("@quatity", quantity);
            SqlDbHelper.ExcuteNonQueryProcedure(SetConst.EditProductById, dict);
        }

        public static void DeleteProduct(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("productId", id);
            SqlDbHelper.ExcuteNonQueryProcedure( SetConst.DeleteProductById, dict);
        }

        public static List<Product> SearchProByName(string search)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@proName", search);
            return SqlDbHelper.ConvertDataTableToList<Product>(SqlDbHelper.ExcuteReaderProcedureToDisplayOnTable(SetConst.SearchProByName, dict));
        }
    }
}


