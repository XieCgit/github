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
    public partial class Add_room : Form
    {
        public Add_room()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "" && textBox2.Text.ToString() != "")
            {
                InternetBarBLL.admin.add_computer(textBox1.Text.ToString().Trim(), textBox2.Text.ToString().Trim());
                MessageBox.Show("添加成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("信息未输入完整，请重新输入！");
                textBox1.Focus();
            }
        }
    }
}
