using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InternetBarDAL
{
    public class User_Info
    {
        private string _UserID;
        private string _UserName;
        private string _UserAge;
        private string _UserGender;
        private string _UserTele;

        #region
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string UserAge
        {
            get { return _UserAge; }
            set { _UserAge = value; }
        }

        public string UserGender
        {
            get { return _UserGender; }
            set { _UserGender = value; }
        }
        
        public string UserTele
        {
            get { return _UserTele; }
            set { _UserTele = value; }
        }
        #endregion

        //用户注册
        static public int RegisterUser(User_Info user_info)
        {
            string sql = "insert into user_info(user_id, user_name, user_age, user_gender, user_tele) values (@userID, @userName, @userAge, @userGender, @userTele)";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@userID", user_info._UserID), new MySqlParameter("@userName", user_info._UserName), new MySqlParameter("@userAge", user_info._UserAge), new MySqlParameter("@userGender", user_info._UserGender), new MySqlParameter("@userTele", user_info._UserTele));

            return result;
        }

        //查看个人信息
        static public MySqlDataReader QueryUser_Info(string user_id)
        {
            string sql = "select * from user_info where user_id = @user_id";
            MySqlDataReader reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id));
            return reader;
        }

        
        //判断是否存在该用户
        static public bool IsUserExist(string userID)
        {
            string sql = "select * from user_info where user_id = @userid";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql,new MySqlParameter("@userid", userID));
            if (obj == null)
                return false;

            return true;
        }
        //判断是否存在改脸部信息
        static public bool IsFaceExist(string face_id)
        {
            string sql = "select * from face_info where face_id = @face_id";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@face_id", face_id));
            if (obj == null)
                return false;

            return true;
        }
        //通过face_id得到user_info中的user_name
        static public MySqlDataReader name_by_face(string face_id)
        {
            string sql = "select user_name from user_info where user_id = (select user_id from face_info where face_id = @face_id)";
            MySqlDataReader result = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@face_id",face_id));
            return result;
        }
        static public MySqlDataReader id_by_face(string face_id)
        {
            string sql = "select user_id from face_info where face_id = @face_id";
            MySqlDataReader result = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@face_id", face_id));
            return result;
        }
        //修改用户绑定手机号码
        static public int ModifyTele(string user_id, string userTele)
        {
            string sql = "update user_info set user_tele = @userTele where user_id = @userID";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@userTele", userTele), new MySqlParameter("@userID", user_id));
            return result;
        }
        //查找用户是否进行第一次会员注册
        static public bool IsUserin_member(string userID)
        {
            string sql = "select * from member_info where user_id = @userid";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@userid", userID));
            if (obj == null)
                return false;

            return true;
        }

        //首次账户充值recharge
        static public int FirstRecharge(string user_id, double balance)
        {
            string sql = "insert into member_info(user_id, balance) values (@user_id, @balance)";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text,sql, new MySqlParameter("@user_id", user_id), new MySqlParameter("@balance", balance));
            return result;
        }
        //普通账户充值  && 更新账户余额
        static public int Recharge(string user_id, double balance)
        {
            string sql = "update member_info set balance = @balance where user_id = @user_id";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@balance", balance), new MySqlParameter("@user_id", user_id));
            return result;
        }
        //查看账户余额
        static public MySqlDataReader balance(string user_id)
        {
            string sql = "select balance from member_info where user_id = @user_id";
            MySqlDataReader result = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id));
            return result;
        }
    }
}
