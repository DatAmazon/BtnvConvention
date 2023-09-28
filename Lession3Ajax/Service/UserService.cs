using System.Collections.Generic;

namespace Lession3
{
    public class UserService
    {
        public static List<User> GetAllUser()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            return SqlHelper.ConvertDataTableToList<User>(SqlHelper.ExcuteReaderQuery(SetConst.ProcGetAllUser, dict));
        }
        public static User GetUserById(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@id", id);
            return SqlHelper.ExcuteReaderAnObject<User>(SetConst.ProcGetUserById, dict);
        }
        public static void AddUser(string accName, string pass, string userName, string email)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@AccountName", accName);
            dict.Add("@Pass", pass);
            dict.Add("@UserName", userName);
            dict.Add("@Email", email);
            SqlHelper.ExcuteNonQuery(SetConst.InsertUser, dict);
        }

        public static void EditUser(int id, string accName, string pass, string userName, string email)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@id", id);
            dict.Add("@AccountName", accName);
            dict.Add("@Pass", pass);
            dict.Add("@UserName", userName);
            dict.Add("@Email", email);
            SqlHelper.ExcuteNonQuery(SetConst.EditUser, dict);
        }

        public static void DeleteUser(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@id", id);
            SqlHelper.ExcuteNonQuery(SetConst.ProcDeleteUser, dict);
        }

    }
}