using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InternetBarDAL
{
    public class Admin_Info
    {
        private string _AdminID;
        private string _AdminPassward;

        #region
        public string AdminID
        {
            get { return _AdminID; }
            set { _AdminID = value; }
        }
        public string AdminPassward
        {
            get { return _AdminPassward; }
            set { _AdminPassward = value; }
        }
        #endregion
        //添加管理员
        static public int AddAdmin(Admin_Info admin_info)
        {
            string sql = "insert into admin_info(admin_id, admin_passward) values (@adminID, @adminPassward)";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@adminID", admin_info._AdminID), new MySqlParameter("@adminPassward", admin_info._AdminPassward));
            return result;
        }

        static public bool IsAdminExist(string adminID)
        {
            string sql = "select * from admin where admin_id = @adminID";
            object obj = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@adminID", adminID));

            if (obj == null)
                return false;
            return true;
        }
        //删除管理员信息
        static public int DeleteAdmin(Admin_Info admin_info)
        {
            string sql = "delete from admin_info where admin_id = @adminID";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@adminID", admin_info._AdminID));
            return result;
        }
        //查看所有管理员
        static public DataSet QueryAdmin()
        {
            string sql = "select * from admin_info";
            DataSet ds = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, sql, null);
            return ds;
        }
        //修改密码
        public int ModifyBook(string adminID, string adminPassward)
        {
            string sql = "update admin_info set admin_passward = @adminPassward where admin_id = @adminID";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text,sql, new MySqlParameter("@adminPassward", adminPassward), new MySqlParameter("@adminID", AdminID));
            return result;
        }
        //登录
        public static MySqlDataReader admin_r(string admin_id)
        {
            string sql = "select admin_passward from admin_info where admin_id = @admin_id";
            MySqlDataReader reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql ,new MySqlParameter("@admin_id", admin_id));
            return reader;
        }
        //判断是否有该管理员
        public static bool IsAdmin(string admin_id)
        {
            string sql = "select * from admin_info where admin_id = @admin_id";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@admin_id", admin_id));
            if (obj == null)
                return false;

            return true;
        }
        //添加房间
        public static int add_room(string room_id, string room_pos)
        {
            string sql = "insert into room_info(room_id, room_pos) values (@room_id, @room_pos)";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@room_id", room_id), new MySqlParameter("@room_pos", room_pos));
            return result;
        }
        //判断是否存在该房间编号
        public static bool IsRoomExist(string room_id)
        {
            string sql = "select * from room_info where room_id = @room_id";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@room_id", room_id));
            if (obj == null)
                return false;
            return true;
        }
        //判断是否存在该点电脑编号
        public static bool IsComputerExist(string computer_id)
        {
            string sql = "select * from computer_info where room_id = @computer_id";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@computer_id", computer_id));
            if (obj == null)
                return false;
            return true;
        }
        //添加电脑
        public static int add_computer(string computer_id, string computer_position)
        {
            string sql = "insert into room_info(computer_id, computer_position, computer_state) values (@computer_id, @computer_position, '0')";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@computer_id", computer_id), new MySqlParameter("@computer_position", computer_position));
            return result;
        }
    }
}
