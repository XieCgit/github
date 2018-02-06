using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace InternetBarBLL
{
    public class user
    {
        static public int RegisterUser(string userID, string userName, string userAge,string userGender, string userTele)
        {
            if (InternetBarDAL.User_Info.IsUserExist(userID))
            {
                MessageBox.Show("您已经注册过了，无需注册就可以登陆！");
                return 0;
            }
            InternetBarDAL.User_Info _user_info = new InternetBarDAL.User_Info();
            _user_info.UserID = userID;
            _user_info.UserName = userName;
            _user_info.UserAge = userAge;
            _user_info.UserGender = userGender;
            _user_info.UserTele = userTele;
            int result = InternetBarDAL.User_Info.RegisterUser(_user_info);

            return result;
        }
        static public string name_by_face(string face_id)
        {
            string name = null;
            MySqlDataReader user_name = InternetBarDAL.User_Info.name_by_face(face_id);
            if (user_name.HasRows)
            {
                user_name.Read();
                name = user_name.GetString(0);
                
            }
            return name;
        }
        static public string id_by_face(string face_id)
        {
            string id = null;
            MySqlDataReader user_id = InternetBarDAL.User_Info.id_by_face(face_id);
            if (user_id.HasRows)
            {
                user_id.Read();
                id = user_id.GetString(0);

            }
            user_id.Close();
            return id;
        }
        //账户充值
        static public int Recharge(string user_id, double money)
        {
            if(InternetBarDAL.User_Info.IsUserin_member(user_id))
            {
                double balance;
                MySqlDataReader reader = InternetBarDAL.User_Info.balance(user_id); 
                if(reader.HasRows)
                {
                    reader.Read();
                    balance = reader.GetDouble(0);
                    double b = balance + money;
                    return InternetBarDAL.User_Info.Recharge(user_id, b);
                }
                reader.Close();
            }
            return InternetBarDAL.User_Info.FirstRecharge(user_id, money);
        }
        //自助结算
        static public int CheckOut(string user_id)
        {
            DateTime datetime;
            if (InternetBarDAL.Computer_Info.Isuserid(user_id))
            {
                
                MySqlDataReader reader = InternetBarDAL.Computer_Info.start_time(user_id);
                if(reader.HasRows)
                {
                    reader.Read();
                    datetime = reader.GetDateTime(0);
                    DateTime currentdt = DateTime.Now.ToLocalTime();
                    TimeSpan time_interval = currentdt - datetime;
                    double hours = time_interval.Hours * 1.0 + time_interval.Minutes * 0.017;
                    double cost = hours * 5;
                    double ba ;
                    if (IsCost(user_id, cost, out ba))
                    {
                        MessageBox.Show("结算成功！本次上网时间为" + hours + "h, 共花费 ￥" + cost + "，账户余额为 ￥" + ba);
                        InternetBarDAL.User_Info.Recharge(user_id, ba);
                        InternetBarBLL.computer.balance_state(user_id);
                        InternetBarDAL.Computer_Info.delinfo(user_id);
                    }
                    else
                        MessageBox.Show("余额不足，此次上网网费为 ￥" + cost + "账户余额为 ￥"+ ba + "请先充值！");

                }
                reader.Close();
                return 1;
                
            }
            MessageBox.Show("你还未开机，无需结账！");
            return 0;
        }
        
        //判断余额是否够支付费用
        static public bool IsCost(string user_id, double money, out double ba)
        {
            double balance;
            ba = 0;
            MySqlDataReader reader1 = InternetBarDAL.User_Info.balance(user_id);
            if (reader1.HasRows)
            {
                reader1.Read();
                balance = reader1.GetDouble(0);
                ba = balance;
                double b = balance - money;
                if (b >= 0)
                {
                    ba = b;
                    return true;
                }
                return false;
            }
            reader1.Close();
            MessageBox.Show("数据库连接异常！");
            return false;
            
        }
    }
}
