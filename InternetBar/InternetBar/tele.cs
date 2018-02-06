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
    public partial class tele : Form
    {
        private string User_id;
        public tele()
        {
            InitializeComponent();
        }
        public tele(string user_id)
        {
            this.User_id = user_id;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                InternetBarDAL.User_Info.ModifyTele(this.User_id, this.textBox1.Text.ToString().Trim());
                MessageBox.Show("修改成功!");
                this.Close();
            }
            else
            {
                MessageBox.Show("数据连接出错！");
                this.Close();
            }
            
        }
        private bool ValidateInput()
        {
            if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入电话号码！");
                textBox1.Clear();
                textBox1.Focus();
                return false;
            }

            return true;
        }
    }
}
