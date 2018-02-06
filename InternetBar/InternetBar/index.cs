using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InternetBar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            admin_register admin_r = new admin_register();
            admin_r.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new register().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmd = @"F:&cd F:\C#窗体程序\InternetBar\InternetBar\bin\Debug\PythonFiles";
            string commend = "python face_recognition.py";
            string output = "";
            InternetBarBLL.CmdHelper.RunCmd(cmd, commend, out output);
            string b = "";
            string str1;
            InternetBarBLL.CmdHelper.GetInfo(output, out b, out str1);
            string face_id = str1;
            if (InternetBarDAL.User_Info.IsFaceExist(face_id))
            {
                string user_name = InternetBarBLL.user.name_by_face(face_id);
                string user_id = InternetBarBLL.user.id_by_face(face_id);
                main main = new main(user_id);
                main.Text = "Hello " + user_name;
                main.ShowDialog();
            }
            else
                MessageBox.Show(face_id);
        }
    }
}
