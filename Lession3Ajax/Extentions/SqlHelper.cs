using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Lession3
{
    public class SqlHelper
    {
        public static SqlConnection SqlConnection()
        {
            var conn = new SqlConnection(SetConst.GetConnectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            return conn;
        }

        public static void ExcuteNonQuery(string procedure, Dictionary<string, object> param)
        {
            using (var cmd = SqlConnection().CreateCommand())
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

        public static DataTable ExcuteReaderQuery(string procedure, Dictionary<string, object> param)
        {
            var dt = new DataTable();
            using (var cmd = SqlConnection().CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedure;

                foreach (KeyValuePair<string, object> item in param)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                using (SqlDataReader da = cmd.ExecuteReader())
                {
                    dt.Load(da);
                }
                return dt;
            }
        }

        public static T ExcuteReaderAnObject<T>(string procedure, Dictionary<string, object> param)
        {
            using (var cmd = SqlConnection().CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedure;

                foreach (var item in param)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        T result = Activator.CreateInstance<T>();
                        foreach (var prop in typeof(T).GetProperties())
                        {
                            if (!object.Equals(reader[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(result, reader[prop.Name], null);
                            }
                        }
                        return result;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
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


    }
}