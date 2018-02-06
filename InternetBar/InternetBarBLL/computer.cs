using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace InternetBarBLL
{
    public class computer
    {
        static public MySqlDataReader select_roompos()
        { 
            return InternetBarDAL.Computer_Info.select_roompos();
        }
        static public MySqlDataReader select_computer(string room_id)
        {
            return InternetBarDAL.Computer_Info.select_computer(room_id);
        }
        //改变computer_info的电脑状态
        static public int state_change(string computer_id, string state)
        {
            return InternetBarDAL.Computer_Info.state_change(computer_id, state);
        }
        //surf_internet_info中的电脑信息
        static public bool use_computer(string computer_id, string user_id)
        {
            string datetime = DateTime.Now.ToLocalTime().ToString();
            if(InternetBarDAL.User_Info.IsUserin_member(user_id))
            {
                if (InternetBarDAL.Computer_Info.Isuserid(user_id))
                {
                    MessageBox.Show("你正在使用一台电脑，无法继续开机！");
                    return false;
                }
                string state = "1";
                state_change(computer_id,state);
                InternetBarDAL.Computer_Info.addComputer_info(computer_id, user_id, datetime);
                MessageBox.Show("你已开机，当前时间为" + datetime + "请合理安排上网时间！");
                return true;
            }
            MessageBox.Show("首次注册使用先进行账户充值！");
            return false;
        }
        //结算改变电脑状态
        static public void balance_state(string user_id)
        {
            MySqlDataReader reader = InternetBarDAL.Computer_Info.get_computer_id(user_id);
            if (reader.HasRows)
            {
                reader.Read();
                string cpid = reader.GetString(0);
                string state = "0";
                InternetBarBLL.computer.state_change(cpid, state);
            }
        }
    }
}
