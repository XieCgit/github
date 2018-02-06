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
    public partial class Account : Form
    {
        private string User_id;
        public Account()
        {
            InitializeComponent();
        }

        public Account(string user_id)
        {
            this.User_id = user_id;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                InternetBarBLL.user.Recharge(this.User_id, double.Parse(textBox1.Text));
                MessageBox.Show("充值成功!");
                this.Close();
            }
            //   else
            //{
            //    MessageBox.Show("数据连接出错！");
            //    this.Close();
            //}

        }

        private bool ValidateInput()
        {
            if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入充值金额！");
                textBox1.Clear();
                textBox1.Focus();
                return false;
            }

            return true;
        }
    }
}
