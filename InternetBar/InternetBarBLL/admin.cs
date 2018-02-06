using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace InternetBarBLL
{
    public class admin
    {
        public static string admin_r(string admin_id)
        {
            string admin_ps = null;
            if (InternetBarDAL.Admin_Info.IsAdmin(admin_id))
            {
                MySqlDataReader reader = InternetBarDAL.Admin_Info.admin_r(admin_id);
                if (reader.HasRows)
                {
                    reader.Read();
                    admin_ps = reader.GetString(0);
                
                }
            }
            return admin_ps;
        }
        public static int add_admin(string admin_id, string admin_ps)
        {
            if(InternetBarDAL.Admin_Info.IsAdmin(admin_id))
            {
                MessageBox.Show("改管理员账号已存在！");
                return 0;
            }
            InternetBarDAL.Admin_Info _admin_info = new InternetBarDAL.Admin_Info();
            _admin_info.AdminID = admin_id;
            _admin_info.AdminPassward = admin_ps;
           return InternetBarDAL.Admin_Info.AddAdmin(_admin_info);
        }

        public static int add_room(string room_id, string room_pos)
        {
            if (InternetBarDAL.Admin_Info.IsRoomExist(room_id))
            {
                MessageBox.Show("该房间编号已存在！");
                return 0;
            }
            return InternetBarDAL.Admin_Info.add_room(room_id,room_pos);
        }

        public static int add_computer(string computer_id, string computer_position)
        {
            if (InternetBarDAL.Admin_Info.IsComputerExist(computer_id))
            {
                
               
            }
            else if (!InternetBarDAL.Admin_Info.IsRoomExist(computer_position))
            {
                MessageBox.Show("改机房位置不存在！");
                return 0;
            }
                return InternetBarDAL.Admin_Info.add_computer(computer_id, computer_position);
        }
    }
}
