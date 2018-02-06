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
    public partial class admin_register : Form
    {
        public admin_register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text != "" && this.textBox2.Text != "")
            {
                string admin_ps = InternetBarBLL.admin.admin_r(this.textBox1.Text.ToString().Trim());
                if (admin_ps != null)
                {
                    if (this.textBox2.Text.ToString().Trim() == admin_ps)
                    {
                        admin_main admin_m = new admin_main(this.textBox1.Text.ToString().Trim());   
                        admin_m.ShowDialog();
                        
                    }
                    else
                        MessageBox.Show("密码输入错误!");
                }
                else
                    MessageBox.Show("查无次管理员，请确认是否输入正确的管理员账号！");
            }
            else
            {
                MessageBox.Show("信息未输入完整，请重新输入！");
                textBox1.Focus();
            }
      
        }
    }
}
