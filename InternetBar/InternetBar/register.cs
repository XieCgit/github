using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InternetBar
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput() && IsAdult())
            {
                string userid = this.textBox1.Text;
                string username = this.textBox2.Text;
                string userage = this.textBox3.Text;
                string usergender = this.comboBox1.Text;
                string usertele = this.textBox5.Text;
                InternetBarBLL.user.RegisterUser(userid, username, userage, usergender, usertele);

                string cmd = @"F:&cd F:\C#窗体程序\InternetBar\InternetBar\bin\Debug\PythonFiles";
                string commend = "python get_face_data.py";
                string output = "";
                InternetBarBLL.CmdHelper.RunCmd(cmd, commend, out output);
                //string b = "";
                //InternetBarBLL.CmdHelper.GetInfo(output, out b);
                MessageBox.Show("ok");
            }


        }

        private bool ValidateInput()
        {
            if (this.textBox1.Text.Length == 0 || this.textBox2.Text.Length ==0 || this.textBox3.Text.Length == 0 || this.comboBox1.Text.Length == 0 || this.textBox5.Text.Length == 0)
            {
                MessageBox.Show("信息未输入完整，请重新输入！");
                return false;
            }

            return true;
        }
        public bool IsAdult()
        {
            if (int.Parse(this.textBox3.Text) < 18)
            {
                MessageBox.Show("未成年不允许注册！");
                return false;
            }
            return true;

        }
    }
}
