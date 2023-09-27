using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DB.DbAccess
{
    public static class SqlDbHelper
    {
        public static SqlConnection Connection()
        {
            SqlConnection conn = new SqlConnection(SetConst.connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            return conn;
        }

        public static void ExcuteNonQueryProcedure(string procedure, Dictionary<string, object> param)
        {
            using (var cmd = Connection().CreateCommand())
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
        public static DataTable ExcuteReaderProcedureToDisplayOnTable(string procedure, Dictionary<string, object> param)
        {

            var dataTable = new DataTable();
            using (var cmd = Connection().CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedure;
                foreach (KeyValuePair<string, object> item in param)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    dataTable.Load(rd);
                }
            }
            return dataTable;
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

        public static T ExcuteReaderAnObject<T>(string procedure, Dictionary<string, object> param)
        {
            SqlCommand cmd = Connection().CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            foreach (KeyValuePair<string, object> item in param)
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

}

