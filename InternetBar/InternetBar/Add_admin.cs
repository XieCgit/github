﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InternetBar
{
    public partial class Add_admin : Form
    {
        public Add_admin()
        {
            InitializeComponent();
        }

        private void Add_admin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "" && textBox2.Text.ToString() != "" && textBox3.Text.ToString() != "")
            {
                if (textBox2.Text == textBox3.Text)
                {
                    InternetBarBLL.admin.add_admin(textBox1.Text.ToString().Trim(), textBox2.Text.ToString().Trim());
                    MessageBox.Show("注册成功！");
                    this.Close();
                }
                    
                else
                {
                    MessageBox.Show("两次密码输入不一致！");
                    textBox2.Focus();
                }
            }
            else
            {
                MessageBox.Show("信息未输入完整，请重新输入！");
                textBox1.Focus();
            }
        }
    }
}
