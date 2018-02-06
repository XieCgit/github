using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InternetBarDAL
{
   public class Computer_Info
    {
        static public MySqlDataReader select_roompos()
        {
            string sql = "select * from room_info";
            MySqlDataReader result = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, null);
            return result;
        }
        //改变电脑状态
        static public int state_change(string computer_id, string state)
        {
            string sql = "update computer_info set computer_state = @state where computer_id = @computer_id";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@state", state), new MySqlParameter("@computer_id", computer_id));
            return result;
        }

        static public MySqlDataReader select_computer(string room_id)
        {
            string sql = "select * from computer_info where computer_position = @room_id";
            MySqlDataReader result = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@room_id", room_id));
            return result;
        }
        //判断该电脑id是否存在于surf_internet_info  表中
        static public bool IsComputerid(string computer_id)
        {
            string sql = "select * from surf_internet_info where computer_id = @computer_id";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@computer_id", computer_id));
            if (obj == null)
                return false;
            return true;
        }
        //判断该user_id是否存在于surf_internet_info  表中
        static public bool Isuserid(string user_id)
        {
            string sql = "select * from surf_internet_info where user_id = @user_id";
            object obj = MySqlHelper.ExecuteScalar(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id));
            if (obj == null)
                return false;
            return true;
        }
        //添加信息到surf_internet_info 
        static public int addComputer_info(string computer_id, string user_id, string datetime)
        {
            string sql = "insert into surf_internet_info(computer_id, user_id, start_time) values (@computer_id, @user_id, @datetime)";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@computer_id", computer_id), new MySqlParameter("@user_id", user_id), new MySqlParameter("@datetime", datetime));
            return result;
        }
        //更新信息surf_internet_info 
        static public int changeComputer_info(string computer_id, string user_id, string datetime)
        {
            string sql = "update surf_internet_info set user_id = @user_id, start_time = @datetime where computer_id = @computer_id";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id), new MySqlParameter("@datetime", datetime), new MySqlParameter("@computer_id", computer_id));
            return result;
        }
        //查看user_id 的上网开始时间
        static public MySqlDataReader start_time(string user_id)
        {
            string sql = "select start_time from surf_internet_info where user_id = @user_id";
            MySqlDataReader reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id));
            return reader;
        }
        //删除user_id的上网信息
        static public int delinfo(string user_id)
        {
            string sql = "delete from surf_internet_info where user_id = @user_id";
            int result = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id));
            return result;
        }
        //通过user_id查看上网电脑
        static public MySqlDataReader get_computer_id(string user_id)
        {
            string sql = "select computer_id from surf_internet_info where user_id = @user_id";
            MySqlDataReader reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, sql, new MySqlParameter("@user_id", user_id));
            return reader;
        }
    }
}
